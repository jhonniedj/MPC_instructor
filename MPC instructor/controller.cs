using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Threading;
using System.Globalization;
using System.Diagnostics;
using mshtml;

namespace MPC_instructor
{
    public partial class controller : Form
    {
        public controller()
        {
            InitializeComponent();
        }
        
        string referer;
        string last_url;
        int retry_counts = 5;
        string returnstring = "";
        public static string current_time = "";
        string[] videoUrls;
        //List<string> downVideoUrls = new List<string>();
        List<YouTubeVideoQuality> downVideoUrls = new List<YouTubeVideoQuality>();

        
        private void controller_Load(object sender, EventArgs e)
        {
            this.Size = new Size(406, 616); 
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (!webBrowser1.Focused)
            {
                if ((keyData == (Keys.OemMinus)) || (keyData == (Keys.Subtract)))
                {
                    vol_min_button(new object { }, new EventArgs { });
                }
                if ((keyData == (Keys.Oemplus)) || (keyData == (Keys.Add)))
                {
                    vol_plus_button(new object { }, new EventArgs { });
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            webBrowser1.Document.Body.MouseOver += new HtmlElementEventHandler(Body_MouseOver);

        }

        void Body_MouseOver(object sender, HtmlElementEventArgs e)
        {
            if (!webBrowser1.Focused)
            {
                webBrowser1.Document.Body.Focus();
                webBrowser1.Select();
            }
        }

        private void Play_Click(object sender, EventArgs e)
        {
            textBox1.AppendText(POST_CMD(887));
        }

        private void Pause_Click(object sender, EventArgs e)
        {
            textBox1.AppendText(POST_CMD(888));
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox2.Text = GET_CMD();
        }

        private void Browser_Click(object sender, EventArgs e)
        {
           if ((Program.browser_form != null) && (!Program.browser_form.IsDisposed))
            {
                Focus_browser();
            }
            else
            {
                Program.browser_form = new browser();
                Program.browser_form.Show();
                Program.browser_form.Location = new Point(Screen.PrimaryScreen.WorkingArea.Width - Program.browser_form.Width, 0);
            }
            /* 
             if ((Program.browser_form != null) && (!Program.browser_form.IsDisposed))
             {
                 //Focus_preview();
                 Program.browser_form.Close();
                 Browser_button.Font = new Font(Browser_button.Font, FontStyle.Regular);
             }
             else
             {
                 Program.browser_form = new browser();
                 Program.browser_form.Show();
                 Program.browser_form.Location = new Point(Screen.PrimaryScreen.WorkingArea.Width - Program.browser_form.Width, 0);
                 Browser_button.Font = new Font(Browser_button.Font, FontStyle.Strikeout);
                 if (backgroundWorker1.IsBusy != true)
                 {
                     backgroundWorker1.RunWorkerAsync();
                 }
             }
             */

        }




        private void Preview_Click(object sender, EventArgs e)
        {

            if ((Program.preview_form != null) && (!Program.preview_form.IsDisposed))
            {
                //Focus_preview();
                Program.preview_form.Close();
                previewToolStripMenuItem.Font = new Font(previewToolStripMenuItem.Font, FontStyle.Regular);
            }
            else
            {
                Program.preview_form = new preview();
                Program.preview_form.Show();
                previewToolStripMenuItem.Font = new Font(previewToolStripMenuItem.Font, FontStyle.Strikeout);
            }

            if (backgroundWorker1.IsBusy != true)
            {
                backgroundWorker1.RunWorkerAsync();
            }
            //else { bw.CancelAsync(); }

        }

        private delegate void controllerCallback();

        public void Focus_controller()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new controllerCallback(Focus_controller));
            }
            else
            {
                if (this.CanFocus) { this.Focus(); }
            }
        }

        private delegate void previewCallback();

