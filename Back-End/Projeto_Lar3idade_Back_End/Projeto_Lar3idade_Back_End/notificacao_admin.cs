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
    public partial class notificacao_admin : UserControl
    {
        private MySqlConnection conexao;
        private int idupt;
        private int iduser;
        private string assunto = "";
        private int idsend;
        private int control = 0;
        private string remetente = "";
        private int process = 0;
        private string tipo = "";
        private int id_notificacaoo = 0;



        public notificacao_admin()
        {
            InitializeComponent();
            string connectionString = "Server=localhost;Port=3306;Database=mydb;User ID=root;Password=ipbcurso";
            conexao = new MySqlConnection(connectionString);
            display_data();
            dataGridView1.CellClick += dataGridView1_CellClick;
            dataGridView1.CellFormatting += DataGridView1_CellFormatting;
            dataGridView1.Columns["proccessada"].HeaderText = string.Empty;

        }
        private void DataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["proccessada"].Index && e.Value != null)
            {
                int processadaValue = Convert.ToInt32(e.Value);
                if (processadaValue == 0)
                {
                    e.Value = "❌";
                    e.CellStyle.ForeColor = Color.Red;
                }
                else if (processadaValue == 1)
                {
                    e.Value = "✔️";
                    e.CellStyle.ForeColor = Color.Green;
                }
            }
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Verifica se uma célula da linha foi clicada
            if (e.RowIndex >= 0)
            {
                
                // Obtém o valor do idFuncionario da célula clicada
                idupt = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["idnotificacao"].Value);
                assunto = Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells["assunto"].Value);
                idsend = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["idremetente"].Value);
                remetente = Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells["remetente"].Value);
                tipo = Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells["tipo"].Value);
                process = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["proccessada"].Value);

                Console.WriteLine("rowcount:" + Convert.ToInt32(dataGridView1.SelectedRows.Count));
                Console.WriteLine("id:" + Convert.ToInt32(idupt));
                control = 1;
                Console.WriteLine("control:" + control);
                Console.WriteLine("process:" + process);
                // Obtém os dados do funcionário com base no idFuncionario
                ExibirDadosFuncionario(idupt);
                conexao.Open();
                if (dataGridView1.CurrentRow.Cells["proccessada"].Value.ToString() == "0")
                {
                    // Your existing code to update the database goes here

                    // Update the database
                    if (tipo == "func")
                    {
                        MySqlCommand cmd = new MySqlCommand("UPDATE notificacao_func SET proccessada = 1 WHERE idNotificacao_Func = @id", conexao);
                        cmd.Parameters.AddWithValue("@id", idupt);
                        cmd.ExecuteNonQuery();
                    }
                    else if (tipo == "medico")
                    {
                        MySqlCommand cmd = new MySqlCommand("UPDATE notificacao_medico SET proccessada = 1 WHERE idnotificacao_medico = @id", conexao);
                        cmd.Parameters.AddWithValue("@id", idupt);
                        cmd.ExecuteNonQuery();
                    }
                    conexao.Close();
                    // Update the data source and refresh the DataGridView
                    DataGridViewRow selectedRow = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex];
                    selectedRow.Cells["proccessada"].Value = 1;
                    selectedRow.Cells["proccessada"].Style.ForeColor = Color.Green;
                }
            }

        }
        private void ExibirDadosFuncionario(int idnotificacao)
        {
            conexao.Open();
            MySqlCommand cmd = conexao.CreateCommand();

            if (tipo == "func")
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from notificacao_func where idNotificacao_Func = @idNotificacao_Func";
                cmd.Parameters.AddWithValue("@idNotificacao_Func", idnotificacao);
            }
            if (tipo == "medico")
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from notificacao_medico where idnotificacao_medico = @idnotificacao_medico";
                cmd.Parameters.AddWithValue("@idnotificacao_medico", idnotificacao);
            }


            MySqlDataReader reader = cmd.ExecuteReader();

            // Verifica se há dados a serem lidos
            if (reader.Read())
            {
                // Exibe os dados nos TextBoxes
                textBox1.Text = reader["assunto"].ToString();
                textBox2.Text = reader["messagem"].ToString().Replace("\n", Environment.NewLine);

            }

            conexao.Close();
        }
        private void display_data()
        {
            conexao.Open();
            MySqlCommand cmd = conexao.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = @"SELECT tipo, idnotificacao, funcionario_idFuncionario, idremetente, remetente, assunto, messagem, proccessada, data_envio
                                FROM (
                                SELECT 'func' AS tipo, idnotificacao_func AS idnotificacao, funcionario_idFuncionario, idremetente, remetente, assunto, messagem, proccessada, data_envio
                                FROM notificacao_func 
                                WHERE funcionario_idFuncionario = 10

                                UNION

                                SELECT 'medico' AS tipo, idnotificacao_medico AS idnotificacao, idFunc AS funcionario_idFuncionario, idmedico AS idremetente, remetente, assunto, messagem, proccessada, data_envio
                                FROM notificacao_medico 
                                WHERE remetente <> 'Admin'
                                ) AS resultado
                                ORDER BY data_envio;";

            cmd.Parameters.AddWithValue("@funcionario_idFuncionario", 10);
            cmd.ExecuteNonQuery();
            DataTable dta = new DataTable();
            MySqlDataAdapter dataadapter = new MySqlDataAdapter(cmd);
            dataadapter.Fill(dta);



            dataGridView1.DataSource = dta;
            dataGridView1.Columns["messagem"].Visible = false;
            dataGridView1.Columns["idremetente"].Visible = false;
            dataGridView1.Columns["funcionario_idFuncionario"].Visible = false;
            dataGridView1.Columns["tipo"].Visible = false;
            dataGridView1.Columns["idnotificacao"].Visible = false;
            dataGridView1.Columns["data_envio"].HeaderText = "Data";




            conexao.Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Console.WriteLine("control:" + control);
            Console.WriteLine("process:" + process);

            if (control == 1 && process == 0)
            {
                try
                {
                    using (MySqlConnection conexao = new MySqlConnection("Server=localhost;Port=3306;Database=mydb;User ID=root;Password=ipbcurso"))
                    {
                        Console.WriteLine("aqui1");
                        conexao.Open();
                        string query = "INSERT INTO mydb.notificacao_func (remetente,assunto, messagem, idremetente, funcionario_idFuncionario,proccessada,Data_envio)" +
                                        "VALUES (@remetente, @assunto, @messagem, @idremetente, @funcionario_idFuncionario,@proccessada,@Data_envio)";
                        string query2 = "INSERT INTO mydb.notificacao_medico (remetente,assunto, messagem, idmedico, idFunc,proccessada,Data_envio)" +
                                        "VALUES (@remetente, @assunto, @messagem, @idmedico, @idFunc,@proccessada,@Data_envio)";
                        string mensagem = "Caro " + remetente + ",\n\n Agradecemos por compartilhar suas necessidades conosco.Fico feliz em informar que sua solicitação de alteração das Escalas foi aprovada.Por favor, confirme se está tudo em ordem e se há mais alguma coisa que possamos fazer para auxiliá-lo.";
                        Console.WriteLine("aqui2");


                        // Crie um comando MySqlCommand
                        if (tipo == "func")
                            using (MySqlCommand comando = new MySqlCommand(query, conexao))
                            {
                                Console.WriteLine("aqui3");
                                comando.Parameters.AddWithValue("@remetente", "Admin");
                                comando.Parameters.AddWithValue("@assunto", assunto + "- Resposta");
                                comando.Parameters.AddWithValue("@messagem", mensagem);
                                comando.Parameters.AddWithValue("@idremetente", 10);
                                comando.Parameters.AddWithValue("@funcionario_idFuncionario", idsend);
                                comando.Parameters.AddWithValue("@proccessada", 0);
                                comando.Parameters.AddWithValue("@Data_envio", DateTime.Now);



                                comando.ExecuteNonQuery();

                                MessageBox.Show("Resposta enviada com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                control = 0;



                            }
                        if (tipo == "medico")
                            using (MySqlCommand comando = new MySqlCommand(query2, conexao))
                            {
                                Console.WriteLine("aqui4");
                                comando.Parameters.AddWithValue("@remetente", "Admin");
                                comando.Parameters.AddWithValue("@assunto", assunto + "- Resposta");
                                comando.Parameters.AddWithValue("@messagem", mensagem);
                                comando.Parameters.AddWithValue("@idFunc", 10);
                                comando.Parameters.AddWithValue("@idmedico", idsend);
                                comando.Parameters.AddWithValue("@proccessada", 0);
                                comando.Parameters.AddWithValue("@Data_envio", DateTime.Now);


                                comando.ExecuteNonQuery();

                                MessageBox.Show("Resposta enviada com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                control = 0;



                            }
                        Console.WriteLine("aqui3.5");

                        if (dataGridView1.CurrentRow.Cells["proccessada"].Value.ToString() == "0")
                        {
                            Console.WriteLine("aqui5");
                            if (tipo == "func")
                            {
                                Console.WriteLine("aqui6");
                                MySqlCommand cmd = new MySqlCommand("UPDATE notificacao_func SET proccessada = 1 WHERE idNotificacao_Func = @id", conexao);
                                cmd.Parameters.AddWithValue("@id", idupt);
                                cmd.ExecuteNonQuery();
                                conexao.Close();

                            }
                            if (tipo == "medico")
                            {
                                Console.WriteLine("aqui7");

                                MySqlCommand cmd = new MySqlCommand("UPDATE notificacao_medico SET proccessada = 1 WHERE idnotificacao_medico = @id", conexao);
                                cmd.Parameters.AddWithValue("@id", idupt);
                                cmd.ExecuteNonQuery();
                                conexao.Close();
                            }
                            // Update the database
                            Console.WriteLine("aqui8");

                            dataGridView1.CurrentRow.Cells["proccessada"].Value = 1;
                            dataGridView1.CurrentRow.Cells["proccessada"].Style.ForeColor = Color.Green;
                            process = Convert.ToInt32(dataGridView1.CurrentRow.Cells["proccessada"].Value);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao enviar notificação: " + ex.Message);
                    Console.WriteLine(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Nao tem uma notificação selecionada ou a notificação que escolheu ja foi processada");
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Console.WriteLine("control:" + control);
            Console.WriteLine("process:" + process);
            if (control == 1 && process == 0)
            {
                try
                {
                    using (MySqlConnection conexao = new MySqlConnection("Server=localhost;Port=3306;Database=mydb;User ID=root;Password=ipbcurso"))
                    {
                        conexao.Open();
                        string query = "INSERT INTO mydb.notificacao_func (remetente,assunto, messagem, idremetente, funcionario_idFuncionario,proccessada,Data_envio)" +
                                        "VALUES (@remetente, @assunto, @messagem, @idremetente, @funcionario_idFuncionario,@proccessada,@Data_envio)";
                        string query2 = "INSERT INTO mydb.notificacao_medico (remetente,assunto, messagem, idmedico, idFunc,proccessada,Data_envio)" +
                                        "VALUES (@remetente, @assunto, @messagem, @idmedico, @idFunc,@proccessada,@Data_envio)";
                        string mensagem = "Caro " + remetente + ",\n\n Agradecemos por compartilhar suas preocupações conosco. Compreendemos a importância de um horário flexível para atender às suas necessidades pessoais. No entanto, neste momento, não podemos atender ao seu pedido de alteração das Escalas.";


                        // Crie um comando MySqlCommand
                        if (tipo == "func")
                            using (MySqlCommand comando = new MySqlCommand(query, conexao))
                            {
                                comando.Parameters.AddWithValue("@remetente", "Adimin");
                                comando.Parameters.AddWithValue("@assunto", assunto + "- Resposta");
                                comando.Parameters.AddWithValue("@messagem", mensagem);
                                comando.Parameters.AddWithValue("@idremetente", 10);
                                comando.Parameters.AddWithValue("@funcionario_idFuncionario", idsend);
                                comando.Parameters.AddWithValue("@proccessada", 0);
                                comando.Parameters.AddWithValue("@Data_envio", DateTime.Now);


                                comando.ExecuteNonQuery();

                                MessageBox.Show("Resposta enviada com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                control = 0;



                            }
                        if (tipo == "medico")
                            using (MySqlCommand comando = new MySqlCommand(query2, conexao))
                            {
                                comando.Parameters.AddWithValue("@remetente", "Admin");
                                comando.Parameters.AddWithValue("@assunto", assunto + "- Resposta");
                                comando.Parameters.AddWithValue("@messagem", mensagem);
                                comando.Parameters.AddWithValue("@idFunc", 10);
                                comando.Parameters.AddWithValue("@idmedico", idsend);
                                comando.Parameters.AddWithValue("@proccessada", 0);
                                comando.Parameters.AddWithValue("@Data_envio", DateTime.Now);



                                comando.ExecuteNonQuery();

                                MessageBox.Show("Resposta enviada com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                control = 0;



                            }
                        if (dataGridView1.CurrentRow.Cells["proccessada"].Value.ToString() == "0")
                        {
                            Console.WriteLine("aqui5");
                            if (tipo == "func")
                            {
                                Console.WriteLine("aqui6");
                                MySqlCommand cmd = new MySqlCommand("UPDATE notificacao_func SET proccessada = 1 WHERE idNotificacao_Func = @id", conexao);
                                cmd.Parameters.AddWithValue("@id", idupt);
                                cmd.ExecuteNonQuery();
                                conexao.Close();
                            }
                            if (tipo == "medico")
                            {
                                Console.WriteLine("aqui7");

                                MySqlCommand cmd = new MySqlCommand("UPDATE notificacao_medico SET proccessada = 1 WHERE idnotificacao_medico = @id", conexao);
                                cmd.Parameters.AddWithValue("@id", idupt);
                                cmd.ExecuteNonQuery();
                                conexao.Close();

                            }
                            // Update the database
                            Console.WriteLine("aqui8");

                            dataGridView1.CurrentRow.Cells["proccessada"].Value = 1;
                            dataGridView1.CurrentRow.Cells["proccessada"].Style.ForeColor = Color.Green;
                            process = Convert.ToInt32(dataGridView1.CurrentRow.Cells["proccessada"].Value);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao enviar notificação: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Nao tem uma notificação selecionada ou a notificação que escolheu ja foi processada");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            display_data();
        }

        
    }
}
