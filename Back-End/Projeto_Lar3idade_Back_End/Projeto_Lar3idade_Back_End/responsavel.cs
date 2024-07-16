using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO; // operações com arquivos
using System.Drawing;  // manipulação de imagem
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;




namespace Projeto_Lar3idade_Back_End
{
    public partial class responsavel : UserControl
    {
        private int idAdd;
        private int idAddUt;
        private string imagePath; // Variável de membro para armazenar o caminho do arquivo selecionado

        private int idUt1;
        private int idUt2;
        private int idUt3;
        private int idUf1;
        private int idUf2;
        private int idUf3;
        private int control1;
        private int control2;
        private int control3;
        private int doublecontrol1 = 0;
        private int doublecontrol2 = 0;
        private int doublecontrol3 = 0;
        private string relacao = "";
        private Byte[] img = null;

        private List<string> Lista_utente_Familiar = new List<string>(); 

        private MySqlConnection conexao;
        private Dictionary<string, string> utente_ = new Dictionary<string, string>();

        public responsavel()
        {
            InitializeComponent();
            conexao = new MySqlConnection(DatabaseConfig.ConnectionString);
            display_data();
            LoadComboBox();
            textBox3_senha.PasswordChar = '*';

            // Adicione o evento CellClick ao DataGridView
            dataGridView1.CellClick += dataGridView1_CellClick;
            dataGridView1.CellFormatting += dataGridView1_CellFormatting;

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
        private void LoadComboBox()
        {

            comboBox1_Utente.Items.Add("-----------------");
            


            using (conexao)
            {
                string query = "SELECT idUtente,nome FROM utente";
                using (MySqlCommand cmd = new MySqlCommand(query, conexao))
                {
                    conexao.Open();
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            string nome = reader["nome"].ToString();
                            string id = reader["idUtente"].ToString();
                            comboBox1_Utente.Items.Add(reader["nome"].ToString());
                           
                            utente_[nome] = id;

                        }
                    }
                }
            }
        }

