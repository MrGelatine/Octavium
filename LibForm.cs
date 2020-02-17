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
        string[] files;
        int selectedFileIndex = 0;
        string selectedFilePath = "";
        string libPath = string.Format("{0}Resources", Path.GetFullPath(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, @"..\..\")));
        public LibForm()
        {
            InitializeComponent();
        }

        private void LibForm_Load(object sender, EventArgs e)
        {
            flowLayoutPanel1.BackColor = Color.FromArgb(46, 46, 46);
            this.BackColor = Color.FromArgb(69, 69, 69);
            flowLayoutPanel2.BackColor = Color.FromArgb(69, 69, 69);
            flowLayoutPanel3.BackColor = Color.FromArgb(46, 46, 46);
            files = Directory.GetFiles(libPath, "*.mid");
            for(int i = 0; i < files.Length; i++)
            {
                TimeSpan timespan = MidiFile.Read(files[i]).GetDuration<MetricTimeSpan>();
                string time = string.Format("{0}:{1:00}", (int)timespan.TotalMinutes, timespan.Seconds);
                ShowTrack(i, File.GetCreationTime(files[i]).ToString("dd/MM/yyyy"), Path.GetFileName(files[i]), time);
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
            selectedFileIndex = (int)senderPanel.Name[0] - (int)'0';
            selectedFilePath = files[selectedFileIndex];
            labelSongName.Text = Path.GetFileName(selectedFilePath);
        }

        void TrackLabel_Click(Object sender,
                           EventArgs e)
        {
            Label senderLabel = (Label)sender;
            selectedFileIndex = (int)senderLabel.Name[0] - (int)'0';
            selectedFilePath = files[selectedFileIndex];
            labelSongName.Text = Path.GetFileName(selectedFilePath);
        }
        private void BackPictureBox_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LibForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            MenuForm fmenu = (MenuForm)this.Owner;
            fmenu.filepath = selectedFilePath;

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
                    ShowTrack(files.Length, DateTime.Now.ToString("dd/MM/yyyy"), openFileDialog1.SafeFileName, time);
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Не удалось записать файл: " + ex.Message);
                }
            }
        }
    }
}
