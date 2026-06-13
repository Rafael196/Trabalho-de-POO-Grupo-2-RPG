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
            panelMenu.SuspendLayout();
            panelNome.SuspendLayout();
            panelHall.SuspendLayout();
            panelVeterano.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Impact", 24.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.White;
            label1.Location = new Point(383, 265);
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
            btnNovoJogo.Location = new Point(421, 320);
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
            panelMenu.BackgroundImage = (Image)resources.GetObject("panelMenu.BackgroundImage");
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
            btnSair.Location = new Point(421, 432);
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
            btnCarregarJogo.Location = new Point(421, 376);
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
            panelHall.BackColor = Color.Peru;
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
            panelHall.Size = new Size(934, 651);
            panelHall.TabIndex = 7;
            panelHall.Visible = false;
            // 
            // rtbMensagens
            // 
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
            panelVeterano.BackColor = Color.Peru;
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
            // 
            // TelaInicial
            // 
            AutoScaleDimensions = new SizeF(8F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Gray;
            ClientSize = new Size(934, 651);
            Controls.Add(panelVeterano);
            Controls.Add(panelMenu);
            Controls.Add(panelNome);
            Controls.Add(panelHall);
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
        private Button button4;
        private Button button5;
        private Button button6;
        private Button opcV1;
        private Button btnVoltarVeterano;
        private Button btnPedirDica;
    }
}
