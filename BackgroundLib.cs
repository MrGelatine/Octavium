using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp3
{
    public partial class BackgroundLib : Form
    {
        Color backColor1 = Color.FromArgb(46, 46, 46);
        Color backColor2 = Color.FromArgb(69, 69, 69);

        List<string> backList = new List<string>();
        string selectedImg = null;
        string projectPath = Path.GetFullPath(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, @"..\..\"));
        string backLibPath;

        public BackgroundLib()
        {
            InitializeComponent();
            backLibPath = string.Format("{0}Resources\\Backgrounds", projectPath);
            this.BackColor = backColor1;
            BackGroundListPanel.BackColor = backColor2;
            GetBacksFromLib();
            ShowBacks();
        }

        private void GetBacksFromLib()
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(backLibPath);
            foreach (var file in directoryInfo.GetFiles()) 
            {
                if (Path.GetExtension(file.FullName) == ".jpg" || Path.GetExtension(file.FullName) == ".png")
                    backList.Add(file.FullName);
            }
        }

        private void ShowBacks()
        {
            BackGroundListPanel.Controls.Clear();
            int i = 0;
            foreach (string img in backList)
            {
                createBackPanel(i, img);
                i++;
            }
        }

        void createBackPanel(int i, string img, int w = 230, int h = 150)
        {
            Panel panel = new Panel();
            panel.Name = i.ToString() + "backPanel";
            panel.Size = new Size(w, h);
            panel.Location = new Point(0, 0);
            panel.BackColor = Color.White;
            panel.Margin = new Padding(5);

            panel.BackgroundImageLayout = ImageLayout.Stretch;
            panel.BackgroundImage = Image.FromFile(img);

            panel.Click += new EventHandler(this.BackPanel_Click);
            panel.Cursor = Cursors.Hand;
            BackGroundListPanel.Controls.Add(panel);
        }

        void BackPanel_Click(Object sender,
                           EventArgs e)
        {
            Panel senderPanel = (Panel)sender;
            string indexString = senderPanel.Name.Substring(0, senderPanel.Name.Length - 9);
            try
            {
                int i = Int32.Parse(indexString);
                selectedImg = backList[i];
                this.Close();
            }
            catch (FormatException expt)
            {
                Console.WriteLine(expt.Message);
            }
        }

        private void BackgroundLib_FormClosing(object sender, FormClosingEventArgs e)
        {
            SettingsForm sform = (SettingsForm)this.Owner;
            sform.backgroundPath = selectedImg;
        }
    }
}
