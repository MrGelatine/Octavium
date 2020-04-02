using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sanford.Multimedia.Midi;
using Sanford.Multimedia.Midi.UI;
using System.Threading;
using System.Diagnostics;
using System.Windows.Forms;

namespace WindowsFormsApp3
{
    public partial class Play : Form
    {
        int time = 0;
        MIDINotesData My;
        bool pathcheck = false;
        private OutputDevice outDevice;
        private int outDeviceID = 0;
        private OutputDeviceDialog outDialog = new OutputDeviceDialog();
        public Play(String Path = "")
        {
            if (Path != "")
            {
                My = new MIDINotesData(MIDIFuncs.UnpackDataToNote(Path));
                pathcheck = true;
                if ((int)My.flowkeys[0].time > 1000)
                    time = (int)My.flowkeys[0].time - 1000;
            }
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            if (OutputDevice.DeviceCount == 0)
            {
                MessageBox.Show("No MIDI output devices available.", "Error!",
                    MessageBoxButtons.OK, MessageBoxIcon.Stop);

                Close();
            }
            else
            {
                try
                {
                    outDevice = new OutputDevice(outDeviceID);



                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error!",
                        MessageBoxButtons.OK, MessageBoxIcon.Stop);

                    Close();
                }
            }

            base.OnLoad(e);
        }
        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
        }

        protected override void OnClosed(EventArgs e)
        {
            timer1.Stop();
            time = 0;
            if (outDevice != null)
            {
                outDevice.Dispose();
            }

            outDialog.Dispose();

            base.OnClosed(e);
        }
        private void PlayingMid() {
            foreach (var x in My.flowkeys) {
                if ((x.time / (timer1.Interval) == (time / timer1.Interval))) {
                    outDevice.Send(new ChannelMessage(ChannelCommand.NoteOn, 0, x.pos+20, 127));
                }
                if ((x.time + x.length) / timer1.Interval == (time / timer1.Interval)) {
                    outDevice.Send(new ChannelMessage(ChannelCommand.NoteOff, 0, x.pos + 20, 0));
                }
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
          if(pathcheck)
              PlayingMid();
            time += timer1.Interval; //increase the time
            Invalidate();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Stop();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
