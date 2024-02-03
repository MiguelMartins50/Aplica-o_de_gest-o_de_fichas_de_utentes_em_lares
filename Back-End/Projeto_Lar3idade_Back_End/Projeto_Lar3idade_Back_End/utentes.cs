using System;
using System.Drawing;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Relational;
using System.IO;

namespace Projeto_Lar3idade_Back_End
{
    public partial class utentes : UserControl
    {

        private MySqlConnection conexao;
        private string imagePath; // Variável de membro para armazenar o caminho do arquivo selecionado
        private Dictionary<string, string> medico_ = new Dictionary<string, string>();
        private int idQuarto;
        private int idMedico;
        private string medico_nome;
        private int idUtente; // Adicione esta variável para rastrear o IdUtente

        public utentes()
        {

            InitializeComponent();
            string connectionString = "Server=localhost;Port=3306;Database=mydb;User ID=root;Password=ipbcurso";
            conexao = new MySqlConnection(connectionString);
            LoadComboBox();
            display_data();

        }

        private void LoadComboBox()
        {
            try
            {
                conexao.Open();

                // Carregar dados para a ComboBox de Quartos
                string queryQuarto = "SELECT idQuarto FROM quarto WHERE estado = 'Livre'";
                using (MySqlCommand cmdQuarto = new MySqlCommand(queryQuarto, conexao))
                {
                    using (MySqlDataReader readerQuarto = cmdQuarto.ExecuteReader())
                    {
                        while (readerQuarto.Read())
                        {
                            string idQuarto = readerQuarto["idQuarto"].ToString();
                            comboBox_quarto.Items.Add(idQuarto);
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
                            comboBox_medico.Items.Add(nomeMedico);
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

        private void button_insert_Click(object sender, EventArgs e)
        {
            try
            {
                conexao.Open();
                string grauDependencia = ObterGrauDependenciaSelecionado();
                MySqlCommand cmd = conexao.CreateCommand();
                cmd.CommandType = CommandType.Text;

                // Converta a imagem em bytes
                byte[] imageBytes = null;
                if (!string.IsNullOrEmpty(imagePath))
                {
                    using (FileStream stream = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
                    {
                        using (BinaryReader reader = new BinaryReader(stream))
                        {
                            imageBytes = reader.ReadBytes((int)stream.Length);
                        }
                    }
                }

                cmd.CommandText = "INSERT INTO mydb.utente (idUtente,Medico_idMedico, nome, numero_cc, data_validade, nif, niss, n_utenteSaude, genero, data_nascimento, idade, estado_civil, morada, localidade, cod_postal, telefone_casa, telemovel, grau_dependencia, email, senha, Quarto_idQuarto, Imagem) " +
                                  "SELECT COALESCE(MAX(idUtente), 0) + 1, @MedicoId, @Nome, @NumeroCC, @DataValidade, @Nif, @Niss, @NUtenteSaude, @Genero, @DataNascimento, @Idade, @EstadoCivil, @Morada, @Localidade, @CodPostal, @TelefoneCasa, @Telemovel, @GrauDependencia, @Email, @Senha, @QuartoId,@Imagem " +
                                  "FROM mydb.utente";

                cmd.Parameters.AddWithValue("@MedicoId", idMedico);
                cmd.Parameters.AddWithValue("@Nome", textBox_Name.Text);
                cmd.Parameters.AddWithValue("@NumeroCC", textBox_Cc.Text);
                cmd.Parameters.AddWithValue("@DataValidade", dateTimePicker_DtaValidade.Value.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@Nif", textBox_nif.Text);
                cmd.Parameters.AddWithValue("@Niss", textBox_niss.Text);
                cmd.Parameters.AddWithValue("@NUtenteSaude", textBox_nus.Text);
                cmd.Parameters.AddWithValue("@Genero", textBox_genero.Text);
                cmd.Parameters.AddWithValue("@DataNascimento", dateTimePicker_dataNascimento.Value.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@Idade", textBox_idade.Text);
                cmd.Parameters.AddWithValue("@EstadoCivil", textBox_EstCivil.Text);
                cmd.Parameters.AddWithValue("@Morada", textBox_Morada.Text);
                cmd.Parameters.AddWithValue("@Localidade", textBox_Localidade.Text);
                cmd.Parameters.AddWithValue("@CodPostal", textBox_codPostal.Text);
                cmd.Parameters.AddWithValue("@TelefoneCasa", textBox_telCasa.Text);
                cmd.Parameters.AddWithValue("@Telemovel", textBox_telemovel.Text);
                cmd.Parameters.AddWithValue("@GrauDependencia", grauDependencia);
                cmd.Parameters.AddWithValue("@Email", textBox_email.Text);
                cmd.Parameters.AddWithValue("@Senha", textBox_senha.Text);
                cmd.Parameters.AddWithValue("@QuartoId", idQuarto);
                cmd.Parameters.AddWithValue("@Imagem", imageBytes);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Utente adicionado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao adicionar utente: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conexao.Close();
            }

            LimparTextBoxes();
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

            // Limpar outras ComboBoxes e controles conforme necessário
            comboBox_medico.SelectedIndex = -1;
            comboBox_quarto.SelectedIndex = -1;

            // Limpar seleção de RadioButtons
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            radioButton3.Checked = false;
            radioButton4.Checked = false;

            //Limpar panel
            panel1.BackgroundImage = null;
        }

        private string ObterGrauDependenciaSelecionado()
        {
            if (radioButton1.Checked) return "Sem necessidade de apoio";
            if (radioButton2.Checked) return "Necessita de apoio básico";
            if (radioButton3.Checked) return "Necessita de apoio constante";
            if (radioButton4.Checked) return "Acamado apoio total";

            return string.Empty;
        }

        private void comboBox_quarto_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox_quarto.SelectedItem != null)
            {
                string selectedValue = comboBox_quarto.SelectedItem.ToString();
                idQuarto = int.Parse(selectedValue);
            }
        }

        private void comboBox_medico_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox_medico.SelectedItem != null)
            {
                string selectedValue = comboBox_medico.SelectedItem.ToString();

                if (medico_.TryGetValue(selectedValue, out string id))
                {
                    idMedico = int.Parse(id);
                }
            }
        }

        private void button_Search_Click(object sender, EventArgs e)
        {
            conexao.Open();

            MySqlCommand cmd = conexao.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT utente.*, medico.nome AS nome_medico FROM utente INNER JOIN medico ON utente.Medico_idMedico = medico.idMedico WHERE utente.nome LIKE @searchText OR SUBSTRING_INDEX(utente.nome, ' ', 1) LIKE @searchText ORDER BY utente.idUtente ASC";
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
                textBox_nif.Text = "";
                textBox_niss.Text = "";
                textBox_nus.Text = "";
                textBox_genero.Text = "";
                dateTimePicker_dataNascimento.Text = "";
                textBox_idade.Text = "";
                textBox_EstCivil.Text = "";
                textBox_Morada.Text = "";
                textBox_Localidade.Text = "";
                textBox_telCasa.Text = "";
                textBox_telemovel.Text = "";
                textBox_email.Text = "";
                textBox_senha.Text = "";
                comboBox_medico.SelectedIndex = 0;
                comboBox_quarto.SelectedIndex = 0;
            }
            else
            {
                MessageBox.Show("Nenhum resultado encontrado.");
            }

        }

        private void button_delete_Click(object sender, EventArgs e)
        {
            try
            {
                // Verifica se há uma linha selecionada no DataGridView
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    // Pergunta ao usuário se ele tem certeza de que deseja apagar o utente
                    DialogResult result = MessageBox.Show("Tem certeza que deseja apagar este utente?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    // Se o usuário confirmar a exclusão
                    if (result == DialogResult.Yes)
                    {
                        // Obtém o valor do idUtente da célula "idUtente" na linha selecionada
                        object cellValue = dataGridView1.SelectedRows[0].Cells["idUtente"].Value;

                        if (cellValue != null && cellValue != DBNull.Value)
                        {
                            // Converte o valor da célula para um inteiro
                            int idUtenteParaExcluir = Convert.ToInt32(cellValue);

                            conexao.Open();

                            MySqlCommand cmd = conexao.CreateCommand();
                            cmd.CommandType = CommandType.Text;

                            // Utilizando parâmetros para prevenir injeção de SQL
                            cmd.CommandText = "DELETE FROM mydb.utente WHERE idUtente = @IdUtente";
                            cmd.Parameters.AddWithValue("@IdUtente", idUtenteParaExcluir);

                            cmd.ExecuteNonQuery();

                            conexao.Close();

                            // Atualizando a exibição dos dados no DataGridView
                            display_data();

                            MessageBox.Show("Utente apagado com sucesso");
                        }
                        else
                        {
                            MessageBox.Show("O valor do ID do utente está vazio.");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Por favor, selecione uma linha para remover.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao apagar utente: " + ex.Message);
            }
        }


        private void display_data()
        {
            conexao.Open();
            MySqlCommand cmd = conexao.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT medico.nome AS nome_medico, utente.* FROM utente INNER JOIN medico ON utente.Medico_idMedico = medico.idMedico ORDER BY utente.idUtente ASC;";

            cmd.ExecuteNonQuery();
            DataTable dta = new DataTable();
            MySqlDataAdapter dataadapter = new MySqlDataAdapter(cmd);
            dataadapter.Fill(dta);
            if (dta.Rows.Count > 0)
            {
                // Assuming that Medico_idMedico is of integer type, you may need to cast it accordingly
                idMedico = Convert.ToInt32(dta.Rows[0]["Medico_idMedico"]);
                medico_nome = Convert.ToString(dta.Rows[0]["nome_medico"]);
            }
            dataGridView1.DataSource = dta;
            conexao.Close();
        }


        private void Mostrar_Click(object sender, EventArgs e)
        {
            display_data();
            LimparTextBoxes();
        }

        private void button_update_Click(object sender, EventArgs e)
        {
            try
            {
                conexao.Open();

                MySqlCommand cmd = conexao.CreateCommand();
                cmd.CommandType = CommandType.Text;

                // Certifique-se de fornecer o valor do IdUtente a ser atualizado
                if (!string.IsNullOrEmpty(textBox_Name.Text) && !string.IsNullOrEmpty(textBox_Cc.Text))
                {
                    // Converta a imagem em bytes
                    byte[] imageBytes = null;
                    if (!string.IsNullOrEmpty(imagePath))
                    {
                        using (FileStream stream = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
                        {
                            using (BinaryReader reader = new BinaryReader(stream))
                            {
                                imageBytes = reader.ReadBytes((int)stream.Length);
                            }
                        }
                    }

                    // Obtém o valor do idUtente dos TextBoxes
                    // Utilizando parâmetros para prevenir injeção de SQL
                    cmd.CommandText = "UPDATE mydb.utente SET Medico_idMedico = @MedicoId, nome = @Nome, numero_cc = @NumeroCC, data_validade = @DataValidade, nif = @Nif, niss = @Niss, n_utenteSaude = @NUtenteSaude, genero = @Genero, data_nascimento = @DataNascimento, idade = @Idade, estado_civil = @EstadoCivil, morada = @Morada, localidade = @Localidade, cod_postal = @CodPostal, telefone_casa = @TelefoneCasa, telemovel = @Telemovel, grau_dependencia = @GrauDependencia, email = @Email, senha = @Senha, Quarto_idQuarto = @QuartoId, Imagem =@Imagem WHERE idUtente = @IdUtente";

                    // Adicionando parâmetros
                    cmd.Parameters.AddWithValue("@MedicoId", idMedico);
                    cmd.Parameters.AddWithValue("@Nome", textBox_Name.Text);
                    cmd.Parameters.AddWithValue("@NumeroCC", textBox_Cc.Text);
                    cmd.Parameters.AddWithValue("@DataValidade", dateTimePicker_DtaValidade.Value.ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@Nif", textBox_nif.Text);
                    cmd.Parameters.AddWithValue("@Niss", textBox_niss.Text);
                    cmd.Parameters.AddWithValue("@NUtenteSaude", textBox_nus.Text);
                    cmd.Parameters.AddWithValue("@Genero", textBox_genero.Text);
                    cmd.Parameters.AddWithValue("@DataNascimento", dateTimePicker_dataNascimento.Value.ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@Idade", textBox_idade.Text);
                    cmd.Parameters.AddWithValue("@EstadoCivil", textBox_EstCivil.Text);
                    cmd.Parameters.AddWithValue("@Morada", textBox_Morada.Text);
                    cmd.Parameters.AddWithValue("@Localidade", textBox_Localidade.Text);
                    cmd.Parameters.AddWithValue("@CodPostal", textBox_codPostal.Text);
                    cmd.Parameters.AddWithValue("@TelefoneCasa", textBox_telCasa.Text);
                    cmd.Parameters.AddWithValue("@Telemovel", textBox_telemovel.Text);
                    cmd.Parameters.AddWithValue("@GrauDependencia", ObterGrauDependenciaSelecionado());
                    cmd.Parameters.AddWithValue("@Email", textBox_email.Text);
                    cmd.Parameters.AddWithValue("@Senha", textBox_senha.Text);
                    cmd.Parameters.AddWithValue("@QuartoId", idQuarto);
                    cmd.Parameters.AddWithValue("@IdUtente", idUtente);
                    cmd.Parameters.AddWithValue("@Imagem", imageBytes);
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

 
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                object cellValue = dataGridView1.Rows[e.RowIndex].Cells["idUtente"].Value;
                if (cellValue != DBNull.Value)
                {
                    int idUtenteParaEditar = Convert.ToInt32(cellValue);
                    PreencherCamposParaEdicao(idUtenteParaEditar);
                }
                else
                {
                // Lidar com o caso em que o valor da célula é DBNull
                 MessageBox.Show("O valor do ID do utente está vazio.");
                }
            }
            
        }
        private void PreencherCamposParaEdicao(int idUtenteParaEditar)
        {
            try
            {
                conexao.Open();

                MySqlCommand cmd = conexao.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT utente.*, medico.nome AS nome_medico FROM utente INNER JOIN medico ON utente.Medico_idMedico = medico.idMedico WHERE idUtente = @IdUtente";
                cmd.Parameters.AddWithValue("@IdUtente", idUtenteParaEditar);
                DataTable dta = new DataTable();
                MySqlDataAdapter dataadapter = new MySqlDataAdapter(cmd);
                dataadapter.Fill(dta);
                if (dta.Rows.Count > 0)
                {
                    // Assuming that Medico_idMedico is of integer type, you may need to cast it accordingly
                    idMedico = Convert.ToInt32(dta.Rows[0]["Medico_idMedico"]);
                    medico_nome = Convert.ToString(dta.Rows[0]["nome_medico"]);
                }
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        // Atualize a variável idUtente com o valor do idUtenteParaEditar
                        idUtente = idUtenteParaEditar;

                        // Preenche os campos com os dados do utente
                        comboBox_medico.Text = medico_nome;
                        textBox_Name.Text = reader["nome"].ToString();
                        textBox_Cc.Text = reader["numero_cc"].ToString();
                        dateTimePicker_DtaValidade.Value = Convert.ToDateTime(reader["data_validade"]);
                        textBox_nif.Text = reader["nif"].ToString();
                        textBox_niss.Text = reader["niss"].ToString();
                        textBox_nus.Text = reader["n_utenteSaude"].ToString();
                        textBox_genero.Text = reader["genero"].ToString();
                        dateTimePicker_dataNascimento.Value = Convert.ToDateTime(reader["data_nascimento"]);
                        textBox_idade.Text = reader["idade"].ToString();
                        textBox_EstCivil.Text = reader["estado_civil"].ToString();
                        textBox_Morada.Text = reader["morada"].ToString();
                        textBox_Localidade.Text = reader["localidade"].ToString();
                        textBox_codPostal.Text = reader["cod_postal"].ToString();
                        textBox_telCasa.Text = reader["telefone_casa"].ToString();
                        textBox_telemovel.Text = reader["telemovel"].ToString();
                        SelecionarRadioButtonPorGrauDependencia(reader["grau_dependencia"].ToString());
                        textBox_email.Text = reader["email"].ToString();
                        textBox_senha.Text = reader["senha"].ToString();
                        comboBox_quarto.Text = reader["Quarto_idQuarto"].ToString();

                        // Carregar imagem no Panel1
                        if (reader["Imagem"] != DBNull.Value)
                        {
                            byte[] imageBytes = (byte[])reader["Imagem"];
                            using (MemoryStream ms = new MemoryStream(imageBytes))
                            {
                                Image image = Image.FromStream(ms);
                                panel1.BackgroundImage = image;
                                panel1.BackgroundImageLayout = ImageLayout.Zoom; // Redimensiona a imagem para caber no controle Panel
                            }
                        }
                        else
                        {
                            panel1.BackgroundImage = null; // Limpar o Panel1 se não houver imagem
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao obter dados do utente para edição: " + ex.Message);
            }
            finally
            {
                conexao.Close();
            }
        }

        private void SelecionarRadioButtonPorGrauDependencia(string grauDependencia)
        {
            // Seleciona o RadioButton de acordo com o grau de dependência
            switch (grauDependencia)
            {
                case "Sem necessidade de apoio":
                    radioButton1.Checked = true;
                    break;
                case "Necessita de apoio básico":
                    radioButton2.Checked = true;
                    break;
                case "Necessita de apoio constante":
                    radioButton3.Checked = true;
                    break;
                case "Acamado Apoio total":
                    radioButton4.Checked = true;
                    break;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            // Defina o filtro de arquivo para imagens apenas
            openFileDialog1.Filter = "Arquivos de Imagem|*.jpg;*.jpeg;*.png;*.gif;*.bmp";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // Obtenha o caminho do arquivo selecionado
                imagePath = openFileDialog1.FileName;

                try
                {
                    // Carregue a imagem no controle Panel
                    Image image = Image.FromFile(imagePath);
                    panel1.BackgroundImage = image;
                    panel1.BackgroundImageLayout = ImageLayout.Zoom; // Redimensiona a imagem para caber no controle Panel
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao carregar a imagem: " + ex.Message);
                }
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
          
        }
    }
}

