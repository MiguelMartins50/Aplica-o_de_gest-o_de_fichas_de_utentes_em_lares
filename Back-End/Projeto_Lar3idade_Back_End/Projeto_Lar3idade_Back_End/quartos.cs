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
    public partial class quartos : UserControl
    {

        public quartos()
        {
            InitializeComponent();
            FillListView();
        }
        private void FillListView()
        {
            listView1.Columns.Clear(); // Clear previously added columns
            listView1.Items.Clear(); // Clear previously populated items
            listView1.View = View.Details; // Set View property
            listView1.Columns.Add("Id", 50);
            listView1.Columns.Add("Estado", 125);
            listView1.Columns.Add("quantidade_cama", 75);
            listView1.Columns.Add("utente_1", 150);
            listView1.Columns.Add("utente_2", 150);
            listView1.Columns.Add("utente_3", 150);
            listView1.HeaderStyle = ColumnHeaderStyle.None;
            // Set up MySqlConnection
            string connectionString = "Server=localhost;Port=3306;Database=mydb;User ID=root;Password=ipbcurso";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                // Open the connection
                connection.Open();

                // Create MySqlCommand
                string query = "SELECT quarto.idQuarto, quarto.estado, quarto.quantidade_cama, MAX(CASE WHEN quarto.idQuarto = utente.Quarto_idQuarto THEN utente.nome END) AS utente_1, MAX(CASE WHEN quarto.idQuarto = utente.Quarto_idQuarto THEN utente.nome END) AS utente_2, MAX(CASE WHEN quarto.idQuarto = utente.Quarto_idQuarto THEN utente.nome END) AS utente_3 FROM quarto LEFT JOIN utente ON quarto.idQuarto = utente.Quarto_idQuarto GROUP BY quarto.idQuarto, quarto.estado, quarto.quantidade_cama";
                Console.WriteLine();
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    // Execute the query
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        // Create a list to store data
                        List<string[]> data = new List<string[]>();

                        // Iterate through the results
                        while (reader.Read())
                        {
                            string aviso = "quarto";
                            Console.WriteLine(aviso);
                            // Add data to the list
                            string idQuarto = reader["idQuarto"].ToString();
                            string estado = reader["estado"].ToString();
                            string quantidade_cama = reader["quantidade_cama"].ToString();
                            string utente_1 = reader["utente_1"].ToString();
                            string utente_2 = reader["utente_2"].ToString();
                            string utente_3 = reader["utente_3"].ToString();
                            string[] row = { idQuarto, estado, quantidade_cama, utente_1, utente_2, utente_3 };
                            Console.WriteLine(row);
                            ListViewItem item = new ListViewItem(row);
                            item.Font = new Font("Arial", 13, FontStyle.Regular);
                            Console.WriteLine(item);
                            listView1.Items.Add(item);

                            Console.WriteLine(row);
                            Console.WriteLine(idQuarto);
                            Console.WriteLine(estado);
                            Console.WriteLine(quantidade_cama);
                            Console.WriteLine(utente_1);
                            Console.WriteLine(utente_2);
                            Console.WriteLine(utente_3);

                        }


                    }
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
