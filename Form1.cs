using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Sanford.Multimedia.Midi;
using Sanford.Multimedia.Midi.UI;
using System.Threading;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.DirectX.DirectSound;
namespace WindowsFormsApp3
{

    public partial class Form1 : Form
    {
        public string filepath = "beethoven_fur_elise.mid";
        int ratio = 25;
        List<int> WhiteKey = new List<int> { 1,3,4,6,8,9,11,13,15,16,18,20,21,23,25,27,28,30,32,33,35,37,39,40,42,44,45,47,49,51,52,54,56,57,59,61,63,64,68,68,69,71,73,75,76,78,80,81,83,85,87,88};
        MIDINotesData M;
        int Button_Y_Position = 115;
        int Button_width = 20;
        Pen p = new Pen(Color.Black);
        System.Drawing.SolidBrush red = new System.Drawing.SolidBrush(Color.Red);
        System.Drawing.SolidBrush green = new System.Drawing.SolidBrush(Color.Green);
        System.Drawing.SolidBrush pink = new System.Drawing.SolidBrush(Color.Pink);
        System.Drawing.SolidBrush lightgreen = new System.Drawing.SolidBrush(Color.LightGreen);
        List<MyRectangle> RectangleList = new List<MyRectangle>();
        int time = 0; //Golbal Time 
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
            M = new MIDINotesData(filepath);
            base.OnLoad(e);
        }

        private void MakeRectangle(MIDINotesData M)
        {
               //Loop For Making Rectangles 
               foreach (var x in M.flowkeys)
                {
                    if ((x.time / timer1.Interval) == (time / timer1.Interval))
                    {
                        if (WhiteKey.Contains(x.pos))
                        {
                        Rectangle R = new Rectangle();
                        R.Y = 0;
                        R.X = ((WhiteKey.IndexOf(x.pos)) * Button_width) + 1;
                        R.Width = Button_width - 2;
                        R.Height = 0;
                        if (x.pos <= 44)
                        {
                            MyRectangle Rec = new MyRectangle(R, x.length, red, x.pos);
                            RectangleList.Add(Rec);
                        }
                        else
                        {                           
                            MyRectangle Rec = new MyRectangle(R, x.length, green, x.pos);
                            RectangleList.Add(Rec);
                        }               
                        }
                        else if(!WhiteKey.Contains(x.pos))
                        {
                        Rectangle R = new Rectangle();
                        R.Y = 0;
                        R.X = ((WhiteKey.IndexOf(x.pos - 1)) * Button_width) + 14;
                        R.Width = Button_width - 8;
                        R.Height = 0;
                        if (x.pos < 44)
                        {
                            MyRectangle Rec = new MyRectangle(R, x.length, pink, x.pos);
                            RectangleList.Add(Rec);
                        }
                        else
                        {
                            MyRectangle Rec = new MyRectangle(R, x.length, lightgreen, x.pos);
                            RectangleList.Add(Rec);
                        }                       
                        }
                    }
                }
               //Loop For Moving Rectangles and play notes
                 for (var i = 0; i < RectangleList.Count; i++)
                 {
                    if (RectangleList[i].MyRec.Height < (RectangleList[i].Period/ ratio) && RectangleList[i].Check == false)
                    {
                        RectangleList[i].increasespeed((((double)RectangleList[i].Period * timer1.Interval / (double)ratio) / ((double)RectangleList[i].Period)));
                        RectangleList[i].IncreaseHeight((int)RectangleList[i].Speed);//increase the Height
                    }

                    else RectangleList[i].Check = true;

                    if (RectangleList[i].MyRec.Y != Button_Y_Position && RectangleList[i].Check == true)
                    {
                        RectangleList[i].increaseypos((((double)RectangleList[i].Period * timer1.Interval / (double)ratio) / ((double)RectangleList[i].Period)));
                        RectangleList[i].Move((int)RectangleList[i].YPos);//Move Down
                        if (RectangleList[i].MyRec.Height + RectangleList[i].MyRec.Y == Button_Y_Position && RectangleList[i].Hit == false)
                        {
                            outDevice.Send(new ChannelMessage(ChannelCommand.NoteOn, 0, RectangleList[i].Position + 20, 127));//Playing Sound
                            RectangleList[i].Hit = true;
                        }
                        if (RectangleList[i].MyRec.Height + RectangleList[i].MyRec.Y >= Button_Y_Position && RectangleList[i].MyRec.Height > 0)
                        {
                            RectangleList[i].decreaseheight((((double)RectangleList[i].Period * timer1.Interval / (double)ratio) / ((double)RectangleList[i].Period)));
                            RectangleList[i].Decrease((int)RectangleList[i].Height);//Decrease the Height
                        }
                    }
                   if (RectangleList[i].MyRec.Y == Button_Y_Position && RectangleList[i].Check == true && RectangleList[i].Hit == true)
                    {
                    outDevice.Send(new ChannelMessage(ChannelCommand.NoteOff, 0, RectangleList[i].Position + 20, 0)); //Stop Sound
                    RectangleList[i].Hit = false;
                    }
            }
               
        }
        private void b1_down(object sender, MouseEventArgs e)
        {
            outDevice.Send(new ChannelMessage(ChannelCommand.NoteOn, 0, 17, 127));
        }

        private void b1_up(object sender, MouseEventArgs e)
        {
            outDevice.Send(new ChannelMessage(ChannelCommand.NoteOff, 0, 17, 0));
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
            outDevice.Send(new ChannelMessage(ChannelCommand.NoteOff, 0, 44, 0));
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

        private void timer1_Tick(object sender, EventArgs e)
        {

           MakeRectangle(M);
            time += timer1.Interval; //increase the time every 10 millisecond
            Invalidate();
         }

         private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            Graphics g = e.Graphics;
            foreach (var x in RectangleList)
            {
                
                g.DrawRectangle(p, x.MyRec);
                g.FillRectangle(x.Color, x.MyRec);
            }
        }

    }
}