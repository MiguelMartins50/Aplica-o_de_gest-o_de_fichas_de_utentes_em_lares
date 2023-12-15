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
    public partial class add_pagamento : UserControl
    {
        private int idAdd;
        private int control1;
        private int control2;
        private int idunt;
        private int idresp;
        public event EventHandler ButtonClicked;
        private Dictionary<string, string> Utente_ = new Dictionary<string, string>();
        private Dictionary<string, string> resp_ = new Dictionary<string, string>();
        private MySqlConnection conexao;
        private string naopago = "Não Pago";

        public add_pagamento()
        {
            InitializeComponent();
            string connectionString = "Server=localhost;Port=3306;Database=mydb;User ID=root;Password=ipbcurso";
            conexao = new MySqlConnection(connectionString);
            LoadComboBox();
        }

        private void LoadComboBox()
        {


            using (conexao)
            {
                conexao.Open();
                
                    string query = "SELECT idUtente,nome FROM utente";
                    using (MySqlCommand cmd = new MySqlCommand(query, conexao))
                    {
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string id = reader["idUtente"].ToString();
                                string tipo = reader["nome"].ToString();
                                Utente_[tipo] = id;
                                comboBox1.Items.Add(tipo);
                            }
                        }
                    }
                
                
                
                    
                
                
            }
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Console.WriteLine(control1);
            Console.WriteLine(control2);
            control1 = 1;
            if (comboBox1.SelectedItem != null)
            {
                string selectedValue = comboBox1.SelectedItem.ToString();

                if (Utente_.TryGetValue(selectedValue, out string id))
                {

                    // Use specificColumnData as needed (e.g., assign it to a variable)
                    idunt = int.Parse(id.ToString());
                    Console.WriteLine(id);

                    LoadComboBox2(idunt);



                }
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            Console.WriteLine(control1);
            Console.WriteLine(control2);
            control2 = 1;
            foreach (var kvp in resp_)
            {
                Console.WriteLine($"{kvp.Key}: {kvp.Value}");
            }
            string selectedValue = comboBox2.SelectedItem.ToString();
            Console.WriteLine(selectedValue);


            if (resp_.TryGetValue(selectedValue, out string id))
                {

                    // Use specificColumnData as needed (e.g., assign it to a variable)
                    Console.WriteLine(idresp + "inicio");
                    idresp = int.Parse(id.ToString());
                    Console.WriteLine("id"+id );
                    Console.WriteLine("idresp"+idresp );




                }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string valor = textBox1.Text;
            DateTime datalimite = dateTimePicker1.Value;





            try
            {
                using (MySqlConnection conexao = new MySqlConnection("Server=localhost;Port=3306;Database=mydb;User ID=root;Password=ipbcurso"))
                {
                    conexao.Open();
                    string query = "INSERT INTO mydb.pagamento (idPagamento,Utente_idUtente, Familiar_idFamiliar, data_limitel, valor, estado)" +
                                    "VALUES (@idPagamento, @Utente_idUtente, @Familiar_idFamiliar, @data_limitel, @valor, @estado)";
                    string query2 = "SELECT * FROM pagamento ORDER BY idPagamento DESC LIMIT 1";

                    using (MySqlCommand procurarId = new MySqlCommand(query2, conexao))
                    {
                        using (MySqlDataReader reader = procurarId.ExecuteReader())
                        {
                            // Create a list to store data
                            List<string[]> data = new List<string[]>();

                            // Iterate through the results
                            while (reader.Read())
                            {
                                // Add data to the list
                                idAdd = 1 + int.Parse(reader["idPagamento"].ToString());

                            }
                        }
                    }


                    // Crie um comando MySqlCommand
                    using (MySqlCommand comando = new MySqlCommand(query, conexao))
                    {
                        Console.WriteLine("id"+idAdd);
                        Console.WriteLine("utente"+idunt);
                        Console.WriteLine("responsavel "+idresp);
                        Console.WriteLine(datalimite);
                        Console.WriteLine(valor);
                        comando.Parameters.AddWithValue("@idPagamento", idAdd);
                        comando.Parameters.AddWithValue("@Utente_idUtente", idunt);
                        comando.Parameters.AddWithValue("@Familiar_idFamiliar", idresp);
                        comando.Parameters.AddWithValue("@data_limitel", datalimite);
                        comando.Parameters.AddWithValue("@valor", valor);
                        comando.Parameters.AddWithValue("@estado", naopago);
                        




                        comando.ExecuteNonQuery();

                        MessageBox.Show("Pagamento adcionada com sucesso!");

                        ButtonClicked?.Invoke(this, EventArgs.Empty);
                        Limpar();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao adicionar Pagamento: " + ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ButtonClicked?.Invoke(this, EventArgs.Empty);
            Limpar();
        }

        private void Limpar()
        {
            textBox1.Clear();
            dateTimePicker1.Value = DateTime.Now;
            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;


        }
        private void LoadComboBox2(int selectedUtenteId)
        {
            comboBox2.Items.Clear();
            comboBox2.SelectedIndex = -1; // Set the selected index to -1 to clear the selection
            using (conexao)
            {
                conexao.Open();
                string query3 = $"SELECT idFamiliar, nomel FROM familiar JOIN utente_familiar  ON idFamiliar = utente_familiar.Familiar_idFamiliar WHERE utente_familiar.Utente_idUtente ={ selectedUtenteId};";
                using (MySqlCommand cmd = new MySqlCommand(query3, conexao))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string id = reader["idFamiliar"].ToString();
                            string nome = reader["nomel"].ToString();
                            comboBox2.Items.Add(nome);
                            resp_[nome] = id;
                            foreach (var kvp in resp_)
                            {
                                Console.WriteLine($"{kvp.Key}: {kvp.Value}");
                            }

                            Console.WriteLine("resp"+id);
                        }
                    }
                }
            }
        }

    }
}
