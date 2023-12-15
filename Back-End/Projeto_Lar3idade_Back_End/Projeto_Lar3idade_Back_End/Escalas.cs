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
        private Dictionary<string, string> semana_begin = new Dictionary<string, string>();
        private Dictionary<string, string> semana_end = new Dictionary<string, string>();
        private Dictionary<string, string> func_id = new Dictionary<string, string>();
        private Dictionary<string, string> func_type = new Dictionary<string, string>();
        private int control = 0;
        private int id_func = 1;
        private string tipo_func = "";
        private string estado;
        private int idescala_func;
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
            display_data();

        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (comboBox1.SelectedItem != null)
            {
                string selectedValue = comboBox1.SelectedItem.ToString();
                if (semana_begin.TryGetValue(selectedValue, out string date_begin))
                {
                    data_inicio = Convert.ToDateTime(date_begin);
                    Console.WriteLine(data_inicio);
                    Console.WriteLine("IN_time");
                    display_data();
                    control = 1;

                }
                if (semana_end.TryGetValue(selectedValue, out string date_end))
                {
                    data_fim = Convert.ToDateTime(date_end);
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
            if(control == 0) 
            {
                Console.WriteLine("Begining");
                string query = "SELECT * FROM escala_servico ORDER BY data_inicial;";
                Console.WriteLine();
                using (MySqlCommand command = new MySqlCommand(query, conexao))
                {
                    // Execute the query
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        // Create a list to store data
                        List<string[]> data = new List<string[]>();

                        // Iterate through the results
                        while (reader.Read())
                        {
                            // Add data to the list
                            data_inicio = Convert.ToDateTime(reader["data_inicial"]);
                            data_fim = Convert.ToDateTime(reader["data_final"]);
                            string data_begin = Convert.ToString(reader["data_inicial"]);
                            string data_end = Convert.ToString(reader["data_final"]);
                            Console.WriteLine(data_inicio);
                            Console.WriteLine(data_fim);
                            string week = Convert.ToString(data_inicio);
                            comboBox1.Items.Add(week);
                            semana_begin[week] = data_begin;
                            semana_end[week] = data_end;


                        }


                    }
                }
                Console.WriteLine("Begining_func");
                string query2 = "SELECT nome, idFuncionario as id, 'Funcionario' as tipo FROM funcionario UNION SELECT nome, idMedico as id, 'Medico' as tipo FROM medico;";
                Console.WriteLine();
                using (MySqlCommand command = new MySqlCommand(query2, conexao))
                {
                    // Execute the query
                    using (MySqlDataReader reader = command.ExecuteReader())
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
                            Console.WriteLine(nome);
                            Console.WriteLine(id);
                            Console.WriteLine(tipo);
                            comboBox2.Items.Add(nome);
                            func_id[nome] = id;
                            func_type[nome] = tipo;


                        }


                    }
                }
                comboBox1.SelectedIndex = 0;
                comboBox2.SelectedIndex = 0;
            }
            MySqlCommand cmd = conexao.CreateCommand();
            cmd.CommandType = CommandType.Text;
            if (tipo_func == "Funcionario")
            {
                cmd.CommandText = "SELECT  Dia, dia_da_semana, horario_inicio, horario_fim, estado FROM funcionario_escala WHERE Funcionario_idFuncionario = @funcionario;";
                cmd.Parameters.AddWithValue("@funcionario", id_func);
                Console.WriteLine("IN_FUNC");
            }
            if (tipo_func == "Medico")
            {
                cmd.CommandText = "SELECT Dia, dia_da_semana, horario_inicio, horario_fim, estado FROM escala_medico WHERE Medico_idMedico = @medico;";
                cmd.Parameters.AddWithValue("@medico", id_func);
                Console.WriteLine("IN_MED");
            }

            cmd.ExecuteNonQuery();
            DataTable dta = new DataTable();
            MySqlDataAdapter dataadapter = new MySqlDataAdapter(cmd);
            dataadapter.Fill(dta);
            dataGridView1.DataSource = dta;
            comboBox3.Items.Add("Trabalho");
            comboBox3.Items.Add("Folga");
            comboBox3.Items.Add("Falta");
            comboBox3.Items.Add("Falta Justificada");
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
                display_data();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox3.SelectedItem != null)
            {
                estado = comboBox3.SelectedItem.ToString();
            }

        }
    }
}
