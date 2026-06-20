using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using CampusQuest.Core;
using CampusQuest.Itens;
using CampusQuest.Materias;
using CampusQuest.NPCs;
using CampusQuest.Persistencia;
using CampusQuest.Quiz;
using CampusQuest.Estados;


namespace CampusQuest.WinForms
{
    public partial class TelaInicial : Form
    {
        private Aluno? alunoAtual;
        private Veterano? veteranoAtual;
        private Professor? professorAtual;
        private Pergunta[] perguntasQuiz = Array.Empty<Pergunta>();
        private int perguntaAtual = 0;
        private int acertosQuiz = 0;
        private int semestreQuiz = 1;
        private Materia? chefeAtual;
        private List<Pergunta>? perguntasExame;
        private int perguntaExameAtual;
        private int acertosExame;
        private int errosExame;
        private Persistencia.IRepositorio? repositorio;
        private IRepositorioQuestoes repositorioQuestoes;

        private void DesbloquearHabilidade()
        {
            string habilidade = "";

            switch (alunoAtual.SemestreAtual)
            {
                case 2:
                    habilidade = "Lógica de Programação";
                    break;

                case 3:
                    habilidade = "Estruturas de Dados";
                    break;

                case 4:
                    habilidade = "Programação Orientada a Objetos";
                    break;
            }

            if (!string.IsNullOrEmpty(habilidade))
            {
                rtbMensagens.AppendText(
                    $"\nNova habilidade desbloqueada: {habilidade}\n");
            }
        }

        public TelaInicial()
        {
            InitializeComponent();
            repositorioQuestoes = new SqliteRepositorioQuestoes();
            repositorio = new RepositorioJson("saves/slot1.json");
        }

        private void TelaInicial_Load(object sender, EventArgs e)
        {
            panelMenu.Visible = true;
            panelNome.Visible = false;
            panelHall.Visible = false;
            panelMenu.BringToFront();
        }

        private void btnNovoJogo_Click(object sender, EventArgs e)
        {
            panelMenu.Visible = false;
            panelNome.Visible = true;
            panelNome.BringToFront();
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNome.Text))
            {
                MessageBox.Show("Digite um nome.");
                return;
            }

            alunoAtual = new Aluno();
            alunoAtual.DefinirNome(txtNome.Text);

            panelNome.Visible = false;
            panelHall.Visible = true;

