document.addEventListener("DOMContentLoaded", () => {
    const btnMenuMobile = document.getElementById("btnMenuMobile");
    const menuPrincipal = document.getElementById("menuPrincipal");
    const dropdowns = document.querySelectorAll(".item-dropdown");
    const botoesDropdown = document.querySelectorAll(".btn-dropdown");

    // Menu mobile (hambúrguer)
    const larguraMobile = window.matchMedia("(max-width: 850px)");
    let menuTravouScroll = false;

    function abrirMenuMobile() {
        if (!btnMenuMobile || !menuPrincipal) return;
        menuPrincipal.classList.add("menu-aberto");
        btnMenuMobile.setAttribute("aria-expanded", "true");
        btnMenuMobile.setAttribute("aria-label", "Fechar menu");
        btnMenuMobile.innerHTML = '<i class="bi bi-x-lg"></i>';
        document.body.style.overflow = "hidden";
        menuTravouScroll = true;
    }

    function fecharMenuMobile() {
        if (!btnMenuMobile || !menuPrincipal) return;
        menuPrincipal.classList.remove("menu-aberto");
        btnMenuMobile.setAttribute("aria-expanded", "false");
        btnMenuMobile.setAttribute("aria-label", "Abrir menu");
        btnMenuMobile.innerHTML = '<i class="bi bi-list"></i>';
        if (menuTravouScroll) {
            document.body.style.overflow = "";
            menuTravouScroll = false;
        }
        fecharDropdowns();
    }

    if (btnMenuMobile && menuPrincipal) {
        btnMenuMobile.addEventListener("click", (evento) => {
            evento.stopPropagation();
            if (menuPrincipal.classList.contains("menu-aberto")) {
                fecharMenuMobile();
            } else {
                abrirMenuMobile();
            }
        });

        larguraMobile.addEventListener("change", (evento) => {
            if (!evento.matches) fecharMenuMobile();
        });

        document.querySelectorAll(".div-icones .link-icone").forEach((icone) => {
            icone.addEventListener("click", fecharMenuMobile);
        });
    }

    // Submenus
    botoesDropdown.forEach((botao) => {
        botao.addEventListener("click", (evento) => {
            evento.stopPropagation();
            const dropdownAtual = botao.closest(".item-dropdown");
            if (!dropdownAtual) return;

            const estavaAberto = dropdownAtual.classList.contains("aberto");
            fecharDropdowns();

            if (!estavaAberto) {
                dropdownAtual.classList.add("aberto");
                botao.setAttribute("aria-expanded", "true");
            }
        });
    });

    function fecharDropdowns() {
        dropdowns.forEach((dropdown) => {
            dropdown.classList.remove("aberto");
            const botao = dropdown.querySelector(".btn-dropdown");
            if (botao) botao.setAttribute("aria-expanded", "false");
        });
    }

    document.addEventListener("click", (evento) => {
        if (!evento.target.closest(".header-menu")) {
            fecharDropdowns();
            if (larguraMobile.matches) fecharMenuMobile();
        }
    });

    document.addEventListener("keydown", (evento) => {
        if (evento.key === "Escape") {
            fecharMenuMobile();
            fecharDropdowns();
        }
    });

    document.querySelectorAll(".submenu a, .ul-menu-principal > .item-menu > a")
        .forEach((link) => {
            link.addEventListener("click", () => {
                fecharMenuMobile();
                fecharDropdowns();
            });
        });
});

// Modo Claro e Escuro
const btnTema = document.getElementById("btnTema");
const iconeTema = document.getElementById("iconeTema");

if (btnTema && iconeTema) {
    btnTema.addEventListener("click", () => {
        document.body.classList.toggle("modo-escuro");
        const modoEscuro = document.body.classList.contains("modo-escuro");

        iconeTema.className = modoEscuro ? "bi bi-moon" : "bi bi-sun";
        btnTema.setAttribute(
            "aria-label",
            modoEscuro ? "Ativar modo claro" : "Ativar modo escuro"
        );
    });
}

// Catálogo (painel)
const sheetFundo = document.getElementById("sheetFundo");
const sheetFechar = document.getElementById("sheetFechar");

document.querySelectorAll(".card-categoria").forEach((card) => {
    card.addEventListener("click", () => {
        const conteudo = document.getElementById(card.dataset.id);
        if (!conteudo || !sheetFundo) return;

        document.querySelectorAll(".sheet-conteudo").forEach((item) => {
            item.classList.remove("ativo");
        });

        conteudo.classList.add("ativo");
        sheetFundo.classList.add("aberto");
        document.body.style.overflow = "hidden";

        const painel = sheetFundo.querySelector(".sheet");
        if (painel) painel.scrollTop = 0;
    });
});

function fecharSheet() {
    if (!sheetFundo) return;
    sheetFundo.classList.remove("aberto");
    document.body.style.overflow = "";
}

if (sheetFechar) sheetFechar.addEventListener("click", fecharSheet);

if (sheetFundo) {
    sheetFundo.addEventListener("click", (evento) => {
        if (evento.target === sheetFundo) fecharSheet();
    });
}

document.addEventListener("keydown", (evento) => {
    if (evento.key === "Escape") fecharSheet();
});

// Animação de rolar
const elementosAnimados = document.querySelectorAll(".animar, .animar-grupo");

const observador = new IntersectionObserver((entradas) => {
    entradas.forEach((entrada) => {
        if (entrada.isIntersecting) {
            entrada.target.classList.add("visivel");
            observador.unobserve(entrada.target);
        }
    });
}, {
    threshold: 0.15,
    rootMargin: "0px 0px -10% 0px"
});

elementosAnimados.forEach((elemento) => observador.observe(elemento));

