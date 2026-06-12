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
            panelMenu.SuspendLayout();
            panelNome.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Impact", 24.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.White;
            label1.Location = new Point(444, 311);
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
            btnNovoJogo.Location = new Point(481, 372);
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
            lblNome.Location = new Point(486, 311);
            lblNome.Name = "lblNome";
            lblNome.Size = new Size(145, 25);
            lblNome.TabIndex = 2;
            lblNome.Text = "Digite seu nome:";
            lblNome.Click += lblNome_Click;
            // 
            // txtNome
            // 
            txtNome.ForeColor = Color.Black;
            txtNome.Location = new Point(486, 339);
            txtNome.Name = "txtNome";
            txtNome.Size = new Size(149, 27);
            txtNome.TabIndex = 4;
            txtNome.TextChanged += txtNome_TextChanged;
            // 
            // btnConfirmar
            // 
            btnConfirmar.Font = new Font("Cambria", 15F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnConfirmar.Location = new Point(504, 372);
            btnConfirmar.Name = "btnConfirmar";
            btnConfirmar.Size = new Size(116, 33);
            btnConfirmar.TabIndex = 5;
            btnConfirmar.Text = "INICIAR";
            btnConfirmar.UseVisualStyleBackColor = true;
            btnConfirmar.Click += btnConfirmar_Click;
            // 
            // panelMenu
            // 
            panelMenu.BackgroundImage = Properties.Resources.image;
            panelMenu.Controls.Add(btnSair);
            panelMenu.Controls.Add(btnCarregarJogo);
            panelMenu.Controls.Add(label1);
            panelMenu.Controls.Add(btnNovoJogo);
            panelMenu.Dock = DockStyle.Fill;
            panelMenu.Location = new Point(0, 0);
            panelMenu.Name = "panelMenu";
            panelMenu.Size = new Size(1125, 749);
            panelMenu.TabIndex = 6;
            panelMenu.Paint += panel1_Paint;
            // 
            // btnSair
            // 
            btnSair.BackColor = SystemColors.ControlDarkDark;
            btnSair.FlatStyle = FlatStyle.Flat;
            btnSair.Font = new Font("Impact", 15F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnSair.Location = new Point(481, 504);
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
            btnCarregarJogo.Location = new Point(481, 438);
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
            panelNome.Dock = DockStyle.Fill;
            panelNome.Location = new Point(0, 0);
            panelNome.Name = "panelNome";
            panelNome.Size = new Size(1125, 749);
            panelNome.TabIndex = 7;
            panelNome.Visible = false;
            panelNome.Paint += panelNome_Paint;
            // 
            // TelaInicial
            // 
            AutoScaleDimensions = new SizeF(8F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Gray;
            ClientSize = new Size(1125, 749);
            Controls.Add(panelMenu);
            Controls.Add(panelNome);
            Font = new Font("MV Boli", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            ForeColor = Color.White;
            Name = "TelaInicial";
            Text = "Form1";
            Load += TelaInicial_Load;
            panelMenu.ResumeLayout(false);
            panelMenu.PerformLayout();
            panelNome.ResumeLayout(false);
            panelNome.PerformLayout();
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
    }
}
