using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

// .NET Framework 4.x | NuGet: Newtonsoft.Json | Referência: System.Net.Http
public static class Api
{
    static readonly HttpClient Http = new HttpClient { BaseAddress = new Uri("http://localhost:5000/") };

    static StringContent Json(object o)
    {
        return new StringContent(JsonConvert.SerializeObject(o), Encoding.UTF8, "application/json");
    }

    // lança Exception com a mensagem que a API devolveu
    static async Task Verificar(HttpResponseMessage r)
    {
        if (r.IsSuccessStatusCode) return;
        var msg = (await r.Content.ReadAsStringAsync()).Trim().Trim('"');
        throw new Exception(string.IsNullOrEmpty(msg) ? "Erro " + (int)r.StatusCode : msg);
    }

    static async Task<T> Get<T>(string url)
    {
        var r = await Http.GetAsync(url);
        await Verificar(r);
        return JsonConvert.DeserializeObject<T>(await r.Content.ReadAsStringAsync());
    }

    // ---------- LOGIN (só administrador) ----------
    public static async Task<Usuario> Login(string email, string senha)
    {
        var r = await Http.PostAsync("login", Json(new { email = email, senha = senha }));
        if (!r.IsSuccessStatusCode) return null;
        var u = JsonConvert.DeserializeObject<Usuario>(await r.Content.ReadAsStringAsync());
        return (u != null && u.TipoConta == "administrador") ? u : null;
    }

    // ---------- PRODUTOS ----------
    public static Task<List<Produto>> ListarProdutos()
    {
        return Get<List<Produto>>("produtos?todos=true");
    }

    public static async Task SalvarProduto(Produto p)
    {
        if (p.IdProduto == 0)
            throw new InvalidOperationException("A API não permite cadastrar produto novo.");
        var r = await Http.PutAsync("produtos/" + p.IdProduto, Json(new
        {
            nome = p.Nome,
            descricao = p.Descricao,
            precoUnitario = p.PrecoUnitario,
            ativo = p.Ativo
        }));
        await Verificar(r);
    }

    public static async Task ExcluirProduto(int id)
    {
        await Verificar(await Http.DeleteAsync("produtos/" + id));
    }

    // ---------- PEDIDOS ----------
    // idUsuario = null lista todos; com valor, filtra por cliente
    public static Task<List<Pedido>> ListarPedidos(int? idUsuario = null)
    {
        return Get<List<Pedido>>(idUsuario == null ? "pedidos" : "pedidos?idUsuario=" + idUsuario);
    }

    public static Task<List<ItemPedido>> ListarItensPedido(int idPedido)
    {
        return Get<List<ItemPedido>>("pedidos/" + idPedido + "/itens");
    }

    public static async Task CancelarPedido(int idPedido)
    {
        await Verificar(await Http.DeleteAsync("pedidos/" + idPedido));
    }

    // ---------- CLIENTES ----------
    public static Task<List<Usuario>> ListarClientes()
    {
        return Get<List<Usuario>>("clientes");
    }

    public static Task<List<Endereco>> ListarEnderecos(int idUsuario)
    {
        return Get<List<Endereco>>("usuarios/" + idUsuario + "/enderecos");
    }

    // serve para cliente e administrador
    public static async Task AtualizarUsuario(Usuario u)
    {
        var r = await Http.PutAsync("usuarios/" + u.IdUsuario, Json(new
        {
            nome = u.Nome,
            email = u.Email,
            telefone = u.Telefone
        }));
        await Verificar(r);
    }

    // serve para cliente e administrador
    public static async Task RemoverUsuario(int idUsuario)
    {
        await Verificar(await Http.DeleteAsync("usuarios/" + idUsuario));
    }

    // ---------- ADMINISTRADORES ----------
    public static Task<List<Usuario>> ListarAdministradores()
    {
        return Get<List<Usuario>>("administradores");
    }

    public static async Task AdicionarAdministrador(string nome, string email, string telefone, string senha)
    {
        var r = await Http.PostAsync("administradores", Json(new
        {
            nome = nome,
            email = email,
            telefone = telefone,
            senha = senha
        }));
        await Verificar(r);
    }
}

public class Produto
{
    [JsonProperty("id_produto")] public int IdProduto { get; set; }
    [JsonProperty("nome")] public string Nome { get; set; }
    [JsonProperty("descricao")] public string Descricao { get; set; }
    [JsonProperty("preco_unitario")] public decimal PrecoUnitario { get; set; }
    [JsonProperty("ativo")] public bool Ativo { get; set; }

    public Produto() { Nome = ""; Ativo = true; }
}

public class Pedido
{
    [JsonProperty("id_pedido")] public int IdPedido { get; set; }
    [JsonProperty("id_usuario")] public int IdUsuario { get; set; }
    [JsonProperty("cliente")] public string Cliente { get; set; }
    [JsonProperty("telefone")] public string Telefone { get; set; }
    [JsonProperty("valor_total")] public decimal ValorTotal { get; set; }
    [JsonProperty("data_pedido")] public DateTime DataPedido { get; set; }
    [JsonProperty("data_entrega")] public DateTime? DataEntrega { get; set; }
    [JsonProperty("turno_entrega")] public string TurnoEntrega { get; set; }
    // vazios quando for retirada na loja
    [JsonProperty("rua")] public string Rua { get; set; }
    [JsonProperty("complemento")] public string Complemento { get; set; }
    [JsonProperty("bairro")] public string Bairro { get; set; }
    [JsonProperty("cidade")] public string Cidade { get; set; }
    [JsonProperty("cep")] public string Cep { get; set; }
}

public class ItemPedido
{
    [JsonProperty("id_item")] public int IdItem { get; set; }
    [JsonProperty("id_pedido")] public int IdPedido { get; set; }
    [JsonProperty("id_produto")] public int IdProduto { get; set; }
    [JsonProperty("nome")] public string Produto { get; set; }
    [JsonProperty("quantidade")] public decimal Quantidade { get; set; }
    [JsonProperty("preco_unitario")] public decimal PrecoUnitario { get; set; }
}

public class Usuario
{
    [JsonProperty("id_usuario")] public int IdUsuario { get; set; }
    [JsonProperty("nome")] public string Nome { get; set; }
    [JsonProperty("email")] public string Email { get; set; }
    [JsonProperty("telefone")] public string Telefone { get; set; }
    [JsonProperty("tipo_conta")] public string TipoConta { get; set; }
    [JsonProperty("data_cadastro")] public DateTime? DataCadastro { get; set; }
}

public class Endereco
{
    [JsonProperty("id_endereco")] public int IdEndereco { get; set; }
    [JsonProperty("rua")] public string Rua { get; set; }
    [JsonProperty("complemento")] public string Complemento { get; set; }
    [JsonProperty("bairro")] public string Bairro { get; set; }
    [JsonProperty("cidade")] public string Cidade { get; set; }
    [JsonProperty("estado")] public string Estado { get; set; }
    [JsonProperty("cep")] public string Cep { get; set; }
}