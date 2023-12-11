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
            add_utente1.Hide();
            add_funcionario1.Hide();
            add_responsavel1.Hide();
            add_tarefas1.Hide();
            add_quartos1.Hide();
            add_pagamento1.Hide();
            funcionario1.Hide();
            responsavel1.Hide();
            atividades1.Hide();
            add_atividade1.Hide();
            tarefas1.Hide();
            pagamentos1.Hide();
            quartos1.Hide();
            utentes1.ButtonClicked += utenteControl2_ButtonClicked;
            add_utente1.ButtonClicked += utenteControl1_ButtonClicked;
            funcionario1.ButtonClicked += funcControl2_ButtonClicked;
            add_funcionario1.ButtonClicked += funcControl1_ButtonClicked;
            responsavel1.ButtonClicked += respControl2_ButtonClicked;
            add_responsavel1.ButtonClicked += respControl1_ButtonClicked;
            atividades1.ButtonClicked += ativControl2_ButtonClicked;
            add_atividade1.ButtonClicked += ativControl1_ButtonClicked;
            tarefas1.ButtonClicked += taskControl2_ButtonClicked;
            add_tarefas1.ButtonClicked += taskControl1_ButtonClicked;
            quartos1.ButtonClicked += roomControl2_ButtonClicked;
            add_quartos1.ButtonClicked += roomControl1_ButtonClicked;
            pagamentos1.ButtonClicked += payControl2_ButtonClicked;
            add_pagamento1.ButtonClicked += payControl1_ButtonClicked;
        }
        private void utenteControl2_ButtonClicked(object sender, EventArgs e)
        {
            // Hide UserControl1 when the button is clicked in UserControl2
            add_utente1.Show();
            utentes1.Hide();
            add_funcionario1.Hide();
            add_responsavel1.Hide();
            add_tarefas1.Hide();
            add_quartos1.Hide();
            add_pagamento1.Hide();
            funcionario1.Hide();
            responsavel1.Hide();
            atividades1.Hide();
            add_atividade1.Hide();
            tarefas1.Hide();
            pagamentos1.Hide();
            quartos1.Hide();
        }
        private void utenteControl1_ButtonClicked(object sender, EventArgs e)
        {
            // Hide UserControl1 when the button is clicked in UserControl2
            add_utente1.Hide();
            utentes1.Show(); 
            add_funcionario1.Hide();
            add_responsavel1.Hide();
            add_tarefas1.Hide();
            add_quartos1.Hide();
            add_pagamento1.Hide();
            funcionario1.Hide();
            responsavel1.Hide();
            atividades1.Hide();
            add_atividade1.Hide();
            tarefas1.Hide();
            pagamentos1.Hide();
            quartos1.Hide();
        }
        private void funcControl2_ButtonClicked(object sender, EventArgs e)
        {
            // Hide UserControl1 when the button is clicked in UserControl2
            add_funcionario1.Show();
            funcionario1.Hide();
            add_utente1.Hide();
            add_responsavel1.Hide();
            add_tarefas1.Hide();
            add_quartos1.Hide();
            add_pagamento1.Hide();
            responsavel1.Hide();
            atividades1.Hide();
            add_atividade1.Hide();
            tarefas1.Hide();
            pagamentos1.Hide();
            quartos1.Hide();
        }
        private void funcControl1_ButtonClicked(object sender, EventArgs e)
        {
            // Hide UserControl1 when the button is clicked in UserControl2
            add_funcionario1.Hide();
            funcionario1.Show();
            add_utente1.Hide();
          
            add_responsavel1.Hide();
            add_tarefas1.Hide();
            add_quartos1.Hide();
            add_pagamento1.Hide();
            responsavel1.Hide();
            atividades1.Hide();
            add_atividade1.Hide();
            tarefas1.Hide();
            pagamentos1.Hide();
            quartos1.Hide();
        }
        private void respControl2_ButtonClicked(object sender, EventArgs e)
        {
            // Hide UserControl1 when the button is clicked in UserControl2
            add_responsavel1.Show();
            responsavel1.Hide();
            add_utente1.Hide();
            add_funcionario1.Hide();
            add_tarefas1.Hide();
            add_quartos1.Hide();
            add_pagamento1.Hide();
            funcionario1.Hide();
            atividades1.Hide();
            add_atividade1.Hide();
            tarefas1.Hide();
            pagamentos1.Hide();
            quartos1.Hide();
        }
        private void respControl1_ButtonClicked(object sender, EventArgs e)
        {
            // Hide UserControl1 when the button is clicked in UserControl2
            add_responsavel1.Hide();
            responsavel1.Show();
            add_utente1.Hide();
            add_funcionario1.Hide();
            add_tarefas1.Hide();
            add_quartos1.Hide();
            add_pagamento1.Hide();
            funcionario1.Hide();
            atividades1.Hide();
            add_atividade1.Hide();
            tarefas1.Hide();
            pagamentos1.Hide();
            quartos1.Hide();
        }
        private void ativControl2_ButtonClicked(object sender, EventArgs e)
        {
            // Hide UserControl1 when the button is clicked in UserControl2
            add_atividade1.Show();
            atividades1.Hide();
            add_utente1.Hide();
            add_funcionario1.Hide();
            add_responsavel1.Hide();
            add_tarefas1.Hide();
            add_quartos1.Hide();
            add_pagamento1.Hide();
            funcionario1.Hide();
            responsavel1.Hide();
            tarefas1.Hide();
            pagamentos1.Hide();
            quartos1.Hide();
        }
        private void ativControl1_ButtonClicked(object sender, EventArgs e)
        {
            // Hide UserControl1 when the button is clicked in UserControl2
            add_atividade1.Hide();
            atividades1.Show();
            add_utente1.Hide();
            add_funcionario1.Hide();
            add_responsavel1.Hide();
            add_tarefas1.Hide();
            add_quartos1.Hide();
            add_pagamento1.Hide();
            funcionario1.Hide();
            responsavel1.Hide();
            tarefas1.Hide();
            pagamentos1.Hide();
            quartos1.Hide();
        }
        private void taskControl2_ButtonClicked(object sender, EventArgs e)
        {
            // Hide UserControl1 when the button is clicked in UserControl2
            add_tarefas1.Show();
            tarefas1.Hide();
            add_utente1.Hide();
            add_funcionario1.Hide();
            add_responsavel1.Hide();
            add_quartos1.Hide();
            add_pagamento1.Hide();
            funcionario1.Hide();
            responsavel1.Hide();
            atividades1.Hide();
            add_atividade1.Hide();
            pagamentos1.Hide();
            quartos1.Hide();
        }
        private void taskControl1_ButtonClicked(object sender, EventArgs e)
        {
            // Hide UserControl1 when the button is clicked in UserControl2
            add_tarefas1.Hide();
            tarefas1.Show();
            add_utente1.Hide();
            add_funcionario1.Hide();
            add_responsavel1.Hide();
            add_quartos1.Hide();
            add_pagamento1.Hide();
            funcionario1.Hide();
            responsavel1.Hide();
            atividades1.Hide();
            add_atividade1.Hide();
            pagamentos1.Hide();
            quartos1.Hide();
        }
        private void roomControl2_ButtonClicked(object sender, EventArgs e)
        {
            // Hide UserControl1 when the button is clicked in UserControl2
            add_quartos1.Show();
            quartos1.Hide();
            add_utente1.Hide();
            add_funcionario1.Hide();
            add_responsavel1.Hide();
            add_tarefas1.Hide();
            add_pagamento1.Hide();
            funcionario1.Hide();
            responsavel1.Hide();
            atividades1.Hide();
            add_atividade1.Hide();
            tarefas1.Hide();
            pagamentos1.Hide();
        }
        private void roomControl1_ButtonClicked(object sender, EventArgs e)
        {
            // Hide UserControl1 when the button is clicked in UserControl2
            add_quartos1.Hide();
            quartos1.Show(); 
            add_utente1.Hide();
            add_funcionario1.Hide();
            add_responsavel1.Hide();
            add_tarefas1.Hide();
            add_pagamento1.Hide();
            funcionario1.Hide();
            responsavel1.Hide();
            atividades1.Hide();
            add_atividade1.Hide();
            tarefas1.Hide();
            pagamentos1.Hide();
        }
        private void payControl2_ButtonClicked(object sender, EventArgs e)
        {
            // Hide UserControl1 when the button is clicked in UserControl2
            add_pagamento1.Show();
            pagamentos1.Hide();
            add_utente1.Hide();
            add_funcionario1.Hide();
            add_responsavel1.Hide();
            add_tarefas1.Hide();
            add_quartos1.Hide();
            funcionario1.Hide();
            responsavel1.Hide();
            atividades1.Hide();
            add_atividade1.Hide();
            tarefas1.Hide();
            pagamentos1.Hide();
        }
        private void payControl1_ButtonClicked(object sender, EventArgs e)
        {
            // Hide UserControl1 when the button is clicked in UserControl2
            add_pagamento1.Hide();
            pagamentos1.Show();
            add_utente1.Hide();
            add_funcionario1.Hide();
            add_responsavel1.Hide();
            add_tarefas1.Hide();
            add_quartos1.Hide();
            funcionario1.Hide();
            responsavel1.Hide();
            atividades1.Hide();
            add_atividade1.Hide();
            tarefas1.Hide();
            quartos1.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            add_utente1.Hide();
            add_funcionario1.Hide();
            add_responsavel1.Hide();
            add_tarefas1.Hide();
            add_quartos1.Hide();
            add_pagamento1.Hide();
            add_atividade1.Hide();
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
            add_utente1.Hide();
            add_funcionario1.Hide();
            add_responsavel1.Hide();
            add_tarefas1.Hide();
            add_quartos1.Hide();
            add_pagamento1.Hide();
            add_atividade1.Hide();
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
            add_utente1.Hide();
            add_funcionario1.Hide();
            add_responsavel1.Hide();
            add_tarefas1.Hide();
            add_quartos1.Hide();
            add_pagamento1.Hide();
            add_atividade1.Hide();
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
            add_utente1.Hide();
            add_funcionario1.Hide();
            add_responsavel1.Hide();
            add_tarefas1.Hide();
            add_quartos1.Hide();
            add_pagamento1.Hide();
            add_atividade1.Hide();
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
            add_utente1.Hide();
            add_funcionario1.Hide();
            add_responsavel1.Hide();
            add_tarefas1.Hide();
            add_quartos1.Hide();
            add_pagamento1.Hide();
            add_atividade1.Hide();
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
            add_utente1.Hide();
            add_funcionario1.Hide();
            add_responsavel1.Hide();
            add_tarefas1.Hide();
            add_quartos1.Hide();
            add_pagamento1.Hide();
            add_atividade1.Hide();
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
            add_utente1.Hide();
            add_funcionario1.Hide();
            add_responsavel1.Hide();
            add_tarefas1.Hide();
            add_quartos1.Hide();
            add_pagamento1.Hide();
            add_atividade1.Hide();
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
    }
}
