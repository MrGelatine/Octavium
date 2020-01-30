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

        public Rectangle myRec;
        public int period;
        public bool check;
        public MyRectangle(Rectangle r, int p)
        {
            myRec = r;
            period = p;
            check = false;
        }
        public int Period
        {
            get { return period; }
            set { period = value; }
        }
        public bool Check {
            get { return check; }
            set { check = value; }
        }
        public Rectangle MyRec {
            get { return myRec; }
            set { myRec = value; }
        }
        public void IncreaseHeight(int x) {
            myRec.Height += x;
        }
        public void Move(int x) {
            myRec.Y += x;
        }
        public void Decrease(int x) {
            myRec.Height -= x;
        }
            

    }
       
}
