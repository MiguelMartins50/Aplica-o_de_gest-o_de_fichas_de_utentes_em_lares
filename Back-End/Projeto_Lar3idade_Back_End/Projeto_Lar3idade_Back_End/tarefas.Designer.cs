﻿
namespace Projeto_Lar3idade_Back_End
{
    partial class tarefas
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.richTextBox_descricao = new System.Windows.Forms.RichTextBox();
            this.Mostrar = new System.Windows.Forms.Button();
            this.button_Search = new System.Windows.Forms.Button();
            this.textBox_Search = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.button_delete = new System.Windows.Forms.Button();
            this.button_update = new System.Windows.Forms.Button();
            this.button_insert = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBox_funcionario = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox_nome = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBox_tipo = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(451, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 27);
            this.label1.TabIndex = 38;
            this.label1.Text = "Tarefas";
            // 
            // richTextBox_descricao
            // 
            this.richTextBox_descricao.Location = new System.Drawing.Point(21, 153);
            this.richTextBox_descricao.Name = "richTextBox_descricao";
            this.richTextBox_descricao.Size = new System.Drawing.Size(984, 183);
            this.richTextBox_descricao.TabIndex = 127;
            this.richTextBox_descricao.Text = "";
            // 
            // Mostrar
            // 
            this.Mostrar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            this.Mostrar.Location = new System.Drawing.Point(271, 342);
            this.Mostrar.Name = "Mostrar";
            this.Mostrar.Size = new System.Drawing.Size(142, 28);
            this.Mostrar.TabIndex = 126;
            this.Mostrar.Text = "Atualizar/Limpar";
            this.Mostrar.UseVisualStyleBackColor = false;
            this.Mostrar.Click += new System.EventHandler(this.Mostrar_Click);
            // 
            // button_Search
            // 
            this.button_Search.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(113)))), ((int)(((byte)(161)))), ((int)(((byte)(255)))));
            this.button_Search.Location = new System.Drawing.Point(923, 347);
            this.button_Search.Name = "button_Search";
            this.button_Search.Size = new System.Drawing.Size(82, 29);
            this.button_Search.TabIndex = 125;
            this.button_Search.Text = "Search";
            this.button_Search.UseVisualStyleBackColor = false;
            this.button_Search.Click += new System.EventHandler(this.button_Search_Click);
            // 
            // textBox_Search
            // 
            this.textBox_Search.Location = new System.Drawing.Point(703, 350);
            this.textBox_Search.Multiline = true;
            this.textBox_Search.Name = "textBox_Search";
            this.textBox_Search.Size = new System.Drawing.Size(226, 26);
            this.textBox_Search.TabIndex = 124;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.Location = new System.Drawing.Point(21, 378);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(984, 353);
            this.dataGridView1.TabIndex = 123;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // button_delete
            // 
            this.button_delete.BackColor = System.Drawing.Color.Red;
            this.button_delete.Location = new System.Drawing.Point(189, 342);
            this.button_delete.Name = "button_delete";
            this.button_delete.Size = new System.Drawing.Size(76, 30);
            this.button_delete.TabIndex = 122;
            this.button_delete.Text = "Apagar";
            this.button_delete.UseVisualStyleBackColor = false;
            this.button_delete.Click += new System.EventHandler(this.button_delete_Click);
            // 
            // button_update
            // 
            this.button_update.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(113)))), ((int)(((byte)(161)))), ((int)(((byte)(255)))));
            this.button_update.Location = new System.Drawing.Point(114, 342);
            this.button_update.Name = "button_update";
            this.button_update.Size = new System.Drawing.Size(69, 30);
            this.button_update.TabIndex = 121;
            this.button_update.Text = "Alterar";
            this.button_update.UseVisualStyleBackColor = false;
            this.button_update.Click += new System.EventHandler(this.button_update_Click);
            // 
            // button_insert
            // 
            this.button_insert.BackColor = System.Drawing.Color.Lime;
            this.button_insert.Location = new System.Drawing.Point(21, 342);
            this.button_insert.Name = "button_insert";
            this.button_insert.Size = new System.Drawing.Size(87, 30);
            this.button_insert.TabIndex = 120;
            this.button_insert.Text = "Adicionar";
            this.button_insert.UseVisualStyleBackColor = false;
            this.button_insert.Click += new System.EventHandler(this.button_insert_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(17, 128);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(91, 22);
            this.label5.TabIndex = 119;
            this.label5.Text = "Descrição";
            // 
            // comboBox_funcionario
            // 
            this.comboBox_funcionario.FormattingEnabled = true;
            this.comboBox_funcionario.Location = new System.Drawing.Point(205, 84);
            this.comboBox_funcionario.Name = "comboBox_funcionario";
            this.comboBox_funcionario.Size = new System.Drawing.Size(492, 24);
            this.comboBox_funcionario.TabIndex = 118;
            this.comboBox_funcionario.SelectedIndexChanged += new System.EventHandler(this.comboBox_funcionario_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(17, 86);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(182, 22);
            this.label4.TabIndex = 117;
            this.label4.Text = "Nome do funcionário ";
            // 
            // textBox_nome
            // 
            this.textBox_nome.Location = new System.Drawing.Point(141, 45);
            this.textBox_nome.Name = "textBox_nome";
            this.textBox_nome.Size = new System.Drawing.Size(395, 22);
            this.textBox_nome.TabIndex = 115;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(17, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(113, 22);
            this.label2.TabIndex = 114;
            this.label2.Text = "Nome Tarefa";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(650, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 22);
            this.label3.TabIndex = 128;
            this.label3.Text = "Tipo";
            // 
            // comboBox_tipo
            // 
            this.comboBox_tipo.FormattingEnabled = true;
            this.comboBox_tipo.Location = new System.Drawing.Point(703, 42);
            this.comboBox_tipo.Name = "comboBox_tipo";
            this.comboBox_tipo.Size = new System.Drawing.Size(285, 24);
            this.comboBox_tipo.TabIndex = 116;
            this.comboBox_tipo.SelectedIndexChanged += new System.EventHandler(this.comboBox_tipo_SelectedIndexChanged);
            // 
            // tarefas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            this.Controls.Add(this.label3);
            this.Controls.Add(this.richTextBox_descricao);
            this.Controls.Add(this.Mostrar);
            this.Controls.Add(this.button_Search);
            this.Controls.Add(this.textBox_Search);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.button_delete);
            this.Controls.Add(this.button_update);
            this.Controls.Add(this.button_insert);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.comboBox_funcionario);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.comboBox_tipo);
            this.Controls.Add(this.textBox_nome);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "tarefas";
            this.Size = new System.Drawing.Size(1022, 735);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox richTextBox_descricao;
        private System.Windows.Forms.Button Mostrar;
        private System.Windows.Forms.Button button_Search;
        private System.Windows.Forms.TextBox textBox_Search;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button_delete;
        private System.Windows.Forms.Button button_update;
        private System.Windows.Forms.Button button_insert;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboBox_funcionario;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox_nome;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBox_tipo;
    }
}
