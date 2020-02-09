﻿using System;
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
        Form1 form1 = new Form1();
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
            form1.ShowDialog();
            this.Show();
        }

        private void PictureBox5_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}