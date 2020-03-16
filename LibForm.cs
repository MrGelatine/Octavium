using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Melanchall.DryWetMidi.Interaction;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.MusicTheory;
using Melanchall.DryWetMidi.Tools;
using Melanchall.DryWetMidi;

namespace WindowsFormsApp3
{
    public partial class LibForm : Form
    {
        List<Tuple<int, string, string, string>> libfileList = new List<Tuple<int, string, string, string>>();
        List<Tuple<int, string, string, string>> curfiles = new List<Tuple<int, string, string, string>>();
        int selectedFileIndex = -1;
        string selectedFileName = "";
        Color backColor1 = Color.FromArgb(46, 46, 46);
        Color backColor2 = Color.FromArgb(69, 69, 69);
        string projectPath = Path.GetFullPath(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, @"..\..\"));
        string libPath;
        public LibForm()
        {
            libPath = string.Format("{0}Resources\\DataStorage", projectPath);
            InitializeComponent();
        }

        private void LibForm_Load(object sender, EventArgs e)
        {
            flowLayoutPanel1.BackColor = backColor1;
            this.BackColor = backColor2;
            searchTextBox.BackColor = backColor1;
            flowLayoutPanel2.BackColor = backColor2;
            flowLayoutPanel3.BackColor = backColor1;
            GetTracksFromLib();
            ShowTracks();
        }

        private void ShowTracks()
        {
            flowLayoutPanel3.Controls.Clear();
            selectedFileIndex = -1;
            ShowTrack(-1, "Дата", "Название", "Время", false);
            foreach (Tuple<int, string, string, string> fileInfo in curfiles) 
            {
                ShowTrack(fileInfo.Item1, fileInfo.Item2, fileInfo.Item3, fileInfo.Item4);
            }
        }

        private void GetTracksFromLib()
        {
            string[] filestxt = System.IO.File.ReadAllLines(libPath + @"\lib.txt");
            for(int i = 0; i < filestxt.Length; i++)
            {
                string[] fileAttrs = filestxt[i].Split('|');
                string fileName = fileAttrs[0];
                Tuple<int, string, string, string> fileInfo = new Tuple<int, string, string, string>(i, fileAttrs[0], fileAttrs[1], fileAttrs[2]);
                libfileList.Add(fileInfo);
                curfiles.Add(fileInfo);
            }

        }
        private void ShowTrack(int i, string date, string name, string time, bool activeHandler = true)
        {
            
            Panel pdate = new Panel();
            pdate.Name = i.ToString() + "date";
            pdate.Size = new Size(200, 43);
            pdate.Location = new Point(0, 0);
            pdate.BackColor = Color.White;
            pdate.Margin = new Padding(5);
            if (activeHandler)
            {
                pdate.Click += new EventHandler(this.TrackPanel_Click);
                pdate.Cursor = Cursors.Hand;
            }
            flowLayoutPanel3.Controls.Add(pdate);
            Label ldate = new Label();
            ldate.AutoSize = true;
            ldate.Font = new Font(FontFamily.GenericSansSerif, 14);
            ldate.Name = i.ToString() + "ldate";
            ldate.Text = date;
            ldate.Location = new Point(25, 9);
            ldate.Margin = new Padding(25);
            if (activeHandler)
            {
                ldate.Click += new EventHandler(this.TrackLabel_Click);
                ldate.Cursor = Cursors.Hand;
            }
            pdate.Controls.Add(ldate);


            Panel pname = new Panel();
            pname.Name = i.ToString() + "name";
            pname.Size = new Size(589, 43);
            pname.Location = new Point(0, 0);
            pname.BackColor = Color.White;
            pname.Margin = new Padding(5);
            if (activeHandler)
            {
                pname.Click += new EventHandler(this.TrackPanel_Click);
                pname.Cursor = Cursors.Hand;
            }
            flowLayoutPanel3.Controls.Add(pname);
            Label lname = new Label();
            lname.AutoSize = true;
            lname.Font = new Font(FontFamily.GenericSansSerif, 14);
            lname.Name = i.ToString() + "lname";
            lname.Text = name;
            lname.Location = new Point(25, 9);
            lname.Margin = new Padding(25);
            if (activeHandler)
            {
                lname.Click += new EventHandler(this.TrackLabel_Click);
                lname.Cursor = Cursors.Hand;
            }
            pname.Controls.Add(lname);

            Panel ptime = new Panel();
            ptime.Name = i.ToString() + "time";
            ptime.Size = new Size(198, 43);
            ptime.Location = new Point(0, 0);
            ptime.BackColor = Color.White;
            ptime.Margin = new Padding(5);
            if (activeHandler)
            {
                ptime.Click += new EventHandler(this.TrackPanel_Click);
                ptime.Cursor = Cursors.Hand;
            }
            flowLayoutPanel3.Controls.Add(ptime);
            Label ltime = new Label();
            ltime.AutoSize = true;
            ltime.Font = new Font(FontFamily.GenericSansSerif, 14);
            ltime.Name = i.ToString() + "ltime";
            ltime.Text = time;
            ltime.Location = new Point(25, 9);
            ltime.Margin = new Padding(25);
            if (activeHandler)
            {
                ltime.Click += new EventHandler(this.TrackLabel_Click);
                ltime.Cursor = Cursors.Hand;
            }
            ptime.Controls.Add(ltime);

        }

        void TrackPanel_Click(Object sender,
                           EventArgs e)
        {
            Panel senderPanel = (Panel)sender;
            int i  = (int)senderPanel.Name[0] - (int)'0';
            selectTrack(i);
        }

        void TrackLabel_Click(Object sender,
                           EventArgs e)
        {
            Label senderLabel = (Label)sender;
            int i = (int)senderLabel.Name[0] - (int)'0';
            selectTrack(i);
        }
        private void BackPictureBox_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LibForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            MenuForm fmenu = (MenuForm)this.Owner;
            if (selectedFileName != "")
                fmenu.filepath = libPath + @"\" + selectedFileName + ".dat" ;

        }

