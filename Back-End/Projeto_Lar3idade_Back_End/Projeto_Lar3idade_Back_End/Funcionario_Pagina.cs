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
    public partial class Funcionario_Pagina : Form
    {
        private Escalas_func escalas_Func1;
        public int iduser { get; set; }
        public Funcionario_Pagina(int IDuser)
        {
            InitializeComponent();
            this.iduser = IDuser;
            atividades_func1 = new Atividades_func(iduser);
            atividades_func1.Location = new Point(215, 60); 
            atividades_func1.Size = new Size(1022, 700);
            this.Controls.Add(atividades_func1);
            escalas_Func1 = new Escalas_func(iduser);
            escalas_Func1.Location = new Point(215, 60);
            escalas_Func1.Size = new Size(1022, 700);
            this.Controls.Add(escalas_Func1);
            consultas1.Hide();
            atividades_func1.Hide();
            escalas_Func1.Hide();

            Console.WriteLine("Id Utilizador do funcionario nº1:" + iduser);
            Console.WriteLine("Id Utilizador do funcionario nº2:" + IDuser);


        }


        private void label1_Click(object sender, EventArgs e)
        {
            consultas1.Hide();
            utentes1.Show();
            atividades_func1.Hide();
            escalas_Func1.Hide();
            label5.BackColor = this.BackColor;
            label2.BackColor = this.BackColor;
            label1.BackColor = Color.White;
            label3.BackColor = this.BackColor;

        }
        private void label2_Click(object sender, EventArgs e)
        {
            consultas1.Show();
            utentes1.Hide();
            atividades_func1.Hide();
            escalas_Func1.Hide();
            label5.BackColor = this.BackColor;
            label2.BackColor = Color.White;
            label1.BackColor = this.BackColor;
            label3.BackColor = this.BackColor;

        }

        private void label3_Click(object sender, EventArgs e)
        {
            consultas1.Hide();
            utentes1.Hide();
            atividades_func1.Show();
            escalas_Func1.Hide();
            label5.BackColor = this.BackColor;
            label2.BackColor = this.BackColor;
            label1.BackColor = this.BackColor;
            label3.BackColor = Color.White;
        }

        private void label5_Click(object sender, EventArgs e)
        {
            consultas1.Hide();
            utentes1.Hide();
            atividades_func1.Hide();
            escalas_Func1.Show();
            label2.BackColor = this.BackColor;
            label1.BackColor = this.BackColor;
            label3.BackColor = this.BackColor;
            label5.BackColor = Color.White;

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Hide();
        }
    }
}
