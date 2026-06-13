using CampusQuest.Core;
using CampusQuest.NPCs;
namespace CampusQuest.WinForms
{
    public partial class TelaInicial : Form
    {
        private Aluno? alunoAtual;
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
            MessageBox.Show("Botão Novo Jogo clicado!");
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
            rtbMensagens.AppendText($"Bem-vindo, {alunoAtual.Nome}!\n\n");
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

        private void rtbMensagens_TextChanged(object sender, EventArgs e)
        {

        }
        private void label1_Click(object sender, EventArgs e)
        {
        }
        private void lblNome_Click(object sender, EventArgs e)
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

        private void opc1_Click(object sender, EventArgs e)
        {
            if (alunoAtual == null)
                return;

            panelHall.Visible = false;
            panelVeterano.Visible = true;

            Veterano veterano = new Veterano();

            rtbVeterano.Clear();
            rtbVeterano.AppendText(veterano.GetMensagemAbertura(alunoAtual));
        }

        private void opc2_Click(object sender, EventArgs e)
        {
        }

        private void opc3_Click(object sender, EventArgs e)
        {
        }

        private void opc4_Click(object sender, EventArgs e)
        {
        }

        private void opc5_Click(object sender, EventArgs e)
        {
        }

        private void opc6_Click(object sender, EventArgs e)
        {
        }

        private void opc7_Click(object sender, EventArgs e)
        {
        }

        private void rtbVeterano_TextChanged(object sender, EventArgs e)
        {

        }
        private void btnPedirItem_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnPedirDica_Click(object sender, EventArgs e)
        {
            if (alunoAtual == null)
                return;

            Veterano veterano = new Veterano();
            rtbVeterano.Clear();
            rtbVeterano.AppendText(veterano.ObterDica(alunoAtual));
        }
    }
}