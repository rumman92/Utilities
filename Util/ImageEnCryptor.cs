using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Speech.Recognition;

namespace Utility
{
    public partial class ImageEnCryptor : Form
    {
        SqlConnection conc;
        SqlDataAdapter da;

        SpeechRecognitionEngine sre = new SpeechRecognitionEngine();

        public ImageEnCryptor()
        {
            InitializeComponent();
        }

        private void saveKeyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void backToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void browse()
        {
            richTextBox1.Clear();

            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "Images(*.jpg);(*.jpeg);(*.png);(*.tiff);(*.bmp);(*.gif)|*.jpg;*.jpeg;*.png;*.tiff;*.bmp;*.gif";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    Bitmap bmp = new Bitmap(ofd.FileName);
                    pictureBox1.Image = bmp;
                    textBox1.Text = ofd.FileName;

                    MemoryStream ms = new MemoryStream();

                    bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);

                    byte[] b = ms.ToArray();

                    string txt = Convert.ToBase64String(b);

                    Random r = new Random();

                    richTextBox1.Text = txt;
                    textBox2.Text = r.Next().ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Faliure");
            }
        }

        private void encrypt()
        {
            Cursor.Current = Cursors.WaitCursor;
            if (textBox1.Text == "")
            {
                MessageBox.Show("No image found to crypt, please select an image");
            }
            else
            {
                try
                {
                    conc = new SqlConnection(@"Data Source=RUMMAN92;Initial Catalog=image_cryptor;Integrated Security=True");
                    conc.Open();

                    string str = textBox2.Text;

                    MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
                    byte[] data = System.Text.Encoding.ASCII.GetBytes(str);
                    data = md5.ComputeHash(data);
                    string text = System.Text.Encoding.ASCII.GetString(data);

                    char[] ch = richTextBox1.Text.ToCharArray();
                    Array.Reverse(ch);
                    string rev = new string(ch);

                    string query = string.Format("insert into image values ('{0}','{1}')", text, rev);
                    da = new SqlDataAdapter();
                    da.InsertCommand = new SqlCommand(query, conc);
                    da.InsertCommand.ExecuteNonQuery();

                    MessageBox.Show("Crypted !!", "Success");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Faliure");
                }
                finally
                {
                    conc.Close();
                }
            }
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Copy();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Paste();
        }

        private void ImageEnCryptor_Load(object sender, EventArgs e)
        {
            Choices ch = new Choices();
            ch.Add(new string[] { "encrypt", "browse", "save", "image", "text", "close" });

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
                if (e.Result.Text == "encrypt")
                {
                    encrypt();
                }

                else if (e.Result.Text == "browse")
                {
                    browse();
                }

                else if (e.Result.Text == "save")
                {
                    SaveFileDialog sfd = new SaveFileDialog();
                    sfd.Filter = "Key(*.ick)|*.ick";
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        File.WriteAllText(sfd.FileName, textBox2.Text);
                    }
                }

                else if (e.Result.Text == "text")
                {
                    tabControl2.SelectTab(tabPage4);
                }

                else if (e.Result.Text == "image")
                {
                    tabControl2.SelectTab(tabPage3);
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