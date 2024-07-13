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
        private int type = 0;

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
        private void ShowMessageBox(string message)
        {
            Point middle = new Point(this.Location.X + (this.Width - 200) / 2, this.Location.Y + (this.Height - 100) / 2);
            Mensagem mensagem = new Mensagem(message, middle);
            mensagem.ShowDialog();
        }
        private async void button1_Click(object sender, EventArgs e)
        {
            string email = textBox1.Text;
            string senha = textBox2.Text;
            int userID;
            string nome = "";

            // Verifique se os campos de e-mail e senha não estão vazios
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(senha))
            {
                ShowMessageBox("Por favor, preencha todos os campos.");
                return;
            }

            Point middle = new Point(this.Location.X + (this.Width - 200) / 2, this.Location.Y + (this.Height - 100) / 2);

            Loading loadingForm = new Loading(middle);
            loadingForm.Show();

            try
            {
                // Perform the login check in a separate task
                await Task.Run(() =>
                {
                    // Open the connection
                    conexao.Open();

                    // Check login as Funcionario
                    string queryFuncionario = $"SELECT idFuncionario FROM mydb.funcionario WHERE email='{email}' AND senha='{senha}'";
                    MySqlCommand cmdFuncionario = new MySqlCommand(queryFuncionario, conexao);
                    object resultFuncionario = cmdFuncionario.ExecuteScalar();

                    if (resultFuncionario != null)
                    {
                        userID = Convert.ToInt32(resultFuncionario);
                        if (email.Equals("Admin01@gmail.com", StringComparison.OrdinalIgnoreCase))
                        {
                            Invoke((Action)(() =>
                            {
                                type = 0;
                                admin admin = new admin();
                                admin.Show();
                                this.Hide();
                            }));
                        }
                        else
                        {
                            Invoke((Action)(() =>
                            {
                                type = 1;
                                Funcionario_Pagina Funcionario_Pagina = new Funcionario_Pagina(userID);
                                Funcionario_Pagina.Show();
                                this.Hide();
                            }));
                        }
                    }
                    else
                    {
                        // Check login as Medico
                        string queryidMedico = $"SELECT idMedico FROM mydb.medico WHERE email='{email}' AND password='{senha}'";
                        MySqlCommand cmdidMedico = new MySqlCommand(queryidMedico, conexao);
                        object resultidMedico = cmdidMedico.ExecuteScalar();
                        string querynomeMedico = $"SELECT nome FROM mydb.medico WHERE email='{email}' AND password='{senha}'";
                        MySqlCommand cmdnomeMedico = new MySqlCommand(querynomeMedico, conexao);
                        object resultnomeMedico = cmdnomeMedico.ExecuteScalar();

                        if (resultidMedico != null)
                        {
                            nome = Convert.ToString(resultnomeMedico);
                            userID = Convert.ToInt32(resultidMedico);
                            Invoke((Action)(() =>
                            {
                                type = 2;
                                Medico_Pagina Medico_Pagina = new Medico_Pagina(userID, nome);
                                Medico_Pagina.Show();
                                this.Hide();
                            }));
                        }
                        else
                        {
                            type = 3;
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                ShowMessageBox("Erro ao autenticar: " + ex.Message);
            }
            finally
            {
                // Close the connection and loading form
                if (conexao.State == ConnectionState.Open)
                    conexao.Close();

                loadingForm.Close();
                if(type == 0)
                    ShowMessageBox("Login bem-sucedido como administrador!");
                if(type == 1)
                    ShowMessageBox("Login bem-sucedido como funcionario!");
                if(type == 2)
                    ShowMessageBox("Login bem-sucedido como medico!");
                if (type == 3)
                    ShowMessageBox("Login falhou. Verifique suas credenciais.");


            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox2.PasswordChar == '*')
            {
                // Se a senha estiver oculta, mostre-a
                textBox2.PasswordChar = '\0'; // Caractere nulo para mostrar o texto da senha
                button2.Text = "Ocultar"; // Altera o texto do botão
            }
            else
            {
                // Se a senha estiver visível, oculte-a
                textBox2.PasswordChar = '*'; // Caractere '*' para ocultar a senha
                button2.Text = "Mostrar"; // Altera o texto do botão
            }
        }

       
    }
}
