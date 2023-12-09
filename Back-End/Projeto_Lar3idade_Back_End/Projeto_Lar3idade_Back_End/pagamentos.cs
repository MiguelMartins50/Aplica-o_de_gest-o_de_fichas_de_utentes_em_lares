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
    public partial class pagamentos : UserControl
    {

        public pagamentos()
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
            listView1.Columns.Add("Estado", 100);
            listView1.Columns.Add("Valor", 75);
            listView1.Columns.Add("Data limite", 125);
            listView1.Columns.Add("Utente", 200);
            listView1.Columns.Add("Responsavel", 200);
            listView1.HeaderStyle = ColumnHeaderStyle.None;
            // Set up MySqlConnection
            string connectionString = "Server=localhost;Port=3306;Database=mydb;User ID=root;Password=ipbcurso";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                // Open the connection
                connection.Open();

                // Create MySqlCommand
                string query = "SELECT pagamento.idPagamento, pagamento.estado, pagamento.data_limitel, pagamento.valor, utente.nome, familiar.nomel FROM pagamento JOIN utente ON pagamento.Utente_idUtente = utente.idUtente JOIN familiar ON pagamento.Familiar_idFamiliar = familiar.idFamiliar";
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
                            string aviso = "pagamento";
                            Console.WriteLine(aviso);
                            // Add data to the list
                            string idPagamento = reader["idPagamento"].ToString();
                            string estado = reader["estado"].ToString();
                            string data_limitel = reader["data_limitel"].ToString();
                            string valor = reader["valor"].ToString();
                            string utente_nome = reader["nome"].ToString();
                            string familiar_nomel = reader["nomel"].ToString();
                            string[] row = { idPagamento , estado, valor, data_limitel , utente_nome , familiar_nomel };
                            Console.WriteLine(row);
                            ListViewItem item = new ListViewItem(row);
                            item.Font = new Font("Arial", 13, FontStyle.Regular);
                            Console.WriteLine(item);
                            listView1.Items.Add(item);

                            Console.WriteLine(row);
                            Console.WriteLine(idPagamento);
                            Console.WriteLine(estado);
                            Console.WriteLine(valor);
                            Console.WriteLine(data_limitel);
                            Console.WriteLine(utente_nome);
                            Console.WriteLine(familiar_nomel);
                            
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
