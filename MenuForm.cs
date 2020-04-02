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
        //путь к выбранному треку
        public string filepath = "";

        public MenuForm()
        {
            InitializeComponent();
        }

        private void MenuForm_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(71, 71, 71);
        }
        
        //Открываем форму с пианино, передав ей путь к треку как параметр
        private void BeginPictureBox_Click(object sender, EventArgs e)
        {
            if (filepath == "")
            {
                MessageBox.Show("Необходимо выбрать трек из галлереи");
                return;
            }
            this.Hide();
            InterfaceFuncs.Base_Library_Call(filepath);
            this.Show();
        }

        //Закрытие формы меню
        private void PictureBox5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //Открываем форму галлереи
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
