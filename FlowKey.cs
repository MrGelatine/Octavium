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
        //Номер клавишы на клавиатуре
        public byte pos { get; }
        //Время столкновения падающей клавишы
        public uint time { get; }
        //Длительность падающей клавишы
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

