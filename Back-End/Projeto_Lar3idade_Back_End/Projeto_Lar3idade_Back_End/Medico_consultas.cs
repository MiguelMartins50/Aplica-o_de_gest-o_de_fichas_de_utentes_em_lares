using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Projeto_Lar3idade_Back_End
{
    public partial class Medico_consultas : UserControl
    {
        private int user_ID;
        private Dictionary<string, string> utente_ = new Dictionary<string, string>();
        private Dictionary<string, string> consulta_ = new Dictionary<string, string>();
        private Dictionary<string, string> prescricao_ = new Dictionary<string, string>();
        private int idutente;
        private int idConsulta;
        private int idprescricao;
        private int idPrescricao_medica;
        private string estado_prescricao = "";
        private string estado_consulta = "";
        private string nome_utente = "";
        private string descricao_ = "";
        private string relatorio_ = "";
        private int control = 0;

        private MySqlConnection conexao;
        public Medico_consultas(int userID)
        {
            InitializeComponent();
            dateTimePicker_data.Format = DateTimePickerFormat.Custom;
            dateTimePicker_data.CustomFormat = "yyyy-MM-dd HH:mm";
            dateTimePicker_data.ShowUpDown = true;
            conexao = new MySqlConnection(DatabaseConfig.ConnectionString);
            this.user_ID = userID;
            LoadComboBox();
            display_data();
            comboBox_consulta.Items.Add("Agendada");
            comboBox_consulta.Items.Add("Efetuada");
            comboBox1.Items.Add("Ativo");
            comboBox1.Items.Add("Terminado");
            comboBox1.Items.Add("Por Começar");
            dataGridView1.CellClick += DataGridView1_CellClick;

        }

        private void display_data()
        {
            if (conexao.State != ConnectionState.Open)
            {
                conexao.Open();
            }
            MySqlCommand cmd = conexao.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT c.idConsulta,u.nome AS Utente,c.Utente_idUtente,c.data,c.estado,c.relatorio,p.descricao,p.idPrescricao_medica,p.estado AS \"estado da prescricao\" FROM mydb.consulta c JOIN mydb.utente u ON c.Utente_idUtente = u.idUtente JOIN mydb.medico m ON c.Medico_idMedico = m.idMedico LEFT JOIN mydb.prescricao_medica p ON p.Consulta_idConsulta = c.idConsulta WHERE c.Medico_idMedico =@iduser ORDER BY CASE WHEN c.estado = 'Agendada' THEN 0 ELSE 1 END;";
            cmd.Parameters.AddWithValue("@iduser", user_ID);
            cmd.ExecuteNonQuery();
            DataTable dta = new DataTable();
            MySqlDataAdapter dataadapter = new MySqlDataAdapter(cmd);
            dataadapter.Fill(dta);
            if (dta.Rows.Count > 0)
            {
                // Assuming that Medico_idMedico is of integer type, you may need to cast it accordingly
                idprescricao = Convert.ToInt32(dta.Rows[0]["idPrescricao_medica"]);
                idutente = Convert.ToInt32(dta.Rows[0]["Utente_idUtente"]);
            }
            dataGridView1.DataSource = dta;
            dataGridView1.Columns["Utente_idUtente"].Visible = false;
            dataGridView1.Columns["idPrescricao_medica"].Visible = false;
            conexao.Close();
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }

        }
        private void button1_Click(object sender, EventArgs e)
        {
            Console.WriteLine(idutente);
            Console.WriteLine(idutente);

            try
            {
                if (conexao.State != ConnectionState.Open)
                {
                    conexao.Open();
                }

                string query2 = "SELECT idConsulta FROM consulta ORDER BY idConsulta DESC LIMIT 1";

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
                            idConsulta = 1 + int.Parse(reader["idConsulta"].ToString());

                        }
                    }
                }
                string query3 = "SELECT idPrescricao_medica FROM prescricao_medica ORDER BY idPrescricao_medica DESC LIMIT 1";

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
                            idPrescricao_medica = 1 + int.Parse(reader["idPrescricao_medica"].ToString());

                        }
                    }
                }
                if ((!string.IsNullOrWhiteSpace(textBox1.Text) && (!string.IsNullOrWhiteSpace(comboBox1.Text))))
                {
                    control = 1;
                }
                // Inserir dados na tabela consulta
                string insertConsultaQuery = "INSERT INTO consulta (idConsulta, Utente_idUtente, Medico_idMedico, data, estado, relatorio) " +
                                            "VALUES (@idConsulta, @Utente_idUtente, @Medico_idMedico, @data, @estado, @relatorio)";
                using (MySqlCommand cmdConsulta = new MySqlCommand(insertConsultaQuery, conexao))
                {
                    cmdConsulta.Parameters.AddWithValue("@idConsulta", idConsulta);
                    cmdConsulta.Parameters.AddWithValue("@Utente_idUtente", idutente);
                    cmdConsulta.Parameters.AddWithValue("@Medico_idMedico", user_ID);
                    cmdConsulta.Parameters.AddWithValue("@data", dateTimePicker_data.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                    cmdConsulta.Parameters.AddWithValue("@estado", estado_consulta);
                    cmdConsulta.Parameters.AddWithValue("@relatorio", textBox_relatorio.Text);
                    cmdConsulta.ExecuteNonQuery();
                }

                // Inserir dados na tabela prescricao_medica
                string insertPrescricaoQuery = "INSERT INTO prescricao_medica (idPrescricao_medica, estado, descricao, Consulta_idConsulta) " +
                                               "VALUES (@idPrescricao_medica, @estadoPrescricao, @descricao, @idConsulta)";
                using (MySqlCommand cmdPrescricao = new MySqlCommand(insertPrescricaoQuery, conexao))
                {
                    cmdPrescricao.Parameters.AddWithValue("@idPrescricao_medica", idPrescricao_medica);
                    cmdPrescricao.Parameters.AddWithValue("@estadoPrescricao", estado_prescricao);
                    cmdPrescricao.Parameters.AddWithValue("@descricao", textBox1.Text);
                    cmdPrescricao.Parameters.AddWithValue("@idConsulta", idConsulta);
                    cmdPrescricao.ExecuteNonQuery();



                    display_data();
                    LoadComboBox();
                    LimparTextBoxes();
                    MessageBox.Show("Dados inseridos com sucesso.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao inserir dados: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conexao.Close();
            }
            
        }

        private void LoadComboBox()
        {
            try
            {
                if (conexao.State != ConnectionState.Open)
                {
                    conexao.Open();
                }


                string queryuntente = "SELECT idUtente,nome FROM utente";
                using (MySqlCommand cmdQuarto = new MySqlCommand(queryuntente, conexao))
                {
                    using (MySqlDataReader readerQuarto = cmdQuarto.ExecuteReader())
                    {
                        while (readerQuarto.Read())
                        {
                            string idUtente = readerQuarto["idUtente"].ToString();
                            string utenteNome = readerQuarto["nome"].ToString();
                            comboBox_utente.Items.Add(utenteNome);
                            utente_[utenteNome] = idUtente;
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


        private void comboBox_utente_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox_utente.SelectedItem != null)
            {
                string selectedValue = comboBox_utente.SelectedItem.ToString();

                if (utente_.TryGetValue(selectedValue, out string id))
                {
                    idutente = int.Parse(id);
                }
            }
        }

        private void comboBox_consulta_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox_consulta.SelectedItem != null)
            {
                
                    estado_consulta = comboBox_consulta.SelectedItem.ToString();
                
            }

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null)
            {
                estado_prescricao = comboBox1.SelectedItem.ToString();

            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            display_data();
            LoadComboBox();
            LimparTextBoxes();
        }

        private void textBox_Search_TextChanged(object sender, EventArgs e)
        {
            if (conexao.State != ConnectionState.Open)
            {
                conexao.Open();
            }

            MySqlCommand cmd = conexao.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT c.idConsulta, u.nome AS Utente, c.Utente_idUtente, m.nome AS Medico, c.data, c.estado, p.descricao, p.idPrescricao_medica, p.estado AS \"estado da prescricao\" FROM mydb.consulta c JOIN mydb.utente u ON c.Utente_idUtente = u.idUtente JOIN mydb.medico m ON c.Medico_idMedico = m.idMedico LEFT JOIN mydb.prescricao_medica p ON p.Consulta_idConsulta = c.idConsulta WHERE u.nome LIKE @searchText OR m.nome LIKE @searchText OR SUBSTRING_INDEX(u.nome, ' ', 1) LIKE @searchText OR SUBSTRING_INDEX(m.nome, ' ', 1) LIKE @searchText;";

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
                comboBox1.SelectedIndex = 0;
                comboBox_consulta.SelectedIndex = 0;
                comboBox_utente.SelectedIndex = 0;
            }
            else
            {
                MessageBox.Show("Nenhum resultado encontrado.");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            try
            {
                if (conexao.State != ConnectionState.Open)
                {
                    conexao.Open();
                }

                MySqlCommand cmd = conexao.CreateCommand();
                cmd.CommandType = CommandType.Text;

                // Certifique-se de fornecer o valor do IdUtente a ser atualizado
                if (!string.IsNullOrEmpty(comboBox1.Text) && !string.IsNullOrEmpty(comboBox_consulta.Text) && !string.IsNullOrEmpty(comboBox_utente.Text))
                {
                   
                    // Obtém o valor do idUtente dos TextBoxes
                    // Utilizando parâmetros para prevenir injeção de SQL
                    cmd.CommandText = "UPDATE mydb.consulta SET Utente_idUtente = @Utente_idUtente, data = @data, estado = @estado, relatorio = @relatorio WHERE idconsulta = @idconsulta";


                    // Adicionando parâmetros
                    cmd.Parameters.AddWithValue("@Utente_idUtente", idutente);
                    cmd.Parameters.AddWithValue("@data", dateTimePicker_data.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                    cmd.Parameters.AddWithValue("@idConsulta", idConsulta);
                    cmd.Parameters.AddWithValue("@estado", estado_consulta);
                    cmd.Parameters.AddWithValue("@relatorio", textBox_relatorio.Text);
                    // Executando o comando
                    cmd.ExecuteNonQuery();
                    
                        string query3 = "Select idPrescricao_medica from mydb.prescricao_medica where Consulta_idConsulta = @idconsulta;";

                        using (MySqlCommand procurarId = new MySqlCommand(query3, conexao))
                        {
                            procurarId.Parameters.AddWithValue("@idconsulta", idConsulta);

                            using (MySqlDataReader reader = procurarId.ExecuteReader())
                            {
                                // Create a list to store data
                                List<string[]> data = new List<string[]>();

                                // Iterate through the results
                                while (reader.Read())
                                {
                                    // Add data to the list
                                    idPrescricao_medica = int.Parse(reader["idPrescricao_medica"].ToString());

                                }
                            }
                        }
                        cmd.CommandText = "UPDATE mydb.prescricao_medica SET estado = @estado_, descricao = @descricao WHERE idPrescricao_medica = @idPrescricao_medica";


                        // Adicionando parâmetros
                        cmd.Parameters.AddWithValue("@estado_", estado_prescricao);
                        cmd.Parameters.AddWithValue("@descricao", textBox1.Text);
                        cmd.Parameters.AddWithValue("@idPrescricao_medica", idPrescricao_medica);                      
                        cmd.ExecuteNonQuery();

                    

                    // Limpando os campos e atualizando a exibição dos dados
                    LimparTextBoxes();
                    display_data();
                    LoadComboBox();

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
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Console.WriteLine("entrou1");

            if (e.RowIndex >= 0)
            {
                Console.WriteLine("entrou2");
                int idconsultaParaEditar = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["idConsulta"].Value);
                PreencherCamposParaEdicao(idconsultaParaEditar);
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
                if (conexao.State != ConnectionState.Open)
                {
                    conexao.Open();
                }

                MySqlCommand cmd = conexao.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT c.idConsulta,u.nome AS Utente,c.Utente_idUtente,c.data,c.estado,c.relatorio,p.descricao,p.estado AS \"estado da prescricao\" FROM mydb.consulta c JOIN mydb.utente u ON c.Utente_idUtente = u.idUtente JOIN mydb.medico m ON c.Medico_idMedico = m.idMedico LEFT JOIN mydb.prescricao_medica p ON p.Consulta_idConsulta = c.idConsulta WHERE c.idconsulta = @idconsulta;";
                cmd.Parameters.AddWithValue("@idconsulta", idconsultarParaEditar);
                DataTable dta = new DataTable();
                MySqlDataAdapter dataadapter = new MySqlDataAdapter(cmd);
                dataadapter.Fill(dta);
                if (dta.Rows.Count > 0)
                {
                    // Assuming that Medico_idMedico is of integer type, you may need to cast it accordingly
                    idutente = Convert.ToInt32(dta.Rows[0]["Utente_idUtente"]);
                    nome_utente = Convert.ToString(dta.Rows[0]["Utente"]);
                    estado_consulta = Convert.ToString(dta.Rows[0]["estado"]);
                    estado_prescricao = Convert.ToString(dta.Rows[0]["estado da prescricao"]);
                    relatorio_ = Convert.ToString(dta.Rows[0]["relatorio"]);
                    descricao_ = Convert.ToString(dta.Rows[0]["descricao"]);
                    Console.WriteLine("idu " + idutente);

                }
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        // Atualize a variável idUtente com o valor do idUtenteParaEditar
                        idConsulta = idconsultarParaEditar;

                        // Preenche os campos com os dados do utente
                        comboBox1.Text = estado_prescricao;
                        comboBox_utente.Text = nome_utente;
                        comboBox_consulta.Text = estado_consulta;
                        textBox_relatorio.Text = relatorio_;
                        textBox1.Text = descricao_;
                        dateTimePicker_data.Value = Convert.ToDateTime(reader["data"]);

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
        private void button3_Click(object sender, EventArgs e)
        {
            
            if (dataGridView1.SelectedRows.Count > 0)
            {
                try
                {
                    
                    int rowIndex = dataGridView1.SelectedRows[0].Index;
                    int idconsulta = Convert.ToInt32(dataGridView1.Rows[rowIndex].Cells["idConsulta"].Value);
                    int idprescricao = idPrescricao_medica;


                    using (MySqlConnection conexao = new MySqlConnection("Server=projetolar3idade.mysql.database.azure.com;Port=3306;Database=mydb;Uid=projeto4461045279;Pwd=Ipbcurso1"))
                    {
                        conexao.Open();

                        // Delete associated records in prescricao_medica table
                        string deletePrescricaoQuery = "DELETE FROM prescricao_medica WHERE Consulta_idConsulta = @idConsulta";
                        using (MySqlCommand deletePrescricaoCmd = new MySqlCommand(deletePrescricaoQuery, conexao))
                        {
                            deletePrescricaoCmd.Parameters.AddWithValue("@idConsulta", idconsulta);
                            deletePrescricaoCmd.ExecuteNonQuery();
                        }

                        // Now, delete the record in the consulta table
                        string deleteConsultaQuery = "DELETE FROM consulta WHERE idConsulta = @idConsulta";
                        using (MySqlCommand deleteConsultaCmd = new MySqlCommand(deleteConsultaQuery, conexao))
                        {
                            deleteConsultaCmd.Parameters.AddWithValue("@idConsulta", idconsulta);
                            deleteConsultaCmd.ExecuteNonQuery();
                        }

                        MessageBox.Show("Consulta Apagada com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        display_data();
                        LimparTextBoxes();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao excluir atividade: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecione uma atividade para excluir.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void LimparTextBoxes()
        {
            comboBox1.SelectedIndex = -1;
            comboBox_consulta.SelectedIndex = -1;
            comboBox_utente.SelectedIndex = -1;
            textBox1.Text = "";
            textBox_relatorio.Text = "";
            
        }
    }
}
