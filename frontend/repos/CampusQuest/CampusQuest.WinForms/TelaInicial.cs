using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using CampusQuest.Core;
using CampusQuest.NPCs;
using CampusQuest.Quiz;
using CampusQuest.Itens;
using CampusQuest.Persistencia;

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
        private readonly IRepositorioQuestoes repositorioQuestoes;

        public TelaInicial()
        {
            InitializeComponent();
            repositorioQuestoes = new SqliteRepositorioQuestoes();
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
            MessageBox.Show("Funcionalidade de carregar jogo ainda não implementada.");
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
            int aproveitamento =
                (int)((double)acertosQuiz /
                perguntasQuiz.Length * 100);

            int ganhoConhecimento = acertosQuiz * 10;

            alunoAtual?.AumentarConhecimento(ganhoConhecimento);

            string recompensa = "Nenhuma";

            CampusQuest.Core.Item? itemRecebido = null;

            if (aproveitamento >= 80)
            {
                itemRecebido = new LivroTecnico("POO");
            }
            else if (aproveitamento >= 50)
            {
                itemRecebido = new Caderno();
            }
            else
            {
                itemRecebido = new Cafe();
            }

            if (alunoAtual != null &&
                itemRecebido != null &&
                alunoAtual.PodeReceberItem(itemRecebido))
            {
                if (alunoAtual.Inventario.Adicionar(itemRecebido))
                {
                    alunoAtual.RegistrarItemRecebido(itemRecebido);
                    recompensa = itemRecebido.GetType().Name;
                }
            }
            panelQuiz.Visible = false;
            panelProfessor.Visible = true;

            rtbProfessor.Clear();

            rtbProfessor.AppendText("=== QUIZ CONCLUÍDO ===\n");
            rtbProfessor.AppendText($"Acertos: {acertosQuiz}/{perguntasQuiz.Length}\n");
            rtbProfessor.AppendText($"Aproveitamento: {aproveitamento}%\n");
            rtbProfessor.AppendText($"Conhecimento ganho: +{ganhoConhecimento}\n");
            rtbProfessor.AppendText($"Item recebido: {recompensa}\n");

            rtbProfessor.AppendText("O professor analisa seu desempenho.\n");
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
                    new[] {"Parte física","Programa","Rede","Arquivo"},
                    0,
                    "Hardware é a parte física."),

                new Pergunta("O que é software?",
                    new[] {"Programa","Placa","Teclado","Memória"},
                    0,
                    "Software é o conjunto de programas."),

                new Pergunta("Qual sistema gerencia recursos do computador?",
                    new[] {"Sistema operacional","Editor","Navegador","Jogos"},
                    0,
                    "O sistema operacional gerencia recursos."),

                new Pergunta("Qual número representa binário?",
                    new[] {"0 e 1","2 e 3","5 e 6","8 e 9"},
                    0,
                    "Binário usa 0 e 1."),

                new Pergunta("Qual dispositivo conecta redes?",
                    new[] {"Roteador","Mouse","Monitor","Teclado"},
                    0,
                    "Roteador conecta redes.")
                    };
                    break;

                case 2:
                    perguntasQuiz = new Pergunta[]
                    {
                new Pergunta("Pilha segue qual regra?",
                    new[] {"LIFO","FIFO","ABC","XYZ"},
                    0,
                    "Pilha é LIFO."),

                new Pergunta("Fila segue qual regra?",
                    new[] {"FIFO","LIFO","DFS","BFS"},
                    0,
                    "Fila é FIFO."),

                new Pergunta("Busca binária exige dados:",
                    new[] {"Ordenados","Aleatórios","Vazios","Repetidos"},
                    0,
                    "Busca binária exige ordenação."),

                new Pergunta("Complexidade O(n) indica crescimento:",
                    new[] {"Linear","Constante","Quadrático","Logarítmico"},
                    0,
                    "O(n) é linear."),

                new Pergunta("Qual estrutura combina recursão?",
                    new[] {"Pilha","Fila","Lista","Matriz"},
                    0,
                    "Recursão usa pilha.")
                    };
                    break;

                default:
                    perguntasQuiz = new Pergunta[]
                    {
                new Pergunta("Encapsulamento controla:",
                    new[] {"Acesso aos dados","Rede","Compilação","Memória"},
                    0,
                    "Encapsulamento controla acesso."),

                new Pergunta("Herança permite:",
                    new[] {"Reuso","Apagar dados","Acelerar CPU","Criar rede"},
                    0,
                    "Herança promove reuso."),

                new Pergunta("Polimorfismo significa:",
                    new[] {"Mesmo método com comportamentos diferentes",
                           "Só um construtor",
                           "Sem classes",
                           "Sem objetos"},
                    0,
                    "Polimorfismo altera comportamento."),

                new Pergunta("Abstração é:",
                    new[] {"Focar no essencial",
                           "Duplicar código",
                           "Evitar classes",
                           "Criar bugs"},
                    0,
                    "Abstração foca no essencial."),

                new Pergunta("Objeto é:",
                    new[] {"Instância de classe",
                           "Método",
                           "Interface",
                           "Namespace"},
                    0,
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

        private void opc3_Click(object sender, EventArgs e)
        {
        }

        private void rtbVeterano_TextChanged(object sender, EventArgs e)
        {
        }
        private void rtbProfessor_TextChanged(object sender, EventArgs e)
        {
        }

        private void lblNome_Click(object sender, EventArgs e)
        {

        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
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
    }
}