            rtbMensagens.Clear();
            rtbMensagens.AppendText($"Bem-vindo, {alunoAtual.Nome}!\n");
            rtbMensagens.AppendText("Escolha uma opção pelos botões abaixo.\n");
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnCarregarJogo_Click(object sender, EventArgs e)
        {
            if (!repositorio.ExisteArquivo())
            {
                MessageBox.Show("Nenhum save encontrado.", "Carregar Jogo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                EstadoJogo estado = repositorio.Carregar();
                alunoAtual = new Aluno();
                EstadoJogoFactory.Aplicar(alunoAtual, estado);

                panelMenu.Visible = false;
                panelHall.Visible = true;
                panelHall.BringToFront();

                rtbMensagens.Clear();
                rtbMensagens.AppendText($"Bem-vindo de volta, {alunoAtual.Nome}!\n");
                rtbMensagens.AppendText($"Semestre: {alunoAtual.SemestreAtual}\n");
                rtbMensagens.AppendText($"Vida: {alunoAtual.Vida}/{alunoAtual.VidaMaxima}\n");
                rtbMensagens.AppendText($"Conhecimento: {alunoAtual.Conhecimento}\n");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar jogo: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ====================== VETERANO ======================
        private void opc1_Click(object sender, EventArgs e)
        {
            if (alunoAtual == null) return;

            panelHall.Visible = false;
            panelVeterano.Visible = true;

            veteranoAtual = new Veterano();

            rtbVeterano.Clear();
            rtbVeterano.AppendText(veteranoAtual.GetMensagemAbertura(alunoAtual));
        }

        private void btnPedirDica_Click(object sender, EventArgs e)
        {
            if (alunoAtual == null) return;

            Veterano veterano = new Veterano();
            rtbVeterano.Clear();
            rtbVeterano.AppendText(veterano.ObterDica(alunoAtual));
        }

        private void btnPedirItem_Click(object sender, EventArgs e)
        {
            if (alunoAtual == null || veteranoAtual == null) return;

            var item = veteranoAtual.SolicitarItem(alunoAtual);

            if (item == null)
            {
                rtbVeterano.AppendText("\nNenhum item disponível no momento.\n");
                return;
            }

            if (!alunoAtual.PodeReceberItem(item))
            {
                rtbVeterano.AppendText("\nLimite de itens do semestre atingido.\n");
                return;
            }

            if (alunoAtual.Inventario.Adicionar(item))
            {
                alunoAtual.RegistrarItemRecebido(item);
                rtbVeterano.AppendText($"\nItem recebido: {item.GetType().Name}\n");
            }
            else
            {
                rtbVeterano.AppendText("\nInventário cheio. Item não adicionado.\n");
            }
        }

        private void btnVoltarVeterano_Click(object sender, EventArgs e)
        {
            panelVeterano.Visible = false;
            panelHall.Visible = true;
        }

        // ====================== PROFESSOR ======================
        private void opc2_Click(object sender, EventArgs e)
        {
            if (alunoAtual == null)
            {
                MessageBox.Show("Nenhum aluno logado!");
                return;
            }

            panelHall.Visible = false;
            panelProfessor.Visible = true;
            panelProfessor.BringToFront();

            Professor professor = new Professor(repositorioQuestoes);
            rtbProfessor.Clear();
            rtbProfessor.AppendText(professor.GetMensagemAbertura(alunoAtual));
        }

        private void btnPedirItemProfessor_Click(object sender, EventArgs e)
        {
            if (alunoAtual == null) return;

            Professor professor = new Professor(repositorioQuestoes);
            string mensagem = professor.MensagemItemIndisponivel(alunoAtual);

            rtbProfessor.Clear();
            rtbProfessor.AppendText(mensagem);
        }

        private void btnPedirDica2_Click(object sender, EventArgs e)
        {
            if (alunoAtual == null) return;

            Professor professor = new Professor(repositorioQuestoes);
            string dica = professor.ObterDica(alunoAtual);

            rtbProfessor.Clear();
            rtbProfessor.AppendText(dica);
        }

        private void btnFazerQuiz_Click(object sender, EventArgs e)
        {
            DialogResult escolha = MessageBox.Show(
                "Sim = Semestre 1\nNão = Semestre 2\nCancelar = Semestre 3",
                "Escolha o semestre",
                MessageBoxButtons.YesNoCancel);

            if (escolha == DialogResult.Yes)
                semestreQuiz = 1;
            else if (escolha == DialogResult.No)
                semestreQuiz = 2;
            else
                semestreQuiz = 3;

            CarregarPerguntasQuiz();

            perguntaAtual = 0;
            acertosQuiz = 0;

            rtbQuiz.Clear();

            panelProfessor.Visible = false;
            panelQuiz.Visible = true;

            MostrarPerguntaAtual();
        }

        private void MostrarPerguntaAtual()
        {
            Pergunta pergunta = perguntasQuiz[perguntaAtual];

            rtbQuiz.Clear();
            rtbQuiz.AppendText($"Pergunta {perguntaAtual + 1} de {perguntasQuiz.Length}\n\n");
            rtbQuiz.AppendText(pergunta.Enunciado);

            btnAltA.Text = pergunta.Alternativas[0];
            btnAltB.Text = pergunta.Alternativas[1];
            btnAltC.Text = pergunta.Alternativas[2];
            btnAltD.Text = pergunta.Alternativas[3];

            rtbQuiz.SelectionStart = 0;
            rtbQuiz.ScrollToCaret();
        }

        private void ResponderPergunta(int indiceEscolhido)
        {
            Pergunta pergunta = perguntasQuiz[perguntaAtual];

            rtbQuiz.AppendText("\n\n");

            if (pergunta.VerificarResposta(indiceEscolhido))
            {
                acertosQuiz++;
                rtbQuiz.AppendText("✓ Correto!\n");
            }
            else
            {
                rtbQuiz.AppendText("✗ Errado!\n");
                rtbQuiz.AppendText(pergunta.Explicacao + "\n");
            }

            perguntaAtual++;

            if (perguntaAtual >= perguntasQuiz.Length)
            {
                FinalizarQuiz();
                return;
            }

            MostrarPerguntaAtual();
        }

        private void FinalizarQuiz()
        {
            int aproveitamento = (int)((double)acertosQuiz / perguntasQuiz.Length * 100);

            bool revisao = semestreQuiz < alunoAtual?.SemestreAtual;
            int ganhoConhecimento = acertosQuiz * 10 * (revisao ? 50 : 100) / 100;

            alunoAtual?.AumentarConhecimento(ganhoConhecimento);

            string recompensa = "Nenhuma";
            CampusQuest.Core.Item? itemRecebido = null;

            if (aproveitamento >= 80)
            {
                string materiaAlvo = semestreQuiz switch
                {
                    1 => "IC",
                    2 => "AED",
                    3 => "POO",
                    _ => "IC"
                };
                itemRecebido = new LivroTecnico(materiaAlvo);
            }
            else if (aproveitamento >= 50)
            {
                itemRecebido = new Caderno();
            }
            else
            {
                itemRecebido = new Cafe();
            }

            if (alunoAtual != null && itemRecebido != null && alunoAtual.PodeReceberItem(itemRecebido))
            {
                if (alunoAtual.Inventario.Adicionar(itemRecebido))
                {
                    alunoAtual.RegistrarItemRecebido(itemRecebido);
                    recompensa = itemRecebido.GetType().Name;
                }
                else
                {
                    rtbProfessor.AppendText("Inventário cheio. Item não adicionado.\n");
                }
            }
            else if (alunoAtual != null && itemRecebido != null)
            {
                rtbProfessor.AppendText($"Limite de {itemRecebido.GetType().Name} do semestre atingido.\n");
            }

            panelQuiz.Visible = false;
            panelProfessor.Visible = true;

            rtbProfessor.Clear();
            rtbProfessor.AppendText("=== QUIZ CONCLUÍDO ===\n");
            rtbProfessor.AppendText($"Acertos: {acertosQuiz}/{perguntasQuiz.Length}\n");
            rtbProfessor.AppendText($"Aproveitamento: {aproveitamento}%\n");
            rtbProfessor.AppendText($"Conhecimento ganho: +{ganhoConhecimento}\n");
            rtbProfessor.AppendText($"Item recebido: {recompensa}\n");
            rtbProfessor.AppendText("Escolha uma nova ação abaixo.\n");
        }

        private void CarregarPerguntasQuiz()
        {
            switch (semestreQuiz)
            {
                case 1:
                    perguntasQuiz = new Pergunta[]
                    {
                        new Pergunta("O que é hardware?",
                            new[] {"Parte física","Programa","Rede","Arquivo"}, 0,
                            "Hardware é a parte física."),
                        new Pergunta("O que é software?",
                            new[] {"Programa","Placa","Teclado","Memória"}, 0,
                            "Software é o conjunto de programas."),
                        new Pergunta("Qual sistema gerencia recursos do computador?",
                            new[] {"Sistema operacional","Editor","Navegador","Jogos"}, 0,
                            "O sistema operacional gerencia recursos."),
                        new Pergunta("Qual número representa binário?",
                            new[] {"0 e 1","2 e 3","5 e 6","8 e 9"}, 0,
                            "Binário usa 0 e 1."),
                        new Pergunta("Qual dispositivo conecta redes?",
                            new[] {"Roteador","Mouse","Monitor","Teclado"}, 0,
                            "Roteador conecta redes.")
                    };
                    break;

                case 2:
                    perguntasQuiz = new Pergunta[]
                    {
                        new Pergunta("Pilha segue qual regra?",
                            new[] {"LIFO","FIFO","ABC","XYZ"}, 0,
                            "Pilha é LIFO."),
                        new Pergunta("Fila segue qual regra?",
                            new[] {"FIFO","LIFO","DFS","BFS"}, 0,
                            "Fila é FIFO."),
                        new Pergunta("Busca binária exige dados:",
                            new[] {"Ordenados","Aleatórios","Vazios","Repetidos"}, 0,
                            "Busca binária exige ordenação."),
                        new Pergunta("Complexidade O(n) indica crescimento:",
                            new[] {"Linear","Constante","Quadrático","Logarítmico"}, 0,
                            "O(n) é linear."),
                        new Pergunta("Qual estrutura combina recursão?",
                            new[] {"Pilha","Fila","Lista","Matriz"}, 0,
                            "Recursão usa pilha.")
                    };
                    break;

                default:
                    perguntasQuiz = new Pergunta[]
                    {
                        new Pergunta("Encapsulamento controla:",
                            new[] {"Acesso aos dados","Rede","Compilação","Memória"}, 0,
                            "Encapsulamento controla acesso."),
                        new Pergunta("Herança permite:",
                            new[] {"Reuso","Apagar dados","Acelerar CPU","Criar rede"}, 0,
                            "Herança promove reuso."),
                        new Pergunta("Polimorfismo significa:",
                            new[] {"Mesmo método com comportamentos diferentes","Só um construtor","Sem classes","Sem objetos"}, 0,
                            "Polimorfismo altera comportamento."),
                        new Pergunta("Abstração é:",
                            new[] {"Focar no essencial","Duplicar código","Evitar classes","Criar bugs"}, 0,
                            "Abstração foca no essencial."),
                        new Pergunta("Objeto é:",
                            new[] {"Instância de classe","Método","Interface","Namespace"}, 0,
                            "Objeto é uma instância.")
                    };
                    break;
            }
        }

        private void btnVoltarProfessor_Click(object sender, EventArgs e)
        {
            panelProfessor.Visible = false;
            panelHall.Visible = true;
        }

        // ====================== SALA DE EXAME ======================

        private void opc3_Click(object sender, EventArgs e)
        {
            if (alunoAtual == null) return;

            panelHall.Visible = false;
            panelSalaExame.Visible = true;

            chefeAtual = CriarChefe(alunoAtual);

            perguntasExame = chefeAtual.GetPerguntasExame();

            perguntaExameAtual = 0;
            acertosExame = 0;
            errosExame = 0;

            lblMateria.Text = chefeAtual.Nome;

            AtualizarStatusExame();

            MostrarPerguntaExame();
        }

        private static Materia CriarChefe(Aluno aluno)
        {
            if (aluno.SemestresCompletos())
                return new TCC(aluno.GetMediaFinal());

            return aluno.SemestreAtual switch
            {
                1 => new IC(),
                2 => new AED(),
                3 => new POO(),
                _ => new IC()
            };
        }

        private void AtualizarStatusExame()
        {
            lblVidaAluno.Text =
                $"Aluno: {alunoAtual.Vida}/{alunoAtual.VidaMaxima}";

            lblVidaChefe.Text =
                $"Chefe: {chefeAtual.GetVidaAtual()}/{chefeAtual.GetVidaMaxima()}";
        }

        private void MostrarPerguntaExame()
        {
            Pergunta pergunta = perguntasExame[perguntaExameAtual];

            rtbExame.Text =
                $"Pergunta {perguntaExameAtual + 1} de {perguntasExame.Count}\n\n" +
                pergunta.Enunciado;

            btnExamA.Text = pergunta.Alternativas[0];
            btnExamB.Text = pergunta.Alternativas[1];
            btnExamC.Text = pergunta.Alternativas[2];
            btnExamD.Text = pergunta.Alternativas[3];
        }

        private void btnExamA_Click(object sender, EventArgs e)
        {
            ResponderPerguntaExame(0);
        }

        private void btnExamB_Click(object sender, EventArgs e)
        {
            ResponderPerguntaExame(1);
        }

        private void btnExamC_Click(object sender, EventArgs e)
        {
            ResponderPerguntaExame(2);
        }

        private void btnExamD_Click(object sender, EventArgs e)
        {
            ResponderPerguntaExame(3);
        }

        private void ResponderPerguntaExame(int respostaEscolhida)
        {
            Pergunta pergunta = perguntasExame[perguntaExameAtual];

            if (pergunta.VerificarResposta(respostaEscolhida))
            {
                acertosExame++;

                int danoAluno =
                (int)Math.Round(
                15 * (1 + alunoAtual.Conhecimento / 100.0));

                chefeAtual.ReceberDano(danoAluno);

                rtbExame.Text = "✓ Resposta correta!";
                Application.DoEvents();
                Thread.Sleep(1000);
            }
            else
            {
                errosExame++;

                int danoChefe =
                (int)Math.Round(
                20 * (1 - alunoAtual.Conhecimento / 100.0));

                danoChefe = Math.Max(1, danoChefe);

                alunoAtual.ReceberDano(danoChefe);

                rtbExame.Text =
                $"✗ Resposta incorreta!\n\n{pergunta.Explicacao}";

                Application.DoEvents();
                Thread.Sleep(2000);
            }

            AtualizarStatusExame();

            if (chefeAtual.EstaVencido())
            {
                VitoriaExame();
                return;
            }

            if (!alunoAtual.EstaVivo())
            {
                DerrotaExame();
                return;
            }

            perguntaExameAtual++;

            if (perguntaExameAtual >= perguntasExame.Count)
            {
                if (chefeAtual.GetVidaAtual() < alunoAtual.Vida)
                    VitoriaExame();
                else
                    DerrotaExame();

                return;
            }

            MostrarPerguntaExame();
        }

        private void VitoriaExame()
        {
            bool eraTCC = chefeAtual is TCC;
            int aproveitamento = (int)((double)acertosExame / perguntasExame!.Count * 100);

            alunoAtual!.RegistrarAproveitamento(alunoAtual.SemestreAtual, aproveitamento);
            alunoAtual.AvancarSemestre();

            panelSalaExame.Visible = false;

            if (eraTCC)
            {
                MostrarTelaVitoria(aproveitamento);
                return;
            }

            panelHall.Visible = true;
            rtbMensagens.Clear();
            rtbMensagens.AppendText("=== RESULTADO DO EXAME ===\n\n");
            rtbMensagens.AppendText($"Acertos: {acertosExame}\n");
            rtbMensagens.AppendText($"Erros: {errosExame}\n");
            rtbMensagens.AppendText($"Aproveitamento: {aproveitamento}%\n");
            rtbMensagens.AppendText($"Média Geral: {alunoAtual.GetMediaFinal():0.0}\n");
            rtbMensagens.AppendText($"Semestre Atual: {alunoAtual.SemestreAtual}\n\n");
            rtbMensagens.AppendText("Você foi aprovado!\n");

            DesbloquearHabilidade();
        }

        private void DerrotaExame()
        {
            rtbMensagens.Clear();

            rtbMensagens.AppendText(
                "=== RESULTADO DO EXAME ===\n\n");

            rtbMensagens.AppendText(
                $"Acertos: {acertosExame}\n");

            rtbMensagens.AppendText(
                $"Erros: {errosExame}\n");

            rtbMensagens.AppendText(
                "Você foi reprovado.\n");

            panelSalaExame.Visible = false;
            panelHall.Visible = true;
        }
        // ====================== SALA COORDENACAO ======================

        private void opc4_Click(object sender, EventArgs e)
        {
            if (alunoAtual == null) return;

            panelHall.Visible = false;
            panelCoordenacao.Visible = true;
            panelCoordenacao.BringToFront();

            Coordenador coordenador = new Coordenador();
            rtbCoordenadora.Clear();
            rtbCoordenadora.AppendText(coordenador.GetMensagemAbertura(alunoAtual));
        }

        private void btnPedirItemCoordenadora_Click(object sender, EventArgs e)
        {
            if (alunoAtual == null) return;

            Coordenador coordenador = new Coordenador();
            string mensagem = coordenador.MensagemItemIndisponivel(alunoAtual);

            rtbCoordenadora.Clear();
            rtbCoordenadora.AppendText(mensagem);
        }

        private void btnPedirDicaCoordenadora_Click(object sender, EventArgs e)
        {
            if (alunoAtual == null) return;

            Coordenador coordenador = new Coordenador();
            string dica = coordenador.ObterDica(alunoAtual);

            rtbCoordenadora.Clear();
            rtbCoordenadora.AppendText(dica);
        }

        private void btnTrancarSemestre_Click(object sender, EventArgs e)
        {
            if (alunoAtual == null || repositorio == null) return;

            Coordenador coordenador = new Coordenador(repositorio);
            bool trancado = coordenador.TrancarSemestre(alunoAtual, repositorio);
            string mensagem = coordenador.GetDialogo();

            rtbCoordenadora.Clear();
            rtbCoordenadora.AppendText(mensagem);

            if (!trancado)
                rtbCoordenadora.AppendText("\nTente novamente mais tarde.");
        }

        private void btnVoltarCoordenadora_Click(object sender, EventArgs e)
        {
            panelHall.Visible = true;
            panelCoordenacao.Visible = false;
        }

        // ====================== VER STATUS E INVENTARIO ======================

        private void opc5_Click(object sender, EventArgs e)
        {
            if (alunoAtual == null)
            {
                return;
            }
            rtbMensagens.Clear();
            rtbMensagens.AppendText("=== Status do Aluno ===\n\n");
            rtbMensagens.AppendText($"Vida: {alunoAtual.Vida}/{alunoAtual.VidaMaxima}\n");
            rtbMensagens.AppendText($"Conhecimento: {alunoAtual.Conhecimento}\n");
            rtbMensagens.AppendText($"Semestre: {alunoAtual.SemestreAtual}\n");

            var itens = alunoAtual.Inventario.ListarItens();
            if (itens.Count == 0)
            {
                rtbMensagens.AppendText($"Inventario: Nenhum item no inventario.");
                return;
            }
            else
            {
                rtbMensagens.AppendText($"Inventario: {alunoAtual.Inventario.Quantidade}/{Inventario.CapacidadeMaxima}\n");
            }
        }

        // ====================== NAO USO ======================

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void lblNome_Click(object sender, EventArgs e)
        {
        }

        private void rtbVeterano_TextChanged(object sender, EventArgs e)
        {
        }

        private void rtbProfessor_TextChanged(object sender, EventArgs e)
        {
        }

        private void txtNome_TextChanged(object sender, EventArgs e)
        {
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void panelNome_Paint(object sender, PaintEventArgs e)
        {
        }

        private void rtbMensagens_TextChanged(object sender, EventArgs e)
        {
        }

        private void btnAltA_Click(object sender, EventArgs e)
        {
            ResponderPergunta(0);
        }
        private void btnAltB_Click(object sender, EventArgs e)
        {
            ResponderPergunta(1);

        }
        private void btnAltC_Click(object sender, EventArgs e)
        {
            ResponderPergunta(2);

        }
        private void btnAltD_Click(object sender, EventArgs e)
        {
            ResponderPergunta(3);

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void lblVidaAluno_Click(object sender, EventArgs e)
        {

        }

        private void lblVidaChefe_Click(object sender, EventArgs e)
        {

        }

        private void opc7_Click(object sender, EventArgs e)
        {
            if (alunoAtual != null)
            {
                try
                {
                    EstadoJogo estado = EstadoJogoFactory.Criar(alunoAtual);
                    repositorio?.Salvar(estado);
                    rtbMensagens.AppendText("\nJogo salvo com sucesso.\n");
                }
                catch
                {
                    rtbMensagens.AppendText("\nFalha ao salvar o jogo.\n");
                }
            }

            panelHall.Visible = false;
            panelMenu.Visible = true;
        }

        private void MostrarTelaVitoria(int aproveitamentoTCC)
        {
            float media = alunoAtual!.GetMediaFinal();
            rtbVitoriaStats.Clear();
            rtbVitoriaStats.AppendText("=== HISTÓRICO ===\n\n");
            rtbVitoriaStats.AppendText($"Semestre 1 — IC:  {alunoAtual.Aproveitamentos[0]}%\n");
            rtbVitoriaStats.AppendText($"Semestre 2 — AED: {alunoAtual.Aproveitamentos[1]}%\n");
            rtbVitoriaStats.AppendText($"Semestre 3 — POO: {alunoAtual.Aproveitamentos[2]}%\n");
            rtbVitoriaStats.AppendText($"TCC:              {aproveitamentoTCC}%\n\n");
            rtbVitoriaStats.AppendText($"Média Final: {media:0.0}\n\n");
            rtbVitoriaStats.AppendText("=== ESTATÍSTICAS ===\n\n");
            rtbVitoriaStats.AppendText($"Conhecimento: {alunoAtual.Conhecimento}\n");
            rtbVitoriaStats.AppendText($"Quizzes concluídos: {alunoAtual.QuizzesConcluidos}\n");
            rtbVitoriaStats.AppendText($"Itens usados: {alunoAtual.ItensUsados}\n");
            panelVitoria.Visible = true;
            panelVitoria.BringToFront();
        }

        private void btnVoltarMenuVitoria_Click(object sender, EventArgs e)
        {
            panelVitoria.Visible = false;
            alunoAtual = null;
            panelMenu.Visible = true;
            panelMenu.BringToFront();
        }

        private void panelCoordenacao_Paint(object sender, PaintEventArgs e)
        {

        }

        private void opc6_Click(object sender, EventArgs e)
        {
            if (alunoAtual == null) return;

            rtbMensagens.Clear();
            rtbMensagens.AppendText("=== INVENTÁRIO ===\n\n");

            var itens = alunoAtual.Inventario.ListarItens();

            if (itens.Count == 0)
            {
                rtbMensagens.AppendText("Nenhum item.\n");
                return;
            }

            foreach (CampusQuest.Itens.Item item in itens)
            {
                rtbMensagens.AppendText(item.Nome + "\n");
            }
        }
    }
}
