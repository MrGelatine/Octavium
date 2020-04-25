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
using Melanchall.DryWetMidi.Interaction;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.MusicTheory;
using Melanchall.DryWetMidi.Tools;
using Melanchall.DryWetMidi;

namespace WindowsFormsApp3
{
    class MIDINotesData
    {
        //Информация для каждой падающей клавишы
        public List<FlowKeyData> flowkeys;
        //Кол-во клавиш
        public uint count;
        public MIDINotesData(string path)
        {
            try
            {
                this.flowkeys = new List<FlowKeyData>();
                var midifile = MidiFile.Read(path);
                var tempomap = midifile.GetTempoMap();
                foreach (var chunk in midifile.Chunks)
                {
                    using (var notesManager = new NotesManager(((TrackChunk)chunk).Events))
                    {
                        foreach (var note in notesManager.Notes)
                        {
                            count++;
                            this.flowkeys.Add(new FlowKeyData(note, tempomap));
                        }
                    }
                }
            }
            catch(NotEnoughBytesException e)
            {
                throw new NotEnoughBytesException();
            }
        }
        public MIDINotesData(List<FlowKeyData> data)
        {
            flowkeys = data;
        }
    }
}
