using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projeto_Lar3idade_Back_End
{
    public partial class Medico_Utente : UserControl
    {
        private MySqlConnection conexao;
        public Medico_Utente()
        {
            InitializeComponent();
            string connectionString = "Server=projetolar3idade.mysql.database.azure.com;Port=3306;Database=mydb;Uid=projeto4461045279;Pwd=Ipbcurso1";
            conexao = new MySqlConnection(connectionString);
            display_data();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {


        }
        private void button_Search_Click(object sender, EventArgs e)
        {
            conexao.Open();

            MySqlCommand cmd = conexao.CreateCommand();
            cmd.CommandType = CommandType.Text;

            // Utilizando o parâmetro @searchText na consulta SQL para buscar pelo nome
            cmd.CommandText = "SELECT * FROM utente WHERE nome LIKE @searchText;";
            cmd.Parameters.AddWithValue("@searchText", "%" + textBox_Search.Text + "%");

            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(dt);

            // Verificando se há resultados da pesquisa
            if (dt.Rows.Count > 0)
            {
                dataGridView1.DataSource = dt;
            }
            else
            {
                // Nenhum resultado encontrado, exibir mensagem
                MessageBox.Show("Nenhum resultado encontrado.");
                // Limpar os campos de texto ou realizar outras ações necessárias
            }

            conexao.Close();
        }




        private void display_data()
        {
            conexao.Open();
            MySqlCommand cmd = conexao.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Select * from utente";

            cmd.ExecuteNonQuery();
            DataTable dta = new DataTable();
            MySqlDataAdapter dataadapter = new MySqlDataAdapter(cmd);
            dataadapter.Fill(dta);
            dataGridView1.DataSource = dta;
            conexao.Close();
        }


    }
}
