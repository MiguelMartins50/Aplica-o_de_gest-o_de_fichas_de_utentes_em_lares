using System;
using System.Drawing;
using System.Windows.Forms;

namespace Projeto_Lar3idade_Back_End
{
    public partial class Mensagem : Form
    {
        private Label label1; 
        private Button buttonOk = new Button();

        public Mensagem(string message, Point location)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;
            this.Location = location;
            this.Size = new Size(250, 110);
            this.Text = "";
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            label1 = new Label();
            label1.Text = message;
            label1.AutoSize = true;
            label1.Location = new Point(10, 10); 

            this.Controls.Add(label1);

            buttonOk.Text = "OK";
            buttonOk.Location = new Point(150, 40); 
            buttonOk.Click += new EventHandler(buttonOk_Click);

            this.Controls.Add(buttonOk);
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
