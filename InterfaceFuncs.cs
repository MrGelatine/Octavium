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
        public static Tuple<String,String,String> GetAndAddData(string engien_path = @"C:\Users\Денис\Desktop\Rep\Octavium\sheet.exe", string sheet_path = @"C:\Users\Денис\Desktop\Rep\Octavium\Gallery\Sheets", string data_storage_path = @"C:\Users\Денис\Desktop\Rep\Octavium\Gallery\Piano data")
        {
            string midi_data_path = null;
            using (var dialog = new FolderBrowserDialog())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    midi_data_path = dialog.SelectedPath;
                }
            }
            CreateSheet(engien_path, midi_data_path, sheet_path);
            MIDIFuncs.SaveToData(new MIDINotesData(midi_data_path), data_storage_path);
            return Tuple.Create(GetDate(), GetFileName(midi_data_path), MIDIFuncs.GetDuration(midi_data_path));
        }
        public static void CreateSheet(string engien_path,string data_path, string sheet_path)
        {
            var name = Regex.Match(data_path, @"[\\]+[^\\]+.mid").Value.Remove(0, 1);
            var sheetMaker = new ProcessStartInfo(engien_path, $"{data_path} {name}");

            sheetMaker.WindowStyle = ProcessWindowStyle.Hidden;
            sheetMaker.RedirectStandardOutput = true;
            sheetMaker.UseShellExecute = false;
            sheetMaker.CreateNoWindow = true;

            Process procCommand = Process.Start(sheetMaker);
            procCommand.WaitForExit();
            foreach (var line in Directory.GetFiles(data_path.Remove(data_path.Length - name.Length - 3, name.Length + 4)))
            {
                if (Regex.IsMatch(line, ".png"))
                {
                    File.Move(line, $@"{sheet_path}\{Regex.Match(line, @"[\\]+[^\\]+.png").Value.Remove(0, 1)}");
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
            return res.Remove(res.Length - 3, 3);
        }
    }
}
