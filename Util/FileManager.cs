using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Speech.Recognition;
namespace Utility
{
    public partial class FileManager : Form
    {
        DirectoryInfo di;
        SpeechRecognitionEngine sre = new SpeechRecognitionEngine();

        public FileManager()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void browse()
        {
            listBox1.Items.Clear();

            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = fbd.SelectedPath;

                di = new DirectoryInfo(textBox1.Text);

                //Images
                FileInfo[] fijpe = di.GetFiles("*.jpe");
                FileInfo[] fijpg = di.GetFiles("*.jpg");
                FileInfo[] fibmp = di.GetFiles("*.bmp");
                FileInfo[] fitif = di.GetFiles("*.tif");
                FileInfo[] fiimg = di.GetFiles("*.img");
                FileInfo[] figif = di.GetFiles("*.gif");
                FileInfo[] fiwmf = di.GetFiles("*.wmf");
                FileInfo[] fijfif = di.GetFiles("*.jfif");
                FileInfo[] fipng = di.GetFiles("*.png");
                FileInfo[] fiico = di.GetFiles("*.ico");
                FileInfo[] fidib = di.GetFiles("*.dib");
                FileInfo[] fijbig = di.GetFiles("*.jbig");

                //Documents
                FileInfo[] fipdf = di.GetFiles("*.pdf");
                FileInfo[] fidoc = di.GetFiles("*.doc");
                FileInfo[] fixml = di.GetFiles("*.xml");
                FileInfo[] fitxt = di.GetFiles("*.txt");
                FileInfo[] fippt = di.GetFiles("*.ppt");
                FileInfo[] fipps = di.GetFiles("*.pps");
                FileInfo[] firtf = di.GetFiles("*.rtf");
                FileInfo[] fixls = di.GetFiles("*.xls");
                FileInfo[] fichm = di.GetFiles("*.chm");
                FileInfo[] fitem = di.GetFiles("*.tem");
                FileInfo[] fipfw = di.GetFiles("*.pfw");
                FileInfo[] fiaccdb = di.GetFiles("*.accdb");

                //Midea
                FileInfo[] fivob = di.GetFiles("*.vob");
                FileInfo[] fivlc = di.GetFiles("*.vlc");
                FileInfo[] fimp3 = di.GetFiles("*.mp3");
                FileInfo[] fimp4 = di.GetFiles("*.mp4");
                FileInfo[] fidat = di.GetFiles("*.dat");
                FileInfo[] fi3gp = di.GetFiles("*.3gp");
                FileInfo[] fimkv = di.GetFiles("*.mkv");
                FileInfo[] fiwav = di.GetFiles("*.wav");
                FileInfo[] fiflv = di.GetFiles("*.flv");
                FileInfo[] fiavi = di.GetFiles("*.avi");
                FileInfo[] fimpeg = di.GetFiles("*.mpeg");
                FileInfo[] fiwmv = di.GetFiles("*.wmv");

                //Others
                FileInfo[] fiexe = di.GetFiles(".exe");
                FileInfo[] firar = di.GetFiles(".rar");
                FileInfo[] fizip = di.GetFiles(".zip");
                FileInfo[] fidll = di.GetFiles(".dll");
                FileInfo[] fiink = di.GetFiles(".ink");

                //Images list
                listBox1.Items.AddRange(fijpe);
                listBox2.Items.AddRange(fijpg);
                listBox3.Items.AddRange(fibmp);
                listBox4.Items.AddRange(fitif);
                listBox5.Items.AddRange(fiimg);
                listBox6.Items.AddRange(figif);
                listBox7.Items.AddRange(fiwmf);
                listBox8.Items.AddRange(fijfif);
                listBox9.Items.AddRange(fipng);
                listBox10.Items.AddRange(fiico);
                listBox11.Items.AddRange(fidib);
                listBox12.Items.AddRange(fijbig);

                //Documents list
                listBox13.Items.AddRange(fipdf);
                listBox14.Items.AddRange(fidoc);
                listBox15.Items.AddRange(fixml);
                listBox16.Items.AddRange(fitxt);
                listBox17.Items.AddRange(fippt);
                listBox18.Items.AddRange(fipps);
                listBox19.Items.AddRange(firtf);
                listBox20.Items.AddRange(fixls);
                listBox21.Items.AddRange(fichm);
                listBox22.Items.AddRange(fitem);
                listBox23.Items.AddRange(fipfw);
                listBox24.Items.AddRange(fiaccdb);

                //Misc list
                listBox25.Items.AddRange(fivob);
                listBox26.Items.AddRange(fivlc);
                listBox27.Items.AddRange(fimp3);
                listBox28.Items.AddRange(fimp4);
                listBox29.Items.AddRange(fidat);
                listBox30.Items.AddRange(fi3gp);
                listBox31.Items.AddRange(fimkv);
                listBox32.Items.AddRange(fiwav);
                listBox33.Items.AddRange(fiflv);
                listBox34.Items.AddRange(fiavi);
                listBox35.Items.AddRange(fimpeg);
                listBox36.Items.AddRange(fiwmv);

                //Others list
                listBox37.Items.AddRange(fiexe);
                listBox38.Items.AddRange(firar);
                listBox39.Items.AddRange(fizip);
                listBox40.Items.AddRange(fidll);
                listBox41.Items.AddRange(fiink);

                //Details
                listBox42.Items.Clear();
                listBox42.Items.AddRange(di.GetDirectories());
                label47.Text = listBox42.Items.Count.ToString();

                listBox43.Items.Clear();
                listBox43.Items.AddRange(di.GetFiles());
                label48.Text = listBox43.Items.Count.ToString();
            }

        }

