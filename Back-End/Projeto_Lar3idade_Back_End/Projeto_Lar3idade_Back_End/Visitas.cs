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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;


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
            string connectionString = "Server=projetolar3idade.mysql.database.azure.com;Port=3306;Database=mydb;Uid=projeto4461045279;Pwd=Ipbcurso1";
            conexao = new MySqlConnection(connectionString);
            display_data();
            LoadComboBox();
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
        

        private bool VisitaJaAgendada()
        {
            try
            {
                conexao.Open();
                MySqlCommand cmd = conexao.CreateCommand();
                cmd.CommandType = CommandType.Text;

                // Consulta para verificar se já existe uma visita agendada para o mesmo responsável (familiar) dentro de um intervalo de 30 minutos
                cmd.CommandText = "SELECT COUNT(*) FROM visita WHERE Familiar_idFamiliar = @idFamiliar " +
                                  "AND data BETWEEN @startInterval AND @endInterval";

                cmd.Parameters.AddWithValue("@idFamiliar", idresponsavel);

                // Definir o intervalo de 30 minutos
                DateTime startInterval = dateTimePicker1.Value.AddMinutes(-15);
                DateTime endInterval = dateTimePicker1.Value.AddMinutes(15);

                cmd.Parameters.AddWithValue("@startInterval", startInterval.ToString("yyyy-MM-dd HH:mm:ss"));
                cmd.Parameters.AddWithValue("@endInterval", endInterval.ToString("yyyy-MM-dd HH:mm:ss"));

                // Se estiver editando uma visita existente, exclua a visita atual da contagem
                if (idVisita > 0)
                {
                    cmd.CommandText += " AND idVisita != @idVisita";
                    cmd.Parameters.AddWithValue("@idVisita", idVisita);
                }

                int count = Convert.ToInt32(cmd.ExecuteScalar());

                return count > 0; // Retorna true se já existir uma visita agendada para o mesmo responsável dentro do intervalo de 30 minutos, false caso contrário
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao verificar se a visita já está agendada: " + ex.Message);
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
                // Verifica se já existe uma visita agendada para o mesmo responsável e utente dentro do intervalo de 30 minutos
                if (VisitaJaAgendada())
                {
                    MessageBox.Show("Já existe uma visita agendada para este responsável e utente dentro deste intervalo de 30 minutos.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    // Obtém o ID do responsável (familiar) selecionado na comboBox2
                    string selectedResponsavel = comboBox2.SelectedItem.ToString();
                    if (responsavel_.TryGetValue(selectedResponsavel, out string idResponsavel))
                    {
                        idresponsavel = int.Parse(idResponsavel);
                    }

                    // Obtém o ID do utente selecionado na comboBox1
                    string selectedUtente = comboBox1.SelectedItem.ToString();
                    if (utente_.TryGetValue(selectedUtente, out string idUtente))
                    {
                        idutente = int.Parse(idUtente);
                    }

                    // Abre a conexão com o banco de dados
                    conexao.Open();
                    MySqlCommand cmd = conexao.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "INSERT INTO mydb.visita (Utente_idUtente, Familiar_idFamiliar, data) VALUES(@Utente_idUtente, @Familiar_idFamiliar, @data)";
                    cmd.Parameters.AddWithValue("@Familiar_idFamiliar", idresponsavel);
                    cmd.Parameters.AddWithValue("@Utente_idUtente", idutente);
                    cmd.Parameters.AddWithValue("@data", dateTimePicker1.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Consulta agendada com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao adicionar consulta: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conexao.Close();
            }

            LimparComboBoxes();
            display_data();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                // Verifica se já existe uma visita agendada para o mesmo responsável e utente dentro do intervalo de 30 minutos
                if (VisitaJaAgendada())
                {
                    MessageBox.Show("Já existe uma visita agendada para este responsável e utente dentro deste intervalo de 30 minutos.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return; // Retorna imediatamente para evitar a execução do restante do código
                }

                // Se não houver visita agendada para o mesmo responsável e utente, continua com o código de atualização

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

                        using (MySqlConnection conexao = new MySqlConnection("Server=projetolar3idade.mysql.database.azure.com;Port=3306;Database=mydb;Uid=projeto4461045279;Pwd=Ipbcurso1"))
                        {
                            conexao.Open();

                            string query = "DELETE FROM visita WHERE idVisita = @idVisita";

                            using (MySqlCommand comando = new MySqlCommand(query, conexao))
                            {
                                comando.Parameters.AddWithValue("@idVisita", idVisita);
                                comando.ExecuteNonQuery();

                                MessageBox.Show("Visita desmarcada com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                display_data();
                                LimparComboBoxes();
                            }
                        }
                    }

                    catch (Exception ex)
                    {
                        MessageBox.Show("Erro ao desmarcar uma visita: " + ex.Message);
                    }
                }
                else
                {
                    MessageBox.Show("Por favor, selecione uma linha para excluir.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            LimparComboBoxes();
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
                string selectedResponsavel = comboBox2.SelectedItem.ToString();

                if (responsavel_.TryGetValue(selectedResponsavel, out string idResponsavel))
                {
                    idresponsavel = int.Parse(idResponsavel);

                    comboBox1.Items.Clear(); 

                    try
                    {
                        conexao.Open();
                        
                        string query = "SELECT u.nome FROM utente u INNER JOIN utente_familiar uf ON u.idUtente = uf.Utente_idUtente WHERE uf.Familiar_idFamiliar = @idFamiliar";
                        using (MySqlCommand cmd = new MySqlCommand(query, conexao))
                        {
                            cmd.Parameters.AddWithValue("@idFamiliar", idresponsavel);

                            using (MySqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    string utenteNome = reader["nome"].ToString();
                                    comboBox1.Items.Add(utenteNome);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erro ao carregar utentes relacionados: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        
                        conexao.Close();
                    }
                }
            }
        }
        private void LoadComboBox()
        {
            try
            {
                conexao.Open();

                
                string queryUtente = "SELECT idUtente, nome FROM utente";
                using (MySqlCommand cmdUtente = new MySqlCommand(queryUtente, conexao))
                {
                    using (MySqlDataReader readerUtente = cmdUtente.ExecuteReader())
                    {
                        while (readerUtente.Read())
                        {
                            string idUtente = readerUtente["idUtente"].ToString();
                            string utenteNome = readerUtente["nome"].ToString();
                            comboBox1.Items.Add(utenteNome);
                            utente_[utenteNome] = idUtente;
                        }
                    }
                }

                
                string queryFamiliar = "SELECT idFamiliar, nomel FROM familiar";
                using (MySqlCommand cmdFamiliar = new MySqlCommand(queryFamiliar, conexao))
                {
                    using (MySqlDataReader readerFamiliar = cmdFamiliar.ExecuteReader())
                    {
                        while (readerFamiliar.Read())
                        {
                            string idFamiliar = readerFamiliar["idFamiliar"].ToString();
                            string nomeFamiliar = readerFamiliar["nomel"].ToString();
                            comboBox2.Items.Add(nomeFamiliar);
                            responsavel_[nomeFamiliar] = idFamiliar;
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


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           
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
