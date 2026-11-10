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

// Conta do cliente (cadastro, painel lateral, histórico e sair)
(function () {
    const CHAVE_USUARIO = "docura_usuario";
    const CHAVE_PEDIDOS = "docura_pedidos";
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
        return lerJSON(CHAVE_USUARIO, null);
    }

    async function protegerSenha(senha) {
        if (!window.crypto || !window.crypto.subtle) return null;

        const salt = Array.from(crypto.getRandomValues(new Uint8Array(16)))
            .map((b) => b.toString(16).padStart(2, "0"))
            .join("");
        const bytes = new TextEncoder().encode(salt + senha);
        const resumo = await crypto.subtle.digest("SHA-256", bytes);
        const hash = Array.from(new Uint8Array(resumo))
            .map((b) => b.toString(16).padStart(2, "0"))
            .join("");

        return { salt, hash };
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
        mostrarHistorico(usuario.email);
    }

    function mostrarHistorico(email) {
        const pedidos = lerJSON(CHAVE_PEDIDOS, []).filter((p) => p.email === email);
        listaHistorico.textContent = "";

        if (!pedidos.length) {
            const vazio = document.createElement("p");
            vazio.className = "historico-vazio";
            vazio.textContent = "Você ainda não fez nenhum pedido. Quando fizer, ele aparece aqui.";
            listaHistorico.appendChild(vazio);
            return;
        }

        pedidos.forEach((pedido) => {
            const card = document.createElement("article");
            card.className = "historico-card";

            const topo = document.createElement("div");
            topo.className = "historico-topo";
            const codigo = document.createElement("span");
            codigo.textContent = `Pedido ${pedido.id}`;
            const data = document.createElement("span");
            data.className = "historico-data";
            const quando = new Date(pedido.data);
            data.textContent = isNaN(quando)
                ? ""
                : quando.toLocaleDateString("pt-BR") + " às " +
                  quando.toLocaleTimeString("pt-BR", { hour: "2-digit", minute: "2-digit" });
            topo.append(codigo, data);

            const itens = document.createElement("ul");
            itens.className = "historico-itens";
            (pedido.itens || []).forEach((item) => {
                const linha = document.createElement("li");
                linha.textContent = `${item.quantidade || 1}x ${item.nome}`;
                itens.appendChild(linha);
            });

            const rodape = document.createElement("div");
            rodape.className = "historico-rodape";
            const tipo = document.createElement("span");
            tipo.textContent = pedido.tipo || "Pedido";
            const total = document.createElement("strong");
            total.textContent = dinheiro(pedido.total);
            rodape.append(tipo, total);

            card.append(topo, itens, rodape);
            listaHistorico.appendChild(card);
        });
    }

    function registrarPedido(pedido) {
        const usuario = usuarioAtual();
        if (!usuario || !pedido) return false;

        const pedidos = lerJSON(CHAVE_PEDIDOS, []);
        pedidos.unshift({
            id: pedido.id || "#" + String(Date.now()).slice(-6),
            email: usuario.email,
            data: pedido.data || new Date().toISOString(),
            itens: pedido.itens || [],
            total: pedido.total === undefined ? null : pedido.total,
            tipo: pedido.tipo || ""
        });

        return salvarJSON(CHAVE_PEDIDOS, pedidos.slice(0, 50));
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

    formConta.addEventListener("submit", async (evento) => {
        evento.preventDefault();
        if (!validarFormulario()) return;

        const usuario = {
            nome: campoNome.value.trim(),
            telefone: campoTelefone.value.trim(),
            email: campoEmail.value.trim().toLowerCase(),
            senha: await protegerSenha(campoSenha.value),
            criadoEm: new Date().toISOString()
        };

        if (!salvarJSON(CHAVE_USUARIO, usuario)) {
            avisar("Não foi possível salvar seus dados neste navegador.");
            return;
        }

        formConta.reset();
        atualizarConta();
        avisar("Conta criada! Seus dados foram salvos.");
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
        registrarPedido,
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
            return bruto ? JSON.parse(bruto) : null;
        } catch (erro) {
            return null;
        }
    }

    Object.assign(sacolaGlobal, lerSacolaSalva());

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
        const id = nome.trim() + "|" + preco.toFixed(2);
        const imagem = produto.querySelector("img");

        if (sacolaGlobal[id]) {
            sacolaGlobal[id].quantidade += 1;
        } else {
            sacolaGlobal[id] = {
                id,
                nome: nome.trim(),
                preco,
                img: imagem ? imagem.getAttribute("src") : "",
                quantidade: 1
            };
        }

        renderSacola();
        return sacolaGlobal[id];
    }

    document.addEventListener("click", (evento) => {
        const botao = evento.target.closest(".btn-produto");
        if (!botao) return;

        const produto = botao.closest(".produto");
        if (!produto) return;

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
        ["rua", "bairro", "cidade"].forEach((nome) => {
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

    function finalizarPedido() {
        const dados = new FormData(formCheckout);
        const retirada = dados.get("recebimento") === "retirada";
        const itens = itensDaSacola().map((item) => ({
            nome: item.nome,
            quantidade: item.quantidade,
            preco: item.preco
        }));
        const total = totalDaSacola();
        const id = "#" + String(Date.now()).slice(-6);
        const tipo = retirada ? "Retirada na loja" : "Entrega";

        if (window.DocuraConta) {
            window.DocuraConta.registrarPedido({ id, itens, total, tipo });
        }

        const linhas = [`Pedido ${id}`, ""];
        itens.forEach((item) => linhas.push(`${item.quantidade}x ${item.nome}`));
        linhas.push("", `Total: ${dinheiro(total)}`, "", tipo);

        if (!retirada) {
            const endereco = [dados.get("rua"), dados.get("bairro"), dados.get("cidade")]
                .map((parte) => String(parte || "").trim())
                .filter(Boolean)
                .join(", ");
            if (endereco) linhas.push(endereco);
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

    renderSacola();

    window.DocuraSacola = {
        abrir: abrirSacola,
        itens: itensDaSacola,
        total: totalDaSacola
    };
})();