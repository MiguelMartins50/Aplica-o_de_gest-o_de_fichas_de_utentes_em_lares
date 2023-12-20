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

        public Medico_Pagina(int iduser)
        {
            InitializeComponent();
            this.userid = iduser;
            medico_Consultas1= new Medico_consultas(userid);
            medico_Consultas1.Location = new Point(215, 60);
            medico_Consultas1.Size = new Size(1022, 700);
            this.Controls.Add(medico_Consultas1);
            medico_Consultas1.Hide();
        }


        private void label2_Click(object sender, EventArgs e)
        {
            label2.BackColor = Color.White;
            medico_Consultas1.Show();
            medico_Utente1.Hide();
            label1.BackColor = this.BackColor;

        }

        private void label1_Click(object sender, EventArgs e)
        {
            label2.BackColor = this.BackColor;
            medico_Consultas1.Hide();
            medico_Utente1.Show();
            label1.BackColor = Color.White;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Hide();
        }
    }
}