// Carrossel automático
(function () {
    const reduzirMovimento = window.matchMedia("(prefers-reduced-motion: reduce)").matches;

    document.querySelectorAll(".carrossel").forEach((carrossel) => {
        const slides = carrossel.querySelectorAll(".carrossel-slide");
        const areaPontos = carrossel.querySelector(".carrossel-pontos");
        if (slides.length < 2 || !areaPontos) return;

        let atual = 0;
        let timer;
        const pontos = [];

        slides.forEach((_, indice) => {
            const ponto = document.createElement("button");
            ponto.type = "button";
            ponto.className = "carrossel-ponto";
            ponto.setAttribute("aria-label", `Ver foto ${indice + 1}`);

            ponto.addEventListener("click", () => {
                irPara(indice);
                iniciar();
            });

            areaPontos.appendChild(ponto);
            pontos.push(ponto);
        });

        pontos[0].classList.add("ativo");

        function irPara(indice) {
            slides[atual].classList.remove("ativo");
            pontos[atual].classList.remove("ativo");

            atual = (indice + slides.length) % slides.length;

            slides[atual].classList.add("ativo");
            pontos[atual].classList.add("ativo");
        }

        function iniciar() {
            clearInterval(timer);
            if (reduzirMovimento) return;

            timer = setInterval(() => {
                if (carrossel.offsetParent !== null) irPara(atual + 1);
            }, 4000);
        }

        carrossel.addEventListener("mouseenter", () => clearInterval(timer));
        carrossel.addEventListener("mouseleave", iniciar);
        iniciar();
    });

    document.querySelectorAll("[data-sheet]").forEach((botao) => {
        botao.addEventListener("click", (evento) => {
            evento.preventDefault();

            const conteudo = document.getElementById(botao.dataset.sheet);
            const fundo = document.getElementById("sheetFundo");
            if (!conteudo || !fundo) return;

            document.querySelectorAll(".sheet-conteudo").forEach((item) => {
                item.classList.remove("ativo");
            });

            conteudo.classList.add("ativo");
            fundo.classList.add("aberto");
            document.body.style.overflow = "hidden";

            const painel = fundo.querySelector(".sheet");
            if (painel) painel.scrollTop = 0;
        });
    });
})();

// Estado global da sacola
let sacolaGlobal = {};

function dinheiro(valor) {
    if (valor === null || valor === undefined) return "A definir";

    return Number(valor).toLocaleString("pt-BR", {
        style: "currency",
        currency: "BRL"
    });
}

// ===== Comunicação com a API =====
const API_URL = "http://localhost:5000";

function normalizar(texto) {
    return String(texto || "")
        .normalize("NFD")
        .replace(/[\u0300-\u036f]/g, "")
        .toLowerCase()
        .replace(/\s+/g, " ")
        .trim();
}

function mensagemDeErro(corpo) {
    if (!corpo) return "";
    if (typeof corpo === "string") return corpo;
    return corpo.erro || corpo.title || corpo.detail || "";
}

async function api(caminho, opcoes) {
    const config = Object.assign({}, opcoes);
    if (config.body !== undefined) {
        config.headers = Object.assign({ "Content-Type": "application/json" }, config.headers);
        config.body = JSON.stringify(config.body);
    }

    let resposta;
    try {
        resposta = await fetch(API_URL + caminho, config);
    } catch (erro) {
        const falha = new Error("Não foi possível conectar à loja. Tente novamente em instantes.");
        falha.rede = true;
        throw falha;
    }

    const texto = await resposta.text();
    let corpo = null;
    try { corpo = texto ? JSON.parse(texto) : null; } catch (erro) { corpo = texto; }

    if (!resposta.ok) {
        const falha = new Error(mensagemDeErro(corpo) || `Erro ${resposta.status}`);
        falha.status = resposta.status;
        throw falha;
    }

    return corpo;
}

// Catálogo da API: nome normalizado -> { id, preco }
const catalogo = new Map();

// Liga cada card .produto do HTML ao produto do banco (pelo nome) e sincroniza o preço
async function carregarCatalogo() {
    const produtos = await api("/produtos");
    catalogo.clear();
    produtos.forEach((p) => {
        catalogo.set(normalizar(p.nome), { id: p.id_produto, preco: Number(p.preco_unitario) });
    });

    const naoEncontrados = [];
    document.querySelectorAll(".produto").forEach((card) => {
        const titulo = card.querySelector("h3");
        const precoEl = card.querySelector(".produto-preco");
        const dado = titulo ? catalogo.get(normalizar(titulo.textContent)) : null;

        if (!dado) {
            card.dataset.indisponivel = "1";
            delete card.dataset.idProduto;
            if (titulo) naoEncontrados.push(titulo.textContent.trim());
            return;
        }

        delete card.dataset.indisponivel;
        card.dataset.idProduto = dado.id;
        if (precoEl) precoEl.textContent = dinheiro(dado.preco);
    });

    if (naoEncontrados.length) {
        console.warn("Produtos do site que não existem (ou estão inativos) no banco:", naoEncontrados);
    }
}

function iniciarCatalogo() {
    return carregarCatalogo().then(() => true, (erro) => {
        console.error("Falha ao carregar o catálogo:", erro);
        return false;
    });
}

let catalogoPronto = iniciarCatalogo();

// true quando o catálogo está carregado; se a API estava fora, tenta de novo
async function garantirCatalogo() {
    if (!(await catalogoPronto)) catalogoPronto = iniciarCatalogo();
    return catalogoPronto;
}

