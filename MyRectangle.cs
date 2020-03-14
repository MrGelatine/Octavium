using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;

namespace WindowsFormsApp3
{
    class MyRectangle
    {

        private Rectangle myRec;
        private uint period;
        private bool check;
        private bool hit;
        private double speed;
        private double ypos;
        private double height;
        private int pos;
        private System.Drawing.SolidBrush color;
        public MyRectangle(Rectangle r, uint p, System.Drawing.SolidBrush c, int po)
        {
            myRec = r;
            period = p;
            check = false;
            hit = false;
            speed = 0.0;
            ypos = 0.0;
            height = period / 5;
            color = c;
            pos = po;
        }
        public int Position {
            get { return pos; }
        }
        public System.Drawing.SolidBrush Color {
            get { return color; }
        }
        public double Height {
            get { return height; }
        }
        public double YPos {
            get { return ypos; }
        }
        public void decreaseheight(double x) {
            height -= x;
        }
        public void increaseypos(double x) {
            ypos += x;
        }
        public uint Period
        {
            get { return period; }
            set { period = value; }
        }
        public double Speed {
            get { return speed; }
           
        }
        public void increasespeed(double x) {
            speed += x;
        }
        public bool Check {
            get { return check; }
            set { check = value; }
        }
        public bool Hit
        {
            get { return hit; }
            set { hit = value; }
        }
        public Rectangle MyRec {
            get { return myRec; }
            set { myRec = value; }
        }
        public void IncreaseHeight(int x) {
            myRec.Height = x;
        }
        public void Move(int x) {
            myRec.Y = x;
        }
        public void Decrease(int x) {
            myRec.Height = x;
        }
            

    }
       
}
