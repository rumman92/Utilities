using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Speech.Recognition;

namespace Utility
{
    public partial class BTechDelBook : Form
    {
        SpeechRecognitionEngine sre = new SpeechRecognitionEngine();

        SqlConnection con;
        SqlDataAdapter da;

        public BTechDelBook()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void remove()
        {
            try
            {
                con = new SqlConnection(@"Data Source=RUMMAN92;Initial Catalog=BTech;Integrated Security=True");
                con.Open();

                string del = string.Format("delete from BTechDetails where [ID Number] ='{0}'", textBox1.Text);

                da = new SqlDataAdapter();
                da.DeleteCommand = new SqlCommand(del, con);
                da.DeleteCommand.ExecuteNonQuery();

                MessageBox.Show("Book Deleted", "Delete");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BTechDelBook_Load(object sender, EventArgs e)
        {
            Choices ch = new Choices();
            ch.Add(new string[] { "remove", "close" });

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
                if (e.Result.Text == "remove")
                {
                    remove();
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
