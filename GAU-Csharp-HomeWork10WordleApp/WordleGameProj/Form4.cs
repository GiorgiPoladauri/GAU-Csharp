using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WordleGameProj
{
    public partial class Form4 : Form
    {
        public Form4(string email)
        {
            InitializeComponent();
            label2.Text = email;
        }
    }
}