        //Images
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.Items.Count != 0)
            {
                DirectoryInfo sd = di.CreateSubdirectory("Jpeg");

                try
                {
                    string snm = "\\" + listBox1.SelectedItem.ToString();

                    Directory.Move(string.Concat(textBox1.Text, snm), string.Concat(sd.FullName, snm));
                    listBox1.Items.Clear();
                    FileInfo[] fijpeg = di.GetFiles("*.jpe");
                    listBox1.Items.AddRange(fijpeg);

                    listBox43.Items.Clear();
                    listBox43.Items.AddRange(di.GetFiles());
                    label48.Text = listBox43.Items.Count.ToString();
                }
                catch (Exception)
                {

                }
            }
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox2.Items.Count != 0)
            {
                DirectoryInfo sd = di.CreateSubdirectory("Jpg");

                try
                {
                    string snm = "\\" + listBox2.SelectedItem.ToString();

                    Directory.Move(string.Concat(textBox1.Text, snm), string.Concat(sd.FullName, snm));
                    listBox2.Items.Clear();
                    FileInfo[] fijpg = di.GetFiles("*.jpg");
                    listBox2.Items.AddRange(fijpg);

                    listBox43.Items.Clear();
                    listBox43.Items.AddRange(di.GetFiles());
                    label48.Text = listBox43.Items.Count.ToString();
                }
                catch (Exception)
                {

                }
            }
        }

        private void listBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox3.Items.Count != 0)
            {
                DirectoryInfo sd = di.CreateSubdirectory("Bmp");

                try
                {
                    string snm = "\\" + listBox3.SelectedItem.ToString();

                    Directory.Move(string.Concat(textBox1.Text, snm), string.Concat(sd.FullName, snm));
                    listBox3.Items.Clear();
                    FileInfo[] fibmp = di.GetFiles("*.bmp");
                    listBox3.Items.AddRange(fibmp);

                    listBox43.Items.Clear();
                    listBox43.Items.AddRange(di.GetFiles());
                    label48.Text = listBox43.Items.Count.ToString();
                }
                catch (Exception)
                {

                }
            }
        }

        private void listBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox4.Items.Count != 0)
            {
                DirectoryInfo sd = di.CreateSubdirectory("Tiff");

                try
                {
                    string snm = "\\" + listBox4.SelectedItem.ToString();

                    Directory.Move(string.Concat(textBox1.Text, snm), string.Concat(sd.FullName, snm));
                    listBox4.Items.Clear();
                    FileInfo[] fitif = di.GetFiles("*.tif");
                    listBox4.Items.AddRange(fitif);

                    listBox43.Items.Clear();
                    listBox43.Items.AddRange(di.GetFiles());
                    label48.Text = listBox43.Items.Count.ToString();
                }
                catch (Exception)
                {

                }
            }
        }

        private void listBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox5.Items.Count != 0)
            {
                DirectoryInfo sd = di.CreateSubdirectory("Img");

                try
                {
                    string snm = "\\" + listBox5.SelectedItem.ToString();

                    Directory.Move(string.Concat(textBox1.Text, snm), string.Concat(sd.FullName, snm));
                    listBox5.Items.Clear();
                    FileInfo[] fiimg = di.GetFiles("*.img");
                    listBox5.Items.AddRange(fiimg);

                    listBox43.Items.Clear();
                    listBox43.Items.AddRange(di.GetFiles());
                    label48.Text = listBox43.Items.Count.ToString();
                }
                catch (Exception)
                {

                }
            }
        }

        private void listBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox6.Items.Count != 0)
            {
                DirectoryInfo sd = di.CreateSubdirectory("Gif");

                try
                {
                    string snm = "\\" + listBox6.SelectedItem.ToString();

                    Directory.Move(string.Concat(textBox1.Text, snm), string.Concat(sd.FullName, snm));
                    listBox6.Items.Clear();
                    FileInfo[] figif = di.GetFiles("*.gif");
                    listBox6.Items.AddRange(figif);

                    listBox43.Items.Clear();
                    listBox43.Items.AddRange(di.GetFiles());
                    label48.Text = listBox43.Items.Count.ToString();
                }
                catch (Exception)
                {

                }
            }
        }

        private void listBox7_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox7.Items.Count != 0)
            {
                DirectoryInfo sd = di.CreateSubdirectory("Wmf");

                try
                {
                    string snm = "\\" + listBox7.SelectedItem.ToString();

                    Directory.Move(string.Concat(textBox1.Text, snm), string.Concat(sd.FullName, snm));
                    listBox7.Items.Clear();
                    FileInfo[] fiwmf = di.GetFiles("*.wmf");
                    listBox7.Items.AddRange(fiwmf);

                    listBox43.Items.Clear();
                    listBox43.Items.AddRange(di.GetFiles());
                    label48.Text = listBox43.Items.Count.ToString();
                }
                catch (Exception)
                {

                }
            }
        }

        private void listBox8_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox8.Items.Count != 0)
            {
                DirectoryInfo sd = di.CreateSubdirectory("Jfif");

                try
                {
                    string snm = "\\" + listBox8.SelectedItem.ToString();

                    Directory.Move(string.Concat(textBox1.Text, snm), string.Concat(sd.FullName, snm));
                    listBox8.Items.Clear();
                    FileInfo[] fijfif = di.GetFiles("*.jfif");
                    listBox8.Items.AddRange(fijfif);

                    listBox43.Items.Clear();
                    listBox43.Items.AddRange(di.GetFiles());
                    label48.Text = listBox43.Items.Count.ToString();
                }
                catch (Exception)
                {

                }
            }
        }

        private void listBox9_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox9.Items.Count != 0)
            {
                DirectoryInfo sd = di.CreateSubdirectory("Png");

                try
                {
                    string snm = "\\" + listBox9.SelectedItem.ToString();

                    Directory.Move(string.Concat(textBox1.Text, snm), string.Concat(sd.FullName, snm));
                    listBox9.Items.Clear();
                    FileInfo[] fipng = di.GetFiles("*.png");
                    listBox9.Items.AddRange(fipng);

                    listBox43.Items.Clear();
                    listBox43.Items.AddRange(di.GetFiles());
                    label48.Text = listBox43.Items.Count.ToString();
                }
                catch (Exception)
                {

                }
            }
        }

        private void listBox10_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox10.Items.Count != 0)
            {
                DirectoryInfo sd = di.CreateSubdirectory("Ico");

                try
                {
                    string snm = "\\" + listBox10.SelectedItem.ToString();

                    Directory.Move(string.Concat(textBox1.Text, snm), string.Concat(sd.FullName, snm));
                    listBox10.Items.Clear();
                    FileInfo[] fiico = di.GetFiles("*.ico");
                    listBox10.Items.AddRange(fiico);

                    listBox43.Items.Clear();
                    listBox43.Items.AddRange(di.GetFiles());
                    label48.Text = listBox43.Items.Count.ToString();
                }
                catch (Exception)
                {

                }
            }
        }

        private void listBox11_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox11.Items.Count != 0)
            {
                DirectoryInfo sd = di.CreateSubdirectory("Dib");

                try
                {
                    string snm = "\\" + listBox11.SelectedItem.ToString();

                    Directory.Move(string.Concat(textBox1.Text, snm), string.Concat(sd.FullName, snm));
                    listBox11.Items.Clear();
                    FileInfo[] fidib = di.GetFiles("*.dib");
                    listBox11.Items.AddRange(fidib);

                    listBox43.Items.Clear();
                    listBox43.Items.AddRange(di.GetFiles());
                    label48.Text = listBox43.Items.Count.ToString();
                }
                catch (Exception)
                {

                }
            }
        }

        private void listBox12_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox12.Items.Count != 0)
            {
                DirectoryInfo sd = di.CreateSubdirectory("Jbig");

                try
                {
                    string snm = "\\" + listBox12.SelectedItem.ToString();

                    Directory.Move(string.Concat(textBox1.Text, snm), string.Concat(sd.FullName, snm));
                    listBox12.Items.Clear();
                    FileInfo[] fijbig = di.GetFiles("*.jbig");
                    listBox12.Items.AddRange(fijbig);

                    listBox43.Items.Clear();
                    listBox43.Items.AddRange(di.GetFiles());
                    label48.Text = listBox43.Items.Count.ToString();
                }
                catch (Exception)
                {

                }
            }
        }

        //Documents
        private void listBox13_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox13.Items.Count != 0)
            {
                DirectoryInfo sd = di.CreateSubdirectory("Pdf");

                try
                {
                    string snm = "\\" + listBox13.SelectedItem.ToString();

                    Directory.Move(string.Concat(textBox1.Text, snm), string.Concat(sd.FullName, snm));
                    listBox13.Items.Clear();
                    FileInfo[] fipdf = di.GetFiles("*.pdf");
                    listBox13.Items.AddRange(fipdf);

                    listBox43.Items.Clear();
                    listBox43.Items.AddRange(di.GetFiles());
                    label48.Text = listBox43.Items.Count.ToString();
                }
                catch (Exception)
                {

                }
            }
        }

        private void listBox14_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox14.Items.Count != 0)
            {
                DirectoryInfo sd = di.CreateSubdirectory("Doc");

                try
                {
                    string snm = "\\" + listBox14.SelectedItem.ToString();

                    Directory.Move(string.Concat(textBox1.Text, snm), string.Concat(sd.FullName, snm));
                    listBox14.Items.Clear();
                    FileInfo[] fidoc = di.GetFiles("*.doc");
                    listBox14.Items.AddRange(fidoc);

                    listBox43.Items.Clear();
                    listBox43.Items.AddRange(di.GetFiles());
                    label48.Text = listBox43.Items.Count.ToString();
                }
                catch (Exception)
                {

                }
            }
        }

        private void listBox15_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox15.Items.Count != 0)
            {
                DirectoryInfo sd = di.CreateSubdirectory("Xml");

                try
                {
                    string snm = "\\" + listBox15.SelectedItem.ToString();

                    Directory.Move(string.Concat(textBox1.Text, snm), string.Concat(sd.FullName, snm));
                    listBox15.Items.Clear();
                    FileInfo[] fixml = di.GetFiles("*.xml");
                    listBox15.Items.AddRange(fixml);

                    listBox43.Items.Clear();
                    listBox43.Items.AddRange(di.GetFiles());
                    label48.Text = listBox43.Items.Count.ToString();
                }
                catch (Exception)
                {

                }
            }
        }

        private void listBox16_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox16.Items.Count != 0)
            {
                DirectoryInfo sd = di.CreateSubdirectory("Txt");

                try
                {
                    string snm = "\\" + listBox16.SelectedItem.ToString();

                    Directory.Move(string.Concat(textBox1.Text, snm), string.Concat(sd.FullName, snm));
                    listBox16.Items.Clear();
                    FileInfo[] fitxt = di.GetFiles("*.txt");
                    listBox16.Items.AddRange(fitxt);

                    listBox43.Items.Clear();
                    listBox43.Items.AddRange(di.GetFiles());
                    label48.Text = listBox43.Items.Count.ToString();
                }
                catch (Exception)
                {

                }
            }
        }

        private void listBox17_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox17.Items.Count != 0)
            {
                DirectoryInfo sd = di.CreateSubdirectory("Ppt");

                try
                {
                    string snm = "\\" + listBox17.SelectedItem.ToString();

                    Directory.Move(string.Concat(textBox1.Text, snm), string.Concat(sd.FullName, snm));
                    listBox17.Items.Clear();
                    FileInfo[] fippt = di.GetFiles("*.ppt");
                    listBox17.Items.AddRange(fippt);

                    listBox43.Items.Clear();
                    listBox43.Items.AddRange(di.GetFiles());
                    label48.Text = listBox43.Items.Count.ToString();
                }
                catch (Exception)
                {

                }
            }
        }

        private void listBox18_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox18.Items.Count != 0)
            {
                DirectoryInfo sd = di.CreateSubdirectory("Pps");

                try
                {
                    string snm = "\\" + listBox18.SelectedItem.ToString();

                    Directory.Move(string.Concat(textBox1.Text, snm), string.Concat(sd.FullName, snm));
                    listBox18.Items.Clear();
                    FileInfo[] fipps = di.GetFiles("*.pps");
                    listBox18.Items.AddRange(fipps);

                    listBox43.Items.Clear();
                    listBox43.Items.AddRange(di.GetFiles());
                    label48.Text = listBox43.Items.Count.ToString();
                }
                catch (Exception)
                {

                }
            }
        }

        private void listBox19_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox19.Items.Count != 0)
            {
                DirectoryInfo sd = di.CreateSubdirectory("Rtf");

                try
                {
                    string snm = "\\" + listBox19.SelectedItem.ToString();

                    Directory.Move(string.Concat(textBox1.Text, snm), string.Concat(sd.FullName, snm));
                    listBox19.Items.Clear();
                    FileInfo[] firtf = di.GetFiles("*.rtf");
                    listBox19.Items.AddRange(firtf);

                    listBox43.Items.Clear();
                    listBox43.Items.AddRange(di.GetFiles());
                    label48.Text = listBox43.Items.Count.ToString();
                }
                catch (Exception)
                {

                }
            }
        }

        private void listBox20_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox20.Items.Count != 0)
            {
                DirectoryInfo sd = di.CreateSubdirectory("Xls");

                try
                {
                    string snm = "\\" + listBox20.SelectedItem.ToString();

                    Directory.Move(string.Concat(textBox1.Text, snm), string.Concat(sd.FullName, snm));
                    listBox20.Items.Clear();
                    FileInfo[] fixls = di.GetFiles("*.xls");
                    listBox20.Items.AddRange(fixls);

                    listBox43.Items.Clear();
                    listBox43.Items.AddRange(di.GetFiles());
                    label48.Text = listBox43.Items.Count.ToString();
                }
                catch (Exception)
                {

                }
            }
        }

        private void listBox21_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox21.Items.Count != 0)
            {
                DirectoryInfo sd = di.CreateSubdirectory("Chm");

                try
                {
                    string snm = "\\" + listBox21.SelectedItem.ToString();

                    Directory.Move(string.Concat(textBox1.Text, snm), string.Concat(sd.FullName, snm));
                    listBox21.Items.Clear();
                    FileInfo[] fichm = di.GetFiles("*.chm");
                    listBox21.Items.AddRange(fichm);

                    listBox43.Items.Clear();
                    listBox43.Items.AddRange(di.GetFiles());
                    label48.Text = listBox43.Items.Count.ToString();
                }
                catch (Exception)
                {

                }
            }
        }

        private void listBox22_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox22.Items.Count != 0)
            {
                DirectoryInfo sd = di.CreateSubdirectory("Tem");

                try
                {
                    string snm = "\\" + listBox22.SelectedItem.ToString();

                    Directory.Move(string.Concat(textBox1.Text, snm), string.Concat(sd.FullName, snm));
                    listBox22.Items.Clear();
                    FileInfo[] fitem = di.GetFiles("*.tem");
                    listBox22.Items.AddRange(fitem);

                    listBox43.Items.Clear();
                    listBox43.Items.AddRange(di.GetFiles());
                    label48.Text = listBox43.Items.Count.ToString();
                }
                catch (Exception)
                {

                }
            }
        }

        private void listBox23_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox23.Items.Count != 0)
            {
                DirectoryInfo sd = di.CreateSubdirectory("Pfw");

                try
                {
                    string snm = "\\" + listBox23.SelectedItem.ToString();

                    Directory.Move(string.Concat(textBox1.Text, snm), string.Concat(sd.FullName, snm));
                    listBox23.Items.Clear();
                    FileInfo[] fipfw = di.GetFiles("*.pfw");
                    listBox23.Items.AddRange(fipfw);

                    listBox43.Items.Clear();
                    listBox43.Items.AddRange(di.GetFiles());
                    label48.Text = listBox43.Items.Count.ToString();
                }
                catch (Exception)
                {

                }
            }
        }

        private void listBox24_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox24.Items.Count != 0)
            {
                DirectoryInfo sd = di.CreateSubdirectory("Accdb");

                try
                {
                    string snm = "\\" + listBox24.SelectedItem.ToString();

                    Directory.Move(string.Concat(textBox1.Text, snm), string.Concat(sd.FullName, snm));
                    listBox24.Items.Clear();
                    FileInfo[] fiaccdb = di.GetFiles("*.accdb");
                    listBox24.Items.AddRange(fiaccdb);

                    listBox43.Items.Clear();
                    listBox43.Items.AddRange(di.GetFiles());
                    label48.Text = listBox43.Items.Count.ToString();
                }
                catch (Exception)
                {

                }
            }
        }

        //Misc
        private void listBox25_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox25.Items.Count != 0)
            {
                DirectoryInfo sd = di.CreateSubdirectory("Vob");

                try
                {
                    string snm = "\\" + listBox25.SelectedItem.ToString();

                    Directory.Move(string.Concat(textBox1.Text, snm), string.Concat(sd.FullName, snm));
                    listBox25.Items.Clear();
                    FileInfo[] fivob = di.GetFiles("*.vob");
                    listBox25.Items.AddRange(fivob);

                    listBox43.Items.Clear();
                    listBox43.Items.AddRange(di.GetFiles());
                    label48.Text = listBox43.Items.Count.ToString();
                }
                catch (Exception)
                {

                }
            }
        }

        private void listBox26_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox26.Items.Count != 0)
            {
                DirectoryInfo sd = di.CreateSubdirectory("Vlc");

                try
                {
                    string snm = "\\" + listBox26.SelectedItem.ToString();

                    Directory.Move(string.Concat(textBox1.Text, snm), string.Concat(sd.FullName, snm));
                    listBox26.Items.Clear();
                    FileInfo[] fivlc = di.GetFiles("*.vlc");
                    listBox26.Items.AddRange(fivlc);

                    listBox43.Items.Clear();
                    listBox43.Items.AddRange(di.GetFiles());
                    label48.Text = listBox43.Items.Count.ToString();
                }
                catch (Exception)
                {

                }
            }
        }

        private void listBox27_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox27.Items.Count != 0)
            {
                DirectoryInfo sd = di.CreateSubdirectory("Mp3");

                try
                {
                    string snm = "\\" + listBox27.SelectedItem.ToString();

                    Directory.Move(string.Concat(textBox1.Text, snm), string.Concat(sd.FullName, snm));
                    listBox27.Items.Clear();
                    FileInfo[] fimp3 = di.GetFiles("*.mp3");
                    listBox27.Items.AddRange(fimp3);

                    listBox43.Items.Clear();
                    listBox43.Items.AddRange(di.GetFiles());
                    label48.Text = listBox43.Items.Count.ToString();
                }
                catch (Exception)
                {

                }
            }
        }

        private void listBox28_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox28.Items.Count != 0)
            {
                DirectoryInfo sd = di.CreateSubdirectory("Mp4");

                try
                {
                    string snm = "\\" + listBox28.SelectedItem.ToString();

                    Directory.Move(string.Concat(textBox1.Text, snm), string.Concat(sd.FullName, snm));
                    listBox28.Items.Clear();
                    FileInfo[] fimp4 = di.GetFiles("*.mp4");
                    listBox28.Items.AddRange(fimp4);

                    listBox43.Items.Clear();
                    listBox43.Items.AddRange(di.GetFiles());
                    label48.Text = listBox43.Items.Count.ToString();
                }
                catch (Exception)
                {

                }
            }
        }

        private void listBox29_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox29.Items.Count != 0)
            {
                DirectoryInfo sd = di.CreateSubdirectory("Dat");

                try
                {
                    string snm = "\\" + listBox29.SelectedItem.ToString();

                    Directory.Move(string.Concat(textBox1.Text, snm), string.Concat(sd.FullName, snm));
                    listBox29.Items.Clear();
                    FileInfo[] fidat = di.GetFiles("*.dat");
                    listBox29.Items.AddRange(fidat);

                    listBox43.Items.Clear();
                    listBox43.Items.AddRange(di.GetFiles());
                    label48.Text = listBox43.Items.Count.ToString();
                }
                catch (Exception)
                {

                }
            }
        }

        private void listBox30_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox30.Items.Count != 0)
            {
                DirectoryInfo sd = di.CreateSubdirectory("3gp");

                try
                {
                    string snm = "\\" + listBox30.SelectedItem.ToString();

                    Directory.Move(string.Concat(textBox1.Text, snm), string.Concat(sd.FullName, snm));
                    listBox30.Items.Clear();
                    FileInfo[] fi3gp = di.GetFiles("*.3gp");
                    listBox30.Items.AddRange(fi3gp);

                    listBox43.Items.Clear();
                    listBox43.Items.AddRange(di.GetFiles());
                    label48.Text = listBox43.Items.Count.ToString();
                }
                catch (Exception)
                {

                }
            }
        }

        private void listBox31_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox31.Items.Count != 0)
            {
                DirectoryInfo sd = di.CreateSubdirectory("Mkv");

                try
                {
                    string snm = "\\" + listBox31.SelectedItem.ToString();

                    Directory.Move(string.Concat(textBox1.Text, snm), string.Concat(sd.FullName, snm));
                    listBox31.Items.Clear();
                    FileInfo[] fimkv = di.GetFiles("*.mkv");
                    listBox31.Items.AddRange(fimkv);

                    listBox43.Items.Clear();
                    listBox43.Items.AddRange(di.GetFiles());
                    label48.Text = listBox43.Items.Count.ToString();
                }
                catch (Exception)
                {

                }
            }
        }

        private void listBox32_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox32.Items.Count != 0)
            {
                DirectoryInfo sd = di.CreateSubdirectory("Wav");

                try
                {
                    string snm = "\\" + listBox32.SelectedItem.ToString();

                    Directory.Move(string.Concat(textBox1.Text, snm), string.Concat(sd.FullName, snm));
                    listBox32.Items.Clear();
                    FileInfo[] fiwav = di.GetFiles("*.wav");
                    listBox32.Items.AddRange(fiwav);

                    listBox43.Items.Clear();
                    listBox43.Items.AddRange(di.GetFiles());
                    label48.Text = listBox43.Items.Count.ToString();
                }
                catch (Exception)
                {

                }
            }
        }

        private void listBox33_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox33.Items.Count != 0)
            {
                DirectoryInfo sd = di.CreateSubdirectory("Flv");

                try
                {
                    string snm = "\\" + listBox33.SelectedItem.ToString();

                    Directory.Move(string.Concat(textBox1.Text, snm), string.Concat(sd.FullName, snm));
                    listBox33.Items.Clear();
                    FileInfo[] fiflv = di.GetFiles("*.flv");
                    listBox33.Items.AddRange(fiflv);

                    listBox43.Items.Clear();
                    listBox43.Items.AddRange(di.GetFiles());
                    label48.Text = listBox43.Items.Count.ToString();
                }
                catch (Exception)
                {

                }
            }
        }

        private void listBox34_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox34.Items.Count != 0)
            {
                DirectoryInfo sd = di.CreateSubdirectory("Avi");

                try
                {
                    string snm = "\\" + listBox34.SelectedItem.ToString();

                    Directory.Move(string.Concat(textBox1.Text, snm), string.Concat(sd.FullName, snm));
                    listBox34.Items.Clear();
                    FileInfo[] fiavi = di.GetFiles("*.avi");
                    listBox34.Items.AddRange(fiavi);

                    listBox43.Items.Clear();
                    listBox43.Items.AddRange(di.GetFiles());
                    label48.Text = listBox43.Items.Count.ToString();
                }
                catch (Exception)
                {

                }
            }
        }

        private void listBox35_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox35.Items.Count != 0)
            {
                DirectoryInfo sd = di.CreateSubdirectory("Mpeg");

                try
                {
                    string snm = "\\" + listBox35.SelectedItem.ToString();

                    Directory.Move(string.Concat(textBox1.Text, snm), string.Concat(sd.FullName, snm));
                    listBox35.Items.Clear();
                    FileInfo[] fimpeg = di.GetFiles("*.mpeg");
                    listBox35.Items.AddRange(fimpeg);

                    listBox43.Items.Clear();
                    listBox43.Items.AddRange(di.GetFiles());
                    label48.Text = listBox43.Items.Count.ToString();
                }
                catch (Exception)
                {

                }
            }
        }

        private void listBox36_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox36.Items.Count != 0)
            {
                DirectoryInfo sd = di.CreateSubdirectory("Wmv");

                try
                {
                    string snm = "\\" + listBox36.SelectedItem.ToString();

                    Directory.Move(string.Concat(textBox1.Text, snm), string.Concat(sd.FullName, snm));
                    listBox36.Items.Clear();
                    FileInfo[] fiwmv = di.GetFiles("*.wmv");
                    listBox36.Items.AddRange(fiwmv);

                    listBox43.Items.Clear();
                    listBox43.Items.AddRange(di.GetFiles());
                    label48.Text = listBox43.Items.Count.ToString();
                }
                catch (Exception)
                {

                }
            }
        }

        //Others
        private void listBox37_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox37.Items.Count != 0)
            {
                DirectoryInfo sd = di.CreateSubdirectory("Exe");

                try
                {
                    string snm = "\\" + listBox37.SelectedItem.ToString();

                    Directory.Move(string.Concat(textBox1.Text, snm), string.Concat(sd.FullName, snm));
                    listBox36.Items.Clear();
                    FileInfo[] fiexe = di.GetFiles("*.exe");
                    listBox36.Items.AddRange(fiexe);

                    listBox43.Items.Clear();
                    listBox43.Items.AddRange(di.GetFiles());
                    label48.Text = listBox43.Items.Count.ToString();
                }
                catch (Exception)
                {

                }
            }
        }

        private void listBox38_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox38.Items.Count != 0)
            {
                DirectoryInfo sd = di.CreateSubdirectory("Rar");

                try
                {
                    string snm = "\\" + listBox38.SelectedItem.ToString();

                    Directory.Move(string.Concat(textBox1.Text, snm), string.Concat(sd.FullName, snm));
                    listBox38.Items.Clear();
                    FileInfo[] firar = di.GetFiles("*.rar");
                    listBox38.Items.AddRange(firar);

                    listBox43.Items.Clear();
                    listBox43.Items.AddRange(di.GetFiles());
                    label48.Text = listBox43.Items.Count.ToString();
                }
                catch (Exception)
                {

                }
            }
        }

        private void listBox39_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox39.Items.Count != 0)
            {
                DirectoryInfo sd = di.CreateSubdirectory("Zip");

                try
                {
                    string snm = "\\" + listBox39.SelectedItem.ToString();

                    Directory.Move(string.Concat(textBox1.Text, snm), string.Concat(sd.FullName, snm));
                    listBox39.Items.Clear();
                    FileInfo[] fizip = di.GetFiles("*.zip");
                    listBox39.Items.AddRange(fizip);

                    listBox43.Items.Clear();
                    listBox43.Items.AddRange(di.GetFiles());
                    label48.Text = listBox43.Items.Count.ToString();
                }
                catch (Exception)
                {

                }
            }
        }

        private void listBox40_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox40.Items.Count != 0)
            {
                DirectoryInfo sd = di.CreateSubdirectory("Dll");

                try
                {
                    string snm = "\\" + listBox40.SelectedItem.ToString();

                    Directory.Move(string.Concat(textBox1.Text, snm), string.Concat(sd.FullName, snm));
                    listBox40.Items.Clear();
                    FileInfo[] fidll = di.GetFiles("*.dll");
                    listBox40.Items.AddRange(fidll);

                    listBox43.Items.Clear();
                    listBox43.Items.AddRange(di.GetFiles());
                    label48.Text = listBox43.Items.Count.ToString();
                }
                catch (Exception)
                {

                }
            }
        }

        private void listBox41_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox41.Items.Count != 0)
            {
                DirectoryInfo sd = di.CreateSubdirectory("Ink");

                try
                {
                    string snm = "\\" + listBox41.SelectedItem.ToString();

                    Directory.Move(string.Concat(textBox1.Text, snm), string.Concat(sd.FullName, snm));
                    listBox41.Items.Clear();
                    FileInfo[] fiink = di.GetFiles("*.ink");
                    listBox41.Items.AddRange(fiink);

                    listBox43.Items.Clear();
                    listBox43.Items.AddRange(di.GetFiles());
                    label48.Text = listBox43.Items.Count.ToString();
                }
                catch (Exception)
                {

                }
            }
        }

        private void open(object sender, MouseEventArgs e)
        {
            try
            {
                string file = textBox1.Text + "//" + listBox43.SelectedItem.ToString();
                System.Diagnostics.Process.Start(file);
            }
            catch (Exception)
            {

            }
        }

        private void FileManager_Load(object sender, EventArgs e)
        {
            Choices ch = new Choices();
            ch.Add(new string[] { "browse", "images", "documents", "media", "other", "close" });

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
                    browse();
                }

                else if (e.Result.Text == "close")
                {
                    this.Close();
                    sre.Dispose();
                }

                else if (e.Result.Text == "images")
                {
                    tabControl1.SelectTab(tabPage1);
                }
                else if (e.Result.Text == "documents")
                {
                    tabControl1.SelectTab(tabPage2);
                }
                else if (e.Result.Text == "media")
                {
                    tabControl1.SelectTab(tabPage3);
                }
                else if (e.Result.Text == "other")
                {
                    tabControl1.SelectTab(tabPage4);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
