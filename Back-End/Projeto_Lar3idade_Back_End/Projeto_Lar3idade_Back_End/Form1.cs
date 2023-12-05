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
    public partial class Form1 : Form
    {
       
        public Form1()
        {
            InitializeComponent();
            string BluePath = System.IO.Path.Combine(Application.StartupPath, "azul2.png");
            panel1.BackgroundImage = Image.FromFile(BluePath);
            string logoPath = System.IO.Path.Combine(Application.StartupPath, "5857e68f1800001c00e435b3.jpeg");
            panel2.BackgroundImage = Image.FromFile(logoPath);
            panel1.BackgroundImageLayout = ImageLayout.Stretch;
            panel2.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
