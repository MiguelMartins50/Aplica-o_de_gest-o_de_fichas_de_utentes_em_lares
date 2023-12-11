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
    public partial class add_responsavel : UserControl
    {
        private int idAdd;
        private int idAddUt;
        private int idAddUt2;
        private int idAddUt3;
        private int idAddUt4;
        private int idUt1;
        private int idUt2;
        private int idUt3;
        private int idUt4;
        private int control1;
        private int control2;
        private int control3;
        private int control4;
       




        private MySqlConnection conexao;

        public event EventHandler ButtonClicked;
        private Dictionary<string, string> utente_ = new Dictionary<string, string>();


        public add_responsavel()
        {
           
            InitializeComponent();
            string connectionString = "Server=localhost;Port=3306;Database=mydb;User ID=root;Password=ipbcurso";
            conexao = new MySqlConnection(connectionString);
            LoadComboBox();
        }

        private void LoadComboBox()
        {

            comboBox1.Items.Add("-----------------");
            comboBox2.Items.Add("-----------------");
            comboBox3.Items.Add("-----------------");
            comboBox4.Items.Add("-----------------");

            using (conexao)
            {
                string query = "SELECT idUtente,nome FROM utente";
                using (MySqlCommand cmd = new MySqlCommand(query, conexao))
                {
                    conexao.Open();
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            
                            string nome = reader["nome"].ToString();
                            string id = reader["idUtente"].ToString();
                            comboBox1.Items.Add(reader["nome"].ToString());
                            comboBox2.Items.Add(reader["nome"].ToString());
                            comboBox3.Items.Add(reader["nome"].ToString());
                            comboBox4.Items.Add(reader["nome"].ToString());
                            utente_[nome] = id;
                            
                        }
                    }
                }
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            ButtonClicked?.Invoke(this, EventArgs.Empty);
        }
          

        private void button1_Click(object sender, EventArgs e)
        {
            

            // Pegue os valores dos controles do formulário
            string nomel = textBox1.Text;
            DateTime data_nascimento = dateTimePicker1.Value;
            string numero_cc = textBox2.Text;
            DateTime data_validade = dateTimePicker2.Value;
            string morada = textBox3.Text;
            string cod_postal = textBox4.Text;
            string tel_casa = textBox5.Text;
            string telemovel = textBox6.Text;
            string ocupacao = textBox7.Text;
            string email = textBox8.Text;
            string senha = textSenha.Text;
            string parentesco = textBox11.Text;

            try
            {
                using (MySqlConnection conexao = new MySqlConnection("Server=localhost;Port=3306;Database=mydb;User ID=root;Password=ipbcurso"))
                {
                    conexao.Open();
                    string query = "INSERT INTO mydb.familiar (idFamiliar, nomel, numero_cc, data_validade, telemovel, data_nascimento,parentesco_relacao,morada,cod_postal,ocupacao,tel_casa, email, senha)" +
                                  "VALUES (@idFamiliar,@nomel, @numero_cc, @data_validade, @telemovel, @data_nascimento, @parentesco_relacao,@morada,@cod_postal,@ocupacao,@tel_casa,@email, @senha)";

                    string query2 = "SELECT * FROM familiar ORDER BY idFamiliar DESC LIMIT 1";
                    string query3 = "SELECT * FROM utente_familiar ORDER BY idUtente_familiar DESC LIMIT 1";
                    string query4 = "LOCK TABLES utente_familiar WRITE;" +
                                    "ALTER TABLE utente_familiar DISABLE KEYS;" +
                                    "INSERT INTO mydb.utente_familiar (idUtente_familiar, Utente_idUtente, Familiar_idFamiliar)" +
                                    "VALUES (@idUtente_familiar, @Utente_idUtente, @Familiar_idFamiliar);" +
                                    "ALTER TABLE utente_familiar ENABLE KEYS;" +
                                    "UNLOCK TABLES;";
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
                                idAdd = 1 + int.Parse(reader["idFamiliar"].ToString());

                            }
                        }
                    }
                    using (MySqlCommand procurarId = new MySqlCommand(query3, conexao))
                    {
                        using (MySqlDataReader reader = procurarId.ExecuteReader())
                        {
                            // Create a list to store data
                            List<string[]> data = new List<string[]>();

                            // Iterate through the results
                            while (reader.Read())
                            {
                                // Add data to the list
                                idAddUt = 1 + int.Parse(reader["idUtente_familiar"].ToString());

                            }
                        }
                    }
                    using (MySqlCommand procurarId = new MySqlCommand(query3, conexao))
                    {
                        using (MySqlDataReader reader = procurarId.ExecuteReader())
                        {
                            // Create a list to store data
                            List<string[]> data = new List<string[]>();

                            // Iterate through the results
                            while (reader.Read())
                            {
                                // Add data to the list
                                idAddUt2 = 1 + int.Parse(reader["idUtente_familiar"].ToString());

                            }
                        }
                    }
                    using (MySqlCommand procurarId = new MySqlCommand(query3, conexao))
                    {
                        using (MySqlDataReader reader = procurarId.ExecuteReader())
                        {
                            // Create a list to store data
                            List<string[]> data = new List<string[]>();

                            // Iterate through the results
                            while (reader.Read())
                            {
                                // Add data to the list
                                idAddUt3 = 1 + int.Parse(reader["idUtente_familiar"].ToString());

                            }
                        }
                    }
                    using (MySqlCommand procurarId = new MySqlCommand(query3, conexao))
                    {
                        using (MySqlDataReader reader = procurarId.ExecuteReader())
                        {
                            // Create a list to store data
                            List<string[]> data = new List<string[]>();

                            // Iterate through the results
                            while (reader.Read())
                            {
                                // Add data to the list
                                idAddUt4 = 1 + int.Parse(reader["idUtente_familiar"].ToString());

                            }
                        }
                    }
                    

                    // Crie um comando MySqlCommand
                    using (MySqlCommand comando = new MySqlCommand(query, conexao))
                    {
                        // Adicione os parâmetros com os valores obtidos do formulário
                        comando.Parameters.AddWithValue("@idFamiliar", idAdd);
                        comando.Parameters.AddWithValue("@nomel", nomel);
                        comando.Parameters.AddWithValue("@numero_cc", numero_cc);
                        comando.Parameters.AddWithValue("@data_validade", data_validade);
                        comando.Parameters.AddWithValue("@telemovel", telemovel);
                        comando.Parameters.AddWithValue("@data_nascimento", data_nascimento);
                        comando.Parameters.AddWithValue("@parentesco_relacao", parentesco);
                        comando.Parameters.AddWithValue("@morada", morada);
                        comando.Parameters.AddWithValue("@cod_postal", cod_postal);
                        comando.Parameters.AddWithValue("@ocupacao", ocupacao);
                        comando.Parameters.AddWithValue("@tel_casa",tel_casa );
                        comando.Parameters.AddWithValue("@email", email);
                        comando.Parameters.AddWithValue("@senha", senha);

                        // Execute a consulta de inserção
                        comando.ExecuteNonQuery();

                        MessageBox.Show("Familiar adicionado com sucesso!");

                        ButtonClicked?.Invoke(this, EventArgs.Empty);

                    }
                    if (control1 == 1)
                    {
                        using (MySqlCommand comando = new MySqlCommand(query4, conexao))
                        {
                            // Adicione os parâmetros com os valores obtidos do formulário
                            comando.Parameters.AddWithValue("@idUtente_familiar", idAddUt);
                            comando.Parameters.AddWithValue("@Utente_idUtente", idUt1);
                            comando.Parameters.AddWithValue("@Familiar_idFamiliar", idAdd);


                            // Execute a consulta de inserção
                            comando.ExecuteNonQuery();

                            MessageBox.Show("Familiar associado ao utente com sucesso!");


                        }
                    }
                    if (control2 == 1)
                    {
                        using (MySqlCommand comando = new MySqlCommand(query4, conexao))
                        {
                            // Adicione os parâmetros com os valores obtidos do formulário
                            comando.Parameters.AddWithValue("@idUtente_familiar", idAddUt2);
                            comando.Parameters.AddWithValue("@Utente_idUtente", idUt2);
                            comando.Parameters.AddWithValue("@Familiar_idFamiliar", idAdd);


                            // Execute a consulta de inserção
                            comando.ExecuteNonQuery();

                            MessageBox.Show("Familiar associado ao utente com sucesso!");


                        }
                    }
                    if (control3 == 1)
                    {
                        using (MySqlCommand comando = new MySqlCommand(query4, conexao))
                        {
                            // Adicione os parâmetros com os valores obtidos do formulário
                            comando.Parameters.AddWithValue("@idUtente_familiar", idAddUt3);
                            comando.Parameters.AddWithValue("@Utente_idUtente", idUt3);
                            comando.Parameters.AddWithValue("@Familiar_idFamiliar", idAdd);


                            // Execute a consulta de inserção
                            comando.ExecuteNonQuery();

                            MessageBox.Show("Familiar associado ao utente com sucesso!");


                        }
                    }
                    if (control4 == 1)
                    {
                        using (MySqlCommand comando = new MySqlCommand(query4, conexao))
                        {
                            // Adicione os parâmetros com os valores obtidos do formulário
                            comando.Parameters.AddWithValue("@idUtente_familiar", idAddUt4);
                            comando.Parameters.AddWithValue("@Utente_idUtente", idUt4);
                            comando.Parameters.AddWithValue("@Familiar_idFamiliar", idAdd);


                            // Execute a consulta de inserção
                            comando.ExecuteNonQuery();

                            MessageBox.Show("Familiar associado ao utente com sucesso!");


                        }
                    }


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao adicionar familiar: " + ex.Message);
            }

            LimparTextBoxes();
        }
        private void LimparTextBoxes()
        {
            textBox1.Clear();
            textBox2.Clear();
            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker2.Value = DateTime.Now;
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
            textBox8.Clear();
            textSenha.Clear();
            textBox11.Clear();
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (comboBox1.SelectedItem != null)
            {
                string selectedValue = comboBox1.SelectedItem.ToString();
                if (selectedValue == "-----------------")
                {
                    // Clear the ComboBox selection
                    comboBox1.SelectedIndex = -1;
                    control1 = 0;
                    // Additional actions if needed when the ComboBox is emptied
                    Console.WriteLine("ComboBox is now empty!");
                }
                else
                {
                    // Retrieve the stored specific column data for the selected item
                    if (utente_.TryGetValue(selectedValue, out string id))
                    {
                        control1 = 1;
                        // Use specificColumnData as needed (e.g., assign it to a variable)
                        idUt1 = int.Parse(id.ToString());
                        Console.WriteLine(idUt1);
                    }
                }
            }
            
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (comboBox1.SelectedItem != null)
            {
                string selectedValue = comboBox1.SelectedItem.ToString();

                if (selectedValue == "-----------------")
                {
                    // Clear the ComboBox selection
                    comboBox1.SelectedIndex = -1;
                    control2 = 0;
                    // Additional actions if needed when the ComboBox is emptied
                    Console.WriteLine("ComboBox is now empty!");
                }
                else
                {
                    // Retrieve the stored specific column data for the selected item
                    if (utente_.TryGetValue(selectedValue, out string id))
                    {
                        control2 = 1;
                        // Use specificColumnData as needed (e.g., assign it to a variable)
                        idUt2 = int.Parse(id.ToString());
                        Console.WriteLine(idUt2);
                    }
                }
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (comboBox1.SelectedItem != null)
            {
                string selectedValue = comboBox1.SelectedItem.ToString();

                if (selectedValue == "-----------------")
                {
                    // Clear the ComboBox selection
                    comboBox1.SelectedIndex = -1;
                    control3 = 0;
                    // Additional actions if needed when the ComboBox is emptied
                    Console.WriteLine("ComboBox is now empty!");
                }
                else
                {
                    // Retrieve the stored specific column data for the selected item
                    if (utente_.TryGetValue(selectedValue, out string id))
                    {
                        control3 = 1;
                        // Use specificColumnData as needed (e.g., assign it to a variable)
                        idUt3 = int.Parse(id.ToString());
                        Console.WriteLine(idUt3);
                    }
                }
            }
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (comboBox1.SelectedItem != null)
            {
                string selectedValue = comboBox1.SelectedItem.ToString();

                if (selectedValue == "-----------------")
                {
                    // Clear the ComboBox selection
                    comboBox1.SelectedIndex = -1;
                    control4 = 0;
                    // Additional actions if needed when the ComboBox is emptied
                    Console.WriteLine("ComboBox is now empty!");
                }
                else
                {
                    // Retrieve the stored specific column data for the selected item
                    if (utente_.TryGetValue(selectedValue, out string id))
                    {
                        control4 = 1;
                        // Use specificColumnData as needed (e.g., assign it to a variable)
                        idUt4 = int.Parse(id.ToString());
                        Console.WriteLine(idUt4);
                    }
                }
            }
        }
    }

    
}
