﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projeto_Lar3idade_Back_End
{
    public partial class utentes : UserControl

    {
        public event EventHandler ButtonClicked;

        public utentes()
        {
            InitializeComponent();
     
        }
        
     

        private void button1_Click(object sender, EventArgs e)
        
        {
            ButtonClicked?.Invoke(this, EventArgs.Empty);
        }
    }
}
