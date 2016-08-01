using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using System.Drawing.Drawing2D;
using System.Speech.Recognition;
using SpeechLib;

namespace Utility
{
    public partial class Util : Form
    {
        SpeechRecognitionEngine sre = new SpeechRecognitionEngine();
        SpVoice speak = new SpVoice();

        public Util()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                System.Drawing.Icon ic = new System.Drawing.Icon(Environment.CurrentDirectory + "\\Violet Light (ICO).ico");
                Util.ActiveForm.Icon = ic;

                pictureBox1.Hide();
                pictureBox2.Hide();
                pictureBox3.Hide();
                pictureBox4.Hide();
                pictureBox5.Hide();
                pictureBox6.Hide();
                pictureBox7.Hide();
                pictureBox8.Hide();
                pictureBox10.Hide();
                pictureBox11.Hide();
                pictureBox12.Hide();
                pictureBox13.Hide();

                groupBox1.MouseHover += new EventHandler(groupBox1_MouseHover);
                groupBox1.MouseLeave += new EventHandler(groupBox1_MouseLeave);

                groupBox2.MouseHover += new EventHandler(groupBox2_MouseHover);
                groupBox2.MouseLeave += new EventHandler(groupBox2_MouseLeave);

                groupBox3.MouseHover += new EventHandler(groupBox3_MouseHover);
                groupBox3.MouseLeave += new EventHandler(groupBox3_MouseLeave);

