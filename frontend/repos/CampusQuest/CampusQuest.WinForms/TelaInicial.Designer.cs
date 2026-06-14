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
            panelProfessor = new Panel();
            rtbProfessor = new RichTextBox();
            btnPedirItemProfessor = new Button();
            btnPedirDica2 = new Button();
            btnFazerQuiz = new Button();
            btnVoltarProfessor = new Button();
            panelQuiz = new Panel();
            rtbQuiz = new RichTextBox();
            btnAltA = new Button();
            btnAltB = new Button();
            btnAltC = new Button();
            btnAltD = new Button();
            panelMenu.SuspendLayout();
            panelNome.SuspendLayout();
            panelHall.SuspendLayout();
            panelVeterano.SuspendLayout();
            panelProfessor.SuspendLayout();
            panelQuiz.SuspendLayout();
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
            panelMenu.BackgroundImage = Properties.Resources.image1;
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
            opc7.Text = "7) Voltar ao menu";
            opc7.UseVisualStyleBackColor = false;
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
            opc6.Text = "6) Usar item do inventario";
            opc6.UseVisualStyleBackColor = false;
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
            opc5.Text = "5) Ver status e inventario";
            opc5.UseVisualStyleBackColor = false;
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
            opc4.Text = "4) Ir para sala da coordenacao (trancar semestre)";
            opc4.UseVisualStyleBackColor = false;
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
            opc3.Text = "3) Ir para sala de exame (batalha)";
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
            opc2.Text = "2) Ir para sala do professor (quiz)";
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
            opc1.Text = "1) Falar com veterano (dicas/itens)";
            opc1.UseVisualStyleBackColor = false;
            opc1.Click += opc1_Click;
            // 
            // panelVeterano
            // 
            panelVeterano.BackColor = Color.PeachPuff;
            panelVeterano.Controls.Add(rtbVeterano);
            panelVeterano.Controls.Add(btnPedirItem);
            panelVeterano.Controls.Add(btnPedirDica);
            panelVeterano.Controls.Add(btnVoltarVeterano);
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
            btnPedirItem.Text = "1) Pedir item para a prova";
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
            btnPedirDica.Text = "2) Pedir dica de estudo";
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
            btnVoltarVeterano.Text = "3) Encerrar conversa";
            btnVoltarVeterano.UseVisualStyleBackColor = false;
            btnVoltarVeterano.Click += btnVoltarVeterano_Click;
            // 
            // panelProfessor
            // 
            panelProfessor.BackColor = Color.Tan;
            panelProfessor.Controls.Add(rtbProfessor);
            panelProfessor.Controls.Add(btnPedirItemProfessor);
            panelProfessor.Controls.Add(btnPedirDica2);
            panelProfessor.Controls.Add(btnFazerQuiz);
            panelProfessor.Controls.Add(btnVoltarProfessor);
            panelProfessor.ForeColor = Color.Transparent;
            panelProfessor.Location = new Point(0, 0);
            panelProfessor.Name = "panelProfessor";
            panelProfessor.Size = new Size(934, 651);
            panelProfessor.TabIndex = 10;
            panelProfessor.Visible = false;
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
            // btnPedirItemProfessor
            // 
            btnPedirItemProfessor.BackColor = Color.Transparent;
            btnPedirItemProfessor.Font = new Font("Impact", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnPedirItemProfessor.ForeColor = Color.Black;
            btnPedirItemProfessor.Location = new Point(25, 586);
            btnPedirItemProfessor.Name = "btnPedirItemProfessor";
            btnPedirItemProfessor.Size = new Size(200, 50);
            btnPedirItemProfessor.TabIndex = 0;
            btnPedirItemProfessor.Text = "1) Pedir item para a prova";
            btnPedirItemProfessor.UseVisualStyleBackColor = false;
            btnPedirItemProfessor.Click += btnPedirItemProfessor_Click;
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
            btnPedirDica2.Text = "2) Pedir dica de estudo";
            btnPedirDica2.UseVisualStyleBackColor = false;
            btnPedirDica2.Click += btnPedirDica2_Click;
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
            btnFazerQuiz.Text = "3) Fazer quiz";
            btnFazerQuiz.UseVisualStyleBackColor = false;
            btnFazerQuiz.Click += btnFazerQuiz_Click;
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
            btnVoltarProfessor.Text = "4) Encerrar conversa";
            btnVoltarProfessor.UseVisualStyleBackColor = false;
            btnVoltarProfessor.Click += btnVoltarProfessor_Click;
            // 
            // panelQuiz
            // 
            panelQuiz.BackColor = Color.Gold;
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
            // TelaInicial
            // 
            AutoScaleDimensions = new SizeF(8F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Gray;
            ClientSize = new Size(934, 651);
            Controls.Add(panelHall);
            Controls.Add(panelVeterano);
            Controls.Add(panelMenu);
            Controls.Add(panelNome);
            Controls.Add(panelProfessor);
            Controls.Add(panelQuiz);
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
            panelProfessor.ResumeLayout(false);
            panelQuiz.ResumeLayout(false);
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
        private Panel panelProfessor;
        private Button button4;
        private Button button3;
        private Button btnPedirDica2;
        private RichTextBox rtbProfessor;
        private Button btnPedirItemProfessor;
        private Button btnVoltarProfessor;
        private Button btnFazerQuiz;
        private Panel panelQuiz;
        private RichTextBox rtbQuiz;
        private Button button1;
        private Button button2;
        private Button button7;
        private Button btnAltD;
        private Button btnAltA;
        private Button btnAltB;
        private Button btnAltC;
    }
}
