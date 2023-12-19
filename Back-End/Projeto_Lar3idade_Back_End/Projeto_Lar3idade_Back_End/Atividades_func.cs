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
    public partial class Atividades_func : UserControl
    {
        private int iduser;
        private int idAdd;
        private int idtipo;
        private int idunt;
        private Dictionary<string, string> Tipo_ = new Dictionary<string, string>();
        private Dictionary<string, string> Func_ = new Dictionary<string, string>();
        private Dictionary<string, string> Utente_ = new Dictionary<string, string>();
        private MySqlConnection conexao;
        public Atividades_func( int userid)
        {
            InitializeComponent();
            dateTimePicker_realizacao.Format = DateTimePickerFormat.Custom;
            dateTimePicker_realizacao.CustomFormat = "yyyy-MM-dd HH:mm";
            dateTimePicker_realizacao.ShowUpDown = true;
            string connectionString = "Server=localhost;Port=3306;Database=mydb;User ID=root;Password=ipbcurso";
            conexao = new MySqlConnection(connectionString);
            this.iduser = userid;
            Console.WriteLine("id Utlizador da Atividade nª1:" + iduser);
            dataGridView1.CellClick += dataGridView1_CellClick;
            LoadComboBox();
            display_data();
        }
        private void display_data()
        {
            try
            {
                conexao.Open();
                MySqlCommand cmd = conexao.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT A.idAtividade, A.Utente_idUtente, U.nome AS Utente,A.nome AS Titulo , A.data, A.descricao, A.Tipo_idTipo ,t.tipo FROM atividade A JOIN utente U ON A.Utente_idUtente = U.idUtente JOIN tipo t ON a.Tipo_idTipo = t.idTipo where Funcionario_idFuncionario = @iduser;";
                cmd.Parameters.AddWithValue("@iduser", iduser);
                cmd.ExecuteNonQuery();
                DataTable dta = new DataTable();
                MySqlDataAdapter dataadapter = new MySqlDataAdapter(cmd);
                dataadapter.Fill(dta);
                if (dta.Rows.Count > 0)
                {
                    // Assuming that Medico_idMedico is of integer type, you may need to cast it accordingly
                    idtipo = Convert.ToInt32(dta.Rows[0]["Tipo_idTipo"]);
                    idunt = Convert.ToInt32(dta.Rows[0]["Utente_idUtente"]);
                }
                dataGridView1.DataSource = dta;
                dataGridView1.Columns["Utente_idUtente"].Visible = false;
                dataGridView1.Columns["Tipo_idTipo"].Visible = false;
                foreach (DataGridViewColumn column in dataGridView1.Columns)
                {
                    column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
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
            }
        }
        private void Mostrar_Click(object sender, EventArgs e)
        {
            display_data();
            Limpar();
        }
        private void button_insert_Click(object sender, EventArgs e)
        {
            string nome = textBox_nome.Text;
            DateTime datarealizacao = dateTimePicker_realizacao.Value;
            string desc = richTextBox_descricao.Text;

            try
            {
                using (MySqlConnection conexao = new MySqlConnection("Server=localhost;Port=3306;Database=mydb;User ID=root;Password=ipbcurso"))
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
                        comando.Parameters.AddWithValue("@Funcionario_idFuncionario", iduser);
                        comando.Parameters.AddWithValue("@nome", nome);
                        comando.Parameters.AddWithValue("@data", datarealizacao);
                        comando.Parameters.AddWithValue("@descricao", desc);
                        comando.Parameters.AddWithValue("@Tipo_idTipo", idtipo);
                        comando.ExecuteNonQuery();

                        MessageBox.Show("Atividade adicionado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

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

        private void button_delete_Click(object sender, EventArgs e)
        {
            
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DialogResult result = MessageBox.Show("Tem a certeza que quer excluir esta atividade?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (result == DialogResult.OK)
                {
                    try
                    {
                        int rowIndex = dataGridView1.SelectedRows[0].Index;
                        int idAtividade = Convert.ToInt32(dataGridView1.Rows[rowIndex].Cells["idAtividade"].Value);

                        using (MySqlConnection conexao = new MySqlConnection("Server=localhost;Port=3306;Database=mydb;User ID=root;Password=ipbcurso"))
                        {
                            conexao.Open();

                            string query = "DELETE FROM atividade WHERE idAtividade = @idAtividade";

                            using (MySqlCommand comando = new MySqlCommand(query, conexao))
                            {
                                comando.Parameters.AddWithValue("@idAtividade", idAtividade);
                                comando.ExecuteNonQuery();

                                MessageBox.Show("Atividade excluída com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                display_data();
                                Limpar();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erro ao excluir atividade: " + ex.Message);
                    }
                }
               
                
            }
            else
            {
                MessageBox.Show("Por favor, selecione uma atividade para excluir.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                                          "nome = @nome, " +
                                          "data = @data, " +
                                          "descricao = @descricao, " +
                                          "Tipo_idTipo = @Tipo_idTipo " +
                                      "WHERE idAtividade = @idAtividade";

                    // Adicionando parâmetros
                    cmd.Parameters.AddWithValue("@Utente_idUtente", idunt);
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

        
        private void comboBox_tipo_SelectedIndexChanged(object sender, EventArgs e)
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

        private void comboBox_utente_SelectedIndexChanged(object sender, EventArgs e)
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

        private void Limpar()
        {
            textBox_nome.Clear();
            comboBox_tipo.SelectedIndex = -1;
            comboBox_utente.SelectedIndex = -1;
            dateTimePicker_realizacao.Value = DateTime.Now;
            richTextBox_descricao.Clear();

        }

        private void textBox_Search_TextChanged(object sender, EventArgs e)
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
            }
            else
            {
                MessageBox.Show("Nenhum resultado encontrado.");
            }
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                // Preencher os TextBoxes e ComboBoxes com os valores da linha selecionada
                textBox_nome.Text = row.Cells["Titulo"].Value.ToString();  // Use "Titulo" instead of "nome"
                dateTimePicker_realizacao.Value = Convert.ToDateTime(row.Cells["data"].Value);
                richTextBox_descricao.Text = row.Cells["descricao"].Value.ToString();

                // Selecionar os itens nos ComboBoxes com base nos valores da linha
                string tipoSelecionado = row.Cells["Tipo_idTipo"].Value.ToString();
                string utenteSelecionado = row.Cells["Utente_idUtente"].Value.ToString();

                comboBox_tipo.SelectedItem = comboBox_tipo.Items.Cast<string>()
                    .FirstOrDefault(item => Tipo_.TryGetValue(item, out string id) && id == tipoSelecionado);

                comboBox_utente.SelectedItem = comboBox_utente.Items.Cast<string>()
                    .FirstOrDefault(item => Utente_.TryGetValue(item, out string id) && id == utenteSelecionado);
            }
        }

    }
}
