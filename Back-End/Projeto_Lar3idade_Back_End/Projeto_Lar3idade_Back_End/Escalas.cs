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
    public partial class Escalas : UserControl
    {
        private string connectionString = "Server=localhost;Port=3306;Database=mydb;User ID=root;Password=ipbcurso";
        private DateTime data_inicio;
        private DateTime data_fim;
        private Dictionary<string, string> mes_begin = new Dictionary<string, string>();
        private Dictionary<string, string> mes_end = new Dictionary<string, string>();
        private Dictionary<string, string> func_id = new Dictionary<string, string>();
        private Dictionary<string, string> func_type = new Dictionary<string, string>();
        private int control = 0;
        private int id_func = 1;
        private string tipo_func = "";
        private string nome_func = "";
        private string estado;
        private int idescala_func;
        private int idescala_med;
        private int idescala;
        private int month;
        private int year;
        private int day_inicio;
        private int day_final;





        private MySqlConnection conexao;

        public Escalas()
        {
            InitializeComponent();
            conexao = new MySqlConnection(connectionString);
            comboBox3.Items.Add("trabalho");
            comboBox3.Items.Add("folga");
            comboBox3.Items.Add("falta");
            comboBox3.Items.Add("falta Justificada");
            display_data();

        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (comboBox1.SelectedItem != null)
            {
                string selectedValue = comboBox1.SelectedItem.ToString();
                if (mes_begin.TryGetValue(selectedValue, out string id))
                {
                    idescala = Convert.ToInt32(id);
                    Console.WriteLine(data_inicio);
                    Console.WriteLine("IN_time");
                    display_data();
                    control = 1;

                }
                
            }
        }
        private void Reload()
        {

        }
        private void display_data()
        {

            if (conexao.State != ConnectionState.Open)
            {
                conexao.Open();
            }

            if (control == 0)
            {
                Console.WriteLine("Begining");

                // Query for the first MySqlCommand
                string query1 = "SELECT * FROM escala_servico ORDER BY data_inicial;";
                Console.WriteLine();
                using (MySqlCommand command1 = new MySqlCommand(query1, conexao))
                {
                    // Execute the first query
                    using (MySqlDataReader reader = command1.ExecuteReader())
                    {
                        // Create a list to store data
                        List<string[]> data = new List<string[]>();

                        // Iterate through the results
                        while (reader.Read())
                        {
                            // Add data to the list

                            data_inicio = Convert.ToDateTime(reader["data_inicial"]);
                            data_fim = Convert.ToDateTime(reader["data_final"]);
                            string id = Convert.ToString(reader["idEscala_servico"]);
                            Console.WriteLine(data_inicio);
                            Console.WriteLine(data_fim);
                            string month = data_inicio.ToString("MMMM yyyy", new System.Globalization.CultureInfo("pt-BR"));
                            comboBox1.Items.Add(month);
                            mes_begin[month] = id;
                           
                        }
                    }
                }

                Console.WriteLine("Begining_func");

                // Query for the second MySqlCommand
                string query2 = "SELECT nome, idFuncionario as id, 'Funcionario' as tipo FROM funcionario UNION SELECT nome, idMedico as id, 'Medico' as tipo FROM medico;";
                Console.WriteLine();
                using (MySqlCommand command2 = new MySqlCommand(query2, conexao))
                {
                    // Execute the second query
                    using (MySqlDataReader reader = command2.ExecuteReader())
                    {
                        // Create a list to store data
                        List<string[]> data = new List<string[]>();

                        // Iterate through the results
                        while (reader.Read())
                        {
                            // Add data to the list
                            string nome = Convert.ToString(reader["nome"]);
                            string id = Convert.ToString(reader["id"]);
                            string tipo = Convert.ToString(reader["tipo"]);
                            /*string titulo = Convert.ToString(reader["nome"]) + "-" + Convert.ToString(reader["tipo"]);
                            Console.WriteLine(titulo);*/
                            Console.WriteLine(nome);
                            Console.WriteLine(id);
                            Console.WriteLine(tipo);
                           
                            nome_func = nome;
                            comboBox2.Items.Add(nome_func);
                            func_id[nome] = id;
                            func_type[nome] = tipo;
                        }
                    }
                }
                comboBox1.SelectedIndex = 0;
                comboBox2.SelectedIndex = 0;

            }
          
            Console.WriteLine(tipo_func);
            MySqlCommand cmd = conexao.CreateCommand();
            cmd.CommandType = CommandType.Text;
            if (tipo_func == "Funcionario")
            {
                cmd.CommandText = "SELECT idFuncionario_Escala, Escala_servico_idEscala_servico,Dia, dia_da_semana, horario_inicio, horario_fim, estado FROM funcionario_escala WHERE Funcionario_idFuncionario = @funcionario AND Escala_servico_idEscala_servico = @idescala;";
                cmd.Parameters.AddWithValue("@funcionario", id_func);
                cmd.Parameters.AddWithValue("@idescala", idescala);
                Console.WriteLine("IN_FUNC");
            }
            if (tipo_func == "Medico")
            {
                cmd.CommandText = "SELECT idEscala_Medico,Escala_servico_idEscala_servico,Dia,dia_da_semana,horario_inicio,horario_fim,estado FROM escala_medico WHERE Medico_idMedico = @medico AND Escala_servico_idEscala_servico = @idescala;";
                cmd.Parameters.AddWithValue("@medico", id_func);
                cmd.Parameters.AddWithValue("@idescala", idescala);
                Console.WriteLine("IN_MED");
            }

            cmd.ExecuteNonQuery();
            DataTable dta = new DataTable();
            MySqlDataAdapter dataadapter = new MySqlDataAdapter(cmd);
            dataadapter.Fill(dta);
            dataGridView1.DataSource = dta;
            
        }
        private void button1_Click(object sender, EventArgs e)
        {



        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedItem != null)
            {
                string selectedValue = comboBox2.SelectedItem.ToString();
                if (func_id.TryGetValue(selectedValue, out string id))
                {
                    id_func = Convert.ToInt32(id);
                    Console.WriteLine(id_func);
                    Console.WriteLine("IN_id");
                    control = 1;

                }
                if (func_type.TryGetValue(selectedValue, out string type))
                {
                    tipo_func = Convert.ToString(type);
                    Console.WriteLine(tipo_func);
                    Console.WriteLine("IN_tipo");

                    control = 1;

                }
                Console.WriteLine("tipo: "+ tipo_func);
                display_data();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            display_data();
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox3.SelectedItem != null)
            {
                estado = comboBox3.SelectedItem.ToString();
            }

        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {   
                if(tipo_func == "Funcionario")
                {
                    int idescalaParaEditar = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["idFuncionario_Escala"].Value);
                    Console.WriteLine("id_func: "+ idescalaParaEditar);
                    PreencherCamposParaEdicao(idescalaParaEditar);
                }
                if (tipo_func == "Medico")
                {
                    int idescalaParaEditar = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["idEscala_Medico"].Value);
                    Console.WriteLine("id_med: "+idescalaParaEditar);

                    PreencherCamposParaEdicao(idescalaParaEditar);
                }

            }
        }
        private void PreencherCamposParaEdicao(int idescalaParaEditar)
        {

            try
            {
                if (conexao.State != ConnectionState.Open)
                {
                    conexao.Open();
                }

                MySqlCommand cmd = conexao.CreateCommand();
                cmd.CommandType = CommandType.Text;
                if (tipo_func == "Funcionario")
                {
                    cmd.CommandText = "SELECT idFuncionario_Escala,Dia, dia_da_semana, horario_inicio, horario_fim, estado FROM funcionario_escala WHERE idFuncionario_Escala = @idescala_func";
                    cmd.Parameters.AddWithValue("@idescala_func", idescalaParaEditar);
                    DataTable dta = new DataTable();
                    
                    MySqlDataAdapter dataadapter = new MySqlDataAdapter(cmd);
                    dataadapter.Fill(dta);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Atualize a variável idUtente com o valor do idUtenteParaEditar
                            
                                estado = Convert.ToString(dta.Rows[0]["estado"]);
                                Console.WriteLine(estado + " :estado1-func");
                                idescala_func = idescalaParaEditar;
                            
                                
                          


                            // Preenche os campos com os dados do utente
                            Console.WriteLine(estado + " :estado2-func");

                            comboBox3.Text = estado;
                            textBox1.Text = reader["horario_inicio"].ToString();
                            textBox2.Text = reader["horario_fim"].ToString();

                        }
                    }
                }
                if (tipo_func == "Medico")
                {
                    cmd.CommandText = "SELECT idEscala_Medico,Dia, dia_da_semana, horario_inicio, horario_fim, estado FROM escala_medico WHERE Medico_idMedico = @Idescala_med";
                    cmd.Parameters.AddWithValue("@Idescala_med", idescalaParaEditar);
                    DataTable dta = new DataTable();

                    MySqlDataAdapter dataadapter = new MySqlDataAdapter(cmd);
                    dataadapter.Fill(dta);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Atualize a variável idUtente com o valor do idUtenteParaEditar
                                estado = Convert.ToString(dta.Rows[0]["estado"]);
                                Console.WriteLine(estado + " :estado1-func");
                                idescala_med = idescalaParaEditar;


                            // Preenche os campos com os dados do utente
                            Console.WriteLine(estado + " :estado2-med");

                            comboBox3.Text = estado;
                            textBox1.Text = reader["horario_inicio"].ToString();
                            textBox2.Text = reader["horario_fim"].ToString();

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
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                conexao.Open();

                MySqlCommand cmd = conexao.CreateCommand();
                cmd.CommandType = CommandType.Text;
                if(tipo_func == "Funcionario")
                {
                    if (!string.IsNullOrEmpty(textBox1.Text) && !string.IsNullOrEmpty(textBox2.Text))
                    {
                        // Obtém o valor do idUtente dos TextBoxes
                        // Utilizando parâmetros para prevenir injeção de SQL
                        cmd.CommandText = "UPDATE `mydb`.`funcionario_escala` SET `horario_inicio` = @horario_inicio, `horario_fim` = @horario_fim, `estado` = @estado WHERE (`idFuncionario_Escala` = @idFuncionario_Escala);";

                        // Adicionando parâmetros
                        cmd.Parameters.AddWithValue("@horario_inicio", Convert.ToDateTime(textBox1.Text).TimeOfDay);
                        cmd.Parameters.AddWithValue("@horario_fim", Convert.ToDateTime(textBox2.Text).TimeOfDay);
                        cmd.Parameters.AddWithValue("@estado", comboBox3.Text);
                        cmd.Parameters.AddWithValue("@idFuncionario_Escala", idescala_func);

                        // Executando o comando
                        cmd.ExecuteNonQuery();
                        conexao.Close();

                        // Limpando os campos e atualizando a exibição dos dados
                        display_data();

                        MessageBox.Show("Dados da escala atualizados com sucesso");
                    }
                    else
                    {
                        MessageBox.Show("Por favor, selecione uma linha para editar ou preencha os campos obrigatórios.");
                    }
                }
                if (tipo_func == "Medico")
                {
                    if (!string.IsNullOrEmpty(textBox1.Text) && !string.IsNullOrEmpty(textBox2.Text))
                    {
                        // Obtém o valor do idUtente dos TextBoxes
                        // Utilizando parâmetros para prevenir injeção de SQL
cmd.CommandText = "UPDATE `mydb`.`escala_medico` SET `horario_inicio` = @horario_inicio, `horario_fim` = @horario_fim, `estado` = @estado WHERE (`idEscala_Medico` = @idEscala_Medico);";

                        // Adicionando parâmetros
                        cmd.Parameters.AddWithValue("@horario_inicio",Convert.ToDateTime(textBox1.Text).TimeOfDay);
                        cmd.Parameters.AddWithValue("@horario_fim",Convert.ToDateTime(textBox2.Text).TimeOfDay);
                        cmd.Parameters.AddWithValue("@estado", comboBox3.Text);
                        cmd.Parameters.AddWithValue("@idEscala_Medico", idescala_med);

                        // Executando o comando
                        cmd.ExecuteNonQuery();
                        conexao.Close();

                        // Limpando os campos e atualizando a exibição dos dados
                        display_data();

                        MessageBox.Show("Dados da escala atualizados com sucesso");
                    }
                    else
                    {
                        MessageBox.Show("Por favor, selecione uma linha para editar ou preencha os campos obrigatórios.");
                    }
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao atualizar dados das escalas: " + ex.Message);
            }
            finally
            {
                conexao.Close();
            }
        }
    }
}