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
    public partial class enviar_medico : UserControl
    {
        private MySqlConnection conexao;
        private int iduser;
        private string remetente = "";
        public event EventHandler NavigateToEscalsFuncClicked;
        public enviar_medico(int userid, string usernome)
        {
            InitializeComponent();
            string connectionString = "Server=localhost;Port=3306;Database=mydb;User ID=root;Password=ipbcurso";
            conexao = new MySqlConnection(connectionString);
            this.iduser = userid;
            this.remetente = usernome;
        }


        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Tem a que querer enviar este pedido?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == DialogResult.OK)
            {
                try
                {
                    using (MySqlConnection conexao = new MySqlConnection("Server=localhost;Port=3306;Database=mydb;User ID=root;Password=ipbcurso"))
                    {
                        conexao.Open();
                        string query = "INSERT INTO mydb.notificacao_medico (remetente,assunto, messagem, idmedico, idFunc,proccessada,Data_envio)" +
                                        "VALUES (@remetente, @assunto, @messagem, @idmedico, @idFunc,@proccessada,@Data_envio)";
                        string assunto = textBox1.Text;
                        string mensagem = textBox2.Text;




                        // Crie um comando MySqlCommand
                        using (MySqlCommand comando = new MySqlCommand(query, conexao))
                        {
                            comando.Parameters.AddWithValue("@remetente", remetente);
                            comando.Parameters.AddWithValue("@assunto", assunto);
                            comando.Parameters.AddWithValue("@messagem", mensagem);
                            comando.Parameters.AddWithValue("@idmedico", iduser);
                            comando.Parameters.AddWithValue("@idFunc", 10);
                            comando.Parameters.AddWithValue("@proccessada", 0);
                            comando.Parameters.AddWithValue("@Data_envio", DateTime.Now);



                            comando.ExecuteNonQuery();

                            MessageBox.Show("Resposta enviada com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);



                        }
                        textBox1.Text = "";
                        textBox2.Text = "";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao enviar notificação: " + ex.Message);
                }
                NavigateToEscalsFuncClicked?.Invoke(this, EventArgs.Empty);

            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            NavigateToEscalsFuncClicked?.Invoke(this, EventArgs.Empty);
        }
    }
}
