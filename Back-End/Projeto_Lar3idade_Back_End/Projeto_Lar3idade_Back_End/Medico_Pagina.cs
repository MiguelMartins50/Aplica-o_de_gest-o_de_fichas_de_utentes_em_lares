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
    public partial class Medico_Pagina : Form
    {
        private int userid;
        private Medico_consultas medico_Consultas1;
        private Escalas_func escalas_Func1;
        private string tipo_func = "medico";
        private string nome = "";
        private int tipo = 1;
        private notificacao_func notificacao_Func1;
        private enviar_medico enviar_Medico1;
        public Medico_Pagina(int iduser, string usernome)
        {
            InitializeComponent();
            this.userid = iduser;
            this.nome = usernome;
            enviar_Medico1 = new enviar_medico(iduser, usernome);
            enviar_Medico1.Location = new Point(215, 60);
            enviar_Medico1.Size = new Size(1022, 700);
            this.Controls.Add(enviar_Medico1);
            notificacao_Func1 = new notificacao_func(iduser, tipo, usernome);
            notificacao_Func1.Location = new Point(215, 60);
            notificacao_Func1.Size = new Size(1022, 700);
            this.Controls.Add(notificacao_Func1);
            medico_Consultas1 = new Medico_consultas(userid);
            medico_Consultas1.Location = new Point(215, 60);
            medico_Consultas1.Size = new Size(1022, 700);
            this.Controls.Add(medico_Consultas1);
            escalas_Func1 = new Escalas_func(userid,tipo_func);
            escalas_Func1.Location = new Point(215, 60);
            escalas_Func1.Size = new Size(1022, 700);
            this.Controls.Add(escalas_Func1);

            medico_Consultas1.Hide();
            escalas_Func1.Hide();
            notificacao_Func1.Hide();
            enviar_Medico1.Hide();

            escalas_Func1.NavigateToEnviarFuncClicked += EscalasFunc_NavigateToEnviarFuncClicked;
            enviar_Medico1.NavigateToEscalsFuncClicked += EnviarFunc_NavigateToEscalasFuncClicked;
        }


        private void EscalasFunc_NavigateToEnviarFuncClicked(object sender, EventArgs e)
        {
            escalas_Func1.Hide();
            enviar_Medico1.Show();
        }
        private void EnviarFunc_NavigateToEscalasFuncClicked(object sender, EventArgs e)
        {
            escalas_Func1.Show();
            enviar_Medico1.Hide();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            label2.BackColor = Color.White;
            medico_Consultas1.Show();
            medico_Utente1.Hide();
            escalas_Func1.Hide();
            label3.BackColor = this.BackColor;
            label1.BackColor = this.BackColor;
            label4.BackColor = this.BackColor;

            notificacao_Func1.Hide();
            enviar_Medico1.Hide();


        }

        private void label1_Click(object sender, EventArgs e)
        {
            label2.BackColor = this.BackColor;
            medico_Consultas1.Hide();
            medico_Utente1.Show();
            escalas_Func1.Hide();
            label3.BackColor = this.BackColor;
            label1.BackColor = Color.White;
            label4.BackColor = this.BackColor;

            notificacao_Func1.Hide();
            enviar_Medico1.Hide();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Hide();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            label2.BackColor = this.BackColor;
            medico_Consultas1.Hide();
            medico_Utente1.Hide();
            escalas_Func1.Show();
            label3.BackColor = Color.White;
            label1.BackColor = this.BackColor;
            label4.BackColor = this.BackColor;

            notificacao_Func1.Hide();
            enviar_Medico1.Hide();

        }
        private void label4_Click(object sender, EventArgs e)
        {
            label2.BackColor = this.BackColor;
            medico_Consultas1.Hide();
            medico_Utente1.Hide();
            escalas_Func1.Hide();
            label3.BackColor = this.BackColor;
            label1.BackColor = this.BackColor;
            label4.BackColor = Color.White;
            notificacao_Func1.Show();
            enviar_Medico1.Hide();

        }
    }
}
