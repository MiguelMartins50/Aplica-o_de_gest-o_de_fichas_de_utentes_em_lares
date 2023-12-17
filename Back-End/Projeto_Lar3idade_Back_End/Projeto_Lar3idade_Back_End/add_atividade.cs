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
    public partial class add_atividade : UserControl
    {
        public event EventHandler ButtonClicked;
        private int idAdd;
        private int idtipo;
        private int idunt;
        private int idfunc;
        private Dictionary<string, string> Tipo_ = new Dictionary<string, string>();
        private Dictionary<string, string> Func_ = new Dictionary<string, string>();
        private Dictionary<string, string> Utente_ = new Dictionary<string, string>();
        private MySqlConnection conexao;

        public add_atividade()
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

                string query = "SELECT idTipo,tipo FROM tipo";
                using (MySqlCommand cmd = new MySqlCommand(query, conexao))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string id = reader["idTipo"].ToString();
                            string tipo = reader["tipo"].ToString();
                            Tipo_[tipo] = id;
                            comboBox1.Items.Add(tipo);
                        }
                    }
                }
                string query2 = "SELECT idUtente,nome FROM utente";
                using (MySqlCommand cmd = new MySqlCommand(query2, conexao))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            string id = reader["idUtente"].ToString();
                            string nome = reader["nome"].ToString();
                            comboBox2.Items.Add(nome);
                            Utente_[nome] = id;


                        }
                    }
                }
                string query3 = "SELECT idFuncionario,nome FROM funcionario";
                using (MySqlCommand cmd = new MySqlCommand(query3, conexao))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            string id = reader["idFuncionario"].ToString();
                            string nome = reader["nome"].ToString();
                            comboBox3.Items.Add(nome);
                            Func_[nome] = id;


                        }
                    }
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string nome = textBox1.Text;
            DateTime datarealizacao = dateTimePicker1.Value;
            string desc = richTextBox1.Text;

            try
            {
                using (MySqlConnection conexao = new MySqlConnection("Server=localhost;Port=3306;Database=mydb;User ID=root;Password=ipbcurso"))
                {
                    conexao.Open();
                    string query = "INSERT INTO mydb.atividade (idAtividade,Utente_idUtente, Funcionario_idFuncionario, nome, data, descricao, Tipo_idTipo)" +
                                    "VALUES (@idAtividade, @Utente_idUtente, @Funcionario_idFuncionario, @nome, @data, @descricao, @Tipo_idTipo)";
                    string query2 = "SELECT * FROM atividade ORDER BY idAtividade DESC LIMIT 1";

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
                                idAdd = 1 + int.Parse(reader["idAtividade"].ToString());

                            }
                        }
                    }


                    // Crie um comando MySqlCommand
                    using (MySqlCommand comando = new MySqlCommand(query, conexao))
                    {
                        comando.Parameters.AddWithValue("@idAtividade", idAdd);
                        comando.Parameters.AddWithValue("@Utente_idUtente", idunt);
                        comando.Parameters.AddWithValue("@Funcionario_idFuncionario", idfunc);
                        comando.Parameters.AddWithValue("@nome", nome);
                        comando.Parameters.AddWithValue("@data", datarealizacao);
                        comando.Parameters.AddWithValue("@descricao", desc);
                        comando.Parameters.AddWithValue("@Tipo_idTipo", idtipo);




                        comando.ExecuteNonQuery();

                        MessageBox.Show("Atividade adcionada com sucesso!");

                        ButtonClicked?.Invoke(this, EventArgs.Empty);
                        Limpar();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao adicionar Atividade: " + ex.Message);
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
            dateTimePicker1.Value = DateTime.Now;
            richTextBox1.Clear();


        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null)
            {
                string selectedValue = comboBox1.SelectedItem.ToString();

                if (Tipo_.TryGetValue(selectedValue, out string id))
                {

                    // Use specificColumnData as needed (e.g., assign it to a variable)
                    idtipo = int.Parse(id.ToString());
                    Console.WriteLine(id);
                }
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedItem != null)
            {
                string selectedValue = comboBox2.SelectedItem.ToString();

                if (Utente_.TryGetValue(selectedValue, out string id))
                {

                    // Use specificColumnData as needed (e.g., assign it to a variable)
                    idunt = int.Parse(id.ToString());
                    Console.WriteLine(idunt);
                }
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox3.SelectedItem != null)
            {
                string selectedValue = comboBox3.SelectedItem.ToString();

                if (Func_.TryGetValue(selectedValue, out string id))
                {

                    // Use specificColumnData as needed (e.g., assign it to a variable)
                    idfunc = int.Parse(id.ToString());
                    Console.WriteLine(idfunc);
                }
            }
        }
    }
}
