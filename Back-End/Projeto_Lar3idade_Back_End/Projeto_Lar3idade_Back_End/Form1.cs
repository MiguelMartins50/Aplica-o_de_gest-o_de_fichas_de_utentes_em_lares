
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
    public partial class Form1 : Form
    {
        MySqlConnection conexao;

        private Rectangle logoOriginalRectangle;
        private Rectangle blueOriginalRectangle;
        private Rectangle logoinOriginalRectangle;
        private Rectangle Originalformsize;

        public Form1()
        {

            InitializeComponent();
            txtSenha.PasswordChar = '*';

            // Inicialize a conexão no construtor do formulário
            string connectionString = "Server=localhost;Port=3306;Database=mydb;User ID=root;Password=ipbcurso";
            conexao = new MySqlConnection(connectionString);

            string BluePath = System.IO.Path.Combine(Application.StartupPath, "azul2.png");
            panel1.BackgroundImage = Image.FromFile(BluePath);
            string logoPath = System.IO.Path.Combine(Application.StartupPath, "5857e68f1800001c00e435b3.jpeg");
            panel2.BackgroundImage = Image.FromFile(logoPath);
            panel1.BackgroundImageLayout = ImageLayout.Stretch;
            panel2.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text;
            string senha = txtSenha.Text;

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
                    admin_utentes admin = new admin_utentes();
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


        private void Form1_Load(object sender, EventArgs e)
        {
            Originalformsize = new Rectangle(this.Location.X, this.Location.Y, this.Width, this.Height);
            logoinOriginalRectangle = new Rectangle(buttonEntrar.Location.X, buttonEntrar.Location.Y, buttonEntrar.Width, buttonEntrar.Height);
            blueOriginalRectangle = new Rectangle(panel1.Location.X, panel1.Location.Y, panel1.Width, panel1.Height);
            logoOriginalRectangle = new Rectangle(panel2.Location.X, panel2.Location.Y, panel2.Width, panel2.Height);
        }

        private void Form1_Resize(object sender, EventArgs e)
        {

        }
    }

}