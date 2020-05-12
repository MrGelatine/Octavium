using System.Collections.Generic;
using System.Text;
using System.IO;
using Melanchall.DryWetMidi.Interaction;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.MusicTheory;


namespace WindowsFormsApp3
{
    class MIDIFuncs
    {
        //Возвращает номер ноты в октаве
        public static byte NoteGetPosInOctave(Melanchall.DryWetMidi.Interaction.Note n)
        {
            byte res = 0;
            switch (n.NoteName)
            {
                case NoteName.C:
                    res = 1;
                    break;
                case NoteName.CSharp:
                    res = 2;
                    break;
                case NoteName.D:
                    res = 3;
                    break;
                case NoteName.DSharp:
                    res = 4;
                    break;
                case NoteName.E:
                    res = 5;
                    break;
                case NoteName.F:
                    res = 6;
                    break;
                case NoteName.FSharp:
                    res = 7;
                    break;
                case NoteName.G:
                    res = 8;
                    break;
                case NoteName.GSharp:
                    res = 9;
                    break;
                case NoteName.A:
                    res = 10;
                    break;
                case NoteName.ASharp:
                    res = 11;
                    break;
                case NoteName.B:
                    res = 12;
                    break;
            }
            return res;
        }
        //Возвращает номер ноты на клавиатуре
        public static byte NoteGetPosOnKeyboard(Melanchall.DryWetMidi.Interaction.Note n) => n.Octave > 1 ? (byte)(NoteGetPosInOctave(n) + (n.Octave - 1) * 12 + 3) : (byte)(NoteGetPosInOctave(n));
        //Возвращает длину клавишы в секундах
        public static uint GetNoteLength(Melanchall.DryWetMidi.Interaction.Note note, TempoMap tempoMap)
        {
            uint res = (uint)note.LengthAs<MetricTimeSpan>(tempoMap).Milliseconds;
            if (note.LengthAs<MetricTimeSpan>(tempoMap).Seconds > 0)
            {
                res += (uint)(note.LengthAs<MetricTimeSpan>(tempoMap).Seconds * 1000);
            }
            if (note.LengthAs<MetricTimeSpan>(tempoMap).Minutes > 0)
            {
                res += (uint)(note.LengthAs<MetricTimeSpan>(tempoMap).Minutes * 60000);
            }
            return res;
        }
        //Возвращает время нажатия клавишы
        public static uint GetNoteTime(Melanchall.DryWetMidi.Interaction.Note note, TempoMap tempoMap)
        {
            uint res = (uint)note.TimeAs<MetricTimeSpan>(tempoMap).Milliseconds;
            if (note.TimeAs<MetricTimeSpan>(tempoMap).Seconds > 0)
            {
                res += (uint)note.TimeAs<MetricTimeSpan>(tempoMap).Seconds * 1000;
            }
            if (note.TimeAs<MetricTimeSpan>(tempoMap).Minutes > 0)
            {
                res += (uint)note.TimeAs<MetricTimeSpan>(tempoMap).Minutes * 60000;
            }
            return res;
        }
        //Возвращает аналог клавишы из правой октавы
        public static void HigherOctave(Melanchall.DryWetMidi.Interaction.Note n) => n.SetNoteNameAndOctave(n.NoteName, n.Octave + 1);
        //Возвращает аналог клавишы из левой октавы
        public static void LowerOctave(Melanchall.DryWetMidi.Interaction.Note n) => n.SetNoteNameAndOctave(n.NoteName, n.Octave - 1);
        //Запаковывает информацию для падающих клавиш в файл
        public static void SaveToData(MIDINotesData rawData, string path)
        {
            using (var fl = new BinaryWriter(File.Create(path + ".dat"), Encoding.Default))
            {
                fl.Write(rawData.count);
                foreach (var data in rawData.flowkeys)
                {
                    fl.Write(data.pos);
                    fl.Write(data.time);
                    fl.Write(data.length);
                }
            }
        }
        //Возвращает список информации для падающих клавиш из запакованного файла
        public static List<FlowKeyData> UnpackDataToNote(string path) 
        {
            var res = new List<FlowKeyData>();
            using (var fl = new BinaryReader(File.Open(path, FileMode.Open)))
            {
                uint roof = fl.ReadUInt32();
                for (int i = 1; i <= roof; i++)
                {
                    res.Add(new FlowKeyData(fl.ReadByte(), fl.ReadUInt32(), fl.ReadUInt32()));
                }
            }
            return res;
        }
        //Возвращает длины трека из файла
        public static string GetDuration(string path)
        {

                var t = MidiFile.Read(path).GetDuration<MetricTimeSpan>();
                if (t.Hours == 0)
                {
                    if (t.Minutes == 0)
                    {
                        return "0:" + t.Seconds.ToString();
                    }
                    else
                    {
                        return t.Minutes.ToString() + ':' + (t.Seconds).ToString();
                    }
                }
                else
                {
                    return t.Hours + ':' + (t.Hours * 60 - t.Minutes).ToString() + ':' + (t.Seconds - t.Hours * 3600 - t.Minutes * 60).ToString();
                }
        }
    }
}
