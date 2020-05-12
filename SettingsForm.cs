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
        int speed = 100;
        Color backColor1 = Color.FromArgb(46, 46, 46);
        Color backColor2 = Color.FromArgb(69, 69, 69);
        string projectPath = Path.GetFullPath(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, @"..\..\"));
        string setPath;
        public SettingsForm()
        {
            InitializeComponent();
            flowLayoutPanel1.BackColor = backColor1;
            this.BackColor = backColor2;
            setPath = projectPath + @"\set.txt";
            try
            {
                GetSettingsFromTxt();
            }
            catch
            {

            }
            colorPanel1.BackColor = colorNote1;
            colorPanel2.BackColor = colorNote2;
            speedBar.Value = speed;
        }
        private void GetSettingsFromTxt()
        {
            string[] settingsTxt = System.IO.File.ReadAllLines(setPath);
            for (int i = 0; i < settingsTxt.Length; i++)
            {
                string[] setAttr = settingsTxt[i].Split('=');
                if (setAttr.Length < 2)
                    continue;
                switch (setAttr[0])
                {
                    case "cl1":
                        colorNote1 = GetColorFromStr(setAttr[1]);
                        break;
                    case "cl2":
                        colorNote2 = GetColorFromStr(setAttr[1]);
                        break;
                    case "backgroundPath":
                        backgroundPath = setAttr[1];
                        break;
                    case "speed":
                        Int32.TryParse(setAttr[1],out speed);
                        break;
                    default:
                        break;
                }
            }
        }

        private void SaveSettingsToTxt()
        {
            using (var setW = new StreamWriter(setPath))
            {
                setW.WriteLine("cl1=" + colorNote1.R.ToString() + "," + colorNote1.G.ToString() + "," + colorNote1.B.ToString());
                setW.WriteLine("cl2=" + colorNote2.R.ToString() + "," + colorNote2.G.ToString() + "," + colorNote2.B.ToString());
                setW.WriteLine("backgroundPath=" + backgroundPath);
                setW.WriteLine("speed=" + speed.ToString());
            }
        }

        private Color GetColorFromStr(string stringColor)
        {
            string[] colAttr = stringColor.Split(',');
            int r, g, b;
            Int32.TryParse(colAttr[0], out r);
            Int32.TryParse(colAttr[1], out g);
            Int32.TryParse(colAttr[2], out b);
            return Color.FromArgb(r, g, b);
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


        private void SettingsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveSettingsToTxt();
            MenuForm fmenu = (MenuForm)this.Owner;
            if (backgroundPath != null && backgroundPath != "")
                fmenu.image = backgroundPath;
            fmenu.clNote1 = colorNote1;
            fmenu.clNote2 = colorNote2;
            fmenu.speed = (double) speed / 100;
  
        }

        private void SpeedBar_Scroll(object sender, EventArgs e)
        {
            speed = speedBar.Value;
            speedLabel.Text = speed.ToString() + "%";
        }
    }
}
