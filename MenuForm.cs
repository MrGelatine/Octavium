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
        public bool starting = false;
        public string image = null;
        public Color clNote1 = Color.Red;
        public Color clNote2 = Color.Pink;
        public int volume = 100;
        public int speed = 1;

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
            filepath = "";
            this.Hide();
            LibForm libForm = new LibForm(false);
            AddOwnedForm(libForm);
            libForm.ShowDialog();
            if (starting && filepath != "")
                InterfaceFuncs.Base_Library_Call(filepath, speed, image, clNote1, clNote2, volume);
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

        private void PictureBox2_Click(object sender, EventArgs e)
        {
            this.Hide();
            SettingsForm setForm = new SettingsForm();
            AddOwnedForm(setForm);
            setForm.ShowDialog();
            this.Show();
        }

        private void MenuForm_KeyDown(object sender, KeyEventArgs e)
        {
        }
    }
}
