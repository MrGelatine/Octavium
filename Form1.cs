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
namespace WindowsFormsApp3
{
    
    public partial class Form1 : Form
    {
        public enum Colors { Red, Orange, Yellow, Green, Blue, Indigo, Violet }
        class Rainbow
        {
            public Colors c_color;
            public Rainbow()
            {
                this.c_color = Colors.Red;
            }
            public Color GetNext()
            {
                switch (c_color)
                {
                    case Colors.Red:
                        {
                            c_color = Colors.Orange;
                            return Color.Orange;
                        }
                    case Colors.Orange:
                        {
                            c_color = Colors.Yellow;
                            return Color.Yellow;
                        }
                    case Colors.Yellow:
                        {
                            c_color = Colors.Green;
                            return Color.Green;
                        }
                    case Colors.Green:
                        {
                            c_color = Colors.Blue;
                            return Color.Blue;
                        }
                    case Colors.Blue:
                        {
                            c_color = Colors.Indigo;
                            return Color.Indigo;
                        }
                    case Colors.Indigo:
                        {
                            c_color = Colors.Violet;
                            return Color.Violet;
                        }
                    case Colors.Violet:
                        {
                            c_color = Colors.Red;
                            return Color.Red;
                        }
                }
                return Color.White;
            }
        }
        bool play = false;
        int ft;
        int st;
        Image im;
        List<Button> buttonlist = new List<Button>();
        Rainbow rainbow;
        double Myspeed=1.00 ;
        List<int> WhiteKey = new List<int> { 1, 3, 4, 6, 8, 9, 11, 13, 15, 16, 18, 20, 21, 23, 25, 27, 28, 30, 32, 33, 35, 37, 39, 40, 42, 44, 45, 47, 49, 51, 52, 54, 56, 57, 59, 61, 63, 64, 66, 68, 69, 71, 73, 75, 76, 78, 80, 81, 83, 85, 87, 88 };
        List<int> LeftBlackKey = new List<int> { 5, 10, 17, 22, 29, 34, 41, 46, 53, 58, 65, 70, 77, 82 };
        List<int> RightBlackKey = new List<int> { 2, 7, 14, 19, 26, 31, 38, 43, 50, 55, 62, 67, 74, 79, 86 };
        List<int> MiddleBlackKey = new List<int> { 12, 24, 36, 48, 60, 72, 84 };
        MIDINotesData My;
        int MyButton_Y_Position = 376;
        int MyButton_width = 20;
        bool pathcheck = false;
        Pen p = new Pen(Color.Black);
        System.Drawing.SolidBrush red;
        System.Drawing.SolidBrush green;
        System.Drawing.SolidBrush pink;
        System.Drawing.SolidBrush lightgreen;
        Color cl1;
        Color cl2;
        Color cl3;
        Color cl4;
        double oldspeed = 1.00;
        List<MyRectangle> MyRectangleList = new List<MyRectangle>();

