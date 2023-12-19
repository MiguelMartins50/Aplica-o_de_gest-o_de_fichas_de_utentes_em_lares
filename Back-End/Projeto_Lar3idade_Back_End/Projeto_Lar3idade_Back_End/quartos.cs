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
            comboBox2_Utente.Items.Add("-----------------");
            comboBox3_Utente.Items.Add("-----------------");

            using (conexao)
            {
                string query = "SELECT idUtente, nome FROM utente WHERE utente.Quarto_idQuarto; ";
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
            comboBox2_Utente.SelectedIndex = -1;
            comboBox3_Utente.SelectedIndex = -1;

        }

        private void button_insert_Click(object sender, EventArgs e)
        {
            // Pegue os valores dos controles do formulário
            int quantidade_cama = int.Parse(textBox_camas.Text.ToString());


            try
            {
                using (MySqlConnection conexao = new MySqlConnection("Server=localhost;Port=3306;Database=mydb;User ID=root;Password=ipbcurso"))
                {
                    conexao.Open();
                    string query = "INSERT INTO mydb.quarto (idQuarto, estado, quantidade_cama)" +
                                  "VALUES (@idQuarto,@estado, @quantidade_cama)";

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

                    // Crie um comando MySqlCommand
                    using (MySqlCommand comando = new MySqlCommand(query, conexao))
                    {
                        // Adicione os parâmetros com os valores obtidos do formulário
                        comando.Parameters.AddWithValue("@idQuarto", idAdd);
                        comando.Parameters.AddWithValue("@estado", estado);
                        comando.Parameters.AddWithValue("@quantidade_cama", quantidade_cama);


                        // Execute a consulta de inserção
                        comando.ExecuteNonQuery();

                        MessageBox.Show("Quarto associado ao utente com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    if (control1 == 1)
                    {
                        using (MySqlCommand comando = new MySqlCommand(query3, conexao))
                        {
                            // Adicione os parâmetros com os valores obtidos do formulário
                            comando.Parameters.AddWithValue("@QuartoId", idAdd);
                            comando.Parameters.AddWithValue("@IdUtente", idunt1);


                            // Execute a consulta de inserção
                            comando.ExecuteNonQuery();
                        }
                    }
                    if (control2 == 1)
                    {
                        using (MySqlCommand comando = new MySqlCommand(query3, conexao))
                        {
                            // Adicione os parâmetros com os valores obtidos do formulário
                            comando.Parameters.AddWithValue("@QuartoId", idAdd);
                            comando.Parameters.AddWithValue("@IdUtente", idunt2);


                            // Execute a consulta de inserção
                            comando.ExecuteNonQuery();
                        }
                    }
                    if (control3 == 1)
                    {
                        using (MySqlCommand comando = new MySqlCommand(query3, conexao))
                        {
                            // Adicione os parâmetros com os valores obtidos do formulário
                            comando.Parameters.AddWithValue("@QuartoId", idAdd);
                            comando.Parameters.AddWithValue("@IdUtente", idunt3);


                            // Execute a consulta de inserção
                            comando.ExecuteNonQuery();
                        }
                    }

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
                    //Ações adicionais se necessárias quando o ComboBox for esvaziado
                    Console.WriteLine("ComboBox agora está vazio!");
                }
                else
                {
                    // Recupera os dados armazenados da coluna específica para o item selecionado
                    if (Utente_.TryGetValue(selectedValue, out string id))
                    {
                        control2 = 1;
                        // Use specificColumnData as needed (e.g., assign it to a variable)
                        idunt2 = int.Parse(id.ToString());
                        Console.WriteLine(idunt2);
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
                    Console.WriteLine("ComboBox agora está vazio!");
                }
                else
                {
                    // Retrieve the stored specific column data for the selected item
                    if (Utente_.TryGetValue(selectedValue, out string id))
                    {
                        control3 = 1;
                        // Use specificColumnData as needed (e.g., assign it to a variable)
                        idunt3 = int.Parse(id.ToString());
                        Console.WriteLine(idunt3);
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
                cmd.CommandText = "SELECT q.idQuarto, q.estado, q.quantidade_cama, MAX(CASE WHEN u.rank = 1 THEN u.nome END) AS utente_1,  MAX(CASE WHEN u.rank = 2 THEN u.nome END) AS utente_2, MAX(CASE WHEN u.rank = 3 THEN u.nome END) AS utente_3 FROM quarto q LEFT JOIN (SELECT Quarto_idQuarto, nome, ROW_NUMBER() OVER (PARTITION BY Quarto_idQuarto ORDER BY nome) AS \"rank\"FROM utente) u ON q.idQuarto = u.Quarto_idQuarto GROUP BY q.idQuarto, q.estado, q.quantidade_cama;";

                DataTable dta = new DataTable();
                MySqlDataAdapter dataadapter = new MySqlDataAdapter(cmd);
                dataadapter.Fill(dta);
                dataGridView1.DataSource = dta;
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
            if (dataGridView1.SelectedRows.Count > 0)
            {
                try
                {
                    int rowIndex = dataGridView1.SelectedRows[0].Index;
                    int idQuarto = Convert.ToInt32(dataGridView1.Rows[rowIndex].Cells["idQuarto"].Value);

                    using (MySqlConnection conexao = new MySqlConnection("Server=localhost;Port=3306;Database=mydb;User ID=root;Password=ipbcurso"))
                    {
                        conexao.Open();

                        // Query para atualizar os dados do quarto
                        string query = "UPDATE quarto SET estado = @estado, quantidade_cama = @quantidade_cama WHERE idQuarto = @idQuarto";

                        using (MySqlCommand comando = new MySqlCommand(query, conexao))
                        {
                            // Adicione os parâmetros com os valores obtidos do formulário
                            comando.Parameters.AddWithValue("@idQuarto", idQuarto);
                            comando.Parameters.AddWithValue("@estado", comboBox_estado.SelectedItem.ToString());
                            comando.Parameters.AddWithValue("@quantidade_cama", int.Parse(textBox_camas.Text));

                            // Execute a consulta de atualização
                            comando.ExecuteNonQuery();

                            MessageBox.Show("Quarto atualizado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            display_data();
                            Limpar();
                        }
                    }
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

                        string query = "DELETE FROM quarto WHERE idQuarto = @idQuarto";

                        using (MySqlCommand comando = new MySqlCommand(query, conexao))
                        {
                            comando.Parameters.AddWithValue("@idQuarto", idQuarto);
                            comando.ExecuteNonQuery();

                            MessageBox.Show("Quarto excluído com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            display_data();
                            Limpar();
                        }
                    }
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
                    comboBox2_Utente.SelectedIndex = -1;
                    comboBox3_Utente.SelectedIndex = -1;
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
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                // Preencher os controles com os valores da célula selecionada
                textBox_camas.Text = row.Cells["quantidade_cama"].Value.ToString();
                comboBox_estado.SelectedItem = row.Cells["estado"].Value.ToString();

                // Se a célula "utente_1" for nula, definir ComboBox1_Utente para o item padrão
                if (row.Cells["utente_1"].Value == null || string.IsNullOrWhiteSpace(row.Cells["utente_1"].Value.ToString()))
                {
                    comboBox1_Utente.SelectedIndex = comboBox1_Utente.FindStringExact("-----------------");
                    comboBox2_Utente.SelectedIndex = comboBox2_Utente.FindStringExact("-----------------");
                    comboBox3_Utente.SelectedIndex = comboBox3_Utente.FindStringExact("-----------------");
                }
                else
                {
                    comboBox1_Utente.SelectedItem = row.Cells["utente_1"].Value.ToString();
                    comboBox2_Utente.SelectedItem = row.Cells["utente_2"].Value?.ToString();
                    comboBox3_Utente.SelectedItem = row.Cells["utente_3"].Value?.ToString();
                }

                
            }
        }

    }
}
