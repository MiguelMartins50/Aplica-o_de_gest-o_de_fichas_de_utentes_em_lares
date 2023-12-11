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

        private MySqlConnection conexao;

        public event EventHandler ButtonClicked;

        public add_responsavel()
        {
           
            InitializeComponent();
            string connectionString = "Server=localhost;Port=3306;Database=mydb;User ID=root;Password=ipbcurso";
            conexao = new MySqlConnection(connectionString);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ButtonClicked?.Invoke(this, EventArgs.Empty);
        }
          

        private void button1_Click(object sender, EventArgs e)
        {
            ButtonClicked?.Invoke(this, EventArgs.Empty);

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

    }

    
}
