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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;


namespace Projeto_Lar3idade_Back_End
{
    public partial class responsavel : UserControl
    {
        private int idAdd;
        private int idAddUt;
        private int idAddUt2;
        private int idAddUt3;
        private int idUt1;
        private int idUt2;
        private int idUt3;
        private int control1;
        private int control2;
        private int control3;
       
        private MySqlConnection conexao;
        private Dictionary<string, string> utente_ = new Dictionary<string, string>();

        public responsavel()
        {
            InitializeComponent();
            string connectionString = "Server=localhost;Port=3306;Database=mydb;User ID=root;Password=ipbcurso";
            conexao = new MySqlConnection(connectionString);
            display_data();
            LoadComboBox();
            

            // Adicione o evento CellClick ao DataGridView
            dataGridView1.CellClick += dataGridView1_CellClick;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
        private void LoadComboBox()
        {

            comboBox1_Utente.Items.Add("-----------------");
            comboBox2_Utente.Items.Add("-----------------");
            comboBox3_Utente.Items.Add("-----------------");


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
                            comboBox2_Utente.Items.Add(reader["nome"].ToString());
                            comboBox3_Utente.Items.Add(reader["nome"].ToString());
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
            string parentesco = textBox1_parentesco.Text;

            try
            {
                using (MySqlConnection conexao = new MySqlConnection("Server=localhost;Port=3306;Database=mydb;User ID=root;Password=ipbcurso"))
                {
                    conexao.Open();
                    string query = "INSERT INTO mydb.familiar (idFamiliar, nomel, numero_cc, data_validade, telemovel, data_nascimento,parentesco_relacao,morada,cod_postal,ocupacao,tel_casa, email, senha)" +
                                  "VALUES (@idFamiliar,@nomel, @numero_cc, @data_validade, @telemovel, @data_nascimento, @parentesco_relacao,@morada,@cod_postal,@ocupacao,@tel_casa,@email, @senha)";

                    string query2 = "SELECT * FROM familiar ORDER BY idFamiliar DESC LIMIT 1";
                    string query3 = "SELECT * FROM utente_familiar ORDER BY idUtente_familiar DESC LIMIT 1";
                    string query4 = "LOCK TABLES utente_familiar WRITE;" +
                                    "ALTER TABLE utente_familiar DISABLE KEYS;" +
                                    "INSERT INTO mydb.utente_familiar (idUtente_familiar, Utente_idUtente, Familiar_idFamiliar)" +
                                    "VALUES (@idUtente_familiar, @Utente_idUtente, @Familiar_idFamiliar);" +
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
                                idAddUt2 = 1 + int.Parse(reader["idUtente_familiar"].ToString());

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
                                idAddUt3 = 1 + int.Parse(reader["idUtente_familiar"].ToString());

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
                        comando.Parameters.AddWithValue("@parentesco_relacao", parentesco);
                        comando.Parameters.AddWithValue("@morada", morada);
                        comando.Parameters.AddWithValue("@cod_postal", cod_postal);
                        comando.Parameters.AddWithValue("@ocupacao", ocupacao);
                        comando.Parameters.AddWithValue("@tel_casa", tel_casa);
                        comando.Parameters.AddWithValue("@email", email);
                        comando.Parameters.AddWithValue("@senha", senha);

                        // Execute a consulta de inserção
                        comando.ExecuteNonQuery();

                        MessageBox.Show("Responsavél adicionado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    if (control1 == 1)
                    {
                        using (MySqlCommand comando = new MySqlCommand(query4, conexao))
                        {
                            // Adicione os parâmetros com os valores obtidos do formulário
                            comando.Parameters.AddWithValue("@idUtente_familiar", idAddUt);
                            comando.Parameters.AddWithValue("@Utente_idUtente", idUt1);
                            comando.Parameters.AddWithValue("@Familiar_idFamiliar", idAdd);


                            // Execute a consulta de inserção
                            comando.ExecuteNonQuery();

                        }
                    }
                    if (control2 == 1)
                    {
                        using (MySqlCommand comando = new MySqlCommand(query4, conexao))
                        {
                            // Adicione os parâmetros com os valores obtidos do formulário
                            comando.Parameters.AddWithValue("@idUtente_familiar", idAddUt2);
                            comando.Parameters.AddWithValue("@Utente_idUtente", idUt2);
                            comando.Parameters.AddWithValue("@Familiar_idFamiliar", idAdd);

                            // Execute a consulta de inserção
                            comando.ExecuteNonQuery();

                        }
                    }
                    if (control3 == 1)
                    {
                        using (MySqlCommand comando = new MySqlCommand(query4, conexao))
                        {
                            // Adicione os parâmetros com os valores obtidos do formulário
                            comando.Parameters.AddWithValue("@idUtente_familiar", idAddUt3);
                            comando.Parameters.AddWithValue("@Utente_idUtente", idUt3);
                            comando.Parameters.AddWithValue("@Familiar_idFamiliar", idAdd);


                            // Execute a consulta de inserção
                            comando.ExecuteNonQuery();

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
            // Limpar outras ComboBoxes e controles conforme necessário
            comboBox1_Utente.SelectedIndex = -1;
            comboBox2_Utente.SelectedIndex = -1;
            comboBox3_Utente.SelectedIndex = -1;
        }
        
        private void button_update_Click(object sender, EventArgs e)
        {
            try
            {
                // Verifica se há uma linha selecionada no DataGridView
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    // Obtém o idFamiliar da linha selecionada
                    int idFamiliarParaAtualizar = ObterIdFamiliarSelecionado();

                    // Obtém os novos valores dos controles do formulário
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
                    string parentesco = textBox1_parentesco.Text;

                    using (MySqlConnection conexao = new MySqlConnection("Server=localhost;Port=3306;Database=mydb;User ID=root;Password=ipbcurso"))
                    {
                        conexao.Open();
                        string query = "UPDATE mydb.familiar SET nomel = @nomel, numero_cc = @numero_cc, data_validade = @data_validade, " +
                                       "telemovel = @telemovel, data_nascimento = @data_nascimento, parentesco_relacao = @parentesco_relacao, " +
                                       "morada = @morada, cod_postal = @cod_postal, ocupacao = @ocupacao, tel_casa = @tel_casa, " +
                                       "email = @email, senha = @senha " +
                                       "WHERE idFamiliar = @idFamiliar";

                        using (MySqlCommand comando = new MySqlCommand(query, conexao))
                        {
                            // Adicione os parâmetros com os novos valores obtidos do formulário
                            comando.Parameters.AddWithValue("@nomel", nomel);
                            comando.Parameters.AddWithValue("@numero_cc", numero_cc);
                            comando.Parameters.AddWithValue("@data_validade", data_validade);
                            comando.Parameters.AddWithValue("@telemovel", telemovel);
                            comando.Parameters.AddWithValue("@data_nascimento", data_nascimento);
                            comando.Parameters.AddWithValue("@parentesco_relacao", parentesco);
                            comando.Parameters.AddWithValue("@morada", morada);
                            comando.Parameters.AddWithValue("@cod_postal", cod_postal);
                            comando.Parameters.AddWithValue("@ocupacao", ocupacao);
                            comando.Parameters.AddWithValue("@tel_casa", tel_casa);
                            comando.Parameters.AddWithValue("@email", email);
                            comando.Parameters.AddWithValue("@senha", senha);
                            comando.Parameters.AddWithValue("@idFamiliar", idFamiliarParaAtualizar);

                            // Execute a consulta de atualização
                            comando.ExecuteNonQuery();

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
                display_data();  // Atualize a exibição dos dados após a atualização
            }
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

                    using (MySqlConnection conexao = new MySqlConnection("Server=localhost;Port=3306;Database=mydb;User ID=root;Password=ipbcurso"))
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
                display_data();  // Atualize a exibição dos dados após a exclusão
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
            cmd.CommandText = "SELECT * FROM familiar WHERE nomel LIKE @searchText OR SUBSTRING_INDEX(nomel, ' ', 1) LIKE @searchText";
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
                textBox2_parentesco.Text="";
                textBox3_parentesco.Text = "";
                textBox_morada.Text = "";
                textBox_codPostal.Text = "";
                textBox_ocupacao.Text = "";
                textBox3_senha.Text = "";
                comboBox1_Utente.SelectedIndex = 0;
                comboBox2_Utente.SelectedIndex = 0;
                comboBox3_Utente.SelectedIndex = 0;              
                

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

                // Abra a conexão se não estiver aberta
                if (conexao.State == ConnectionState.Closed)
                {
                    conexao.Open();
                }

                using (MySqlCommand cmd = conexao.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT f.idFamiliar, f.nomel, f.numero_cc,f.data_validade, f.telemovel,  f.data_nascimento,   f.parentesco_relacao,  f.morada,  f.cod_postal , f.ocupacao , f.tel_casa,    f.senha,  f.email, u.nome AS NomeUtente FROM   familiar f JOIN   utente_familiar uf ON f.idFamiliar = uf.Familiar_idFamiliar JOIN  utente u ON uf.Utente_idUtente = u.idUtente";
                    cmd.ExecuteNonQuery();
                    DataTable dta = new DataTable();
                    MySqlDataAdapter dataadapter = new MySqlDataAdapter(cmd);
                    dataadapter.Fill(dta);
                    dataGridView1.DataSource = dta;
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao exibir dados: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                //  para garantir que seja fechada, independentemente de ocorrer uma exceção ou não.
                if (conexao.State == ConnectionState.Open)
                {
                    conexao.Close();
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
                        Console.WriteLine(idUt1);
                    }
                }
            }

        }

        private void comboBox2_Utente_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2_Utente.SelectedItem != null)
            {
                string selectedValue = comboBox2_Utente.SelectedItem.ToString();

                if (selectedValue == "-----------------")
                {
                    // Clear the ComboBox selection
                    comboBox2_Utente.SelectedIndex = -1;
                    control2 = 0;
                    // Additional actions if needed when the ComboBox is emptied
                    Console.WriteLine("ComboBox is now empty!");
                }
                else
                {
                    // Retrieve the stored specific column data for the selected item
                    if (utente_.TryGetValue(selectedValue, out string id))
                    {
                        control2 = 1;
                        // Use specificColumnData as needed (e.g., assign it to a variable)
                        idUt2 = int.Parse(id.ToString());
                        Console.WriteLine(idUt2);
                    }
                }
            }
        }

        private void comboBox3_Utente_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox3_Utente.SelectedItem != null)
            {
                string selectedValue = comboBox3_Utente.SelectedItem.ToString();

                if (selectedValue == "-----------------")
                {
                    // Clear the ComboBox selection
                    comboBox3_Utente.SelectedIndex = -1;
                    control3 = 0;
                    // Additional actions if needed when the ComboBox is emptied
                    Console.WriteLine("ComboBox is now empty!");
                }
                else
                {
                    // Retrieve the stored specific column data for the selected item
                    if (utente_.TryGetValue(selectedValue, out string id))
                    {
                        control3 = 1;
                        // Use specificColumnData as needed (e.g., assign it to a variable)
                        idUt3 = int.Parse(id.ToString());
                        Console.WriteLine(idUt3);
                    }
                }
            }

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Certifique-se de que a célula clicada não seja o cabeçalho e que haja pelo menos uma linha
            if (e.RowIndex >= 0 && dataGridView1.Rows.Count > 0)
            {
                // Obtém os valores da linha clicada
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];
                
                // Preenche os TextBoxes com os valores da linha
                textBox_Name.Text = selectedRow.Cells["nomel"].Value.ToString();
                textBox_Cc.Text = selectedRow.Cells["numero_cc"].Value.ToString();
                dateTimePicker_DtaValidade.Value = Convert.ToDateTime(selectedRow.Cells["data_validade"].Value);
                textBox_telCasa.Text = selectedRow.Cells["tel_casa"].Value.ToString();
                dateTimePicker_dataNascimento.Value = Convert.ToDateTime(selectedRow.Cells["data_nascimento"].Value);
                textBox_telemovel.Text = selectedRow.Cells["telemovel"].Value.ToString();
                textBox_email.Text = selectedRow.Cells["email"].Value.ToString(); 
                textBox1_parentesco.Text = selectedRow.Cells["parentesco_relacao"].Value.ToString();
                textBox2_parentesco.Text = selectedRow.Cells["parentesco_relacao"].Value.ToString();
                textBox3_parentesco.Text = selectedRow.Cells["parentesco_relacao"].Value.ToString();
                textBox_morada.Text = selectedRow.Cells["morada"].Value.ToString();
                textBox_codPostal.Text = selectedRow.Cells["cod_postal"].Value.ToString();
                textBox_ocupacao.Text = selectedRow.Cells["ocupacao"].Value.ToString();
                textBox3_senha.Text = selectedRow.Cells["senha"].Value.ToString();
                comboBox1_Utente.SelectedItem = selectedRow.Cells["NomeUtente"].Value.ToString();
                comboBox2_Utente.SelectedItem = selectedRow.Cells["NomeUtente"].Value.ToString();
                comboBox3_Utente.SelectedItem = selectedRow.Cells["NomeUtente"].Value.ToString();
            }


        }
        
    }
}
