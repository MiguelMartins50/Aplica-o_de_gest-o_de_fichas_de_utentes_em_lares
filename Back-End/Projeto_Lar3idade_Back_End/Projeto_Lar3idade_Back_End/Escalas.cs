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
        private int idADD;
        private int idADD2;
        private int idADD3;
        private string titulo = "";
        private DateTime Lastmonth;






        private MySqlConnection conexao;

        public Escalas()
        {
            InitializeComponent();
            conexao = new MySqlConnection(connectionString);
            comboBox3.Items.Add("trabalho");
            comboBox3.Items.Add("folga");
            comboBox3.Items.Add("falta");
            comboBox3.Items.Add("falta Justificada");
            Loadcomboboxes();
            display_data();
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
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
        private void Loadcomboboxes()
        {
            int count = 1;
            if (conexao.State != ConnectionState.Open)
            {
                conexao.Open();
            }
            Console.WriteLine("Begining");
            if (control == 0)
            {
                // Query for the first MySqlCommand
                string query1 = "SELECT * FROM escala_servico ORDER BY data_inicial;";
                Console.WriteLine("escalas");
                comboBox1.DataSource = null;
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
                            Lastmonth = Convert.ToDateTime(reader["data_final"]);
                            string id = Convert.ToString(reader["idEscala_servico"]);
                            Console.WriteLine(data_inicio);
                            Console.WriteLine(data_fim);
                            string month = data_inicio.ToString("MMMM yyyy", new System.Globalization.CultureInfo("pt-BR"));
                            comboBox1.Items.Add(data_inicio.ToString("MMMM yyyy", new System.Globalization.CultureInfo("pt-BR")));
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
                            tipo_func = tipo;
                            nome_func = nome;
                            if (tipo_func == "Funcionario")
                                titulo = nome_func + " Funcionario";
                            if (tipo_func == "Medico")
                                titulo = nome_func + " Medico";
                            comboBox2.Items.Add(titulo);
                            string countstr = Convert.ToString(count);
                            func_id[titulo] = id;
                            func_type[titulo] = tipo;
                            count++;
                            Console.WriteLine("counter:"+countstr);


                        }
                    }
                }

            }
        }
        private void display_data()
        {
            if (conexao.State != ConnectionState.Open)
            {
                conexao.Open();
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
            DialogResult result = MessageBox.Show("Tem a certeza que quer criar um novo mês de escalas?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == DialogResult.OK)
            {
                CriarEscalaServico();
            }


        }

        private void CriarEscalaServico()
        {
            int count = 1;
            Console.WriteLine($"Month: {Lastmonth.Month}, Year: {Lastmonth.Year}");
            int counter = 0;
            int dateadd;
            DateTime data_create = Lastmonth.AddDays(1);
            if (Lastmonth.Month == 2)
            {

                // February
                if (IsLeapYear(Lastmonth.Year))
                    dateadd = 29; // Leap year
                else
                    dateadd = 28; // Non-leap year
                Console.WriteLine("F");

            }
            else if (Lastmonth.Month == 4 || Lastmonth.Month == 6 || Lastmonth.Month == 9 || Lastmonth.Month == 11)
            {
                // April, June, September, November
                dateadd = 30;
                Console.WriteLine("A-J-S-N");
            }
            else
            {
                // Other months (excluding February, April, June, September, November)
                dateadd = 31;
                Console.WriteLine("J-M-M-J-A-O-D");

            }
            bool IsLeapYear(int year)
            {
                return (year % 4 == 0 && year % 100 != 0) || (year % 400 == 0);
            }
            // ...

            // Function to check if a year is a leap year
            try
            {
                if (conexao.State != ConnectionState.Open)
                {
                    conexao.Open();
                }
                if (tipo_func == "Funcionario")
                {
                    while (counter < dateadd) // Adjust the condition based on your requirements
                    {
                        string Querryid_func = "SELECT idFuncionario_Escala FROM funcionario_escala ORDER BY idFuncionario_Escala DESC LIMIT 1";
                        using (MySqlCommand cmd = new MySqlCommand(Querryid_func, conexao))
                        {
                            using (MySqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.Read()) // Check if there are records before trying to read
                                {
                                    idADD2 = Convert.ToInt32(reader["idFuncionario_Escala"]) + 1;
                                }
                                else
                                {
                                    MessageBox.Show("ultimo id nao encontrado-ESCALA-FUNCIONARIO");
                                }
                            }

                        }
                        string insertFuncionarioEscalaQuery = "INSERT INTO funcionario_escala (idFuncionario_Escala,Funcionario_idFuncionario, Escala_servico_idEscala_servico, Dia,dia_da_semana, horario_inicio, horario_fim, estado) VALUES (@idFuncionario_Escala,@funcionario, @idescala, @dia, @dia_da_semana, @horario_inicio, @horario_fim, @estado);";
                        using (MySqlCommand cmdInsertFuncionarioEscala = new MySqlCommand(insertFuncionarioEscalaQuery, conexao))
                        {
                            cmdInsertFuncionarioEscala.Parameters.AddWithValue("@idFuncionario_Escala", idADD2);
                            cmdInsertFuncionarioEscala.Parameters.AddWithValue("@funcionario", id_func);
                            cmdInsertFuncionarioEscala.Parameters.AddWithValue("@idescala", idescala);
                            cmdInsertFuncionarioEscala.Parameters.AddWithValue("@dia", data_create);
                            string dia_da_semana = data_create.ToString("dddd", new System.Globalization.CultureInfo("pt-BR"));
                            cmdInsertFuncionarioEscala.Parameters.AddWithValue("@dia_da_semana", dia_da_semana);
                            cmdInsertFuncionarioEscala.Parameters.AddWithValue("@horario_inicio", "00:00:00"); // Replace with your logic for setting the start time
                            cmdInsertFuncionarioEscala.Parameters.AddWithValue("@horario_fim", "00:00:00"); // Replace with your logic for setting the end time
                            cmdInsertFuncionarioEscala.Parameters.AddWithValue("@estado", "Sem Estado"); // Replace with your logic for setting the estado
                            cmdInsertFuncionarioEscala.ExecuteNonQuery();
                        }
                        Console.WriteLine("Acabou de criar uma escala_funcionario");
                        count++;
                        counter++;
                        Console.WriteLine(Convert.ToString(count));
                        data_create = data_create.AddDays(1);
                    }
                }



                // Create entries in escala_medico for each medico for each day
                if (tipo_func == "Medico")
                {


                    while (counter < dateadd) // Adjust the condition based on your requirements
                    {
                        string Querryid_med = "SELECT idEscala_Medico FROM escala_medico ORDER BY idEscala_Medico DESC LIMIT 1";
                        using (MySqlCommand cmd = new MySqlCommand(Querryid_med, conexao))
                        {
                            using (MySqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.Read()) // Check if there are records before trying to read
                                {
                                    idADD3 = Convert.ToInt32(reader["idEscala_Medico"]) + 1;
                                }
                                else
                                {
                                    MessageBox.Show("ultimo id nao encontrado-ESCALA-MEDICO");
                                }
                            }

                        }
                        string insertEscalaMedicoQuery = "INSERT INTO escala_medico (idEscala_Medico,Medico_idMedico, Escala_servico_idEscala_servico, Dia,dia_da_semana, horario_inicio, horario_fim, estado) VALUES (@idEscala_Medico,@medico, @idescala, @dia,@dia_da_semana, @horario_inicio, @horario_fim, @estado);";
                        using (MySqlCommand cmdInsertEscalaMedico = new MySqlCommand(insertEscalaMedicoQuery, conexao))
                        {
                            cmdInsertEscalaMedico.Parameters.AddWithValue("@idEscala_Medico", idADD3);
                            cmdInsertEscalaMedico.Parameters.AddWithValue("@medico", id_func);
                            cmdInsertEscalaMedico.Parameters.AddWithValue("@idescala", idescala);
                            cmdInsertEscalaMedico.Parameters.AddWithValue("@dia", data_create);
                            string dia_da_semana = data_create.ToString("dddd", new System.Globalization.CultureInfo("pt-BR"));
                            cmdInsertFuncionarioEscala.Parameters.AddWithValue("@dia_da_semana", dia_da_semana);
                            cmdInsertEscalaMedico.Parameters.AddWithValue("@horario_inicio", "00:00:00"); // Replace with your logic for setting the start time
                            cmdInsertEscalaMedico.Parameters.AddWithValue("@horario_fim", "00:00:00"); // Replace with your logic for setting the end time
                            cmdInsertEscalaMedico.Parameters.AddWithValue("@estado", "Sem Estado"); // Replace with your logic for setting the estado
                            cmdInsertEscalaMedico.ExecuteNonQuery();

                        }
                        Console.WriteLine("Acabou de criar uma escala_medico");
                        count++;
                        counter++;
                        Console.WriteLine(Convert.ToString(count));
                        data_create = data_create.AddDays(1);
                    }
                }
                Console.WriteLine("Acabou de criar as escala_medico");
                Console.WriteLine("Acabou o INSERT");

                // Display the updated data
                display_data();
                MessageBox.Show("Escala_servico and associated entries created successfully");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error creating escala_servico and associated entries: " + ex.Message);
            }
            finally
            {
                // Close the connection
                conexao.Close();
            }

            
            /*
                Console.WriteLine("Acabou de criar a escala");
                // Get the ID of the newly created escala_servico
                int idEscalaServico;
                string getLastInsertIdQuery = "SELECT LAST_INSERT_ID();";
                using (MySqlCommand cmdLastInsertId = new MySqlCommand(getLastInsertIdQuery, conexao))
                {
                    idEscalaServico = Convert.ToInt32(cmdLastInsertId.ExecuteScalar());
                }

                // Create entries in funcionario_escala for each funcionario for each day
                
                
            }
           */
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
                    tipo_func = type; // Just assign the value directly without converting to string
                    Console.WriteLine(tipo_func);
                    Console.WriteLine("IN_tipo");
                    control = 1;

                    display_data();
                }
                Console.WriteLine("tipo: " + tipo_func);
                Console.WriteLine("Nome: " + selectedValue);

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            control = 0;
            display_data();
            comboBox1.Items.Clear();
            comboBox2.Items.Clear();
            Loadcomboboxes();
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
                if (tipo_func == "Funcionario")
                {
                    int idescalaParaEditar = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["idFuncionario_Escala"].Value);
                    Console.WriteLine("id_func: " + idescalaParaEditar);
                    PreencherCamposParaEdicao(idescalaParaEditar);
                }
                if (tipo_func == "Medico")
                {
                    int idescalaParaEditar = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["idEscala_Medico"].Value);
                    Console.WriteLine("id_med: " + idescalaParaEditar);

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
                if (tipo_func == "Funcionario")
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
                        cmd.Parameters.AddWithValue("@horario_inicio", Convert.ToDateTime(textBox1.Text).TimeOfDay);
                        cmd.Parameters.AddWithValue("@horario_fim", Convert.ToDateTime(textBox2.Text).TimeOfDay);
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

        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Tem a certeza que quer criar um novo mês de escalas?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == DialogResult.OK)
            {
                // Open the connection
                if (conexao.State != ConnectionState.Open)
                {
                    conexao.Open();
                }

                try
                {
                    string Querryid = "SELECT idEscala_servico FROM escala_servico ORDER BY idEscala_servico DESC LIMIT 1;";
                    using (MySqlCommand cmd = new MySqlCommand(Querryid, conexao))
                    {
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read()) // Check if there are records before trying to read
                            {
                                idADD = Convert.ToInt32(reader["idEscala_servico"]) + 1;
                            }
                            else
                            {
                                MessageBox.Show("ultimo id nao encontrado-ESCALA");
                            }
                        }
                    }
                    Console.WriteLine($"Month: {Lastmonth.Month}, Year: {Lastmonth.Year}");

                    int dateadd;
                    DateTime data_create = Lastmonth.AddDays(1);
                    if (Lastmonth.Month == 2)
                    {

                        // February
                        if (IsLeapYear(Lastmonth.Year))
                            dateadd = 29; // Leap year
                        else
                            dateadd = 28; // Non-leap year
                        Console.WriteLine("F");

                    }
                    else if (Lastmonth.Month == 4 || Lastmonth.Month == 6 || Lastmonth.Month == 9 || Lastmonth.Month == 11)
                    {
                        // April, June, September, November
                        dateadd = 30;
                        Console.WriteLine("A-J-S-N");
                    }
                    else
                    {
                        // Other months (excluding February, April, June, September, November)
                        dateadd = 31;
                        Console.WriteLine("J-M-M-J-A-O-D");

                    }
                    bool IsLeapYear(int year)
                    {
                        return (year % 4 == 0 && year % 100 != 0) || (year % 400 == 0);
                    }
                    // Create a new entry in escala_servico
                    string QuerryESCALAS = "INSERT INTO escala_servico (idEscala_servico,data_inicial, data_final) VALUES (@idEscala_servico,@data_inicial, @data_final);";
                    using (MySqlCommand cmd = new MySqlCommand(QuerryESCALAS, conexao))
                    {
                        cmd.Parameters.AddWithValue("@idEscala_servico", idADD); // Replace with your desired data_inicial

                        cmd.Parameters.AddWithValue("@data_inicial", Lastmonth.AddDays(1)); // Replace with your desired data_inicial

                        cmd.Parameters.AddWithValue("@data_final", Lastmonth.AddDays(dateadd)); // Replace with your desired data_final

                        cmd.ExecuteNonQuery();
                    }
                    MessageBox.Show("Mês foi criado com sucesso");
                    display_data();
                    comboBox1.Items.Clear();
                    comboBox2.Items.Clear();

                    Loadcomboboxes();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error creating escala_servico and associated entries: " + ex.Message);
                }
                finally
                {
                    // Close the connection
                    conexao.Close();
                }

            }
        }
    }
}