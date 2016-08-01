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
using System.Speech.Recognition;

namespace Utility
{
    public partial class Chat : Form
    {
        SpeechRecognitionEngine sre = new SpeechRecognitionEngine();
        TcpListener tcp;
        int c = 0;

        public Chat()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void Chat_Load(object sender, EventArgs e)
        {
            try
            {
                tcp = new TcpListener(IPAddress.Any, 8080);
                tcp.Start();

                IPHostEntry ip = System.Net.Dns.GetHostEntry(Dns.GetHostName());
                label4.Text = "Your IP: " + ip.AddressList[1].ToString();

                listBox3.Items.AddRange(ip.AddressList);

                if (File.Exists(Environment.CurrentDirectory + "\\IPs.txt"))
                {
                    string ips = File.ReadAllText(Environment.CurrentDirectory + "\\IPs.txt");
                    listBox2.Items.AddRange(ips.Split(' '));
                }

                Choices ch = new Choices();
                ch.Add(new string[] { "clear", "save", "send", "change", "close" });

                GrammarBuilder gb = new GrammarBuilder(ch);
                Grammar g = new Grammar(gb);

                sre.LoadGrammarAsync(g);
                sre.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(sre_SpeechRecognized);
                sre.SetInputToDefaultAudioDevice();
                sre.RecognizeAsync(RecognizeMode.Multiple);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Chat already opened\n" + ex.Message, "Error");
            }
        }

        void sre_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            try
            {
                if (e.Result.Text == "clear")
                {
                    listBox1.Items.Clear();
                    richTextBox2.Clear();
                }

                else if (e.Result.Text == "save")
                {
                    SaveFileDialog sfd = new SaveFileDialog();
                    sfd.Filter = "Text(*.txt)|*.txt";
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        File.WriteAllText(sfd.FileName, listBox1.Text);
                    }
                }

                else if (e.Result.Text == "change")
                {
                    textBox1.ReadOnly = false;
                }

                else if (e.Result.Text == "send")
                {
                    try
                    {
                        textBox1.ReadOnly = true;

                        TcpClient tcpc = new TcpClient(textBox1.Text, 8080);

                        NetworkStream ns = tcpc.GetStream();

                        StreamWriter sw = new StreamWriter(ns);
                        sw.WriteLine(richTextBox2.Text);

                        listBox1.Items.Add(System.Net.Dns.GetHostName() + ": " + richTextBox2.Text);
                        richTextBox2.Clear();

                        sw.Close();
                        ns.Close();

                        tcpc.Close();

                        if (c == 0)
                        {
                            File.AppendAllText(Environment.CurrentDirectory + "\\IPs.txt", textBox1.Text + " ");
                            c++;
                        }

                        server();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error");
                        textBox1.ReadOnly = false;
                        textBox1.Focus();
                    }
                }

                else if (e.Result.Text == "close")
                {
                    tcp.Stop();
                    this.Dispose();
                    sre.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Failure");
            }
        }            

        void server()
        {
            if (tcp.Pending())
            {
                Socket skt = tcp.AcceptSocket();

                NetworkStream ns = new NetworkStream(skt);

                StreamReader sr = new StreamReader(ns);

                IPHostEntry ip = Dns.GetHostEntry(textBox1.Text);
                listBox1.Items.Add(ip.HostName + ": " + sr.ReadToEnd());

                richTextBox2.Clear();

                sr.Close();
                ns.Close();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            server();
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Text = listBox2.SelectedItem.ToString();
        }

        private void close(object sender, FormClosedEventArgs e)
        {
            tcp.Stop();
            this.Dispose();
            sre.Dispose();
        }
    }
}
