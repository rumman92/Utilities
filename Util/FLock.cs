using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Security.AccessControl;
using WindowsFormsToolkit.Controls;
using System.Speech.Recognition;

namespace Utility
{
    public partial class FLock : Form
    {
        SpeechRecognitionEngine sre = new SpeechRecognitionEngine();

        string clsid = ".{2559A1F2-21D7-11D4-BDAF-00C04F60B9F0}";

        string mpath = null;

        public FLock()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void islock()
        {
            if (textBox1.Text == string.Empty || textBox2.Text == string.Empty || textBox3.Text == string.Empty)
            {
                MessageBox.Show("Enter value of fields", "Error");
            }

            else if (!textBox2.Text.Equals(textBox3.Text))
            {
                MessageBox.Show("Password does not match", "Error");
            }

            else
            {
                if (!mpath.Contains(clsid))
                {
                    try
                    {
                        File.WriteAllText(textBox1.Text + "\\lock.txt", textBox2.Text);

                        DirectoryInfo di = new DirectoryInfo(textBox1.Text);
                        di.MoveTo(textBox1.Text + clsid);

                        textBox1.Clear();
                        textBox2.Clear();
                        textBox3.Clear();

                        MessageBox.Show("Directory Locked", "Locked");
                    }
                    catch (Exception ex)
                    {
                        File.Delete(textBox1.Text + "\\lock.txt");
                        MessageBox.Show(ex.Message, "Failure");
                    }
                }
                else
                    MessageBox.Show("Folder is already locked", "Error");
            }
        }

        private void unlock()
        {
            if (textBox1.Text == string.Empty || textBox2.Text == string.Empty || textBox3.Text == string.Empty)
            {
                MessageBox.Show("Enter value of fields", "Error");
            }

            else if (!textBox2.Text.Equals(textBox3.Text))
            {
                MessageBox.Show("Password does not match", "Error");
            }

            else
            {
                if (mpath.Contains(clsid))
                {

                    try
                    {
                        DirectoryInfo di = new DirectoryInfo(mpath);
                        di.MoveTo(mpath.Substring(0, mpath.LastIndexOf(".")));

                        string path = textBox1.Text + "\\lock.txt";
                        string cont = File.ReadAllText(path);

                        if (cont == textBox2.Text)
                        {
                            File.Delete(path);

                            textBox1.Clear();
                            textBox2.Clear();
                            textBox3.Clear();

                            MessageBox.Show("Directory UnLocked", "Unlocked");
                        }

                        else
                        {
                            DirectoryInfo dia = new DirectoryInfo(textBox1.Text);
                            dia.MoveTo(textBox1.Text + clsid);

                            MessageBox.Show("Folder Not Unlocked", "Error");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Failure");
                    }
                }
                else
                {
                    MessageBox.Show("Folder is not locked", "Error");
                }
            }
        }

        private void FLock_Load(object sender, EventArgs e)
        {
            CueTextExtender cue = new CueTextExtender();
            cue.SetCueText(textBox2, "Password");
            cue.SetCueText(textBox3, "Re-enter Password");

            Choices ch = new Choices();
            ch.Add(new string[] { "browse", "lock", "unlock", "close" });

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
                if (e.Result.Text == "browse")
                {
                    FolderBrowserDialog fbd = new FolderBrowserDialog();
                    if (fbd.ShowDialog() == DialogResult.OK)
                    {
                        if (fbd.SelectedPath.Contains(clsid))
                        {
                            textBox1.Text = fbd.SelectedPath.Substring(0, fbd.SelectedPath.LastIndexOf("."));
                            mpath = fbd.SelectedPath;
                        }

                        else
                        {
                            textBox1.Text = fbd.SelectedPath;
                            mpath = fbd.SelectedPath;
                        }
                    }
                }

                else if (e.Result.Text == "lock")
                {
                    islock();
                }

                else if (e.Result.Text == "unlock")
                {
                    unlock();
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
    }
}
