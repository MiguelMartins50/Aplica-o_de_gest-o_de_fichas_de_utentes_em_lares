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
        private int idupt;
        private string funcao = "";
        public funcionario()
        {
            InitializeComponent();

            comboBox1.Items.AddRange(new string[] { "Médico(a)", "Cuidador(a)", "Recepcionista" });

            string connectionString = "Server=localhost;Port=3306;Database=mydb;User ID=root;Password=ipbcurso";
            conexao = new MySqlConnection(connectionString);
            display_data();
            dataGridView1.CellClick += dataGridView1_CellClick;

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            conexao.Open();

            MySqlCommand cmd = conexao.CreateCommand();
            cmd.CommandType = CommandType.Text;

            if (funcao == "Médico(a)")
            {
                // Get the current maximum idMedico from the medico table
                MySqlCommand getMaxIdCmd = new MySqlCommand("SELECT COALESCE(MAX(idMedico), 0) + 1 FROM mydb.medico", conexao);
                int newMedicoId = Convert.ToInt32(getMaxIdCmd.ExecuteScalar());

                // Use the fetched ID for the new insertion
                cmd.CommandText = "INSERT INTO mydb.medico(idMedico, nome, numero_cc, Data_validade, telemovel, salario_hora, email, password) VALUES (@idMedico, @nome, @numero_cc, @Data_validade, @telemovel, @salario_hora, @email, @senha)";
                cmd.Parameters.AddWithValue("@idMedico", newMedicoId);
            }
            else
            {
                // Get the current maximum idFuncionario from the funcionario table
                MySqlCommand getMaxIdCmd = new MySqlCommand("SELECT COALESCE(MAX(idFuncionario), 0) + 1 FROM mydb.funcionario", conexao);
                int newFuncionarioId = Convert.ToInt32(getMaxIdCmd.ExecuteScalar());

                // Use the fetched ID for the new insertion
                cmd.CommandText = "INSERT INTO mydb.funcionario(idFuncionario, nome, numero_cc, data_validade, telemovel, salario_hora, email, senha, funcao) VALUES (@idFuncionario, @nome, @numero_cc, @data_validade, @telemovel, @salario_hora, @email, @senha, @funcao)";
                cmd.Parameters.AddWithValue("@idFuncionario", newFuncionarioId);
            }

            // Add parameters to avoid SQL Injection and problems with special characters
            cmd.Parameters.AddWithValue("@nome", textBox_Name.Text);
            cmd.Parameters.AddWithValue("@numero_cc", textBox_Cc.Text);
            cmd.Parameters.AddWithValue("@Data_validade", dateTimePicker_DtaValidade.Value);
            cmd.Parameters.AddWithValue("@telemovel", textBox_Tel.Text);
            cmd.Parameters.AddWithValue("@salario_hora", textBox_Salario.Text);
            cmd.Parameters.AddWithValue("@email", textBox_Email.Text);
            cmd.Parameters.AddWithValue("@senha", textBox_Senha.Text);
            cmd.Parameters.AddWithValue("@funcao", comboBox1.Text);

            cmd.ExecuteNonQuery();

            conexao.Close();
            textBox_Name.Text = "";
            textBox_Cc.Text = "";
            dateTimePicker_DtaValidade.Text = "";
            textBox_Tel.Text = "";
            textBox_Salario.Text = "";
            textBox_Email.Text = "";
            comboBox1.SelectedIndex = -1;

            display_data();
            LimparTextBoxes();

            MessageBox.Show("Funcionario adicionado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void display_data()
        {
            conexao.Open();
            MySqlCommand cmd = conexao.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = @"SELECT idFuncionario, nome, numero_cc, data_validade, telemovel, salario_hora, email, senha, Funcao
                              FROM (
                              SELECT idFuncionario, nome, numero_cc, data_validade, telemovel, salario_hora, email,senha,Funcao
                              FROM funcionario 

                              UNION                                 
                              
                              SELECT idMedico AS idFuncionario, nome , numero_cc , Data_validade AS data_validade, telemovel, salario_hora, email, password as senha,'Médico(a)' as Funcao
                              FROM medico 
    
                              ) AS resultado
                              Order by idFuncionario;";
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
            LimparTextBoxes();
            comboBox1.SelectedIndex = -1;
        }
        private void LimparTextBoxes()
        {
            // Limpar todos os controles TextBox e outros controles conforme necessário
            foreach (Control control in Controls)
            {
                if (control is TextBox textBox)
                {
                    textBox.Clear();
                }
            }
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
                    if (funcao == "Médico(a)")
                    {
                        cmd.CommandText = "DELETE FROM mydb.medico WHERE idMedico = @idMedico";
                        cmd.Parameters.AddWithValue("@idMedico", idFuncionarioParaExcluir);
                    }
                    else
                    {
                        cmd.CommandText = "DELETE FROM mydb.funcionario WHERE idFuncionario = @IdFuncionario";
                        cmd.Parameters.AddWithValue("@IdFuncionario", idFuncionarioParaExcluir);
                    }


                    // Executando o comando DELETE
                    cmd.ExecuteNonQuery();

                    conexao.Close();

                    // Atualizando a exibição dos dados no DataGridView
                    display_data();
                    LimparTextBoxes();

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
                    // Utilizando parâmetros para prevenir injeção de SQL
                    if (funcao == "Médico(a)")
                    {
                        cmd.CommandText = "UPDATE mydb.medico SET nome = @Nome, numero_cc = @NumeroCC, Data_validade = @DataValidade, telemovel = @Telemovel, salario_hora = @SalarioHora, email = @Email, password = @Senha WHERE idMedico = @idMedico";

                        // Adicionando parâmetros
                        cmd.Parameters.AddWithValue("@Nome", textBox_Name.Text);
                        cmd.Parameters.AddWithValue("@NumeroCC", textBox_Cc.Text);
                        cmd.Parameters.AddWithValue("@DataValidade", dateTimePicker_DtaValidade.Value.ToString("yyyy-MM-dd"));
                        cmd.Parameters.AddWithValue("@Telemovel", textBox_Tel.Text);
                        cmd.Parameters.AddWithValue("@SalarioHora", textBox_Salario.Text);
                        cmd.Parameters.AddWithValue("@Email", textBox_Email.Text);
                        cmd.Parameters.AddWithValue("@Senha", textBox_Senha.Text);
                        cmd.Parameters.AddWithValue("@idMedico", idupt);
                        cmd.Parameters.AddWithValue("@Funcao", comboBox1.SelectedItem.ToString());
                    }
                    else
                    {
                        cmd.CommandText = "UPDATE mydb.funcionario SET nome = @Nome, numero_cc = @NumeroCC, data_validade = @DataValidade, telemovel = @Telemovel, salario_hora = @SalarioHora, email = @Email, senha = @Senha,funcao = @Funcao WHERE idFuncionario = @IdFuncionario";

                        // Adicionando parâmetros
                        cmd.Parameters.AddWithValue("@Nome", textBox_Name.Text);
                        cmd.Parameters.AddWithValue("@NumeroCC", textBox_Cc.Text);
                        cmd.Parameters.AddWithValue("@DataValidade", dateTimePicker_DtaValidade.Value.ToString("yyyy-MM-dd"));
                        cmd.Parameters.AddWithValue("@Telemovel", textBox_Tel.Text);
                        cmd.Parameters.AddWithValue("@SalarioHora", textBox_Salario.Text);
                        cmd.Parameters.AddWithValue("@Email", textBox_Email.Text);
                        cmd.Parameters.AddWithValue("@Senha", textBox_Senha.Text);
                        cmd.Parameters.AddWithValue("@IdFuncionario", idupt);
                        cmd.Parameters.AddWithValue("@Funcao", comboBox1.SelectedItem.ToString());
                    }


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
                    comboBox1.SelectedIndex = -1;
                    display_data();
                    LimparTextBoxes();

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
            cmd.CommandText = @"SELECT idFuncionario, nome, numero_cc, data_validade, telemovel, salario_hora, email, senha, Funcao
                              FROM (
                              SELECT idFuncionario, nome, numero_cc, data_validade, telemovel, salario_hora, email,senha,Funcao
                              FROM funcionario 

                              UNION

                              SELECT idMedico AS idFuncionario, nome , numero_cc , Data_validade AS data_validade, telemovel, salario_hora, email, password as senha,'Médico(a)' as Funcao
                              FROM medico 
    
                              ) AS resultado
                              WHERE nome LIKE @searchText OR SUBSTRING_INDEX(nome, ' ', 1) LIKE @searchText
                              Order by idFuncionario";
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
                comboBox1.SelectedIndex = -1;
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
                comboBox1.Text = reader["funcao"].ToString();
            }

            conexao.Close();
        }
        private void ExibirDadosMedico(int idmedico)
        {
            conexao.Open();

            MySqlCommand cmd = conexao.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM mydb.medico WHERE idMedico = @idMedico";
            cmd.Parameters.AddWithValue("@idMedico", idmedico);

            MySqlDataReader reader = cmd.ExecuteReader();

            // Verifica se há dados a serem lidos
            if (reader.Read())
            {
                // Exibe os dados nos TextBoxes
                textBox_Name.Text = reader["nome"].ToString();
                textBox_Cc.Text = reader["numero_cc"].ToString();
                dateTimePicker_DtaValidade.Value = Convert.ToDateTime(reader["Data_validade"]);
                textBox_Tel.Text = reader["telemovel"].ToString();
                textBox_Salario.Text = reader["salario_hora"].ToString();
                textBox_Email.Text = reader["email"].ToString();
                textBox_Senha.Text = reader["password"].ToString();
                comboBox1.SelectedIndex = 0;
            }

            conexao.Close();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Verifica se uma célula da linha foi clicada
            if (e.RowIndex >= 0)
            {

                // Obtém o valor do idFuncionario da célula clicada
                int idFuncionarioSelecionado = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["idFuncionario"].Value);
                funcao = Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells["Funcao"].Value);
                Console.WriteLine("rowcount:" + Convert.ToInt32(dataGridView1.SelectedRows.Count));
                Console.WriteLine("id:" + Convert.ToInt32(idFuncionarioSelecionado));
                idupt = idFuncionarioSelecionado;
                // Obtém os dados do funcionário com base no idFuncionario
                if (funcao == "Médico(a)")
                {
                    ExibirDadosMedico(idFuncionarioSelecionado);
                }
                else
                {
                    ExibirDadosFuncionario(idFuncionarioSelecionado);
                }

            }

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null)
            {
                funcao = comboBox1.SelectedItem.ToString();
            }
        }
    }
}

