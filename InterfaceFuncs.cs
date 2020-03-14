using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Windows.Forms;

namespace WindowsFormsApp3
{
    class InterfaceFuncs
    {
        public static Tuple<String,String,String> GetAndAddData(string engien_path = @"C:\Users\Денис\Desktop\Oct\Octavium\sheet.exe", string sheet_path = @"C:\Users\Денис\Desktop\Rep\Octavium\Gallery", string data_storage_path = @"C:\Users\Денис\Desktop\Oct\Octavium\Storage")
        {
            string midi_data_path = null;
            using (var dialog = new OpenFileDialog())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    midi_data_path = dialog.FileName;
                }
            }
            if(!Is_mid(midi_data_path))
            {
                throw new FormatException("Выбранный файл не является midi!");
            }
            //CreateSheet(engien_path, midi_data_path, sheet_path);
            MIDIFuncs.SaveToData(new MIDINotesData(midi_data_path), $@"{data_storage_path}\{InterfaceFuncs.GetFileName(midi_data_path)}");
            return Tuple.Create(GetDate(), GetFileName(midi_data_path), MIDIFuncs.GetDuration(midi_data_path));
        }
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
        public static string GetDate()
        {
            string res = DateTime.Today.ToString().Split().First();
            return res;
        }
        public static string GetFileName(string path)
        {
            var res = Regex.Match(path, @"[\\]+[^\\]+.mid").Value.Remove(0, 1);
            return res.Remove(res.Length - 4, 4);
        }
        public static bool Is_mid(string s)
        {
            return s.Contains(".mid");
        }
    }
}
