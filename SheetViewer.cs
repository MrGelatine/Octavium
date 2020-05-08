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
        public SheetViewer(string sheet_path, string sheet_name)
        {
            if(!InterfaceFuncs.IsSheetGenerated(sheet_path,sheet_name))
            {
                throw new System.IO.FileNotFoundException("Наш генератор не смог справиться с мощью ваших нот!");
            }
            sheet_lists = new List<string>();
            foreach(var list in System.IO.Directory.GetFiles(sheet_path))
            {
                sheet_lists.Add(list);
            }
            sheet_lists.Sort();
            InitializeComponent();
            pictureBox1.BackgroundImage = Image.FromFile(sheet_lists.First());
        }

    }
}
