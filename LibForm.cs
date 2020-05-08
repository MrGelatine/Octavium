using System;
using System.Collections.Generic;
using System.Globalization;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using Sanford.Multimedia.Midi;
using Sanford.Multimedia.Midi.UI;

using Melanchall.DryWetMidi.Interaction;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.MusicTheory;
using Melanchall.DryWetMidi.Tools;
using Melanchall.DryWetMidi;

namespace WindowsFormsApp3
{
    public partial class LibForm : Form
    {
        //Player
        int time = 0;
        MIDINotesData My;
        private OutputDevice outDevice;
        private int outDeviceID = 0;
        private String CurrentSong = "";
        private OutputDeviceDialog outDialog = new OutputDeviceDialog();

        //список всех треков, которые есть в галлерее(Индекс, Дата, Наименование, Длительность)
        List<Tuple<int, string, string, string>> libfileList = new List<Tuple<int, string, string, string>>();
        //Список текущих(выведенный на экране по поиску) треков(Индекс, Дата, Наименование, длительность)
        List<Tuple<int, string, string, string>> curfiles = new List<Tuple<int, string, string, string>>();
        //Индекс выбранного трека в списке libfileList и curfiles
        int selectedFileIndex = -1;
        //наименование выбранного трека
        string selectedFileName = "";
        int selectedFileDuration = 0;
        //прослушивается ли трек
        bool playing = false;
        Color backColor1 = Color.FromArgb(46, 46, 46);
        Color backColor2 = Color.FromArgb(69, 69, 69);
        //путь к проекту
        string projectPath = Path.GetFullPath(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, @"..\..\"));
        //путь к галлерее
        string libPath;

        //запуск пианино сразу после выбора трека
        bool starting = false;

        //форма открыта по кнопке "Галлерея", а не по кнопке "Начало"
        bool library = true;

        public LibForm(bool lib = true)
        {
            libPath = string.Format("{0}Resources\\DataStorage", projectPath);
            library = lib;
            InitializeComponent();
            this.KeyPreview = true;
            if (!lib)
            {
                playLabel.Visible = false;
                addPanel.Visible = false;
                labelSongName.Location = new Point(28, -2);
            }
        }

        //Player
        protected override void OnLoad(EventArgs e)
        {
            if (OutputDevice.DeviceCount == 0)
            {
                MessageBox.Show("No MIDI output devices available.", "Error!",
                    MessageBoxButtons.OK, MessageBoxIcon.Stop);

                Close();
            }
            else
            {
                try
                {
                    outDevice = new OutputDevice(outDeviceID);



                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error!",
                        MessageBoxButtons.OK, MessageBoxIcon.Stop);

                    Close();
                }
            }

            base.OnLoad(e);
        }

        //При загрузке формы устанавливаем цвета для элементов и выводим все треки из галлереи
        private void LibForm_Load(object sender, EventArgs e)
        {
            InterfaceFuncs.catalog_inform_refresh(libPath+@"\lib.txt", libPath);
            flowLayoutPanel1.BackColor = backColor1;
            this.BackColor = backColor2;
            searchTextBox.BackColor = backColor1;
            flowLayoutPanel2.BackColor = backColor2;
            TrackListPanel.BackColor = backColor1;
            //заполняем списки curfiles и libFileList(при создании формы они одинаковы)
            GetTracksFromLib();
            //выводим треки из списка curfiles
            ShowTracks();
        }

        //отображаем все треки из списка curFiles
        private void ShowTracks()
        {
            TrackListPanel.Controls.Clear();
            selectedFileIndex = -1;
            //Первую строку-заголовок тоже отображаю как "неактивный трек"(который нельзя выбрать)
            ShowTrack(-1, "Дата", "Название", "Время", false);
            foreach (Tuple<int, string, string, string> fileInfo in curfiles) 
            {
                ShowTrack(fileInfo.Item1, fileInfo.Item2, fileInfo.Item3, fileInfo.Item4);
            }
        }

