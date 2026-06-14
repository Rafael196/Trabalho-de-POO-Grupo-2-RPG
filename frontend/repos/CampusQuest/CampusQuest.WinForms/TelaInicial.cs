using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using CampusQuest.Core;
using CampusQuest.NPCs;
using CampusQuest.Quiz;

namespace CampusQuest.WinForms
{
    public partial class TelaInicial : Form
    {
        private Aluno? alunoAtual;
        private Veterano? veteranoAtual;
        private Professor? professorAtual;

        public TelaInicial()
        {
            InitializeComponent();
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

            Professor professor = new Professor();
            rtbProfessor.Clear();
            rtbProfessor.AppendText(professor.GetMensagemAbertura(alunoAtual));
        }

        private void btnPedirItemProfessor_Click(object sender, EventArgs e)
        {
            if (alunoAtual == null) return;

            Professor professor = new Professor();
            string mensagem = professor.MensagemItemIndisponivel(alunoAtual);

            rtbProfessor.Clear();
            rtbProfessor.AppendText(mensagem);
        }

        private void btnPedirDica2_Click(object sender, EventArgs e)
        {
            if (alunoAtual == null) return;

            Professor professor = new Professor();
            string dica = professor.ObterDica(alunoAtual);

            rtbProfessor.Clear();
            rtbProfessor.AppendText(dica);
        }

        private void btnFazerQuiz_Click(object sender, EventArgs e)
        {
            if (alunoAtual == null) return;

            professorAtual = new Professor();

            rtbProfessor.Clear();
            rtbProfessor.AppendText(professorAtual.GetMensagemAbertura(alunoAtual) + "\n\n");

            var aceitar = MessageBox.Show("Aceitar fazer o quiz de reforço?",
                "Quiz do Professor", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (aceitar == DialogResult.No)
            {
                rtbProfessor.AppendText("Quiz recusado. Volte quando quiser estudar.\n");
            }
        }
    }
}
