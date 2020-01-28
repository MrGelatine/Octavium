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
        public static int NoteGetPosInOctave(Melanchall.DryWetMidi.Interaction.Note n)
        {
            int res = 0;
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
        public static int NoteGetPosOnKeyboard(Melanchall.DryWetMidi.Interaction.Note n) => n.Octave > 1 ? NoteGetPosInOctave(n) + n.Octave * 8 + 3 : NoteGetPosInOctave(n);
        public static int GetNoteLength(Melanchall.DryWetMidi.Interaction.Note note, TempoMap tempoMap)
        {
            int res = note.LengthAs<MetricTimeSpan>(tempoMap).Milliseconds;
            if (note.LengthAs<MetricTimeSpan>(tempoMap).Seconds > 0)
            {
                res += note.LengthAs<MetricTimeSpan>(tempoMap).Seconds * 1000;
            }
            if (note.LengthAs<MetricTimeSpan>(tempoMap).Minutes > 0)
            {
                res += note.LengthAs<MetricTimeSpan>(tempoMap).Minutes * 60000;
            }
            return res;
        }
        public static int GetNoteTime(Melanchall.DryWetMidi.Interaction.Note note, TempoMap tempoMap)
        {
            int res = note.TimeAs<MetricTimeSpan>(tempoMap).Milliseconds;
            if (note.TimeAs<MetricTimeSpan>(tempoMap).Seconds > 0)
            {
                res += note.TimeAs<MetricTimeSpan>(tempoMap).Seconds * 1000;
            }
            if (note.TimeAs<MetricTimeSpan>(tempoMap).Minutes > 0)
            {
                res += note.TimeAs<MetricTimeSpan>(tempoMap).Minutes * 60000;
            }
            return res;
        }
        public static void MidiFileGetInfo(string path)
        {
            using (var flow = new StreamWriter("informer.txt"))
            {
                var f = MidiFile.Read(path);
                SortedDictionary<int, int> octave_counter = new SortedDictionary<int, int>();
                flow.WriteLine("Track Name : " + Regex.Match(Regex.Match(path, @"[^.]+.").Value, @"[^.]+").Value);
                var tempoMap = f.GetTempoMap();
                int notrecount = 0;
                int length = 0;
                int time = 0;
                foreach (var chunk in f.Chunks)
                {
                    using (var notesManager = new NotesManager(((TrackChunk)chunk).Events))
                    {
                        notrecount += notesManager.Notes.Count();
                        foreach (var note in notesManager.Notes)
                        {
                            length = note.LengthAs<MetricTimeSpan>(tempoMap).Milliseconds;
                            time = note.TimeAs<MetricTimeSpan>(tempoMap).Milliseconds;
                            flow.WriteLine($"{note.NoteName} , {time} , {length}");
                            if (octave_counter.ContainsKey(note.Octave))
                            {
                                octave_counter[note.Octave]++;
                            }
                            else
                            {
                                octave_counter.Add(note.Octave, 1);
                            }
                        }
                    }
                }
                flow.WriteLine($"Общее кол-во нот : {notrecount}");
                flow.WriteLine("Общая статистика по октавам : ");
                foreach (var elem in octave_counter)
                {
                    flow.WriteLine($"{elem.Key}-ая октава : {elem.Value}");
                }
                flow.WriteLine();
                flow.WriteLine($"Самая часто используемая октава : {octave_counter.OrderBy(x => x.Value).Last().Key}-ая");
                flow.WriteLine($"Самая редко используемая октава : {octave_counter.OrderBy(x => x.Value).First().Key}-ая");
                flow.WriteLine($"Временное деление файла : {f.TimeDivision}");
            }
        }
        public static void HigherOctave(Melanchall.DryWetMidi.Interaction.Note n) => n.SetNoteNameAndOctave(n.NoteName, n.Octave + 1);
        public static void LowerOctave(Melanchall.DryWetMidi.Interaction.Note n) => n.SetNoteNameAndOctave(n.NoteName, n.Octave - 1);
    }
}
