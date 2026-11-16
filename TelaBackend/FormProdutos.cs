using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

// Lógica do painel administrativo (.NET Framework / C# 7.3).
// 1) Troque "FormPrincipal" pelo nome do seu form (ex.: Form1) e "Doceria.Admin" pelo namespace do seu Designer.
// 2) Os eventos são ligados AQUI no construtor: não ligue nada pelo Designer (senão executa em dobro).
// 3) Nomes (propriedade Name) que os controles precisam ter:
//    tabs (TabControl)
//    Produtos:  txtProdId, txtProdNome, txtProdPreco, cmbProdAtivo, txtProdDescricao, dgvProdutos,
//               btnAtualizarProduto, btnRemoverProduto
//    Pedidos:   txtPedidoId, btnCancelarPedido, txtPedidoCliente, btnFiltrarPedidos, dgvPedidos, dgvItens
//    Clientes:  txtCliId, txtCliNome, txtCliEmail, mskCliTelefone (MaskedTextBox), dgvClientes, dgvEnderecos,
//               btnAtualizarCliente, btnRemoverCliente
//    Admins:    txtAdmId, txtAdmNome, txtAdmEmail, mskAdmTelefone (MaskedTextBox), txtAdmSenha (NOVO),
//               dgvAdmins, btnAtualizarAdmin, btnAdicionarAdmin, btnRemoverAdmin
namespace TelaBackend
{
    public partial class FormProdutos : Form
    {
        Usuario logado;
        int? filtroCliente;

        public FormProdutos()
        {
            InitializeComponent();

            cmbProdAtivo.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbProdAtivo.Items.AddRange(new object[] { "Sim", "Não" });
            txtAdmSenha.UseSystemPasswordChar = true;

            foreach (var g in new[] { dgvProdutos, dgvPedidos, dgvItens, dgvClientes, dgvEnderecos, dgvAdmins })
                ConfigurarGrid(g);

            Load += async (s, e) => await Executar(CarregarTudo);
            tabs.SelectedIndexChanged += async (s, e) => await Executar(AtualizarAbaAtual);

            dgvProdutos.SelectionChanged += dgvProdutos_SelectionChanged;
            btnAtualizarProduto.Click += btnAtualizarProduto_Click;
            btnRemoverProduto.Click += btnRemoverProduto_Click;

            dgvPedidos.SelectionChanged += dgvPedidos_SelectionChanged;
            btnFiltrarPedidos.Click += btnFiltrarPedidos_Click;
            btnCancelarPedido.Click += btnCancelarPedido_Click;

            dgvClientes.SelectionChanged += dgvClientes_SelectionChanged;
            btnAtualizarCliente.Click += btnAtualizarCliente_Click;
            btnRemoverCliente.Click += btnRemoverCliente_Click;

            dgvAdmins.SelectionChanged += dgvAdmins_SelectionChanged;
            btnAtualizarAdmin.Click += btnAtualizarAdmin_Click;
            btnAdicionarAdmin.Click += btnAdicionarAdmin_Click;
            btnRemoverAdmin.Click += btnRemoverAdmin_Click;
        }

        public FormProdutos(Usuario usuarioLogado) : this()
        {
            logado = usuarioLogado;
        }

        // ===================== utilitários =====================

        async Task Executar(Func<Task> acao)
        {
            try { await acao(); }
            catch (Exception ex) { Aviso(ex.Message); }
        }

