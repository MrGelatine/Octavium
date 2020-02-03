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
        public byte pos { get; }
        //Time of press key
        public uint time { get; }
        //Time of pressing key
        public uint length { get; }
        public FlowKeyData(Melanchall.DryWetMidi.Interaction.Note note, TempoMap tempomap)
        {
            pos = MIDIFuncs.NoteGetPosOnKeyboard(note);
            time = MIDIFuncs.GetNoteTime(note, tempomap);
            length = MIDIFuncs.GetNoteLength(note, tempomap);
        }
        public FlowKeyData(byte _pos, uint _time, uint _length)
        {
            this.pos = _pos;
            this.time = _time;
            this.length = _length;
        }
    }
}

