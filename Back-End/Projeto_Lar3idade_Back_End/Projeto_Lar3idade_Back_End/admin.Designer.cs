
namespace Projeto_Lar3idade_Back_End
{
    partial class admin
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(admin));
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.pagamentos1 = new Projeto_Lar3idade_Back_End.pagamentos();
            this.quartos1 = new Projeto_Lar3idade_Back_End.quartos();
            this.responsavel1 = new Projeto_Lar3idade_Back_End.responsavel();
            this.tarefas1 = new Projeto_Lar3idade_Back_End.tarefas();
            this.utentes1 = new Projeto_Lar3idade_Back_End.utentes();
            this.funcionario1 = new Projeto_Lar3idade_Back_End.funcionario();
            this.escalas1 = new Projeto_Lar3idade_Back_End.Escalas();
            this.atividades1 = new Projeto_Lar3idade_Back_End.atividades();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(113)))), ((int)(((byte)(161)))), ((int)(((byte)(255)))));
            this.panel1.Controls.Add(this.button1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1318, 58);
            this.panel1.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1177, 17);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(101, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Sair";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(44, 17);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(100, 92);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(40, 175);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(141, 27);
            this.label1.TabIndex = 1;
            this.label1.Text = "Gerir Utentes";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(40, 250);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(192, 27);
            this.label2.TabIndex = 2;
            this.label2.Text = "Gerir Funcionarios";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(40, 325);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(198, 27);
            this.label3.TabIndex = 3;
            this.label3.Text = "Gerir Responsávies";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(40, 400);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(185, 27);
            this.label4.TabIndex = 4;
            this.label4.Text = "Atividades do Lar";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(40, 475);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(82, 27);
            this.label5.TabIndex = 5;
            this.label5.Text = "Tarefas";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(40, 550);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(191, 27);
            this.label6.TabIndex = 6;
            this.label6.Text = "Escalas de Serviço";
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(40, 625);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(128, 27);
            this.label7.TabIndex = 7;
            this.label7.Text = "Pagamentos";
            this.label7.Click += new System.EventHandler(this.label7_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(40, 700);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(88, 27);
            this.label8.TabIndex = 8;
            this.label8.Text = "Quartos";
            this.label8.Click += new System.EventHandler(this.label8_Click);
            // 
            // pagamentos1
            // 
            this.pagamentos1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            this.pagamentos1.Location = new System.Drawing.Point(296, 64);
            this.pagamentos1.Name = "pagamentos1";
            this.pagamentos1.Size = new System.Drawing.Size(1022, 735);
            this.pagamentos1.TabIndex = 13;
            // 
            // quartos1
            // 
            this.quartos1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            this.quartos1.Location = new System.Drawing.Point(296, 66);
            this.quartos1.Name = "quartos1";
            this.quartos1.Size = new System.Drawing.Size(1022, 735);
            this.quartos1.TabIndex = 12;
            // 
            // responsavel1
            // 
            this.responsavel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            this.responsavel1.Location = new System.Drawing.Point(296, 66);
            this.responsavel1.Name = "responsavel1";
            this.responsavel1.Size = new System.Drawing.Size(1022, 735);
            this.responsavel1.TabIndex = 11;
            // 
            // tarefas1
            // 
            this.tarefas1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            this.tarefas1.Location = new System.Drawing.Point(296, 66);
            this.tarefas1.Name = "tarefas1";
            this.tarefas1.Size = new System.Drawing.Size(1022, 735);
            this.tarefas1.TabIndex = 10;
            // 
            // utentes1
            // 
            this.utentes1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            this.utentes1.Location = new System.Drawing.Point(296, 64);
            this.utentes1.Name = "utentes1";
            this.utentes1.Size = new System.Drawing.Size(1022, 735);
            this.utentes1.TabIndex = 9;
            // 
            // funcionario1
            // 
            this.funcionario1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            this.funcionario1.Location = new System.Drawing.Point(296, 66);
            this.funcionario1.Name = "funcionario1";
            this.funcionario1.Size = new System.Drawing.Size(1022, 735);
            this.funcionario1.TabIndex = 14;
            // 
            // escalas1
            // 
            this.escalas1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            this.escalas1.Location = new System.Drawing.Point(296, 66);
            this.escalas1.Name = "escalas1";
            this.escalas1.Size = new System.Drawing.Size(1022, 735);
            this.escalas1.TabIndex = 15;
            // 
            // atividades1
            // 
            this.atividades1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            this.atividades1.Location = new System.Drawing.Point(296, 66);
            this.atividades1.Name = "atividades1";
            this.atividades1.Size = new System.Drawing.Size(1022, 735);
            this.atividades1.TabIndex = 16;
            // 
            // admin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(1318, 813);
            this.Controls.Add(this.atividades1);
            this.Controls.Add(this.escalas1);
            this.Controls.Add(this.funcionario1);
            this.Controls.Add(this.pagamentos1);
            this.Controls.Add(this.quartos1);
            this.Controls.Add(this.responsavel1);
            this.Controls.Add(this.tarefas1);
            this.Controls.Add(this.utentes1);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.panel1);
            this.Name = "admin";
            this.Text = "admin";
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private utentes utentes1;
        private tarefas tarefas1;
        private responsavel responsavel1;
        private quartos quartos1;
        private pagamentos pagamentos1;
        private funcionario funcionario1;
        private Escalas escalas1;
        private atividades atividades1;
    }
}