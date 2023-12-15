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

                // Consulta SQL para verificar o login
                string query = $"SELECT COUNT(*) FROM mydb.funcionario WHERE email='{email}' AND senha='{senha}'";
                MySqlCommand cmd = new MySqlCommand(query, conexao);

                // Execute a consulta e obtenha o resultado
                int count = Convert.ToInt32(cmd.ExecuteScalar());

                // Verifique se o login foi bem-sucedido
                if (count > 0)
                    // Verifica se o email é do administrador
                    if (email.Equals("Admin01@gmail.com", StringComparison.OrdinalIgnoreCase))
                    {
                        MessageBox.Show("Login bem-sucedido!");

                        // link pra outra tela

                        admin admin = new admin();
                        admin.Show();

                        // Feche este formulário de login 
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Login falhou. Verifique suas credenciais.");
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
