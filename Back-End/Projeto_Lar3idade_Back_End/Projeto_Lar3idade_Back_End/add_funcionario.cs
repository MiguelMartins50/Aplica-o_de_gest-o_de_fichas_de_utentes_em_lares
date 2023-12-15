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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Projeto_Lar3idade_Back_End
{
    public partial class add_funcionario : UserControl
    {
        private int idAdd;

        public event EventHandler ButtonClicked;

        private MySqlConnection conexao;
        public add_funcionario()
        {
            InitializeComponent();
            string connectionString = "Server=localhost;Port=3306;Database=mydb;User ID=root;Password=ipbcurso";
            conexao = new MySqlConnection(connectionString);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Pegue os valores dos controles do formulário
            string nome = textBox1.Text;
            string numero_cc = textBox2.Text;
            DateTime data_validade = dateTimePicker1.Value;
            string telemovel = textBox3.Text;
            //string funcao = textBox4.Text;
            decimal salario_hora = Convert.ToDecimal(textBox5.Text);
            string email = textBox6.Text;
            string senha = textBox7.Text;

            try
            {
                using (MySqlConnection conexao = new MySqlConnection("Server=localhost;Port=3306;Database=mydb;User ID=root;Password=ipbcurso"))
                {
                    conexao.Open();
                    string query = "INSERT INTO mydb.funcionario (idFuncionario, nome, numero_cc, data_validade, telemovel, salario_hora, email, senha)" +
                                  "VALUES (@idFuncionario,@nome, @numero_cc, @data_validade, @telemovel, @salario_hora, @email, @senha)";

                    string query2 = "SELECT * FROM funcionario ORDER BY idFuncionario DESC LIMIT 1";
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
                                idAdd = 1 + int.Parse(reader["idFuncionario"].ToString());

                            }
                        }
                    }

                    // Crie um comando MySqlCommand
                    using (MySqlCommand comando = new MySqlCommand(query, conexao))
                    {
                        // Adicione os parâmetros com os valores obtidos do formulário
                        comando.Parameters.AddWithValue("@idFuncionario", idAdd);
                        comando.Parameters.AddWithValue("@nome", nome);
                        comando.Parameters.AddWithValue("@numero_cc", numero_cc);
                        comando.Parameters.AddWithValue("@data_validade", data_validade);
                        comando.Parameters.AddWithValue("@telemovel", telemovel);
                        //comando.Parameters.AddWithValue("@funcao", funcao);
                        comando.Parameters.AddWithValue("@salario_hora", salario_hora);
                        comando.Parameters.AddWithValue("@email", email);
                        comando.Parameters.AddWithValue("@senha", senha);

                        // Execute a consulta de inserção
                        comando.ExecuteNonQuery();

                        MessageBox.Show("Funcionário adicionado com sucesso!");

                        ButtonClicked?.Invoke(this, EventArgs.Empty);

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao adicionar funcionário: " + ex.Message);
            }
            LimparTextBoxes();
        }

        private void LimparTextBoxes()
        {
            textBox1.Clear();
            textBox2.Clear();
            dateTimePicker1.Value = DateTime.Now;
            textBox3.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();  
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ButtonClicked?.Invoke(this, EventArgs.Empty);
        }
    }
}
