using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;


namespace Projeto_Lar3idade_Back_End
{
    public partial class atividades : UserControl
    {
        private int verticalPosition = 50;

        public event EventHandler ButtonClicked;
        public atividades()
        {
            InitializeComponent();
            LoadData();
            vScrollBar1.Dock = DockStyle.Right;
            vScrollBar1.Scroll += vScrollBar1_Scroll;
        }

        private void LoadData()
        {
            string connectionString = "Server=localhost;Port=3306;Database=mydb;User ID=root;Password=ipbcurso";
            string query = "SELECT atividade.nome, atividade.data, atividade.descricao, tipo.tipo FROM atividade JOIN tipo ON atividade.Tipo_idTipo = tipo.idTipo";

            using (MySqlConnection conexao = new MySqlConnection(connectionString))
            {
                conexao.Open();

                using (MySqlCommand command = new MySqlCommand(query, conexao))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Read data from the reader and create panels dynamically
                            string nome = reader["nome"].ToString();
                            string data_ = reader["data"].ToString();
                            string desc = reader["descricao"].ToString();
                            string tipo = reader["tipo"].ToString();


                            CreatePanel(nome, data_, desc, tipo);
                        }
                    }
                }
            }
        }
        private void CreatePanel(string nome, string data_, string desc, string tipo)
        {
            // Create a new panel for each database entry
            Panel panel = new Panel();
            panel.BorderStyle = BorderStyle.FixedSingle;
            panel.Size = new System.Drawing.Size(600, 200);
            panel.Location = new System.Drawing.Point(75, verticalPosition);
            panel.BackColor = Color.White;

            // Create labels to display data
            Label labelnome = new Label();
            labelnome.Text = "Nome: " + nome;
            labelnome.Location = new System.Drawing.Point(10, 10);
            labelnome.Size = new System.Drawing.Size(300, 25);

            Label labeltipo = new Label();
            labeltipo.Text = "Tipo: " + tipo;
            labeltipo.Location = new System.Drawing.Point(10, 45);
            labeltipo.Size = new System.Drawing.Size(300, 25);

            Label labeldata_ = new Label();
            labeldata_.Text = "Data de realização: " + data_;
            labeldata_.Location = new System.Drawing.Point(10, 80);
            labeldata_.Size = new System.Drawing.Size(300, 25);


            Label labeldesc = new Label();
            labeldesc.Text = "Descrição:\n: " + desc;
            labeldesc.Location = new System.Drawing.Point(10, 115);
            labeldesc.Size = new System.Drawing.Size(550, 100);


            // Add labels to the panel
            panel.Controls.Add(labelnome);
            panel.Controls.Add(labeltipo);
            panel.Controls.Add(labeldata_);
            panel.Controls.Add(labeldesc);

            // Add the panel to the main form
            this.Controls.Add(panel);
            verticalPosition += panel.Height; // No spacing adjustment here
        }
        private void button1_Click(object sender, EventArgs e)
        {
            ButtonClicked?.Invoke(this, EventArgs.Empty);

        }
        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            // Adjust the vertical position based on the scrollbar value
            verticalPosition = 10 - e.NewValue;

            // Reposition the panels based on the new vertical position
            RepositionPanels();
        }
        private void RepositionPanels()
        {
            // Iterate through all controls in the form and adjust the location of panels
            foreach (Control control in this.Controls)
            {
                if (control is Panel)
                {
                    Panel panel = (Panel)control;
                    panel.Location = new Point(panel.Location.X, verticalPosition);
                    verticalPosition += panel.Height + 10; // Adjusted spacing
                }
            }
        }
    }
}
