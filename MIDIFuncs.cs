using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Melanchall.DryWetMidi.Interaction;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.MusicTheory;
using Melanchall.DryWetMidi.Tools;
using Melanchall.DryWetMidi;
using System.IO;
using System.Text.RegularExpressions;
namespace WindowsFormsApp3
{
    class MIDIFuncs
    {
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
        public static byte NoteGetPosOnKeyboard(Melanchall.DryWetMidi.Interaction.Note n) => n.Octave > 1 ? (byte)(NoteGetPosInOctave(n) + n.Octave * 8 + 3) : (byte)(NoteGetPosInOctave(n));
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
        public static void HigherOctave(Melanchall.DryWetMidi.Interaction.Note n) => n.SetNoteNameAndOctave(n.NoteName, n.Octave + 1);
        public static void LowerOctave(Melanchall.DryWetMidi.Interaction.Note n) => n.SetNoteNameAndOctave(n.NoteName, n.Octave - 1);
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
    }
}
