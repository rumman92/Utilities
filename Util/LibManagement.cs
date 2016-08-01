using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Speech.Recognition;

namespace Utility
{
    public partial class LibManagement : Form
    {
        SpeechRecognitionEngine sre = new SpeechRecognitionEngine();

        public LibManagement()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            
        }

        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            
        }

        private void linkLabel6_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            
        }

        private void linkLabel7_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            
        }

        private void linkLabel8_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            
        }

        private void LibManagement_Load(object sender, EventArgs e)
        {
            Choices ch = new Choices();
            ch.Add(new string[] { "B.Arch", "BSC", "B.Tech", "M.Arch", "MBA", "MCA", "MSC", "M.Tech", "close" });

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
                if (e.Result.Text == "B.Arch")
                {
                    BArchDecider barchdecider = new BArchDecider();
                    barchdecider.Show();
                }

                else if (e.Result.Text == "BSC")
                {
                    BScDecider bscdecider = new BScDecider();
                    bscdecider.Show();
                }

                else if (e.Result.Text == "B.Tech")
                {
                    BTechDecider btechdecider = new BTechDecider();
                    btechdecider.Show();
                }
                else if (e.Result.Text == "M.Arch")
                {
                    MArchDecider marchdecider = new MArchDecider();
                    marchdecider.Show();
                }

                else if (e.Result.Text == "MBA")
                {
                    MBADecider mbadecider = new MBADecider();
                    mbadecider.Show();
                }

                else if (e.Result.Text == "MCA")
                {
                    MCADecider mcadecider = new MCADecider();
                    mcadecider.Show();
                }

                else if (e.Result.Text == "MSC")
                {
                    MScDecider mscdecider = new MScDecider();
                    mscdecider.Show();
                }

                else if (e.Result.Text == "M.Tech")
                {
                    MTechDecider mtechdecider = new MTechDecider();
                    mtechdecider.Show();
                }

                else if (e.Result.Text == "close" && ActiveForm.Name=="LibManagement")
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
