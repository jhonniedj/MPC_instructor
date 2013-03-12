namespace MPC_instructor
{
    partial class frmFileDownloader
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
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.stop_Button = new System.Windows.Forms.Button();
            this.pause_Button = new System.Windows.Forms.Button();
            this.open_Button = new System.Windows.Forms.Button();
            this.status_Label = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.close_Button = new System.Windows.Forms.Button();
            this.name_Label = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar1.Location = new System.Drawing.Point(12, 68);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(348, 18);
            this.progressBar1.TabIndex = 0;
            // 
            // stop_Button
            // 
            this.stop_Button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.stop_Button.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.stop_Button.Location = new System.Drawing.Point(285, 13);
            this.stop_Button.Name = "stop_Button";
            this.stop_Button.Size = new System.Drawing.Size(75, 23);
            this.stop_Button.TabIndex = 1;
            this.stop_Button.Text = "Stop";
            this.stop_Button.UseVisualStyleBackColor = true;
            this.stop_Button.Click += new System.EventHandler(this.stop_Button_Click);
            // 
            // pause_Button
            // 
            this.pause_Button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pause_Button.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.pause_Button.Location = new System.Drawing.Point(204, 13);
            this.pause_Button.Name = "pause_Button";
            this.pause_Button.Size = new System.Drawing.Size(75, 23);
            this.pause_Button.TabIndex = 1;
            this.pause_Button.Text = "Pause";
            this.pause_Button.UseVisualStyleBackColor = true;
            this.pause_Button.Click += new System.EventHandler(this.pause_Button_Click);
            // 
            // open_Button
            // 
            this.open_Button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.open_Button.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.open_Button.Location = new System.Drawing.Point(204, 13);
            this.open_Button.Name = "open_Button";
            this.open_Button.Size = new System.Drawing.Size(75, 23);
            this.open_Button.TabIndex = 2;
            this.open_Button.Text = "Open";
            this.open_Button.UseVisualStyleBackColor = true;
            this.open_Button.Visible = false;
            this.open_Button.Click += new System.EventHandler(this.open_Button_Click);
            // 
            // status_Label
            // 
            this.status_Label.AutoEllipsis = true;
            this.status_Label.Location = new System.Drawing.Point(60, 37);
            this.status_Label.Name = "status_Label";
            this.status_Label.Size = new System.Drawing.Size(297, 13);
            this.status_Label.TabIndex = 3;
            this.status_Label.Text = "{Status}";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Gainsboro;
            this.panel1.Controls.Add(this.stop_Button);
            this.panel1.Controls.Add(this.close_Button);
            this.panel1.Controls.Add(this.pause_Button);
            this.panel1.Controls.Add(this.open_Button);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 101);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 9, 3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(372, 48);
            this.panel1.TabIndex = 4;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // close_Button
            // 
            this.close_Button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.close_Button.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.close_Button.Location = new System.Drawing.Point(285, 13);
            this.close_Button.Name = "close_Button";
            this.close_Button.Size = new System.Drawing.Size(75, 23);
            this.close_Button.TabIndex = 2;
            this.close_Button.Text = "Close";
            this.close_Button.UseVisualStyleBackColor = true;
            this.close_Button.Visible = false;
            this.close_Button.Click += new System.EventHandler(this.close_Button_Click);
            // 
            // name_Label
            // 
            this.name_Label.AutoEllipsis = true;
            this.name_Label.Location = new System.Drawing.Point(60, 18);
            this.name_Label.Name = "name_Label";
            this.name_Label.Size = new System.Drawing.Size(300, 13);
            this.name_Label.TabIndex = 3;
            this.name_Label.Text = "{File Name}";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.DimGray;
            this.label1.Location = new System.Drawing.Point(16, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Name:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.DimGray;
            this.label2.Location = new System.Drawing.Point(12, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Status:";
            // 
            // frmFileDownloader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(372, 149);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.name_Label);
            this.Controls.Add(this.status_Label);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "frmFileDownloader";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "File Downloader";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmFileDownloader_FormClosing);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button stop_Button;
        private System.Windows.Forms.Button pause_Button;
        private System.Windows.Forms.Button open_Button;
        private System.Windows.Forms.Label status_Label;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label name_Label;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button close_Button;
    }
}