        int time = 0; //Golbal Time 
        private OutputDevice outDevice;
        private int outDeviceID = 0;
        private OutputDeviceDialog outDialog = new OutputDeviceDialog();
        public Form1(String Path = "", double Speed = 1.00, String image ="", Color? c1 = null , Color? c2 = null, Color? c3 = null, Color? c4 = null,bool RainbowMode = false)
        {
            if (Path != "")
            {
                My = new MIDINotesData(MIDIFuncs.UnpackDataToNote(Path));
                pathcheck = true;
                ft = (int)My.flowkeys[0].time;
                st = (int)My.flowkeys[My.flowkeys.Count - 1].time + (int)My.flowkeys[My.flowkeys.Count - 1].length;
            }
            if (RainbowMode)
            {
                rainbow = new Rainbow();
            }
            red = new System.Drawing.SolidBrush(c1 ?? Color.Red);
            green = new System.Drawing.SolidBrush(c2 ?? Color.Green);
            pink = new System.Drawing.SolidBrush(c3 ?? Color.Pink);
            lightgreen = new System.Drawing.SolidBrush(c4 ?? Color.LightGreen);
            cl1 = c1 ?? Color.Red;
            cl2 = c2 ?? Color.Green;
            cl3 = c3 ?? Color.Pink;
            cl4 = c4 ?? Color.LightGreen;
            Myspeed = Speed;
            if (image != null)
            {
                //this.BackgroundImage = Image.FromFile(image);
                im = Image.FromFile(image);
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
            MyRectangleList.Clear();
            time = 0;
            for (int i = 1; i <= 88; i++)
            {
                if (WhiteKey.Contains(i))
                    buttonlist[i - 1].BackColor = Color.White;
                else buttonlist[i - 1].BackColor = Color.Black;
            }
            if (outDevice != null)
            {
                outDevice.Dispose();
            }

            outDialog.Dispose();

            base.OnClosed(e);
        }


        private void MakeRectangle(MIDINotesData M, List<MyRectangle> RectangleList, int Button_Y_Position, int Button_width, double speed)
        {
            //Loop For Making Rectangles 
            foreach (var x in M.flowkeys)
            {
                System.Drawing.SolidBrush r_color = rainbow == null ? red :new System.Drawing.SolidBrush(rainbow.GetNext());
                System.Drawing.SolidBrush l_color = rainbow == null ? pink : new System.Drawing.SolidBrush(rainbow.GetNext());
                if ((int)((x.time / (timer1.Interval * speed))) == time)
                {
                    if (WhiteKey.Contains(x.pos))//Check if Button is White
                    {
                        Rectangle R = new Rectangle();//Creating Rectangle
                        if (x.pos == 1)//First Button
                        {
                            R.Width = Button_width - 4;
                            R.X = 0;
                        }
                        else if (x.pos == 88)//Last Button
                        {
                            R.Width = Button_width;
                            R.X = 1020;
                        }
                        else
                        {
                            //Determine width and place of falling note
                            if (RightBlackKey.Contains(x.pos - 1))
                            {
                                R.X = ((WhiteKey.IndexOf(x.pos)) * Button_width) + 8;
                                R.Width = Button_width - 8;
                            }
                            else if (LeftBlackKey.Contains(x.pos - 1) && RightBlackKey.Contains(x.pos + 1))
                            {
                                R.X = ((WhiteKey.IndexOf(x.pos)) * Button_width) + 4;
                                R.Width = Button_width - 8;
                            }
                            else if (LeftBlackKey.Contains(x.pos - 1) && MiddleBlackKey.Contains(x.pos + 1))
                            {
                                R.X = ((WhiteKey.IndexOf(x.pos)) * Button_width) + 4;
                                R.Width = Button_width - 10;
                            }
                            else if (MiddleBlackKey.Contains(x.pos - 1))
                            {
                                R.X = ((WhiteKey.IndexOf(x.pos)) * Button_width) + 6;
                                R.Width = Button_width - 10;
                            }
                            else
                            {
                                R.X = WhiteKey.IndexOf(x.pos) * Button_width;
                                R.Width = Button_width - 9;
                            }
                        }
                        R.Y = 0;
                        R.Height = 0;
                        if (x.pos <= 44)//button is in the first half
                        {
                            if (x.length > 0)
                            {
                                MyRectangle Rec = new MyRectangle(R, x.length,r_color, x.pos);
                                RectangleList.Add(Rec);
                            }
                        }
                        else //button is in the second half
                        {
                            if (x.length > 0)
                            {
                                MyRectangle Rec = new MyRectangle(R, x.length,r_color, x.pos);
                                RectangleList.Add(Rec); 
                            }
                        }
                    }
                    else if (!WhiteKey.Contains(x.pos))//Button is black
                    {
                        Rectangle R = new Rectangle();
                        R.Y = 0;
                        //Determine the place of falling note
                        if (RightBlackKey.Contains(x.pos))
                            R.X = ((WhiteKey.IndexOf(x.pos - 1)) * Button_width) + 16;
                        else if (LeftBlackKey.Contains(x.pos))
                            R.X = ((WhiteKey.IndexOf(x.pos - 1)) * Button_width) + 12;
                        else
                            R.X = ((WhiteKey.IndexOf(x.pos - 1)) * Button_width) + 14;

                        R.Width = Button_width - 8;
                        R.Height = 0;
                        if (x.pos < 44)//button is in the first half
                        {
                            if (x.length > 0)
                            {
                                MyRectangle Rec = new MyRectangle(R, x.length, l_color, x.pos);
                                RectangleList.Add(Rec);
                            }
                        }
                        else //button is in the second half
                        {
                            if (x.length > 0)
                            {
                                MyRectangle Rec = new MyRectangle(R, x.length, l_color, x.pos);
                                RectangleList.Add(Rec);
                            }
                        }
                    }
                }
            }
            //Loop For Moving Rectangles and play notes
            for (var i = 0; i < RectangleList.Count; i++)
            {
                if (RectangleList[i].Period / 5 >= Button_Y_Position)
                {
                    if (RectangleList[i].MyRec.Height < (RectangleList[i].Period / 5) && RectangleList[i].HeightLeft > 0)
                    {
                        RectangleList[i].increasespeed((((double)RectangleList[i].Period * timer1.Interval * speed / (double)5) / ((double)RectangleList[i].Period)));
                        if (RectangleList[i].MyRec.Height < Button_Y_Position)
                            RectangleList[i].IncreaseHeight((int)RectangleList[i].Speed);
                        RectangleList[i].decreaseHeightLeft((((double)RectangleList[i].Period * timer1.Interval * speed / (double)5) / ((double)RectangleList[i].Period)));
                    }
                    else RectangleList[i].Check = true;
                    if (RectangleList[i].MyRec.Height + RectangleList[i].MyRec.Y >= Button_Y_Position && RectangleList[i].Hit == false)
                    {
                        outDevice.Send(new ChannelMessage(ChannelCommand.NoteOn, 0, RectangleList[i].Position + 20, 127));//playing note
                        if (!play)
                            play = true;
                        //change the color of button
                        if (WhiteKey.Contains(RectangleList[i].Position))
                        {
                            if (RectangleList[i].Position <= 44)
                                buttonlist[RectangleList[i].Position - 1].BackColor = RectangleList[i].Color.Color;
                            else
                                buttonlist[RectangleList[i].Position - 1].BackColor = RectangleList[i].Color.Color;
                        }
                        else
                        {
                            if (RectangleList[i].Position <= 44)
                                buttonlist[RectangleList[i].Position - 1].BackColor = RectangleList[i].Color.Color;
                            else
                                buttonlist[RectangleList[i].Position - 1].BackColor = RectangleList[i].Color.Color;
                        }
                        RectangleList[i].Hit = true;
                    }
                    if (RectangleList[i].Hit == true && RectangleList[i].MyRec.Height > 0)
                    {
                        if (RectangleList[i].Check == true)
                        {
                            RectangleList[i].decreaseheight((((double)RectangleList[i].Period * timer1.Interval * speed / (double)5) / ((double)RectangleList[i].Period)));
                            RectangleList[i].Decrease((int)RectangleList[i].Height);
                        }
                        else
                            RectangleList[i].Decrease(RectangleList[i].MyRec.Height);

                        if (RectangleList[i].Check == true)
                        {
                            RectangleList[i].increaseypos((((double)RectangleList[i].Period * timer1.Interval * speed / (double)5) / ((double)RectangleList[i].Period)));
                            RectangleList[i].Move((int)RectangleList[i].YPos);
                        }
                    }
                    if (RectangleList[i].MyRec.Y >= Button_Y_Position - (4+(6*Myspeed)) && RectangleList[i].Check == true && RectangleList[i].Hit == true&&RectangleList[i].ChangeButton==false)
                    {
                        if (WhiteKey.Contains(RectangleList[i].Position))
                            buttonlist[RectangleList[i].Position - 1].BackColor = Color.White;
                        else
                            buttonlist[RectangleList[i].Position - 1].BackColor = Color.Black;
                        outDevice.Send(new ChannelMessage(ChannelCommand.NoteOff, 0, RectangleList[i].Position + 20, 0));
                        RectangleList[i].ChangeButton = true;
                        RectangleList.RemoveAt(i);
                    }
        
                }
                else
                {
                    //Increasing the Height from 0 to the height of note
                    if (RectangleList[i].MyRec.Height < (RectangleList[i].Period / 5) && RectangleList[i].Check == false)
                    {
                        RectangleList[i].increasespeed((((double)RectangleList[i].Period * timer1.Interval * speed / (double)5) / ((double)RectangleList[i].Period)));
                        RectangleList[i].IncreaseHeight((int)RectangleList[i].Speed);

                    }

                    else RectangleList[i].Check = true;//Rectangle is in the full height

                    //Decreasing the Height when touching the button
                    if (RectangleList[i].MyRec.Height + RectangleList[i].MyRec.Y >= Button_Y_Position && RectangleList[i].MyRec.Height > 0)
                    {
                        RectangleList[i].decreaseheight((((double)RectangleList[i].Period * timer1.Interval * speed / (double)5) / ((double)RectangleList[i].Period)));
                        RectangleList[i].Decrease((int)RectangleList[i].Height);
                    }

                    //Moving Rectangle down
                    if (RectangleList[i].MyRec.Y <= Button_Y_Position && RectangleList[i].Check == true)
                    {

                        RectangleList[i].increaseypos((((double)RectangleList[i].Period * timer1.Interval * speed / (double)5) / ((double)RectangleList[i].Period)));
                        RectangleList[i].Move((int)RectangleList[i].YPos);

                        //Playing Note and changing the color of button
                        if (RectangleList[i].MyRec.Height + RectangleList[i].MyRec.Y >= Button_Y_Position && RectangleList[i].Hit == false)
                        {
                            outDevice.Send(new ChannelMessage(ChannelCommand.NoteOn, 0, RectangleList[i].Position + 20, 127));//playing note
                            if (!play)
                                play = true;
                            //change the color of button
                            if (WhiteKey.Contains(RectangleList[i].Position))
                            {
                                if (RectangleList[i].Position <= 44)
                                    buttonlist[RectangleList[i].Position - 1].BackColor = RectangleList[i].Color.Color;
                                else
                                    buttonlist[RectangleList[i].Position - 1].BackColor = RectangleList[i].Color.Color;
                            }
                            else
                            {
                                if (RectangleList[i].Position <= 44)
                                    buttonlist[RectangleList[i].Position - 1].BackColor = RectangleList[i].Color.Color;
                                else
                                    buttonlist[RectangleList[i].Position - 1].BackColor = RectangleList[i].Color.Color;
                            }
                            RectangleList[i].Hit = true;
                        }
                    }

                    //returning color of button to original color
                    if (RectangleList[i].MyRec.Y >= Button_Y_Position - 4 && RectangleList[i].Check == true && RectangleList[i].Hit == true && RectangleList[i].ChangeButton == false)
                    {
                        if (WhiteKey.Contains(RectangleList[i].Position))
                            buttonlist[RectangleList[i].Position - 1].BackColor = Color.White;
                        else
                            buttonlist[RectangleList[i].Position - 1].BackColor = Color.Black;
                        outDevice.Send(new ChannelMessage(ChannelCommand.NoteOff, 0, RectangleList[i].Position + 20, 0));
                        RectangleList[i].ChangeButton = true;
                        RectangleList.RemoveAt(i);
                    }

                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            pictureBox2.Refresh();
            if (pathcheck)
                MakeRectangle(My, MyRectangleList, MyButton_Y_Position, MyButton_width, Myspeed);
            time++; //increase the time
            if (play)
            {
                if (progressBar1.Value < progressBar1.Maximum)
                    progressBar1.Increment((int)(100 * Myspeed));
                //colorSlider2.Value += (int)(100 * Myspeed);
            }
            // progressBar1.Increment((int)(100*Myspeed));
            if (time >= (((My.flowkeys[My.flowkeys.Count - 1].time + My.flowkeys[My.flowkeys.Count - 1].length) / (timer1.Interval * Myspeed)) + (200 / Myspeed)))
                this.Close();
            Invalidate();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBox2.BackgroundImage = im;
            progressBar1.Maximum = 100 * (st - ft) / timer1.Interval;
            buttonlist.Add(button1);
            buttonlist.Add(button53);
            buttonlist.Add(button2);
            buttonlist.Add(button3);
            buttonlist.Add(button54);
            buttonlist.Add(button4);
            buttonlist.Add(button55);
            buttonlist.Add(button5);
            buttonlist.Add(button6);
            buttonlist.Add(button56);
            buttonlist.Add(button7);
            buttonlist.Add(button57);
            buttonlist.Add(button8);
            buttonlist.Add(button58);
            buttonlist.Add(button9);
            buttonlist.Add(button10);
            buttonlist.Add(button59);
            buttonlist.Add(button11);
            buttonlist.Add(button60);
            buttonlist.Add(button12);
            buttonlist.Add(button13);
            buttonlist.Add(button61);
            buttonlist.Add(button14);
            buttonlist.Add(button62);
            buttonlist.Add(button15);
            buttonlist.Add(button63);
            buttonlist.Add(button16);
            buttonlist.Add(button17);
            buttonlist.Add(button64);
            buttonlist.Add(button18);
            buttonlist.Add(button65);
            buttonlist.Add(button19);
            buttonlist.Add(button20);
            buttonlist.Add(button66);
            buttonlist.Add(button21);
            buttonlist.Add(button67);
            buttonlist.Add(button22);
            buttonlist.Add(button68);
            buttonlist.Add(button23);
            buttonlist.Add(button24);
            buttonlist.Add(button69);
            buttonlist.Add(button25);
            buttonlist.Add(button70);
            buttonlist.Add(button26);
            buttonlist.Add(button27);
            buttonlist.Add(button71);
            buttonlist.Add(button28);
            buttonlist.Add(button72);
            buttonlist.Add(button29);
            buttonlist.Add(button73);
            buttonlist.Add(button30);
            buttonlist.Add(button31);
            buttonlist.Add(button74);
            buttonlist.Add(button32);
            buttonlist.Add(button75);
            buttonlist.Add(button33);
            buttonlist.Add(button34);
            buttonlist.Add(button76);
            buttonlist.Add(button35);
            buttonlist.Add(button77);
            buttonlist.Add(button36);
            buttonlist.Add(button78);
            buttonlist.Add(button37);
            buttonlist.Add(button38);
            buttonlist.Add(button79);
            buttonlist.Add(button39);
            buttonlist.Add(button80);
            buttonlist.Add(button40);
            buttonlist.Add(button41);
            buttonlist.Add(button81);
            buttonlist.Add(button42);
            buttonlist.Add(button82);
            buttonlist.Add(button43);
            buttonlist.Add(button83);
            buttonlist.Add(button44);
            buttonlist.Add(button45);
            buttonlist.Add(button84);
            buttonlist.Add(button46);
            buttonlist.Add(button85);
            buttonlist.Add(button47);
            buttonlist.Add(button48);
            buttonlist.Add(button86);
            buttonlist.Add(button49);
            buttonlist.Add(button87);
            buttonlist.Add(button50);
            buttonlist.Add(button88);
            buttonlist.Add(button51);
            buttonlist.Add(button52);
            timer1.Start();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {

          /*  SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            Graphics g = e.Graphics;
            foreach (var x in MyRectangleList)
            {

                g.DrawRectangle(p, x.MyRec);
                g.FillRectangle(x.Color, x.MyRec);
            }
            g.DrawLine(p, 0, 30, 1058, 30);*/
        }
        private void Restart(object sender, EventArgs e)
        {
            MyRectangleList.Clear();
            time = 0;
            if ((int)My.flowkeys[0].time > 1000)
                time = (int)My.flowkeys[0].time - 1000;
        }

        private void Play(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void Pause(object sender, EventArgs e)
        {
            timer1.Stop();
        }

        private void button92_Click(object sender, EventArgs e)
        {
            if (Myspeed > 1.90 && Myspeed < 2.00)
            {
                Myspeed = 2.00;
                time = (int)(time * (oldspeed / Myspeed));
                oldspeed = Myspeed;
                colorSlider1.Value = 200;
                label1.Text = "Speed: " + (Myspeed * 100).ToString() + "%";
            }
            else if (Myspeed < 2.00)
            {
                Myspeed += 0.10;
                time = (int)(time * (oldspeed / Myspeed));
                oldspeed = Myspeed;
                label1.Text = "Speed: " + (Myspeed * 100).ToString() + "%";
                colorSlider1.Value += 10;
            }
        }

        private void button91_Click(object sender, EventArgs e)
        {
            if (Myspeed < 0.15 && Myspeed > 0.05)
            {
                Myspeed = 0.05;
                time = (int)(time * (oldspeed / Myspeed));
                oldspeed = Myspeed;
                colorSlider1.Value = 5;
                label1.Text = "Speed: " + (Myspeed * 100).ToString() + "%";
            }
            if (Myspeed >= 0.15)
            {
                Myspeed -= 0.10;
                time = (int)(time * (oldspeed / Myspeed));
                oldspeed = Myspeed;
                label1.Text = "Speed: " + (Myspeed * 100).ToString() + "%";
                colorSlider1.Value -= 10;
            }
        }

        private void pictureBox2_Paint(object sender, PaintEventArgs e)
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            Graphics g = e.Graphics;
            foreach (var x in MyRectangleList)
            {
                g.DrawRectangle(p, x.MyRec);
                g.FillRectangle(x.Color, x.MyRec);
            }
        }

        private void colorSlider1_Scroll_1(object sender, ScrollEventArgs e)
        {
            if ((int)colorSlider1.Value == 0)
            {

                Myspeed = 0.05;
                time = (int)(time * (oldspeed / Myspeed));
                oldspeed = Myspeed;
                label1.Text = "Speed: " + (Myspeed * 100).ToString() + "%";
            }
            else
            {
                int r = (int)colorSlider1.Value / 5;
                double b = (double)r * 5 / (double)100;
                Myspeed = b;
                time = (int)(time * (oldspeed / Myspeed));
                oldspeed = Myspeed;
                label1.Text = "Speed: " + (Myspeed * 100).ToString() + "%";
            }
        }
    }
}
