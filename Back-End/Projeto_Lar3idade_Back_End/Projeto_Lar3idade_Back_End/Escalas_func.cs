using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using FluentEmail.Core;
using FluentEmail.Smtp;



namespace Projeto_Lar3idade_Back_End
{
    public partial class Escalas_func : UserControl
    {
        private MySqlConnection conexao;
        private int iduser;
        private DateTime data_inicio;
        private DateTime data_fim;
        private Dictionary<string, string> mes_begin = new Dictionary<string, string>();
        private Dictionary<string, string> mes_end = new Dictionary<string, string>();
        private Dictionary<string, string> func_id = new Dictionary<string, string>();
        private Dictionary<string, string> func_type = new Dictionary<string, string>();
        private int control = 0;
        
        private string tipo_func = "";
        private string nome_func = "";
        private string estado= "";
        private string mail = "";
        private string senha = "";
        private string nome = "";
        private int idescala;
       
        private DateTime Lastmonth;


        public Escalas_func( int userid)
        {
            InitializeComponent();
            string connectionString = "Server=localhost;Port=3306;Database=mydb;User ID=root;Password=ipbcurso";
            conexao = new MySqlConnection(connectionString);
            this.iduser = userid;
            comboBox3.Items.Add("folga");
            comboBox3.Items.Add("falta Justificada");
            Console.WriteLine("id Utlizador da Escalas:" + iduser);
            display_data();
            Loadcomboboxes();
            comboBox1.SelectedIndex = 0;
        }

        private void button2_Click(object sender, EventArgs e)
        {

            control = 0;
            display_data();
            comboBox1.Items.Clear();
            Loadcomboboxes();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Get the selected row
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                // Make sure to replace these values with your own email settings
                string smtpServer = "smtp-mail.outlook.com";
                int smtpPort = 587; // Replace with your SMTP port
                string smtpUsername = mail;
                string smtpPassword = senha;
                string dia = ((DateTime)selectedRow.Cells["Dia"].Value).ToString("dddd MMMM yyyy");
                string estadoAtual = selectedRow.Cells["estado"].Value.ToString();
                string recipientEmail = "a44610@alunos.ipb.pt";
                string subject = "Change of Estado Request";
                string body = $"Prezado Adminstrador,\n\nSolicito a alteração do estado da escala do dia {dia} de {estadoAtual} para {estado} \n\nAtenciosamente,\nSua {nome}";

                try
                {
                    using (MailMessage mail = new MailMessage(smtpUsername, recipientEmail, subject, body))
                    {
                        using (SmtpClient smtp = new SmtpClient(smtpServer, smtpPort))
                        {
                            smtp.UseDefaultCredentials = false;
                            smtp.EnableSsl = true;
                            smtp.Credentials = new NetworkCredential(smtpUsername, smtpPassword);
                            smtp.Send(mail);
                            MessageBox.Show("Email de pedido enviado", "Success");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro ao enviar email: {ex.Message}");
                }
            }
        }


        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox3.SelectedItem != null)
            {
                estado = comboBox3.SelectedItem.ToString();
            }

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (comboBox1.SelectedItem != null)
            {
                string selectedValue = comboBox1.SelectedItem.ToString();
                if (mes_begin.TryGetValue(selectedValue, out string id))
                {
                    idescala = Convert.ToInt32(id);
                    Console.WriteLine(data_inicio);
                    Console.WriteLine("IN_time");
                    display_data();
                    control = 1;

                }

            }
        }
        private void Loadcomboboxes()
        {
            if (conexao.State != ConnectionState.Open)
            {
                conexao.Open();
            }
            
                // Query for the first MySqlCommand
                string query1 = "SELECT * FROM escala_servico ORDER BY data_inicial;";
                Console.WriteLine("escalas");
                comboBox1.DataSource = null;
                using (MySqlCommand command1 = new MySqlCommand(query1, conexao))
                {
                    // Execute the first query
                    using (MySqlDataReader reader = command1.ExecuteReader())
                    {
                        // Create a list to store data
                        List<string[]> data = new List<string[]>();

                        // Iterate through the results
                        while (reader.Read())
                        {
                            // Add data to the list

                            data_inicio = Convert.ToDateTime(reader["data_inicial"]);
                            data_fim = Convert.ToDateTime(reader["data_final"]);
                            Lastmonth = Convert.ToDateTime(reader["data_final"]);
                            string id = Convert.ToString(reader["idEscala_servico"]);
                            Console.WriteLine(data_inicio);
                            Console.WriteLine(data_fim);
                            string month = data_inicio.ToString("MMMM yyyy", new System.Globalization.CultureInfo("pt-BR"));
                            comboBox1.Items.Add(data_inicio.ToString("MMMM yyyy", new System.Globalization.CultureInfo("pt-BR")));
                            mes_begin[month] = id;

                        }
                    }
                }
            
        }
        private void display_data()
        {
            if (conexao.State != ConnectionState.Open)
            {
                conexao.Open();
            }
            string query2 = "select nome,email,senha from funcionario where idFuncionario = @idFuncionario;";

            using (MySqlCommand procuraremail = new MySqlCommand(query2, conexao))
            {
                procuraremail.Parameters.AddWithValue("@idFuncionario", iduser);

                using (MySqlDataReader reader = procuraremail.ExecuteReader())
                {
                    // Create a list to store data
                    List<string[]> data = new List<string[]>();

                    // Iterate through the results
                    while (reader.Read())
                    {
                        mail = Convert.ToString(reader["email"]);
                        senha = Convert.ToString(reader["senha"]);
                        nome = Convert.ToString(reader["nome"]);
                    }
                }
            }
            MySqlCommand cmd = conexao.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT idFuncionario_Escala,Dia, dia_da_semana, horario_inicio, horario_fim, estado FROM funcionario_escala WHERE Funcionario_idFuncionario = @funcionario AND Escala_servico_idEscala_servico = @idescala;";
            cmd.Parameters.AddWithValue("@funcionario", iduser);
            cmd.Parameters.AddWithValue("@idescala", idescala);
            Console.WriteLine("IN_FUNC");
            
           

            cmd.ExecuteNonQuery();
            DataTable dta = new DataTable();
            MySqlDataAdapter dataadapter = new MySqlDataAdapter(cmd);
            dataadapter.Fill(dta);
            dataGridView1.DataSource = dta;
        }
        
    }
}
