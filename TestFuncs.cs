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
    class TestFuncs
    {
        //Записывает данные из структуры для хранения информации для падающих клавиш в текстовый файл
        static void midiRawToTXT(MIDINotesData rawData,string path)
        {
            using (var fl = new StreamWriter(path))
            {
                foreach (var flowdata in rawData.flowkeys)
                {
                    fl.WriteLine($"{flowdata.pos} {flowdata.time} {flowdata.length}");
                }
            }
        }
        //Выводит основную информацию из файла .mid
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
    }
}
