namespace CampusQuest.WinForms
{
    partial class TelaInicial
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TelaInicial));
            label1 = new Label();
            btnNovoJogo = new Button();
            lblNome = new Label();
            txtNome = new TextBox();
            btnConfirmar = new Button();
            panelMenu = new Panel();
            btnSair = new Button();
            btnCarregarJogo = new Button();
            panelNome = new Panel();
            panelHall = new Panel();
            rtbMensagens = new RichTextBox();
            opc7 = new Button();
            opc6 = new Button();
            opc5 = new Button();
            opc4 = new Button();
            opc3 = new Button();
            opc2 = new Button();
            opc1 = new Button();
            panelVeterano = new Panel();
            rtbVeterano = new RichTextBox();
            btnPedirItem = new Button();
            btnPedirDica = new Button();
            btnVoltarVeterano = new Button();
            pictureBox2 = new PictureBox();
            panelQuiz = new Panel();
            rtbQuiz = new RichTextBox();
            btnAltA = new Button();
            btnAltB = new Button();
            btnAltC = new Button();
            btnAltD = new Button();
            btnVoltarProfessor = new Button();
            btnFazerQuiz = new Button();
            btnPedirDica2 = new Button();
            btnPedirItemProfessor = new Button();
            rtbProfessor = new RichTextBox();
            panelProfessor = new Panel();
            pictureBox1 = new PictureBox();
            panelSalaExame = new Panel();
            lblVidaChefe = new Label();
            lblVidaAluno = new Label();
            lblMateria = new Label();
            rtbExame = new RichTextBox();
            btnExamA = new Button();
            btnExamB = new Button();
            btnExamC = new Button();
            btnExamD = new Button();
            panelCoordenacao = new Panel();
            rtbCoordenadora = new RichTextBox();
            btnPedirItemCoordenadora = new Button();
            btnPedirDicaCoordenadora = new Button();
            btnTrancarSemestre = new Button();
            btnVoltarCoordenadora = new Button();
            Coordenadora = new PictureBox();
            panelVitoria = new Panel();
            lblVitoriaTitle = new Label();
            lblVitoriaSubtitle = new Label();
            rtbVitoriaStats = new RichTextBox();
            btnVoltarMenuVitoria = new Button();
            panelMenu.SuspendLayout();
            panelNome.SuspendLayout();
            panelHall.SuspendLayout();
            panelVeterano.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            panelQuiz.SuspendLayout();
            panelProfessor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panelSalaExame.SuspendLayout();
            panelCoordenacao.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)Coordenadora).BeginInit();
            panelVitoria.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Impact", 24.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.White;
            label1.Location = new Point(355, 265);
            label1.Name = "label1";
            label1.Size = new Size(216, 41);
            label1.TabIndex = 0;
            label1.Text = "CAMPUS QUEST";
            label1.Click += label1_Click;
            // 
            // btnNovoJogo
            // 
            btnNovoJogo.BackColor = SystemColors.ControlDarkDark;
            btnNovoJogo.FlatStyle = FlatStyle.Flat;
            btnNovoJogo.Font = new Font("Impact", 15F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnNovoJogo.Location = new Point(394, 320);
            btnNovoJogo.Name = "btnNovoJogo";
            btnNovoJogo.Size = new Size(150, 50);
            btnNovoJogo.TabIndex = 1;
            btnNovoJogo.Text = "Novo Jogo";
            btnNovoJogo.UseVisualStyleBackColor = false;
            btnNovoJogo.Click += btnNovoJogo_Click;
            // 
            // lblNome
            // 
            lblNome.AutoSize = true;
            lblNome.Font = new Font("Impact", 15F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblNome.Location = new Point(399, 306);
            lblNome.Name = "lblNome";
            lblNome.Size = new Size(145, 25);
            lblNome.TabIndex = 2;
            lblNome.Text = "Digite seu nome:";
            lblNome.Click += lblNome_Click;
            // 
            // txtNome
            // 
            txtNome.Font = new Font("Impact", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtNome.ForeColor = Color.Black;
            txtNome.Location = new Point(368, 334);
            txtNome.Name = "txtNome";
            txtNome.Size = new Size(203, 27);
            txtNome.TabIndex = 4;
            txtNome.TextAlign = HorizontalAlignment.Center;
            txtNome.TextChanged += txtNome_TextChanged;
            // 
            // btnConfirmar
            // 
            btnConfirmar.BackColor = Color.DimGray;
            btnConfirmar.Font = new Font("Impact", 15F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnConfirmar.Location = new Point(411, 367);
            btnConfirmar.Name = "btnConfirmar";
            btnConfirmar.Size = new Size(116, 33);
            btnConfirmar.TabIndex = 5;
            btnConfirmar.Text = "INICIAR";
            btnConfirmar.UseVisualStyleBackColor = false;
            btnConfirmar.Click += btnConfirmar_Click;
            // 
            // panelMenu
            // 
            panelMenu.BackColor = Color.Transparent;
            panelMenu.BackgroundImage = Properties.Resources.image;
            panelMenu.Controls.Add(btnSair);
            panelMenu.Controls.Add(btnCarregarJogo);
            panelMenu.Controls.Add(label1);
            panelMenu.Controls.Add(btnNovoJogo);
            panelMenu.Location = new Point(0, 0);
            panelMenu.Name = "panelMenu";
            panelMenu.Size = new Size(934, 651);
            panelMenu.TabIndex = 6;
            panelMenu.Paint += panel1_Paint;
            // 
            // btnSair
            // 
            btnSair.BackColor = SystemColors.ControlDarkDark;
            btnSair.FlatStyle = FlatStyle.Flat;
            btnSair.Font = new Font("Impact", 15F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnSair.Location = new Point(394, 432);
            btnSair.Name = "btnSair";
            btnSair.Size = new Size(150, 50);
            btnSair.TabIndex = 3;
            btnSair.Text = "Sair";
            btnSair.UseVisualStyleBackColor = false;
            btnSair.Click += btnSair_Click;
            // 
            // btnCarregarJogo
            // 
            btnCarregarJogo.BackColor = SystemColors.ControlDarkDark;
            btnCarregarJogo.FlatStyle = FlatStyle.Flat;
            btnCarregarJogo.Font = new Font("Impact", 15F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnCarregarJogo.Location = new Point(394, 376);
            btnCarregarJogo.Name = "btnCarregarJogo";
            btnCarregarJogo.Size = new Size(150, 50);
            btnCarregarJogo.TabIndex = 2;
            btnCarregarJogo.Text = "Carregar Jogo";
            btnCarregarJogo.UseVisualStyleBackColor = false;
            btnCarregarJogo.Click += btnCarregarJogo_Click;
            // 
            // panelNome
            // 
            panelNome.BackColor = Color.Black;
            panelNome.Controls.Add(lblNome);
            panelNome.Controls.Add(txtNome);
            panelNome.Controls.Add(btnConfirmar);
            panelNome.Location = new Point(0, 0);
            panelNome.Name = "panelNome";
            panelNome.Size = new Size(934, 651);
            panelNome.TabIndex = 7;
            panelNome.Visible = false;
            panelNome.Paint += panelNome_Paint;
            // 
            // panelHall
            // 
            panelHall.BackColor = Color.Transparent;
            panelHall.BackgroundImage = Properties.Resources.hallInicio;
            panelHall.Controls.Add(rtbMensagens);
            panelHall.Controls.Add(opc7);
            panelHall.Controls.Add(opc6);
            panelHall.Controls.Add(opc5);
            panelHall.Controls.Add(opc4);
            panelHall.Controls.Add(opc3);
            panelHall.Controls.Add(opc2);
            panelHall.Controls.Add(opc1);
            panelHall.ForeColor = Color.Transparent;
            panelHall.Location = new Point(0, 0);
            panelHall.Name = "panelHall";
            panelHall.Size = new Size(950, 651);
            panelHall.TabIndex = 7;
            panelHall.Visible = false;
            // 
            // rtbMensagens
            // 
            rtbMensagens.Font = new Font("Calibri", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            rtbMensagens.Location = new Point(25, 429);
            rtbMensagens.Name = "rtbMensagens";
            rtbMensagens.Size = new Size(879, 79);
            rtbMensagens.TabIndex = 7;
            rtbMensagens.Text = "";
            rtbMensagens.TextChanged += rtbMensagens_TextChanged;
            // 
            // opc7
            // 
            opc7.BackColor = Color.Transparent;
            opc7.Font = new Font("Impact", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            opc7.ForeColor = Color.Black;
            opc7.Location = new Point(204, 589);
            opc7.Name = "opc7";
            opc7.Size = new Size(160, 50);
            opc7.TabIndex = 6;
            opc7.Text = "Voltar ao menu";
            opc7.UseVisualStyleBackColor = false;
            opc7.Click += opc7_Click;
            // 
            // opc6
            // 
            opc6.BackColor = Color.Transparent;
            opc6.Font = new Font("Impact", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            opc6.ForeColor = Color.Black;
            opc6.Location = new Point(25, 589);
            opc6.Name = "opc6";
            opc6.Size = new Size(160, 50);
            opc6.TabIndex = 5;
            opc6.Text = "Usar item do inventario";
            opc6.UseVisualStyleBackColor = false;
            opc6.Click += opc6_Click;
            // 
            // opc5
            // 
            opc5.BackColor = Color.Transparent;
            opc5.Font = new Font("Impact", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            opc5.ForeColor = Color.Black;
            opc5.Location = new Point(744, 530);
            opc5.Name = "opc5";
            opc5.Size = new Size(160, 50);
            opc5.TabIndex = 4;
            opc5.Text = "Ver status e inventario";
            opc5.UseVisualStyleBackColor = false;
            opc5.Click += opc5_Click;
            // 
            // opc4
            // 
            opc4.BackColor = Color.Transparent;
            opc4.Font = new Font("Impact", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            opc4.ForeColor = Color.Black;
            opc4.Location = new Point(563, 530);
            opc4.Name = "opc4";
            opc4.Size = new Size(160, 50);
            opc4.TabIndex = 3;
            opc4.Text = "Ir para sala da coordenacao";
            opc4.UseVisualStyleBackColor = false;
            opc4.Click += opc4_Click;
            // 
            // opc3
            // 
            opc3.BackColor = Color.Transparent;
            opc3.Font = new Font("Impact", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            opc3.ForeColor = Color.Black;
            opc3.Location = new Point(384, 530);
            opc3.Name = "opc3";
            opc3.Size = new Size(160, 50);
            opc3.TabIndex = 2;
            opc3.Text = "Ir para sala de exame\r\n";
            opc3.UseVisualStyleBackColor = false;
            opc3.Click += opc3_Click;
            // 
            // opc2
            // 
            opc2.BackColor = Color.Transparent;
            opc2.Font = new Font("Impact", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            opc2.ForeColor = Color.Black;
            opc2.Location = new Point(204, 530);
            opc2.Name = "opc2";
            opc2.Size = new Size(160, 50);
            opc2.TabIndex = 1;
            opc2.Text = "Ir para sala do professor";
            opc2.UseVisualStyleBackColor = false;
            opc2.Click += opc2_Click;
            // 
            // opc1
            // 
            opc1.BackColor = Color.Transparent;
            opc1.Font = new Font("Impact", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            opc1.ForeColor = Color.Black;
            opc1.Location = new Point(25, 530);
            opc1.Name = "opc1";
            opc1.Size = new Size(160, 50);
            opc1.TabIndex = 0;
            opc1.Text = "Falar com veterano";
            opc1.UseVisualStyleBackColor = false;
            opc1.Click += opc1_Click;
            // 
            // panelVeterano
            // 
            panelVeterano.BackColor = Color.PeachPuff;
            panelVeterano.BackgroundImage = Properties.Resources.hallInicio;
            panelVeterano.Controls.Add(rtbVeterano);
            panelVeterano.Controls.Add(btnPedirItem);
            panelVeterano.Controls.Add(btnPedirDica);
            panelVeterano.Controls.Add(btnVoltarVeterano);
            panelVeterano.Controls.Add(pictureBox2);
            panelVeterano.ForeColor = Color.Transparent;
            panelVeterano.Location = new Point(0, 0);
            panelVeterano.Name = "panelVeterano";
            panelVeterano.Size = new Size(934, 651);
            panelVeterano.TabIndex = 8;
            panelVeterano.Visible = false;
            // 
            // rtbVeterano
            // 
            rtbVeterano.Font = new Font("Calibri", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            rtbVeterano.Location = new Point(25, 501);
            rtbVeterano.Name = "rtbVeterano";
            rtbVeterano.Size = new Size(879, 79);
            rtbVeterano.TabIndex = 7;
            rtbVeterano.Text = "";
            rtbVeterano.TextChanged += rtbVeterano_TextChanged;
            // 
            // btnPedirItem
            // 
            btnPedirItem.BackColor = Color.Transparent;
            btnPedirItem.Font = new Font("Impact", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnPedirItem.ForeColor = Color.Black;
            btnPedirItem.Location = new Point(25, 586);
            btnPedirItem.Name = "btnPedirItem";
            btnPedirItem.Size = new Size(247, 50);
            btnPedirItem.TabIndex = 0;
            btnPedirItem.Text = "Pedir item para a prova";
            btnPedirItem.UseVisualStyleBackColor = false;
            btnPedirItem.Click += btnPedirItem_Click;
            // 
            // btnPedirDica
            // 
            btnPedirDica.BackColor = Color.Transparent;
            btnPedirDica.Font = new Font("Impact", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnPedirDica.ForeColor = Color.Black;
            btnPedirDica.Location = new Point(341, 586);
            btnPedirDica.Name = "btnPedirDica";
            btnPedirDica.Size = new Size(247, 50);
            btnPedirDica.TabIndex = 8;
            btnPedirDica.Text = "Pedir dica de estudo";
            btnPedirDica.UseVisualStyleBackColor = false;
            btnPedirDica.Click += btnPedirDica_Click;
            // 
            // btnVoltarVeterano
            // 
            btnVoltarVeterano.BackColor = Color.Transparent;
            btnVoltarVeterano.Font = new Font("Impact", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnVoltarVeterano.ForeColor = Color.Black;
            btnVoltarVeterano.Location = new Point(657, 586);
            btnVoltarVeterano.Name = "btnVoltarVeterano";
            btnVoltarVeterano.Size = new Size(247, 50);
            btnVoltarVeterano.TabIndex = 9;
            btnVoltarVeterano.Text = "Encerrar conversa";
            btnVoltarVeterano.UseVisualStyleBackColor = false;
            btnVoltarVeterano.Click += btnVoltarVeterano_Click;
            // 
            // pictureBox2
            // 
            pictureBox2.BackColor = Color.Transparent;
            pictureBox2.Image = Properties.Resources.bonecodefinitivo;
            pictureBox2.Location = new Point(34, 265);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(282, 263);
            pictureBox2.TabIndex = 10;
            pictureBox2.TabStop = false;
            // 
            // panelQuiz
            // 
            panelQuiz.BackColor = Color.Gold;
            panelQuiz.BackgroundImage = Properties.Resources.sala_quiz;
            panelQuiz.Controls.Add(rtbQuiz);
            panelQuiz.Controls.Add(btnAltA);
            panelQuiz.Controls.Add(btnAltB);
            panelQuiz.Controls.Add(btnAltC);
            panelQuiz.Controls.Add(btnAltD);
            panelQuiz.ForeColor = Color.Transparent;
            panelQuiz.Location = new Point(0, 0);
            panelQuiz.Name = "panelQuiz";
            panelQuiz.Size = new Size(934, 651);
            panelQuiz.TabIndex = 11;
            panelQuiz.Visible = false;
            // 
            // rtbQuiz
            // 
            rtbQuiz.Font = new Font("Calibri", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            rtbQuiz.Location = new Point(25, 501);
            rtbQuiz.Name = "rtbQuiz";
            rtbQuiz.Size = new Size(879, 79);
            rtbQuiz.TabIndex = 7;
            rtbQuiz.Text = "";
            // 
            // btnAltA
            // 
            btnAltA.BackColor = Color.Transparent;
            btnAltA.Font = new Font("Impact", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnAltA.ForeColor = Color.Black;
            btnAltA.Location = new Point(25, 586);
            btnAltA.Name = "btnAltA";
            btnAltA.Size = new Size(200, 50);
            btnAltA.TabIndex = 0;
            btnAltA.Text = "1";
            btnAltA.UseVisualStyleBackColor = false;
            btnAltA.Click += btnAltA_Click;
            // 
            // btnAltB
            // 
            btnAltB.BackColor = Color.Transparent;
            btnAltB.Font = new Font("Impact", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnAltB.ForeColor = Color.Black;
            btnAltB.Location = new Point(249, 586);
            btnAltB.Name = "btnAltB";
            btnAltB.Size = new Size(200, 50);
            btnAltB.TabIndex = 8;
            btnAltB.Text = "2";
            btnAltB.UseVisualStyleBackColor = false;
            btnAltB.Click += btnAltB_Click;
            // 
            // btnAltC
            // 
            btnAltC.BackColor = Color.Transparent;
            btnAltC.Font = new Font("Impact", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnAltC.ForeColor = Color.Black;
            btnAltC.Location = new Point(476, 586);
            btnAltC.Name = "btnAltC";
            btnAltC.Size = new Size(200, 50);
            btnAltC.TabIndex = 9;
            btnAltC.Text = "3";
            btnAltC.UseVisualStyleBackColor = false;
            btnAltC.Click += btnAltC_Click;
            // 
            // btnAltD
            // 
            btnAltD.BackColor = Color.Transparent;
            btnAltD.Font = new Font("Impact", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnAltD.ForeColor = Color.Black;
            btnAltD.Location = new Point(704, 586);
            btnAltD.Name = "btnAltD";
            btnAltD.Size = new Size(200, 50);
            btnAltD.TabIndex = 10;
            btnAltD.Text = "4";
            btnAltD.UseVisualStyleBackColor = false;
            btnAltD.Click += btnAltD_Click;
            // 
            // btnVoltarProfessor
            // 
            btnVoltarProfessor.BackColor = Color.Transparent;
            btnVoltarProfessor.Font = new Font("Impact", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnVoltarProfessor.ForeColor = Color.Black;
            btnVoltarProfessor.Location = new Point(704, 586);
            btnVoltarProfessor.Name = "btnVoltarProfessor";
            btnVoltarProfessor.Size = new Size(200, 50);
            btnVoltarProfessor.TabIndex = 10;
            btnVoltarProfessor.Text = "Encerrar conversa";
            btnVoltarProfessor.UseVisualStyleBackColor = false;
            btnVoltarProfessor.Click += btnVoltarProfessor_Click;
            // 
            // btnFazerQuiz
            // 
            btnFazerQuiz.BackColor = Color.Transparent;
            btnFazerQuiz.Font = new Font("Impact", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnFazerQuiz.ForeColor = Color.Black;
            btnFazerQuiz.Location = new Point(476, 586);
            btnFazerQuiz.Name = "btnFazerQuiz";
            btnFazerQuiz.Size = new Size(200, 50);
            btnFazerQuiz.TabIndex = 9;
            btnFazerQuiz.Text = "Fazer quiz";
            btnFazerQuiz.UseVisualStyleBackColor = false;
            btnFazerQuiz.Click += btnFazerQuiz_Click;
            // 
            // btnPedirDica2
            // 
            btnPedirDica2.BackColor = Color.Transparent;
            btnPedirDica2.Font = new Font("Impact", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnPedirDica2.ForeColor = Color.Black;
            btnPedirDica2.Location = new Point(249, 586);
            btnPedirDica2.Name = "btnPedirDica2";
            btnPedirDica2.Size = new Size(200, 50);
            btnPedirDica2.TabIndex = 8;
            btnPedirDica2.Text = "Pedir dica de estudo";
            btnPedirDica2.UseVisualStyleBackColor = false;
            btnPedirDica2.Click += btnPedirDica2_Click;
            // 
            // btnPedirItemProfessor
            // 
            btnPedirItemProfessor.BackColor = Color.Transparent;
            btnPedirItemProfessor.Font = new Font("Impact", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnPedirItemProfessor.ForeColor = Color.Black;
            btnPedirItemProfessor.Location = new Point(25, 586);
            btnPedirItemProfessor.Name = "btnPedirItemProfessor";
            btnPedirItemProfessor.Size = new Size(200, 50);
            btnPedirItemProfessor.TabIndex = 0;
            btnPedirItemProfessor.Text = "Pedir item para a prova";
            btnPedirItemProfessor.UseVisualStyleBackColor = false;
            btnPedirItemProfessor.Click += btnPedirItemProfessor_Click;
            // 
            // rtbProfessor
            // 
            rtbProfessor.Font = new Font("Calibri", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            rtbProfessor.Location = new Point(25, 501);
            rtbProfessor.Name = "rtbProfessor";
            rtbProfessor.Size = new Size(879, 79);
            rtbProfessor.TabIndex = 7;
            rtbProfessor.Text = "";
            rtbProfessor.TextChanged += rtbProfessor_TextChanged;
            // 
            // panelProfessor
            // 
            panelProfessor.BackColor = Color.Tan;
            panelProfessor.BackgroundImage = Properties.Resources.salaProfessor;
            panelProfessor.Controls.Add(rtbProfessor);
            panelProfessor.Controls.Add(btnPedirItemProfessor);
            panelProfessor.Controls.Add(btnPedirDica2);
            panelProfessor.Controls.Add(btnFazerQuiz);
            panelProfessor.Controls.Add(btnVoltarProfessor);
            panelProfessor.Controls.Add(pictureBox1);
            panelProfessor.ForeColor = Color.Transparent;
            panelProfessor.Location = new Point(0, 0);
            panelProfessor.Name = "panelProfessor";
            panelProfessor.Size = new Size(934, 651);
            panelProfessor.TabIndex = 10;
            panelProfessor.Visible = false;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.Transparent;
            pictureBox1.Image = Properties.Resources.professor__1___2_;
            pictureBox1.Location = new Point(-109, 173);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(372, 377);
            pictureBox1.TabIndex = 11;
            pictureBox1.TabStop = false;
            // 
            // panelSalaExame
            // 
            panelSalaExame.BackColor = Color.Transparent;
            panelSalaExame.BackgroundImage = (Image)resources.GetObject("panelSalaExame.BackgroundImage");
            panelSalaExame.Controls.Add(lblVidaChefe);
            panelSalaExame.Controls.Add(lblVidaAluno);
            panelSalaExame.Controls.Add(lblMateria);
            panelSalaExame.Controls.Add(rtbExame);
            panelSalaExame.Controls.Add(btnExamA);
            panelSalaExame.Controls.Add(btnExamB);
            panelSalaExame.Controls.Add(btnExamC);
            panelSalaExame.Controls.Add(btnExamD);
            panelSalaExame.ForeColor = Color.Transparent;
            panelSalaExame.Location = new Point(0, 0);
            panelSalaExame.Name = "panelSalaExame";
            panelSalaExame.Size = new Size(934, 651);
            panelSalaExame.TabIndex = 11;
            panelSalaExame.Visible = false;
            // 
            // lblVidaChefe
            // 
            lblVidaChefe.AutoSize = true;
            lblVidaChefe.Font = new Font("Impact", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblVidaChefe.Location = new Point(25, 66);
            lblVidaChefe.Name = "lblVidaChefe";
            lblVidaChefe.Size = new Size(177, 34);
            lblVidaChefe.TabIndex = 14;
            lblVidaChefe.Text = "Chefe: 100/100";
            lblVidaChefe.Click += lblVidaChefe_Click;
            // 
            // lblVidaAluno
            // 
            lblVidaAluno.AutoSize = true;
            lblVidaAluno.Font = new Font("Impact", 20.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblVidaAluno.Location = new Point(715, 66);
            lblVidaAluno.Name = "lblVidaAluno";
            lblVidaAluno.Size = new Size(189, 34);
            lblVidaAluno.TabIndex = 13;
            lblVidaAluno.Text = "Aluno: 100/100";
            lblVidaAluno.Click += lblVidaAluno_Click;
            // 
            // lblMateria
            // 
            lblMateria.AutoSize = true;
            lblMateria.Font = new Font("Impact", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblMateria.Location = new Point(445, 9);
            lblMateria.Name = "lblMateria";
            lblMateria.Size = new Size(38, 34);
            lblMateria.TabIndex = 12;
            lblMateria.Text = "IC";
            // 
            // rtbExame
            // 
            rtbExame.Font = new Font("Calibri", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            rtbExame.Location = new Point(25, 501);
            rtbExame.Name = "rtbExame";
            rtbExame.Size = new Size(879, 79);
            rtbExame.TabIndex = 7;
            rtbExame.Text = "";
            // 
            // btnExamA
            // 
            btnExamA.BackColor = Color.Transparent;
            btnExamA.Font = new Font("Impact", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnExamA.ForeColor = Color.Black;
            btnExamA.Location = new Point(25, 586);
            btnExamA.Name = "btnExamA";
            btnExamA.Size = new Size(200, 50);
            btnExamA.TabIndex = 0;
            btnExamA.Text = "1";
            btnExamA.UseVisualStyleBackColor = false;
            btnExamA.Click += btnExamA_Click;
            // 
            // btnExamB
            // 
            btnExamB.BackColor = Color.Transparent;
            btnExamB.Font = new Font("Impact", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnExamB.ForeColor = Color.Black;
            btnExamB.Location = new Point(249, 586);
            btnExamB.Name = "btnExamB";
            btnExamB.Size = new Size(200, 50);
            btnExamB.TabIndex = 8;
            btnExamB.Text = "2";
            btnExamB.UseVisualStyleBackColor = false;
            btnExamB.Click += btnExamB_Click;
            // 
            // btnExamC
            // 
            btnExamC.BackColor = Color.Transparent;
            btnExamC.Font = new Font("Impact", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnExamC.ForeColor = Color.Black;
            btnExamC.Location = new Point(476, 586);
            btnExamC.Name = "btnExamC";
            btnExamC.Size = new Size(200, 50);
            btnExamC.TabIndex = 9;
            btnExamC.Text = "3";
            btnExamC.UseVisualStyleBackColor = false;
            btnExamC.Click += btnExamC_Click;
            // 
            // btnExamD
            // 
            btnExamD.BackColor = Color.Transparent;
            btnExamD.Font = new Font("Impact", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnExamD.ForeColor = Color.Black;
            btnExamD.Location = new Point(704, 586);
            btnExamD.Name = "btnExamD";
            btnExamD.Size = new Size(200, 50);
            btnExamD.TabIndex = 10;
            btnExamD.Text = "4";
            btnExamD.UseVisualStyleBackColor = false;
            btnExamD.Click += btnExamD_Click;
            // 
            // panelCoordenacao
            // 
            panelCoordenacao.BackColor = Color.Tan;
            panelCoordenacao.BackgroundImage = (Image)resources.GetObject("panelCoordenacao.BackgroundImage");
            panelCoordenacao.Controls.Add(rtbCoordenadora);
            panelCoordenacao.Controls.Add(btnPedirItemCoordenadora);
            panelCoordenacao.Controls.Add(btnPedirDicaCoordenadora);
            panelCoordenacao.Controls.Add(btnTrancarSemestre);
            panelCoordenacao.Controls.Add(btnVoltarCoordenadora);
            panelCoordenacao.Controls.Add(Coordenadora);
            panelCoordenacao.ForeColor = Color.Transparent;
            panelCoordenacao.Location = new Point(0, 0);
            panelCoordenacao.Name = "panelCoordenacao";
            panelCoordenacao.Size = new Size(934, 651);
            panelCoordenacao.TabIndex = 12;
            panelCoordenacao.Visible = false;
            panelCoordenacao.Paint += panelCoordenacao_Paint;
            // 
            // rtbCoordenadora
            // 
            rtbCoordenadora.Font = new Font("Calibri", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            rtbCoordenadora.Location = new Point(25, 501);
            rtbCoordenadora.Name = "rtbCoordenadora";
            rtbCoordenadora.Size = new Size(879, 79);
            rtbCoordenadora.TabIndex = 7;
            rtbCoordenadora.Text = "";
            // 
            // btnPedirItemCoordenadora
            // 
            btnPedirItemCoordenadora.BackColor = Color.Transparent;
            btnPedirItemCoordenadora.Font = new Font("Impact", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnPedirItemCoordenadora.ForeColor = Color.Black;
            btnPedirItemCoordenadora.Location = new Point(25, 586);
            btnPedirItemCoordenadora.Name = "btnPedirItemCoordenadora";
            btnPedirItemCoordenadora.Size = new Size(200, 50);
            btnPedirItemCoordenadora.TabIndex = 0;
            btnPedirItemCoordenadora.Text = "Pedir item para a prova";
            btnPedirItemCoordenadora.UseVisualStyleBackColor = false;
            btnPedirItemCoordenadora.Click += btnPedirItemCoordenadora_Click;
            // 
            // btnPedirDicaCoordenadora
            // 
            btnPedirDicaCoordenadora.BackColor = Color.Transparent;
            btnPedirDicaCoordenadora.Font = new Font("Impact", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnPedirDicaCoordenadora.ForeColor = Color.Black;
            btnPedirDicaCoordenadora.Location = new Point(249, 586);
            btnPedirDicaCoordenadora.Name = "btnPedirDicaCoordenadora";
            btnPedirDicaCoordenadora.Size = new Size(200, 50);
            btnPedirDicaCoordenadora.TabIndex = 8;
            btnPedirDicaCoordenadora.Text = "Pedir dica de estudo";
            btnPedirDicaCoordenadora.UseVisualStyleBackColor = false;
            btnPedirDicaCoordenadora.Click += btnPedirDicaCoordenadora_Click;
            // 
            // btnTrancarSemestre
            // 
            btnTrancarSemestre.BackColor = Color.Transparent;
            btnTrancarSemestre.Font = new Font("Impact", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnTrancarSemestre.ForeColor = Color.Black;
            btnTrancarSemestre.Location = new Point(476, 586);
            btnTrancarSemestre.Name = "btnTrancarSemestre";
            btnTrancarSemestre.Size = new Size(200, 50);
            btnTrancarSemestre.TabIndex = 9;
            btnTrancarSemestre.Text = "Trancar semestre";
            btnTrancarSemestre.UseVisualStyleBackColor = false;
            btnTrancarSemestre.Click += btnTrancarSemestre_Click;
            // 
            // btnVoltarCoordenadora
            // 
            btnVoltarCoordenadora.BackColor = Color.Transparent;
            btnVoltarCoordenadora.Font = new Font("Impact", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnVoltarCoordenadora.ForeColor = Color.Black;
            btnVoltarCoordenadora.Location = new Point(704, 586);
            btnVoltarCoordenadora.Name = "btnVoltarCoordenadora";
            btnVoltarCoordenadora.Size = new Size(200, 50);
            btnVoltarCoordenadora.TabIndex = 10;
            btnVoltarCoordenadora.Text = "Encerrar conversa";
            btnVoltarCoordenadora.UseVisualStyleBackColor = false;
            btnVoltarCoordenadora.Click += btnVoltarCoordenadora_Click;
            // 
            // Coordenadora
            // 
            Coordenadora.BackColor = Color.Transparent;
            Coordenadora.Image = (Image)resources.GetObject("Coordenadora.Image");
            Coordenadora.Location = new Point(-123, 204);
            Coordenadora.Name = "Coordenadora";
            Coordenadora.Size = new Size(386, 379);
            Coordenadora.TabIndex = 11;
            Coordenadora.TabStop = false;
            //
            // panelVitoria
            //
            panelVitoria.BackColor = Color.FromArgb(15, 15, 50);
            panelVitoria.Controls.Add(lblVitoriaTitle);
            panelVitoria.Controls.Add(lblVitoriaSubtitle);
            panelVitoria.Controls.Add(rtbVitoriaStats);
            panelVitoria.Controls.Add(btnVoltarMenuVitoria);
            panelVitoria.Location = new Point(0, 0);
            panelVitoria.Name = "panelVitoria";
            panelVitoria.Size = new Size(934, 651);
            panelVitoria.TabIndex = 13;
            panelVitoria.Visible = false;
            //
            // lblVitoriaTitle
            //
            lblVitoriaTitle.Font = new Font("Impact", 48F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblVitoriaTitle.ForeColor = Color.Gold;
            lblVitoriaTitle.Location = new Point(0, 60);
            lblVitoriaTitle.Name = "lblVitoriaTitle";
            lblVitoriaTitle.Size = new Size(934, 80);
            lblVitoriaTitle.TabIndex = 0;
            lblVitoriaTitle.Text = "PARABÉNS!";
            lblVitoriaTitle.TextAlign = ContentAlignment.MiddleCenter;
            //
            // lblVitoriaSubtitle
            //
            lblVitoriaSubtitle.Font = new Font("Impact", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblVitoriaSubtitle.ForeColor = Color.White;
            lblVitoriaSubtitle.Location = new Point(0, 155);
            lblVitoriaSubtitle.Name = "lblVitoriaSubtitle";
            lblVitoriaSubtitle.Size = new Size(934, 40);
            lblVitoriaSubtitle.TabIndex = 1;
            lblVitoriaSubtitle.Text = "Você concluiu o Campus Quest!";
            lblVitoriaSubtitle.TextAlign = ContentAlignment.MiddleCenter;
            //
            // rtbVitoriaStats
            //
            rtbVitoriaStats.BackColor = Color.FromArgb(15, 15, 50);
            rtbVitoriaStats.Font = new Font("Calibri", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
            rtbVitoriaStats.ForeColor = Color.White;
            rtbVitoriaStats.Location = new Point(217, 210);
            rtbVitoriaStats.Name = "rtbVitoriaStats";
            rtbVitoriaStats.ReadOnly = true;
            rtbVitoriaStats.Size = new Size(500, 295);
            rtbVitoriaStats.TabIndex = 2;
            rtbVitoriaStats.Text = "";
            //
            // btnVoltarMenuVitoria
            //
            btnVoltarMenuVitoria.BackColor = Color.Gold;
            btnVoltarMenuVitoria.Font = new Font("Impact", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnVoltarMenuVitoria.Location = new Point(367, 520);
            btnVoltarMenuVitoria.Name = "btnVoltarMenuVitoria";
            btnVoltarMenuVitoria.Size = new Size(200, 50);
            btnVoltarMenuVitoria.TabIndex = 3;
            btnVoltarMenuVitoria.Text = "Voltar ao Menu";
            btnVoltarMenuVitoria.UseVisualStyleBackColor = false;
            btnVoltarMenuVitoria.Click += btnVoltarMenuVitoria_Click;
            //
            // TelaInicial
            //
            AutoScaleDimensions = new SizeF(8F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Gray;
            ClientSize = new Size(934, 651);
            Controls.Add(panelHall);
            Controls.Add(panelSalaExame);
            Controls.Add(panelCoordenacao);
            Controls.Add(panelMenu);
            Controls.Add(panelProfessor);
            Controls.Add(panelVeterano);
            Controls.Add(panelQuiz);
            Controls.Add(panelNome);
            Controls.Add(panelVitoria);
            Font = new Font("MV Boli", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            ForeColor = Color.White;
            Name = "TelaInicial";
            Text = "Campus Quest";
            Load += TelaInicial_Load;
            panelMenu.ResumeLayout(false);
            panelMenu.PerformLayout();
            panelNome.ResumeLayout(false);
            panelNome.PerformLayout();
            panelHall.ResumeLayout(false);
            panelVeterano.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            panelQuiz.ResumeLayout(false);
            panelProfessor.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            panelSalaExame.ResumeLayout(false);
            panelSalaExame.PerformLayout();
            panelCoordenacao.ResumeLayout(false);
            panelVitoria.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)Coordenadora).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Label label1;
        private Button btnNovoJogo;
        private Label lblNome;
        private TextBox txtNome;
        private Button btnConfirmar;
        private Panel panelMenu;
        private Panel panelNome;
        private Button btnCarregarJogo;
        private Button btnSair;
        private Panel panelHall;
        private Button opc1;
        private Button opc2;
        private Button opc3;
        private Button opc4;
        private Button opc7;
        private Button opc6;
        private Button opc5;
        private RichTextBox rtbMensagens;
        private Panel panelVeterano;
        private RichTextBox rtbVeterano;
        private Button btnPedirItem;
        private Button button5;
        private Button button6;
        private Button opcV1;
        private Button btnVoltarVeterano;
        private Button btnPedirDica;
        private Button btnExamD;
        private Button btnExamC;
        private Panel panelQuiz;
        private RichTextBox rtbQuiz;
        private Button button1;
        private Button btnExamB;
        private Button button7;
        private Button btnAltD;
        private Button btnAltA;
        private Button btnAltB;
        private Button btnAltC;
        private Button btnVoltarProfessor;
        private Button btnFazerQuiz;
        private Button btnPedirDica2;
        private Button btnPedirItemProfessor;
        private RichTextBox rtbProfessor;
        private Panel panelProfessor;
        private Panel panelSalaExame;
        private RichTextBox rtbExame;
        private Button btnExamA;
        private PictureBox pictureBox1;
        private Label lblMateria;
        private Label lblVidaChefe;
        private Label lblVidaAluno;
        private PictureBox pictureBox2;
        private Panel panelCoordenacao;
        private RichTextBox rtbCoordenadora;
        private RichTextBox richTextBox1;
        private Button button2;
        private Button button3;
        private Button button4;
        private PictureBox Coordenadora;
        private Button btnPedirItemCoordenadora;
        private Button btnPedirDicaCoordenadora;
        private Button btnTrancarSemestre;
        private Button btnVoltarCoordenadora;
        private Panel panelVitoria;
        private Label lblVitoriaTitle;
        private Label lblVitoriaSubtitle;
        private RichTextBox rtbVitoriaStats;
        private Button btnVoltarMenuVitoria;
    }
}
