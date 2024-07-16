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
    public partial class notificacao_func : UserControl
    {
        private MySqlConnection conexao;
        private int iduser;
        private int usertipo;
        private string usernome = "";
        private string assunto = "";
        private int idsend;
        private int control = 0;
        private string remetente = "";
        private int process = 0;
        public notificacao_func(int userid, int tipo, string nome)
        {
            InitializeComponent();
            conexao = new MySqlConnection(DatabaseConfig.ConnectionString);
            this.iduser = userid;
            this.usertipo = tipo;
            this.usernome = nome;

            display_data();
            dataGridView1.CellClick += dataGridView1_CellClick;
            dataGridView1.CellFormatting += DataGridView1_CellFormatting;
            dataGridView1.Columns["proccessada"].HeaderText = string.Empty;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Verifica se uma célula da linha foi clicada
            if (e.RowIndex >= 0)
            {
                int idnotificacaoSelecionado = 0;
                if (usertipo == 0)
                {
                    // Obtém o valor do idFuncionario da célula clicada
                    idnotificacaoSelecionado = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["idNotificacao_Func"].Value);
                    // Check if the processada column is currently 0
                    if (dataGridView1.Rows[e.RowIndex].Cells["proccessada"].Value.ToString() == "0")
                    {
                        // Update the database
                        conexao.Open();
                        MySqlCommand cmd = new MySqlCommand("UPDATE notificacao_func SET proccessada = 1 WHERE idNotificacao_Func = @id", conexao);
                        cmd.Parameters.AddWithValue("@id", idnotificacaoSelecionado);
                        cmd.ExecuteNonQuery();
                        conexao.Close();

                        dataGridView1.Rows[e.RowIndex].Cells["proccessada"].Value = 1;
                        dataGridView1.Rows[e.RowIndex].Cells["proccessada"].Style.ForeColor = Color.Green;

                    }
                }
                if (usertipo == 1)
                {
                    // Obtém o valor do idFuncionario da célula clicada
                    idnotificacaoSelecionado = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["idnotificacao_medico"].Value);
                    // Check if the processada column is currently 0
                    if (dataGridView1.Rows[e.RowIndex].Cells["proccessada"].Value.ToString() == "0")
                    {
                        // Update the database
                        conexao.Open();
                        MySqlCommand cmd = new MySqlCommand("UPDATE notificacao_medico SET proccessada = 1 WHERE idnotificacao_medico = @id", conexao);
                        cmd.Parameters.AddWithValue("@id", idnotificacaoSelecionado);
                        cmd.ExecuteNonQuery();
                        conexao.Close();

                        dataGridView1.Rows[e.RowIndex].Cells["proccessada"].Value = 1;
                        dataGridView1.Rows[e.RowIndex].Cells["proccessada"].Style.ForeColor = Color.Green;

                    }
                }

                Console.WriteLine("rowcount:" + Convert.ToInt32(dataGridView1.SelectedRows.Count));
                Console.WriteLine("id:" + Convert.ToInt32(idnotificacaoSelecionado));

                // Obtém os dados do funcionário com base no idFuncionario
                ExibirDadosFuncionario(idnotificacaoSelecionado);
            }

        }
        private void ExibirDadosFuncionario(int idnotificacao)
        {
            conexao.Open();

            MySqlCommand cmd = conexao.CreateCommand();
            if (usertipo == 0)
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from notificacao_func where idNotificacao_Func = @idNotificacao_Func";
                cmd.Parameters.AddWithValue("@idNotificacao_Func", idnotificacao);

                MySqlDataReader reader = cmd.ExecuteReader();

                // Verifica se há dados a serem lidos
                if (reader.Read())
                {
                    // Exibe os dados nos TextBoxes
                    textBox1.Text = reader["assunto"].ToString();
                    textBox2.Text = reader["messagem"].ToString().Replace("\n", Environment.NewLine);
                    Console.WriteLine(reader["messagem"].ToString());

                }
            }
            if (usertipo == 1)
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from notificacao_medico where idnotificacao_medico = @idnotificacao_medico";
                cmd.Parameters.AddWithValue("@idnotificacao_medico", idnotificacao);

                MySqlDataReader reader = cmd.ExecuteReader();

                // Verifica se há dados a serem lidos
                if (reader.Read())
                {
                    // Exibe os dados nos TextBoxes
                    textBox1.Text = reader["assunto"].ToString();
                    textBox2.Text = reader["messagem"].ToString().Replace("\n", Environment.NewLine);
                    Console.WriteLine(reader["messagem"].ToString());

                }
            }


            conexao.Close();
        }
        private void display_data()
        {
            conexao.Open();
            MySqlCommand cmd = conexao.CreateCommand();
            if (usertipo == 0)
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from notificacao_func where funcionario_idFuncionario = @funcionario_idFuncionario";
                cmd.Parameters.AddWithValue("@funcionario_idFuncionario", iduser);
                cmd.ExecuteNonQuery();
                DataTable dta = new DataTable();
                MySqlDataAdapter dataadapter = new MySqlDataAdapter(cmd);
                dataadapter.Fill(dta);
                dataGridView1.DataSource = dta;
                dataGridView1.Columns["messagem"].Visible = false;
                dataGridView1.Columns["idremetente"].Visible = false;
                dataGridView1.Columns["funcionario_idFuncionario"].Visible = false;
                dataGridView1.Columns["idNotificacao_Func"].Visible = false;
            }
            if (usertipo == 1)
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM notificacao_medico WHERE idmedico = @idmedico AND remetente <> @nome;";
                cmd.Parameters.AddWithValue("@idmedico", iduser);
                cmd.Parameters.AddWithValue("@nome", usernome);

                cmd.ExecuteNonQuery();
                DataTable dta = new DataTable();
                MySqlDataAdapter dataadapter = new MySqlDataAdapter(cmd);
                dataadapter.Fill(dta);
                dataGridView1.DataSource = dta;
                dataGridView1.Columns["messagem"].Visible = false;
                dataGridView1.Columns["idFunc"].Visible = false;
                dataGridView1.Columns["idmedico"].Visible = false;
                dataGridView1.Columns["idnotificacao_medico"].Visible = false;
            }


            conexao.Close();
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
        private void button3_Click(object sender, EventArgs e)
        {
            display_data();
        }
    }
}
