using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp3
{
    public partial class SheetViewer : Form
    {
        List<string> sheet_lists;
        int current;
        public SheetViewer(string sheet_path, string sheet_name)
        {
            if(!InterfaceFuncs.IsSheetGenerated(sheet_path,sheet_name))
            {
                throw new System.IO.FileNotFoundException("Наш генератор не смог справиться с мощью ваших нот!");
            }
            sheet_lists = new List<string>();
            foreach(var list in System.IO.Directory.GetFiles(sheet_path))
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(list,sheet_name))
                {
                    sheet_lists.Add(list);
                }
            }
            sheet_lists.Sort();
            InitializeComponent();
            current = 0;
            pictureBox1.BackgroundImage = Image.FromFile(sheet_lists[current]);
            pictureBox1.Height = Image.FromFile(sheet_lists[current]).Height;
            pictureBox1.Width = Image.FromFile(sheet_lists[current]).Width;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (current < sheet_lists.Count-1)
            {
                current++;
                pictureBox1.BackgroundImage = Image.FromFile(sheet_lists[current]);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (current > 0)
            {
                current--;
                pictureBox1.BackgroundImage = Image.FromFile(sheet_lists[current]);
            }
        }
    }
}
