using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using System.Drawing;

namespace MPC_instructor
{
    public partial class frmFileDownloader : Form
    {
        FileDownloader downloader;
        public frmFileDownloader(string Url,string SaveTo)
        {
            InitializeComponent();
            var folder = Path.GetDirectoryName(SaveTo);
            string file = Path.GetFileName(SaveTo);
            name_Label.Text = file;
            downloader = new FileDownloader(Url, folder, file);
            downloader.ProgressChanged += downloader_ProgressChanged;
            downloader.RunWorkerCompleted += downloader_RunWorkerCompleted;
           
            downloader.RunWorkerAsync();
        }

        void downloader_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            RefreshStatus();
            if (e.Cancelled) Close();
        }
        private bool processing;
        void downloader_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (processing) return;

            if (InvokeRequired) Invoke(new ProgressChangedEventHandler(downloader_ProgressChanged), sender, e);
            else
            {
                try
                {
                    processing = true;
                    progressBar1.Value = e.ProgressPercentage > 100 ? 100 : e.ProgressPercentage;
                    this.Text = "File Downloader - " + e.ProgressPercentage + " %";
                    string speed = String.Format(new FileSizeFormatProvider(), "{0:fs}", downloader.DownloadSpeed);
                    string ETA = downloader.ETA == 0 ? "" : "  [ " + FormatLeftTime.Format(((long)downloader.ETA) * 1000) + " ]";
                    status_Label.Text = speed + ETA;
                    RefreshStatus();
                }
                catch { }
                finally { processing = false; }
            }
        }

        private void stop_Button_Click(object sender, EventArgs e)
        {
            stop_Button.Enabled = false;
            pause_Button.Enabled = false;
            downloader.CancelAsync();
        }

        private void pause_Button_Click(object sender, EventArgs e)
        {
            if (downloader.DownloadStatus == DownloadStatus.Downloading)
                downloader.Pause();
            else if (downloader.DownloadStatus == DownloadStatus.Paused)
                downloader.Resume();
            RefreshStatus();
        }

        private void RefreshStatus()
        {
            pause_Button.Visible = stop_Button.Visible = downloader.DownloadStatus == DownloadStatus.Downloading || downloader.DownloadStatus == DownloadStatus.Paused;
            open_Button.Visible = downloader.DownloadStatus == DownloadStatus.Success;
            close_Button.Visible = downloader.DownloadStatus == DownloadStatus.Success || downloader.DownloadStatus == DownloadStatus.Failed;

            if (downloader.DownloadStatus == DownloadStatus.Success)
                status_Label.Text = "Completed";
          else  if (downloader.DownloadStatus == DownloadStatus.Downloading)
                pause_Button.Text = "Pause";
            else if (downloader.DownloadStatus == DownloadStatus.Paused)
            {
                pause_Button.Text = "Resume";
                status_Label.Text = "Paused";
            }

        }

        private void open_Button_Click(object sender, EventArgs e)
        {
            try { Process.Start(downloader.DestFolder + "\\" + downloader.DestFileName); }
            catch (Exception ex) { MessageBox.Show(this, ex.Message, ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void frmFileDownloader_FormClosing(object sender, FormClosingEventArgs e)
        {
            downloader.Abort();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawLine(new Pen(Color.Silver, 2), new Point(0, 1), new Point(panel1.Width, 1));
        }

        private void close_Button_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
