using System;
using System.Drawing; // manipulação de imagem
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Relational;
using System.IO; // operações com arquivos

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
        private string genero = "";
        int numeroQ = 0;
        public utentes()
        {

            InitializeComponent();
            string connectionString = "Server=projetolar3idade.mysql.database.azure.com;Port=3306;Database=mydb;Uid=projeto4461045279;Pwd=Ipbcurso1";
            conexao = new MySqlConnection(connectionString);
            comboBox1.Items.Add("Masculino");
            comboBox1.Items.Add("Feminino");
            textBox_senha.PasswordChar = '*';
            LoadComboBox();
            display_data();

        }

        private void LoadComboBox()
        {
            try
            {
                conexao.Open();

                // Carregar dados para a ComboBox de Quartos
                string queryQuarto = "SELECT idQuarto , Numero FROM quarto WHERE estado = 'Livre'";
                using (MySqlCommand cmdQuarto = new MySqlCommand(queryQuarto, conexao))
                {
                    using (MySqlDataReader readerQuarto = cmdQuarto.ExecuteReader())
                    {
                        while (readerQuarto.Read())
                        {
                            string idQuarto = readerQuarto["idQuarto"].ToString();
                            string NQuarto = readerQuarto["Numero"].ToString();
                            string titulo = "ID:" + idQuarto + " Nº:" + NQuarto;
                            comboBox_quarto.Items.Add(titulo);
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
            int controlo = 0;
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
                cmd.Parameters.AddWithValue("@Genero", genero);
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
                MySqlCommand getMaxIdCmd = new MySqlCommand("SELECT COALESCE(MAX(idUtente), 0) FROM mydb.utente", conexao);
                int Novoid = Convert.ToInt32(getMaxIdCmd.ExecuteScalar());

                // After retrieving Novoid
                Console.WriteLine("Novoid: " + Novoid);
                int contar = 0;
                int ocupacao = 0;
                // Before executing queryQ
                Console.WriteLine("Query para idUtente: " + Novoid);
                int quartoid = 0;
                string queryQ = "SELECT Quarto_idQuarto FROM utente WHERE idUtente = @idUtente;";
                using (MySqlCommand cmdQ = new MySqlCommand(queryQ, conexao))
                {
                    cmdQ.Parameters.AddWithValue("@idUtente", Novoid);
                    Console.WriteLine("Aqui1");
                    using (MySqlDataReader readerQ = cmdQ.ExecuteReader())
                    {
                        Console.WriteLine("Aqui2");
                        Console.WriteLine("ReaderQ HasRows: " + readerQ.HasRows);

                        while (readerQ.Read())
                        {
                            Console.WriteLine("Aqui3");

                            Console.WriteLine("Quarto_idQuarto:::::::");

                            quartoid = Convert.ToInt32(readerQ["Quarto_idQuarto"]);
                            Console.WriteLine("QUARTOID:::::::" + quartoid);

                           
                        }
                    }
                }

                string queryQuarto = "SELECT COUNT(u.idUtente) AS ocupacao, q.quantidade_cama FROM quarto q LEFT JOIN utente u ON q.idQuarto = u.Quarto_idQuarto WHERE q.idQuarto = @quartoid;";
                using (MySqlCommand cmdQuarto = new MySqlCommand(queryQuarto, conexao))
                {
                    Console.WriteLine("Quartoid::::" + quartoid);
                    cmdQuarto.Parameters.AddWithValue("@quartoid", quartoid);
                    using (MySqlDataReader readerQuarto = cmdQuarto.ExecuteReader())
                    {

                        Console.WriteLine("Entering the while loop");
                        if (readerQuarto.HasRows)
                        {
                            Console.WriteLine("Reader has rows");
                            while (readerQuarto.Read())
                            {
                                Console.WriteLine("Inside the while loop");
                                ocupacao = Convert.ToInt32(readerQuarto["ocupacao"].ToString());
                                Console.WriteLine("Numero:::::" + ocupacao);
                                contar = ocupacao;
                                Console.WriteLine("CONTAR::::::" + contar);
                                int quantidade_cama = Convert.ToInt32(readerQuarto["quantidade_cama"].ToString());

                                // Check if contar is greater than or equal to quantidade_cama
                                if (contar > quantidade_cama)
                                {
                                    // If yes, set contar to quantidade_cama
                                    contar = quantidade_cama;
                                    MessageBox.Show("O Utente não foi assignado um quarto porque o quarto ja tem capacidade completa!\nTem que associar um quarto a este utente na secção de quartos!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    controlo = 1;
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("Reader has no rows");
                        }
                    }
                }
                string updateQuery = "UPDATE quarto SET ocupacao = @Ocupacao WHERE idQuarto = @QuartoId";
                using (MySqlCommand updateCmd = new MySqlCommand(updateQuery, conexao))
                {
                    updateCmd.Parameters.AddWithValue("@Ocupacao", contar);
                    updateCmd.Parameters.AddWithValue("@QuartoId", quartoid);
                    updateCmd.ExecuteNonQuery();
                }
                if(controlo == 1)
                {
                    string resetQuartoIdQuery = "UPDATE utente SET Quarto_idQuarto = 0  WHERE idUtente = @idUtente";
                    using (MySqlCommand resetQuartoIdCmd = new MySqlCommand(resetQuartoIdQuery, conexao))
                    {
                        resetQuartoIdCmd.Parameters.AddWithValue("@idUtente", Novoid);
                        resetQuartoIdCmd.ExecuteNonQuery();
                    }
                }
                
                MessageBox.Show("Utente adicionado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                conexao.Close();
                display_data();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao adicionar utente: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine("Erro ao adicionar utente"+ex.Message);
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
                string selectedTitle = comboBox_quarto.SelectedItem.ToString();
                string[] parts = selectedTitle.Split(' '); 
                string idQuartoPart = parts[0].Substring(3); 
                idQuarto = int.Parse(idQuartoPart); 
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
                comboBox1.SelectedIndex = 0;
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
                            int quartoid = 0;
                            conexao.Open();
                            string queryQ = "SELECT Quarto_idQuarto FROM utente WHERE idUtente = @idUtente;";
                            using (MySqlCommand cmdQ = new MySqlCommand(queryQ, conexao))
                            {
                                cmdQ.Parameters.AddWithValue("@idUtente", idUtenteParaExcluir);
                                Console.WriteLine("Aqui1");

                                using (MySqlDataReader readerQ = cmdQ.ExecuteReader())
                                {
                                    Console.WriteLine("Aqui2");

                                    while (readerQ.Read())
                                    {
                                        Console.WriteLine("Aqui3");

                                        Console.WriteLine("Quarto_idQuarto:::::::");

                                        quartoid = Convert.ToInt32(readerQ["Quarto_idQuarto"]);

                                        Console.WriteLine("QUARTOID:::::::" + quartoid);
                                    }
                                }
                            }
                            int contar = 0;
                            int ocupacao = 0;
                            string queryQuarto = "SELECT COUNT(u.idUtente) AS ocupacao FROM quarto q LEFT JOIN utente u ON q.idQuarto = u.Quarto_idQuarto where q.idQuarto = @quartoid;";
                            using (MySqlCommand cmdQuarto = new MySqlCommand(queryQuarto, conexao))
                            {
                                Console.WriteLine("Quartoid::::" + quartoid);
                                cmdQuarto.Parameters.AddWithValue("@quartoid", quartoid);
                                using (MySqlDataReader readerQuarto = cmdQuarto.ExecuteReader())
                                {

                                    Console.WriteLine("Entering the while loop");
                                    if (readerQuarto.HasRows)
                                    {
                                        Console.WriteLine("Reader has rows");
                                        while (readerQuarto.Read())
                                        {
                                            Console.WriteLine("Inside the while loop");
                                            ocupacao = Convert.ToInt32(readerQuarto["ocupacao"].ToString());
                                            Console.WriteLine("Numero:::::" + ocupacao);
                                            contar = ocupacao - 1;
                                            Console.WriteLine("CONTAR::::::" + contar);

                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Reader has no rows");
                                    }
                                }
                            }
                            MySqlCommand cmd = conexao.CreateCommand();
                            cmd.CommandType = CommandType.Text;

                            // Utilizando parâmetros para prevenir injeção de SQL
                            cmd.CommandText = "DELETE FROM mydb.utente WHERE idUtente = @IdUtente";
                            cmd.Parameters.AddWithValue("@IdUtente", idUtenteParaExcluir);

                            cmd.ExecuteNonQuery();
                            
                           
                            
                            string updateQuery = "UPDATE quarto SET ocupacao = @Ocupacao WHERE idQuarto = @QuartoId";
                            using (MySqlCommand updateCmd = new MySqlCommand(updateQuery, conexao))
                            {
                                updateCmd.Parameters.AddWithValue("@Ocupacao", contar);
                                updateCmd.Parameters.AddWithValue("@QuartoId", quartoid);
                                updateCmd.ExecuteNonQuery();
                            }
                            conexao.Close();

                            // Atualizando a exibição dos dados no DataGridView
                            display_data();
                            LimparTextBoxes();

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
            cmd.CommandText = "SELECT u.idUtente, u.nome AS Nome,m.nome AS Medico, u.numero_cc, " +
                "u.data_validade,u.nif, u.niss, u.n_utenteSaude, u.genero,  u.data_nascimento,  u.idade, u.estado_civil," +
                "u.morada, u.localidade, u.cod_postal, u.telefone_casa, u.telemovel, u.grau_dependencia, u.email,  '****' as senha,   " +
                "q.Numero AS numero_quarto,u.Quarto_idQuarto, u.Imagem, u.Medico_idMedico FROM utente u JOIN medico m ON u.Medico_idMedico = m.idMedico JOIN quarto q " +
                "ON u.Quarto_idQuarto = q.idQuarto;";

            cmd.ExecuteNonQuery();
            DataTable dta = new DataTable();
            MySqlDataAdapter dataadapter = new MySqlDataAdapter(cmd);
            dataadapter.Fill(dta);
            dta.DefaultView.Sort = "idUtente ASC";
            if (dta.Rows.Count > 0)
            {
                // Assuming that Medico_idMedico is of integer type, you may need to cast it accordingly
                idMedico = Convert.ToInt32(dta.Rows[0]["Medico_idMedico"]);
                medico_nome = Convert.ToString(dta.Rows[0]["Medico"]);
            }
            dataGridView1.DataSource = dta;
            dataGridView1.Columns["Quarto_idQuarto"].Visible = false;

            ResizeDataGridViewColumnImages("Imagem");

            conexao.Close();
        }
        private void ResizeDataGridViewColumnImages(string columnName)
        {
            // Check if the specified column exists
            if (dataGridView1.Columns.Contains(columnName))
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    // Get the value from the cell in the specified column
                    object cellValue = row.Cells[columnName].Value;

                    // Check if the cell value is not null and is of type byte[]
                    if (cellValue != null && cellValue.GetType() == typeof(byte[]))
                    {
                        byte[] imageData = (byte[])cellValue;

                        // Convert the byte array to an Image
                        Image originalImage;
                        using (MemoryStream ms = new MemoryStream(imageData))
                        {
                            originalImage = Image.FromStream(ms);
                        }

                        // Resize the image to 30x30 pixels
                        Image resizedImage = new Bitmap(60, 60);
                        using (Graphics g = Graphics.FromImage(resizedImage))
                        {
                            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                            g.DrawImage(originalImage, 0, 0, 60, 60);
                        }

                        // Set the resized image back to the cell
                        row.Cells[columnName].Value = resizedImage;
                    }
                    else
                    {
                        // If the cell value is null or not of type byte[], set the cell value to null
                        row.Cells[columnName].Value = null;
                    }
                }
            }
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
                    cmd.Parameters.AddWithValue("@Genero", genero);
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
                    int contar = 0;
                    int ocupacao = 0;
                    int controlo = 0;
                    string queryQuarto = "SELECT COUNT(u.idUtente) AS ocupacao, q.quantidade_cama FROM quarto q LEFT JOIN utente u ON q.idQuarto = u.Quarto_idQuarto WHERE q.idQuarto = @quartoid;";
                    using (MySqlCommand cmdQuarto = new MySqlCommand(queryQuarto, conexao))
                    {
                        Console.WriteLine("Quartoid::::" + idQuarto);
                        cmdQuarto.Parameters.AddWithValue("@quartoid", idQuarto);
                        using (MySqlDataReader readerQuarto = cmdQuarto.ExecuteReader())
                        {

                            Console.WriteLine("Entering the while loop");
                            if (readerQuarto.HasRows)
                            {
                                Console.WriteLine("Reader has rows");
                                while (readerQuarto.Read())
                                {
                                    Console.WriteLine("Inside the while loop");
                                    ocupacao = Convert.ToInt32(readerQuarto["ocupacao"].ToString());
                                    Console.WriteLine("Numero:::::" + ocupacao);
                                    contar = ocupacao;
                                    Console.WriteLine("CONTAR::::::" + contar);
                                    int quantidade_cama = Convert.ToInt32(readerQuarto["quantidade_cama"].ToString());

                                    // Check if contar is greater than or equal to quantidade_cama
                                    if (contar > quantidade_cama)
                                    {
                                        // If yes, set contar to quantidade_cama
                                        contar = quantidade_cama;
                                        MessageBox.Show("O Utente não foi assignado um quarto porque o quarto ja tem capacidade completa!\nTem que associar um quarto a este utente na secção de quartos!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        controlo = 1;
                                    }
                                }
                            }
                            else
                            {
                                Console.WriteLine("Reader has no rows");
                            }
                        }
                    }
                    string updateQuery = "UPDATE quarto SET ocupacao = @Ocupacao WHERE idQuarto = @QuartoId";
                    using (MySqlCommand updateCmd = new MySqlCommand(updateQuery, conexao))
                    {
                        updateCmd.Parameters.AddWithValue("@Ocupacao", contar);
                        updateCmd.Parameters.AddWithValue("@QuartoId", idQuarto);
                        updateCmd.ExecuteNonQuery();
                    }
                    if (controlo == 1)
                    {
                        string resetQuartoIdQuery = "UPDATE utente SET Quarto_idQuarto = 0  WHERE idUtente = @idUtente";
                        using (MySqlCommand resetQuartoIdCmd = new MySqlCommand(resetQuartoIdQuery, conexao))
                        {
                            resetQuartoIdCmd.Parameters.AddWithValue("@idUtente", idUtente);
                            resetQuartoIdCmd.ExecuteNonQuery();
                        }
                    }
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
                int quartoid = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Quarto_idQuarto"].Value);
                object cellValue = dataGridView1.Rows[e.RowIndex].Cells["idUtente"].Value;
                if (cellValue != DBNull.Value)
                {
                    int idUtenteParaEditar = Convert.ToInt32(cellValue);
                    PreencherCamposParaEdicao(idUtenteParaEditar ,quartoid);
                }
                else
                {
                // Lidar com o caso em que o valor da célula é DBNull
                 MessageBox.Show("O valor do ID do utente está vazio.");
                }
            }
            
        }
        private void PreencherCamposParaEdicao(int idUtenteParaEditar, int quartoid)
        {
            
            try
            {
                conexao.Open();
                Console.WriteLine("qaurtoid===" + quartoid);
                string queryQuarto = "SELECT Numero FROM quarto where idQuarto = @quartoid;";
                using (MySqlCommand cmdQuarto = new MySqlCommand(queryQuarto, conexao))
                {
                    cmdQuarto.Parameters.AddWithValue("@quartoid", quartoid);
                    using (MySqlDataReader readerQuarto = cmdQuarto.ExecuteReader())
                    {
                        Console.WriteLine("Entering the while loop");
                        if (readerQuarto.HasRows)
                        {
                            Console.WriteLine("Reader has rows");
                            while (readerQuarto.Read())
                            {
                                Console.WriteLine("Inside the while loop");
                                numeroQ = Convert.ToInt32(readerQuarto["Numero"].ToString());
                                Console.WriteLine("Numero:::::" + numeroQ);
                            }
                        }
                        else
                        {
                            Console.WriteLine("Reader has no rows");
                        }
                    }
                }
                MySqlCommand cmd = conexao.CreateCommand();

                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "   SELECT utente.*, medico.nome  AS Medico , Quarto.Numero AS Numero_Quarto FROM utente INNER JOIN medico ON utente.Medico_idMedico = medico.idMedico INNER JOIN   Quarto ON utente.Quarto_idQuarto = Quarto.idQuarto WHERE idUtente = @idUtente";
                cmd.Parameters.AddWithValue("@idUtente", idUtenteParaEditar);
                DataTable dta = new DataTable();
                MySqlDataAdapter dataadapter = new MySqlDataAdapter(cmd);
                dataadapter.Fill(dta);
                if (dta.Rows.Count > 0)
                {
                    // Assuming that Medico_idMedico is of integer type, you may need to cast it accordingly
                    idMedico = Convert.ToInt32(dta.Rows[0]["Medico_idMedico"]);
                    medico_nome = Convert.ToString(dta.Rows[0]["Medico"]);
                }
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        // Atualize a variável idUtente com o valor do idUtenteParaEditar
                        idUtente = idUtenteParaEditar;
                        idQuarto = Convert.ToInt32( reader["Quarto_idQuarto"].ToString());
                        // Preenche os campos com os dados do utente
                        comboBox_medico.Text = medico_nome;
                        textBox_Name.Text = reader["nome"].ToString();
                        textBox_Cc.Text = reader["numero_cc"].ToString();
                        dateTimePicker_DtaValidade.Value = Convert.ToDateTime(reader["data_validade"]);
                        textBox_nif.Text = reader["nif"].ToString();
                        textBox_niss.Text = reader["niss"].ToString();
                        textBox_nus.Text = reader["n_utenteSaude"].ToString();
                        comboBox1.Text = reader["genero"].ToString();
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
                        comboBox_quarto.Text = "ID:"+reader["Quarto_idQuarto"].ToString()+" Nº:"+ numeroQ;

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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null)
            {
                genero = comboBox1.SelectedItem.ToString();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (textBox_senha.PasswordChar == '*')
            {
                // Se a senha estiver oculta, mostre-a
                textBox_senha.PasswordChar = '\0'; // Caractere nulo para mostrar o texto da senha
                button2.Text = "Ocultar"; // Altera o texto do botão
            }
            else
            {
                // Se a senha estiver visível, oculte-a
                textBox_senha.PasswordChar = '*'; // Caractere '*' para ocultar a senha
                button2.Text = "Mostrar"; // Altera o texto do botão
            }
        }
    }
}

