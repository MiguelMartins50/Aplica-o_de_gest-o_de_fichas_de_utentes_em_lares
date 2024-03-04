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
    public partial class consultas : UserControl
    {
        private MySqlConnection conexao;
        private Dictionary<string, string> medico_ = new Dictionary<string, string>();
        private Dictionary<string, string> utente_ = new Dictionary<string, string>();
        private int idconsulta;
        private int idpres;

        private int idMedico;
        private int idutente;
        private string nome_Medico = "";
        private string nome_utente = "";


        public consultas()
        {
            InitializeComponent();
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "yyyy-MM-dd HH:mm";
            dateTimePicker1.ShowUpDown = true;
            string connectionString = "Server=localhost;Port=3306;Database=mydb;User ID=root;Password=ipbcurso";
            conexao = new MySqlConnection(connectionString);
            LoadComboBox();
            display_data();
        }
        private void display_data()
        {
            conexao.Open();
            MySqlCommand cmd = conexao.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT c.idConsulta, u.nome as Utente, c.Utente_idUtente, c.Medico_idMedico, m.nome as Medico, c.data, c.estado FROM mydb.consulta c JOIN mydb.utente u ON c.Utente_idUtente = u.idUtente JOIN mydb.medico m ON c.Medico_idMedico = m.idMedico WHERE c.estado = 'Agendada';\r\n";
            cmd.ExecuteNonQuery();
            DataTable dta = new DataTable();
            MySqlDataAdapter dataadapter = new MySqlDataAdapter(cmd);
            dataadapter.Fill(dta);
            if (dta.Rows.Count > 0)
            {
                // Assuming that Medico_idMedico is of integer type, you may need to cast it accordingly
                idMedico = Convert.ToInt32(dta.Rows[0]["Medico_idMedico"]);
                idutente = Convert.ToInt32(dta.Rows[0]["Utente_idUtente"]);
            }
            dataGridView1.DataSource = dta;
            dataGridView1.Columns["Utente_idUtente"].Visible = false;
            dataGridView1.Columns["Medico_idMedico"].Visible = false;
            conexao.Close();
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        }
        private void LoadComboBox()
        {
            try
            {
                conexao.Open();

                string queryuntente = "SELECT idUtente,nome FROM utente";
                using (MySqlCommand cmdQuarto = new MySqlCommand(queryuntente, conexao))
                {
                    using (MySqlDataReader readerQuarto = cmdQuarto.ExecuteReader())
                    {
                        while (readerQuarto.Read())
                        {
                            string idUtente = readerQuarto["idUtente"].ToString();
                            string utenteNome = readerQuarto["nome"].ToString();
                            comboBox1.Items.Add(utenteNome);
                            utente_[utenteNome] = idUtente;
                        }
                    }
                }


                string queryMedico = "SELECT idMedico, nome FROM medico";
                using (MySqlCommand cmdMedico = new MySqlCommand(queryMedico, conexao))
                {
                    using (MySqlDataReader readerMedico = cmdMedico.ExecuteReader())
                    {
                        while (readerMedico.Read())
                        {
                            string idMedico = readerMedico["idMedico"].ToString();
                            string nomeMedico = readerMedico["nome"].ToString();
                            comboBox2.Items.Add(nomeMedico);
                            medico_[nomeMedico] = idMedico;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar ComboBoxes: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conexao.Close();
            }
        }


        private void button3_Click(object sender, EventArgs e)
        {
            LimparComboBoxes();
            display_data();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (ConsultaJaAgendada())
                {
                    MessageBox.Show("Já existe uma consulta agendada para este utente ou médico neste horário. Por favor, faça outro agendamento com um intervalo de 10 minutos.");
                    return;
                }

                conexao.Open();

                MySqlCommand cmd = conexao.CreateCommand();
                cmd.CommandType = CommandType.Text;

                // Certifique-se de fornecer o valor do IdUtente a ser atualizado
                if (!string.IsNullOrEmpty(comboBox1.Text) && !string.IsNullOrEmpty(comboBox2.Text))
                {
                    // Obtém o valor do idUtente dos TextBoxes
                    // Utilizando parâmetros para prevenir injeção de SQL
                    cmd.CommandText = "UPDATE mydb.consulta SET Utente_idUtente = @Utente_idUtente, Medico_idMedico = @Medico_idMedico, data = @data WHERE idconsulta = @idconsulta";

                    cmd.Parameters.AddWithValue("@Medico_idMedico", idMedico);
                    cmd.Parameters.AddWithValue("@Utente_idUtente", idutente);
                    cmd.Parameters.AddWithValue("@data", dateTimePicker1.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                    cmd.Parameters.AddWithValue("@idConsulta", idconsulta);

                    cmd.ExecuteNonQuery();
                    conexao.Close();

                    LimparComboBoxes();
                    display_data();

                    MessageBox.Show("Consulta alterada com sucesso");
                }
                else
                {
                    MessageBox.Show("Por favor, selecione uma linha para editar ou preencha os campos obrigatórios.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao atualizar dados da consulta: " + ex.Message + "\n" + ex.StackTrace);
            }
            finally
            {
                conexao.Close();
            }
        }
        private void LimparComboBoxes()
        {
            comboBox1.Text = "";
            comboBox2.Text = "";
            dataGridView1.ClearSelection();

        }

        private bool ConsultaJaAgendada()
        {
            try
            {
                conexao.Open();
                MySqlCommand cmd = conexao.CreateCommand();
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = "SELECT COUNT(*) FROM consulta " +
                                  "WHERE Medico_idMedico = @idMedico AND data BETWEEN @startInterval AND @endInterval " +
                                  "OR Utente_idUtente = @idUtente AND data BETWEEN @startInterval AND @endInterval";

                cmd.Parameters.AddWithValue("@idMedico", idMedico);
                cmd.Parameters.AddWithValue("@idUtente", idutente);


                DateTime startInterval = dateTimePicker1.Value.AddMinutes(-5);
                DateTime endInterval = dateTimePicker1.Value.AddMinutes(5);

                cmd.Parameters.AddWithValue("@startInterval", startInterval.ToString("yyyy-MM-dd HH:mm:ss"));
                cmd.Parameters.AddWithValue("@endInterval", endInterval.ToString("yyyy-MM-dd HH:mm:ss"));

                int count = Convert.ToInt32(cmd.ExecuteScalar());

                return count > 0; // Retorna true se já existir uma consulta agendada para o mesmo médico ou utente dentro do intervalo de 10 minutos, false caso contrário
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao verificar se a consulta já está agendada: " + ex.Message);
                return false;
            }
            finally
            {
                conexao.Close();
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (ConsultaJaAgendada())
                {
                    MessageBox.Show("Já existe uma consulta agendada para este utente ou médico neste horário. Por favor, faça outro agendamento com um intervalo de 10 minutos.");
                    return;
                }

                conexao.Open();

                string query4 = "SELECT * FROM prescricao_medica ORDER BY idPrescricao_medica DESC LIMIT 1";

                using (MySqlCommand procurarIdpres = new MySqlCommand(query4, conexao))
                {
                    using (MySqlDataReader reader = procurarIdpres.ExecuteReader())
                    {
                        // Create a list to store data
                        List<string[]> data = new List<string[]>();

                        // Iterate through the results
                        while (reader.Read())
                        {
                            // Add data to the list
                            idpres = 1 + int.Parse(reader["idPrescricao_medica"].ToString());

                        }
                    }
                    string query3 = "SELECT * FROM consulta ORDER BY idConsulta DESC LIMIT 1";

                    using (MySqlCommand procurarId = new MySqlCommand(query3, conexao))
                    {
                        using (MySqlDataReader reader = procurarId.ExecuteReader())
                        {
                            // Create a list to store data
                            List<string[]> data = new List<string[]>();

                            // Iterate through the results
                            while (reader.Read())
                            {
                                // Add data to the list
                                idconsulta = 1 + int.Parse(reader["idConsulta"].ToString());

                            }
                        }
                    }
                    MySqlCommand cmd = conexao.CreateCommand();
                    cmd.CommandType = CommandType.Text;

                    cmd.CommandText = "INSERT INTO mydb.consulta (idConsulta, Utente_idUtente, Medico_idMedico, data, estado, relatorio) " +
                          "VALUES (@idConsulta, @Utente_idUtente, @Medico_idMedico, @data, @estado, @relatorio)";

                    cmd.Parameters.AddWithValue("@idConsulta", idconsulta);
                    cmd.Parameters.AddWithValue("@Medico_idMedico", idMedico);
                    cmd.Parameters.AddWithValue("@Utente_idUtente", idutente);
                    cmd.Parameters.AddWithValue("@data", dateTimePicker1.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                    cmd.Parameters.AddWithValue("@estado", "Agendada");
                    cmd.Parameters.AddWithValue("@relatorio", "");

                    cmd.ExecuteNonQuery();
                    string insertPrescricaoQuery = "INSERT INTO prescricao_medica (idPrescricao_medica, estado, descricao, Consulta_idConsulta) " +
                                                   "VALUES (@idPrescricao_medica, @estadoPrescricao, @descricao, @idConsulta)";
                    using (MySqlCommand cmdPrescricao = new MySqlCommand(insertPrescricaoQuery, conexao))
                    {
                        cmdPrescricao.Parameters.AddWithValue("@idPrescricao_medica", idpres);
                        cmdPrescricao.Parameters.AddWithValue("@estadoPrescricao", "Ativo");
                        cmdPrescricao.Parameters.AddWithValue("@descricao", " ");
                        cmdPrescricao.Parameters.AddWithValue("@idConsulta", idconsulta);
                        cmdPrescricao.ExecuteNonQuery();



                        
                    }
                    conexao.Close();
                    display_data();
                    LoadComboBox();
                    MessageBox.Show("Consulta agendada com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao adicionar consulta: " + ex.Message + "\n" + ex.StackTrace, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conexao.Close();
            }

            LimparComboBoxes();
            display_data();

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedItem != null)
            {
                string selectedValue = comboBox2.SelectedItem.ToString();

                if (medico_.TryGetValue(selectedValue, out string id))
                {
                    idMedico = int.Parse(id);
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null)
            {
                string selectedValue = comboBox1.SelectedItem.ToString();

                if (utente_.TryGetValue(selectedValue, out string id))
                {
                    idutente = int.Parse(id);
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int idUtenteParaEditar = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["idConsulta"].Value);
                PreencherCamposParaEdicao(idUtenteParaEditar);
                foreach (DataGridViewColumn column in dataGridView1.Columns)
                {
                    column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
            }
        }
        private void PreencherCamposParaEdicao(int idconsultarParaEditar)
        {
            try
            {
                conexao.Open();

                MySqlCommand cmd = conexao.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT c.idConsulta,u.nome as Utente, c.Utente_idUtente,c.Medico_idMedico,m.nome as Medico,c.data,c.estado FROM mydb.consulta c JOIN mydb.utente u ON c.Utente_idUtente = u.idUtente JOIN mydb.medico m ON c.Medico_idMedico = m.idMedico WHERE c.idconsulta = @idconsulta";
                cmd.Parameters.AddWithValue("@idconsulta", idconsultarParaEditar);
                DataTable dta = new DataTable();
                MySqlDataAdapter dataadapter = new MySqlDataAdapter(cmd);
                dataadapter.Fill(dta);
                if (dta.Rows.Count > 0)
                {
                    // Assuming that Medico_idMedico is of integer type, you may need to cast it accordingly
                    idMedico = Convert.ToInt32(dta.Rows[0]["Medico_idMedico"]);
                    idutente = Convert.ToInt32(dta.Rows[0]["Utente_idUtente"]);
                    nome_Medico = Convert.ToString(dta.Rows[0]["Medico"]);
                    nome_utente = Convert.ToString(dta.Rows[0]["Utente"]);
                    Console.WriteLine("idM " + idMedico);
                    Console.WriteLine("idu " + idutente);

                }
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        // Atualize a variável idUtente com o valor do idUtenteParaEditar
                        idconsulta = idconsultarParaEditar;

                        // Preenche os campos com os dados do utente
                        comboBox1.Text = nome_Medico;
                        comboBox2.Text = nome_utente;
                        dateTimePicker1.Value = Convert.ToDateTime(reader["data"]);

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao obter dados da consulta para edição: " + ex.Message + "\n" + ex.StackTrace);
            }
            finally
            {
                conexao.Close();
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            conexao.Open();

            MySqlCommand cmd = conexao.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT c.idConsulta,u.nome as Utente, c.Utente_idUtente,c.Medico_idMedico,m.nome as Medico,c.data,c.estado " +
                              "FROM mydb.consulta c " +
                              "JOIN mydb.utente u ON c.Utente_idUtente = u.idUtente " +
                              "JOIN mydb.medico m ON c.Medico_idMedico = m.idMedico " +
                              "WHERE u.nome LIKE @searchText OR m.nome LIKE @searchText OR " +
                              "SUBSTRING_INDEX(u.nome, ' ', 1) LIKE @searchText OR SUBSTRING_INDEX(m.nome, ' ', 1) LIKE @searchText";

            cmd.Parameters.AddWithValue("@searchText", "%" + textBox2.Text + "%");
            cmd.ExecuteNonQuery();

            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;

            conexao.Close();

            // Os campos de texto são limpos apenas se houver resultados da pesquisa
            if (dt.Rows.Count > 0)
            {
                comboBox1.SelectedIndex = 0;
                comboBox2.SelectedIndex = 0;
            }
            else
            {
                MessageBox.Show("Nenhum resultado encontrado.");
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (idconsulta > 0)
            {
                // Exibe uma caixa de diálogo de confirmação
                DialogResult result = MessageBox.Show("Tem certeza que deseja apagar esta consulta?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                // Verifica a resposta do usuário
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        conexao.Open();

                        // Exclui os registros relacionados na tabela prescricao_medica primeiro
                        MySqlCommand cmdDeletePrescricao = conexao.CreateCommand();
                        cmdDeletePrescricao.CommandType = CommandType.Text;
                        cmdDeletePrescricao.CommandText = "DELETE FROM mydb.prescricao_medica WHERE Consulta_idConsulta = @idConsulta";
                        cmdDeletePrescricao.Parameters.AddWithValue("@idConsulta", idconsulta);
                        cmdDeletePrescricao.ExecuteNonQuery();

                        // Agora podemos excluir a consulta na tabela consulta
                        MySqlCommand cmdDeleteConsulta = conexao.CreateCommand();
                        cmdDeleteConsulta.CommandType = CommandType.Text;
                        cmdDeleteConsulta.CommandText = "DELETE FROM mydb.consulta WHERE idConsulta = @idConsulta";
                        cmdDeleteConsulta.Parameters.AddWithValue("@idConsulta", idconsulta);
                        cmdDeleteConsulta.ExecuteNonQuery();

                        MessageBox.Show("Consulta excluída com sucesso!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erro ao excluir consulta: " + ex.Message);
                    }
                    finally
                    {
                        conexao.Close();
                        LimparComboBoxes();
                        display_data();
                    }
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecione uma consulta para excluir.");
            }
        }
    }
}

