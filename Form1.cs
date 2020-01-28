using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Sanford.Multimedia.Midi;
using Sanford.Multimedia.Midi.UI;
using Toub.Sound.Midi;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.DirectX.DirectSound;
namespace WindowsFormsApp3
{
    public partial class Form1 : Form
    {
        
        int i = 0;
        int[,] array = {  { 8, 500 }, {10, 500 },{ 8, 500 },{ 13,500},{ 12, 1000}, { 8, 500 },  { 10, 500 }, { 8, 500 },{ 15,500},{ 13, 800 }, { 8, 500 }, { 20, 500 }, { 17, 500 }, { 13, 500 }, { 12, 500 }, { 10, 1000 },{ 18, 500 },{ 17, 500 },{ 13, 500 },{ 15, 500 },{ 13, 2000 } };

        private OutputDevice outDevice;

        private int outDeviceID = 0;

        private OutputDeviceDialog outDialog = new OutputDeviceDialog();

        public Form1()
        {
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
        



        private async void SoundMaker(int Note,int Period) {
            outDevice.Send(new ChannelMessage(ChannelCommand.NoteOn, 0, 20+ Note, 127));
            //await Task.Delay(Period);
            Thread.Sleep(Period);
            outDevice.Send(new ChannelMessage(ChannelCommand.NoteOff, 0, 20+ Note, 0));

        }
        private Rectangle MakeRectangle(int Note,int Height)
        {
            Rectangle r = new Rectangle();
            r.Width = button100.Width;
            r.X = (Note - 1) * button100.Width;
            r.Height = Height;
            r.Y = 0;
            return r;
        }
        private void b1_down(object sender, MouseEventArgs e)
        {
            outDevice.Send(new ChannelMessage(ChannelCommand.NoteOn, 0, 21, 127));
        }

        private void b1_up(object sender, MouseEventArgs e)
        {
            outDevice.Send(new ChannelMessage(ChannelCommand.NoteOff, 0, 21, 0));
        }

        private void b2_down(object sender, MouseEventArgs e)
        {
            outDevice.Send(new ChannelMessage(ChannelCommand.NoteOn, 0, 22, 127));
        }

        private void b2_up(object sender, MouseEventArgs e)
        {
            outDevice.Send(new ChannelMessage(ChannelCommand.NoteOff, 0, 22, 0));
        }
        private void b3_down(object sender, MouseEventArgs e)
        {
            outDevice.Send(new ChannelMessage(ChannelCommand.NoteOn, 0, 23, 127));
        }

        private void b3_up(object sender, MouseEventArgs e)
        {
            outDevice.Send(new ChannelMessage(ChannelCommand.NoteOff, 0, 23, 0));
        }

        private void b4_down(object sender, MouseEventArgs e)
        {
            outDevice.Send(new ChannelMessage(ChannelCommand.NoteOn, 0, 24, 127));
        }

        private void b4_up(object sender, MouseEventArgs e)
        {
            outDevice.Send(new ChannelMessage(ChannelCommand.NoteOff, 0, 24, 0));
        }
        private void b5_down(object sender, MouseEventArgs e)
        {
            outDevice.Send(new ChannelMessage(ChannelCommand.NoteOn, 0, 25, 127));
        }

        private void b5_up(object sender, MouseEventArgs e)
        {
            outDevice.Send(new ChannelMessage(ChannelCommand.NoteOff, 0, 25, 0));
        }
        private void b6_down(object sender, MouseEventArgs e)
        {
            outDevice.Send(new ChannelMessage(ChannelCommand.NoteOn, 0, 26, 127));
        }

        private void b6_up(object sender, MouseEventArgs e)
        {
            outDevice.Send(new ChannelMessage(ChannelCommand.NoteOff, 0, 26, 0));
        }
        private void b7_down(object sender, MouseEventArgs e)
        {
            outDevice.Send(new ChannelMessage(ChannelCommand.NoteOn, 0, 27, 127));
        }

        private void b7_up(object sender, MouseEventArgs e)
        {
            outDevice.Send(new ChannelMessage(ChannelCommand.NoteOff, 0, 27, 0));
        }
        private void b8_down(object sender, MouseEventArgs e)
        {
            outDevice.Send(new ChannelMessage(ChannelCommand.NoteOn, 0, 28, 127));
        }

        private void b8_up(object sender, MouseEventArgs e)
        {
            outDevice.Send(new ChannelMessage(ChannelCommand.NoteOff, 0, 28, 0));
        }
        private void b9_down(object sender, MouseEventArgs e)
        {
            outDevice.Send(new ChannelMessage(ChannelCommand.NoteOn, 0, 29, 127));
        }

        private void b9_up(object sender, MouseEventArgs e)
        {
            outDevice.Send(new ChannelMessage(ChannelCommand.NoteOff, 0, 29, 0));
        }

        private void b10_down(object sender, MouseEventArgs e)
        {
            outDevice.Send(new ChannelMessage(ChannelCommand.NoteOn, 0, 30, 127));
        }

        private void b10_up(object sender, MouseEventArgs e)
        {
            outDevice.Send(new ChannelMessage(ChannelCommand.NoteOff, 0, 30, 0));
        }
        private void b11_down(object sender, MouseEventArgs e)
        {
            outDevice.Send(new ChannelMessage(ChannelCommand.NoteOn, 0, 31, 127));
        }

        private void b11_up(object sender, MouseEventArgs e)
        {
            outDevice.Send(new ChannelMessage(ChannelCommand.NoteOff, 0, 31, 0));
        }

        private void b12_down(object sender, MouseEventArgs e)
        {
            outDevice.Send(new ChannelMessage(ChannelCommand.NoteOn, 0, 32, 127));
        }

        private void b12_up(object sender, MouseEventArgs e)
        {
            outDevice.Send(new ChannelMessage(ChannelCommand.NoteOff, 0, 32, 0));
        }
        private void b13_down(object sender, MouseEventArgs e)
        {
            outDevice.Send(new ChannelMessage(ChannelCommand.NoteOn, 0, 33, 127));
        }

        private void b13_up(object sender, MouseEventArgs e)
        {
            outDevice.Send(new ChannelMessage(ChannelCommand.NoteOff, 0, 33, 0));
        }
        private void b14_down(object sender, MouseEventArgs e)
        {
            outDevice.Send(new ChannelMessage(ChannelCommand.NoteOn, 0, 34, 127));
        }

        private void b14_up(object sender, MouseEventArgs e)
        {
            outDevice.Send(new ChannelMessage(ChannelCommand.NoteOff, 0, 34, 0));
        }
        private void b15_down(object sender, MouseEventArgs e)
        {
            outDevice.Send(new ChannelMessage(ChannelCommand.NoteOn, 0, 35, 127));
        }

        private void b15_up(object sender, MouseEventArgs e)
        {
            outDevice.Send(new ChannelMessage(ChannelCommand.NoteOff, 0, 35, 0));
        }
        private void b16_down(object sender, MouseEventArgs e)
        {
            outDevice.Send(new ChannelMessage(ChannelCommand.NoteOn, 0, 36, 127));
        }

        private void b16_up(object sender, MouseEventArgs e)
        {
            outDevice.Send(new ChannelMessage(ChannelCommand.NoteOff, 0, 36, 0));
        }

        private void b17_down(object sender, MouseEventArgs e)
        {
            outDevice.Send(new ChannelMessage(ChannelCommand.NoteOn, 0, 37, 127));
        }

        private void b17_up(object sender, MouseEventArgs e)
        {
            outDevice.Send(new ChannelMessage(ChannelCommand.NoteOff, 0, 37, 0));
        }

        private void b18_down(object sender, MouseEventArgs e)
        {
            outDevice.Send(new ChannelMessage(ChannelCommand.NoteOn, 0, 38, 127));
        }

        private void b18_up(object sender, MouseEventArgs e)
        {
            outDevice.Send(new ChannelMessage(ChannelCommand.NoteOff, 0, 38, 0));
        }
        private void b19_down(object sender, MouseEventArgs e)
        {
            outDevice.Send(new ChannelMessage(ChannelCommand.NoteOn, 0, 39, 127));
        }

        private void b19_up(object sender, MouseEventArgs e)
        {
            outDevice.Send(new ChannelMessage(ChannelCommand.NoteOff, 0, 39, 0));
        }

        private void b20_down(object sender, MouseEventArgs e)
        {
            outDevice.Send(new ChannelMessage(ChannelCommand.NoteOn, 0, 40, 127));
        }

        private void b20_up(object sender, MouseEventArgs e)
        {
            outDevice.Send(new ChannelMessage(ChannelCommand.NoteOff, 0, 40, 0));
        }
        private void b21_down(object sender, MouseEventArgs e)
        {
            outDevice.Send(new ChannelMessage(ChannelCommand.NoteOn, 0, 41, 127));
        }

        private void b21_up(object sender, MouseEventArgs e)
        {
            outDevice.Send(new ChannelMessage(ChannelCommand.NoteOff, 0, 41, 0));
        }
        private void b22_down(object sender, MouseEventArgs e)
        {
            outDevice.Send(new ChannelMessage(ChannelCommand.NoteOn, 0, 42, 127));
        }

        private void b22_up(object sender, MouseEventArgs e)
        {
            outDevice.Send(new ChannelMessage(ChannelCommand.NoteOff, 0, 42, 0));
        }
        private void b23_down(object sender, MouseEventArgs e)
        {
            outDevice.Send(new ChannelMessage(ChannelCommand.NoteOn, 0, 43, 127));
        }

        private void b23_up(object sender, MouseEventArgs e)
        {
            outDevice.Send(new ChannelMessage(ChannelCommand.NoteOff, 0, 43, 0));
        }
        private void b24_down(object sender, MouseEventArgs e)
        {
            outDevice.Send(new ChannelMessage(ChannelCommand.NoteOn, 0, 44, 127));
        }

        private void b24_up(object sender, MouseEventArgs e)
        {
            outDevice.Send(new ChannelMessage(ChannelCommand.NoteOff, 0,44, 0));
        }
        private void b25_down(object sender, MouseEventArgs e)
        {
            outDevice.Send(new ChannelMessage(ChannelCommand.NoteOn, 0, 45, 127));
        }

        private void b25_up(object sender, MouseEventArgs e)
        {
            outDevice.Send(new ChannelMessage(ChannelCommand.NoteOff, 0, 45, 0));
        }

        private void b26_down(object sender, MouseEventArgs e)
        {
            outDevice.Send(new ChannelMessage(ChannelCommand.NoteOn, 0, 46, 127));
        }

        private void b26_up(object sender, MouseEventArgs e)
        {
            outDevice.Send(new ChannelMessage(ChannelCommand.NoteOff, 0, 46, 0));
        }
        private void b27_down(object sender, MouseEventArgs e)
        {
            outDevice.Send(new ChannelMessage(ChannelCommand.NoteOn, 0, 47, 127));
        }

        private void b27_up(object sender, MouseEventArgs e)
        {
            outDevice.Send(new ChannelMessage(ChannelCommand.NoteOff, 0, 47, 0));
        }

        private void b28_down(object sender, MouseEventArgs e)
        {
            outDevice.Send(new ChannelMessage(ChannelCommand.NoteOn, 0, 48, 127));
        }

        private void b28_up(object sender, MouseEventArgs e)
        {
            outDevice.Send(new ChannelMessage(ChannelCommand.NoteOff, 0, 48, 0));
        }
        private void b29_down(object sender, MouseEventArgs e)
        {
            outDevice.Send(new ChannelMessage(ChannelCommand.NoteOn, 0, 49, 127));
        }

        private void b29_up(object sender, MouseEventArgs e)
        {
            outDevice.Send(new ChannelMessage(ChannelCommand.NoteOff, 0, 49, 0));
        }
        private void b30_down(object sender, MouseEventArgs e)
        {
            outDevice.Send(new ChannelMessage(ChannelCommand.NoteOn, 0, 50, 127));
        }

        private void b30_up(object sender, MouseEventArgs e)
        {
            outDevice.Send(new ChannelMessage(ChannelCommand.NoteOff, 0, 50, 0));
        }
        private void b31_down(object sender, MouseEventArgs e)
        {
            outDevice.Send(new ChannelMessage(ChannelCommand.NoteOn, 0, 51, 127));
        }

        private void b31_up(object sender, MouseEventArgs e)
        {
            outDevice.Send(new ChannelMessage(ChannelCommand.NoteOff, 0, 51, 0));
        }
        private void b32_down(object sender, MouseEventArgs e)
        {
            outDevice.Send(new ChannelMessage(ChannelCommand.NoteOn, 0, 52, 127));
        }

        private void b32_up(object sender, MouseEventArgs e)
        {
            outDevice.Send(new ChannelMessage(ChannelCommand.NoteOff, 0, 52, 0));
        }

       /* private void timer1_Tick(object sender, EventArgs e)
        {
            if (i< array.Length/2)
            {
                SoundMaker(array[i, 0], array[i, 1]);
                i++;
            }
            Thread.Sleep(array[i - 1, 1]*2);
            
        }*/

        private void Form1_Load(object sender, EventArgs e)
        {
            //timer1.Start();
            for(int l = 0; l < array.Length; l++)
            {
                SoundMaker(array[l, 0], array[l, 1]);
            }
        }

        
    }
}