// Conta do cliente (cadastro, painel lateral, histórico e sair)
(function () {
    const CHAVE_USUARIO = "docura_usuario";
    const CHAVE_AVISO = "docura_aviso_conta";

    const btnAbrirConta = document.getElementById("btnAbrirConta");
    const contaOverlay = document.getElementById("contaOverlay");
    const contaFechar = document.getElementById("contaFechar");
    const visaoDeslogado = document.getElementById("visaoDeslogado");
    const visaoLogado = document.getElementById("visaoLogado");
    const formConta = document.getElementById("formContaCliente");
    const btnSairConta = document.getElementById("btnSairConta");
    const listaHistorico = document.getElementById("listaHistoricoPedidos");

    const avisoOverlay = document.getElementById("avisoContaOverlay");
    const btnCriarContaAgora = document.getElementById("btnCriarContaAgora");
    const btnContaMaisTarde = document.getElementById("btnContaMaisTarde");

    if (!btnAbrirConta || !contaOverlay || !formConta) return;

    function lerJSON(chave, padrao) {
        try {
            const bruto = localStorage.getItem(chave);
            return bruto ? JSON.parse(bruto) : padrao;
        } catch (erro) {
            return padrao;
        }
    }

    function salvarJSON(chave, valor) {
        try {
            localStorage.setItem(chave, JSON.stringify(valor));
            return true;
        } catch (erro) {
            return false;
        }
    }

    function usuarioAtual() {
        const usuario = lerJSON(CHAVE_USUARIO, null);
        return usuario && usuario.idUsuario ? usuario : null;
    }

    const toast = document.getElementById("toastAviso");
    const toastTexto = document.getElementById("toastTexto");
    let timerToast;

    function avisar(mensagem) {
        if (!toast || !toastTexto) return;
        toastTexto.textContent = mensagem;
        toast.classList.add("ativo");
        clearTimeout(timerToast);
        timerToast = setTimeout(() => toast.classList.remove("ativo"), 3000);
    }

    function travarScroll(travar) {
        if (travar) {
            document.body.style.overflow = "hidden";
        } else if (!document.querySelector(".sheet-fundo.aberto, .aviso-conta-overlay.ativo")) {
            document.body.style.overflow = "";
        }
    }

    function abrirConta() {
        fecharAviso(false);
        atualizarConta();
        contaOverlay.classList.add("ativo");
        contaOverlay.setAttribute("aria-hidden", "false");
        travarScroll(true);

        setTimeout(() => {
            const alvo = usuarioAtual()
                ? contaFechar
                : document.getElementById("usr-nome");
            if (alvo) alvo.focus();
        }, 350);
    }

    function fecharConta() {
        if (!contaOverlay.classList.contains("ativo")) return;
        contaOverlay.classList.remove("ativo");
        contaOverlay.setAttribute("aria-hidden", "true");
        travarScroll(false);
        resetarBotaoSair();
        btnAbrirConta.focus();
    }

    function abrirAviso() {
        if (!avisoOverlay) return;
        avisoOverlay.classList.add("ativo");
        avisoOverlay.setAttribute("aria-hidden", "false");
        travarScroll(true);
    }

    function fecharAviso(lembrar) {
        if (!avisoOverlay || !avisoOverlay.classList.contains("ativo")) return;
        avisoOverlay.classList.remove("ativo");
        avisoOverlay.setAttribute("aria-hidden", "true");
        travarScroll(false);
        if (lembrar) {
            try { sessionStorage.setItem(CHAVE_AVISO, "1"); } catch (erro) { /* ignora */ }
        }
    }

    function atualizarConta() {
        const usuario = usuarioAtual();
        const icone = btnAbrirConta.querySelector("i");

        visaoDeslogado.classList.toggle("ativo", !usuario);
        visaoLogado.classList.toggle("ativo", !!usuario);

        if (icone) icone.className = usuario ? "bi bi-person-check-fill" : "bi bi-person";
        btnAbrirConta.setAttribute("aria-label", usuario ? "Minha conta" : "Criar conta");

        if (!usuario) return;

        document.getElementById("perfilNome").textContent = usuario.nome;
        document.getElementById("perfilEmail").textContent = usuario.email;
        document.getElementById("perfilTelefone").textContent = usuario.telefone;
        mostrarHistorico(usuario);
    }

    function textoHistorico(mensagem) {
        listaHistorico.textContent = "";
        const aviso = document.createElement("p");
        aviso.className = "historico-vazio";
        aviso.textContent = mensagem;
        listaHistorico.appendChild(aviso);
    }

    let carregamentoHistorico = 0;

    // Histórico vindo da API: pedidos do usuário + itens de cada pedido
    async function mostrarHistorico(usuario) {
        const chamada = ++carregamentoHistorico;
        textoHistorico("Carregando seus pedidos...");

        let pedidos;
        try {
            pedidos = (await api(`/usuarios/${usuario.idUsuario}/pedidos`)).slice(0, 10);
            const listas = await Promise.all(
                pedidos.map((p) => api(`/pedidos/${p.id_pedido}/itens`))
            );
            pedidos.forEach((p, i) => { p.itens = listas[i]; });
        } catch (erro) {
            if (chamada === carregamentoHistorico) {
                textoHistorico("Não foi possível carregar seus pedidos agora.");
            }
            return;
        }

        if (chamada !== carregamentoHistorico) return; // painel reaberto enquanto carregava

        if (!pedidos.length) {
            textoHistorico("Você ainda não fez nenhum pedido. Quando fizer, ele aparece aqui.");
            return;
        }

        listaHistorico.textContent = "";
        pedidos.forEach((pedido) => {
            const card = document.createElement("article");
            card.className = "historico-card";

            const topo = document.createElement("div");
            topo.className = "historico-topo";
            const codigo = document.createElement("span");
            codigo.textContent = `Pedido #${pedido.id_pedido}`;
            const data = document.createElement("span");
            data.className = "historico-data";
            const quando = new Date(pedido.data_pedido);
            data.textContent = isNaN(quando)
                ? ""
                : quando.toLocaleDateString("pt-BR") + " às " +
                  quando.toLocaleTimeString("pt-BR", { hour: "2-digit", minute: "2-digit" });
            topo.append(codigo, data);

            const itens = document.createElement("ul");
            itens.className = "historico-itens";
            (pedido.itens || []).forEach((item) => {
                const linha = document.createElement("li");
                linha.textContent = `${Number(item.quantidade) || 1}x ${item.nome}`;
                itens.appendChild(linha);
            });

            const rodape = document.createElement("div");
            rodape.className = "historico-rodape";
            const tipo = document.createElement("span");
            tipo.textContent = pedido.id_endereco ? "Entrega" : "Retirada na loja";
            const total = document.createElement("strong");
            total.textContent = dinheiro(pedido.valor_total);
            rodape.append(tipo, total);

            card.append(topo, itens, rodape);
            listaHistorico.appendChild(card);
        });
    }

    const campoNome = document.getElementById("usr-nome");
    const campoTelefone = document.getElementById("usr-telefone");
    const campoEmail = document.getElementById("usr-email");
    const campoSenha = document.getElementById("usr-senha");

    campoTelefone.addEventListener("input", () => {
        const n = campoTelefone.value.replace(/\D/g, "").slice(0, 11);
        let texto = n;

        if (n.length > 10) texto = `(${n.slice(0, 2)}) ${n.slice(2, 7)}-${n.slice(7)}`;
        else if (n.length > 6) texto = `(${n.slice(0, 2)}) ${n.slice(2, 6)}-${n.slice(6)}`;
        else if (n.length > 2) texto = `(${n.slice(0, 2)}) ${n.slice(2)}`;
        else if (n.length > 0) texto = `(${n}`;

        campoTelefone.value = texto;
    });

    function mostrarErro(campo, mensagem) {
        const erro = campo.parentElement.querySelector(".contato-erro");
        if (erro) erro.textContent = mensagem;
        campo.classList.toggle("invalido", !!mensagem);
        campo.setAttribute("aria-invalid", mensagem ? "true" : "false");
        return !mensagem;
    }

    function validarFormulario() {
        const nome = campoNome.value.trim();
        const telefone = campoTelefone.value.replace(/\D/g, "");
        const email = campoEmail.value.trim();
        const senha = campoSenha.value;

        const resultados = [
            mostrarErro(campoNome, nome.length < 3 ? "Informe seu nome completo." : ""),
            mostrarErro(campoTelefone, telefone.length < 10 ? "Informe um telefone com DDD." : ""),
            mostrarErro(campoEmail, /^[^\s@]+@[^\s@]+\.[^\s@]{2,}$/.test(email) ? "" : "Informe um e-mail válido."),
            mostrarErro(campoSenha, senha.length < 6 ? "A senha precisa ter pelo menos 6 caracteres." : "")
        ];

        if (resultados.includes(false)) {
            [campoNome, campoTelefone, campoEmail, campoSenha]
                .find((campo) => campo.classList.contains("invalido"))
                .focus();
            return false;
        }

        return true;
    }

    [campoNome, campoTelefone, campoEmail, campoSenha].forEach((campo) => {
        campo.addEventListener("input", () => mostrarErro(campo, ""));
    });

    // O formulário é de cadastro. Se o e-mail já existe, usa a senha digitada para entrar.
    formConta.addEventListener("submit", async (evento) => {
        evento.preventDefault();
        if (!validarFormulario()) return;

        const botao = formConta.querySelector('button[type="submit"]');
        if (botao) botao.disabled = true;

        const dados = {
            nome: campoNome.value.trim(),
            telefone: campoTelefone.value.trim(),
            email: campoEmail.value.trim().toLowerCase(),
            senha: campoSenha.value
        };

        try {
            let usuario;
            let mensagem = "Conta criada! Seus dados foram salvos.";

            try {
                const criado = await api("/cadastro", { method: "POST", body: dados });
                usuario = {
                    idUsuario: criado.id_usuario,
                    nome: dados.nome,
                    telefone: dados.telefone,
                    email: dados.email
                };
            } catch (erro) {
                if (erro.status !== 409) throw erro;

                try {
                    const existente = await api("/login", {
                        method: "POST",
                        body: { email: dados.email, senha: dados.senha }
                    });
                    usuario = {
                        idUsuario: existente.id_usuario,
                        nome: existente.nome,
                        telefone: existente.telefone,
                        email: existente.email
                    };
                    mensagem = "Que bom te ver de novo!";
                } catch (erroLogin) {
                    if (erroLogin.status === 401) {
                        mostrarErro(campoSenha, "Este e-mail já tem conta. Confira a senha para entrar.");
                        campoSenha.focus();
                        return;
                    }
                    throw erroLogin;
                }
            }

            usuario.criadoEm = new Date().toISOString();

            if (!salvarJSON(CHAVE_USUARIO, usuario)) {
                avisar("Não foi possível salvar seus dados neste navegador.");
                return;
            }

            formConta.reset();
            atualizarConta();
            avisar(mensagem);
        } catch (erro) {
            avisar(erro.message);
        } finally {
            if (botao) botao.disabled = false;
        }
    });

    let confirmandoSaida = false;
    let timerSaida;
    const textoBotaoSair = btnSairConta.innerHTML;

    function resetarBotaoSair() {
        confirmandoSaida = false;
        clearTimeout(timerSaida);
        btnSairConta.innerHTML = textoBotaoSair;
    }

    btnSairConta.addEventListener("click", () => {
        if (!confirmandoSaida) {
            confirmandoSaida = true;
            btnSairConta.innerHTML = '<i class="bi bi-exclamation-circle"></i> Toque de novo para sair';
            timerSaida = setTimeout(resetarBotaoSair, 4000);
            return;
        }

        resetarBotaoSair();
        try { localStorage.removeItem(CHAVE_USUARIO); } catch (erro) { /* ignora */ }
        fecharConta();
        atualizarConta();
        avisar("Você saiu da conta.");
    });

    btnAbrirConta.addEventListener("click", (evento) => {
        evento.stopPropagation();
        abrirConta();
    });

    contaFechar.addEventListener("click", fecharConta);

    contaOverlay.addEventListener("click", (evento) => {
        if (evento.target === contaOverlay) fecharConta();
    });

    document.addEventListener("keydown", (evento) => {
        if (evento.key !== "Escape") return;
        fecharConta();
        fecharAviso(true);
    });

    if (avisoOverlay) {
        if (btnCriarContaAgora) {
            btnCriarContaAgora.addEventListener("click", () => {
                fecharAviso(true);
                abrirConta();
            });
        }

        if (btnContaMaisTarde) {
            btnContaMaisTarde.addEventListener("click", () => fecharAviso(true));
        }

        avisoOverlay.addEventListener("click", (evento) => {
            if (evento.target === avisoOverlay) fecharAviso(true);
        });

        let jaViuAviso = false;
        try { jaViuAviso = sessionStorage.getItem(CHAVE_AVISO) === "1"; } catch (erro) { /* ignora */ }
        if (!usuarioAtual() && !jaViuAviso) setTimeout(abrirAviso, 1500);
    }

    atualizarConta();

    window.DocuraConta = {
        usuario: usuarioAtual,
        estaCadastrado: () => !!usuarioAtual()
    };
})();

