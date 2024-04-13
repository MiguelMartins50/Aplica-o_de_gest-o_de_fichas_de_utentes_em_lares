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
    public partial class Login : Form
    {
        MySqlConnection conexao;

        public Login()
        {
            InitializeComponent();
            textBox2.PasswordChar = '*';

            // Inicialize a conexão no construtor do formulário
            string connectionString = "Server=projetolar3idade.mysql.database.azure.com;Port=3306;Database=mydb;Uid=projeto4461045279;Pwd=Ipbcurso1";
            conexao = new MySqlConnection(connectionString);
        }
        private void Form1_Resize(object sender, EventArgs e)
        {
            this.Size = new Size(1000, 700); // Replace width and height with your desired values
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string email = textBox1.Text;
            string senha = textBox2.Text;
            int userID;
            string nome = "";
            // Verifique se os campos de e-mail e senha não estão vazios
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(senha))

            {
                MessageBox.Show("Por favor, preencha todos os campos.");
                return;
            }
            try
            {
                // Abra a conexão
                conexao.Open();

                // Consulta SQL para verificar o login como funcionario
                string queryFuncionario = $"SELECT idFuncionario FROM mydb.funcionario WHERE email='{email}' AND senha='{senha}'";
                MySqlCommand cmdFuncionario = new MySqlCommand(queryFuncionario, conexao);

                // Execute a consulta e obtenha o resultado como funcionario
                object resultFuncionario = cmdFuncionario.ExecuteScalar();

                // Verifique se o login como funcionario foi bem-sucedido
                if (resultFuncionario != null)
                {
                    userID = Convert.ToInt32(resultFuncionario);
                    Console.WriteLine("Id Utilizador do Login:" + userID);
                    // Verifica se o email é do administrador
                    if (email.Equals("Admin01@gmail.com", StringComparison.OrdinalIgnoreCase))
                    {
                        MessageBox.Show("Login bem-sucedido como administrador!");

                        // link pra outra tela
                        admin admin = new admin();
                        admin.Show();

                        // Feche este formulário de login 
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Login bem-sucedido como funcionario!");

                        // link pra outra tela para funcionario (replace 'YourFuncionarioForm' with the actual form for funcionario)
                        Funcionario_Pagina Funcionario_Pagina = new Funcionario_Pagina(userID);
                        Funcionario_Pagina.Show();

                        // Feche este formulário de login 
                        this.Hide();
                    }
                }
                else
                {
                    // If login as funcionario fails, check in the "medico" table
                    string queryidMedico = $"SELECT idMedico FROM mydb.medico WHERE email='{email}' AND password='{senha}'";
                    MySqlCommand cmdidMedico = new MySqlCommand(queryidMedico, conexao);

                    object resultidMedico = cmdidMedico.ExecuteScalar();
                    string querynomeMedico = $"SELECT nome FROM mydb.medico WHERE email='{email}' AND password='{senha}'";
                    MySqlCommand cmdnomeMedico = new MySqlCommand(querynomeMedico, conexao);

                    object resultnomeMedico = cmdnomeMedico.ExecuteScalar();

                    // Check if login as medico is successful
                    if (resultidMedico != null)
                    {
                        nome = Convert.ToString(resultnomeMedico);
                        userID = Convert.ToInt32(resultidMedico);
                        Console.WriteLine("idMedico" + userID);
                        MessageBox.Show("Login bem-sucedido como medico!");
                        Console.WriteLine(nome + userID);
                      
                        Medico_Pagina Medico_Pagina = new Medico_Pagina(userID, nome);
                        Medico_Pagina.Show();

                        // Feche este formulário de login 
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Login falhou. Verifique suas credenciais.");
                    }

                }


            }

            catch (Exception ex)
            {
                MessageBox.Show("Erro ao autenticar: " + ex.Message);
                Console.WriteLine(ex.Message);
                Console.WriteLine(Convert.ToString(ex));
            }
            finally
            {
                // Feche a conexão, independentemente do resultado
                if (conexao.State == ConnectionState.Open)
                    conexao.Close();
            }

        }
    }
}
