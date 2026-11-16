namespace TelaBackend
{
    partial class FormProdutos
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabAdministradores = new System.Windows.Forms.TabPage();
            this.dgvAdmins = new System.Windows.Forms.DataGridView();
            this.txtAdmId = new System.Windows.Forms.TextBox();
            this.lblIdAdmin = new System.Windows.Forms.Label();
            this.txtAdmNome = new System.Windows.Forms.TextBox();
            this.lblNomeAdmin = new System.Windows.Forms.Label();
            this.btnRemoverAdmin = new System.Windows.Forms.Button();
            this.lblGridAdmins = new System.Windows.Forms.Label();
            this.txtAdmEmail = new System.Windows.Forms.TextBox();
            this.lblEmailAdmin = new System.Windows.Forms.Label();
            this.lblTelefoneAdmin = new System.Windows.Forms.Label();
            this.mskAdmTelefone = new System.Windows.Forms.MaskedTextBox();
            this.btnAtualizarAdmin = new System.Windows.Forms.Button();
            this.btnAdicionarAdmin = new System.Windows.Forms.Button();
            this.txtAdmSenha = new System.Windows.Forms.TextBox();
            this.lblAdmSenha = new System.Windows.Forms.Label();
            this.tabClientes = new System.Windows.Forms.TabPage();
            this.dgvClientes = new System.Windows.Forms.DataGridView();
            this.txtCliId = new System.Windows.Forms.TextBox();
            this.lblIdCliente = new System.Windows.Forms.Label();
            this.txtCliNome = new System.Windows.Forms.TextBox();
            this.lblNomeCliente = new System.Windows.Forms.Label();
            this.btnRemoverCliente = new System.Windows.Forms.Button();
            this.lblGridClientes = new System.Windows.Forms.Label();
            this.dgvEnderecos = new System.Windows.Forms.DataGridView();
            this.lblGridEnderecos = new System.Windows.Forms.Label();
            this.txtCliEmail = new System.Windows.Forms.TextBox();
            this.lblEmailCliente = new System.Windows.Forms.Label();
            this.lblTelefoneCliente = new System.Windows.Forms.Label();
            this.mskCliTelefone = new System.Windows.Forms.MaskedTextBox();
            this.btnAtualizarCliente = new System.Windows.Forms.Button();
            this.tabPedidos = new System.Windows.Forms.TabPage();
            this.dgvPedidos = new System.Windows.Forms.DataGridView();
            this.txtPedidoId = new System.Windows.Forms.TextBox();
            this.lblIdPedido = new System.Windows.Forms.Label();
            this.txtPedidoCliente = new System.Windows.Forms.TextBox();
            this.lblIdClientePedido = new System.Windows.Forms.Label();
            this.btnCancelarPedido = new System.Windows.Forms.Button();
            this.lblGridPedidos = new System.Windows.Forms.Label();
            this.dgvItens = new System.Windows.Forms.DataGridView();
            this.lblItensPedido = new System.Windows.Forms.Label();
            this.btnFiltrarPedidos = new System.Windows.Forms.Button();
            this.tabProdutos = new System.Windows.Forms.TabPage();
            this.dgvProdutos = new System.Windows.Forms.DataGridView();
            this.txtProdId = new System.Windows.Forms.TextBox();
            this.lblId = new System.Windows.Forms.Label();
            this.txtProdNome = new System.Windows.Forms.TextBox();
            this.lblNomeProduto = new System.Windows.Forms.Label();
            this.txtProdPreco = new System.Windows.Forms.TextBox();
            this.lblPrecoProduto = new System.Windows.Forms.Label();
            this.lblAtivoProduto = new System.Windows.Forms.Label();
            this.cmbProdAtivo = new System.Windows.Forms.ComboBox();
            this.txtProdDescricao = new System.Windows.Forms.TextBox();
            this.lblDescricaoProduto = new System.Windows.Forms.Label();
            this.btnAtualizarProduto = new System.Windows.Forms.Button();
            this.btnRemoverProduto = new System.Windows.Forms.Button();
            this.lblGridProdutos = new System.Windows.Forms.Label();
            this.tabs = new System.Windows.Forms.TabControl();
            this.tabAdministradores.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAdmins)).BeginInit();
            this.tabClientes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvClientes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEnderecos)).BeginInit();
            this.tabPedidos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPedidos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItens)).BeginInit();
            this.tabProdutos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProdutos)).BeginInit();
            this.tabs.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabAdministradores
            // 
            this.tabAdministradores.Controls.Add(this.lblAdmSenha);
            this.tabAdministradores.Controls.Add(this.txtAdmSenha);
            this.tabAdministradores.Controls.Add(this.txtAdmEmail);
            this.tabAdministradores.Controls.Add(this.txtAdmNome);
            this.tabAdministradores.Controls.Add(this.txtAdmId);
            this.tabAdministradores.Controls.Add(this.btnAdicionarAdmin);
            this.tabAdministradores.Controls.Add(this.btnAtualizarAdmin);
            this.tabAdministradores.Controls.Add(this.mskAdmTelefone);
            this.tabAdministradores.Controls.Add(this.lblTelefoneAdmin);
            this.tabAdministradores.Controls.Add(this.lblEmailAdmin);
            this.tabAdministradores.Controls.Add(this.lblGridAdmins);
            this.tabAdministradores.Controls.Add(this.btnRemoverAdmin);
            this.tabAdministradores.Controls.Add(this.lblNomeAdmin);
            this.tabAdministradores.Controls.Add(this.lblIdAdmin);
            this.tabAdministradores.Controls.Add(this.dgvAdmins);
            this.tabAdministradores.Location = new System.Drawing.Point(4, 22);
            this.tabAdministradores.Name = "tabAdministradores";
            this.tabAdministradores.Size = new System.Drawing.Size(791, 423);
            this.tabAdministradores.TabIndex = 3;
            this.tabAdministradores.Text = "Administradores";
            this.tabAdministradores.UseVisualStyleBackColor = true;
            // 
            // dgvAdmins
            // 
            this.dgvAdmins.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAdmins.Location = new System.Drawing.Point(405, 43);
            this.dgvAdmins.Name = "dgvAdmins";
            this.dgvAdmins.Size = new System.Drawing.Size(366, 164);
            this.dgvAdmins.TabIndex = 71;
            // 
            // txtAdmId
            // 
            this.txtAdmId.Location = new System.Drawing.Point(22, 69);
            this.txtAdmId.Name = "txtAdmId";
            this.txtAdmId.ReadOnly = true;
            this.txtAdmId.Size = new System.Drawing.Size(94, 20);
            this.txtAdmId.TabIndex = 72;
            // 
            // lblIdAdmin
            // 
            this.lblIdAdmin.AutoSize = true;
            this.lblIdAdmin.Location = new System.Drawing.Point(19, 43);
            this.lblIdAdmin.Name = "lblIdAdmin";
            this.lblIdAdmin.Size = new System.Drawing.Size(97, 13);
            this.lblIdAdmin.TabIndex = 73;
            this.lblIdAdmin.Text = "Id do Administrador";
            // 
            // txtAdmNome
            // 
            this.txtAdmNome.Location = new System.Drawing.Point(23, 138);
            this.txtAdmNome.Name = "txtAdmNome";
            this.txtAdmNome.Size = new System.Drawing.Size(94, 20);
            this.txtAdmNome.TabIndex = 74;
            // 
            // lblNomeAdmin
            // 
            this.lblNomeAdmin.AutoSize = true;
            this.lblNomeAdmin.Location = new System.Drawing.Point(20, 112);
            this.lblNomeAdmin.Name = "lblNomeAdmin";
            this.lblNomeAdmin.Size = new System.Drawing.Size(35, 13);
            this.lblNomeAdmin.TabIndex = 75;
            this.lblNomeAdmin.Text = "Nome";
            // 
            // btnRemoverAdmin
            // 
            this.btnRemoverAdmin.Location = new System.Drawing.Point(654, 307);
            this.btnRemoverAdmin.Name = "btnRemoverAdmin";
            this.btnRemoverAdmin.Size = new System.Drawing.Size(117, 65);
            this.btnRemoverAdmin.TabIndex = 76;
            this.btnRemoverAdmin.Text = "Remover Admin";
            this.btnRemoverAdmin.UseVisualStyleBackColor = true;
            // 
            // lblGridAdmins
            // 
            this.lblGridAdmins.AutoSize = true;
            this.lblGridAdmins.Location = new System.Drawing.Point(402, 27);
            this.lblGridAdmins.Name = "lblGridAdmins";
            this.lblGridAdmins.Size = new System.Drawing.Size(81, 13);
            this.lblGridAdmins.TabIndex = 77;
            this.lblGridAdmins.Text = "Administradores";
            // 
            // txtAdmEmail
            // 
            this.txtAdmEmail.Location = new System.Drawing.Point(176, 69);
            this.txtAdmEmail.Name = "txtAdmEmail";
            this.txtAdmEmail.Size = new System.Drawing.Size(94, 20);
            this.txtAdmEmail.TabIndex = 80;
            // 
            // lblEmailAdmin
            // 
            this.lblEmailAdmin.AutoSize = true;
            this.lblEmailAdmin.Location = new System.Drawing.Point(173, 43);
            this.lblEmailAdmin.Name = "lblEmailAdmin";
            this.lblEmailAdmin.Size = new System.Drawing.Size(32, 13);
            this.lblEmailAdmin.TabIndex = 81;
            this.lblEmailAdmin.Text = "Email";
            // 
            // lblTelefoneAdmin
            // 
            this.lblTelefoneAdmin.AutoSize = true;
            this.lblTelefoneAdmin.Location = new System.Drawing.Point(176, 112);
            this.lblTelefoneAdmin.Name = "lblTelefoneAdmin";
            this.lblTelefoneAdmin.Size = new System.Drawing.Size(49, 13);
            this.lblTelefoneAdmin.TabIndex = 82;
            this.lblTelefoneAdmin.Text = "Telefone";
            // 
            // mskAdmTelefone
            // 
            this.mskAdmTelefone.Location = new System.Drawing.Point(179, 138);
            this.mskAdmTelefone.Mask = "(99) 00000-0000";
            this.mskAdmTelefone.Name = "mskAdmTelefone";
            this.mskAdmTelefone.Size = new System.Drawing.Size(90, 20);
            this.mskAdmTelefone.TabIndex = 83;
            // 
            // btnAtualizarAdmin
            // 
            this.btnAtualizarAdmin.Location = new System.Drawing.Point(22, 307);
            this.btnAtualizarAdmin.Name = "btnAtualizarAdmin";
            this.btnAtualizarAdmin.Size = new System.Drawing.Size(117, 65);
            this.btnAtualizarAdmin.TabIndex = 84;
            this.btnAtualizarAdmin.Text = "Atualizar Dados";
            this.btnAtualizarAdmin.UseVisualStyleBackColor = true;
            // 
            // btnAdicionarAdmin
            // 
            this.btnAdicionarAdmin.Location = new System.Drawing.Point(176, 307);
            this.btnAdicionarAdmin.Name = "btnAdicionarAdmin";
            this.btnAdicionarAdmin.Size = new System.Drawing.Size(117, 65);
            this.btnAdicionarAdmin.TabIndex = 85;
            this.btnAdicionarAdmin.Text = "Adicionar Administrador";
            this.btnAdicionarAdmin.UseVisualStyleBackColor = true;
            // 
            // txtAdmSenha
            // 
            this.txtAdmSenha.Location = new System.Drawing.Point(23, 209);
            this.txtAdmSenha.Name = "txtAdmSenha";
            this.txtAdmSenha.Size = new System.Drawing.Size(94, 20);
            this.txtAdmSenha.TabIndex = 86;
            // 
            // lblAdmSenha
            // 
            this.lblAdmSenha.AutoSize = true;
            this.lblAdmSenha.Location = new System.Drawing.Point(20, 183);
            this.lblAdmSenha.Name = "lblAdmSenha";
            this.lblAdmSenha.Size = new System.Drawing.Size(38, 13);
            this.lblAdmSenha.TabIndex = 87;
            this.lblAdmSenha.Text = "Senha";
            // 
            // tabClientes
            // 
            this.tabClientes.Controls.Add(this.btnAtualizarCliente);
            this.tabClientes.Controls.Add(this.mskCliTelefone);
            this.tabClientes.Controls.Add(this.lblTelefoneCliente);
            this.tabClientes.Controls.Add(this.lblEmailCliente);
            this.tabClientes.Controls.Add(this.txtCliEmail);
            this.tabClientes.Controls.Add(this.txtCliNome);
            this.tabClientes.Controls.Add(this.txtCliId);
            this.tabClientes.Controls.Add(this.lblGridEnderecos);
            this.tabClientes.Controls.Add(this.dgvEnderecos);
            this.tabClientes.Controls.Add(this.lblGridClientes);
            this.tabClientes.Controls.Add(this.btnRemoverCliente);
            this.tabClientes.Controls.Add(this.lblNomeCliente);
            this.tabClientes.Controls.Add(this.lblIdCliente);
            this.tabClientes.Controls.Add(this.dgvClientes);
            this.tabClientes.Location = new System.Drawing.Point(4, 22);
            this.tabClientes.Name = "tabClientes";
            this.tabClientes.Size = new System.Drawing.Size(791, 423);
            this.tabClientes.TabIndex = 2;
            this.tabClientes.Text = "Clientes";
            this.tabClientes.UseVisualStyleBackColor = true;
            // 
            // dgvClientes
            // 
            this.dgvClientes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvClientes.Location = new System.Drawing.Point(405, 43);
            this.dgvClientes.Name = "dgvClientes";
            this.dgvClientes.Size = new System.Drawing.Size(366, 164);
            this.dgvClientes.TabIndex = 56;
            // 
            // txtCliId
            // 
            this.txtCliId.Location = new System.Drawing.Point(22, 69);
            this.txtCliId.Name = "txtCliId";
            this.txtCliId.ReadOnly = true;
            this.txtCliId.Size = new System.Drawing.Size(94, 20);
            this.txtCliId.TabIndex = 57;
            // 
            // lblIdCliente
            // 
            this.lblIdCliente.AutoSize = true;
            this.lblIdCliente.Location = new System.Drawing.Point(19, 43);
            this.lblIdCliente.Name = "lblIdCliente";
            this.lblIdCliente.Size = new System.Drawing.Size(66, 13);
            this.lblIdCliente.TabIndex = 58;
            this.lblIdCliente.Text = "Id do Cliente";
            // 
            // txtCliNome
            // 
            this.txtCliNome.Location = new System.Drawing.Point(23, 138);
            this.txtCliNome.Name = "txtCliNome";
            this.txtCliNome.Size = new System.Drawing.Size(94, 20);
            this.txtCliNome.TabIndex = 59;
            // 
            // lblNomeCliente
            // 
            this.lblNomeCliente.AutoSize = true;
            this.lblNomeCliente.Location = new System.Drawing.Point(20, 112);
            this.lblNomeCliente.Name = "lblNomeCliente";
            this.lblNomeCliente.Size = new System.Drawing.Size(35, 13);
            this.lblNomeCliente.TabIndex = 60;
            this.lblNomeCliente.Text = "Nome";
            // 
            // btnRemoverCliente
            // 
            this.btnRemoverCliente.Location = new System.Drawing.Point(22, 313);
            this.btnRemoverCliente.Name = "btnRemoverCliente";
            this.btnRemoverCliente.Size = new System.Drawing.Size(117, 65);
            this.btnRemoverCliente.TabIndex = 61;
            this.btnRemoverCliente.Text = "Remover Cliente";
            this.btnRemoverCliente.UseVisualStyleBackColor = true;
            // 
            // lblGridClientes
            // 
            this.lblGridClientes.AutoSize = true;
            this.lblGridClientes.Location = new System.Drawing.Point(402, 27);
            this.lblGridClientes.Name = "lblGridClientes";
            this.lblGridClientes.Size = new System.Drawing.Size(44, 13);
            this.lblGridClientes.TabIndex = 62;
            this.lblGridClientes.Text = "Clientes";
            // 
            // dgvEnderecos
            // 
            this.dgvEnderecos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvEnderecos.Location = new System.Drawing.Point(405, 232);
            this.dgvEnderecos.Name = "dgvEnderecos";
            this.dgvEnderecos.Size = new System.Drawing.Size(366, 164);
            this.dgvEnderecos.TabIndex = 63;
            // 
            // lblGridEnderecos
            // 
            this.lblGridEnderecos.AutoSize = true;
            this.lblGridEnderecos.Location = new System.Drawing.Point(402, 216);
            this.lblGridEnderecos.Name = "lblGridEnderecos";
            this.lblGridEnderecos.Size = new System.Drawing.Size(108, 13);
            this.lblGridEnderecos.TabIndex = 64;
            this.lblGridEnderecos.Text = "Endereços do Cliente";
            // 
            // txtCliEmail
            // 
            this.txtCliEmail.Location = new System.Drawing.Point(176, 69);
            this.txtCliEmail.Name = "txtCliEmail";
            this.txtCliEmail.Size = new System.Drawing.Size(94, 20);
            this.txtCliEmail.TabIndex = 65;
            // 
            // lblEmailCliente
            // 
            this.lblEmailCliente.AutoSize = true;
            this.lblEmailCliente.Location = new System.Drawing.Point(173, 43);
            this.lblEmailCliente.Name = "lblEmailCliente";
            this.lblEmailCliente.Size = new System.Drawing.Size(32, 13);
            this.lblEmailCliente.TabIndex = 66;
            this.lblEmailCliente.Text = "Email";
            // 
            // lblTelefoneCliente
            // 
            this.lblTelefoneCliente.AutoSize = true;
            this.lblTelefoneCliente.Location = new System.Drawing.Point(176, 97);
            this.lblTelefoneCliente.Name = "lblTelefoneCliente";
            this.lblTelefoneCliente.Size = new System.Drawing.Size(49, 13);
            this.lblTelefoneCliente.TabIndex = 68;
            this.lblTelefoneCliente.Text = "Telefone";
            // 
            // mskCliTelefone
            // 
            this.mskCliTelefone.Location = new System.Drawing.Point(179, 131);
            this.mskCliTelefone.Mask = "(99) 00000-0000";
            this.mskCliTelefone.Name = "mskCliTelefone";
            this.mskCliTelefone.Size = new System.Drawing.Size(90, 20);
            this.mskCliTelefone.TabIndex = 69;
            // 
            // btnAtualizarCliente
            // 
            this.btnAtualizarCliente.Location = new System.Drawing.Point(22, 190);
            this.btnAtualizarCliente.Name = "btnAtualizarCliente";
            this.btnAtualizarCliente.Size = new System.Drawing.Size(117, 65);
            this.btnAtualizarCliente.TabIndex = 70;
            this.btnAtualizarCliente.Text = "Atualizar Dados";
            this.btnAtualizarCliente.UseVisualStyleBackColor = true;
            // 
            // tabPedidos
            // 
            this.tabPedidos.Controls.Add(this.btnFiltrarPedidos);
            this.tabPedidos.Controls.Add(this.lblItensPedido);
            this.tabPedidos.Controls.Add(this.dgvItens);
            this.tabPedidos.Controls.Add(this.lblGridPedidos);
            this.tabPedidos.Controls.Add(this.btnCancelarPedido);
            this.tabPedidos.Controls.Add(this.lblIdClientePedido);
            this.tabPedidos.Controls.Add(this.txtPedidoCliente);
            this.tabPedidos.Controls.Add(this.txtPedidoId);
            this.tabPedidos.Controls.Add(this.lblIdPedido);
            this.tabPedidos.Controls.Add(this.dgvPedidos);
            this.tabPedidos.Location = new System.Drawing.Point(4, 22);
            this.tabPedidos.Name = "tabPedidos";
            this.tabPedidos.Padding = new System.Windows.Forms.Padding(3);
            this.tabPedidos.Size = new System.Drawing.Size(791, 423);
            this.tabPedidos.TabIndex = 1;
            this.tabPedidos.Text = "Pedidos";
            this.tabPedidos.UseVisualStyleBackColor = true;
            // 
            // dgvPedidos
            // 
            this.dgvPedidos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPedidos.Location = new System.Drawing.Point(406, 42);
            this.dgvPedidos.Name = "dgvPedidos";
            this.dgvPedidos.Size = new System.Drawing.Size(366, 164);
            this.dgvPedidos.TabIndex = 38;
            // 
            // txtPedidoId
            // 
            this.txtPedidoId.Location = new System.Drawing.Point(33, 104);
            this.txtPedidoId.Name = "txtPedidoId";
            this.txtPedidoId.ReadOnly = true;
            this.txtPedidoId.Size = new System.Drawing.Size(94, 20);
            this.txtPedidoId.TabIndex = 39;
            // 
            // lblIdPedido
            // 
            this.lblIdPedido.AutoSize = true;
            this.lblIdPedido.Location = new System.Drawing.Point(30, 78);
            this.lblIdPedido.Name = "lblIdPedido";
            this.lblIdPedido.Size = new System.Drawing.Size(66, 13);
            this.lblIdPedido.TabIndex = 40;
            this.lblIdPedido.Text = "id do Pedido";
            // 
            // txtPedidoCliente
            // 
            this.txtPedidoCliente.Location = new System.Drawing.Point(34, 302);
            this.txtPedidoCliente.Name = "txtPedidoCliente";
            this.txtPedidoCliente.Size = new System.Drawing.Size(94, 20);
            this.txtPedidoCliente.TabIndex = 41;
            // 
            // lblIdClientePedido
            // 
            this.lblIdClientePedido.AutoSize = true;
            this.lblIdClientePedido.Location = new System.Drawing.Point(31, 276);
            this.lblIdClientePedido.Name = "lblIdClientePedido";
            this.lblIdClientePedido.Size = new System.Drawing.Size(65, 13);
            this.lblIdClientePedido.TabIndex = 42;
            this.lblIdClientePedido.Text = "id do Cliente";
            // 
            // btnCancelarPedido
            // 
            this.btnCancelarPedido.Location = new System.Drawing.Point(171, 59);
            this.btnCancelarPedido.Name = "btnCancelarPedido";
            this.btnCancelarPedido.Size = new System.Drawing.Size(117, 65);
            this.btnCancelarPedido.TabIndex = 50;
            this.btnCancelarPedido.Text = "Cancelar Pedido";
            this.btnCancelarPedido.UseVisualStyleBackColor = true;
            // 
            // lblGridPedidos
            // 
            this.lblGridPedidos.AutoSize = true;
            this.lblGridPedidos.Location = new System.Drawing.Point(403, 26);
            this.lblGridPedidos.Name = "lblGridPedidos";
            this.lblGridPedidos.Size = new System.Drawing.Size(45, 13);
            this.lblGridPedidos.TabIndex = 51;
            this.lblGridPedidos.Text = "Pedidos";
            // 
            // dgvItens
            // 
            this.dgvItens.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvItens.Location = new System.Drawing.Point(406, 231);
            this.dgvItens.Name = "dgvItens";
            this.dgvItens.Size = new System.Drawing.Size(366, 164);
            this.dgvItens.TabIndex = 52;
            // 
            // lblItensPedido
            // 
            this.lblItensPedido.AutoSize = true;
            this.lblItensPedido.Location = new System.Drawing.Point(403, 215);
            this.lblItensPedido.Name = "lblItensPedido";
            this.lblItensPedido.Size = new System.Drawing.Size(71, 13);
            this.lblItensPedido.TabIndex = 53;
            this.lblItensPedido.Text = "Itens Pedidos";
            // 
            // btnFiltrarPedidos
            // 
            this.btnFiltrarPedidos.Location = new System.Drawing.Point(171, 257);
            this.btnFiltrarPedidos.Name = "btnFiltrarPedidos";
            this.btnFiltrarPedidos.Size = new System.Drawing.Size(117, 65);
            this.btnFiltrarPedidos.TabIndex = 54;
            this.btnFiltrarPedidos.Text = "Filtrar por Cliente";
            this.btnFiltrarPedidos.UseVisualStyleBackColor = true;
            // 
            // tabProdutos
            // 
            this.tabProdutos.Controls.Add(this.lblGridProdutos);
            this.tabProdutos.Controls.Add(this.btnRemoverProduto);
            this.tabProdutos.Controls.Add(this.btnAtualizarProduto);
            this.tabProdutos.Controls.Add(this.lblDescricaoProduto);
            this.tabProdutos.Controls.Add(this.txtProdDescricao);
            this.tabProdutos.Controls.Add(this.txtProdPreco);
            this.tabProdutos.Controls.Add(this.txtProdNome);
            this.tabProdutos.Controls.Add(this.txtProdId);
            this.tabProdutos.Controls.Add(this.cmbProdAtivo);
            this.tabProdutos.Controls.Add(this.lblAtivoProduto);
            this.tabProdutos.Controls.Add(this.lblPrecoProduto);
            this.tabProdutos.Controls.Add(this.lblNomeProduto);
            this.tabProdutos.Controls.Add(this.lblId);
            this.tabProdutos.Controls.Add(this.dgvProdutos);
            this.tabProdutos.Location = new System.Drawing.Point(4, 22);
            this.tabProdutos.Name = "tabProdutos";
            this.tabProdutos.Padding = new System.Windows.Forms.Padding(3);
            this.tabProdutos.Size = new System.Drawing.Size(791, 423);
            this.tabProdutos.TabIndex = 0;
            this.tabProdutos.Text = "Produtos";
            this.tabProdutos.UseVisualStyleBackColor = true;
            // 
            // dgvProdutos
            // 
            this.dgvProdutos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProdutos.Location = new System.Drawing.Point(396, 36);
            this.dgvProdutos.Name = "dgvProdutos";
            this.dgvProdutos.Size = new System.Drawing.Size(367, 221);
            this.dgvProdutos.TabIndex = 20;
            // 
            // txtProdId
            // 
            this.txtProdId.Location = new System.Drawing.Point(15, 62);
            this.txtProdId.Name = "txtProdId";
            this.txtProdId.ReadOnly = true;
            this.txtProdId.Size = new System.Drawing.Size(94, 20);
            this.txtProdId.TabIndex = 21;
            // 
            // lblId
            // 
            this.lblId.AutoSize = true;
            this.lblId.Location = new System.Drawing.Point(12, 36);
            this.lblId.Name = "lblId";
            this.lblId.Size = new System.Drawing.Size(15, 13);
            this.lblId.TabIndex = 22;
            this.lblId.Text = "id";
            // 
            // txtProdNome
            // 
            this.txtProdNome.Location = new System.Drawing.Point(15, 134);
            this.txtProdNome.Name = "txtProdNome";
            this.txtProdNome.Size = new System.Drawing.Size(94, 20);
            this.txtProdNome.TabIndex = 23;
            // 
            // lblNomeProduto
            // 
            this.lblNomeProduto.AutoSize = true;
            this.lblNomeProduto.Location = new System.Drawing.Point(12, 108);
            this.lblNomeProduto.Name = "lblNomeProduto";
            this.lblNomeProduto.Size = new System.Drawing.Size(35, 13);
            this.lblNomeProduto.TabIndex = 24;
            this.lblNomeProduto.Text = "Nome";
            // 
            // txtProdPreco
            // 
            this.txtProdPreco.Location = new System.Drawing.Point(15, 206);
            this.txtProdPreco.Name = "txtProdPreco";
            this.txtProdPreco.Size = new System.Drawing.Size(94, 20);
            this.txtProdPreco.TabIndex = 25;
            // 
            // lblPrecoProduto
            // 
            this.lblPrecoProduto.AutoSize = true;
            this.lblPrecoProduto.Location = new System.Drawing.Point(12, 180);
            this.lblPrecoProduto.Name = "lblPrecoProduto";
            this.lblPrecoProduto.Size = new System.Drawing.Size(35, 13);
            this.lblPrecoProduto.TabIndex = 26;
            this.lblPrecoProduto.Text = "Preço";
            // 
            // lblAtivoProduto
            // 
            this.lblAtivoProduto.AutoSize = true;
            this.lblAtivoProduto.Location = new System.Drawing.Point(199, 36);
            this.lblAtivoProduto.Name = "lblAtivoProduto";
            this.lblAtivoProduto.Size = new System.Drawing.Size(31, 13);
            this.lblAtivoProduto.TabIndex = 31;
            this.lblAtivoProduto.Text = "Ativo";
            // 
            // cmbProdAtivo
            // 
            this.cmbProdAtivo.FormattingEnabled = true;
            this.cmbProdAtivo.Items.AddRange(new object[] {
            "Ativo",
            "Indisponível"});
            this.cmbProdAtivo.Location = new System.Drawing.Point(202, 65);
            this.cmbProdAtivo.Name = "cmbProdAtivo";
            this.cmbProdAtivo.Size = new System.Drawing.Size(64, 21);
            this.cmbProdAtivo.TabIndex = 32;
            // 
            // txtProdDescricao
            // 
            this.txtProdDescricao.Location = new System.Drawing.Point(202, 134);
            this.txtProdDescricao.Multiline = true;
            this.txtProdDescricao.Name = "txtProdDescricao";
            this.txtProdDescricao.Size = new System.Drawing.Size(156, 146);
            this.txtProdDescricao.TabIndex = 33;
            // 
            // lblDescricaoProduto
            // 
            this.lblDescricaoProduto.AutoSize = true;
            this.lblDescricaoProduto.Location = new System.Drawing.Point(199, 108);
            this.lblDescricaoProduto.Name = "lblDescricaoProduto";
            this.lblDescricaoProduto.Size = new System.Drawing.Size(55, 13);
            this.lblDescricaoProduto.TabIndex = 34;
            this.lblDescricaoProduto.Text = "Descrição";
            // 
            // btnAtualizarProduto
            // 
            this.btnAtualizarProduto.Location = new System.Drawing.Point(434, 314);
            this.btnAtualizarProduto.Name = "btnAtualizarProduto";
            this.btnAtualizarProduto.Size = new System.Drawing.Size(118, 65);
            this.btnAtualizarProduto.TabIndex = 36;
            this.btnAtualizarProduto.Text = "Atualizar Produto";
            this.btnAtualizarProduto.UseVisualStyleBackColor = true;
            // 
            // btnRemoverProduto
            // 
            this.btnRemoverProduto.Location = new System.Drawing.Point(616, 314);
            this.btnRemoverProduto.Name = "btnRemoverProduto";
            this.btnRemoverProduto.Size = new System.Drawing.Size(117, 65);
            this.btnRemoverProduto.TabIndex = 37;
            this.btnRemoverProduto.Text = "Remover Produto";
            this.btnRemoverProduto.UseVisualStyleBackColor = true;
            // 
            // lblGridProdutos
            // 
            this.lblGridProdutos.AutoSize = true;
            this.lblGridProdutos.Location = new System.Drawing.Point(393, 20);
            this.lblGridProdutos.Name = "lblGridProdutos";
            this.lblGridProdutos.Size = new System.Drawing.Size(49, 13);
            this.lblGridProdutos.TabIndex = 38;
            this.lblGridProdutos.Text = "Produtos";
            // 
            // tabs
            // 
            this.tabs.Controls.Add(this.tabProdutos);
            this.tabs.Controls.Add(this.tabPedidos);
            this.tabs.Controls.Add(this.tabClientes);
            this.tabs.Controls.Add(this.tabAdministradores);
            this.tabs.Location = new System.Drawing.Point(1, 2);
            this.tabs.Name = "tabs";
            this.tabs.SelectedIndex = 0;
            this.tabs.Size = new System.Drawing.Size(799, 449);
            this.tabs.TabIndex = 0;
            // 
            // FormProdutos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tabs);
            this.Name = "FormProdutos";
            this.Text = "FormProdutos";
            this.tabAdministradores.ResumeLayout(false);
            this.tabAdministradores.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAdmins)).EndInit();
            this.tabClientes.ResumeLayout(false);
            this.tabClientes.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvClientes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEnderecos)).EndInit();
            this.tabPedidos.ResumeLayout(false);
            this.tabPedidos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPedidos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItens)).EndInit();
            this.tabProdutos.ResumeLayout(false);
            this.tabProdutos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProdutos)).EndInit();
            this.tabs.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TabPage tabAdministradores;
        private System.Windows.Forms.Label lblAdmSenha;
        private System.Windows.Forms.TextBox txtAdmSenha;
        private System.Windows.Forms.TextBox txtAdmEmail;
        private System.Windows.Forms.TextBox txtAdmNome;
        private System.Windows.Forms.TextBox txtAdmId;
        private System.Windows.Forms.Button btnAdicionarAdmin;
        private System.Windows.Forms.Button btnAtualizarAdmin;
        private System.Windows.Forms.MaskedTextBox mskAdmTelefone;
        private System.Windows.Forms.Label lblTelefoneAdmin;
        private System.Windows.Forms.Label lblEmailAdmin;
        private System.Windows.Forms.Label lblGridAdmins;
        private System.Windows.Forms.Button btnRemoverAdmin;
        private System.Windows.Forms.Label lblNomeAdmin;
        private System.Windows.Forms.Label lblIdAdmin;
        private System.Windows.Forms.DataGridView dgvAdmins;
        private System.Windows.Forms.TabPage tabClientes;
        private System.Windows.Forms.Button btnAtualizarCliente;
        private System.Windows.Forms.MaskedTextBox mskCliTelefone;
        private System.Windows.Forms.Label lblTelefoneCliente;
        private System.Windows.Forms.Label lblEmailCliente;
        private System.Windows.Forms.TextBox txtCliEmail;
        private System.Windows.Forms.TextBox txtCliNome;
        private System.Windows.Forms.TextBox txtCliId;
        private System.Windows.Forms.Label lblGridEnderecos;
        private System.Windows.Forms.DataGridView dgvEnderecos;
        private System.Windows.Forms.Label lblGridClientes;
        private System.Windows.Forms.Button btnRemoverCliente;
        private System.Windows.Forms.Label lblNomeCliente;
        private System.Windows.Forms.Label lblIdCliente;
        private System.Windows.Forms.DataGridView dgvClientes;
        private System.Windows.Forms.TabPage tabPedidos;
        private System.Windows.Forms.Button btnFiltrarPedidos;
        private System.Windows.Forms.Label lblItensPedido;
        private System.Windows.Forms.DataGridView dgvItens;
        private System.Windows.Forms.Label lblGridPedidos;
        private System.Windows.Forms.Button btnCancelarPedido;
        private System.Windows.Forms.Label lblIdClientePedido;
        private System.Windows.Forms.TextBox txtPedidoCliente;
        private System.Windows.Forms.TextBox txtPedidoId;
        private System.Windows.Forms.Label lblIdPedido;
        private System.Windows.Forms.DataGridView dgvPedidos;
        private System.Windows.Forms.TabPage tabProdutos;
        private System.Windows.Forms.Label lblGridProdutos;
        private System.Windows.Forms.Button btnRemoverProduto;
        private System.Windows.Forms.Button btnAtualizarProduto;
        private System.Windows.Forms.Label lblDescricaoProduto;
        private System.Windows.Forms.TextBox txtProdDescricao;
        private System.Windows.Forms.TextBox txtProdPreco;
        private System.Windows.Forms.TextBox txtProdNome;
        private System.Windows.Forms.TextBox txtProdId;
        private System.Windows.Forms.ComboBox cmbProdAtivo;
        private System.Windows.Forms.Label lblAtivoProduto;
        private System.Windows.Forms.Label lblPrecoProduto;
        private System.Windows.Forms.Label lblNomeProduto;
        private System.Windows.Forms.Label lblId;
        private System.Windows.Forms.DataGridView dgvProdutos;
        private System.Windows.Forms.TabControl tabs;
    }
}