// Sacola, adicionar ao carrinho, checkout e confirmação do pedido
(function () {
    const CHAVE_SACOLA = "docura_sacola";
    const CHAVE_USUARIO = "docura_usuario";

    const btnAbrirSacola = document.getElementById("btnAbrirSacola");
    const badge = document.getElementById("badgeSacola");
    const sacolaOverlay = document.getElementById("sacolaOverlay");
    const sacolaFechar = document.getElementById("sacolaFechar");
    const sacolaItens = document.getElementById("sacolaItens");
    const sacolaTotal = document.getElementById("sacolaTotal");
    const sacolaFinalizar = document.getElementById("sacolaFinalizar");

    const checkoutOverlay = document.getElementById("checkoutOverlay");
    const formCheckout = document.getElementById("formCheckout");
    const checkoutItens = document.getElementById("checkoutItens");
    const checkoutTotal = document.getElementById("checkoutTotal");
    const checkoutLoginAviso = document.getElementById("checkoutLoginAviso");
    const checkoutAbrirConta = document.getElementById("checkoutAbrirConta");
    const enderecoEntrega = document.getElementById("enderecoEntrega");
    const btnFinalizarForm = formCheckout ? formCheckout.querySelector(".checkout-finalizar") : null;

    const confirmacaoOverlay = document.getElementById("confirmacaoOverlay");
    const confirmacaoResumo = document.getElementById("confirmacaoResumo");
    const btnConfirmacaoFechar = document.getElementById("btnConfirmacaoFechar");

    const toast = document.getElementById("toastAviso");
    const toastTexto = document.getElementById("toastTexto");
    const contaOverlay = document.getElementById("contaOverlay");

    if (!sacolaOverlay || !sacolaItens || !btnAbrirSacola) return;

    function lerSacolaSalva() {
        try {
            const bruto = localStorage.getItem(CHAVE_SACOLA);
            const dados = bruto ? JSON.parse(bruto) : {};
            return dados && typeof dados === "object" ? dados : {};
        } catch (erro) {
            return {};
        }
    }

    function salvarSacola() {
        try { localStorage.setItem(CHAVE_SACOLA, JSON.stringify(sacolaGlobal)); } catch (erro) { /* ignora */ }
    }

    function usuarioLogado() {
        try {
            const bruto = localStorage.getItem(CHAVE_USUARIO);
            const dados = bruto ? JSON.parse(bruto) : null;
            return dados && dados.idUsuario ? dados : null;
        } catch (erro) {
            return null;
        }
    }

    // a chave de cada item passa a ser o nome normalizado do produto (migra sacolas antigas)
    Object.values(lerSacolaSalva()).forEach((item) => {
        if (!item || !item.nome) return;
        const chave = normalizar(item.nome);
        if (sacolaGlobal[chave]) {
            sacolaGlobal[chave].quantidade += item.quantidade || 1;
        } else {
            sacolaGlobal[chave] = {
                id: chave,
                nome: item.nome,
                preco: item.preco,
                img: item.img || "",
                idProduto: item.idProduto || null,
                quantidade: item.quantidade || 1
            };
        }
    });

    function lerPreco(texto) {
        const numero = parseFloat(String(texto).replace(/[^\d,]/g, "").replace(",", "."));
        return isNaN(numero) ? 0 : numero;
    }

    function itensDaSacola() {
        return Object.values(sacolaGlobal);
    }

    function totalDaSacola() {
        return itensDaSacola().reduce((soma, item) => soma + item.preco * item.quantidade, 0);
    }

    function quantidadeTotal() {
        return itensDaSacola().reduce((soma, item) => soma + item.quantidade, 0);
    }

    let timerToast;
    function avisar(mensagem, comLink) {
        if (!toast || !toastTexto) return;
        toastTexto.textContent = mensagem;
        const antigo = toast.querySelector("small");
        if (antigo) antigo.remove();
        if (comLink) {
            const link = document.createElement("small");
            link.textContent = "Ver sacola";
            toast.appendChild(link);
        }
        toast.dataset.abreSacola = comLink ? "1" : "";
        toast.classList.add("ativo");
        clearTimeout(timerToast);
        timerToast = setTimeout(() => toast.classList.remove("ativo"), 3500);
    }

    if (toast) {
        toast.addEventListener("click", () => {
            if (toast.dataset.abreSacola === "1") {
                toast.classList.remove("ativo");
                abrirSacola();
            }
        });
    }

    function travarScroll(travar) {
        if (travar) {
            document.body.style.overflow = "hidden";
        } else if (!document.querySelector(".sheet-fundo.aberto, .aviso-conta-overlay.ativo, .conta-overlay.ativo")) {
            document.body.style.overflow = "";
        }
    }

    function criarBotaoQtd(acao, id, icone, rotulo, classeExtra) {
        const botao = document.createElement("button");
        botao.type = "button";
        botao.className = "resumo-btn-qtd" + (classeExtra ? " " + classeExtra : "");
        botao.dataset.acao = acao;
        botao.dataset.id = id;
        botao.setAttribute("aria-label", rotulo);
        botao.innerHTML = icone;
        return botao;
    }

    function criarCardItem(item, editavel) {
        const card = document.createElement("div");
        card.className = "resumo-item-card";

        if (item.img) {
            const img = document.createElement("img");
            img.className = "resumo-item-img";
            img.src = item.img;
            img.alt = "";
            card.appendChild(img);
        }

        const detalhes = document.createElement("div");
        detalhes.className = "resumo-item-detalhes";
        const titulo = document.createElement("h4");
        titulo.textContent = item.nome;
        const valor = document.createElement("span");
        valor.textContent = editavel
            ? dinheiro(item.preco * item.quantidade)
            : `${item.quantidade}x · ${dinheiro(item.preco * item.quantidade)}`;
        detalhes.append(titulo, valor);
        card.appendChild(detalhes);

        if (editavel) {
            const controles = document.createElement("div");
            controles.className = "resumo-item-controles";
            const qtd = document.createElement("span");
            qtd.className = "resumo-qtd-num";
            qtd.textContent = item.quantidade;
            controles.append(
                criarBotaoQtd("menos", item.id, "&minus;", "Diminuir quantidade"),
                qtd,
                criarBotaoQtd("mais", item.id, "+", "Aumentar quantidade"),
                criarBotaoQtd("remover", item.id, '<i class="bi bi-trash"></i>', "Remover item", "resumo-btn-remover")
            );
            card.appendChild(controles);
        }

        return card;
    }

    function renderSacola() {
        const itens = itensDaSacola();
        const qtd = quantidadeTotal();

        badge.textContent = qtd;
        badge.style.display = qtd ? "" : "none";

        sacolaItens.textContent = "";
        if (!itens.length) {
            const vazio = document.createElement("p");
            vazio.className = "resumo-vazio";
            vazio.textContent = "Sua sacola está vazia.";
            sacolaItens.appendChild(vazio);
        } else {
            itens.forEach((item) => sacolaItens.appendChild(criarCardItem(item, true)));
        }

        sacolaTotal.textContent = dinheiro(totalDaSacola());
        sacolaFinalizar.disabled = !itens.length;
        salvarSacola();
    }

    function adicionarItem(produto) {
        const nome = (produto.querySelector("h3") || {}).textContent;
        const precoTexto = (produto.querySelector(".produto-preco") || {}).textContent;
        if (!nome || !precoTexto) return null;

        const preco = lerPreco(precoTexto);
        const id = normalizar(nome);
        const imagem = produto.querySelector("img");
        const idProduto = Number(produto.dataset.idProduto) || null;

        if (sacolaGlobal[id]) {
            sacolaGlobal[id].quantidade += 1;
            sacolaGlobal[id].preco = preco;
            sacolaGlobal[id].idProduto = idProduto;
        } else {
            sacolaGlobal[id] = {
                id,
                nome: nome.trim(),
                preco,
                img: imagem ? imagem.getAttribute("src") : "",
                idProduto,
                quantidade: 1
            };
        }

        renderSacola();
        return sacolaGlobal[id];
    }

    document.addEventListener("click", async (evento) => {
        const botao = evento.target.closest(".btn-produto");
        if (!botao) return;

        const produto = botao.closest(".produto");
        if (!produto) return;

        if (!(await garantirCatalogo())) {
            avisar("Não foi possível conectar à loja. Tente novamente em instantes.");
            return;
        }

        if (produto.dataset.indisponivel === "1") {
            avisar("Este produto está indisponível no momento.");
            return;
        }

        const item = adicionarItem(produto);
        if (!item) return;

        avisar(`${item.nome} adicionado à sacola!`, true);

        const textoOriginal = botao.dataset.textoOriginal || botao.textContent;
        botao.dataset.textoOriginal = textoOriginal;
        botao.textContent = "Adicionado ✓";
        clearTimeout(botao._timer);
        botao._timer = setTimeout(() => { botao.textContent = textoOriginal; }, 1200);
    });

    sacolaItens.addEventListener("click", (evento) => {
        const botao = evento.target.closest("[data-acao]");
        if (!botao) return;

        const item = sacolaGlobal[botao.dataset.id];
        if (!item) return;

        if (botao.dataset.acao === "mais") item.quantidade += 1;
        if (botao.dataset.acao === "menos") item.quantidade -= 1;
        if (botao.dataset.acao === "remover") item.quantidade = 0;

        if (item.quantidade <= 0) delete sacolaGlobal[item.id];
        renderSacola();
    });

    function abrirSacola() {
        renderSacola();
        sacolaOverlay.classList.add("ativo");
        sacolaOverlay.setAttribute("aria-hidden", "false");
        travarScroll(true);
    }

    function fecharSacola(restaurarScroll) {
        sacolaOverlay.classList.remove("ativo");
        sacolaOverlay.setAttribute("aria-hidden", "true");
        if (restaurarScroll !== false) travarScroll(false);
    }

    btnAbrirSacola.addEventListener("click", (evento) => {
        evento.stopPropagation();
        abrirSacola();
    });

    sacolaFechar.addEventListener("click", () => fecharSacola());

    sacolaOverlay.addEventListener("click", (evento) => {
        if (evento.target === sacolaOverlay) fecharSacola();
    });

    const campos = formCheckout ? {
        nome: formCheckout.elements.nome,
        telefone: formCheckout.elements.telefone,
        email: formCheckout.elements.email
    } : {};

    function mascararTelefone(campo) {
        const n = campo.value.replace(/\D/g, "").slice(0, 11);
        let texto = n;
        if (n.length > 10) texto = `(${n.slice(0, 2)}) ${n.slice(2, 7)}-${n.slice(7)}`;
        else if (n.length > 6) texto = `(${n.slice(0, 2)}) ${n.slice(2, 6)}-${n.slice(6)}`;
        else if (n.length > 2) texto = `(${n.slice(0, 2)}) ${n.slice(2)}`;
        else if (n.length > 0) texto = `(${n}`;
        campo.value = texto;
    }

    function dataDeHoje() {
        const hoje = new Date();
        const mes = String(hoje.getMonth() + 1).padStart(2, "0");
        const dia = String(hoje.getDate()).padStart(2, "0");
        return `${hoje.getFullYear()}-${mes}-${dia}`;
    }

    function renderCheckout() {
        checkoutItens.textContent = "";
        itensDaSacola().forEach((item) => checkoutItens.appendChild(criarCardItem(item, false)));
        checkoutTotal.textContent = dinheiro(totalDaSacola());
    }

    function atualizarUsuarioNoCheckout() {
        if (!formCheckout) return;
        const usuario = usuarioLogado();

        checkoutLoginAviso.hidden = !!usuario;
        if (btnFinalizarForm) btnFinalizarForm.disabled = !usuario;

        ["nome", "telefone", "email"].forEach((nome) => {
            const campo = campos[nome];
            if (!campo) return;

            if (usuario) {
                if (!campo.dataset.editado) {
                    campo.value = usuario[nome] || "";
                    campo.dataset.autopreenchido = "1";
                }
            } else if (campo.dataset.autopreenchido) {
                campo.value = "";
                delete campo.dataset.autopreenchido;
                delete campo.dataset.editado;
            }
        });
    }

    function atualizarTipoRecebimento() {
        const retirada = formCheckout.elements.recebimento.value === "retirada";
        enderecoEntrega.classList.toggle("oculto", retirada);
        ["rua", "bairro", "cep", "cidade"].forEach((nome) => {
            formCheckout.elements[nome].required = !retirada;
        });
    }

    function abrirCheckout() {
        if (!itensDaSacola().length) return;

        fecharSacola(false);
        renderCheckout();
        atualizarUsuarioNoCheckout();
        atualizarTipoRecebimento();
        formCheckout.elements.data.min = dataDeHoje();

        checkoutOverlay.classList.add("ativo");
        checkoutOverlay.setAttribute("aria-hidden", "false");
        checkoutOverlay.scrollTop = 0;
        travarScroll(true);
    }

    function fecharCheckout() {
        checkoutOverlay.classList.remove("ativo");
        checkoutOverlay.setAttribute("aria-hidden", "true");
        travarScroll(false);
    }

    function abrirConta() {
        const botaoConta = document.getElementById("btnAbrirConta");
        if (botaoConta) botaoConta.click();
    }

    if (formCheckout && checkoutOverlay) {
        sacolaFinalizar.addEventListener("click", abrirCheckout);

        document.getElementById("fecharCheckout").addEventListener("click", fecharCheckout);
        document.getElementById("voltarMontagem").addEventListener("click", () => {
            fecharCheckout();
            abrirSacola();
        });

        checkoutAbrirConta.addEventListener("click", abrirConta);

        formCheckout.querySelectorAll('input[name="recebimento"]').forEach((radio) => {
            radio.addEventListener("change", atualizarTipoRecebimento);
        });

        Object.values(campos).forEach((campo) => {
            campo.addEventListener("input", () => { campo.dataset.editado = "1"; });
        });
        campos.telefone.addEventListener("input", () => mascararTelefone(campos.telefone));

        const campoCep = formCheckout.elements.cep;
        if (campoCep) {
            campoCep.addEventListener("input", () => {
                const n = campoCep.value.replace(/\D/g, "").slice(0, 8);
                campoCep.value = n.length > 5 ? `${n.slice(0, 5)}-${n.slice(5)}` : n;
            });
        }

        if (contaOverlay) {
            new MutationObserver(() => {
                if (checkoutOverlay.classList.contains("ativo")) atualizarUsuarioNoCheckout();
            }).observe(contaOverlay, { attributes: true, attributeFilter: ["class"], subtree: true });
        }

        formCheckout.addEventListener("submit", (evento) => {
            evento.preventDefault();

            if (!usuarioLogado()) {
                atualizarUsuarioNoCheckout();
                checkoutLoginAviso.scrollIntoView({ behavior: "smooth", block: "center" });
                return;
            }

            if (!formCheckout.reportValidity()) return;
            if (!itensDaSacola().length) return;

            finalizarPedido();
        });
    }

    function formatarData(valor) {
        const partes = String(valor).split("-");
        return partes.length === 3 ? `${partes[2]}/${partes[1]}/${partes[0]}` : valor;
    }

    const TURNOS = {
        "manha": "Manhã",
        "inicio da tarde": "Início da Tarde",
        "fim da tarde": "Fim da Tarde"
    };

    async function finalizarPedido() {
        const usuario = usuarioLogado();
        if (!usuario) return;

        const dados = new FormData(formCheckout);
        const retirada = dados.get("recebimento") === "retirada";

        const turno = TURNOS[normalizar(dados.get("horario"))];
        if (!turno) {
            avisar("Escolha o horário preferido.");
            return;
        }

        // sem endereço = retirada na loja (a API grava id_endereco nulo)
        let endereco = null;
        if (!retirada) {
            const cep = String(dados.get("cep") || "").replace(/\D/g, "");
            if (cep.length !== 8) {
                avisar("Informe um CEP válido (8 dígitos).");
                formCheckout.elements.cep.focus();
                return;
            }
            endereco = {
                ruaNumero: String(dados.get("rua") || "").trim(),
                bairro: String(dados.get("bairro") || "").trim(),
                cep: `${cep.slice(0, 5)}-${cep.slice(5)}`,
                complemento: String(dados.get("complemento") || "").trim() || null,
                cidade: String(dados.get("cidade") || "").trim()
            };
        }

        if (!(await garantirCatalogo())) {
            avisar("Não foi possível conectar à loja. Tente novamente em instantes.");
            return;
        }

        const itens = [];
        for (const item of itensDaSacola()) {
            const dado = catalogo.get(normalizar(item.nome));
            if (!dado) {
                avisar(`"${item.nome}" não está mais disponível.`);
                return;
            }
            itens.push({ idProduto: dado.id, quantidade: item.quantidade, nome: item.nome });
        }

        let resposta;
        if (btnFinalizarForm) btnFinalizarForm.disabled = true;
        try {
            resposta = await api("/pedidos", {
                method: "POST",
                body: {
                    idUsuario: usuario.idUsuario,
                    endereco,
                    dataEntrega: dados.get("data"),
                    turnoEntrega: turno,
                    itens: itens.map((i) => ({ idProduto: i.idProduto, quantidade: i.quantidade }))
                }
            });
        } catch (erro) {
            avisar(erro.message);
            return;
        } finally {
            if (btnFinalizarForm) btnFinalizarForm.disabled = !usuarioLogado();
        }

        const id = "#" + resposta.id_pedido;
        const total = Number(resposta.valor_total);
        const tipo = retirada ? "Retirada na loja" : "Entrega";

        const linhas = [`Pedido ${id}`, ""];
        itens.forEach((item) => linhas.push(`${item.quantidade}x ${item.nome}`));
        linhas.push("", `Total: ${dinheiro(total)}`, "", tipo);

        if (endereco) {
            const texto = [endereco.ruaNumero, endereco.bairro, endereco.cidade]
                .filter(Boolean)
                .join(", ");
            if (texto) linhas.push(texto);
        }

        linhas.push(`Data: ${formatarData(dados.get("data"))} · ${dados.get("horario")}`);
        confirmacaoResumo.textContent = linhas.join("\n");

        Object.keys(sacolaGlobal).forEach((chave) => delete sacolaGlobal[chave]);
        renderSacola();
        formCheckout.reset();
        ["nome", "telefone", "email"].forEach((nome) => {
            delete campos[nome].dataset.editado;
            delete campos[nome].dataset.autopreenchido;
        });

        fecharCheckout();
        confirmacaoOverlay.classList.add("ativo");
        confirmacaoOverlay.setAttribute("aria-hidden", "false");
        travarScroll(true);
    }

    function fecharConfirmacao() {
        confirmacaoOverlay.classList.remove("ativo");
        confirmacaoOverlay.setAttribute("aria-hidden", "true");
        travarScroll(false);
    }

    if (btnConfirmacaoFechar) btnConfirmacaoFechar.addEventListener("click", fecharConfirmacao);

    if (confirmacaoOverlay) {
        confirmacaoOverlay.addEventListener("click", (evento) => {
            if (evento.target === confirmacaoOverlay) fecharConfirmacao();
        });
    }

    document.addEventListener("keydown", (evento) => {
        if (evento.key !== "Escape") return;
        if (contaOverlay && contaOverlay.classList.contains("ativo")) return;
        if (confirmacaoOverlay.classList.contains("ativo")) fecharConfirmacao();
        else if (checkoutOverlay.classList.contains("ativo")) fecharCheckout();
        else if (sacolaOverlay.classList.contains("ativo")) fecharSacola();
    });

    // atualiza preço/id dos itens salvos na sacola com o que está no banco
    async function sincronizarSacola() {
        if (!(await catalogoPronto)) return;

        let removidos = 0;
        Object.keys(sacolaGlobal).forEach((chave) => {
            const dado = catalogo.get(chave);
            if (!dado) {
                delete sacolaGlobal[chave];
                removidos += 1;
                return;
            }
            sacolaGlobal[chave].idProduto = dado.id;
            sacolaGlobal[chave].preco = dado.preco;
        });

        renderSacola();
        if (removidos) avisar("Alguns itens da sacola não estão mais disponíveis e foram removidos.");
    }

    renderSacola();
    sincronizarSacola();

    window.DocuraSacola = {
        abrir: abrirSacola,
        itens: itensDaSacola,
        total: totalDaSacola
    };
})();