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
    public partial class responsavel : UserControl
    {
        public event EventHandler ButtonClicked;

        public responsavel()
        {
            InitializeComponent();
            FillListView();

        }

        private void FillListView()
        {
            listView1.Columns.Clear(); // Clear previously added columns
            listView1.Items.Clear(); // Clear previously populated items
            listView1.View = View.Details; // Set View property
            listView1.Columns.Add("Id", 100);
            listView1.Columns.Add("Nome", 700);
            listView1.HeaderStyle = ColumnHeaderStyle.None;
            // Set up MySqlConnection
            string connectionString = "Server=localhost;Port=3306;Database=mydb;User ID=root;Password=ipbcurso";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                // Open the connection
                connection.Open();

                // Create MySqlCommand
                string query = "SELECT * FROM familiar";
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
                            // Add data to the list
                            string idUtente = reader["idFamiliar"].ToString();
                            string nome = reader["nomel"].ToString();
                            string[] row = { idUtente, nome };
                            Console.WriteLine(row);
                            ListViewItem item = new ListViewItem(row);
                            item.Font = new Font("Arial", 16, FontStyle.Regular);
                            Console.WriteLine(item);
                            listView1.Items.Add(item);

                            Console.WriteLine(row);
                            Console.WriteLine(idUtente);
                            Console.WriteLine(nome);

                        }


                    }
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            ButtonClicked?.Invoke(this, EventArgs.Empty);

        }
    }
}
