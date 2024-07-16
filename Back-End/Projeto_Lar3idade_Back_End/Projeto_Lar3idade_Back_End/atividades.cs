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

    public partial class atividades : UserControl
    {
        
        private int idAdd;
        private int idtipo;
        private int idunt;
        private int idfunc;
        private Dictionary<string, string> Tipo_ = new Dictionary<string, string>();
        private Dictionary<string, string> Func_ = new Dictionary<string, string>();
        private Dictionary<string, string> Utente_ = new Dictionary<string, string>();
        private MySqlConnection conexao;

        public atividades()
        {
            InitializeComponent();
            conexao = new MySqlConnection(DatabaseConfig.ConnectionString);
            LoadComboBox();
            display_data();
            dataGridView1.CellClick += dataGridView1_CellClick;

        }
        private void LoadComboBox()
        {
            using (conexao)
            {
                conexao.Open();

                string query = "SELECT idTipo,tipo FROM tipo";
                using (MySqlCommand cmd = new MySqlCommand(query, conexao))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string id = reader["idTipo"].ToString();
                            string tipo = reader["tipo"].ToString();
                            Tipo_[tipo] = id;
                            comboBox_tipo.Items.Add(tipo);
                        }
                    }
                }
                string query2 = "SELECT idUtente,nome FROM utente";
                using (MySqlCommand cmd = new MySqlCommand(query2, conexao))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            string id = reader["idUtente"].ToString();
                            string nome = reader["nome"].ToString();
                            comboBox_utente.Items.Add(nome);
                            Utente_[nome] = id;


                        }
                    }
                }
                string query3 = "SELECT idFuncionario,nome FROM funcionario";
                using (MySqlCommand cmd = new MySqlCommand(query3, conexao))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            string id = reader["idFuncionario"].ToString();
                            string nome = reader["nome"].ToString();
                            comboBox_funcionario.Items.Add(nome);
                            Func_[nome] = id;


                        }
                    }
                }
            }
        }

        private void atividades_Load(object sender, EventArgs e)
        {

        }

        private void button_insert_Click(object sender, EventArgs e)
        {
            string nome = textBox_nome.Text;
            DateTime datarealizacao = dateTimePicker_realizacao.Value;
            string desc = richTextBox_descricao.Text;

            try
            {
                using (conexao)
                {
                    conexao.Open();
                    string query = "INSERT INTO mydb.atividade (idAtividade,Utente_idUtente, Funcionario_idFuncionario, nome, data, descricao, Tipo_idTipo)" +
                                    "VALUES (@idAtividade, @Utente_idUtente, @Funcionario_idFuncionario, @nome, @data, @descricao, @Tipo_idTipo)";
                    string query2 = "SELECT * FROM atividade ORDER BY idAtividade DESC LIMIT 1";

                    using (MySqlCommand procurarId = new MySqlCommand(query2, conexao))
                    {
                        using (MySqlDataReader reader = procurarId.ExecuteReader())
                        {
                            // Create a list to store data
                            List<string[]> data = new List<string[]>();

                            // Iterate through the results
                            while (reader.Read())
                            {
                                // Add data to the list
                                idAdd = 1 + int.Parse(reader["idAtividade"].ToString());

                            }
                        }
                    }

                    // Crie um comando MySqlCommand
                    using (MySqlCommand comando = new MySqlCommand(query, conexao))
                    {
                        comando.Parameters.AddWithValue("@idAtividade", idAdd);
                        comando.Parameters.AddWithValue("@Utente_idUtente", idunt);
                        comando.Parameters.AddWithValue("@Funcionario_idFuncionario", idfunc);
                        comando.Parameters.AddWithValue("@nome", nome);
                        comando.Parameters.AddWithValue("@data", datarealizacao);
                        comando.Parameters.AddWithValue("@descricao", desc);
                        comando.Parameters.AddWithValue("@Tipo_idTipo", idtipo);
                        comando.ExecuteNonQuery();

                        MessageBox.Show("Atividade adicionado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        conexao.Close();

                        Limpar();
                        display_data();

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao adicionar Atividade: " + ex.Message);
            }

        }
        private void Limpar()
        {
            textBox_nome.Clear();
            comboBox_tipo.SelectedIndex = -1;
            comboBox_utente.SelectedIndex = -1;
            comboBox_funcionario.SelectedIndex = -1;
            dateTimePicker_realizacao.Value = DateTime.Now;
            richTextBox_descricao.Clear();

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox_tipo.SelectedItem != null)
            {
                string selectedValue = comboBox_tipo.SelectedItem.ToString();

                if (Tipo_.TryGetValue(selectedValue, out string id))
                {

                    // Use specificColumnData as needed (e.g., assign it to a variable)
                    idtipo = int.Parse(id.ToString());
                    Console.WriteLine(id);
                }
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox_utente.SelectedItem != null)
            {
                string selectedValue = comboBox_utente.SelectedItem.ToString();

                if (Utente_.TryGetValue(selectedValue, out string id))
                {

                    // Use specificColumnData as needed (e.g., assign it to a variable)
                    idunt = int.Parse(id.ToString());
                    Console.WriteLine(idunt);
                }
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox_funcionario.SelectedItem != null)
            {
                string selectedValue = comboBox_funcionario.SelectedItem.ToString();

                if (Func_.TryGetValue(selectedValue, out string id))
                {

                    // Use specificColumnData as needed (e.g., assign it to a variable)
                    idfunc = int.Parse(id.ToString());
                    Console.WriteLine(idfunc);
                }
            }
        }


        private void button_update_Click(object sender, EventArgs e)
        {
            try
            {
                conexao.Open();

                MySqlCommand cmd = conexao.CreateCommand();
                cmd.CommandType = CommandType.Text;

                // Certifique-se de fornecer o valor do idAtividade a ser atualizado
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    int rowIndex = dataGridView1.SelectedRows[0].Index;
                    int idAtividade = Convert.ToInt32(dataGridView1.Rows[rowIndex].Cells["idAtividade"].Value);

                    // Obtém o valor do idAtividade dos TextBoxes
                    // Utilizando parâmetros para prevenir injeção de SQL
                    cmd.CommandText = "UPDATE mydb.atividade " +
                                      "SET Utente_idUtente = @Utente_idUtente, " +
                                          "Funcionario_idFuncionario = @Funcionario_idFuncionario, " +
                                          "nome = @nome, " +
                                          "data = @data, " +
                                          "descricao = @descricao, " +
                                          "Tipo_idTipo = @Tipo_idTipo " +
                                      "WHERE idAtividade = @idAtividade";

                    // Adicionando parâmetros
                    cmd.Parameters.AddWithValue("@Utente_idUtente", idunt);
                    cmd.Parameters.AddWithValue("@Funcionario_idFuncionario", idfunc);
                    cmd.Parameters.AddWithValue("@nome", textBox_nome.Text);
                    cmd.Parameters.AddWithValue("@data", dateTimePicker_realizacao.Value);
                    cmd.Parameters.AddWithValue("@descricao", richTextBox_descricao.Text);
                    cmd.Parameters.AddWithValue("@Tipo_idTipo", idtipo);
                    cmd.Parameters.AddWithValue("@idAtividade", idAtividade);

                    // Executando o comando
                    cmd.ExecuteNonQuery();
                    conexao.Close();

                    // Limpando os campos e atualizando a exibição dos dados
                    Limpar();
                    display_data();

                    MessageBox.Show("Dados atualizados com sucesso");
                }
                else
                {
                    MessageBox.Show("Por favor, selecione uma linha para editar.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao atualizar dados: " + ex.Message);
            }
            finally
            {
                conexao.Close();
            }

        }

       
        private void button_delete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                try
                {
                    int rowIndex = dataGridView1.SelectedRows[0].Index;
                    int idAtividade = Convert.ToInt32(dataGridView1.Rows[rowIndex].Cells["idAtividade"].Value);

                    using (conexao)
                    {
                        conexao.Open();

                        string query = "DELETE FROM atividade WHERE idAtividade = @idAtividade";

                        using (MySqlCommand comando = new MySqlCommand(query, conexao))
                        {
                            comando.Parameters.AddWithValue("@idAtividade", idAtividade);
                            comando.ExecuteNonQuery();

                            MessageBox.Show("Atividade excluída com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            conexao.Close();

                            display_data();
                            Limpar();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao excluir atividade: " + ex.Message);
                }
                finally
                {
                    conexao.Close();
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecione uma atividade para excluir.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private void Mostrar_Click(object sender, EventArgs e)
        {
            display_data();
            Limpar();
        }

        private void button_Search_Click(object sender, EventArgs e)
        {
            conexao.Open();

            MySqlCommand cmd = conexao.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT A.idAtividade, A.Utente_idUtente, U.nome AS nome_utente, " +
                            "A.Funcionario_idFuncionario, F.nome AS nome_funcionario, " +
                            "A.nome AS nome_atividade, A.data, A.descricao, A.Tipo_idTipo " +
                            "FROM atividade A " +
                            "JOIN utente U ON A.Utente_idUtente = U.idUtente " +
                            "JOIN funcionario F ON A.Funcionario_idFuncionario = F.idFuncionario " +
                            "WHERE A.nome LIKE @searchText OR SUBSTRING_INDEX(A.nome, ' ', 1) LIKE @searchText";
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
                textBox_nome.Text = "";
                dateTimePicker_realizacao.Text = "";
                comboBox_tipo.SelectedIndex = 0;
                comboBox_utente.SelectedIndex = 0;
                comboBox_funcionario.SelectedIndex = 0;
            }
            else
            {
                MessageBox.Show("Nenhum resultado encontrado.");
            }

        }
        private void display_data()
        {
            try
            {
                conexao.Open();
                MySqlCommand cmd = conexao.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT A.idAtividade, A.Utente_idUtente, U.nome AS nome_utente, " +
                                  "A.Funcionario_idFuncionario, F.nome AS nome_funcionario, " +
                                  "A.nome AS nome_atividade, A.data, A.descricao, A.Tipo_idTipo " +
                                  "FROM atividade A " +
                                  "JOIN utente U ON A.Utente_idUtente = U.idUtente " +
                                  "JOIN funcionario F ON A.Funcionario_idFuncionario = F.idFuncionario";

                cmd.ExecuteNonQuery();
                DataTable dta = new DataTable();
                MySqlDataAdapter dataadapter = new MySqlDataAdapter(cmd);
                dataadapter.Fill(dta);

                dataGridView1.DataSource = dta;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao exibir dados: " + ex.Message);
            }
            finally
            {
                conexao.Close();
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                // Preencher os TextBoxes e ComboBoxes com os valores da linha selecionada
                textBox_nome.Text = row.Cells["nome_atividade"].Value.ToString();
                dateTimePicker_realizacao.Value = Convert.ToDateTime(row.Cells["data"].Value);
                richTextBox_descricao.Text = row.Cells["descricao"].Value.ToString();

                // Selecionar os itens nos ComboBoxes com base nos valores da linha
                string tipoSelecionado = row.Cells["Tipo_idTipo"].Value.ToString();
                string utenteSelecionado = row.Cells["Utente_idUtente"].Value.ToString();
                string funcionarioSelecionado = row.Cells["Funcionario_idFuncionario"].Value.ToString();

                comboBox_tipo.SelectedItem = comboBox_tipo.Items.Cast<string>()
                    .FirstOrDefault(item => Tipo_.TryGetValue(item, out string id) && id == tipoSelecionado);

                comboBox_utente.SelectedItem = comboBox_utente.Items.Cast<string>()
                    .FirstOrDefault(item => Utente_.TryGetValue(item, out string id) && id == utenteSelecionado);

                comboBox_funcionario.SelectedItem = comboBox_funcionario.Items.Cast<string>()
                    .FirstOrDefault(item => Func_.TryGetValue(item, out string id) && id == funcionarioSelecionado);
            }
        }
     

    }
}
