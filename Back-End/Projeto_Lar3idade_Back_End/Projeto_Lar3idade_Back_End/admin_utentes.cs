﻿using System;
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
    public partial class admin_utentes : Form
    {
      
        public admin_utentes()
        {
            InitializeComponent();

            string logoPath = System.IO.Path.Combine(Application.StartupPath, "5857e68f1800001c00e435b3.jpeg");
            pictureBox1.BackgroundImage = Image.FromFile(logoPath);
            pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;
            funcionarios1.Hide();
            responsavies1.Hide();
            quartos1.Hide();
            pagamentos1.Hide();
            add_Utente1.Hide();
            add_Funcionario1.Hide();
            addResponsavel1.Hide();
            utentes1.ButtonClicked += UserControl2_ButtonClicked;
            add_Utente1.ButtonClicked += UserControl1_ButtonClicked;
            funcionarios1.ButtonClicked += add_func2_ButtonClicked;
            add_Funcionario1.ButtonClicked += add_func1_ButtonClicked;
            responsavies1.ButtonClicked += add_responsavel2_ButtonClicked;
            addResponsavel1.ButtonClicked += add_responsavel1_ButtonClicked;

        }
        private void UserControl2_ButtonClicked(object sender, EventArgs e)
        {
            // Hide UserControl1 when the button is clicked in UserControl2
            add_Utente1.Show();
            utentes1.Hide();
        }
        private void UserControl1_ButtonClicked(object sender, EventArgs e)
        {
            // Hide UserControl1 when the button is clicked in UserControl2
            add_Utente1.Hide();
            utentes1.Show();
        }
        private void add_responsavel2_ButtonClicked(object sender, EventArgs e)
        {
            // Hide UserControl1 when the button is clicked in UserControl2
            addResponsavel1.Show();
            responsavies1.Hide();
        }
        private void add_responsavel1_ButtonClicked(object sender, EventArgs e)
        {
            // Hide UserControl1 when the button is clicked in UserControl2
            addResponsavel1.Hide();
            responsavies1.Show();
        }
        private void add_func2_ButtonClicked(object sender, EventArgs e)
        {
            // Hide UserControl1 when the button is clicked in UserControl2
            add_Funcionario1.Show();
            funcionarios1.Hide();
        }
        private void add_func1_ButtonClicked(object sender, EventArgs e)
        {
            // Hide UserControl1 when the button is clicked in UserControl2
            add_Funcionario1.Hide();
            funcionarios1.Show();
        }



        private void label3_Click(object sender, EventArgs e)
        {
            funcionarios1.Hide();
            responsavies1.Hide();
            quartos1.Hide();
            pagamentos1.Hide();
            utentes1.Show();
            label3.BackColor = Color.White;
            label4.BackColor = this.BackColor;
            label5.BackColor = this.BackColor;
            label9.BackColor = this.BackColor;
            label10.BackColor = this.BackColor;
        }

        private void label4_Click(object sender, EventArgs e)
        {
            funcionarios1.Show();
            responsavies1.Hide();
            quartos1.Hide();
            pagamentos1.Hide();
            utentes1.Show();
            label3.BackColor = this.BackColor;
            label4.BackColor = Color.White;
            label5.BackColor = this.BackColor;
            label9.BackColor = this.BackColor;
            label10.BackColor = this.BackColor;
        }

        private void label5_Click(object sender, EventArgs e)
        {
            funcionarios1.Hide();
            responsavies1.Show();
            quartos1.Hide();
            pagamentos1.Hide();
            utentes1.Show();
            label3.BackColor = this.BackColor;
            label4.BackColor = this.BackColor;
            label5.BackColor = Color.White;
            label9.BackColor = this.BackColor;
            label10.BackColor = this.BackColor;
        }

        private void label6_Click(object sender, EventArgs e)
        {
            funcionarios1.Hide();
            responsavies1.Hide();
            quartos1.Hide();
            pagamentos1.Hide();
            utentes1.Show();
            label3.BackColor = this.BackColor;
            label4.BackColor = Color.White;
            label5.BackColor = this.BackColor;
            label9.BackColor = this.BackColor;
            label10.BackColor = this.BackColor;
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }
        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {
            funcionarios1.Hide();
            responsavies1.Hide();
            quartos1.Hide();
            pagamentos1.Show();
            utentes1.Hide();
            label3.BackColor = this.BackColor;
            label4.BackColor = this.BackColor;
            label5.BackColor = this.BackColor;
            label9.BackColor = Color.White;
            label10.BackColor = this.BackColor;
        }
       
        private void label10_Click(object sender, EventArgs e)
        {
            funcionarios1.Hide();
            responsavies1.Hide();
            quartos1.Show();
            pagamentos1.Hide();
            utentes1.Hide();
            label3.BackColor = this.BackColor;
            label4.BackColor = this.BackColor;
            label5.BackColor = this.BackColor;
            label4.BackColor = this.BackColor;
            label9.BackColor = this.BackColor;
            label10.BackColor = Color.White;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 login = new Form1();
            login.ShowDialog();
            this.Close();
        }
    }
}
