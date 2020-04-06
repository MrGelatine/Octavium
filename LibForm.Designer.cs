namespace WindowsFormsApp3
{
    partial class LibForm
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
            this.components = new System.ComponentModel.Container();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.labelSongName = new System.Windows.Forms.Label();
            this.playLabel = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.searchTextBox = new System.Windows.Forms.RichTextBox();
            this.addPanel = new System.Windows.Forms.Panel();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.panel7 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.flowLayoutPanel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.playLabel.SuspendLayout();
            this.panel3.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.flowLayoutPanel3.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel7.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.flowLayoutPanel1.Controls.Add(this.panel1);
            this.flowLayoutPanel1.Controls.Add(this.panel2);
            this.flowLayoutPanel1.Controls.Add(this.panel3);
            this.flowLayoutPanel1.Controls.Add(this.addPanel);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(1084, 72);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel1.BackgroundImage = global::WindowsFormsApp3.Properties.Resources.backBtn;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panel1.Location = new System.Drawing.Point(10, 10);
            this.panel1.Margin = new System.Windows.Forms.Padding(10);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(130, 52);
            this.panel1.TabIndex = 0;
            this.panel1.Click += new System.EventHandler(this.BackPictureBox_Click);
            // 
            // panel2
            // 
            this.panel2.AutoSize = true;
            this.panel2.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel2.BackgroundImage = global::WindowsFormsApp3.Properties.Resources.songNameBox;
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel2.Controls.Add(this.labelSongName);
            this.panel2.Controls.Add(this.playLabel);
            this.panel2.Location = new System.Drawing.Point(160, 10);
            this.panel2.Margin = new System.Windows.Forms.Padding(10);
            this.panel2.MaximumSize = new System.Drawing.Size(700, 52);
            this.panel2.MinimumSize = new System.Drawing.Size(500, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(500, 52);
            this.panel2.TabIndex = 1;
            // 
            // labelSongName
            // 
            this.labelSongName.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelSongName.AutoSize = true;
            this.labelSongName.BackColor = System.Drawing.Color.Transparent;
            this.labelSongName.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelSongName.ForeColor = System.Drawing.Color.White;
            this.labelSongName.Location = new System.Drawing.Point(58, -2);
            this.labelSongName.Margin = new System.Windows.Forms.Padding(20);
            this.labelSongName.MaximumSize = new System.Drawing.Size(0, 60);
            this.labelSongName.Name = "labelSongName";
            this.labelSongName.Padding = new System.Windows.Forms.Padding(0, 15, 20, 20);
            this.labelSongName.Size = new System.Drawing.Size(20, 59);
            this.labelSongName.TabIndex = 0;
            // 
            // playLabel
            // 
            this.playLabel.AutoSize = true;
            this.playLabel.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.playLabel.BackgroundImage = global::WindowsFormsApp3.Properties.Resources.icons8_play_96_1;
            this.playLabel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.playLabel.Controls.Add(this.label1);
            this.playLabel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.playLabel.Location = new System.Drawing.Point(18, 8);
            this.playLabel.Margin = new System.Windows.Forms.Padding(10);
            this.playLabel.MaximumSize = new System.Drawing.Size(50, 35);
            this.playLabel.Name = "playLabel";
            this.playLabel.Padding = new System.Windows.Forms.Padding(0, 12, 0, 0);
            this.playLabel.Size = new System.Drawing.Size(35, 35);
            this.playLabel.TabIndex = 2;
            this.playLabel.Click += new System.EventHandler(this.PlayLabel_Click);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(-233, -4);
            this.label1.Margin = new System.Windows.Forms.Padding(20);
            this.label1.MaximumSize = new System.Drawing.Size(0, 60);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(50, 15, 20, 20);
            this.label1.Size = new System.Drawing.Size(70, 59);
            this.label1.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel3.BackgroundImage = global::WindowsFormsApp3.Properties.Resources.search;
            this.panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel3.Controls.Add(this.searchTextBox);
            this.panel3.Location = new System.Drawing.Point(680, 10);
            this.panel3.Margin = new System.Windows.Forms.Padding(10);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(292, 52);
            this.panel3.TabIndex = 2;
            // 
            // searchTextBox
            // 
            this.searchTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.searchTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.searchTextBox.ForeColor = System.Drawing.Color.White;
            this.searchTextBox.Location = new System.Drawing.Point(43, 14);
            this.searchTextBox.Multiline = false;
            this.searchTextBox.Name = "searchTextBox";
            this.searchTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.searchTextBox.Size = new System.Drawing.Size(236, 24);
            this.searchTextBox.TabIndex = 0;
            this.searchTextBox.Text = "";
            this.searchTextBox.TextChanged += new System.EventHandler(this.SearchTextBox_TextChanged);
            // 
            // addPanel
            // 
            this.addPanel.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.addPanel.BackgroundImage = global::WindowsFormsApp3.Properties.Resources.closeIcon;
            this.addPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.addPanel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.addPanel.Location = new System.Drawing.Point(992, 10);
            this.addPanel.Margin = new System.Windows.Forms.Padding(10);
            this.addPanel.Name = "addPanel";
            this.addPanel.Size = new System.Drawing.Size(58, 52);
            this.addPanel.TabIndex = 3;
            this.addPanel.Click += new System.EventHandler(this.AddPanel_Click);
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.AutoSize = true;
            this.flowLayoutPanel2.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.flowLayoutPanel2.Controls.Add(this.flowLayoutPanel3);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(0, 72);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(1084, 389);
            this.flowLayoutPanel2.TabIndex = 1;
            // 
            // flowLayoutPanel3
            // 
            this.flowLayoutPanel3.AutoScroll = true;
            this.flowLayoutPanel3.Controls.Add(this.panel5);
            this.flowLayoutPanel3.Controls.Add(this.panel6);
            this.flowLayoutPanel3.Controls.Add(this.panel7);
            this.flowLayoutPanel3.Location = new System.Drawing.Point(25, 25);
            this.flowLayoutPanel3.Margin = new System.Windows.Forms.Padding(25);
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            this.flowLayoutPanel3.Size = new System.Drawing.Size(1038, 330);
            this.flowLayoutPanel3.TabIndex = 1;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.White;
            this.panel5.Controls.Add(this.label2);
            this.panel5.Location = new System.Drawing.Point(5, 5);
            this.panel5.Margin = new System.Windows.Forms.Padding(5);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(200, 43);
            this.panel5.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(25, 9);
            this.label2.Margin = new System.Windows.Forms.Padding(25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 24);
            this.label2.TabIndex = 3;
            this.label2.Text = "Дата";
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.White;
            this.panel6.Controls.Add(this.label3);
            this.panel6.Location = new System.Drawing.Point(215, 5);
            this.panel6.Margin = new System.Windows.Forms.Padding(5);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(589, 43);
            this.panel6.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(25, 9);
            this.label3.Margin = new System.Windows.Forms.Padding(25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(97, 24);
            this.label3.TabIndex = 4;
            this.label3.Text = "Название";
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.Color.White;
            this.panel7.Controls.Add(this.label4);
            this.panel7.Location = new System.Drawing.Point(814, 5);
            this.panel7.Margin = new System.Windows.Forms.Padding(5);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(198, 43);
            this.panel7.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(25, 9);
            this.label4.Margin = new System.Windows.Forms.Padding(25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 24);
            this.label4.TabIndex = 5;
            this.label4.Text = "Время";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // timer1
            // 
            this.timer1.Interval = 15;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // LibForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1084, 461);
            this.Controls.Add(this.flowLayoutPanel2);
            this.Controls.Add(this.flowLayoutPanel1);
            this.MaximumSize = new System.Drawing.Size(1100, 500);
            this.MinimumSize = new System.Drawing.Size(1100, 500);
            this.Name = "LibForm";
            this.Text = "LibForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LibForm_FormClosing);
            this.Load += new System.EventHandler(this.LibForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.LibForm_KeyDown);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.playLabel.ResumeLayout(false);
            this.playLabel.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel3.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel addPanel;
        private System.Windows.Forms.Label labelSongName;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.RichTextBox searchTextBox;
        private System.Windows.Forms.Panel playLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer timer1;
    }
}