                groupBox4.MouseHover += new EventHandler(groupBox4_MouseHover);
                groupBox4.MouseLeave += new EventHandler(groupBox4_MouseLeave);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Faliure");
            }
        }

        void groupBox3_MouseLeave(object sender, EventArgs e)
        {
            linkLabel7.LinkColor = Color.White;
            linkLabel8.LinkColor = Color.White;
            linkLabel10.LinkColor = Color.White;
        }

        void groupBox3_MouseHover(object sender, EventArgs e)
        {
            linkLabel7.LinkColor = Color.Yellow;
            linkLabel8.LinkColor = Color.Yellow;
            linkLabel10.LinkColor = Color.Yellow;
        }

        void groupBox4_MouseLeave(object sender, EventArgs e)
        {
            linkLabel11.LinkColor = Color.White;
            linkLabel12.LinkColor = Color.White;
            linkLabel13.LinkColor = Color.White;
        }

        void groupBox4_MouseHover(object sender, EventArgs e)
        {
            linkLabel11.LinkColor = Color.Yellow;
            linkLabel12.LinkColor = Color.Yellow;
            linkLabel13.LinkColor = Color.Yellow;
        }

        void groupBox2_MouseLeave(object sender, EventArgs e)
        {
            linkLabel4.LinkColor = Color.White;
            linkLabel5.LinkColor = Color.White;
            linkLabel6.LinkColor = Color.White;
        }

        void groupBox2_MouseHover(object sender, EventArgs e)
        {
            linkLabel4.LinkColor = Color.Yellow;
            linkLabel5.LinkColor = Color.Yellow;
            linkLabel6.LinkColor = Color.Yellow;
        }

        void groupBox1_MouseLeave(object sender, EventArgs e)
        {

            linkLabel1.LinkColor = Color.White;
            linkLabel2.LinkColor = Color.White;
            linkLabel3.LinkColor = Color.White;
        }

        void groupBox1_MouseHover(object sender, EventArgs e)
        {
            linkLabel1.LinkColor = Color.Yellow;
            linkLabel2.LinkColor = Color.Yellow;
            linkLabel3.LinkColor = Color.Yellow;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            panel1.Dispose();
            checkBox1.Checked = true;

            linkLabel1.Enabled = true;
            linkLabel2.Enabled = true;
            linkLabel3.Enabled = true;
            linkLabel4.Enabled = true;
            linkLabel5.Enabled = true;
            linkLabel6.Enabled = true;
            linkLabel7.Enabled = true;
            linkLabel8.Enabled = true;
            linkLabel9.Enabled = true;
            linkLabel10.Enabled = true;
            linkLabel11.Enabled = true;
            linkLabel12.Enabled = true;
            linkLabel13.Enabled = true;

            timer1.Dispose();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            Choices ch = new Choices();
            ch.Add(new string[] { "mail sender", "translator", "web decider" });
            ch.Add(new string[] { "system information", "file manager", "narrator" });
            ch.Add(new string[] { "image cryptor", "data cryptor", "folder lock" });
            ch.Add(new string[] { "library management", "monitoring console", "chat" });
            ch.Add(new string[] { "list view", "image view", "exit", "aid" });
            ch.Add(new string[] { "facebook", "twitter", "developer" });
            ch.Add(new string[] { "extra", "terminate" });
            
            GrammarBuilder gb = new GrammarBuilder(ch);
            Grammar g = new Grammar(gb);

            if (checkBox1.Checked)
            {
                sre = new SpeechRecognitionEngine();
                sre.LoadGrammarAsync(g);
                sre.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(sre_SpeechRecognized);
                sre.SetInputToDefaultAudioDevice();
                sre.RecognizeAsync(RecognizeMode.Multiple);
            }
            else
            {
                sre.Dispose();
            }

        }

        void sre_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            try
            {
                //internet
                if (e.Result.Text == "mail sender")
                {
                    Mail m = new Mail();
                    m.Show();
                }

                else if (e.Result.Text == "translator")
                {
                    Translator trns = new Translator();
                    trns.Show();
                }

                else if (e.Result.Text == "web decider")
                {
                    WebDecider wd = new WebDecider();
                    wd.Show();
                }

                //system
                else if (e.Result.Text == "system information")
                {
                    SystemInfo sinfo = new SystemInfo();
                    sinfo.Show();
                }

                else if (e.Result.Text == "file manager")
                {
                    FileManager fm = new FileManager();
                    fm.Show();
                }

                else if (e.Result.Text == "narrator")
                {
                    Narrator nrtr = new Narrator();
                    nrtr.Show();
                }

                //cryptography
                else if (e.Result.Text == "image cryptor")
                {
                    IDecider de = new IDecider();
                    de.Show();
                }

                else if (e.Result.Text == "data cryptor")
                {
                    DDecider dd = new DDecider();
                    dd.Show();
                }

                else if (e.Result.Text == "folder lock")
                {
                    FLock fl = new FLock();
                    fl.Show();
                }

                //advanced
                else if (e.Result.Text == "library management")
                {
                    LibManagement lmgmt = new LibManagement();
                    lmgmt.Show();
                }

                else if (e.Result.Text == "monitoring console")
                {
                    MConDecider mcd = new MConDecider();
                    mcd.Show();
                }

                else if (e.Result.Text == "chat")
                {
                    Chat ch = new Chat();
                    ch.Show();
                }

                //view
                else if (e.Result.Text == "list view")
                {
                    linkLabel1.Show();
                    linkLabel2.Show();
                    linkLabel3.Show();
                    linkLabel4.Show();
                    linkLabel5.Show();
                    linkLabel6.Show();
                    linkLabel7.Show();
                    linkLabel8.Show();
                    linkLabel10.Show();
                    linkLabel11.Show();
                    linkLabel12.Show();
                    linkLabel13.Show();

                    pictureBox1.Hide();
                    pictureBox2.Hide();
                    pictureBox3.Hide();
                    pictureBox4.Hide();
                    pictureBox5.Hide();
                    pictureBox6.Hide();
                    pictureBox7.Hide();
                    pictureBox8.Hide();
                    pictureBox10.Hide();
                    pictureBox11.Hide();
                    pictureBox12.Hide();
                    pictureBox13.Hide();
                }

                else if (e.Result.Text == "image view")
                {
                    pictureBox1.Show();
                    pictureBox2.Show();
                    pictureBox3.Show();
                    pictureBox4.Show();
                    pictureBox5.Show();
                    pictureBox6.Show();
                    pictureBox7.Show();
                    pictureBox8.Show();
                    pictureBox10.Show();
                    pictureBox11.Show();
                    pictureBox12.Show();
                    pictureBox13.Show();

                    linkLabel1.Hide();
                    linkLabel2.Hide();
                    linkLabel3.Hide();
                    linkLabel4.Hide();
                    linkLabel5.Hide();
                    linkLabel6.Hide();
                    linkLabel7.Hide();
                    linkLabel8.Hide();
                    linkLabel10.Hide();
                    linkLabel11.Hide();
                    linkLabel12.Hide();
                    linkLabel13.Hide();
                }

                //ad
                else if (e.Result.Text == "facebook")
                {
                    System.Diagnostics.Process.Start("www.facebook.com/rumman92");
                }

                else if (e.Result.Text == "twitter")
                {
                    System.Diagnostics.Process.Start("www.twitter.com/rumman92");
                }

                else if (e.Result.Text == "developer")
                {
                    AboutDeveloper ad = new AboutDeveloper();
                    ad.ShowDialog();
                }

                //extra
                else if (e.Result.Text == "extra")
                {
                    Extra ex = new Extra();
                    ex.Show();
                }

                //terminate
                else if (e.Result.Text == "terminate")
                {
                    System.Diagnostics.Process[] prcs = System.Diagnostics.Process.GetProcesses();
                    foreach (System.Diagnostics.Process pr in prcs)
                    {
                        if (pr.ProcessName == "WINWORD")
                        {
                            pr.Kill();
                        }
                    }
                }

                //exit & aid
                else if (e.Result.Text == "exit")
                {
                    Application.Exit();
                }

                else if (e.Result.Text == "aid")
                {
                    System.Diagnostics.Process.Start(@"D:\Util\bin\Help\Help.docx");
                }
            }
            catch (Exception)
            {

            }
        }        

        private void mmh(object sender, EventArgs e)
        {
            linkLabel1.LinkColor = Color.Yellow;
            linkLabel2.LinkColor = Color.White;
            linkLabel3.LinkColor = Color.White;

            if (checkBox2.Checked)
            {
                speak.Speak("Send's mail over the internet.", SpeechVoiceSpeakFlags.SVSFlagsAsync);
            }
        }


        private void tmh(object sender, EventArgs e)
        {
            linkLabel1.LinkColor = Color.White;
            linkLabel2.LinkColor = Color.Yellow;
            linkLabel3.LinkColor = Color.White;

            if (checkBox2.Checked)
            {
                speak.Speak("Translate's text between different languages.", SpeechVoiceSpeakFlags.SVSFlagsAsync);
            }
        }


        private void wmh(object sender, EventArgs e)
        {
            linkLabel1.LinkColor = Color.White;
            linkLabel2.LinkColor = Color.White;
            linkLabel3.LinkColor = Color.Yellow;

            if (checkBox2.Checked)
            {
                speak.Speak("Surf web and scrap web pages.", SpeechVoiceSpeakFlags.SVSFlagsAsync);
            }
        }

        private void simh(object sender, EventArgs e)
        {
            linkLabel4.LinkColor = Color.Yellow;
            linkLabel5.LinkColor = Color.White;
            linkLabel6.LinkColor = Color.White;

            if (checkBox2.Checked)
            {
                speak.Speak("Gives the basic information about system and associated hard disks.", SpeechVoiceSpeakFlags.SVSFlagsAsync);
            }
        }

        private void nmh(object sender, EventArgs e)
        {
            linkLabel4.LinkColor = Color.White;
            linkLabel5.LinkColor = Color.Yellow;
            linkLabel6.LinkColor = Color.White;

            if (checkBox2.Checked)
            {
                speak.Speak("Narrates the given text.", SpeechVoiceSpeakFlags.SVSFlagsAsync);
            }
        }

        private void fmmh(object sender, EventArgs e)
        {
            linkLabel4.LinkColor = Color.White;
            linkLabel5.LinkColor = Color.White;
            linkLabel6.LinkColor = Color.Yellow;

            if (checkBox2.Checked)
            {
                speak.Speak("Manages the files on the system.", SpeechVoiceSpeakFlags.SVSFlagsAsync);
            }
        }

        private void icmh(object sender, EventArgs e)
        {
            linkLabel7.LinkColor = Color.Yellow;
            linkLabel8.LinkColor = Color.White;
            linkLabel10.LinkColor = Color.White;

            if (checkBox2.Checked)
            {
                speak.Speak("Encrypt and decrypt the images.", SpeechVoiceSpeakFlags.SVSFlagsAsync);
            }
        }

        private void dcmh(object sender, EventArgs e)
        {
            linkLabel7.LinkColor = Color.White;
            linkLabel8.LinkColor = Color.Yellow;
            linkLabel10.LinkColor = Color.White;

            if (checkBox2.Checked)
            {
                speak.Speak("Encrypt and decrypt the text data.", SpeechVoiceSpeakFlags.SVSFlagsAsync);
            }
        }

        private void flmh(object sender, EventArgs e)
        {
            linkLabel4.LinkColor = Color.White;
            linkLabel6.LinkColor = Color.White;
            linkLabel10.LinkColor = Color.Yellow;

            if (checkBox2.Checked)
            {
                speak.Speak("Locks the specified folder.", SpeechVoiceSpeakFlags.SVSFlagsAsync);
            }
        }

        private void lmmh(object sender, EventArgs e)
        {
            linkLabel11.LinkColor = Color.Yellow;
            linkLabel12.LinkColor = Color.White;
            linkLabel13.LinkColor = Color.White;

            if (checkBox2.Checked)
            {
                speak.Speak("Electronically manages the library.", SpeechVoiceSpeakFlags.SVSFlagsAsync);
            }
        }

        private void cmh(object sender, EventArgs e)
        {
            linkLabel11.LinkColor = Color.White;
            linkLabel12.LinkColor = Color.Yellow;
            linkLabel13.LinkColor = Color.White;

            if (checkBox2.Checked)
            {
                speak.Speak("Communicate with other user over LAN or internet.", SpeechVoiceSpeakFlags.SVSFlagsAsync);
            }
        }

        private void cmmh(object sender, EventArgs e)
        {
            linkLabel11.LinkColor = Color.White;
            linkLabel12.LinkColor = Color.White;
            linkLabel13.LinkColor = Color.Yellow;

            if (checkBox2.Checked)
            {
                speak.Speak("Allows user to monitor his system, either locally or over network.", SpeechVoiceSpeakFlags.SVSFlagsAsync);
            }
        }

        private void admh(object sender, EventArgs e)
        {
            linkLabel9.LinkColor = Color.Lime;
            Cursor.Current = Cursors.Hand;

            if (checkBox2.Checked)
            {
                speak.Speak("This project is developed by: Rumman Siddiqui", SpeechVoiceSpeakFlags.SVSFlagsAsync);
            }
        }

        private void adml(object sender, EventArgs e)
        {
            linkLabel9.LinkColor = Color.White;
            Cursor.Current = Cursors.Default;
        }
    }
}
