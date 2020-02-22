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
        List<Tuple<int, string, string>> fileList = new List<Tuple<int, string, string>>();
        int selectedFileIndex = 0;
        string selectedFilePath = "";
        Color backColor1 = Color.FromArgb(46, 46, 46);
        Color backColor2 = Color.FromArgb(69, 69, 69);
        string libPath = string.Format("{0}Resources", Path.GetFullPath(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, @"..\..\")));
        public LibForm()
        {
            InitializeComponent();
        }

        private void LibForm_Load(object sender, EventArgs e)
        {
            flowLayoutPanel1.BackColor = backColor1;
            this.BackColor = backColor2;
            searchTextBox.BackColor = backColor1;
            flowLayoutPanel2.BackColor = backColor2;
            flowLayoutPanel3.BackColor = backColor1;
            string [] files = Directory.GetFiles(libPath, "*.mid");
            for(int i = 0; i < files.Length; i++)
            {
                TimeSpan timespan = MidiFile.Read(files[i]).GetDuration<MetricTimeSpan>();
                string time = string.Format("{0}:{1:00}", (int)timespan.TotalMinutes, timespan.Seconds);
                string creationTime = File.GetCreationTime(files[i]).ToString("dd/MM/yyyy");
                string fileName = Path.GetFileName(files[i]);
                Tuple<int, string, string> fileInfo = new Tuple<int, string, string>(i, fileName, creationTime);
                fileList.Add(fileInfo);
                ShowTrack(i, creationTime, fileName, time);
            }
        }


        private void ShowTrack(int i, string date, string name, string time)
        {
         
            Panel pdate = new Panel();
            pdate.Name = i.ToString() + "date";
            pdate.Size = new Size(200, 43);
            pdate.Location = new Point(0, 0);
            pdate.BackColor = Color.White;
            pdate.Margin = new Padding(5);
            pdate.Click += new EventHandler(this.TrackPanel_Click);
            pdate.Cursor = Cursors.Hand;
            flowLayoutPanel3.Controls.Add(pdate);
            Label ldate = new Label();
            ldate.AutoSize = true;
            ldate.Font = new Font(FontFamily.GenericSansSerif, 14);
            ldate.Name = i.ToString() + "ldate";
            ldate.Text = date;
            ldate.Location = new Point(25, 9);
            ldate.Margin = new Padding(25);
            ldate.Cursor = Cursors.Hand;
            ldate.Click += new EventHandler(this.TrackLabel_Click);
            pdate.Controls.Add(ldate);


            Panel pname = new Panel();
            pname.Name = i.ToString() + "name";
            pname.Size = new Size(589, 43);
            pname.Location = new Point(0, 0);
            pname.BackColor = Color.White;
            pname.Margin = new Padding(5);
            pname.Click += new EventHandler(this.TrackPanel_Click);
            pname.Cursor = Cursors.Hand;
            flowLayoutPanel3.Controls.Add(pname);
            Label lname = new Label();
            lname.AutoSize = true;
            lname.Font = new Font(FontFamily.GenericSansSerif, 14);
            lname.Name = i.ToString() + "lname";
            lname.Text = name;
            lname.Cursor = Cursors.Hand;
            lname.Location = new Point(25, 9);
            lname.Margin = new Padding(25);
            lname.Click += new EventHandler(this.TrackLabel_Click);
            pname.Controls.Add(lname);

            Panel ptime = new Panel();
            ptime.Name = i.ToString() + "time";
            ptime.Size = new Size(198, 43);
            ptime.Location = new Point(0, 0);
            ptime.BackColor = Color.White;
            ptime.Margin = new Padding(5);
            ptime.Click += new EventHandler(this.TrackPanel_Click);
            ptime.Cursor = Cursors.Hand;
            flowLayoutPanel3.Controls.Add(ptime);
            Label ltime = new Label();
            ltime.AutoSize = true;
            ltime.Font = new Font(FontFamily.GenericSansSerif, 14);
            ltime.Name = i.ToString() + "ltime";
            ltime.Cursor = Cursors.Hand;
            ltime.Text = time;
            ltime.Location = new Point(25, 9);
            ltime.Margin = new Padding(25);
            ltime.Click += new EventHandler(this.TrackLabel_Click);
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
            if (selectedFilePath != "")
                fmenu.filepath = libPath + "\\" + selectedFilePath;

        }

        private void Panel4_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    MidiFile mfile = MidiFile.Read(openFileDialog1.FileName);
                    string newfilePath = libPath + "\\" + openFileDialog1.SafeFileName;
                    mfile.Write(newfilePath);
                    TimeSpan timespan = mfile.GetDuration<MetricTimeSpan>();
                    string time = string.Format("{0}:{1:00}", (int)timespan.TotalMinutes, timespan.Seconds);
                    ShowTrack(fileList.Count, DateTime.Now.ToString("dd/MM/yyyy"), openFileDialog1.SafeFileName, time);
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Не удалось записать файл: " + ex.Message);
                }
            }
        }

        private void SearchTextBox_TextChanged(object sender, EventArgs e)
        {
            if (searchTextBox.Text == "")
                return;
            int index = -1;
            foreach (Tuple<int, string, string> fileInfo in fileList)
            {
                if (fileInfo.Item2.Contains(searchTextBox.Text))
                    index =  fileInfo.Item1;
            }
            if (index != -1)
            {
                selectTrack(index);
            }

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
            selectedFilePath = fileList[selectedFileIndex].Item2;
            labelSongName.Text = Path.GetFileName(selectedFilePath);
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