        //получаем треки из галлереи и заполняем списки curfiles и libFileList
        private void GetTracksFromLib()
        {
            //информация по трекам хранится в файле "lib.txt", список треков строится по нему
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

        //отображаем один трек по заданной информации
        //activeHandler - можно ли будет выбрать этот трек(поскольку первая строка-заголовок тоже выводится этой функцией)
        private void ShowTrack(int i, string date, string name, string time, bool activeHandler = true)
        {
            if (!library)
            {
                createTrackPanel(i, "date", date, 190, 43, activeHandler);
                createTrackPanel(i, "name", name, 600, 43, activeHandler);
                createTrackPanel(i, "time", time, 200, 43, activeHandler);
            }
            else
            {
                createTrackPanel(i, "date", date, 150, 43, activeHandler);
                createTrackPanel(i, "name", name, 590, 43, activeHandler);
                createTrackPanel(i, "time", time, 190, 43, activeHandler);
                //отображаем панель с иконкой удаления
                if (activeHandler)
                {
                    Panel pdel = new Panel();
                    pdel.Name = i.ToString() + "del";
                    pdel.Size = new Size(45, 43);
                    pdel.Location = new Point(0, 0);
                    pdel.Margin = new Padding(5);
                    pdel.Cursor = Cursors.Hand;
                    pdel.BackgroundImage = Properties.Resources.trashCan;
                    pdel.BackgroundImageLayout = ImageLayout.Stretch;
                    pdel.Click += new EventHandler(this.deletePanel_Click);
                    TrackListPanel.Controls.Add(pdel);
                }
            }

        }

        void createTrackPanel(int i, string name, string text, int w, int h, bool activeHandler)
        {
            Panel panel = new Panel();
            //в наименовании элементов формы прописываем индекс трека
            panel.Name = i.ToString() + name;
            panel.Size = new Size(w, h);
            panel.Location = new Point(0, 0);
            panel.BackColor = Color.White;
            panel.Margin = new Padding(5);
            if (activeHandler)
            {
                panel.Click += new EventHandler(this.TrackPanel_Click);
                panel.DoubleClick += new EventHandler(this.TrackPanel_DoubleClick);
                panel.Cursor = Cursors.Hand;
            }
            TrackListPanel.Controls.Add(panel);
            Label label = new Label();
            label.AutoSize = true;
            label.Font = new Font(FontFamily.GenericSansSerif, 14);
            label.Name = i.ToString() + "l" + name;
            label.Text = text;
            label.Location = new Point(25, 9);
            label.Margin = new Padding(25);
            if (activeHandler)
            {
                label.Click += new EventHandler(this.TrackLabel_Click);
                label.DoubleClick += new EventHandler(this.TrackLabel_DoubleClick);
                label.Cursor = Cursors.Hand;
            }
            panel.Controls.Add(label);
        }

        //срабатывает при нажатии на панель трека из списка
        void TrackPanel_Click(Object sender,
                           EventArgs e)
        {
            Panel senderPanel = (Panel)sender;
            //получаем индекс трека в списках libFileList и curFiles
            string indexString = senderPanel.Name.Substring(0, senderPanel.Name.Length - 4);
            try
            {
                int i = Int32.Parse(indexString);
                //вызываем функцию выбора трека по индексу
                selectTrack(i);
            }
            catch (FormatException expt)
            {
                Console.WriteLine(expt.Message);
            }
        }

        //срабатывает при нажатии на надпись трека из списка
        void TrackLabel_Click(Object sender,
                           EventArgs e)
        {
            Label senderLabel = (Label)sender;
            //получаем индекс трека в списках libFileList и curFiles
            string indexString = senderLabel.Name.Substring(0, senderLabel.Name.Length - 5);
            try
            {
                int i = Int32.Parse(indexString);
                //вызываем функцию выбора трека по индексу
                selectTrack(i);
            }
            catch (FormatException expt)
            {
                Console.WriteLine(expt.Message);
            }
        }

        void TrackPanel_DoubleClick(Object sender,
                          EventArgs e)
        {
            if (library)
            {
                Panel p = (Panel)sender;
                var viewer = new SheetViewer(projectPath + @"Resources\\Sheets", p.Name);
            }
            else
            {
                TrackPanel_Click(sender, e);
                starting = true;
                this.Close();
            }
        }

        void TrackLabel_DoubleClick(Object sender,
                           EventArgs e)
        {
            if (library)
            {
                Panel p = (Panel)sender;
                var viewer = new SheetViewer(projectPath + @"Resources\\Sheets", p.Name);
            }
            else
            {
                TrackPanel_Click(sender, e);
                starting = true;
                this.Close();
            }
        }

        void deletePanel_Click(Object sender, EventArgs e)
        {
            Panel senderPanel = (Panel)sender;
            //получаем индекс трека в списках libFileList и curFiles
            string indexString = senderPanel.Name.Substring(0, senderPanel.Name.Length - 3);
            try
            {
                int i = Int32.Parse(indexString);
                //вызываем функцию выбора трека по индексу
                deleteTrack(i);
            }
            catch (FormatException expt)
            {
                Console.WriteLine(expt.Message);
            }
        }
        private void BackPictureBox_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //при закрытии формы галлереи передаем путь к треку в фому меню
        private void LibForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            MenuForm fmenu = (MenuForm)this.Owner;
            if (selectedFileName != "")
                fmenu.filepath = libPath + @"\" + selectedFileName + ".dat" ;
            fmenu.starting = starting;
            //Player
            base.OnClosing(e);
        }

