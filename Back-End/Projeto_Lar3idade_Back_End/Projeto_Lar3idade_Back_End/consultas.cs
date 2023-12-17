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
            cmd.CommandText = "SELECT c.idConsulta,u.nome as Utente, c.Utente_idUtente,c.Medico_idMedico,m.nome as Medico,c.data,c.estado FROM mydb.consulta c JOIN mydb.utente u ON c.Utente_idUtente = u.idUtente JOIN mydb.medico m ON c.Medico_idMedico = m.idMedico;";
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
        }
        private void LoadComboBox()
        {
            try
            {
                conexao.Open();

                // Carregar dados para a ComboBox de Quartos
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

                // Carregar dados para a ComboBox de Médicos
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
        private void button4_Click(object sender, EventArgs e)
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

        private void button3_Click(object sender, EventArgs e)
        {
            display_data();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                conexao.Open();

                MySqlCommand cmd = conexao.CreateCommand();
                cmd.CommandType = CommandType.Text;

                // Certifique-se de fornecer o valor do IdUtente a ser atualizado
                if (!string.IsNullOrEmpty(comboBox1.Text) && !string.IsNullOrEmpty(comboBox2.Text))
                {
                    // Obtém o valor do idUtente dos TextBoxes
                    // Utilizando parâmetros para prevenir injeção de SQL
                    cmd.CommandText = "UPDATE mydb.consulta SET idConsulta = @idConsulta, Utente_idUtente = @Utente_idUtente, Medico_idMedico = @Medico_idMedico, data = @data WHERE idconsulta = @idconsulta";

                    // Adicionando parâmetros
                    cmd.Parameters.AddWithValue("@Medico_idMedico", idMedico);
                    cmd.Parameters.AddWithValue("@Utente_idUtente", idutente);
                    cmd.Parameters.AddWithValue("@data", dateTimePicker1.Value.ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@idConsulta", idconsulta);
                    

                    // Executando o comando
                    cmd.ExecuteNonQuery();
                    conexao.Close();

                    // Limpando os campos e atualizando a exibição dos dados
                    LimparTextBoxes();
                    display_data();

                    MessageBox.Show("Dados do Utente atualizados com sucesso");
                }
                else
                {
                    MessageBox.Show("Por favor, selecione uma linha para editar ou preencha os campos obrigatórios.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao atualizar dados do Utente: " + ex.Message);
            }
            finally
            {
                conexao.Close();
            }
        }
        private void LimparTextBoxes()
        {
            // Limpar outras ComboBoxes e controles conforme necessário
            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
        }

        private void button1_Click(object sender, EventArgs e)
        {

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
                    Console.WriteLine("idM "+idMedico);
                    Console.WriteLine("idu "+idutente);

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
                MessageBox.Show("Erro ao obter dados do utente para edição: " + ex.Message+ "\n"+ ex.StackTrace);
            }
            finally
            {
                conexao.Close();
            }
        }
    }
}