        private void button_insert_Click(object sender, EventArgs e)
        {
            // Pegue os valores dos controles do formulário
            string nomel = textBox_Name.Text;
            DateTime data_nascimento = dateTimePicker_dataNascimento.Value;
            string numero_cc = textBox_Cc.Text;
            DateTime data_validade = dateTimePicker_DtaValidade.Value;
            string morada = textBox_morada.Text;
            string cod_postal = textBox_codPostal.Text;
            string tel_casa = textBox_telCasa.Text;
            string telemovel = textBox_telemovel.Text;
            string ocupacao = textBox_ocupacao.Text;
            string email = textBox_email.Text;
            string senha = textBox3_senha.Text;

            try
            {
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
                using (conexao)
                {
                    conexao.Open();


                    string query = "INSERT INTO mydb.familiar (idFamiliar, nomel, numero_cc, data_validade, telemovel, data_nascimento,morada,cod_postal,ocupacao,tel_casa, email, senha, Imagem)" +
                                  "VALUES (@idFamiliar,@nomel, @numero_cc, @data_validade, @telemovel, @data_nascimento, @morada,@cod_postal,@ocupacao,@tel_casa,@email, @senha,@Imagem)";

                    string query2 = "SELECT * FROM familiar ORDER BY idFamiliar DESC LIMIT 1";
                    string query3 = "SELECT * FROM utente_familiar ORDER BY idUtente_familiar DESC LIMIT 1";
                    string query4 = "LOCK TABLES utente_familiar WRITE;" +
                                    "ALTER TABLE utente_familiar DISABLE KEYS;" +
                                    "INSERT INTO mydb.utente_familiar (idUtente_familiar, Utente_idUtente, Familiar_idFamiliar,parentesco)" +
                                    "VALUES (@idUtente_familiar, @Utente_idUtente, @Familiar_idFamiliar, @parentesco);" +
                                    "ALTER TABLE utente_familiar ENABLE KEYS;" +
                                    "UNLOCK TABLES;";
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
                                idAdd = 1 + int.Parse(reader["idFamiliar"].ToString());

                            }
                        }
                    }
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
                                idAddUt = 1 + int.Parse(reader["idUtente_familiar"].ToString());

                            }
                        }
                    }
                    
                   

                    // Crie um comando MySqlCommand
                    using (MySqlCommand comando = new MySqlCommand(query, conexao))
                    {
                        // Adicione os parâmetros com os valores obtidos do formulário
                        comando.Parameters.AddWithValue("@idFamiliar", idAdd);
                        comando.Parameters.AddWithValue("@nomel", nomel);
                        comando.Parameters.AddWithValue("@numero_cc", numero_cc);
                        comando.Parameters.AddWithValue("@data_validade", data_validade);
                        comando.Parameters.AddWithValue("@telemovel", telemovel);
                        comando.Parameters.AddWithValue("@data_nascimento", data_nascimento);
                        comando.Parameters.AddWithValue("@morada", morada);
                        comando.Parameters.AddWithValue("@cod_postal", cod_postal);
                        comando.Parameters.AddWithValue("@ocupacao", ocupacao);
                        comando.Parameters.AddWithValue("@tel_casa", tel_casa);
                        comando.Parameters.AddWithValue("@email", email);
                        comando.Parameters.AddWithValue("@senha", senha);
                        comando.Parameters.AddWithValue("@Imagem", imageBytes);

                        comando.ExecuteNonQuery();

                        MessageBox.Show("Responsavél adicionado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }

                    foreach (string entry in Lista_utente_Familiar)
                    {
                        // Split the entry to extract the necessary information
                        string[] parts = entry.Split(',');

                        // Extract the Utente_id and Parentesco from the parts array
                        string utenteId = parts[0].Split(':')[1].Trim();
                        string parentesco = parts[2].Split(':')[1].Trim();


                        // Create and execute the command
                        using (MySqlCommand comando = new MySqlCommand(query4, conexao))
                        {
                            comando.Parameters.AddWithValue("@idUtente_familiar", idAddUt);
                            comando.Parameters.AddWithValue("@Utente_idUtente", utenteId);
                            comando.Parameters.AddWithValue("@Familiar_idFamiliar", idAdd);
                            comando.Parameters.AddWithValue("@parentesco", parentesco);

                            comando.ExecuteNonQuery();
                            idAddUt =idAddUt + 1;
                        }
                    }


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao adicionar familiar: " + ex.Message);
            }

            LimparTextBoxes();
        }
        private void LimparTextBoxes()
        {
            textBox_Name.Clear();
            textBox_Cc.Clear();
            dateTimePicker_dataNascimento.Value = DateTime.Now;
            dateTimePicker_DtaValidade.Value = DateTime.Now;
            textBox_morada.Clear();
            textBox_codPostal.Clear();
            textBox_telCasa.Clear();
            textBox_telemovel.Clear();
            textBox_ocupacao.Clear();
            textBox_email.Clear();
            textBox3_senha.Clear();
            textBox1_parentesco.Clear();
            textBox_UtenteFamiliar.Clear();
            // Limpar outras ComboBoxes e controles conforme necessário
            comboBox1_Utente.SelectedIndex = -1;


            //Limpar panel
            panel1.BackgroundImage = null;

            Lista_utente_Familiar.Clear();


        }

        private void button_update_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    int idFamiliarParaAtualizar = ObterIdFamiliarSelecionado();
                    string nomel = textBox_Name.Text;
                    DateTime data_nascimento = dateTimePicker_dataNascimento.Value;
                    string numero_cc = textBox_Cc.Text;
                    DateTime data_validade = dateTimePicker_DtaValidade.Value;
                    string morada = textBox_morada.Text;
                    string cod_postal = textBox_codPostal.Text;
                    string tel_casa = textBox_telCasa.Text;
                    string telemovel = textBox_telemovel.Text;
                    string ocupacao = textBox_ocupacao.Text;
                    string email = textBox_email.Text;
                    string senha = textBox3_senha.Text;

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

                    using (conexao)
                    {
                        conexao.Open();
                        string queryUpdateFamiliar = "UPDATE mydb.familiar SET " +
                                                     "nomel = @nomel, " +
                                                     "numero_cc = @numero_cc, " +
                                                     "data_validade = @data_validade, " +
                                                     "telemovel = @telemovel, " +
                                                     "data_nascimento = @data_nascimento, " +
                                                     "morada = @morada, " +
                                                     "cod_postal = @cod_postal, " +
                                                     "ocupacao = @ocupacao, " +
                                                     "tel_casa = @tel_casa, " +
                                                     "email = @email, " +
                                                     "senha = @senha, " + 
                                                     "Imagem = @Imagem " + 
                                                     "WHERE idFamiliar = @idFamiliar";
                        string query3 = "SELECT * FROM utente_familiar ORDER BY idUtente_familiar DESC LIMIT 1";
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
                                    idAddUt = 1 + int.Parse(reader["idUtente_familiar"].ToString());

                                }
                            }
                        }

                        string queryInsertRelacaoUtente = "INSERT INTO mydb.utente_familiar (idUtente_familiar,Utente_idUtente, Familiar_idFamiliar,parentesco)" +
                                                          "VALUES (@idUtente_familiar,@Utente_idUtente, @Familiar_idFamiliar, @parentesco)";

                        using (MySqlCommand comandoUpdateFamiliar = new MySqlCommand(queryUpdateFamiliar, conexao))
                        using (MySqlCommand comandoInsertRelacaoUtente = new MySqlCommand(queryInsertRelacaoUtente, conexao))
                        {
                            comandoUpdateFamiliar.Parameters.AddWithValue("@idFamiliar", idFamiliarParaAtualizar);
                            comandoUpdateFamiliar.Parameters.AddWithValue("@nomel", nomel);
                            comandoUpdateFamiliar.Parameters.AddWithValue("@numero_cc", numero_cc);
                            comandoUpdateFamiliar.Parameters.AddWithValue("@data_validade", data_validade);
                            comandoUpdateFamiliar.Parameters.AddWithValue("@telemovel", telemovel);
                            comandoUpdateFamiliar.Parameters.AddWithValue("@data_nascimento", data_nascimento);
                            comandoUpdateFamiliar.Parameters.AddWithValue("@morada", morada);
                            comandoUpdateFamiliar.Parameters.AddWithValue("@cod_postal", cod_postal);
                            comandoUpdateFamiliar.Parameters.AddWithValue("@ocupacao", ocupacao);
                            comandoUpdateFamiliar.Parameters.AddWithValue("@tel_casa", tel_casa);
                            comandoUpdateFamiliar.Parameters.AddWithValue("@email", email);
                            comandoUpdateFamiliar.Parameters.AddWithValue("@senha", senha);
                            if (imageBytes != null)
                            {
                                comandoUpdateFamiliar.Parameters.AddWithValue("@Imagem", imageBytes);
                            }
                            else
                            {
                                comandoUpdateFamiliar.Parameters.AddWithValue("@Imagem", img);
                            }

                            comandoUpdateFamiliar.ExecuteNonQuery();

                            foreach (string entry in Lista_utente_Familiar)
                            {
                                string[] parts = entry.Split(',');
                                string utenteId = parts[0].Split(':')[1].Trim();
                                string parentesco = parts[2].Split(':')[1].Trim();

                                // Check if the entry already exists
                                string queryCheckExistence = "SELECT COUNT(*) FROM mydb.utente_familiar WHERE Utente_idUtente = @Utente_idUtente AND Familiar_idFamiliar = @Familiar_idFamiliar";
                                using (MySqlCommand commandoVerificar = new MySqlCommand(queryCheckExistence, conexao))
                                {
                                    commandoVerificar.Parameters.AddWithValue("@Utente_idUtente", utenteId);
                                    commandoVerificar.Parameters.AddWithValue("@Familiar_idFamiliar", idFamiliarParaAtualizar);
                                    int count = Convert.ToInt32(commandoVerificar.ExecuteScalar());
                                    if (count == 0)
                                    {
                                        comandoInsertRelacaoUtente.Parameters.Clear();
                                        comandoInsertRelacaoUtente.Parameters.AddWithValue("@idUtente_familiar", idAddUt);
                                        comandoInsertRelacaoUtente.Parameters.AddWithValue("@Utente_idUtente", utenteId);
                                        comandoInsertRelacaoUtente.Parameters.AddWithValue("@Familiar_idFamiliar", idFamiliarParaAtualizar);
                                        comandoInsertRelacaoUtente.Parameters.AddWithValue("@parentesco", parentesco);
                                        comandoInsertRelacaoUtente.ExecuteNonQuery();
                                        idAddUt = idAddUt + 1;
                                    }
                                    
                                }
                            }

                            string deleteQuery = "DELETE FROM mydb.utente_familiar " +"WHERE Familiar_idFamiliar = @idFamiliar " +"AND Utente_idUtente NOT IN (" +string.Join(",", Lista_utente_Familiar.Select(entry => entry.Split(',')[0].Split(':')[1].Trim())) + ")";
                            using (MySqlCommand comandoDeleteRelacaoUtente = new MySqlCommand(deleteQuery, conexao))
                            {
                                comandoDeleteRelacaoUtente.Parameters.AddWithValue("@idFamiliar", idFamiliarParaAtualizar);
                                comandoDeleteRelacaoUtente.ExecuteNonQuery();
                            }

                            MessageBox.Show("Responsável atualizado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Por favor, selecione uma linha para atualizar.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao atualizar responsável: " + ex.Message);
            }
            finally
            {
                display_data();
            }
            LimparTextBoxes();
        }




        private void button_delete_Click(object sender, EventArgs e)
        {
            try
            {
                // Verifica se há uma linha selecionada no DataGridView
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    // Obtém o idFamiliar da linha selecionada
                    int idFamiliarParaExcluir = ObterIdFamiliarSelecionado();

                    using (conexao)
                    {
                        conexao.Open();

                        // Query SQL para excluir o familiar
                        string queryDeleteFamiliar = "DELETE FROM mydb.familiar WHERE idFamiliar = @idFamiliar";

                        // Query SQL para excluir a relação com o utente
                        string queryDeleteRelacaoUtente = "DELETE FROM mydb.utente_familiar WHERE Familiar_idFamiliar = @idFamiliar";

                        using (MySqlCommand comandoDeleteFamiliar = new MySqlCommand(queryDeleteFamiliar, conexao))
                        using (MySqlCommand comandoDeleteRelacaoUtente = new MySqlCommand(queryDeleteRelacaoUtente, conexao))
                        {
                            // Adicione os parâmetros com os valores obtidos do formulário
                            comandoDeleteFamiliar.Parameters.AddWithValue("@idFamiliar", idFamiliarParaExcluir);
                            comandoDeleteRelacaoUtente.Parameters.AddWithValue("@idFamiliar", idFamiliarParaExcluir);

                            // Execute as consultas de exclusão
                            comandoDeleteRelacaoUtente.ExecuteNonQuery();
                            comandoDeleteFamiliar.ExecuteNonQuery();

                            MessageBox.Show("Responsável excluído com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Por favor, selecione uma linha para excluir.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao excluir responsável: " + ex.Message);
            }
            finally
            {
                display_data();
                LimparTextBoxes();
            }
        }
        
        // Método para obter o idFamiliar da linha selecionada no DataGridView
        private int ObterIdFamiliarSelecionado()
        {
            // Obtém o valor do idFamiliar da linha selecionada no DataGridView
            int idFamiliar = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["idFamiliar"].Value);
            return idFamiliar;
        }

        private void Mostrar_Click(object sender, EventArgs e)
        {
            display_data();
            LimparTextBoxes();
        }

        private void button_Search_Click(object sender, EventArgs e)
        {
            conexao.Open();
            MySqlCommand cmd = conexao.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT f.idFamiliar, f.nomel, f.numero_cc, f.data_validade, f.telemovel, f.data_nascimento, " +
                               "f.parentesco_relacao, f.morada, f.cod_postal, f.ocupacao, f.tel_casa, f.senha, f.email, " +
                               "u.nome AS NomeUtente " +
                               "FROM familiar f " +
                               "JOIN utente_familiar uf ON f.idFamiliar = uf.Familiar_idFamiliar " +
                               "JOIN utente u ON uf.Utente_idUtente = u.idUtente " +
                               "WHERE f.nomel LIKE @searchText OR SUBSTRING_INDEX(f.nomel, ' ', 1) LIKE @searchText";
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
                dateTimePicker_dataNascimento.Text = "";
                textBox_telCasa.Text = "";
                textBox_telemovel.Text = "";
                textBox_email.Text = "";
                textBox1_parentesco.Text = "";
                
                textBox_morada.Text = "";
                textBox_codPostal.Text = "";
                textBox_ocupacao.Text = "";
                textBox3_senha.Text = "";
                comboBox1_Utente.SelectedIndex = 0;
                //Limpar panel
                panel1.BackgroundImage = null;


            }
            else
            {
                MessageBox.Show("Nenhum resultado encontrado.");
            }
        }

        private void display_data()
        {
            try
            {
                // Open the connection if it's not already open
                if (conexao.State == ConnectionState.Closed)
                {
                    conexao.Open();
                }

                using (MySqlCommand cmd = conexao.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT idFamiliar, nomel, numero_cc, data_validade, telemovel, data_nascimento, morada, cod_postal, ocupacao, tel_casa,'*******' as senha, email, Imagem FROM familiar ORDER BY idFamiliar;";
                    cmd.ExecuteNonQuery();
                    DataTable dta = new DataTable();
                    MySqlDataAdapter dataadapter = new MySqlDataAdapter(cmd);
                    dataadapter.Fill(dta);

                    // Assign the data source to the DataGridView
                    dataGridView1.DataSource = dta;

                    // Resize images in the DataGridView column
                    ResizeDataGridViewColumnImages("Imagem");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error displaying data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Ensure the connection is closed
                if (conexao.State == ConnectionState.Open)
                {
                    conexao.Close();
                }
            }
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


        private void comboBox1_Utente_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (comboBox1_Utente.SelectedItem != null)
            {
                string selectedValue = comboBox1_Utente.SelectedItem.ToString();
                if (selectedValue == "-----------------")
                {
                    // Clear the ComboBox selection
                    comboBox1_Utente.SelectedIndex = -1;
                    control1 = 0;
                    // Additional actions if needed when the ComboBox is emptied
                    Console.WriteLine("ComboBox is now empty!");
                }
                else
                {
                    // Retrieve the stored specific column data for the selected item
                    if (utente_.TryGetValue(selectedValue, out string id))
                    {
                        control1 = 1;
                        // Use specificColumnData as needed (e.g., assign it to a variable)
                        idUt1 = int.Parse(id.ToString());
                        relacao = textBox1_parentesco.Text;
                        Console.WriteLine(idUt1);
                        Console.WriteLine(relacao);

                    }
                }
            }

        }



        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dataGridView1.Rows.Count)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];


                // Extract Familiar_id from the selected row
                int familiarId = Convert.ToInt32(row.Cells["idFamiliar"].Value);

                // Get all information related to Utente and Utente_Familiar based on Familiar_id
                List<string> utenteFamiliarInfos = GetUtenteFamiliarInfosFromDatabase(familiarId);

                // Populate textBox_utentefamiliar
                textBox_UtenteFamiliar.Text = string.Join(Environment.NewLine, utenteFamiliarInfos);

                // Update utenteFamiliarList
                Lista_utente_Familiar.Clear();
                Lista_utente_Familiar.AddRange(utenteFamiliarInfos);

                // Set values from DataGridView to TextBoxes
                textBox_Name.Text = row.Cells["nomel"].Value.ToString();
                textBox_Cc.Text = row.Cells["numero_cc"].Value.ToString();
                dateTimePicker_DtaValidade.Value = Convert.ToDateTime(row.Cells["data_validade"].Value);
                textBox_telCasa.Text = row.Cells["tel_casa"].Value.ToString();
                dateTimePicker_dataNascimento.Value = Convert.ToDateTime(row.Cells["data_nascimento"].Value);
                textBox_telemovel.Text = row.Cells["telemovel"].Value.ToString();
                textBox_email.Text = row.Cells["email"].Value.ToString();
                textBox_morada.Text = row.Cells["morada"].Value.ToString();
                textBox_codPostal.Text = row.Cells["cod_postal"].Value.ToString();
                textBox_ocupacao.Text = row.Cells["ocupacao"].Value.ToString();
                textBox3_senha.Text = row.Cells["senha"].Value.ToString();

                // Carrega a imagem associada à linha selecionada no Panel1
                if (row.Cells["Imagem"].Value != DBNull.Value)
                {
                    byte[] imageBytes = (byte[])row.Cells["Imagem"].Value;
                    img = imageBytes;
                    // Converte os bytes da imagem em um objeto Image
                    using (MemoryStream ms = new MemoryStream(imageBytes))
                    {
                        Image image = Image.FromStream(ms);
                        // Define a imagem como plano de fundo do Panel1 com layout de zoom
                        panel1.BackgroundImage = image;
                        panel1.BackgroundImageLayout = ImageLayout.Zoom;
                    }
                }
                else
                {
                    // Limpa o plano de fundo do Panel1 se não houver imagem associada
                    panel1.BackgroundImage = null;
                }

            }
        }

        private List<string> GetUtenteFamiliarInfosFromDatabase(int familiarId)
        {
            List<string> infos = new List<string>();

            try
            {
                using (conexao)
                {
                    conexao.Open();

                    string selectQuery = "SELECT u.idUtente, u.nome, uf.parentesco " +
                                         "FROM utente u " +
                                         "INNER JOIN utente_familiar uf ON u.idUtente = uf.Utente_idUtente " +
                                         "WHERE uf.Familiar_idFamiliar = @Familiar_id";

                    using (MySqlCommand cmd = new MySqlCommand(selectQuery, conexao))
                    {
                        cmd.Parameters.AddWithValue("@Familiar_id", familiarId);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                // Extract ID, nome, and parentesco for each Utente_Familiar relation
                                int utenteId = reader.GetInt32("idUtente");
                                string nome = reader.GetString("nome");
                                string parentesco = reader.GetString("parentesco");

                                // Create the information string and add it to the list
                                string info = $"ID: {utenteId}, Nome: {nome}, Parentesco: {parentesco}";
                                infos.Add(info);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao obter informações do Utente e Utente_Familiar: " + ex.Message);
            }

            return infos;
        }

        private void button_Associate_Click(object sender, EventArgs e)
        {
            if (comboBox1_Utente.SelectedItem != null && !string.IsNullOrEmpty(textBox1_parentesco.Text))
            {
                string nomeUtente = comboBox1_Utente.SelectedItem.ToString();
                string idUtente = utente_[nomeUtente];
                string parentesco = textBox1_parentesco.Text;

                string utenteFamiliarInfo = $"ID: {idUtente}, Nome: {nomeUtente}, Parentesco: {parentesco}";

                if (!Lista_utente_Familiar.Contains(utenteFamiliarInfo))
                {
                    Lista_utente_Familiar.Add(utenteFamiliarInfo);
                    textBox_UtenteFamiliar.Text = string.Join(Environment.NewLine, Lista_utente_Familiar);
                }
                else
                {
                    MessageBox.Show("Esta associação já foi adicionada.");
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecione um utente e insira o parentesco.");
            }
        }

        private void button_Disassociate_Click(object sender, EventArgs e)
        {
            if (comboBox1_Utente.SelectedItem != null)
            {
                string nomeUtente = comboBox1_Utente.SelectedItem.ToString();

                int index = Lista_utente_Familiar.FindIndex(entry => entry.Contains(nomeUtente));

                if (index != -1)
                {
                    Lista_utente_Familiar.RemoveAt(index);

                    textBox_UtenteFamiliar.Text = string.Join(Environment.NewLine, Lista_utente_Familiar);
                }
                else
                {
                    MessageBox.Show("A entrada selecionada não foi encontrada para desassociar.");
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecione um utente para desassociar.");
            }
        }
        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Check if the current cell is in the column containing the images
            if (dataGridView1.Columns[e.ColumnIndex] is DataGridViewImageColumn)
            {
                // Check if the cell value is an image
                if (e.Value != null && e.Value is Image)
                {
                    // Resize the image to 30x30 pixels
                    Image originalImage = (Image)e.Value;
                    Image resizedImage = new Bitmap(30, 30);
                    using (Graphics g = Graphics.FromImage(resizedImage))
                    {
                        g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                        g.DrawImage(originalImage, 0, 0, 30, 30);
                    }
                    e.Value = resizedImage;
                }
            }
        }


        private void pictureBox1_Click(object sender, EventArgs e)
        {

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

        private void textBox3_senha_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox3_senha.PasswordChar == '*')
            {
                // Se a senha estiver oculta, mostre-a
                textBox3_senha.PasswordChar = '\0'; // Caractere nulo para mostrar o texto da senha
                button2.Text = "Ocultar"; // Altera o texto do botão
            }
            else
            {
                // Se a senha estiver visível, oculte-a
                textBox3_senha.PasswordChar = '*'; // Caractere '*' para ocultar a senha
                button2.Text = "Mostrar"; // Altera o texto do botão
            }
        }
    }
}