        static void Aviso(string msg)
        {
            MessageBox.Show(msg, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        static bool Confirmar(string msg)
        {
            return MessageBox.Show(msg, "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
        }

        static void ConfigurarGrid(DataGridView g)
        {
            g.ReadOnly = true;
            g.MultiSelect = false;
            g.AllowUserToAddRows = false;
            g.AllowUserToDeleteRows = false;
            g.RowHeadersVisible = false;
            g.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            g.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }

        static void Col(DataGridView g, string nome, string titulo, string formato = null)
        {
            var c = g.Columns[nome];
            if (c == null) return;
            c.HeaderText = titulo;
            if (formato != null) c.DefaultCellStyle.Format = formato;
        }

        static void Ocultar(DataGridView g, params string[] nomes)
        {
            foreach (var n in nomes)
                if (g.Columns[n] != null) g.Columns[n].Visible = false;
        }

        static void Selecionar(DataGridView g, string coluna, int id)
        {
            foreach (DataGridViewRow row in g.Rows)
            {
                if ((int)row.Cells[coluna].Value == id)
                {
                    g.CurrentCell = row.Cells[coluna];
                    row.Selected = true;
                    return;
                }
            }
        }

        static void SetTelefone(MaskedTextBox m, string valor)
        {
            m.Clear();
            if (string.IsNullOrWhiteSpace(valor)) return;
            try { m.Text = valor; } catch { }
        }

        async Task CarregarTudo()
        {
            await CarregarProdutos();
            await CarregarPedidos();
            await CarregarClientes();
            await CarregarAdmins();
        }

        async Task AtualizarAbaAtual()
        {
            switch (tabs.SelectedIndex)
            {
                case 0: await CarregarProdutos(); break;
                case 1: await CarregarPedidos(); break;
                case 2: await CarregarClientes(); break;
                case 3: await CarregarAdmins(); break;
            }
        }

        // ===================== PRODUTOS =====================

        async Task CarregarProdutos(int? manter = null)
        {
            dgvProdutos.DataSource = await Api.ListarProdutos();
            Col(dgvProdutos, "IdProduto", "ID");
            Col(dgvProdutos, "Descricao", "Descrição");
            Col(dgvProdutos, "PrecoUnitario", "Preço", "C2");
            if (manter != null) Selecionar(dgvProdutos, "IdProduto", manter.Value);
        }

        void dgvProdutos_SelectionChanged(object sender, EventArgs e)
        {
            var p = dgvProdutos.CurrentRow?.DataBoundItem as Produto;
            txtProdId.Text = p == null ? "" : p.IdProduto.ToString();
            txtProdNome.Text = p == null ? "" : p.Nome;
            txtProdPreco.Text = p == null ? "" : p.PrecoUnitario.ToString("0.00");
            txtProdDescricao.Text = p == null ? "" : p.Descricao;
            cmbProdAtivo.SelectedIndex = p == null ? -1 : (p.Ativo ? 0 : 1);
        }

        async void btnAtualizarProduto_Click(object sender, EventArgs e)
        {
            await Executar(async () =>
            {
                int id;
                if (!int.TryParse(txtProdId.Text, out id)) { Aviso("Selecione um produto na lista."); return; }
                if (string.IsNullOrWhiteSpace(txtProdNome.Text)) { Aviso("Informe o nome do produto."); return; }
                decimal preco;
                if (!decimal.TryParse(txtProdPreco.Text, out preco) || preco < 0) { Aviso("Informe um preço válido."); return; }
                if (cmbProdAtivo.SelectedIndex < 0) { Aviso("Escolha se o produto está ativo."); return; }

                await Api.SalvarProduto(new Produto
                {
                    IdProduto = id,
                    Nome = txtProdNome.Text.Trim(),
                    Descricao = txtProdDescricao.Text,
                    PrecoUnitario = preco,
                    Ativo = cmbProdAtivo.SelectedIndex == 0
                });
                await CarregarProdutos(id);
            });
        }

        async void btnRemoverProduto_Click(object sender, EventArgs e)
        {
            await Executar(async () =>
            {
                int id;
                if (!int.TryParse(txtProdId.Text, out id)) { Aviso("Selecione um produto na lista."); return; }
                if (!Confirmar("Excluir o produto \"" + txtProdNome.Text + "\"?")) return;
                await Api.ExcluirProduto(id);
                await CarregarProdutos();
            });
        }

        // ===================== PEDIDOS =====================

        async Task CarregarPedidos(int? manter = null)
        {
            dgvPedidos.DataSource = await Api.ListarPedidos(filtroCliente);
            Col(dgvPedidos, "IdPedido", "Pedido");
            Col(dgvPedidos, "IdUsuario", "Id Cliente");
            Col(dgvPedidos, "ValorTotal", "Total", "C2");
            Col(dgvPedidos, "DataPedido", "Data do pedido", "dd/MM/yyyy HH:mm");
            Col(dgvPedidos, "DataEntrega", "Entrega", "dd/MM/yyyy");
            Col(dgvPedidos, "TurnoEntrega", "Turno");
            if (manter != null) Selecionar(dgvPedidos, "IdPedido", manter.Value);
        }

        async void dgvPedidos_SelectionChanged(object sender, EventArgs e)
        {
            var ped = dgvPedidos.CurrentRow?.DataBoundItem as Pedido;
            txtPedidoId.Text = ped == null ? "" : ped.IdPedido.ToString();
            if (ped == null) { dgvItens.DataSource = null; return; }

            await Executar(async () =>
            {
                var itens = await Api.ListarItensPedido(ped.IdPedido);
                if (txtPedidoId.Text != ped.IdPedido.ToString()) return; // a seleção mudou enquanto carregava
                dgvItens.DataSource = itens;
                Ocultar(dgvItens, "IdItem", "IdPedido", "IdProduto");
                Col(dgvItens, "PrecoUnitario", "Preço unit.", "C2");
            });
        }

        async void btnFiltrarPedidos_Click(object sender, EventArgs e)
        {
            await Executar(async () =>
            {
                var txt = txtPedidoCliente.Text.Trim();
                if (txt == "")
                {
                    filtroCliente = null; // vazio = mostra todos
                }
                else
                {
                    int id;
                    if (!int.TryParse(txt, out id)) { Aviso("Informe o id do cliente (número)."); return; }
                    filtroCliente = id;
                }
                await CarregarPedidos();
                if (filtroCliente != null && dgvPedidos.Rows.Count == 0)
                    Aviso("Nenhum pedido encontrado para esse cliente.");
            });
        }

        async void btnCancelarPedido_Click(object sender, EventArgs e)
        {
            await Executar(async () =>
            {
                int id;
                if (!int.TryParse(txtPedidoId.Text, out id)) { Aviso("Selecione um pedido na lista."); return; }
                if (!Confirmar("Cancelar o pedido " + id + "? Ele será apagado junto com seus itens.")) return;
                await Api.CancelarPedido(id);
                await CarregarPedidos();
            });
        }

        // ===================== CLIENTES =====================

        async Task CarregarClientes(int? manter = null)
        {
            dgvClientes.DataSource = await Api.ListarClientes();
            Ocultar(dgvClientes, "TipoConta");
            Col(dgvClientes, "IdUsuario", "ID");
            Col(dgvClientes, "Email", "E-mail");
            Col(dgvClientes, "DataCadastro", "Cadastro", "dd/MM/yyyy");
            if (manter != null) Selecionar(dgvClientes, "IdUsuario", manter.Value);
        }

        async void dgvClientes_SelectionChanged(object sender, EventArgs e)
        {
            var c = dgvClientes.CurrentRow?.DataBoundItem as Usuario;
            txtCliId.Text = c == null ? "" : c.IdUsuario.ToString();
            txtCliNome.Text = c == null ? "" : c.Nome;
            txtCliEmail.Text = c == null ? "" : c.Email;
            SetTelefone(mskCliTelefone, c == null ? null : c.Telefone);
            if (c == null) { dgvEnderecos.DataSource = null; return; }

            await Executar(async () =>
            {
                var enderecos = await Api.ListarEnderecos(c.IdUsuario);
                if (txtCliId.Text != c.IdUsuario.ToString()) return;
                dgvEnderecos.DataSource = enderecos;
                Ocultar(dgvEnderecos, "IdEndereco");
            });
        }

        async void btnAtualizarCliente_Click(object sender, EventArgs e)
        {
            await Executar(async () =>
            {
                int id;
                if (!int.TryParse(txtCliId.Text, out id)) { Aviso("Selecione um cliente na lista."); return; }
                if (string.IsNullOrWhiteSpace(txtCliNome.Text)) { Aviso("Informe o nome."); return; }
                if (!txtCliEmail.Text.Contains("@")) { Aviso("Informe um e-mail válido."); return; }
                if (!mskCliTelefone.MaskCompleted) { Aviso("Preencha o telefone completo."); return; }

                await Api.AtualizarUsuario(new Usuario
                {
                    IdUsuario = id,
                    Nome = txtCliNome.Text.Trim(),
                    Email = txtCliEmail.Text.Trim(),
                    Telefone = mskCliTelefone.Text
                });
                await CarregarClientes(id);
            });
        }

        async void btnRemoverCliente_Click(object sender, EventArgs e)
        {
            await Executar(async () =>
            {
                int id;
                if (!int.TryParse(txtCliId.Text, out id)) { Aviso("Selecione um cliente na lista."); return; }
                if (!Confirmar("Remover o cliente \"" + txtCliNome.Text + "\"? Os endereços dele também serão apagados.")) return;
                await Api.RemoverUsuario(id);
                await CarregarClientes();
            });
        }

        // ===================== ADMINISTRADORES =====================

        void LimparAdmin()
        {
            txtAdmId.Clear();
            txtAdmNome.Clear();
            txtAdmEmail.Clear();
            mskAdmTelefone.Clear();
            txtAdmSenha.Clear();
        }

        // manter == null: deixa os campos vazios (pronto para "Adicionar Administrador")
        async Task CarregarAdmins(int? manter = null)
        {
            dgvAdmins.DataSource = await Api.ListarAdministradores();
            Ocultar(dgvAdmins, "TipoConta");
            Col(dgvAdmins, "IdUsuario", "ID");
            Col(dgvAdmins, "Email", "E-mail");
            Col(dgvAdmins, "DataCadastro", "Cadastro", "dd/MM/yyyy");
            if (manter != null)
            {
                Selecionar(dgvAdmins, "IdUsuario", manter.Value);
            }
            else
            {
                dgvAdmins.CurrentCell = null;
                dgvAdmins.ClearSelection();
                LimparAdmin();
            }
        }

        void dgvAdmins_SelectionChanged(object sender, EventArgs e)
        {
            var a = dgvAdmins.CurrentRow?.DataBoundItem as Usuario;
            if (a == null || !dgvAdmins.CurrentRow.Selected) { LimparAdmin(); return; }
            txtAdmId.Text = a.IdUsuario.ToString();
            txtAdmNome.Text = a.Nome;
            txtAdmEmail.Text = a.Email;
            SetTelefone(mskAdmTelefone, a.Telefone);
            txtAdmSenha.Clear();
        }

        async void btnAtualizarAdmin_Click(object sender, EventArgs e)
        {
            await Executar(async () =>
            {
                int id;
                if (!int.TryParse(txtAdmId.Text, out id)) { Aviso("Selecione um administrador na lista."); return; }
                if (string.IsNullOrWhiteSpace(txtAdmNome.Text)) { Aviso("Informe o nome."); return; }
                if (!txtAdmEmail.Text.Contains("@")) { Aviso("Informe um e-mail válido."); return; }
                if (!mskAdmTelefone.MaskCompleted) { Aviso("Preencha o telefone completo."); return; }

                await Api.AtualizarUsuario(new Usuario
                {
                    IdUsuario = id,
                    Nome = txtAdmNome.Text.Trim(),
                    Email = txtAdmEmail.Text.Trim(),
                    Telefone = mskAdmTelefone.Text
                });
                await CarregarAdmins(id);
            });
        }

        async void btnAdicionarAdmin_Click(object sender, EventArgs e)
        {
            await Executar(async () =>
            {
                if (string.IsNullOrWhiteSpace(txtAdmNome.Text)) { Aviso("Informe o nome."); return; }
                if (!txtAdmEmail.Text.Contains("@")) { Aviso("Informe um e-mail válido."); return; }
                if (!mskAdmTelefone.MaskCompleted) { Aviso("Preencha o telefone completo."); return; }
                if (txtAdmSenha.Text.Length < 4) { Aviso("A senha deve ter ao menos 4 caracteres."); return; }

                await Api.AdicionarAdministrador(txtAdmNome.Text.Trim(), txtAdmEmail.Text.Trim(),
                                                 mskAdmTelefone.Text, txtAdmSenha.Text);
                await CarregarAdmins(); // limpa os campos
                MessageBox.Show("Administrador adicionado.", "Pronto", MessageBoxButtons.OK, MessageBoxIcon.Information);
            });
        }

        async void btnRemoverAdmin_Click(object sender, EventArgs e)
        {
            await Executar(async () =>
            {
                int id;
                if (!int.TryParse(txtAdmId.Text, out id)) { Aviso("Selecione um administrador na lista."); return; }
                if (logado != null && id == logado.IdUsuario)
                {
                    Aviso("Você não pode remover o administrador que está logado.");
                    return;
                }
                if (!Confirmar("Remover o administrador \"" + txtAdmNome.Text + "\"?")) return;
                await Api.RemoverUsuario(id);
                await CarregarAdmins();
            });
        }
    }
}