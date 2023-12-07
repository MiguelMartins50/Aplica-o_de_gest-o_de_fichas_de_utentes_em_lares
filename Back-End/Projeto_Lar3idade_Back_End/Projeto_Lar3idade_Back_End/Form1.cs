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
        private Rectangle logoOriginalRectangle;
        private Rectangle blueOriginalRectangle;
        private Rectangle logoinOriginalRectangle;
        private Rectangle Originalformsize;
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
            this.Hide();
            admin_utentes admin = new admin_utentes();
            admin.ShowDialog();
            this.Close();        
        }
       

        private void Form1_Load(object sender, EventArgs e)
        {
            Originalformsize = new Rectangle(this.Location.X, this.Location.Y, this.Width, this.Height);
            logoinOriginalRectangle = new Rectangle(button1.Location.X, button1.Location.Y, button1.Width, button1.Height);
            blueOriginalRectangle = new Rectangle(panel1.Location.X, panel1.Location.Y, panel1.Width, panel1.Height);
            logoOriginalRectangle = new Rectangle(panel2.Location.X, panel2.Location.Y, panel2.Width, panel2.Height);
        }

        private void Form1_Resize(object sender, EventArgs e)
        {

        }
    }

}
