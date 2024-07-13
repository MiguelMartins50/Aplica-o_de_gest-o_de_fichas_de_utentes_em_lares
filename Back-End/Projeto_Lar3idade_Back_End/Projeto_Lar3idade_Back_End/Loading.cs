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
    public partial class Loading : Form
    {
        private Label label;
        private int dotCount = 0;
        private Timer timer;

        public Loading(Point local)
        {
            InitializeComponent();
            this.ControlBox = false;
            this.Text = "";
            this.BackColor = Color.White;
            this.Size = new Size(200, 100);
            this.StartPosition = FormStartPosition.Manual;
            this.Location = local;

            label = new Label();
            label.Text = "Verificar Credenciais";
            label.AutoSize = false;
            label.Dock = DockStyle.Fill;
            label.TextAlign = ContentAlignment.MiddleCenter;
            this.Controls.Add(label);

            timer = new Timer();
            timer.Interval = 500; // 500ms = 0.5s
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            dotCount++;
            label.Text = "Verificar Credenciais" + new string('.', dotCount);

            if (dotCount >= 3)
            {
                dotCount = 0;
                label.Text = "Verificar Credenciais";
            }
        }
    }
}