        public void Focus_preview()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new previewCallback(Focus_preview));
            }
            else
            {
                if (Program.preview_form.CanFocus) { Program.preview_form.Focus(); }
            }
        }

        private delegate void browserCallback();

        public void Focus_browser()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new browserCallback(Focus_browser));
            }
            else
            {
                if ((Program.browser_form != null) && (!Program.browser_form.IsDisposed))
                {
                    if (Program.browser_form.Handle != (IntPtr)0)
                    {
                        if (Program.browser_form.CanFocus) { Program.browser_form.Focus(); }
                    }
                }
            }
        }




        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

            Thread.Sleep(100);
            Focus_controller();
            //relocate_browser();
        }



        public string POST_CMD(int cmd, string rest = "")
        {
            try
            {
                //Console.WriteLine ("d="+date+"&t="+time+"&v1="+watt_hours+"&v2="+watts+"&v5="+celcius+"&v6="+volts);
                // this is what we are sending
                string post_data = "wm_command=" + cmd.ToString() + rest;

                // this is where we will send it
                string uri = "http://" + Program.MPC_target + "/command.html";

                // create a request
                HttpWebRequest request = (HttpWebRequest)
                WebRequest.Create(uri); request.KeepAlive = false;
                request.ProtocolVersion = HttpVersion.Version10;
                request.Method = "POST";
                //request.Headers.Add("X-Pvoutput-Apikey", "24071b850fcf32699e66637f3d043e5762979894");
                //request.Headers.Add("X-Pvoutput-SystemId", "14730");


                // turn our request string into a byte stream
                byte[] postBytes = Encoding.ASCII.GetBytes(post_data);

                // this is important - make sure you specify type this way
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = postBytes.Length;
                Stream requestStream = request.GetRequestStream();

                // now send it
                requestStream.Write(postBytes, 0, postBytes.Length);
                requestStream.Close();

                // grab te response and print it out to the console along with the status code
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                //!Console.WriteLine(new StreamReader(response.GetResponseStream()).ReadToEnd());
                //!Console.WriteLine(response.StatusCode);
                timer_YT.Enabled = true;
                return "Post data:" + post_data + " " + response.StatusCode.ToString() + "\n";
            }
            catch (Exception)
            { timer_YT.Enabled = false; return "Can't Send! did you close Media Player?"; }
        }

        private string GET_CMD()
        {
            try
            {
                string sURL;
                returnstring = "";
                sURL = "http://" + Program.MPC_target + "/status.html";

                WebRequest wrGETURL;
                wrGETURL = WebRequest.Create(sURL);

                WebProxy myProxy = new WebProxy("myproxy", 80);
                myProxy.BypassProxyOnLocal = true;

                //wrGETURL.Proxy = WebProxy.GetDefaultProxy();

                Stream objStream;
                objStream = wrGETURL.GetResponse().GetResponseStream();

                StreamReader objReader = new StreamReader(objStream);


                string sLine = "";
                int i = 0;

                while (sLine != null)
                {
                    i++;
                    sLine = objReader.ReadLine();
                    if (sLine != null)
                        //!Console.WriteLine("{0}:{1}", i, sLine);
                        returnstring += sLine + "\n";
                }
                //Console.ReadLine();

                returnstring = returnstring.Replace("OnStatus(", "");
                returnstring = returnstring.Replace(")", "");
                returnstring = returnstring.Replace("\"", "");
                returnstring = returnstring.Replace(" ", "");
                string[] splitreturn = returnstring.Split(',');

                //current time
                textBox8.Text = splitreturn[3];
                //total time
                textBox10.Text = splitreturn[5];
                trackBar1.Value = Convert.ToInt16(splitreturn[7]);
                DateTime dt1 = DateTime.ParseExact(splitreturn[5], "HH:mm:ss", new DateTimeFormatInfo());
                DateTime dt2 = DateTime.ParseExact(splitreturn[3], "HH:mm:ss", new DateTimeFormatInfo());
                TimeSpan ts1 = dt1.Subtract(dt2);

                //elapsed time
                textBox9.Text = ts1.ToString();
                timer1.Enabled = true;
                return returnstring + " = [" + splitreturn[5] + "]";
            }
            catch (Exception) { timer1.Enabled = false; return "Timed-out! did you close Media Player?"; }
        }

        private void textBox3_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            POST_CMD(-1, "&position=" + textBox3.Text);
        }

        private void textBox4_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            POST_CMD(-1, "&position=" + textBox4.Text);
        }

        private void textBox5_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            POST_CMD(-1, "&position=" + textBox5.Text);
        }

        private void textBox6_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            POST_CMD(-1, "&position=" + textBox6.Text);
        }

        private void textBox7_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            POST_CMD(-1, "&position=" + textBox7.Text);
        }

        private void vol_plus_button(object sender, EventArgs e)
        {
            POST_CMD(907);
        }

        private void vol_min_button(object sender, EventArgs e)
        {
            POST_CMD(908);
        }


        private void textBox2_DoubleClick(object sender, EventArgs e)
        {
            MessageBox.Show(textBox2.Text);

        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            POST_CMD(-2, "&volume=" + trackBar1.Value.ToString());
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Program.MPC_target = textBox12.Text+":"+textBox13.Text;
            timer1.Enabled = true;
        }

        #region Youtube_Gedeelte
        private void YT_down_Button_Click(object sender, EventArgs e)
        {
            try
            {
                YouTubeVideoQuality tempItem = downVideoUrls[YT_quality_ComboBox.SelectedIndex];

                saveFileDialog1.Filter = String.Format("{0} Files|*.{1}", tempItem.Extention.ToUpper(), tempItem.Extention.ToLower());
                //saveFileDialog1.FileName = String.Format("{0}.{1}", tempItem.VideoTitle, tempItem.Extention.ToLower());
                saveFileDialog1.FileName = FormatTitle(tempItem.VideoTitle);

                if (DialogResult.OK != saveFileDialog1.ShowDialog(this)) return;
                new frmFileDownloader(tempItem.DownloadUrl, saveFileDialog1.FileName).Show(this);
            }
            catch (Exception ex) { MessageBox.Show(this, ex.Message, ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void backgroundWorker_YT_DoWork(object sender, DoWorkEventArgs e)
        {
            e.Result = YouTubeDownloader.GetYouTubeVideoUrls(videoUrls);
        }

        private void backgroundWorker_YT_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                UseWaitCursor = false;
                if (e.Error != null)
                    throw e.Error;

                List<YouTubeVideoQuality> urls = e.Result as List<YouTubeVideoQuality>;

                TimeSpan videoLength = TimeSpan.FromSeconds(urls[0].Length);
                if (videoLength.Hours > 0)
                { drawVideoLenght(String.Format("{0}:{1}:{2}", videoLength.Hours, videoLength.Minutes, videoLength.Seconds)); }
                else
                { drawVideoLenght(String.Format("{0}:{1}", videoLength.Minutes, videoLength.Seconds)); };

                foreach (var item in urls)
                {
                    string videoExtention = item.Extention;
                    string videoDimension = formatSize(item.Dimension);
                    string videoSize = formatSizeBinary(item.VideoSize);
                    //string videoTitle = item.VideoTitle.Replace(@"\", "").Replace("&#39;", "'").Replace("&quot;", "'").Replace("&lt;", "(").Replace("&gt;", ")").Replace("+", " ");

                    YT_quality_ComboBox.Items.Add(String.Format("{0} ({1}) - {2}", videoExtention.ToUpper(), videoDimension, videoSize));
                    YT_quality_ComboBox.Text = YT_quality_ComboBox.Items[0].ToString();
                    YT_quality_ComboBox.Enabled = true;
                    downVideoUrls.Add(item);
                    YT_name_Label2.Text = FormatTitle(item.VideoTitle);
                    YT_url_TextBox.Enabled = true;
                    timer_YT.Enabled = true;
                    YT_progressBar.Hide();
                    YT_waiter.Text = "";
                }
            }
            catch (Exception ex)
            {
                if (retry_counts >= 1)
                {
                    label1.Text = label1.Text + ".";
                    YT_get_Button_Click(sender, e);
                    retry_counts--;
                }
                else
                {
                    MessageBox.Show(this, ex.Message, ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //MessageBox.Show("error!" + ex.Message);
                    YT_paste_Button.Enabled = true;
                    YT_url_TextBox.Enabled = true;
                    timer_YT.Enabled = true;
                    YT_progressBar.Hide();
                }
            }
        }

        private void YT_copy_Button_Click(object sender, EventArgs e)
        {
            try
            {
                Clipboard.SetText(downVideoUrls[YT_quality_ComboBox.SelectedIndex].DownloadUrl);
                //MessageBox.Show(this, "URL copied to clipboard", "YouTube Downloader", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex) { MessageBox.Show(this, ex.Message, ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void YT_url_TextBox_TextChanged(object sender, EventArgs e)
        {
            YT_get_Button.Enabled = !String.IsNullOrEmpty(YT_url_TextBox.Text);

            YT_video_PictureBox.Image = null;
            downVideoUrls.Clear();
            YT_quality_ComboBox.Items.Clear();
            YT_quality_ComboBox.Enabled = false;
            YT_name_Label2.Text = "--";
        }



        private void YT_paste_Button_Click(object sender, EventArgs e)
        {
            YT_url_TextBox.Clear();
            YT_url_TextBox.Text = Clipboard.GetText().Trim();
            YT_get_Button_Click(sender, e);
        }

        private void YT_get_Button_Click(object sender, EventArgs e)
        {
            Go_clicker();
        }

        private void Go_clicker()
        {
            try
            {
                if (!Helper.isValidUrl(YT_url_TextBox.Text) || !YT_url_TextBox.Text.ToLower().Contains("youtube.com/watch?"))
                    MessageBox.Show(this, "You enter invalid YouTube URL, Please correct it.\r\n\nNote: URL should start with:\r\nhttp://www.youtube.com/watch?",
                        "Invalid URL", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else
                {
                    downVideoUrls.Clear();
                    YT_url_TextBox.Enabled = false;
                    YT_get_Button.Enabled = false;
                    timer_YT.Enabled = false;
                    YT_paste_Button.Enabled = false;
                    YT_progressBar.Show();

                    this.videoUrls = new string[] { YT_url_TextBox.Text };
                    YT_video_PictureBox.ImageLocation = string.Format("http://i3.ytimg.com/vi/{0}/default.jpg", Helper.GetVideoIDFromUrl(YT_url_TextBox.Text));

                    if (!backgroundWorker_YT.IsBusy)
                        backgroundWorker_YT.RunWorkerAsync();
                }
            }
            catch (Exception ex) { MessageBox.Show(this, ex.Message, ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Error); } 
        }

        private void YT_url_TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                e.Handled = true;
                YT_get_Button_Click(null, null);
            }
        }

        private void YT_quality_ComboBox_EnabledChanged(object sender, EventArgs e)
        {
            YT_down_Button.Enabled = YT_quality_ComboBox.Enabled;
            YT_copy_Button.Enabled = YT_quality_ComboBox.Enabled;
            YT_stream.Enabled = YT_quality_ComboBox.Enabled;
        }

        private void timer_YT_Tick(object sender, EventArgs e)
        {
            YT_paste_Button.Enabled = !String.IsNullOrEmpty(Clipboard.GetText());
        }

        private void YT_url_Label_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.codeproject.com/Tips/323771/YouTube-Downloader-Using-Csharp-NET");
        }

        private void YT_open_Click(object sender, EventArgs e)
        {
            //Clipboard.SetText(downVideoUrls[YT_quality_ComboBox.SelectedIndex].DownloadUrl);
            string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            desktop += @"\\tmptmp.mpcpl";
            string[] lines = { "MPCPLAYLIST", "1,type,0", "1,filename," + downVideoUrls[YT_quality_ComboBox.SelectedIndex].DownloadUrl };
            System.IO.File.WriteAllLines(desktop, lines);
            System.Diagnostics.Process.Start(desktop);
            System.Threading.Thread.Sleep(1000);
            System.IO.File.Delete(desktop);
        }

        private string formatSize(object value)
        {
            string s = ((Size)value).Height >= 720 ? " HD" : "";
            if (value is Size) return ((Size)value).Width + " x " + ((Size)value).Height + s;
            return "";
        }

        private string formatSizeBinary(Int64 size, Int32 decimals = 2)
        {
            string[] sizes = { "Bytes", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" };
            double formattedSize = size;
            Int32 sizeIndex = 0;
            while (formattedSize >= 1024 & sizeIndex < sizes.Length)
            {
                formattedSize /= 1024;
                sizeIndex += 1;
            }
            return string.Format("{0} {1}", Math.Round(formattedSize, decimals).ToString(), sizes[sizeIndex]);
        }

        public static string FormatTitle(string title)
        {
            return title.Replace(@"\", "").Replace("&#39;", "'").Replace("&quot;", "'").Replace("&lt;", "(").Replace("&gt;", ")").Replace("+", " ").Replace(":", "-");
        }

        private void drawVideoLenght(string lenght)
        {
            //video_PictureBox.Update();
            YT_video_PictureBox.Refresh();

            Graphics e = YT_video_PictureBox.CreateGraphics();
            Font mFont = new Font(this.Font.Name, 10.0F, FontStyle.Bold, GraphicsUnit.Point);
            SizeF mSize = e.MeasureString(lenght, mFont);
            Rectangle mRec = new Rectangle((int)(YT_video_PictureBox.Width - mSize.Width - 6),
                                           (int)(YT_video_PictureBox.Height - mSize.Height - 6),
                                           (int)(mSize.Width + 2),
                                           (int)(mSize.Height + 2));

            e.FillRectangle(new SolidBrush(Color.FromArgb(200, Color.Black)), mRec);
            e.DrawString(lenght, mFont, new SolidBrush(Color.Gainsboro), new PointF((YT_video_PictureBox.Width - mSize.Width - 5),
                                                                                (YT_video_PictureBox.Height - mSize.Height - 5)));
            e.Dispose();
        }


        #endregion

        private void button10_Click(object sender, EventArgs e)
        {
            //backgroundWorker_YT.CancelAsync();
            //YT_progressBar.Hide();
            //timer_YT.Dispose();
            //YT_url_TextBox.Enabled = true;
            //timer_YT.Enabled = true;
            //UseWaitCursor = false;
            button10.Hide();
            Application.Restart();
            Environment.Exit(0);
            Focus_controller();
            
        }

        private void url_Label_Click(object sender, EventArgs e)
        {
            Process.Start("https://www.youtube.com/");
        }

        private void url_Label_MouseHover(object sender, EventArgs e)
        {
            url_Label.BorderStyle = BorderStyle.FixedSingle;
        }

        private void url_Label_MouseLeave(object sender, EventArgs e)
        {
            url_Label.BorderStyle = BorderStyle.None;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            Process.Start("http://www.codeproject.com/Tips/323771/YouTube-Downloader-Using-Csharp-NET");
        }

        private void button13_Click(object sender, EventArgs e)
        {
            referer="JonaJona";
            if (textBox14.Text != "searchtag")
            {
                //webBrowser1.Url = new Uri("http://www.jonajona.nl/yt.php?compact=1&search=" + textBox14.Text);
                webBrowser1.Url = new Uri("https://www.youtube.com/results?search_query=" + textBox14.Text);
                webBrowser1.Focus();
                webBrowser1.Select();
            }
            else
            {
                textBox14.Text="";
                textBox14.Focus();
            }
        }



        private void webBrowser1_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {


            

            if (webBrowser1.Url != null)
            {
                //Console.WriteLine("hop... to :" + webBrowser1.Url.AbsoluteUri);
                if (webBrowser1.Url.AbsoluteUri.Contains("youtube.com")){rem_style();}

                if (webBrowser1.Url.AbsoluteUri != last_url)
                {
                    //Console.WriteLine("NAVIGATING... to :" + webBrowser1.Url.AbsoluteUri);
                    if (webBrowser1.Url.AbsoluteUri.Contains("youtube.com/watch?"))
                    {
                        if (webBrowser1.Url.AbsoluteUri.Contains('&'))
                        {
                            YT_url_TextBox.Text = webBrowser1.Url.AbsoluteUri.Substring(0, webBrowser1.Url.AbsoluteUri.IndexOf('&'));
                        }
                        else
                        {
                            YT_url_TextBox.Text = webBrowser1.Url.AbsoluteUri;
                        }
                        Go_clicker();
                        if (referer == "JonaJona")
                        {
                            webBrowser1.Url = new Uri("https://www.youtube.com/results?search_query=" + textBox14.Text);
                            //webBrowser1.Url = new Uri("http://www.jonajona.nl/yt.php?compact=1&search=" + textBox14.Text);
                        }
                        else
                        {
                            webBrowser1.Url = new Uri("http://www.youtube.com");
                        }
                        textBox13.Focus();
                       
                    }
                    last_url = webBrowser1.Url.AbsoluteUri;
                }
            }
            


            
        }

        private void textBox14_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                e.Handled = true;
                button13_Click(null, null);
            }
        }

        private void button19_Click(object sender, EventArgs e)
        {
            textBox3.Text = textBox8.Text;
        }

        private void button20_Click(object sender, EventArgs e)
        {
            textBox4.Text = textBox8.Text;
        }

        private void button21_Click(object sender, EventArgs e)
        {
            textBox5.Text = textBox8.Text;
        }

        private void button22_Click(object sender, EventArgs e)
        {
            textBox6.Text = textBox8.Text;
        }

        private void button23_Click(object sender, EventArgs e)
        {
            textBox7.Text = textBox8.Text;
        }

        private void button14_Click(object sender, EventArgs e)
        {
            textBox3_MouseDoubleClick(null, null);
        }

        private void button15_Click(object sender, EventArgs e)
        {
            textBox4_MouseDoubleClick(null, null);
        }

        private void button16_Click(object sender, EventArgs e)
        {
            textBox5_MouseDoubleClick(null, null);
        }

        private void button17_Click(object sender, EventArgs e)
        {
            textBox6_MouseDoubleClick(null, null);
        }

        private void button18_Click(object sender, EventArgs e)
        {
            textBox7_MouseDoubleClick(null, null);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            
            textBox2.Text = GET_CMD();
            if (textBox10.Text != "")
            {
                int total_seconds = (int)(Convert.ToUInt64(TimeSpan.Parse(textBox10.Text).TotalSeconds));
                trackBar2.Maximum = total_seconds;
            }
            if (textBox8.Text != "")
            {
                int current_seconds = (int)(Convert.ToUInt64(TimeSpan.Parse(textBox8.Text).TotalSeconds));
                trackBar2.Value = current_seconds;
            }
        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            desktop += @"\\tmptmp.mpcpl";
            string[] lines = { "MPCPLAYLIST"};
            System.IO.File.WriteAllLines(desktop, lines);
            System.Diagnostics.Process.Start(desktop);
            System.Threading.Thread.Sleep(1000);
            System.IO.File.Delete(desktop);
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            string tijd=TimeSpan.FromSeconds(trackBar2.Value).ToString();
            POST_CMD(-1, "&position=" + tijd);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            webBrowser1.Url = new Uri("https://www.youtube.com/");
            referer = "YT";
        }

        private void rem_style()
        {
            IHTMLDocument2 _htmlDocument = webBrowser1.Document.DomDocument as IHTMLDocument2;
            int length = _htmlDocument.styleSheets.length;
            IHTMLStyleSheet styleSheet = _htmlDocument.createStyleSheet(@"", length + 1);
            styleSheet.addRule("div#guide", "display:none;");
            styleSheet.addRule(".branded-page-v2-secondary-col", "display:none;");
            styleSheet.addRule(".branded-page-v2-top-row", "display:none;");
            styleSheet.addRule("div#content ", "margin-left:0px  !important;");
            styleSheet.addRule("div#header ", "display:none;");
            if (referer == "JonaJona")
            {
                styleSheet.addRule("div#yt-masthead-container ", "display:none;");
            }
            
        }

        private void button12_Click(object sender, EventArgs e)
        {
            Process[] prs = Process.GetProcesses();
            foreach (Process pr in prs)
            {
                if (pr.ProcessName.Contains("mpc-hc"))
                {
                    pr.Kill();
                }

            }
        }

        private void YouTube_Click(object sender, EventArgs e)
        {
            if (YouTubeToolStripMenuItem1.Font.Strikeout == false)
            {
                this.Size = new Size(1043, 770);
                YouTubeToolStripMenuItem1.Font = new Font(YouTubeToolStripMenuItem1.Font, FontStyle.Strikeout);
            }
            else
            {
                this.Size = new Size(406, 616);
                YouTubeToolStripMenuItem1.Font = new Font(YouTubeToolStripMenuItem1.Font, FontStyle.Regular);
            }
        }

        private void youTubeToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //for loading youtube on expanding screen/showing YouTube content
            /*
            if (webBrowser1.Url == null)
            {
                webBrowser1.Url = new Uri("https://www.youtube.com/");
                referer = "YT"; 
            }
             */
            YouTube_Click(sender, e);
        }

        private void previewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Preview_Click(sender, e);
        }

        private void browserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Browser_Click(sender, e);
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.jonajona.nl");
        }

        private void creditsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.codeproject.com/Tips/323771/YouTube-Downloader-Using-Csharp-NET");
        }

        private void readmeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Not yet implemented, \nbut you can send your suggestions\nor written instructions to:\ninfo@jonajona.nl","Under Construction..",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            desktop += @"\\tmptmp.mpcpl";
            string[] lines = { "MPCPLAYLIST" };
            System.IO.File.WriteAllLines(desktop, lines);
            System.Diagnostics.Process.Start(desktop);
            System.Threading.Thread.Sleep(1000);
            System.IO.File.Delete(desktop);
        }

        private void playToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.AppendText(POST_CMD(887));
        }

        private void pauseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.AppendText(POST_CMD(888));
        }



        private void button4_Click(object sender, EventArgs e)
        {
            string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            desktop += @"\\tmptmp.mpcpl";
            string[] lines = { "MPCPLAYLIST", "1,type,0", "1,filename,C:\\ww_black.jpg" };
            System.IO.File.WriteAllLines(desktop, lines);
            System.Diagnostics.Process.Start(desktop);
            System.Threading.Thread.Sleep(1000);
            System.IO.File.Delete(desktop);
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (textBox14.Focused) { textBox14.SelectAll(); }
            if (textBox12.Focused) { textBox12.SelectAll(); }
            if (textBox8.Focused) { textBox8.SelectAll(); }
            if (textBox9.Focused) { textBox9.SelectAll(); }
            if (textBox10.Focused) { textBox10.SelectAll(); }
            if (textBox3.Focused) { textBox3.SelectAll(); }
            if (textBox4.Focused) { textBox4.SelectAll(); }
            if (textBox5.Focused) { textBox5.SelectAll(); }
            if (textBox6.Focused) { textBox6.SelectAll(); }
            if (textBox7.Focused) { textBox7.SelectAll(); }
            if (YT_url_TextBox.Focused) { YT_url_TextBox.SelectAll(); }
        }

        private void textBox14_Click(object sender, EventArgs e)
        {
            if (textBox14.Text == "searchtag") { textBox14.Text = "";}
        }

        private void textBox14_TextChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            desktop += @"\\tmptmp.mpcpl";
            string[] lines = { "MPCPLAYLIST", "1,type,0", "1,filename,C:\\ww_black_square1.jpg" };
            System.IO.File.WriteAllLines(desktop, lines);
            System.Diagnostics.Process.Start(desktop);
            System.Threading.Thread.Sleep(1000);
            System.IO.File.Delete(desktop);
        }

        private void button24_Click(object sender, EventArgs e)
        {
            string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            desktop += @"\\tmptmp.mpcpl";
            string[] lines = { "MPCPLAYLIST", "1,type,0", "1,filename,C:\\ww_black_square2.jpg" };
            System.IO.File.WriteAllLines(desktop, lines);
            System.Diagnostics.Process.Start(desktop);
            System.Threading.Thread.Sleep(1000);
            System.IO.File.Delete(desktop);
        }





      





    }
}