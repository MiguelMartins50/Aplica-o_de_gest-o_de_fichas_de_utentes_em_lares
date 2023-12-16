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
    public partial class admin : Form
    {
        public admin()
        {
            
            InitializeComponent();
            escalas1.Hide();
            funcionario1.Hide();
            responsavel1.Hide();
            atividades1.Hide();
            tarefas1.Hide();
            pagamentos1.Hide();
            quartos1.Hide();
           // utentes1.ButtonClicked += utenteControl2_ButtonClicked;
           
          //  funcionario1.ButtonClicked += funcControl2_ButtonClicked;
            
        }
        
        

        private void button1_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            escalas1.Hide();
            
            responsavel1.Hide();
            utentes1.Show();
            funcionario1.Hide();
            atividades1.Hide();
            tarefas1.Hide();
            pagamentos1.Hide();
            quartos1.Hide();
            label1.BackColor = Color.White;
            label2.BackColor = this.BackColor;
            label3.BackColor = this.BackColor;
            label4.BackColor = this.BackColor;
            label5.BackColor = this.BackColor;
            label6.BackColor = this.BackColor;
            label7.BackColor = this.BackColor;
            label8.BackColor = this.BackColor;

        }

        private void label2_Click(object sender, EventArgs e)
        {
            escalas1.Hide();
            
            responsavel1.Hide();
            utentes1.Hide();
            funcionario1.Show();
            atividades1.Hide();
            tarefas1.Hide();
            pagamentos1.Hide();
            quartos1.Hide();
            label1.BackColor = this.BackColor;
            label2.BackColor = Color.White;
            label3.BackColor = this.BackColor;
            label4.BackColor = this.BackColor;
            label5.BackColor = this.BackColor;
            label6.BackColor = this.BackColor;
            label7.BackColor = this.BackColor;
            label8.BackColor = this.BackColor;

        }

        private void label3_Click(object sender, EventArgs e)
        {
            escalas1.Hide();
            
            responsavel1.Show();
            utentes1.Hide();
            funcionario1.Hide();
            atividades1.Hide();
            tarefas1.Hide();
            pagamentos1.Hide();
            quartos1.Hide();
            label1.BackColor = this.BackColor;
            label2.BackColor = this.BackColor;
            label3.BackColor = Color.White;
            label4.BackColor = this.BackColor;
            label5.BackColor = this.BackColor;
            label6.BackColor = this.BackColor;
            label7.BackColor = this.BackColor;
            label8.BackColor = this.BackColor;

        }

        private void label4_Click(object sender, EventArgs e)
        {
           escalas1.Hide();
            
            responsavel1.Hide();
            utentes1.Hide();
            funcionario1.Hide();
            atividades1.Show();
            tarefas1.Hide();
            pagamentos1.Hide();
            quartos1.Hide();
            label1.BackColor = this.BackColor;
            label2.BackColor = this.BackColor;
            label3.BackColor = this.BackColor;
            label4.BackColor = Color.White;
            label5.BackColor = this.BackColor;
            label6.BackColor = this.BackColor;
            label7.BackColor = this.BackColor;
            label8.BackColor = this.BackColor;

        }

        private void label5_Click(object sender, EventArgs e)
        {
            escalas1.Hide();
            
            responsavel1.Hide();
            utentes1.Hide();
            funcionario1.Hide();
            atividades1.Hide();
            tarefas1.Show();
            pagamentos1.Hide();
            quartos1.Hide();
            label1.BackColor = this.BackColor;
            label2.BackColor = this.BackColor;
            label3.BackColor = this.BackColor;
            label4.BackColor = this.BackColor;
            label5.BackColor = Color.White;
            label6.BackColor = this.BackColor;
            label7.BackColor = this.BackColor;
            label8.BackColor = this.BackColor;


        }

        private void label7_Click(object sender, EventArgs e)
        {
           escalas1.Hide();
            
            responsavel1.Hide();
            utentes1.Hide();
            funcionario1.Hide();
            atividades1.Hide();
            tarefas1.Hide();
            pagamentos1.Show();
            quartos1.Hide();
            label1.BackColor = this.BackColor;
            label2.BackColor = this.BackColor;
            label3.BackColor = this.BackColor;
            label4.BackColor = this.BackColor;
            label5.BackColor = this.BackColor;
            label6.BackColor = this.BackColor;
            label7.BackColor = Color.White;
            label8.BackColor = this.BackColor;

        }

        private void label8_Click(object sender, EventArgs e)
        {
            escalas1.Hide();
            
            responsavel1.Hide();
            utentes1.Hide();
            funcionario1.Hide();
            atividades1.Hide();
            tarefas1.Hide();
            pagamentos1.Hide();
            quartos1.Show();
            label1.BackColor = this.BackColor;
            label2.BackColor = this.BackColor;
            label3.BackColor = this.BackColor;
            label4.BackColor = this.BackColor;
            label5.BackColor = this.BackColor;
            label6.BackColor = this.BackColor;
            label7.BackColor = this.BackColor;
            label8.BackColor = Color.White;

        }

        private void label6_Click(object sender, EventArgs e)
        {
            escalas1.Show();
            
            responsavel1.Hide();
            utentes1.Hide();
            funcionario1.Hide();
            atividades1.Hide();
            tarefas1.Hide();
            pagamentos1.Hide();
            quartos1.Hide();
            label1.BackColor = this.BackColor;
            label2.BackColor = this.BackColor;
            label3.BackColor = this.BackColor;
            label4.BackColor = this.BackColor;
            label5.BackColor = this.BackColor;
            label6.BackColor = Color.White;
            label7.BackColor = this.BackColor;
            label8.BackColor = this.BackColor;
        }
    }
}
