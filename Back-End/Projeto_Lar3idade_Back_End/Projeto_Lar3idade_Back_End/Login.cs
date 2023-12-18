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
            string connectionString = "Server=localhost;Port=3306;Database=mydb;User ID=root;Password=ipbcurso";
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
                string queryFuncionario = $"SELECT COUNT(*) FROM mydb.funcionario WHERE email='{email}' AND senha='{senha}'";
                MySqlCommand cmdFuncionario = new MySqlCommand(queryFuncionario, conexao);

                // Execute a consulta e obtenha o resultado como funcionario
                int countFuncionario = Convert.ToInt32(cmdFuncionario.ExecuteScalar());

                // Verifique se o login como funcionario foi bem-sucedido
                if (countFuncionario > 0)
                {
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
                        Funcionario_Pagina Funcionario_Pagina = new Funcionario_Pagina();
                        Funcionario_Pagina.Show();

                        // Feche este formulário de login 
                        this.Hide();
                    }
                }
                else
                {
                    // If login as funcionario fails, check in the "medico" table
                    string queryMedico = $"SELECT COUNT(*) FROM mydb.medico WHERE email='{email}' AND senha='{senha}'";
                    MySqlCommand cmdMedico = new MySqlCommand(queryMedico, conexao);

                    int countMedico = Convert.ToInt32(cmdMedico.ExecuteScalar());

                    // Check if login as medico is successful
                    if (countMedico > 0)
                    {
                        MessageBox.Show("Login bem-sucedido como medico!");

                        // link pra outra tela para medico (replace 'YourMedicoForm' with the actual form for medico)
                        /*YourMedicoForm medicoForm = new YourMedicoForm();
                        medicoForm.Show();*/

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
