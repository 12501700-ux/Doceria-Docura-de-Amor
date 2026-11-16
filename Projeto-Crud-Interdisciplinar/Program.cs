using System.Data;
using TesteCrudDelivery;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(o => o.AddDefaultPolicy(p => p.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();
app.UseCors();
app.UseSwagger();
app.UseSwaggerUI();

// erros do banco viram JSON 500
app.Use(async (ctx, next) =>
{
    try { await next(); }
    catch (Exception ex)
    {
        ctx.Response.StatusCode = 500;
        await ctx.Response.WriteAsJsonAsync(new { erro = ex.Message });
    }
});

var db = new Conexao();

// ---------- helpers (usam a classe Conexao) ----------
// Atenção: a Conexao liga os parâmetros pela ORDEM dos @ no SQL; o nome ("@id") é só para leitura.
object?[] Vals((string, object?)[] ps) => ps.Select(p => p.Item2).ToArray();

Task<List<Dictionary<string, object?>>> Query(string sql, params (string, object?)[] ps) => Task.Run(() =>
{
    var dt = (DataTable)db.ExecutarQuery(sql, Vals(ps)!);
    return dt.Rows.Cast<DataRow>().Select(r => dt.Columns.Cast<DataColumn>().ToDictionary(
        c => c.ColumnName,
        c => r[c] is DBNull ? (object?)null
           : c.ColumnName == "ativo" ? Convert.ToBoolean(r[c]) : r[c])).ToList();
});

Task<(int rows, long id)> Exec(string sql, params (string, object?)[] ps) => Task.Run(() =>
{
    var id = (long)db.ExecutarQuery(sql, Vals(ps)!);
    return (1, id);
});

// ---------- PRODUTOS ----------
app.MapGet("/produtos", async (bool? todos) =>
    await Query(todos == true ? "SELECT * FROM produtos" : "SELECT * FROM produtos WHERE ativo=1"));

app.MapPut("/produtos/{id:int}", async (int id, ProdutoDto p) =>
{
    await Exec(
        "UPDATE produtos SET nome=@n, descricao=@d, preco_unitario=@p, ativo=@a WHERE id_produto=@id",
        ("@n", p.Nome), ("@d", p.Descricao), ("@p", p.PrecoUnitario), ("@a", p.Ativo), ("@id", id));
    return Results.NoContent();
});

// exclui de verdade; se o produto já estiver em algum pedido (FK), o banco recusa
app.MapDelete("/produtos/{id:int}", async (int id) =>
{
    try
    {
        await Exec("DELETE FROM produtos WHERE id_produto=@id", ("@id", id));
        return Results.NoContent();
    }
    catch (Exception ex) when (ex.Message.Contains("foreign key", StringComparison.OrdinalIgnoreCase))
    {
        return Results.Conflict("Este produto já foi vendido e não pode ser excluído.");
    }
});

// ---------- USUÁRIOS ----------
app.MapPost("/cadastro", async (CadastroDto u) =>
{
    if ((await Query("SELECT 1 FROM usuarios WHERE email=@e", ("@e", u.Email))).Count > 0)
        return Results.Conflict("E-mail já cadastrado");
    var (_, id) = await Exec(
        "INSERT INTO usuarios (nome, email, senha, telefone, tipo_conta, data_cadastro) VALUES (@n,@e,@s,@t,'cliente',NOW())",
        ("@n", u.Nome), ("@e", u.Email), ("@s", BCrypt.Net.BCrypt.HashPassword(u.Senha)), ("@t", u.Telefone));
    return Results.Created($"/usuarios/{id}", new { id_usuario = id });
});

app.MapPost("/login", async (LoginDto l) =>
{
    var r = await Query("SELECT * FROM usuarios WHERE email=@e", ("@e", l.Email));
    if (r.Count == 0) return Results.Unauthorized();
    var hash = (string)r[0]["senha"]!;
    // aceita senha com BCrypt ($2...) ou senha antiga em texto puro
    var ok = hash.StartsWith("$2") ? BCrypt.Net.BCrypt.Verify(l.Senha, hash) : hash == l.Senha;
    if (!ok) return Results.Unauthorized();
    r[0].Remove("senha");
    return Results.Ok(r[0]);
});

const string ColsUsuario = "id_usuario, nome, email, telefone, tipo_conta, data_cadastro";

app.MapGet("/usuarios", async () =>
    await Query($"SELECT {ColsUsuario} FROM usuarios"));

app.MapGet("/clientes", async () =>
    await Query($"SELECT {ColsUsuario} FROM usuarios WHERE tipo_conta <> 'administrador' ORDER BY nome"));

app.MapGet("/administradores", async () =>
    await Query($"SELECT {ColsUsuario} FROM usuarios WHERE tipo_conta = 'administrador' ORDER BY nome"));

app.MapPost("/administradores", async (AdminDto a) =>
{
    if (string.IsNullOrWhiteSpace(a.Nome) || string.IsNullOrWhiteSpace(a.Email) || string.IsNullOrWhiteSpace(a.Senha))
        return Results.BadRequest("Nome, e-mail e senha são obrigatórios");
    if ((await Query("SELECT 1 FROM usuarios WHERE email=@e", ("@e", a.Email))).Count > 0)
        return Results.Conflict("E-mail já cadastrado");
    var (_, id) = await Exec(
        "INSERT INTO usuarios (nome, email, senha, telefone, tipo_conta, data_cadastro) VALUES (@n,@e,@s,@t,'administrador',NOW())",
        ("@n", a.Nome), ("@e", a.Email), ("@s", BCrypt.Net.BCrypt.HashPassword(a.Senha)), ("@t", a.Telefone));
    return Results.Created($"/usuarios/{id}", new { id_usuario = id });
});

// atualiza dados de cliente ou administrador
app.MapPut("/usuarios/{id:int}", async (int id, UsuarioDto u) =>
{
    if (string.IsNullOrWhiteSpace(u.Nome) || string.IsNullOrWhiteSpace(u.Email))
        return Results.BadRequest("Nome e e-mail são obrigatórios");
    if ((await Query("SELECT 1 FROM usuarios WHERE id_usuario=@id", ("@id", id))).Count == 0)
        return Results.NotFound();
    if ((await Query("SELECT 1 FROM usuarios WHERE email=@e AND id_usuario<>@id", ("@e", u.Email), ("@id", id))).Count > 0)
        return Results.Conflict("E-mail já usado por outro usuário");
    await Exec("UPDATE usuarios SET nome=@n, email=@e, telefone=@t WHERE id_usuario=@id",
        ("@n", u.Nome), ("@e", u.Email), ("@t", u.Telefone), ("@id", id));
    return Results.NoContent();
});

// remove cliente ou administrador (e os endereços dele)
app.MapDelete("/usuarios/{id:int}", async (int id) =>
{
    var alvo = await Query("SELECT tipo_conta FROM usuarios WHERE id_usuario=@id", ("@id", id));
    if (alvo.Count == 0) return Results.NotFound();
    if ((string)alvo[0]["tipo_conta"]! == "administrador" &&
        (await Query("SELECT 1 FROM usuarios WHERE tipo_conta='administrador'")).Count <= 1)
        return Results.Conflict("Não é possível remover o único administrador.");
    if ((await Query("SELECT 1 FROM pedidos WHERE id_usuario=@id LIMIT 1", ("@id", id))).Count > 0)
        return Results.Conflict("Este usuário possui pedidos e não pode ser removido.");
    await Exec("DELETE FROM enderecos WHERE id_usuario=@id", ("@id", id));
    await Exec("DELETE FROM usuarios WHERE id_usuario=@id", ("@id", id));
    return Results.NoContent();
});

// ---------- ENDEREÇOS ----------
app.MapGet("/usuarios/{id:int}/enderecos", async (int id) =>
    await Query("SELECT * FROM enderecos WHERE id_usuario=@id", ("@id", id)));

app.MapPost("/enderecos", async (EnderecoDto e) =>
{
    var (_, id) = await Exec(
        "INSERT INTO enderecos (id_usuario, rua, complemento, bairro, cidade, estado, cep) VALUES (@u,@r,@c,@b,@ci,@es,@cep)",
        ("@u", e.IdUsuario), ("@r", e.Rua), ("@c", e.Complemento),
        ("@b", e.Bairro), ("@ci", e.Cidade), ("@es", e.Estado), ("@cep", e.Cep));
    return Results.Created($"/enderecos/{id}", new { id_endereco = id });
});

app.MapDelete("/enderecos/{id:int}", async (int id) =>
{
    await Exec("DELETE FROM enderecos WHERE id_endereco=@id", ("@id", id));
    return Results.NoContent();
});

// ---------- PEDIDOS ----------
app.MapPost("/pedidos", async (PedidoDto p) =>
{
    if (p.Itens.Count == 0) return Results.BadRequest("Pedido sem itens");
    if ((await Query("SELECT 1 FROM usuarios WHERE id_usuario=@u", ("@u", p.IdUsuario))).Count == 0)
        return Results.BadRequest("Usuário não encontrado");

    string[] turnos = { "Manhã", "Início da Tarde", "Fim da Tarde" };
    if (p.TurnoEntrega is not null && !turnos.Contains(p.TurnoEntrega))
        return Results.BadRequest("Turno inválido (use: Manhã, Início da Tarde ou Fim da Tarde)");

    // 1) valida tudo e pega os preços do banco (nunca do cliente)
    decimal total = 0;
    var linhas = new List<(int idProduto, decimal qtd, decimal preco)>();
    foreach (var i in p.Itens)
    {
        var r = await Query("SELECT preco_unitario FROM produtos WHERE id_produto=@id AND ativo=1", ("@id", i.IdProduto));
        if (r.Count == 0) return Results.BadRequest($"Produto {i.IdProduto} indisponível");
        var pu = Convert.ToDecimal(r[0]["preco_unitario"]);
        total += pu * i.Quantidade;
        linhas.Add((i.IdProduto, i.Quantidade, pu));
    }

    // 2) endereço: sem "endereco" = retirada na loja (id_endereco NULL, nada é gravado em enderecos)
    object? idEndereco = null;
    if (p.Endereco is not null)
    {
        var end = p.Endereco;
        if (string.IsNullOrWhiteSpace(end.RuaNumero) || string.IsNullOrWhiteSpace(end.Bairro) ||
            string.IsNullOrWhiteSpace(end.Cep) || string.IsNullOrWhiteSpace(end.Cidade))
            return Results.BadRequest("Endereço incompleto");

        var rua = end.RuaNumero.Trim(); // rua e número no mesmo campo (ex.: "Rua das Flores, 123")
        var bairro = end.Bairro.Trim();
        var cidade = end.Cidade.Trim();
        var comp = (end.Complemento ?? "").Trim();
        var estado = string.IsNullOrWhiteSpace(end.Estado) ? "MG" : end.Estado.Trim().ToUpper();
        var dig = new string(end.Cep.Where(char.IsDigit).ToArray());
        var cep = dig.Length == 8 ? dig.Substring(0, 5) + "-" + dig.Substring(5) : end.Cep.Trim();

        // já existe esse endereço para o usuário? então reaproveita
        var existente = await Query(
            @"SELECT id_endereco FROM enderecos
              WHERE id_usuario=@u AND rua=@r AND IFNULL(complemento,'')=@c
                AND bairro=@b AND cidade=@ci AND cep=@cep LIMIT 1",
            ("@u", p.IdUsuario), ("@r", rua), ("@c", comp),
            ("@b", bairro), ("@ci", cidade), ("@cep", cep));

        if (existente.Count > 0)
            idEndereco = existente[0]["id_endereco"];
        else
        {
            var (_, novoId) = await Exec(
                "INSERT INTO enderecos (id_usuario, rua, complemento, bairro, cidade, estado, cep) VALUES (@u,@r,@c,@b,@ci,@es,@cep)",
                ("@u", p.IdUsuario), ("@r", rua), ("@c", comp == "" ? null : comp),
                ("@b", bairro), ("@ci", cidade), ("@es", estado), ("@cep", cep));
            idEndereco = novoId;
        }
    }

    // 3) grava pedido + itens (sem transação: se um item falhar, desfaz na mão)
    var (_, idPedido) = await Exec(
        "INSERT INTO pedidos (id_usuario, id_endereco, valor_total, data_pedido, data_entrega, turno_entrega) VALUES (@u,@e,@v,NOW(),@d,@t)",
        ("@u", p.IdUsuario), ("@e", idEndereco), ("@v", total), ("@d", p.DataEntrega), ("@t", p.TurnoEntrega));
    try
    {
        foreach (var l in linhas)
            await Exec("INSERT INTO itens_pedido (id_pedido, id_produto, quantidade, preco_unitario) VALUES (@pd,@pr,@q,@pu)",
                ("@pd", idPedido), ("@pr", l.idProduto), ("@q", l.qtd), ("@pu", l.preco));
    }
    catch
    {
        await Exec("DELETE FROM itens_pedido WHERE id_pedido=@id", ("@id", idPedido));
        await Exec("DELETE FROM pedidos WHERE id_pedido=@id", ("@id", idPedido));
        throw;
    }
    return Results.Created($"/pedidos/{idPedido}", new { id_pedido = idPedido, valor_total = total, entrega = idEndereco != null });
});

app.MapGet("/pedidos", async (int? idUsuario) =>
{
    var sql = @"SELECT p.*, u.nome AS cliente, u.telefone, e.rua, e.complemento, e.bairro, e.cidade, e.cep
                FROM pedidos p
                JOIN usuarios u ON u.id_usuario = p.id_usuario
                LEFT JOIN enderecos e ON e.id_endereco = p.id_endereco"
              + (idUsuario == null ? "" : " WHERE p.id_usuario=@u")
              + " ORDER BY p.data_pedido DESC";
    return idUsuario == null ? await Query(sql) : await Query(sql, ("@u", idUsuario));
});

app.MapGet("/pedidos/{id:int}/itens", async (int id) =>
    await Query(@"SELECT i.*, pr.nome FROM itens_pedido i
                  JOIN produtos pr ON pr.id_produto = i.id_produto
                  WHERE i.id_pedido=@id", ("@id", id)));

// cancelar = apagar o pedido e seus itens (não existe coluna de status)
app.MapDelete("/pedidos/{id:int}", async (int id) =>
{
    if ((await Query("SELECT 1 FROM pedidos WHERE id_pedido=@id", ("@id", id))).Count == 0)
        return Results.NotFound();
    await Exec("DELETE FROM itens_pedido WHERE id_pedido=@id", ("@id", id));
    await Exec("DELETE FROM pedidos WHERE id_pedido=@id", ("@id", id));
    return Results.NoContent();
});

app.MapGet("/usuarios/{id:int}/pedidos", async (int id) =>
    await Query("SELECT * FROM pedidos WHERE id_usuario=@id ORDER BY data_pedido DESC", ("@id", id)));

app.MapGet("/pedidos/{id:int}", async (int id) =>
{
    var ped = await Query(@"SELECT p.*, u.nome AS cliente, u.telefone, e.rua, e.complemento, e.bairro, e.cidade, e.cep
                            FROM pedidos p
                            JOIN usuarios u ON u.id_usuario = p.id_usuario
                            LEFT JOIN enderecos e ON e.id_endereco = p.id_endereco
                            WHERE p.id_pedido=@id", ("@id", id));
    if (ped.Count == 0) return Results.NotFound();
    var itens = await Query(@"SELECT i.*, pr.nome FROM itens_pedido i
                              JOIN produtos pr ON pr.id_produto = i.id_produto
                              WHERE i.id_pedido=@id", ("@id", id));
    return Results.Ok(new { pedido = ped[0], itens });
});

app.Run("http://localhost:5000");

// ---------- DTOs (JSON de entrada em camelCase) ----------
record ProdutoDto(string Nome, string? Descricao, decimal PrecoUnitario, bool Ativo = true);
record CadastroDto(string Nome, string Email, string Senha, string? Telefone);
record LoginDto(string Email, string Senha);
record UsuarioDto(string Nome, string Email, string? Telefone);
record AdminDto(string Nome, string Email, string? Telefone, string Senha);
record EnderecoDto(int IdUsuario, string Rua, string? Complemento, string Bairro, string Cidade, string Estado, string Cep);
record ItemDto(int IdProduto, decimal Quantidade);
record EnderecoPedidoDto(string RuaNumero, string Bairro, string Cep, string? Complemento, string Cidade, string? Estado);
record PedidoDto(int IdUsuario, EnderecoPedidoDto? Endereco, DateTime? DataEntrega, string? TurnoEntrega, List<ItemDto> Itens);