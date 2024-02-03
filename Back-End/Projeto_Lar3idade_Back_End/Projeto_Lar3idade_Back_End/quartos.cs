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


namespace Projeto_Lar3idade_Back_End
{
    public partial class quartos : UserControl
    {
        private int idAdd;
        private int idunt1;
        private int idunt2;
        private int idunt3;
        private Dictionary<string, string> Utente_ = new Dictionary<string, string>();
        private MySqlConnection conexao;
        private int control1;
        private int control2;
        private int control3;
        private string estado;
        private List<string> Lista_utente = new List<string>();

        public quartos()
        {
            InitializeComponent();
            string connectionString = "Server=localhost;Port=3306;Database=mydb;User ID=root;Password=ipbcurso";
            conexao = new MySqlConnection(connectionString);
            LoadComboBox();
            display_data();

            dataGridView1.CellClick += dataGridView1_CellClick;

        }
        private void LoadComboBox()
        {

            comboBox_estado.Items.Add("-----------------");
            comboBox_estado.Items.Add("Livre");
            comboBox_estado.Items.Add("Ocupado");
            comboBox_estado.Items.Add("Limpeza");
            comboBox_estado.Items.Add("Manutenção");
            comboBox_estado.Items.Add("Desinfeção");
            comboBox_estado.Items.Add("Reconstrução");
            comboBox1_Utente.Items.Add("-----------------");
            

            using (conexao)
            {
                string query = "SELECT idUtente, nome FROM utente; ";
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
                            
                            Utente_[nome] = id;


                        }
                    }
                }
            }
        }
        private void Limpar()
        {
            textBox_camas.Clear();
            comboBox_estado.SelectedIndex = -1;
            comboBox1_Utente.SelectedIndex = -1;
            textBox_numero.Clear();

        }

        private void button_insert_Click(object sender, EventArgs e)
        {
            // Pegue os valores dos controles do formulário
            int quantidade_cama = int.Parse(textBox_camas.Text.ToString());
            int numero = int.Parse(textBox_numero.Text.ToString());
            if (Lista_utente.Count > quantidade_cama)
            {
                MessageBox.Show("O número de utentes excede a quantidade de camas disponíveis.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                using (MySqlConnection conexao = new MySqlConnection("Server=localhost;Port=3306;Database=mydb;User ID=root;Password=ipbcurso"))
                {
                    conexao.Open();
                    string query = "INSERT INTO mydb.quarto (idQuarto, estado, quantidade_cama , Numero)" +
                                  "VALUES (@idQuarto,@estado, @quantidade_cama, @Numero)";

                    string query2 = "SELECT * FROM quarto ORDER BY idQuarto DESC LIMIT 1";

                    string query3 = "UPDATE utente SET Quarto_idQuarto = @QuartoId WHERE idUtente = @IdUtente;";

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
                                idAdd = 1 + int.Parse(reader["idQuarto"].ToString());

                            }
                        }
                    }
                    string query4 = "SELECT Numero FROM quarto";

                    using (MySqlCommand NumeroSearch = new MySqlCommand(query4, conexao))
                    {
                        using (MySqlDataReader reader = NumeroSearch.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int existingNumero = int.Parse(reader["Numero"].ToString());
                                if (existingNumero == numero)
                                {
                                    MessageBox.Show("O Numero do Quarto não se pode repetir.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }
                            }
                        }
                    }


                    // Crie um comando MySqlCommand
                    using (MySqlCommand comando = new MySqlCommand(query, conexao))
                    {
                        // Adicione os parâmetros com os valores obtidos do formulário
                        comando.Parameters.AddWithValue("@idQuarto", idAdd);
                        comando.Parameters.AddWithValue("@estado", estado);
                        comando.Parameters.AddWithValue("@quantidade_cama", quantidade_cama);
                        comando.Parameters.AddWithValue("@Numero",numero);



                        // Execute a consulta de inserção
                        comando.ExecuteNonQuery();

                        MessageBox.Show("Quarto associado ao utente com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    foreach (string entry in Lista_utente)
                    {
                        string[] parts = entry.Split(',');

                        string utenteId = parts[0].Split(':')[1].Trim();


                        using (MySqlCommand comando = new MySqlCommand(query3, conexao))
                        {
                            comando.Parameters.AddWithValue("@QuartoId", idAdd);
                            comando.Parameters.AddWithValue("@IdUtente", utenteId);

                            comando.ExecuteNonQuery();
                        }
                    }


                    display_data();
                    Limpar();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao adicionar quarto: " + ex.Message );
            }

        }
        private void comboBox_estado_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox_estado.SelectedItem != null)
            {
                estado = comboBox_estado.SelectedItem.ToString();
            }
        }

        private void comboBox1_Utente_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1_Utente.SelectedItem != null)
            {
                string selectedValue = comboBox1_Utente.SelectedItem.ToString();
                if (selectedValue == "-----------------")
                {
                    
                    comboBox1_Utente.SelectedIndex = -1;
                    control1 = 0;
                    //Ações adicionais se necessárias quando o ComboBox for esvaziado
                    Console.WriteLine("ComboBox agora está vazio!");
                }
                else
                {
                    // Recupera os dados armazenados da coluna específica para o item selecionado
                    if (Utente_.TryGetValue(selectedValue, out string id))
                    {
                        control1 = 1;
                        // Use specificColumnData as needed (e.g., assign it to a variable)
                        idunt1 = int.Parse(id.ToString());
                        Console.WriteLine(idunt1);
                    }
                }
            }

        }

       

        
        private void display_data()
        {
            try
            {
                conexao.Open();
                MySqlCommand cmd = conexao.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT q.idQuarto, q.Numero, q.estado, q.quantidade_cama  FROM quarto q ;";

                DataTable dta = new DataTable();
                MySqlDataAdapter dataadapter = new MySqlDataAdapter(cmd);
                dataadapter.Fill(dta);
                dataGridView1.DataSource = dta;
                dataGridView1.Columns["idQuarto"].Visible = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao exibir dados: " + ex.Message);
            }
            finally
            {
                conexao.Close();
            }
        }

        private void button_update_Click(object sender, EventArgs e)
        {
            int quantidade_cama = int.Parse(textBox_camas.Text);
            int numero = int.Parse(textBox_numero.Text.ToString());
            if (Lista_utente.Count > quantidade_cama)
            {
                MessageBox.Show("O número de utentes excede a quantidade de camas disponíveis.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (dataGridView1.SelectedRows.Count > 0)
            {
                try
                {
                    int rowIndex = dataGridView1.SelectedRows[0].Index;
                    int idQuarto = Convert.ToInt32(dataGridView1.Rows[rowIndex].Cells["idQuarto"].Value);
                    int numeroAntigo = Convert.ToInt32(dataGridView1.Rows[rowIndex].Cells["Numero"].Value); ;

                    using (MySqlConnection conexao = new MySqlConnection("Server=localhost;Port=3306;Database=mydb;User ID=root;Password=ipbcurso"))
                    {
                        conexao.Open();

                        // Query para atualizar os dados do quarto
                        string query = "UPDATE quarto SET Numero = @Numero, estado = @estado, quantidade_cama = @quantidade_cama WHERE idQuarto = @idQuarto";
                        string query3 = "UPDATE utente SET Quarto_idQuarto = @QuartoId WHERE idUtente = @IdUtente;";

                        string query4 = "SELECT Numero FROM quarto";

                        using (MySqlCommand NumeroSearch = new MySqlCommand(query4, conexao))
                        {
                            using (MySqlDataReader reader = NumeroSearch.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    int existingNumero = int.Parse(reader["Numero"].ToString());
                                    if (existingNumero == numero)
                                    {
                                        if(existingNumero != numeroAntigo)
                                        {
                                            MessageBox.Show("O Numero do Quarto não se pode repetir.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                            return;
                                        }
                                        
                                    }
                                }
                            }
                        }
                        using (MySqlCommand comando = new MySqlCommand(query, conexao))
                        {
                            // Adicione os parâmetros com os valores obtidos do formulário
                            comando.Parameters.AddWithValue("@idQuarto", idQuarto);
                            comando.Parameters.AddWithValue("@estado", comboBox_estado.SelectedItem.ToString());
                            comando.Parameters.AddWithValue("@quantidade_cama", quantidade_cama);
                            comando.Parameters.AddWithValue("@Numero", numero);


                            // Execute a consulta de atualização
                            comando.ExecuteNonQuery();

                            MessageBox.Show("Quarto atualizado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            
                        }
                        
                        
                        foreach (string entry in Lista_utente)
                        {
                            string[] parts = entry.Split(',');

                            string utenteId = parts[0].Split(':')[1].Trim();


                            using (MySqlCommand comando = new MySqlCommand(query3, conexao))
                            {
                                comando.Parameters.AddWithValue("@QuartoId", idQuarto);
                                comando.Parameters.AddWithValue("@IdUtente", utenteId);

                                comando.ExecuteNonQuery();
                            }
                        }
                       
                    }
                    display_data();
                    Limpar();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao atualizar quarto: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecione um quarto para atualizar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private void button_delete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                try
                {
                    int rowIndex = dataGridView1.SelectedRows[0].Index;
                    int idQuarto = Convert.ToInt32(dataGridView1.Rows[rowIndex].Cells["idQuarto"].Value);

                    using (MySqlConnection conexao = new MySqlConnection("Server=localhost;Port=3306;Database=mydb;User ID=root;Password=ipbcurso"))
                    {
                        conexao.Open();

                        string query = "DELETE FROM  mydb.quarto WHERE idQuarto = @idQuarto";
                        string queryUpdateUtente = "UPDATE `mydb`.`utente` SET `Quarto_idQuarto` = null WHERE (`Quarto_idQuarto` = '10');";
                        Console.WriteLine("aqui1");
                        using (MySqlCommand comandoUpdateUtente = new MySqlCommand(queryUpdateUtente, conexao))
                        {
                            comandoUpdateUtente.Parameters.AddWithValue("@idQuarto", idQuarto);

                            comandoUpdateUtente.ExecuteNonQuery();

                        }
                        Console.WriteLine("aqui2");

                        using (MySqlCommand comandoDeleteQuarto = new MySqlCommand(query, conexao))
                        {
                            comandoDeleteQuarto.Parameters.AddWithValue("@idQuarto", idQuarto);

                            comandoDeleteQuarto.ExecuteNonQuery();

                        }
                        
                        MessageBox.Show("Quarto excluído com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }

                    display_data();
                    Limpar();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao excluir quarto: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecione um quarto para excluir.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }



        private void Mostrar_Click(object sender, EventArgs e)
        {
            display_data();
            Limpar();
        }

        private void button_Search_Click(object sender, EventArgs e)
        {
            conexao.Open();

            MySqlCommand cmd = conexao.CreateCommand();
            cmd.CommandText = "SELECT quarto.idQuarto, quarto.estado, quarto.quantidade_cama, utente.nome " +
                              "FROM quarto LEFT JOIN utente ON quarto.idQuarto = utente.Quarto_idQuarto " +
                              "WHERE quarto.idQuarto = @searchId OR utente.nome LIKE @searchText";

            // Verifica se o texto é um número inteiro antes de tentar converter
            int searchId;
            bool isNumeric = int.TryParse(textBox_Search.Text, out searchId);

            // Adiciona o parâmetro dependendo se é um número ou uma string
            if (isNumeric)
            {
                cmd.Parameters.AddWithValue("@searchId", searchId);
                cmd.Parameters.AddWithValue("@searchText", "%" + textBox_Search.Text + "%");
            }
            else
            {
                cmd.Parameters.AddWithValue("@searchId", DBNull.Value); // Para ignorar o filtro por idQuarto
                cmd.Parameters.AddWithValue("@searchText", "%" + textBox_Search.Text + "%");
            }

            try
            {
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                dataGridView1.DataSource = dt;

                // Os campos de texto são limpos apenas se houver resultados da pesquisa
                if (dt.Rows.Count > 0)
                {
                    textBox_camas.Text = "";
                    comboBox_estado.SelectedIndex = -1;
                    comboBox1_Utente.SelectedIndex = -1;
                   
                }
                else
                {
                    MessageBox.Show("Nenhum resultado encontrado.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao realizar a pesquisa: " + ex.Message);
            }
            finally
            {
                conexao.Close();
            }
        }

        
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dataGridView1.Rows.Count)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                // Extract Familiar_id from the selected row
                int QuartoId = Convert.ToInt32(row.Cells["idQuarto"].Value);

                // Get all information related to Utente and Utente_Familiar based on Familiar_id
                List<string> utenteQuartosInfos = GetUtenteFamiliarInfosFromDatabase(QuartoId);

                // Populate textBox_utentefamiliar
                textBox_utentes.Text = string.Join(Environment.NewLine, utenteQuartosInfos);

                // Update utenteFamiliarList
                Lista_utente.Clear();
                Lista_utente.AddRange(utenteQuartosInfos);

                // Set values from DataGridView to TextBoxes
                textBox_camas.Text = row.Cells["quantidade_cama"].Value.ToString();
                comboBox_estado.SelectedItem = row.Cells["estado"].Value.ToString();
            }
        }

        private List<string> GetUtenteFamiliarInfosFromDatabase(int QuartoId)
        {
            List<string> infos = new List<string>();

            try
            {
                using (MySqlConnection conexao = new MySqlConnection("Server=localhost;Port=3306;Database=mydb;User ID=root;Password=ipbcurso"))
                {
                    conexao.Open();

                    string selectQuery = "SELECT u.idUtente, u.nome FROM utente u WHERE u.Quarto_idQuarto = @Quarto_idQuarto";

                    using (MySqlCommand cmd = new MySqlCommand(selectQuery, conexao))
                    {
                        cmd.Parameters.AddWithValue("@Quarto_idQuarto", QuartoId);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                // Extract ID, nome, and parentesco for each Utente_Familiar relation
                                int utenteId = reader.GetInt32("idUtente");
                                string nome = reader.GetString("nome");

                                // Create the information string and add it to the list
                                string info = $"ID: {utenteId}, Nome: {nome}";
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
            if (comboBox1_Utente.SelectedItem != null)
            {
                string nomeUtente = comboBox1_Utente.SelectedItem.ToString();
                string idUtente = Utente_[nomeUtente];

                string utenteQuartoInfo = $"ID: {idUtente}, Nome: {nomeUtente}";

                // Check if the entry already exists in the list
                if (!Lista_utente.Contains(utenteQuartoInfo))
                {
                    Lista_utente.Add(utenteQuartoInfo);
                    textBox_utentes.Text = string.Join(Environment.NewLine, Lista_utente);
                }
                else
                {
                    MessageBox.Show("Este utente já foi associado.");
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecione um utente.");
            }
        }


        private void button_Disassociate_Click(object sender, EventArgs e)
        {
            if (comboBox1_Utente.SelectedItem != null)
            {
                string nomeUtente = comboBox1_Utente.SelectedItem.ToString();

                int index = Lista_utente.FindIndex(entry => entry.Contains(nomeUtente));

                if (index != -1)
                {
                    Lista_utente.RemoveAt(index);

                    textBox_utentes.Text = string.Join(Environment.NewLine, Lista_utente);
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

    }
}
