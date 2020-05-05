namespace WindowsFormsApp3
{
    partial class BackgroundLib
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.BackGroundListPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.BackGroundListPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // BackGroundListPanel
            // 
            this.BackGroundListPanel.AutoScroll = true;
            this.BackGroundListPanel.Controls.Add(this.panel5);
            this.BackGroundListPanel.Controls.Add(this.panel1);
            this.BackGroundListPanel.Controls.Add(this.panel2);
            this.BackGroundListPanel.Location = new System.Drawing.Point(11, 13);
            this.BackGroundListPanel.Margin = new System.Windows.Forms.Padding(25);
            this.BackGroundListPanel.Name = "BackGroundListPanel";
            this.BackGroundListPanel.Size = new System.Drawing.Size(772, 416);
            this.BackGroundListPanel.TabIndex = 2;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.White;
            this.panel5.Location = new System.Drawing.Point(5, 5);
            this.panel5.Margin = new System.Windows.Forms.Padding(5);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(215, 146);
            this.panel5.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Location = new System.Drawing.Point(230, 5);
            this.panel1.Margin = new System.Windows.Forms.Padding(5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(215, 146);
            this.panel1.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Location = new System.Drawing.Point(455, 5);
            this.panel2.Margin = new System.Windows.Forms.Padding(5);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(215, 146);
            this.panel2.TabIndex = 2;
            // 
            // BackgroundLib
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.BackGroundListPanel);
            this.Name = "BackgroundLib";
            this.Text = "BackgroundLib";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.BackgroundLib_FormClosing);
            this.BackGroundListPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel BackGroundListPanel;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
    }
}