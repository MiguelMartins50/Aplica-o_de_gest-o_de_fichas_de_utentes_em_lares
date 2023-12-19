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
    public partial class pagamentos : UserControl
    {
        private int idAdd;
        private int control1;
        private int control2;
        private int idunt;
        private int idresp;
        private Dictionary<string, string> Utente_ = new Dictionary<string, string>();
        private Dictionary<string, string> resp_ = new Dictionary<string, string>();
        private MySqlConnection conexao;
       

        public pagamentos()
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


            using (conexao)
            {
                conexao.Open();

                string query = "SELECT idUtente,nome FROM utente";
                using (MySqlCommand cmd = new MySqlCommand(query, conexao))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string id = reader["idUtente"].ToString();
                            string tipo = reader["nome"].ToString();
                            Utente_[tipo] = id;
                            comboBox_utente.Items.Add(tipo);
                        }
                    }
                }

            }
        }
        private void comboBox_utente_SelectedIndexChanged(object sender, EventArgs e)
        {
            Console.WriteLine(control1);
            Console.WriteLine(control2);
            control1 = 1;
            if (comboBox_utente.SelectedItem != null)
            {
                string selectedValue = comboBox_utente.SelectedItem.ToString();

                if (Utente_.TryGetValue(selectedValue, out string id))
                {

                    // Use specificColumnData as needed (e.g., assign it to a variable)
                    idunt = int.Parse(id.ToString());
                    Console.WriteLine(id);

                    LoadComboBox2(idunt);
                }
            }
        }

        private void comboBox_responsavel_SelectedIndexChanged(object sender, EventArgs e)
        {
            Console.WriteLine(control1);
            Console.WriteLine(control2);
            control2 = 1;

            // Verifique se um item está selecionado no comboBox_responsavel
            if (comboBox_responsavel.SelectedItem != null)
            {
                string selectedValue = comboBox_responsavel.SelectedItem.ToString();
                Console.WriteLine(selectedValue);

                if (resp_.TryGetValue(selectedValue, out string id))
                {
                    // Use specificColumnData conforme necessário (por exemplo, atribua a uma variável)
                    Console.WriteLine(idresp + "início");
                    idresp = int.Parse(id.ToString());
                    Console.WriteLine("id" + id);
                    Console.WriteLine("idresp" + idresp);
                }
            }
        }



        private void button_insert_Click(object sender, EventArgs e)
        {
            string valor = textBox_valor.Text;
            DateTime datalimite = dateTimePicker_dataLimite.Value;
            string estado = textBox_estado.Text;

            try
            {
                using (MySqlConnection conexao = new MySqlConnection("Server=localhost;Port=3306;Database=mydb;User ID=root;Password=ipbcurso"))
                {
                    conexao.Open();
                    string query = "INSERT INTO mydb.pagamento (idPagamento,Utente_idUtente, Familiar_idFamiliar, data_limitel, valor, estado)" +
                                    "VALUES (@idPagamento, @Utente_idUtente, @Familiar_idFamiliar, @data_limitel, @valor, @estado)";
                    string query2 = "SELECT * FROM pagamento ORDER BY idPagamento DESC LIMIT 1";

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
                                idAdd = 1 + int.Parse(reader["idPagamento"].ToString());

                            }
                        }
                    }


                    // Crie um comando MySqlCommand
                    using (MySqlCommand comando = new MySqlCommand(query, conexao))
                    {
                        Console.WriteLine("id" + idAdd);
                        Console.WriteLine("utente" + idunt);
                        Console.WriteLine("responsavel " + idresp);
                        Console.WriteLine(datalimite);
                        Console.WriteLine(valor);
                        Console.WriteLine(estado);
                        comando.Parameters.AddWithValue("@idPagamento", idAdd);
                        comando.Parameters.AddWithValue("@Utente_idUtente", idunt);
                        comando.Parameters.AddWithValue("@Familiar_idFamiliar", idresp);
                        comando.Parameters.AddWithValue("@data_limitel", datalimite);
                        comando.Parameters.AddWithValue("@valor", valor);
                        comando.Parameters.AddWithValue("@estado", estado);

                        comando.ExecuteNonQuery();
                        
                        MessageBox.Show("Plano de mensalidade adicionado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);


                        Limpar();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao adicionar Pagamento: " + ex.Message);
            }

        }
        private void Limpar()
        {
            textBox_estado.Clear();
            textBox_valor.Clear();
            dateTimePicker_dataLimite.Value = DateTime.Now;
            comboBox_utente.SelectedIndex = -1;
            comboBox_responsavel.SelectedIndex = -1;
        }
        private void LoadComboBox2(int selectedUtenteId)
        {
            comboBox_responsavel.Items.Clear();
            comboBox_responsavel.SelectedIndex = -1; 
            using (conexao)
            {
                conexao.Open();
                string query3 = $"SELECT idFamiliar, nomel FROM familiar JOIN utente_familiar  ON idFamiliar = utente_familiar.Familiar_idFamiliar WHERE utente_familiar.Utente_idUtente ={selectedUtenteId};";
                using (MySqlCommand cmd = new MySqlCommand(query3, conexao))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string id = reader["idFamiliar"].ToString();
                            string nome = reader["nomel"].ToString();
                            comboBox_responsavel.Items.Add(nome);
                            resp_[nome] = id;
                            foreach (var kvp in resp_)
                            {
                                Console.WriteLine($"{kvp.Key}: {kvp.Value}");
                            }

                            Console.WriteLine("resp" + id);
                        }
                    }
                }
            }
        }
        private void display_data()
        {
            conexao.Open();
            MySqlCommand cmd = conexao.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT pagamento.idPagamento, pagamento.estado, pagamento.data_limitel, pagamento.valor, utente.nome, familiar.nomel FROM pagamento JOIN utente ON pagamento.Utente_idUtente = utente.idUtente JOIN familiar ON pagamento.Familiar_idFamiliar = familiar.idFamiliar;";
            cmd.ExecuteNonQuery();
            DataTable dta = new DataTable();
            MySqlDataAdapter dataadapter = new MySqlDataAdapter(cmd);
            dataadapter.Fill(dta);
            dataGridView1.DataSource = dta;
            conexao.Close();
        }

        private void button_update_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                try
                {
                    int rowIndex = dataGridView1.SelectedRows[0].Index;
                    int idPagamento = Convert.ToInt32(dataGridView1.Rows[rowIndex].Cells["idPagamento"].Value);

                    string valor = textBox_valor.Text;
                    DateTime datalimite = dateTimePicker_dataLimite.Value;
                    string estado = textBox_estado.Text;

                    using (MySqlConnection conexao = new MySqlConnection("Server=localhost;Port=3306;Database=mydb;User ID=root;Password=ipbcurso"))
                    {
                        conexao.Open();
                        string query = "UPDATE pagamento SET data_limitel = @data_limitel, valor = @valor, estado = @estado WHERE idPagamento = @idPagamento";

                        using (MySqlCommand comando = new MySqlCommand(query, conexao))
                        {
                            comando.Parameters.AddWithValue("@idPagamento", idPagamento);
                            comando.Parameters.AddWithValue("@data_limitel", datalimite);
                            comando.Parameters.AddWithValue("@valor", valor);
                            comando.Parameters.AddWithValue("@estado", estado);

                            comando.ExecuteNonQuery();

                            MessageBox.Show("Pagamento atualizado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            display_data();
                            Limpar();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao atualizar pagamento: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecione um pagamento para atualizar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private void button_Search_Click(object sender, EventArgs e)
        {
            conexao.Open();
            MySqlCommand cmd = conexao.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT pagamento.idPagamento, pagamento.estado, pagamento.data_limitel, pagamento.valor, utente.nome, familiar.nomel " +
                              "FROM pagamento " +
                              "JOIN utente ON pagamento.Utente_idUtente = utente.idUtente " +
                              "JOIN familiar ON pagamento.Familiar_idFamiliar = familiar.idFamiliar " +
                              "WHERE utente.nome LIKE @searchText OR pagamento.estado LIKE @searchText";
            cmd.Parameters.AddWithValue("@searchText", "%" + textBox_Search.Text + "%");
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;

            conexao.Close();

            // Clear fields only if there are search results
            if (dt.Rows.Count > 0)
            {
                textBox_estado.Clear();
                textBox_valor.Clear();
                dateTimePicker_dataLimite.Value = DateTime.Now;
                comboBox_utente.SelectedIndex = -1;
                comboBox_responsavel.SelectedIndex = -1;
            }
            else
            {
                MessageBox.Show("Nenhum resultado encontrado.");
            }
        }


        private void button_delete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                try
                {
                    int rowIndex = dataGridView1.SelectedRows[0].Index;
                    int idPagamento = Convert.ToInt32(dataGridView1.Rows[rowIndex].Cells["idPagamento"].Value);

                    using (MySqlConnection conexao = new MySqlConnection("Server=localhost;Port=3306;Database=mydb;User ID=root;Password=ipbcurso"))
                    {
                        conexao.Open();

                        string query = "DELETE FROM pagamento WHERE idPagamento = @idPagamento";

                        using (MySqlCommand comando = new MySqlCommand(query, conexao))
                        {
                            comando.Parameters.AddWithValue("@idPagamento", idPagamento);
                            comando.ExecuteNonQuery();

                            MessageBox.Show("Pagamento excluído com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            display_data();
                            Limpar();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao excluir pagamento: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecione um pagamento para excluir.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private void Mostrar_Click(object sender, EventArgs e)
        {
            Limpar();
            display_data();

        }

        

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Check if a valid row is clicked
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                // Assuming you have columns named "estado", "data_limitel", "valor", "nome", "nomel" in your DataGridView
                textBox_estado.Text = row.Cells["estado"].Value.ToString();
                textBox_valor.Text = row.Cells["valor"].Value.ToString();
                dateTimePicker_dataLimite.Value = Convert.ToDateTime(row.Cells["data_limitel"].Value);
                comboBox_utente.Text = row.Cells["nome"].Value.ToString();
                comboBox_responsavel.Text = row.Cells["nomel"].Value.ToString();
            }
        }

        
    }
}
