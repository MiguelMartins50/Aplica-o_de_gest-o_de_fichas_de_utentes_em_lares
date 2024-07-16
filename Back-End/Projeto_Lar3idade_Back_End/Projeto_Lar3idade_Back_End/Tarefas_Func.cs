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
    public partial class Tarefas_Func : UserControl
    {
        private int iduser;
       
        private int verticalPosition;
        public Tarefas_Func(int userid)
        {
            InitializeComponent();
            verticalPosition = pictureBox1.Height + 5;
            this.iduser = userid;
            LoadData();
            vScrollBar1.Scroll += vScrollBar1_Scroll;

        }
        private void LoadData()
        {
            string query = "SELECT tarefa.nome, tarefa.descricao , tipo.tipo FROM tarefa JOIN tipo ON tarefa.Tipo_idTipo = tipo.idTipo where tarefa.Funcionario_idFuncionario = @iduser";

            using (MySqlConnection conexao = new MySqlConnection(DatabaseConfig.ConnectionString))
            {
                conexao.Open();

                using (MySqlCommand command = new MySqlCommand(query, conexao))
                {
                    command.Parameters.AddWithValue("@iduser", iduser);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Read data from the reader and create panels dynamically
                            string nome = reader["nome"].ToString();
                            string desc = reader["descricao"].ToString();
                            string tipo = reader["tipo"].ToString();

                            CreatePanel(nome, desc, tipo);
                        }
                    }
                }
            }
        }

        private void CreatePanel(string nome,  string desc, string tipo)
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

            Label labeldesc = new Label();
            labeldesc.Text = "Descrição:\n: " + desc;
            labeldesc.Location = new System.Drawing.Point(10, 115);
            labeldesc.Size = new System.Drawing.Size(550, 100);


            // Add labels to the panel
            panel.Controls.Add(labelnome);
            panel.Controls.Add(labeltipo);
            panel.Controls.Add(labeldesc);

            // Add the panel to the main form
            this.Controls.Add(panel);
            verticalPosition += panel.Height; // No spacing adjustment here
        }
        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            // Adjust the vertical position based on the scrollbar value
            verticalPosition = pictureBox1.Height + 10 - e.NewValue;

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

        private void Tarefas_Func_Load(object sender, EventArgs e)
        {

        }
    } 
}