        //Удаление трека из списка по индексу
        private void deleteTrack(int i)
        {
            string filename = "";
            //Удаляем информацию из списков curfiles и libfileList
            foreach(Tuple<int, string, string, string> fileInfo in curfiles)
            {
                if (fileInfo.Item1 == i)
                {
                    filename = fileInfo.Item3;
                    curfiles.Remove(fileInfo);
                    break;
                }

            }
            foreach (Tuple<int, string, string, string> fileInfo in libfileList)
            {
                if (fileInfo.Item1 == i)
                {
                    libfileList.Remove(fileInfo);
                    break;
                }
            }
            //удаляем файл .dat
            File.Delete(libPath + @"\" + filename + ".dat");
            //Обновляем информацию о треках в файле lib.txt
            InterfaceFuncs.catalog_inform_refresh(libPath + @"\lib.txt", libPath);

            //Удаляем элементы формы трека
            deletePanels(i);

            selectedFileIndex = -1;
            selectedFileName = "";
            labelSongName.Text = "";

        }

        //Удаляет элементы формы трека по индексу
        private void deletePanels(int i)
        {
            Panel trackTimePanel = (Panel)this.Controls.Find(i.ToString() + "time", true).First();
            Panel trackCreationPanel = (Panel)this.Controls.Find(i.ToString() + "date", true).First();
            Panel trackNamePanel = (Panel)this.Controls.Find(i.ToString() + "name", true).First();
            Panel delPanel = (Panel)this.Controls.Find(i.ToString() + "del", true).First();
            TrackListPanel.Controls.Remove(trackTimePanel);
            TrackListPanel.Controls.Remove(trackCreationPanel);
            TrackListPanel.Controls.Remove(trackNamePanel);
            TrackListPanel.Controls.Remove(delPanel);
            trackTimePanel.Dispose();
            trackCreationPanel.Dispose();
            trackNamePanel.Dispose();
            delPanel.Dispose();

        }

        //Player
        protected override void OnClosed(EventArgs e)
        {
            timer1.Stop();
            time = 0;
            if (outDevice != null)
            {
                outDevice.Dispose();
            }

            outDialog.Dispose();

            base.OnClosed(e);
        }

        //Добавление трека в галлерею
        private void AddPanel_Click(object sender, EventArgs e)
        {
            //получаем данные трека(если файл не выбран, то получим тройку пустых строк)
            Tuple<string, string, string> fileData = InterfaceFuncs.GetAndAddData(projectPath + @"sheet.exe", projectPath + @"Resources\\Sheets", libPath);
            if (fileData.Item1 == "")
                return;
            //проверяем, что трека с таким названием нету
            foreach (Tuple<int, string, string, string> libFileInfo in libfileList)
            {
                if (libFileInfo.Item3.ToLower().Contains(fileData.Item2.ToLower()))
                {
                    MessageBox.Show("Файл с таким названием уже есть");
                    return;
                }
            }
            //Добавляем информацию по треку в текстовый файл, в котором хранится информация по всем трекам
            AddtxtLib(fileData);
            //Добавляем информацию по треку в оба списка треков(общий и отображаемый)
            Tuple<int, string, string, string> fileInfo = new Tuple<int, string, string, string>(libfileList.Count, fileData.Item1, fileData.Item2, fileData.Item3);
            libfileList.Add(fileInfo);
            curfiles.Add(fileInfo);
            //отображаем трек
            ShowTrack(fileInfo.Item1, fileInfo.Item2, fileInfo.Item3, fileInfo.Item4);
        }

        //добавляет информацию по треку в текстовый файл - описание треков в галлерее
        private void AddtxtLib(Tuple<string, string, string> fileData)
        {
            string fileInfoText = fileData.Item1 + '|' + fileData.Item2 + '|' + fileData.Item3 + Environment.NewLine;
            System.IO.File.AppendAllText(libPath + @"\lib.txt", fileInfoText);
        }

        //при изменении текста в поле поиска переформировываем список отображаемых треков
        private void SearchTextBox_TextChanged(object sender, EventArgs e)
        {
            curfiles.Clear();
            //Если в поле поиска ничего не указано, то в список отображаемых добавляем все треки
            if (searchTextBox.Text == "")
            {
                foreach (Tuple<int, string, string, string> fileInfo in libfileList)
                    curfiles.Add(fileInfo);
            }
            else
            {
                //Треки из общего списка добавляются в список отображаемых, если наименование содержит искомую подстроку
                foreach (Tuple<int, string, string, string> fileInfo in libfileList)
                {
                    if(Regex.IsMatch(fileInfo.Item3.ToLower(),$@"^{searchTextBox.Text.ToLower()}"))
                        curfiles.Add(fileInfo);
                }
            }
            //выводится список отображаемых треков
            ShowTracks();

        }

        //Срабатывает при выборе трека(параметр - индекс выбранного трека в списках libfileList и curfile) 
        private void selectTrack(int i)
        {
            //Получаем элементы формы предыдущего выбранного трека(индекс трека записан в наименовании элементов)
            Panel prevTrackTime = (Panel)this.Controls.Find(selectedFileIndex.ToString() + "time", true).First();
            Panel prevTrackCreation = (Panel)this.Controls.Find(selectedFileIndex.ToString() + "date", true).First();
            Panel prevTrackName = (Panel)this.Controls.Find(selectedFileIndex.ToString() + "name", true).First();
            //меняем цвет предыдущего выбранноого трека
            prevTrackTime.BackColor = Color.White;
            prevTrackTime.ForeColor = Color.Black;
            prevTrackCreation.BackColor = Color.White;
            prevTrackCreation.ForeColor = Color.Black;
            prevTrackName.BackColor = Color.White;
            prevTrackName.ForeColor = Color.Black;
            //Сохраняем индекс нового выбранного трека, его наименование и длительность, меняем надпись текущего трека.
            selectedFileIndex = i;
            selectedFileName = libfileList[selectedFileIndex].Item3;
            try
            {
                DateTime Duration = DateTime.ParseExact(libfileList[selectedFileIndex].Item4, "m:s", new CultureInfo("en-US"));
                selectedFileDuration = (Duration.Minute * 60 + Duration.Second) * 1000;
            }
            catch (FormatException expt)
            {
                Console.WriteLine(expt.Message);
            }
            //Player
            if (selectedFileName != CurrentSong)
            {
                time = 0;
                timer1.Stop();
                playLabel.BackgroundImage = Properties.Resources.icons8_play_96_1;
                playing = false;
                CurrentSong = selectedFileName;
            }
            labelSongName.Text = Path.GetFileName(selectedFileName).Length > 40 ? Path.GetFileName(selectedFileName).Remove(39,Path.GetFileName(selectedFileName).Length-39)+"...": Path.GetFileName(selectedFileName);
            //Получаем элементы формы выбранного трека
            Panel trackTime = (Panel)this.Controls.Find(i.ToString() + "time", true).First();
            Panel TrackCreation = (Panel)this.Controls.Find(i.ToString() + "date", true).First();
            Panel TrackName = (Panel)this.Controls.Find(i.ToString() + "name", true).First();

            //фокусируемся на выбранном треке(чтобы пролистать скролл, когда треков много)
            //TrackName.Select();
            //фокусируемся на поле поиска
            //searchTextBox.Select();

            //меняем цвет выбранного трека
            trackTime.BackColor = backColor2;
            trackTime.ForeColor = Color.White;
            TrackName.BackColor = backColor2;
            TrackName.ForeColor = Color.White;
            TrackCreation.BackColor = backColor2;
            TrackCreation.ForeColor = Color.White;
        }
        
        //Player Function
        private void PlayingMid()
        {
            foreach (var x in My.flowkeys)
            {
                if ((x.time / (timer1.Interval) == (time / timer1.Interval)))
                {
                    outDevice.Send(new ChannelMessage(ChannelCommand.NoteOn, 0, x.pos + 20, 127));
                }
                if ((x.time + x.length) / timer1.Interval == (time / timer1.Interval))
                {
                    outDevice.Send(new ChannelMessage(ChannelCommand.NoteOff, 0, x.pos + 20, 0));
                }
            }
        }
        private void PlayLabel_Click(object sender, EventArgs e)
        {
            if (selectedFileName == "")
                return;
            else
            {
                //Player
                My = new MIDINotesData((MIDIFuncs.UnpackDataToNote(libPath + @"\" + selectedFileName + ".dat")));
                if (playing)
                {
                    timer1.Stop();
                    playLabel.BackgroundImage = Properties.Resources.icons8_play_96_1;
                    playing = !playing;
                }
                else
                {
                    timer1.Start();
                    playLabel.BackgroundImage = Properties.Resources.icons8_pause_96_1;
                    playing = !playing;
                }
            }
        }

        //Player
        private void timer1_Tick(object sender, EventArgs e)
        {
            PlayingMid();
            time += timer1.Interval;
            if (time > selectedFileDuration)
            {
                time = 0;
                timer1.Stop();
                playLabel.BackgroundImage = Properties.Resources.icons8_play_96_1;
                playing = false;
            }
        }

        //"Горячие клавишы" галереи
        private void LibForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && library)
                AddPanel_Click(sender, e);

            if (selectedFileIndex == -1)
                return;
            if (e.KeyCode == Keys.Enter && !library)
            {
                starting = true;
                this.Close();
            }
            if (e.KeyCode == Keys.Delete && library)
                deleteTrack(selectedFileIndex);
        }
    }
}
