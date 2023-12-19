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
    public partial class tarefas : UserControl
    {
       
        private int idAdd;
        private int idtipo;
        private int idfunc;
        private Dictionary<string, string> Tipo_ = new Dictionary<string, string>();
        private Dictionary<string, string> Func_ = new Dictionary<string, string>();
        private MySqlConnection conexao;

        public tarefas()
        {
            InitializeComponent();
            string connectionString = "Server=localhost;Port=3306;Database=mydb;User ID=root;Password=ipbcurso";
            conexao = new MySqlConnection(connectionString);
            LoadComboBox();
            display_data();

        }
        private void LoadComboBox()
        {

            using (conexao)
            {
                conexao.Open();

                string query = "SELECT idTipo,tipo FROM tipo";
                using (MySqlCommand cmd = new MySqlCommand(query, conexao))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string id = reader["idTipo"].ToString();
                            string tipo = reader["tipo"].ToString();
                            Tipo_[tipo] = id;
                            comboBox_tipo.Items.Add(tipo);
                        }
                    }
                }
                string query2 = "SELECT idFuncionario,nome FROM funcionario";
                using (MySqlCommand cmd = new MySqlCommand(query2, conexao))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            string id = reader["idFuncionario"].ToString();
                            string nome = reader["nome"].ToString();
                            comboBox_funcionario.Items.Add(nome);
                            Func_[nome] = id;


                        }
                    }
                }
            }
        }


        private void button_insert_Click(object sender, EventArgs e)
        {
            string nome = textBox_nome.Text;
            string desc = richTextBox_descricao.Text;

            try
            {
                using (MySqlConnection conexao = new MySqlConnection("Server=localhost;Port=3306;Database=mydb;User ID=root;Password=ipbcurso"))
                {
                    conexao.Open();
                    string query = "INSERT INTO mydb.tarefa (idTarefa,nome, descricao, Funcionario_idFuncionario, Tipo_idTipo)" +
                                    "VALUES (@idTarefa, @nome, @descricao, @Funcionario_idFuncionario, @Tipo_idTipo)";
                    string query2 = "SELECT * FROM tarefa ORDER BY idTarefa DESC LIMIT 1";

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
                                idAdd = 1 + int.Parse(reader["idTarefa"].ToString());

                            }
                        }
                    }


                    // Crie um comando MySqlCommand
                    using (MySqlCommand comando = new MySqlCommand(query, conexao))
                    {
                        comando.Parameters.AddWithValue("@idTarefa", idAdd);
                        comando.Parameters.AddWithValue("@Funcionario_idFuncionario", idfunc);
                        comando.Parameters.AddWithValue("@nome", nome);
                        comando.Parameters.AddWithValue("@descricao", desc);
                        comando.Parameters.AddWithValue("@Tipo_idTipo", idtipo);

                        comando.ExecuteNonQuery();

                        MessageBox.Show("Atividade adicionado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                       
                        LimparTextBoxes();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao adicionar tarefa: " + ex.Message);
            }
        }
        private void LimparTextBoxes()
        {
            textBox_nome.Clear();
            comboBox_tipo.SelectedIndex = -1;
            comboBox_funcionario.SelectedIndex = -1;
            richTextBox_descricao.Clear();

        }
        private void comboBox_tipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox_tipo.SelectedItem != null)
            {
                string selectedValue = comboBox_tipo.SelectedItem.ToString();

                if (Tipo_.TryGetValue(selectedValue, out string id))
                {

                    // Use specificColumnData as needed (e.g., assign it to a variable)
                    idtipo = int.Parse(id.ToString());
                    Console.WriteLine(id);
                }
            }
        }

        private void comboBox_funcionario_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox_funcionario.SelectedItem != null)
            {
                string selectedValue = comboBox_funcionario.SelectedItem.ToString();

                if (Func_.TryGetValue(selectedValue, out string id))
                {

                    // Use specificColumnData as needed (e.g., assign it to a variable)
                    idfunc = int.Parse(id.ToString());
                    Console.WriteLine(idfunc);
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
                cmd.CommandText = "SELECT  T.idTarefa, T.nome AS nome_tarefa,  T.descricao, T.Funcionario_idFuncionario,   F.nome AS nome_funcionario,  T.Tipo_idTipo,   TIPO.tipo AS tipo_tarefa FROM   tarefa T INNER JOIN     funcionario F ON T.Funcionario_idFuncionario = F.idFuncionario INNER JOIN  tipo TIPO ON T.Tipo_idTipo = TIPO.idTipo;";

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
                    int idTarefa = Convert.ToInt32(dataGridView1.Rows[rowIndex].Cells["idTarefa"].Value);

                    using (MySqlConnection conexao = new MySqlConnection("Server=localhost;Port=3306;Database=mydb;User ID=root;Password=ipbcurso"))
                    {
                        conexao.Open();

                        string query = "UPDATE tarefa SET nome = @nome, descricao = @descricao, Funcionario_idFuncionario = @Funcionario_idFuncionario, Tipo_idTipo = @Tipo_idTipo WHERE idTarefa = @idTarefa";

                        using (MySqlCommand comando = new MySqlCommand(query, conexao))
                        {
                            comando.Parameters.AddWithValue("@idTarefa", idTarefa);
                            comando.Parameters.AddWithValue("@Funcionario_idFuncionario", idfunc);
                            comando.Parameters.AddWithValue("@nome", textBox_nome.Text);
                            comando.Parameters.AddWithValue("@descricao", richTextBox_descricao.Text);
                            comando.Parameters.AddWithValue("@Tipo_idTipo", idtipo);

                            comando.ExecuteNonQuery();

                            MessageBox.Show("Tarefa atualizada com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            display_data();
                            LimparTextBoxes();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao atualizar tarefa: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecione uma tarefa para editar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void button_delete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                try
                {
                    int rowIndex = dataGridView1.SelectedRows[0].Index;
                    int idTarefa = Convert.ToInt32(dataGridView1.Rows[rowIndex].Cells["idTarefa"].Value);

                    using (MySqlConnection conexao = new MySqlConnection("Server=localhost;Port=3306;Database=mydb;User ID=root;Password=ipbcurso"))
                    {
                        conexao.Open();

                        string query = "DELETE FROM tarefa WHERE idTarefa = @idTarefa";

                        using (MySqlCommand comando = new MySqlCommand(query, conexao))
                        {
                            comando.Parameters.AddWithValue("@idTarefa", idTarefa);
                            comando.ExecuteNonQuery();

                            MessageBox.Show("Tarefa excluída com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            display_data();
                            LimparTextBoxes();
                        }
                    }
                }

                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao excluir tarefa: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecione uma tarefa para excluir.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
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
            cmd.CommandText = "SELECT T.idTarefa, T.nome AS nome_tarefa, T.descricao, " +
                      "F.nome AS nome_funcionario, TIPO.tipo AS tipo_tarefa, " +
                      "T.Funcionario_idFuncionario, T.Tipo_idTipo " +
                      "FROM tarefa T " +
                      "INNER JOIN funcionario F ON T.Funcionario_idFuncionario = F.idFuncionario " +
                      "INNER JOIN tipo TIPO ON T.Tipo_idTipo = TIPO.idTipo " +
                      "WHERE T.nome LIKE @searchText OR SUBSTRING_INDEX(T.nome, ' ', 1) LIKE @searchText";
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
                textBox_nome.Text = "";
                comboBox_tipo.SelectedIndex = 0;
                comboBox_funcionario.SelectedIndex = 0;
                richTextBox_descricao.Text = "";
            }
            else
            {
                MessageBox.Show("Nenhum resultado encontrado.");
            }

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                // Preencher os TextBoxes com os valores da linha selecionada
                textBox_nome.Text = row.Cells["nome_tarefa"].Value.ToString();
                richTextBox_descricao.Text = row.Cells["descricao"].Value.ToString();

                // Selecionar itens no ComboBox com base nos valores da linha
                string tipoSelecionado = row.Cells["Tipo_idTipo"].Value.ToString();
                string funcionarioSelecionado = row.Cells["Funcionario_idFuncionario"].Value.ToString();

                comboBox_tipo.SelectedItem = comboBox_tipo.Items.Cast<string>()
                    .FirstOrDefault(item => Tipo_.TryGetValue(item, out string id) && id == tipoSelecionado);

                comboBox_funcionario.SelectedItem = comboBox_funcionario.Items.Cast<string>()
                    .FirstOrDefault(item => Func_.TryGetValue(item, out string id) && id == funcionarioSelecionado);
            }

        }

    }
}