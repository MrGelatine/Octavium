using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace WindowsFormsApp3
{
    class InterfaceFuncs
    {
        //Принимает директории на генератор нот, на хранилище нот, и на хранилище запакованных партитур, после чего генерирует необходимые файлы и добавляет их по указанным директориям
        // возвращает тройку пустых значений, если файл не был выбран
        public static Tuple<String, String, String> GetAndAddData(string engien_path = @"C:\Users\Денис\Desktop\Oct\Octavium\sheet.exe", string sheet_path = @"C:\Users\Денис\Desktop\Rep\Octavium\Gallery", string data_storage_path = @"C:\Users\Денис\Desktop\Oct\Octavium\Storage")
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
                    //CreateSheet(engien_path, midi_data_path, sheet_path);
                    MIDIFuncs.SaveToData(new MIDINotesData(midi_data_path), $@"{data_storage_path}\{InterfaceFuncs.GetFileName(midi_data_path)}");
                    return Tuple.Create(GetDate(), GetFileName(midi_data_path), MIDIFuncs.GetDuration(midi_data_path));
                }
            }
            return Tuple.Create("", "", "");
        }
        //Принимает директории на генератор нот, на запакованный файл, и на хранилище нот, после чего генерирует ноты и добавляет их в указанную папку.
        public static void CreateSheet(string engien_path, string data_path, string sheet_path)
        {
            var name = InterfaceFuncs.GetFileName(data_path);
            var sheetMaker = new ProcessStartInfo(engien_path, $"{data_path} {name}");

            sheetMaker.WindowStyle = ProcessWindowStyle.Hidden;
            sheetMaker.RedirectStandardOutput = true;
            sheetMaker.UseShellExecute = false;
            sheetMaker.CreateNoWindow = true;
            Process procCommand = Process.Start(sheetMaker);
            procCommand.WaitForExit();
            foreach (var line in Directory.GetFiles(@"C:\Users\Денис\Desktop\Oct\Octavium\bin\Release\"))
            {
                if (Regex.IsMatch(line, ".png"))
                {
                    var dest = $@"{sheet_path}\{Regex.Match(line, @"[\\]+[^\\]+.png").Value.Remove(0, 1)}";
                    if (dest.Equals(line.Replace("\\\\", "\\")))
                    {
                        File.Move(line.Replace("\\\\", "\\"), dest);
                    }
                }
            }

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
    }
}
