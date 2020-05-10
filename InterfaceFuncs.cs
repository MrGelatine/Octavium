using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Windows.Forms;
using System.Drawing;
using System.Globalization;
using System.ComponentModel;
using System.Data;
using Melanchall.DryWetMidi.Interaction;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.MusicTheory;
using Melanchall.DryWetMidi.Tools;
using Melanchall.DryWetMidi;

namespace WindowsFormsApp3
{
    class InterfaceFuncs
    {
        //Принимает директории на генератор нот, на хранилище нот, и на хранилище запакованных партитур, после чего генерирует необходимые файлы и добавляет их по указанным директориям
        // возвращает тройку пустых значений, если файл не был выбран
        public static Tuple<String, String, String> GetAndAddData(string engien_path, string sheet_path, string data_storage_path)
        {
            string midi_data_path = null;
            using (var dialog = new OpenFileDialog())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    midi_data_path = dialog.FileName;
                    if (!Is_mid(midi_data_path))
                    {
                        throw new FormatException("Выбранный файл не является midi!");
                    }
                    CreateSheet(engien_path, midi_data_path, sheet_path);
                    midi_data_path = Regex.Replace(midi_data_path, "_", " ");
                    try
                    {
                        MIDIFuncs.SaveToData(new MIDINotesData(midi_data_path), $@"{data_storage_path}\{InterfaceFuncs.GetFileName(midi_data_path)}");
                        return Tuple.Create(GetDate(), GetFileName(midi_data_path), MIDIFuncs.GetDuration(midi_data_path));
                    }
                    catch(NotEnoughBytesException e)
                    {
                        MessageBox.Show("Входной файл имеет недопустимую для обработки внутреннюю конфигурацию", e.Message, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }
                }
            }
            return Tuple.Create("", "", "");
        }
        //Принимает директории на генератор нот, на запакованный файл, и на хранилище нот, после чего генерирует ноты и добавляет их в указанную папку.
        public static void CreateSheet(string engien_path, string data_path, string sheet_path)
        {
            var work_path = Regex.Replace(data_path, @"\s", "_");
            var smooth_path = Regex.Replace(work_path,"_"," ");
            var name = InterfaceFuncs.GetFileName(work_path);
            File.Move(data_path,work_path);
            var sheetMaker = new ProcessStartInfo(engien_path, $"{work_path} {name}");
            sheetMaker.WindowStyle = ProcessWindowStyle.Hidden;
            sheetMaker.RedirectStandardOutput = true;
            sheetMaker.UseShellExecute = false;
            sheetMaker.CreateNoWindow = true;
            Process procCommand = Process.Start(sheetMaker);
            procCommand.WaitForExit();
            foreach (var line in Directory.GetFiles(engien_path.Remove(engien_path.Length-9,9) + "bin\\Release"))
            {
                if (Regex.IsMatch(line, ".png"))
                {
                    var dest = $@"{sheet_path}\{Regex.Match(line, @"[\\]+[^\\]+.png").Value.Remove(0, 1)}";
                    try
                    {
                        File.Move(line, dest);
                    }
                    catch (IOException e) { }
                }
            }
            foreach(var line in Directory.GetFiles(sheet_path))
            {
                File.Move(line, Regex.Replace(line,"_", " "));
            }
            File.Move(work_path,smooth_path);

        }
        //Возвращает текущую дату
        public static string GetDate()
        {
            return DateTime.Today.ToString().Split().First();
        }
        //Возвращает имя файла из его директории
        public static string GetFileName(string path)
        {
            var res = Regex.Match(path, @"[\\]+[^\\]+.mid").Value.Remove(0, 1);
            return res.Remove(res.Length - 4, 4);
        }
        //Проверка на то, являеться ли файл формата .mid
        public static bool Is_mid(string s)
        {
            return s.Contains(".mid");
        }
        //Обновляет информацию в файла с названиями композиций
        public static void catalog_inform_refresh(string inform_path, string dat_path)
        {
            //File.Create(dat_path + "\\temp_lib.txt");
            using (var inform_w = new StreamWriter(dat_path + "\\temp_lib.txt"))
            {
                var lines = File.ReadAllLines(inform_path);
                foreach (var line in lines)
                {
                    {
                        var str = Regex.Match(line, @"[\|].*[\|]").Value;
                        str = str.Remove(str.Length - 1, 1).Remove(0,1);
                        foreach (var file in Directory.GetFiles(dat_path))
                        {
                            var file_path = Regex.Match(file, @"[\\]+[^\\]+.dat").Value;
                            if (file_path.Length != 0)
                            {
                                file_path = file_path.Remove(0, 1);
                                file_path = file_path.Remove(file_path.Length - 4, 4);
                                if (str == file_path)
                                {
                                    inform_w.WriteLine(line);
                                }
                            }
                        }
                    }
                }
            }
            File.Delete(inform_path);
            File.Move(dat_path + "\\temp_lib.txt", dat_path + "\\lib.txt");
        }
        public static void Base_Library_Call(string filepath, double speed = 1, string img = null, Color? cl1 = null, Color? cl2 = null, int volume = 100)
        {
            Form1 form1;
            string path = Path.GetFullPath(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, @"..\..\"))+ @"\Resources\Backgrounds";
            switch (Regex.Match(filepath, @"[\\]+[^\\]+.dat").Value.Remove(0, 1).Remove(Regex.Match(filepath, @"[\\]+[^\\]+.dat").Value.Remove(0, 1).Length - 4, 4))
            {
                case "Денис Шишов":
                    {
                        form1 = new Form1(filepath, speed, $@"{path}\bloom.jpg", Color.Yellow, Color.Yellow, Color.DarkOrange, Color.DarkOrange);
                        break;
                    }
                case "Дмитрий Иващенко":
                    {
                        form1 = new Form1(filepath, speed, $@"{path}\blue.jpg", Color.DeepSkyBlue, Color.DeepSkyBlue, Color.MidnightBlue, Color.MidnightBlue);
                        break;
                    }
                case "Артем Шевердин":
                    {
                        form1 = new Form1(filepath, speed, $@"{path}\hero.jpg",Color.IndianRed,Color.IndianRed,Color.DarkSlateGray,Color.DarkSlateGray);
                        break;
                    }
                case "Ахмед Хоссайни":
                    {
                        form1 = new Form1(filepath, speed, $@"{path}\pirates.jpg",Color.Firebrick,Color.Firebrick,Color.Chocolate,Color.Chocolate);
                        break;
                    }
                case "Андрей Белоусов":
                    {
                        form1 = new Form1(filepath,speed, $@"{path}\skyrim.jpg",Color.DarkGray,Color.DarkGray,Color.DarkSlateGray,Color.DarkSlateGray);
                        break;
                    }
                case "Александр Данильченко":
                    {
                        form1 = new Form1(filepath, speed, $@"{path}\coat of arm.jpg", Color.DarkGray, Color.DarkRed, Color.Crimson, Color.Gold);
                        break;
                    }
                case "anonymous":
                    {
                        form1 = new Form1(filepath, speed, $@"{path}\school.jpg", Color.ForestGreen, Color.ForestGreen, Color.DarkGreen, Color.DarkGreen);
                        break;
                    }
                case "Иван Игнатенко":
                    {
                        form1 = new Form1(filepath, speed, $@"{path}\piano.jpg", Color.Black, Color.Black, Color.Black, Color.Black);
                        break;
                    }
                case "test":
                    {
                        form1 = new Form1(filepath, speed, $@"{path}\test.jpg", Color.Blue, Color.Blue, Color.Indigo, Color.Indigo,true);
                        break;
                    }
                default:
                    {
                        form1 = new Form1(filepath, speed, img, cl1, null, cl2, null);
                        break;
                    }
            }
            form1.ShowDialog();
        }
        public static bool IsSheetGenerated(string path, string name)
        {
            foreach (string file in Directory.GetFiles(path))
            {
                if (Regex.IsMatch(file,$@"{name} \d+.png"))
                {
                    return true;
                }
            }
            return false;
        }
        public static void Sheets_Flush(string sheet_path, string info_path)
        {
            foreach (var file in Directory.GetFiles(Regex.Replace(sheet_path,@"\\\\",@"\\")))
            {
                bool flag = true;
                foreach (var line in File.ReadLines(info_path))
                {
                    var str = Regex.Match(line, @"[\|].*[\|]").Value;
                    str = str.Remove(str.Length - 1, 1).Remove(0, 1);
                    if (Regex.IsMatch(file, str))
                    {
                        flag = false;   
                    }
                }
                if(flag)
                File.Delete(file);
            }
        }
    }
}
