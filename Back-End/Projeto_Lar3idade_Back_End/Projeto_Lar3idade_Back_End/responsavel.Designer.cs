﻿
namespace Projeto_Lar3idade_Back_End
{
    partial class responsavel
    {
        /// <summary> 
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Designer de Componentes

        /// <summary> 
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(responsavel));
            this.label1 = new System.Windows.Forms.Label();
            this.dateTimePicker_dataNascimento = new System.Windows.Forms.DateTimePicker();
            this.label11 = new System.Windows.Forms.Label();
            this.Mostrar = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button_Search = new System.Windows.Forms.Button();
            this.textBox_Search = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.button_delete = new System.Windows.Forms.Button();
            this.button_update = new System.Windows.Forms.Button();
            this.textBox_Cc = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox_Name = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button_insert = new System.Windows.Forms.Button();
            this.dateTimePicker_DtaValidade = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox_morada = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox_codPostal = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox_telemovel = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.textBox_telCasa = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.textBox_ocupacao = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBox2_parentesco = new System.Windows.Forms.TextBox();
            this.textBox_email = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.textBox3_parentesco = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.comboBox1_Utente = new System.Windows.Forms.ComboBox();
            this.comboBox2_Utente = new System.Windows.Forms.ComboBox();
            this.comboBox3_Utente = new System.Windows.Forms.ComboBox();
            this.textBox1_parentesco = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.textBox3_senha = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(432, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(129, 26);
            this.label1.TabIndex = 2;
            this.label1.Text = "Responsável";
            // 
            // dateTimePicker_dataNascimento
            // 
            this.dateTimePicker_dataNascimento.Location = new System.Drawing.Point(799, 39);
            this.dateTimePicker_dataNascimento.Name = "dateTimePicker_dataNascimento";
            this.dateTimePicker_dataNascimento.Size = new System.Drawing.Size(200, 22);
            this.dateTimePicker_dataNascimento.TabIndex = 115;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(662, 39);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(140, 22);
            this.label11.TabIndex = 114;
            this.label11.Text = "Data nascimento";
            // 
            // Mostrar
            // 
            this.Mostrar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(113)))), ((int)(((byte)(161)))), ((int)(((byte)(255)))));
            this.Mostrar.Location = new System.Drawing.Point(316, 334);
            this.Mostrar.Name = "Mostrar";
            this.Mostrar.Size = new System.Drawing.Size(77, 28);
            this.Mostrar.TabIndex = 112;
            this.Mostrar.Text = "Mostrar";
            this.Mostrar.UseVisualStyleBackColor = false;
            this.Mostrar.Click += new System.EventHandler(this.Mostrar_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(671, 340);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(27, 26);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 111;
            this.pictureBox1.TabStop = false;
            // 
            // button_Search
            // 
            this.button_Search.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(113)))), ((int)(((byte)(161)))), ((int)(((byte)(255)))));
            this.button_Search.Location = new System.Drawing.Point(922, 337);
            this.button_Search.Name = "button_Search";
            this.button_Search.Size = new System.Drawing.Size(77, 29);
            this.button_Search.TabIndex = 110;
            this.button_Search.Text = "Search";
            this.button_Search.UseVisualStyleBackColor = false;
            this.button_Search.Click += new System.EventHandler(this.button_Search_Click);
            // 
            // textBox_Search
            // 
            this.textBox_Search.Location = new System.Drawing.Point(704, 340);
            this.textBox_Search.Multiline = true;
            this.textBox_Search.Name = "textBox_Search";
            this.textBox_Search.Size = new System.Drawing.Size(221, 26);
            this.textBox_Search.TabIndex = 109;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(14, 368);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(985, 353);
            this.dataGridView1.TabIndex = 108;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // button_delete
            // 
            this.button_delete.BackColor = System.Drawing.Color.Red;
            this.button_delete.Location = new System.Drawing.Point(220, 332);
            this.button_delete.Name = "button_delete";
            this.button_delete.Size = new System.Drawing.Size(90, 30);
            this.button_delete.TabIndex = 107;
            this.button_delete.Text = "Delete";
            this.button_delete.UseVisualStyleBackColor = false;
            this.button_delete.Click += new System.EventHandler(this.button_delete_Click);
            // 
            // button_update
            // 
            this.button_update.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(113)))), ((int)(((byte)(161)))), ((int)(((byte)(255)))));
            this.button_update.Location = new System.Drawing.Point(128, 332);
            this.button_update.Name = "button_update";
            this.button_update.Size = new System.Drawing.Size(86, 30);
            this.button_update.TabIndex = 106;
            this.button_update.Text = "Update";
            this.button_update.UseVisualStyleBackColor = false;
            this.button_update.Click += new System.EventHandler(this.button_update_Click);
            // 
            // textBox_Cc
            // 
            this.textBox_Cc.Location = new System.Drawing.Point(177, 80);
            this.textBox_Cc.Name = "textBox_Cc";
            this.textBox_Cc.Size = new System.Drawing.Size(279, 22);
            this.textBox_Cc.TabIndex = 103;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(10, 79);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(161, 22);
            this.label3.TabIndex = 102;
            this.label3.Text = "Nº Cartão Cidadão";
            // 
            // textBox_Name
            // 
            this.textBox_Name.Location = new System.Drawing.Point(155, 40);
            this.textBox_Name.Name = "textBox_Name";
            this.textBox_Name.Size = new System.Drawing.Size(501, 22);
            this.textBox_Name.TabIndex = 101;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(10, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(139, 22);
            this.label2.TabIndex = 100;
            this.label2.Text = "Nome Completo";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // button_insert
            // 
            this.button_insert.BackColor = System.Drawing.Color.LimeGreen;
            this.button_insert.Location = new System.Drawing.Point(32, 332);
            this.button_insert.Name = "button_insert";
            this.button_insert.Size = new System.Drawing.Size(90, 30);
            this.button_insert.TabIndex = 99;
            this.button_insert.Text = "Insert";
            this.button_insert.UseVisualStyleBackColor = false;
            this.button_insert.Click += new System.EventHandler(this.button_insert_Click);
            // 
            // dateTimePicker_DtaValidade
            // 
            this.dateTimePicker_DtaValidade.Location = new System.Drawing.Point(602, 80);
            this.dateTimePicker_DtaValidade.Name = "dateTimePicker_DtaValidade";
            this.dateTimePicker_DtaValidade.Size = new System.Drawing.Size(200, 22);
            this.dateTimePicker_DtaValidade.TabIndex = 105;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(474, 80);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(122, 22);
            this.label4.TabIndex = 116;
            this.label4.Text = "Data Validade";
            // 
            // textBox_morada
            // 
            this.textBox_morada.Location = new System.Drawing.Point(92, 117);
            this.textBox_morada.Name = "textBox_morada";
            this.textBox_morada.Size = new System.Drawing.Size(301, 22);
            this.textBox_morada.TabIndex = 118;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(10, 116);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 22);
            this.label5.TabIndex = 117;
            this.label5.Text = "Morada";
            // 
            // textBox_codPostal
            // 
            this.textBox_codPostal.Location = new System.Drawing.Point(524, 116);
            this.textBox_codPostal.Name = "textBox_codPostal";
            this.textBox_codPostal.Size = new System.Drawing.Size(147, 22);
            this.textBox_codPostal.TabIndex = 120;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(396, 116);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(122, 22);
            this.label6.TabIndex = 119;
            this.label6.Text = "Código Postal";
            // 
            // textBox_telemovel
            // 
            this.textBox_telemovel.Location = new System.Drawing.Point(511, 156);
            this.textBox_telemovel.Name = "textBox_telemovel";
            this.textBox_telemovel.Size = new System.Drawing.Size(133, 22);
            this.textBox_telemovel.TabIndex = 124;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(403, 157);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(94, 22);
            this.label16.TabIndex = 123;
            this.label16.Text = "Telemovél";
            // 
            // textBox_telCasa
            // 
            this.textBox_telCasa.Location = new System.Drawing.Point(137, 157);
            this.textBox_telCasa.Name = "textBox_telCasa";
            this.textBox_telCasa.Size = new System.Drawing.Size(209, 22);
            this.textBox_telCasa.TabIndex = 122;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(12, 156);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(119, 22);
            this.label17.TabIndex = 121;
            this.label17.Text = "Telefone casa";
            // 
            // textBox_ocupacao
            // 
            this.textBox_ocupacao.Location = new System.Drawing.Point(777, 156);
            this.textBox_ocupacao.Name = "textBox_ocupacao";
            this.textBox_ocupacao.Size = new System.Drawing.Size(222, 22);
            this.textBox_ocupacao.TabIndex = 126;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(682, 157);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(89, 22);
            this.label7.TabIndex = 125;
            this.label7.Text = "Ocupação";
            // 
            // textBox2_parentesco
            // 
            this.textBox2_parentesco.Location = new System.Drawing.Point(814, 262);
            this.textBox2_parentesco.Name = "textBox2_parentesco";
            this.textBox2_parentesco.Size = new System.Drawing.Size(185, 22);
            this.textBox2_parentesco.TabIndex = 130;
            // 
            // textBox_email
            // 
            this.textBox_email.Location = new System.Drawing.Point(81, 194);
            this.textBox_email.Name = "textBox_email";
            this.textBox_email.Size = new System.Drawing.Size(554, 22);
            this.textBox_email.TabIndex = 128;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(15, 194);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(57, 22);
            this.label19.TabIndex = 127;
            this.label19.Text = "Email";
            // 
            // textBox3_parentesco
            // 
            this.textBox3_parentesco.Location = new System.Drawing.Point(814, 292);
            this.textBox3_parentesco.Name = "textBox3_parentesco";
            this.textBox3_parentesco.Size = new System.Drawing.Size(185, 22);
            this.textBox3_parentesco.TabIndex = 134;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(641, 232);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(167, 22);
            this.label8.TabIndex = 133;
            this.label8.Text = "Parentesco/Relação";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(15, 232);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(60, 22);
            this.label9.TabIndex = 131;
            this.label9.Text = "Utente";
            // 
            // comboBox1_Utente
            // 
            this.comboBox1_Utente.FormattingEnabled = true;
            this.comboBox1_Utente.Location = new System.Drawing.Point(81, 230);
            this.comboBox1_Utente.Name = "comboBox1_Utente";
            this.comboBox1_Utente.Size = new System.Drawing.Size(554, 24);
            this.comboBox1_Utente.TabIndex = 135;
            this.comboBox1_Utente.SelectedIndexChanged += new System.EventHandler(this.comboBox1_Utente_SelectedIndexChanged);
            // 
            // comboBox2_Utente
            // 
            this.comboBox2_Utente.FormattingEnabled = true;
            this.comboBox2_Utente.Location = new System.Drawing.Point(81, 260);
            this.comboBox2_Utente.Name = "comboBox2_Utente";
            this.comboBox2_Utente.Size = new System.Drawing.Size(554, 24);
            this.comboBox2_Utente.TabIndex = 136;
            this.comboBox2_Utente.SelectedIndexChanged += new System.EventHandler(this.comboBox2_Utente_SelectedIndexChanged);
            // 
            // comboBox3_Utente
            // 
            this.comboBox3_Utente.FormattingEnabled = true;
            this.comboBox3_Utente.Location = new System.Drawing.Point(81, 290);
            this.comboBox3_Utente.Name = "comboBox3_Utente";
            this.comboBox3_Utente.Size = new System.Drawing.Size(554, 24);
            this.comboBox3_Utente.TabIndex = 137;
            this.comboBox3_Utente.SelectedIndexChanged += new System.EventHandler(this.comboBox3_Utente_SelectedIndexChanged);
            // 
            // textBox1_parentesco
            // 
            this.textBox1_parentesco.Location = new System.Drawing.Point(814, 232);
            this.textBox1_parentesco.Name = "textBox1_parentesco";
            this.textBox1_parentesco.Size = new System.Drawing.Size(185, 22);
            this.textBox1_parentesco.TabIndex = 138;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(700, 194);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(57, 22);
            this.label13.TabIndex = 141;
            this.label13.Text = "Senha";
            // 
            // textBox3_senha
            // 
            this.textBox3_senha.Location = new System.Drawing.Point(777, 194);
            this.textBox3_senha.Name = "textBox3_senha";
            this.textBox3_senha.Size = new System.Drawing.Size(222, 22);
            this.textBox3_senha.TabIndex = 142;
            // 
            // responsavel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            this.Controls.Add(this.textBox3_senha);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.textBox1_parentesco);
            this.Controls.Add(this.comboBox3_Utente);
            this.Controls.Add(this.comboBox2_Utente);
            this.Controls.Add(this.comboBox1_Utente);
            this.Controls.Add(this.textBox3_parentesco);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.textBox2_parentesco);
            this.Controls.Add(this.textBox_email);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.textBox_ocupacao);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.textBox_telemovel);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.textBox_telCasa);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.textBox_codPostal);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textBox_morada);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dateTimePicker_dataNascimento);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.Mostrar);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.button_Search);
            this.Controls.Add(this.textBox_Search);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.button_delete);
            this.Controls.Add(this.button_update);
            this.Controls.Add(this.dateTimePicker_DtaValidade);
            this.Controls.Add(this.textBox_Cc);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox_Name);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button_insert);
            this.Controls.Add(this.label1);
            this.Name = "responsavel";
            this.Size = new System.Drawing.Size(1022, 735);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dateTimePicker_dataNascimento;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button Mostrar;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button_Search;
        private System.Windows.Forms.TextBox textBox_Search;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button_delete;
        private System.Windows.Forms.Button button_update;
        private System.Windows.Forms.TextBox textBox_Cc;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox_Name;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button_insert;
        private System.Windows.Forms.DateTimePicker dateTimePicker_DtaValidade;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox_morada;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox_codPostal;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBox_telemovel;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox textBox_telCasa;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox textBox_ocupacao;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBox2_parentesco;
        private System.Windows.Forms.TextBox textBox_email;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox textBox3_parentesco;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox comboBox1_Utente;
        private System.Windows.Forms.ComboBox comboBox2_Utente;
        private System.Windows.Forms.ComboBox comboBox3_Utente;
        private System.Windows.Forms.TextBox textBox1_parentesco;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox textBox3_senha;
    }
}
