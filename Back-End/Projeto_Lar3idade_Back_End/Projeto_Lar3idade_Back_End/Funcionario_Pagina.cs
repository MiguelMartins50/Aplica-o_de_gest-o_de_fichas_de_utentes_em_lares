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
        public Funcionario_Pagina()
        {
            InitializeComponent();
            consultas1.Hide();
            
        }


        private void label1_Click(object sender, EventArgs e)
        {
            consultas1.Hide();
            utentes1.Show();
            label2.BackColor = this.BackColor;
            label1.BackColor = Color.White;
        }
        private void label2_Click(object sender, EventArgs e)
        {
            consultas1.Show();
            utentes1.Hide();
            label2.BackColor = Color.White;
            label1.BackColor = this.BackColor;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
    }
}
