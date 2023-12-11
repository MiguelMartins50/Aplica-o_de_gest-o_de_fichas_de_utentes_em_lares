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
    public partial class add_utente : UserControl
    {
        private int idAdd;

        private MySqlConnection conexao;

        public event EventHandler ButtonClicked;
        private Dictionary<string, string> medico_ = new Dictionary<string, string>();
        private int idQuarto;
        private int idMedico ;

        public add_utente()
        {
            InitializeComponent();
            string connectionString = "Server=localhost;Port=3306;Database=mydb;User ID=root;Password=ipbcurso";
            conexao = new MySqlConnection(connectionString);
            LoadComboBox();
        }

        private void LoadComboBox()
        {

          
            using (conexao)
            {
                conexao.Open();

                string query = "SELECT idQuarto FROM quarto JOIN(    SELECT Quarto_idQuarto, COUNT(*) AS entry_count FROM utente GROUP BY Quarto_idQuarto) utente ON quarto.idQuarto = utente.Quarto_idQuarto WHERE quarto.estado = 'Livre' AND utente.entry_count <= quarto.quantidade_cama; ";
                using (MySqlCommand cmd = new MySqlCommand(query, conexao))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            string id = reader["idQuarto"].ToString();
                            comboBox1.Items.Add(id);
                        }
                    }
                }
                string query2 = "SELECT idMedico,nome FROM medico";
                using (MySqlCommand cmd = new MySqlCommand(query2, conexao))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            string id = reader["idMedico"].ToString();
                            string nome = reader["nome"].ToString();
                            comboBox2.Items.Add(nome);
                            medico_[nome] = id;


                        }
                    }
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            ButtonClicked?.Invoke(this, EventArgs.Empty);

            string nome = textBox1.Text;
            DateTime dataNascimento = dateTimePicker1.Value;
            string idade = textBox2.Text;
            string estadoCivil = textBox16.Text;
            string genero = textBox3.Text;
            string numeroCartaoCidadao = textBox5.Text;
            DateTime dataValidade = dateTimePicker2.Value;
            string nif = textBox6.Text;
            string niss = textBox7.Text;
            string nus = textBox8.Text;
            string morada = textBox9.Text;
            string cod_postal = textBox10.Text;
            string localidade = textBox11.Text;
            string telefoneCasa = textBox12.Text;
            string telemovel = textBox13.Text;
            string email = textBox14.Text;
            string senha = textBox15.Text;
            string grauDependencia = ObterGrauDependenciaSelecionado();

            


            try
            {
                using (MySqlConnection conexao = new MySqlConnection("Server=localhost;Port=3306;Database=mydb;User ID=root;Password=ipbcurso"))
                {
                    conexao.Open();
                    string query = "INSERT INTO mydb.utente (idUtente,Medico_idMedico, nome, numero_cc, data_validade, nif, niss, n_utenteSaude, genero, data_nascimento, idade, estado_civil, morada, localidade, cod_postal, telefone_casa, telemovel, grau_dependencia, email, senha, Quarto_idQuarto)" +
                                    "VALUES (@idUtente, @MedicoId, @Nome, @NumeroCC, @DataValidade, @Nif, @Niss, @NUtenteSaude, @Genero, @DataNascimento, @Idade, @EstadoCivil, @Morada, @Localidade, @CodPostal, @TelefoneCasa, @Telemovel, @GrauDependencia, @Email, @Senha, @QuartoId)";
                    string query2 = "SELECT * FROM utente ORDER BY idUtente DESC LIMIT 1";

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
                                idAdd = 1 + int.Parse(reader["idUtente"].ToString());

                            }
                        }
                    }
                    

                    // Crie um comando MySqlCommand
                    using (MySqlCommand comando = new MySqlCommand(query, conexao))
                    {
                        comando.Parameters.AddWithValue("@idUtente", idAdd);
                        comando.Parameters.AddWithValue("@MedicoId", idMedico);
                        comando.Parameters.AddWithValue("@Nome", nome);
                        comando.Parameters.AddWithValue("@NumeroCC", numeroCartaoCidadao);
                        comando.Parameters.AddWithValue("@DataValidade", dataValidade);
                        comando.Parameters.AddWithValue("@Nif", nif);
                        comando.Parameters.AddWithValue("@Niss", niss);
                        comando.Parameters.AddWithValue("@NUtenteSaude", nus);
                        comando.Parameters.AddWithValue("@Genero", genero);
                        comando.Parameters.AddWithValue("@DataNascimento", dataNascimento);
                        comando.Parameters.AddWithValue("@Idade", idade);
                        comando.Parameters.AddWithValue("@EstadoCivil", estadoCivil);
                        comando.Parameters.AddWithValue("@Morada", morada);
                        comando.Parameters.AddWithValue("@Localidade", localidade);
                        comando.Parameters.AddWithValue("@CodPostal", cod_postal);
                        comando.Parameters.AddWithValue("@TelefoneCasa", telefoneCasa);
                        comando.Parameters.AddWithValue("@Telemovel", telemovel);
                        comando.Parameters.AddWithValue("@GrauDependencia", grauDependencia);
                        comando.Parameters.AddWithValue("@Email", email);
                        comando.Parameters.AddWithValue("@Senha", senha);
                        comando.Parameters.AddWithValue("@QuartoId", idQuarto);

                        comando.ExecuteNonQuery();

                        MessageBox.Show("Utente adicionado com sucesso!");

                        ButtonClicked?.Invoke(this, EventArgs.Empty);

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao adicionar utente: " + ex.Message);
            }
        }

        private string ObterGrauDependenciaSelecionado()
        {
            if (radioButton1.Checked) return "Sem necessidade de apoio";
            if (radioButton2.Checked) return "Necessita de apoio básico";
            if (radioButton3.Checked) return "Necessita de apoio constante";
            if (radioButton4.Checked) return "Acamado apoio total";

            return string.Empty;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null)
            {
                string selectedValue = comboBox1.SelectedItem.ToString();
                idQuarto = int.Parse(selectedValue.ToString());

            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null)
            {
                string selectedValue = comboBox2.SelectedItem.ToString();

                if (medico_.TryGetValue(selectedValue, out string id))
                {
                   
                    // Use specificColumnData as needed (e.g., assign it to a variable)
                    idMedico = int.Parse(id.ToString());
                    Console.WriteLine(idMedico);
                }
            }
        }
    }

}


