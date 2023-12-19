
namespace Projeto_Lar3idade_Back_End
{
    partial class Atividades_func
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
            this.richTextBox_descricao = new System.Windows.Forms.RichTextBox();
            this.Mostrar = new System.Windows.Forms.Button();
            this.textBox_Search = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.button_delete = new System.Windows.Forms.Button();
            this.button_update = new System.Windows.Forms.Button();
            this.button_insert = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBox_utente = new System.Windows.Forms.ComboBox();
            this.dateTimePicker_realizacao = new System.Windows.Forms.DateTimePicker();
            this.label11 = new System.Windows.Forms.Label();
            this.comboBox_tipo = new System.Windows.Forms.ComboBox();
            this.textBox_nome = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // richTextBox_descricao
            // 
            this.richTextBox_descricao.Location = new System.Drawing.Point(20, 184);
            this.richTextBox_descricao.Name = "richTextBox_descricao";
            this.richTextBox_descricao.Size = new System.Drawing.Size(984, 137);
            this.richTextBox_descricao.TabIndex = 128;
            this.richTextBox_descricao.Text = "";
            // 
            // Mostrar
            // 
            this.Mostrar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            this.Mostrar.Location = new System.Drawing.Point(270, 338);
            this.Mostrar.Name = "Mostrar";
            this.Mostrar.Size = new System.Drawing.Size(142, 28);
            this.Mostrar.TabIndex = 127;
            this.Mostrar.Text = "Atualizar/Limpar";
            this.Mostrar.UseVisualStyleBackColor = false;
            this.Mostrar.Click += new System.EventHandler(this.Mostrar_Click);
            // 
            // textBox_Search
            // 
            this.textBox_Search.Location = new System.Drawing.Point(704, 340);
            this.textBox_Search.Multiline = true;
            this.textBox_Search.Name = "textBox_Search";
            this.textBox_Search.Size = new System.Drawing.Size(300, 26);
            this.textBox_Search.TabIndex = 125;
            this.textBox_Search.Text = "Procurar...";
            this.textBox_Search.TextChanged += new System.EventHandler(this.textBox_Search_TextChanged);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(20, 372);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(984, 353);
            this.dataGridView1.TabIndex = 124;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView1.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // button_delete
            // 
            this.button_delete.BackColor = System.Drawing.Color.Red;
            this.button_delete.Location = new System.Drawing.Point(188, 336);
            this.button_delete.Name = "button_delete";
            this.button_delete.Size = new System.Drawing.Size(76, 30);
            this.button_delete.TabIndex = 123;
            this.button_delete.Text = "Delete";
            this.button_delete.UseVisualStyleBackColor = false;
            this.button_delete.Click += new System.EventHandler(this.button_delete_Click);
            // 
            // button_update
            // 
            this.button_update.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(113)))), ((int)(((byte)(161)))), ((int)(((byte)(255)))));
            this.button_update.Location = new System.Drawing.Point(113, 336);
            this.button_update.Name = "button_update";
            this.button_update.Size = new System.Drawing.Size(69, 30);
            this.button_update.TabIndex = 122;
            this.button_update.Text = "Update";
            this.button_update.UseVisualStyleBackColor = false;
            this.button_update.Click += new System.EventHandler(this.button_update_Click);
            // 
            // button_insert
            // 
            this.button_insert.BackColor = System.Drawing.Color.LimeGreen;
            this.button_insert.Location = new System.Drawing.Point(20, 336);
            this.button_insert.Name = "button_insert";
            this.button_insert.Size = new System.Drawing.Size(87, 30);
            this.button_insert.TabIndex = 121;
            this.button_insert.Text = "Insert";
            this.button_insert.UseVisualStyleBackColor = false;
            this.button_insert.Click += new System.EventHandler(this.button_insert_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(16, 159);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(91, 22);
            this.label5.TabIndex = 120;
            this.label5.Text = "Descrição";
            // 
            // comboBox_utente
            // 
            this.comboBox_utente.FormattingEnabled = true;
            this.comboBox_utente.Location = new System.Drawing.Point(496, 91);
            this.comboBox_utente.Name = "comboBox_utente";
            this.comboBox_utente.Size = new System.Drawing.Size(494, 24);
            this.comboBox_utente.TabIndex = 119;
            this.comboBox_utente.SelectedIndexChanged += new System.EventHandler(this.comboBox_utente_SelectedIndexChanged);
            // 
            // dateTimePicker_realizacao
            // 
            this.dateTimePicker_realizacao.Location = new System.Drawing.Point(180, 89);
            this.dateTimePicker_realizacao.Name = "dateTimePicker_realizacao";
            this.dateTimePicker_realizacao.Size = new System.Drawing.Size(200, 22);
            this.dateTimePicker_realizacao.TabIndex = 118;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(16, 90);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(158, 22);
            this.label11.TabIndex = 117;
            this.label11.Text = "Data de realização";
            // 
            // comboBox_tipo
            // 
            this.comboBox_tipo.FormattingEnabled = true;
            this.comboBox_tipo.Location = new System.Drawing.Point(681, 53);
            this.comboBox_tipo.Name = "comboBox_tipo";
            this.comboBox_tipo.Size = new System.Drawing.Size(309, 24);
            this.comboBox_tipo.TabIndex = 116;
            this.comboBox_tipo.SelectedIndexChanged += new System.EventHandler(this.comboBox_tipo_SelectedIndexChanged);
            // 
            // textBox_nome
            // 
            this.textBox_nome.Location = new System.Drawing.Point(84, 52);
            this.textBox_nome.Name = "textBox_nome";
            this.textBox_nome.Size = new System.Drawing.Size(371, 22);
            this.textBox_nome.TabIndex = 115;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(16, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 22);
            this.label2.TabIndex = 114;
            this.label2.Text = "Nome ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(427, 92);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 22);
            this.label3.TabIndex = 131;
            this.label3.Text = "Utente";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(625, 54);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(47, 22);
            this.label10.TabIndex = 130;
            this.label10.Text = "Tipo";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(415, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(188, 27);
            this.label1.TabIndex = 129;
            this.label1.Text = "Atividades do lar  ";
            // 
            // Atividades_func
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(236)))), ((int)(((byte)(255)))));
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.richTextBox_descricao);
            this.Controls.Add(this.Mostrar);
            this.Controls.Add(this.textBox_Search);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.button_delete);
            this.Controls.Add(this.button_update);
            this.Controls.Add(this.button_insert);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.comboBox_utente);
            this.Controls.Add(this.dateTimePicker_realizacao);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.comboBox_tipo);
            this.Controls.Add(this.textBox_nome);
            this.Controls.Add(this.label2);
            this.Name = "Atividades_func";
            this.Size = new System.Drawing.Size(1022, 735);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBox_descricao;
        private System.Windows.Forms.Button Mostrar;
        private System.Windows.Forms.TextBox textBox_Search;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button_delete;
        private System.Windows.Forms.Button button_update;
        private System.Windows.Forms.Button button_insert;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboBox_utente;
        private System.Windows.Forms.DateTimePicker dateTimePicker_realizacao;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox comboBox_tipo;
        private System.Windows.Forms.TextBox textBox_nome;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label1;
    }
}
