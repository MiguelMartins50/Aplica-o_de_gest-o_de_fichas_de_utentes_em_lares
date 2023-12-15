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

    public partial class add_quartos : UserControl
    {
        public event EventHandler ButtonClicked;
        private int idAdd;
        private int idunt1;
        private int idunt2;
        private int idunt3;
        private Dictionary<string, string> Utente_ = new Dictionary<string, string>();
        private MySqlConnection conexao;
        private int control1;
        private int control2;
        private int control3;
        private string estado;




        public add_quartos()
        {
            InitializeComponent();
            string connectionString = "Server=localhost;Port=3306;Database=mydb;User ID=root;Password=ipbcurso";
            conexao = new MySqlConnection(connectionString);
            LoadComboBox();
        }

        private void LoadComboBox()
        {

            comboBox1.Items.Add("-----------------");
            comboBox1.Items.Add("Livre");
            comboBox1.Items.Add("Limpeza");
            comboBox1.Items.Add("Manutenção");
            comboBox1.Items.Add("Desinfeção");
            comboBox1.Items.Add("Reconstrução");            
            comboBox2.Items.Add("-----------------");
            comboBox3.Items.Add("-----------------");
            comboBox4.Items.Add("-----------------");

            using (conexao)
            {
                string query = "SELECT idUtente, nome FROM utente WHERE utente.Quarto_idQuarto IS NULL; ";
                using (MySqlCommand cmd = new MySqlCommand(query, conexao))
                {
                    conexao.Open();
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            string nome = reader["nome"].ToString();
                            string id = reader["idUtente"].ToString();
                            comboBox2.Items.Add(reader["nome"].ToString());
                            comboBox3.Items.Add(reader["nome"].ToString());
                            comboBox4.Items.Add(reader["nome"].ToString());
                            Utente_[nome] = id;

                        }
                    }
                }
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
            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
            comboBox3.SelectedIndex = -1;
            comboBox4.SelectedIndex = -1;


        }
        private void button1_Click(object sender, EventArgs e)
        {


            // Pegue os valores dos controles do formulário
            int quantidade_cama = int.Parse(textBox1.Text.ToString());
            

            try
            {
                using (MySqlConnection conexao = new MySqlConnection("Server=localhost;Port=3306;Database=mydb;User ID=root;Password=ipbcurso"))
                {
                    conexao.Open();
                    string query = "INSERT INTO mydb.quarto (idQuarto, estado, quantidade_cama)" +
                                  "VALUES (@idQuarto,@estado, @quantidade_cama)";

                    string query2 = "SELECT * FROM quarto ORDER BY idQuarto DESC LIMIT 1";
                    string query3 = "UPDATE utente SET Quarto_idQuarto = @QuartoId WHERE nome = @Nome;";

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
                                idAdd = 1 + int.Parse(reader["idQuarto"].ToString());

                            }
                        }
                    }
                    


                    // Crie um comando MySqlCommand
                    using (MySqlCommand comando = new MySqlCommand(query, conexao))
                    {
                        // Adicione os parâmetros com os valores obtidos do formulário
                        comando.Parameters.AddWithValue("@idQuarto", idAdd);
                        comando.Parameters.AddWithValue("@estado", estado);
                        comando.Parameters.AddWithValue("@quantidade_cama", quantidade_cama);
                        

                        // Execute a consulta de inserção
                        comando.ExecuteNonQuery();

                        MessageBox.Show("Quarto adicionado com sucesso!");

                        ButtonClicked?.Invoke(this, EventArgs.Empty);

                    }
                    if (control1 == 1)
                    {
                        using (MySqlCommand comando = new MySqlCommand(query3, conexao))
                        {
                            // Adicione os parâmetros com os valores obtidos do formulário
                            comando.Parameters.AddWithValue("@QuartoId", idAdd);
                            comando.Parameters.AddWithValue("@Familiar_idFamiliar", idunt1);


                            // Execute a consulta de inserção
                            comando.ExecuteNonQuery();

                            MessageBox.Show("Familiar associado ao utente com sucesso!");


                        }
                    }
                    if (control2 == 1)
                    {
                        using (MySqlCommand comando = new MySqlCommand(query3, conexao))
                        {
                            // Adicione os parâmetros com os valores obtidos do formulário
                            comando.Parameters.AddWithValue("@QuartoId", idAdd);
                            comando.Parameters.AddWithValue("@Familiar_idFamiliar", idunt2);


                            // Execute a consulta de inserção
                            comando.ExecuteNonQuery();

                            MessageBox.Show("Familiar associado ao utente com sucesso!");


                        }
                    }
                    if (control3 == 1)
                    {
                        using (MySqlCommand comando = new MySqlCommand(query3, conexao))
                        {
                            // Adicione os parâmetros com os valores obtidos do formulário
                            comando.Parameters.AddWithValue("@QuartoId", idAdd);
                            comando.Parameters.AddWithValue("@Familiar_idFamiliar", idunt3);


                            // Execute a consulta de inserção
                            comando.ExecuteNonQuery();

                            MessageBox.Show("Familiar associado ao utente com sucesso!");


                        }
                    }

                    Limpar();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao adicionar familiar: " + ex.Message);
            }
        }
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (comboBox2.SelectedItem != null)
            {
                string selectedValue = comboBox2.SelectedItem.ToString();
                if (selectedValue == "-----------------")
                {
                    // Clear the ComboBox selection
                    comboBox2.SelectedIndex = -1;
                    control1 = 0;
                    // Additional actions if needed when the ComboBox is emptied
                    Console.WriteLine("ComboBox is now empty!");
                }
                else
                {
                    // Retrieve the stored specific column data for the selected item
                    if (Utente_.TryGetValue(selectedValue, out string id))
                    {
                        control1 = 1;
                        // Use specificColumnData as needed (e.g., assign it to a variable)
                        idunt1 = int.Parse(id.ToString());
                        Console.WriteLine(idunt1);
                    }
                }
            }

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (comboBox3.SelectedItem != null)
            {
                string selectedValue = comboBox3.SelectedItem.ToString();

                if (selectedValue == "-----------------")
                {
                    // Clear the ComboBox selection
                    comboBox3.SelectedIndex = -1;
                    control2 = 0;
                    // Additional actions if needed when the ComboBox is emptied
                    Console.WriteLine("ComboBox is now empty!");
                }
                else
                {
                    // Retrieve the stored specific column data for the selected item
                    if (Utente_.TryGetValue(selectedValue, out string id))
                    {
                        control2 = 1;
                        // Use specificColumnData as needed (e.g., assign it to a variable)
                        idunt2 = int.Parse(id.ToString());
                        Console.WriteLine(idunt2);
                    }
                }
            }
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (comboBox4.SelectedItem != null)
            {
                string selectedValue = comboBox4.SelectedItem.ToString();

                if (selectedValue == "-----------------")
                {
                    // Clear the ComboBox selection
                    comboBox4.SelectedIndex = -1;
                    control3 = 0;
                    // Additional actions if needed when the ComboBox is emptied
                    Console.WriteLine("ComboBox is now empty!");
                }
                else
                {
                    // Retrieve the stored specific column data for the selected item
                    if (Utente_.TryGetValue(selectedValue, out string id))
                    {
                        control3 = 1;
                        // Use specificColumnData as needed (e.g., assign it to a variable)
                        idunt3 = int.Parse(id.ToString());
                        Console.WriteLine(idunt3);
                    }
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null)
            {
                estado = comboBox1.SelectedItem.ToString();
            }
        }
    }
}
