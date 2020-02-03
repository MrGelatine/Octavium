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

namespace WindowsFormsApp3
{
    class MIDINotesData
    {
        public List<FlowKeyData> flowkeys;
        public byte count;
        public MIDINotesData(string path)
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
        public MIDINotesData(List<FlowKeyData> data)
        {
            flowkeys = data;
        }
    }
}
