using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp3
{
    public partial class MenuForm : Form
    {
        public string filepath = "alla-turca.mid";
        public MenuForm()
        {
            InitializeComponent();
        }

        private void MenuForm_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(71, 71, 71);
        }

        private void BeginPictureBox_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 form1 = new Form1(filepath);
            form1.ShowDialog();
            this.Show();
        }

        private void PictureBox5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void PictureBox3_Click(object sender, EventArgs e)
        {
            this.Hide();
            LibForm libForm = new LibForm();
            AddOwnedForm(libForm);
            libForm.ShowDialog();
            this.Show();
        }
    }
}
