using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_Lar3idade_Back_End
{
    public partial class funcionario : UserControl
    {
        private MySqlConnection conexao;

        public funcionario()
        {
            InitializeComponent();
            string connectionString = "Server=localhost;Port=3306;Database=mydb;User ID=root;Password=ipbcurso";
            conexao = new MySqlConnection(connectionString);
            display_data();
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            conexao.Open();

            MySqlCommand cmd = conexao.CreateCommand();
            cmd.CommandType = CommandType.Text;

            // Obtém o último idFuncionario e soma 1
            cmd.CommandText = "INSERT INTO mydb.funcionario(idFuncionario, nome, numero_cc, data_validade, telemovel, salario_hora, email, senha) SELECT COALESCE(MAX(idFuncionario), 0) + 1, '" + textBox_Name.Text + "','" + textBox_Cc.Text + "','" + dateTimePicker_DtaValidade.Value.ToString("yyyy-MM-dd") + "', '" + textBox_Tel.Text + "','" + textBox_Salario.Text + "', '" + textBox_Email.Text + "', '" + textBox_Senha.Text + "' FROM mydb.funcionario";
            cmd.ExecuteNonQuery();
            conexao.Close();
            textBox_Name.Text = "";
            textBox_Cc.Text = "";
            dateTimePicker_DtaValidade.Text = "";
            textBox_Tel.Text = "";
            textBox_Salario.Text = "";
            textBox_Email.Text = "";
            textBox_Senha.Text = "";
            display_data();
            MessageBox.Show("Dados inseridos com sucesso");
        }
        private void display_data()
        {
            conexao.Open();
            MySqlCommand cmd = conexao.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Select * from mydb.funcionario";
            cmd.ExecuteNonQuery();
            DataTable dta = new DataTable();
            MySqlDataAdapter dataadapter = new MySqlDataAdapter(cmd);
            dataadapter.Fill(dta);
            dataGridView1.DataSource = dta;
            conexao.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            display_data();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                // Verifica se há uma linha selecionada no DataGridView
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    // Obtém o valor do idFuncionario da linha selecionada
                    int idFuncionarioParaExcluir = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["idFuncionario"].Value);

                    conexao.Open();

                    MySqlCommand cmd = conexao.CreateCommand();
                    cmd.CommandType = CommandType.Text;

                    // Utilizando parâmetros para prevenir injeção de SQL
                    cmd.CommandText = "DELETE FROM mydb.funcionario WHERE idFuncionario = @IdFuncionario";
                    cmd.Parameters.AddWithValue("@IdFuncionario", idFuncionarioParaExcluir);

                    // Executando o comando DELETE
                    cmd.ExecuteNonQuery();

                    conexao.Close();

                    // Atualizando a exibição dos dados no DataGridView
                    display_data();

                    MessageBox.Show("Dados Apagados com sucesso");
                }
                else
                {
                    MessageBox.Show("Por favor, selecione uma linha para apagar.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao apagar dados: " + ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                conexao.Open();

                MySqlCommand cmd = conexao.CreateCommand();
                cmd.CommandType = CommandType.Text;

                // Certifique-se de fornecer o valor do IdFuncionario a ser atualizado
                if (!string.IsNullOrEmpty(textBox_Name.Text) && !string.IsNullOrEmpty(textBox_Cc.Text))
                {
                    // Obtém o valor do idFuncionario dos TextBoxes
                    int idFuncionarioParaAtualizar = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["idFuncionario"].Value);

                    // Utilizando parâmetros para prevenir injeção de SQL
                    cmd.CommandText = "UPDATE mydb.funcionario SET nome = @Nome, numero_cc = @NumeroCC, data_validade = @DataValidade, telemovel = @Telemovel, salario_hora = @SalarioHora, email = @Email, senha = @Senha WHERE idFuncionario = @IdFuncionario";

                    // Adicionando parâmetros
                    cmd.Parameters.AddWithValue("@Nome", textBox_Name.Text);
                    cmd.Parameters.AddWithValue("@NumeroCC", textBox_Cc.Text);
                    cmd.Parameters.AddWithValue("@DataValidade", dateTimePicker_DtaValidade.Value.ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@Telemovel", textBox_Tel.Text);
                    cmd.Parameters.AddWithValue("@SalarioHora", textBox_Salario.Text);
                    cmd.Parameters.AddWithValue("@Email", textBox_Email.Text);
                    cmd.Parameters.AddWithValue("@Senha", textBox_Senha.Text);
                    cmd.Parameters.AddWithValue("@IdFuncionario", idFuncionarioParaAtualizar);

                    // Executando o comando
                    cmd.ExecuteNonQuery();
                    conexao.Close();

                    // Limpando os campos e atualizando a exibição dos dados
                    textBox_Name.Text = "";
                    textBox_Cc.Text = "";
                    dateTimePicker_DtaValidade.Text = "";
                    textBox_Tel.Text = "";
                    textBox_Salario.Text = "";
                    textBox_Email.Text = "";
                    textBox_Senha.Text = "";
                    display_data();

                    MessageBox.Show("Dados atualizados com sucesso");
                }
                else
                {
                    MessageBox.Show("Por favor, selecione uma linha para editar ou preencha os campos obrigatórios.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao atualizar dados: " + ex.Message);
            }
        }


        private void button4_Click(object sender, EventArgs e)
        {
            conexao.Open();

            MySqlCommand cmd = conexao.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM funcionario WHERE nome LIKE @searchText OR SUBSTRING_INDEX(nome, ' ', 1) LIKE @searchText";
            cmd.Parameters.AddWithValue("@searchText", "%" + textBox_Search.Text + "%");
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;

            conexao.Close();

            // Os campos de texto são limpos apenas se houver resultados da pesquisa
            if (dt.Rows.Count > 0)
            {
                textBox_Name.Text = "";
                textBox_Cc.Text = "";
                dateTimePicker_DtaValidade.Text = "";
                textBox_Tel.Text = "";
                textBox_Salario.Text = "";
                textBox_Email.Text = "";
                textBox_Senha.Text = "";
            }
            else
            {
                MessageBox.Show("Nenhum resultado encontrado.");
            }
        }
        private void ExibirDadosFuncionario(int idFuncionario)
        {
            conexao.Open();

            MySqlCommand cmd = conexao.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM mydb.funcionario WHERE idFuncionario = @IdFuncionario";
            cmd.Parameters.AddWithValue("@IdFuncionario", idFuncionario);

            MySqlDataReader reader = cmd.ExecuteReader();

            // Verifica se há dados a serem lidos
            if (reader.Read())
            {
                // Exibe os dados nos TextBoxes
                textBox_Name.Text = reader["nome"].ToString();
                textBox_Cc.Text = reader["numero_cc"].ToString();
                dateTimePicker_DtaValidade.Value = Convert.ToDateTime(reader["data_validade"]);
                textBox_Tel.Text = reader["telemovel"].ToString();
                textBox_Salario.Text = reader["salario_hora"].ToString();
                textBox_Email.Text = reader["email"].ToString();
                textBox_Senha.Text = reader["senha"].ToString();
            }

            conexao.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Verifica se uma célula da linha foi clicada
            if (e.RowIndex >= 0)
            {
                // Obtém o valor do idFuncionario da célula clicada
                int idFuncionarioSelecionado = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["idFuncionario"].Value);

                // Obtém os dados do funcionário com base no idFuncionario
                ExibirDadosFuncionario(idFuncionarioSelecionado);
            }
        }
    }
}
