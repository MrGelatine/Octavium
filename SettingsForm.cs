using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace WindowsFormsApp3
{
    public partial class SettingsForm : Form
    {
        public string backgroundPath = null;
        Color colorNote1 = Color.Red;
        Color colorNote2 = Color.Pink;
        int volume = 100;
        int speed = 100;
        Color backColor1 = Color.FromArgb(46, 46, 46);
        Color backColor2 = Color.FromArgb(69, 69, 69);
        public SettingsForm()
        {
            InitializeComponent();
            flowLayoutPanel1.BackColor = backColor1;
            this.BackColor = backColor2;
            colorPanel1.BackColor = Color.Red;
            colorPanel2.BackColor = Color.Pink;
        }

        private void PanelBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ColorPanel1_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            colorPanel1.BackColor = colorNote1 = colorDialog1.Color;
        }

        private void ColorPanel2_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            colorPanel2.BackColor = colorNote2 = colorDialog1.Color;
        }

        private void Panel1_Click_1(object sender, EventArgs e)
        {
            BackgroundLib backForm = new BackgroundLib();
            AddOwnedForm(backForm);
            backForm.ShowDialog();

            /*
            using (var dialog = new OpenFileDialog())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    if (!Is_jpg(dialog.FileName))
                    {
                        throw new FormatException("Выбранный файл не является jpg!");
                    }
                    backgroundPath = dialog.FileName;
                }
                else
                {
                    backgroundPath = null;
                }
            }*/
        }

        private bool Is_jpg(string s)
        {
            return s.Contains(".jpg");
        }

        private string GetFileName(string path)
        {
            var res = Regex.Match(path, @"[\\]+[^\\]+.jpg").Value.Remove(0, 1);
            return res.Remove(res.Length - 4, 4);
        }

        private void VolumeBar_Scroll(object sender, EventArgs e)
        {
            volume = volumeBar.Value;
            volumeLabel.Text = volume.ToString() + "%";
        }

        private void SettingsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            MenuForm fmenu = (MenuForm)this.Owner;
            if (backgroundPath != null)
                fmenu.image = backgroundPath;
            fmenu.clNote1 = colorNote1;
            fmenu.clNote2 = colorNote2;
            fmenu.volume = volume;
            fmenu.speed = speed / 100;
  
        }

        private void SpeedBar_Scroll(object sender, EventArgs e)
        {
            speed = speedBar.Value;
            speedLabel.Text = speed.ToString() + "%";
        }
    }
}
