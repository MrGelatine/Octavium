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
    public class FlowKeyData
    {
        //The position of key on keyboard
        public int pos { get; }
        //Time of press key
        public int time { get; }
        //Time of pressing key
        public int length { get; }
        public FlowKeyData(Melanchall.DryWetMidi.Interaction.Note note, TempoMap tempomap)
        {
            pos = MIDIFuncs.NoteGetPosOnKeyboard(note);
            time = MIDIFuncs.GetNoteTime(note, tempomap);
            length = MIDIFuncs.GetNoteLength(note, tempomap);
        }
    }
}

