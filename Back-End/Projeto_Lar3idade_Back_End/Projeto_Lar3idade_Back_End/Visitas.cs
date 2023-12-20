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
    public partial class Visitas : UserControl
    {
        private MySqlConnection conexao;
        private Dictionary<string, string> responsavel_ = new Dictionary<string, string>();
        private Dictionary<string, string> utente_ = new Dictionary<string, string>();
        private int idVisita;
        private int idresponsavel;
        private int idutente;
        private string nome_responsavel = "";
        private string nome_utente = "";
        public Visitas()
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
            cmd.CommandText = "SELECT v.idVisita,u.nome as Utente, v.Utente_idUtente,v.Familiar_idFamiliar,f.nomel as Familiar,v.data FROM mydb.visita v JOIN mydb.utente u ON v.Utente_idUtente = u.idUtente JOIN mydb.familiar f ON v.Familiar_idFamiliar = f.idFamiliar;";
            cmd.ExecuteNonQuery();
            DataTable dta = new DataTable();
            MySqlDataAdapter dataadapter = new MySqlDataAdapter(cmd);
            dataadapter.Fill(dta);
            if (dta.Rows.Count > 0)
            {
                // Assuming that Medico_idMedico is of integer type, you may need to cast it accordingly
                idresponsavel = Convert.ToInt32(dta.Rows[0]["Familiar_idFamiliar"]);
                idutente = Convert.ToInt32(dta.Rows[0]["Utente_idUtente"]);
            }
            dataGridView1.DataSource = dta;
            dataGridView1.Columns["Familiar_idFamiliar"].Visible = false;
            dataGridView1.Columns["Utente_idUtente"].Visible = false;
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
                string queryMedico = "SELECT idFamiliar, nomel FROM familiar";
                using (MySqlCommand cmdMedico = new MySqlCommand(queryMedico, conexao))
                {
                    using (MySqlDataReader readerMedico = cmdMedico.ExecuteReader())
                    {
                        while (readerMedico.Read())
                        {
                            string idresponsavel = readerMedico["idFamiliar"].ToString();
                            string nomeresponavel = readerMedico["nomel"].ToString();
                            comboBox2.Items.Add(nomeresponavel);
                            responsavel_[nomeresponavel] = idresponsavel;
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
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                conexao.Open();
                string query3 = "SELECT * FROM visita ORDER BY idVisita DESC LIMIT 1";

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
                            idVisita = 1 + int.Parse(reader["idVisita"].ToString());

                        }
                    }
                }
                MySqlCommand cmd = conexao.CreateCommand();
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = "INSERT INTO mydb.visita (idVisita, Utente_idUtente, Familiar_idFamiliar, data) VALUES(@idVisita, @Utente_idUtente, @Familiar_idFamiliar, @data)";

                cmd.Parameters.AddWithValue("@idVisita", idVisita);
                cmd.Parameters.AddWithValue("@Familiar_idFamiliar", idresponsavel);
                cmd.Parameters.AddWithValue("@Utente_idUtente", idutente);
                cmd.Parameters.AddWithValue("@data", dateTimePicker1.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                

                cmd.ExecuteNonQuery();
                MessageBox.Show("Consulta agendada com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao adicionar consulta: " + ex.Message + "\n" + ex.StackTrace, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conexao.Close();
            }

            LimparTextBoxes();
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
                    cmd.CommandText = "UPDATE mydb.visita SET Utente_idUtente = @Utente_idUtente, Familiar_idFamiliar = @Familiar_idFamiliar, data = @data WHERE idVisita = @idVisita;";


                    // Adicionando parâmetros
                    cmd.Parameters.AddWithValue("@Familiar_idFamiliar", idresponsavel);
                    cmd.Parameters.AddWithValue("@Utente_idUtente", idutente);
                    cmd.Parameters.AddWithValue("@data", dateTimePicker1.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                    cmd.Parameters.AddWithValue("@idVisita", idVisita);


                    // Executando o comando
                    cmd.ExecuteNonQuery();
                    conexao.Close();

                    // Limpando os campos e atualizando a exibição dos dados
                    LimparTextBoxes();
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
        private void LimparTextBoxes()
        {
            // Limpar outras ComboBoxes e controles conforme necessário
            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
        }
        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Tem a certeza que quer desmarcar a visita?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == DialogResult.OK)
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    try
                    {
                        int rowIndex = dataGridView1.SelectedRows[0].Index;
                        int idVisita = Convert.ToInt32(dataGridView1.Rows[rowIndex].Cells["idVisita"].Value);

                        using (MySqlConnection conexao = new MySqlConnection("Server=localhost;Port=3306;Database=mydb;User ID=root;Password=ipbcurso"))
                        {
                            conexao.Open();

                            string query = "DELETE FROM visita WHERE idVisita = @idVisita";

                            using (MySqlCommand comando = new MySqlCommand(query, conexao))
                            {
                                comando.Parameters.AddWithValue("@idVisita", idVisita);
                                comando.ExecuteNonQuery();

                                MessageBox.Show("Visita desmarcada com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                display_data();
                                LimparTextBoxes();
                            }
                        }
                    }

                    catch (Exception ex)
                    {
                        MessageBox.Show("Erro ao excluir tarefa: " + ex.Message);
                    }
                }
                else
                {
                    MessageBox.Show("Por favor, selecione uma tarefa para excluir.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            display_data();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            conexao.Open();

            MySqlCommand cmd = conexao.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT v.idVisita,u.nome as Utente, v.Utente_idUtente,v.Familiar_idFamiliar,f.nomel as Familiar,v.data FROM mydb.visita v JOIN mydb.utente u ON v.Utente_idUtente = u.idUtente JOIN mydb.familiar f ON v.Familiar_idFamiliar = f.idFamiliar " +
                  "WHERE (SUBSTRING_INDEX(u.nome, ' ', 1) LIKE @searchText OR SUBSTRING_INDEX(f.nomel, ' ', 1) LIKE @searchText)";


            cmd.Parameters.AddWithValue("@searchText", "%" + textBox1.Text + "%");
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

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedItem != null)
            {
                string selectedValue = comboBox2.SelectedItem.ToString();

                if (responsavel_.TryGetValue(selectedValue, out string id))
                {
                    idresponsavel = int.Parse(id);
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
                int idVisitaparaEditar = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["idVisita"].Value);
                PreencherCamposParaEdicao(idVisitaparaEditar);
                foreach (DataGridViewColumn column in dataGridView1.Columns)
                {
                    column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
            }
        }
        private void PreencherCamposParaEdicao(int idvisitaParaEditar)
        {
            try
            {
                conexao.Open();

                MySqlCommand cmd = conexao.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT v.idVisita,u.nome as Utente, v.Utente_idUtente,v.Familiar_idFamiliar,f.nomel as Familiar,v.data FROM mydb.visita v JOIN mydb.utente u ON v.Utente_idUtente = u.idUtente JOIN mydb.familiar f ON v.Familiar_idFamiliar = f.idFamiliar  WHERE v.idVisita = @idVisita";
                cmd.Parameters.AddWithValue("@idVisita", idvisitaParaEditar);
                DataTable dta = new DataTable();
                MySqlDataAdapter dataadapter = new MySqlDataAdapter(cmd);
                dataadapter.Fill(dta);
                if (dta.Rows.Count > 0)
                {
                    // Assuming that Medico_idMedico is of integer type, you may need to cast it accordingly
                    idresponsavel = Convert.ToInt32(dta.Rows[0]["Familiar_idFamiliar"]);
                    idutente = Convert.ToInt32(dta.Rows[0]["Utente_idUtente"]);
                    nome_responsavel = Convert.ToString(dta.Rows[0]["Familiar"]);
                    nome_utente = Convert.ToString(dta.Rows[0]["Utente"]);
                    Console.WriteLine("idv " + idresponsavel);
                    Console.WriteLine("idu " + idutente);

                }
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        // Atualize a variável idUtente com o valor do idUtenteParaEditar
                        idVisita = idvisitaParaEditar;

                        // Preenche os campos com os dados do utente
                        comboBox1.Text = nome_responsavel;
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
    }
}
