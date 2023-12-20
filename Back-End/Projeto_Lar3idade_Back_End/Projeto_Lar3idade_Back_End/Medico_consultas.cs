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
        private int idMedico;
        private int idPrescricao_medica;
        private string estado_prescricao = "";
        private string estado_consulta = "";
        private string nome_utente = "";
        private MySqlConnection conexao;
        public Medico_consultas(int userID)
        {
            InitializeComponent();
            string connectionString = "Server=localhost;Port=3306;Database=mydb;User ID=root;Password=ipbcurso";
            conexao = new MySqlConnection(connectionString);
            this.user_ID = userID;
            LoadComboBox();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                conexao.Open();

                // Inserir dados na tabela utente
                string insertUtenteQuery = "INSERT INTO utente (idUtente, nome) VALUES (@idUtente, @nome)";
                using (MySqlCommand cmdUtente = new MySqlCommand(insertUtenteQuery, conexao))
                {
                    cmdUtente.Parameters.AddWithValue("@idUtente", idutente);
                    cmdUtente.Parameters.AddWithValue("@nome", comboBox_utente.SelectedItem.ToString());
                    cmdUtente.ExecuteNonQuery();
                }

                // Inserir dados na tabela consulta
                string insertConsultaQuery = "INSERT INTO consulta (idConsulta, Utente_idUtente, Medico_idMedico, data, estado, relatorio) " +
                                            "VALUES (@idConsulta, @Utente_idUtente, @Medico_idMedico, @data, @estado, @relatorio)";
                using (MySqlCommand cmdConsulta = new MySqlCommand(insertConsultaQuery, conexao))
                {
                    cmdConsulta.Parameters.AddWithValue("@idConsulta", idConsulta);
                    cmdConsulta.Parameters.AddWithValue("@Utente_idUtente", idutente);
                    cmdConsulta.Parameters.AddWithValue("@Medico_idMedico", idMedico);
                    cmdConsulta.Parameters.AddWithValue("@data", dateTimePicker_data);  // Substitua com a data desejada
                    cmdConsulta.Parameters.AddWithValue("@estado", comboBox_consulta);
                    cmdConsulta.Parameters.AddWithValue("@relatorio", textBox_relatorio);  // Substitua com o relatório desejado
                    cmdConsulta.ExecuteNonQuery();
                }

                // Inserir dados na tabela prescricao_medica
                string insertPrescricaoQuery = "INSERT INTO prescricao_medica (idPrescricao_medica, estado, descricao) " +
                                               "VALUES (@idPrescricao_medica, @estadoPrescricao, @descricao)";
                using (MySqlCommand cmdPrescricao = new MySqlCommand(insertPrescricaoQuery, conexao))
                {
                    cmdPrescricao.Parameters.AddWithValue("@idPrescricao_medica", idPrescricao_medica);
                    cmdPrescricao.Parameters.AddWithValue("@estadoPrescricao", comboBox1);
                    cmdPrescricao.Parameters.AddWithValue("@descricao", textBox1);  // Substitua com a descrição desejada
                    cmdPrescricao.ExecuteNonQuery();
                }

                MessageBox.Show("Dados inseridos com sucesso.");
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
                            comboBox_utente.Items.Add(utenteNome);
                            utente_[utenteNome] = idUtente;
                        }
                    }
                }

                // Carregar dados para a ComboBox 
                string queryConsulta = "SELECT idConsulta, estado FROM consulta";
                using (MySqlCommand cmdConsulta = new MySqlCommand(queryConsulta, conexao))
                {
                    using (MySqlDataReader readerConsulta = cmdConsulta.ExecuteReader())
                    {
                        while (readerConsulta.Read())
                        {
                            string idConsulta = readerConsulta["idConsulta"].ToString();
                            string estadoConsulta = readerConsulta["estado"].ToString();
                            comboBox_consulta.Items.Add(estadoConsulta);
                            consulta_[estadoConsulta] = idConsulta;
                        }
                    }
                }

                string queryprecricao = "SELECT idPrescricao_medica, estado FROM prescricao_medica";
                using (MySqlCommand cmdPrescricao = new MySqlCommand(queryprecricao, conexao))
                {
                    using (MySqlDataReader readerPrescriacao = cmdPrescricao.ExecuteReader())
                    {
                        while (readerPrescriacao.Read())
                        {
                            string idPrescricao_medica = readerPrescriacao["idPrescricao_medica"].ToString();
                            string estadoPrescricao = readerPrescriacao["estado"].ToString();
                            comboBox1.Items.Add(estadoPrescricao);
                            prescricao_[estadoPrescricao] = idPrescricao_medica;
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
                string selectedValue = comboBox_consulta.SelectedItem.ToString();

                if (consulta_.TryGetValue(selectedValue, out string id))
                {
                    idConsulta = int.Parse(id);
                }
            }

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null)
            {
                string selectedValue = comboBox1.SelectedItem.ToString();

                if (prescricao_.TryGetValue(selectedValue, out string id))
                {
                    idPrescricao_medica = int.Parse(id);
                }
            }

        }
    }
}