        private void Panel4_Click(object sender, EventArgs e)
        {
            Tuple<string, string,string> fileData = InterfaceFuncs.GetAndAddData(projectPath + @"\\sheet.exe", projectPath + @"\\Gallery\\Sheets", libPath);
            foreach (Tuple<int, string, string, string> libFileInfo in libfileList)
            {
                if (libFileInfo.Item3.ToLower().Contains(fileData.Item2.ToLower()))
                {
                    MessageBox.Show("Файл с таким названием уже есть");
                    return;
                }
            }
            AddtxtLib(fileData);
            Tuple<int, string, string, string> fileInfo = new Tuple<int, string, string, string>(libfileList.Count, fileData.Item1, fileData.Item2, fileData.Item3);
            libfileList.Add(fileInfo);
            curfiles.Add(fileInfo);
            ShowTrack(fileInfo.Item1, fileInfo.Item2, fileInfo.Item3, fileInfo.Item4);
        }

        private void AddtxtLib(Tuple<string, string, string> fileData)
        {
            string fileInfoText = fileData.Item1 + '|' + fileData.Item2 + '|' + fileData.Item3 + Environment.NewLine;
            System.IO.File.AppendAllText(libPath + @"\lib.txt", fileInfoText);
        }

        private void SearchTextBox_TextChanged(object sender, EventArgs e)
        {
            curfiles.Clear();
            if (searchTextBox.Text == "")
            {
                foreach (Tuple<int, string, string, string> fileInfo in libfileList)
                    curfiles.Add(fileInfo);
            }
            else
            {
                foreach (Tuple<int, string, string, string> fileInfo in libfileList)
                {
                    if (fileInfo.Item3.ToLower().Contains(searchTextBox.Text.ToLower()))
                        curfiles.Add(fileInfo);
                }
            }
            ShowTracks();

        }

        private void selectTrack(int i)
        {
            Panel prevTrackTime = (Panel)this.Controls.Find(selectedFileIndex.ToString() + "time", true).First();
            Panel prevTrackCreation = (Panel)this.Controls.Find(selectedFileIndex.ToString() + "date", true).First();
            Panel prevTrackName = (Panel)this.Controls.Find(selectedFileIndex.ToString() + "name", true).First();
            prevTrackTime.BackColor = Color.White;
            prevTrackTime.ForeColor = Color.Black;
            prevTrackCreation.BackColor = Color.White;
            prevTrackCreation.ForeColor = Color.Black;
            prevTrackName.BackColor = Color.White;
            prevTrackName.ForeColor = Color.Black;
            selectedFileIndex = i;
            selectedFileName = libfileList[selectedFileIndex].Item3;
            labelSongName.Text = Path.GetFileName(selectedFileName);
            Panel trackTime = (Panel)this.Controls.Find(i.ToString() + "time", true).First();
            Panel TrackCreation = (Panel)this.Controls.Find(i.ToString() + "date", true).First();
            Panel TrackName = (Panel)this.Controls.Find(i.ToString() + "name", true).First();
            TrackName.Select();
            searchTextBox.Select();
            trackTime.BackColor = backColor2;
            trackTime.ForeColor = Color.White;
            TrackName.BackColor = backColor2;
            TrackName.ForeColor = Color.White;
            TrackCreation.BackColor = backColor2;
            TrackCreation.ForeColor = Color.White;
        }

    }
}
