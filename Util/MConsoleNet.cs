using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Net.Sockets;
using WindowsFormsToolkit.Controls;
using System.Speech.Recognition;

namespace Utility
{
    public partial class MConsoleNet : Form
    {
        TcpListener tcp;
        int c = 0;

        SpeechRecognitionEngine sre=new SpeechRecognitionEngine();

        public MConsoleNet()
        {
            InitializeComponent();
        }

        private void MConsoleNet_Load(object sender, EventArgs e)
        {
            timer1.Stop();

            tcp = new TcpListener(IPAddress.Any, 8080);
            tcp.Start();

            CueTextExtender cue = new CueTextExtender();
            cue.SetCueText(textBox1, "Enter IP");

            Choices ch = new Choices();
            ch.Add(new string[] { "start", "close" });

            GrammarBuilder gb = new GrammarBuilder(ch);
            Grammar g = new Grammar(gb);

            sre.LoadGrammarAsync(g);
            sre.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(sre_SpeechRecognized);
            sre.SetInputToDefaultAudioDevice();
            sre.RecognizeAsync(RecognizeMode.Multiple);

        }

        void sre_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            try
            {
                if (e.Result.Text == "start")
                {
                    try
                    {
                        timer1.Start();
                        timer2.Start();
                        this.Hide();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error");
                    }
                }

                else if (e.Result.Text == "close")
                {
                    this.Close();
                    sre.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Failure");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                Bitmap bmp = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
                Graphics gr = Graphics.FromImage(bmp);
                gr.CopyFromScreen(0, 0, 0, 0, bmp.Size);

                bmp.Save(Environment.CurrentDirectory + @"\img.bmp", System.Drawing.Imaging.ImageFormat.Bmp);

                extract();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void client(string imgtext)
        {
            try
            {
                textBox1.ReadOnly = true;

                TcpClient tcpc = new TcpClient(textBox1.Text, 8080);

                NetworkStream ns = tcpc.GetStream();

                StreamWriter sw = new StreamWriter(ns);
                sw.WriteLine(imgtext);

                sw.Close();
                ns.Close();

                tcpc.Close();

                server();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
                textBox1.ReadOnly = false;
                textBox1.Focus();
            }
        }

        void server()
        {
            if (tcp.Pending())
            {
                Socket skt = tcp.AcceptSocket();

                NetworkStream ns = new NetworkStream(skt);

                StreamReader sr = new StreamReader(ns);

                string recimgtext = sr.ReadToEnd();

                byte[] bt = Convert.FromBase64String(recimgtext);
                MemoryStream ms = new MemoryStream(bt, 0, bt.Length);

                ms.Write(bt, 0, bt.Length);

                Image img = Image.FromStream(ms);
                
                string path = string.Format(Environment.CurrentDirectory+@"\NImages\img[{0}].bmp", c);
                img.Save(path, System.Drawing.Imaging.ImageFormat.Bmp);

                c++;

                sr.Close();
                ns.Close();
            }
        }

        void extract()
        {
            Bitmap bmp = new Bitmap(Environment.CurrentDirectory+@"\img.bmp");

            MemoryStream ms = new MemoryStream();

            bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);

            bmp.Dispose();

            byte[] b = ms.ToArray();

            string txt = Convert.ToBase64String(b);

            client(txt);
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            server();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
        }

        private void close(object sender, FormClosedEventArgs e)
        {
            this.Close();
            sre.Dispose();
        }
    }
}
