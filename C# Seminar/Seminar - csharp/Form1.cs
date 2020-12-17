using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Seminar___csharp
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }


        private void Calculate_Click(object sender, EventArgs e)
        { //Button za izracun
            textBox2.Clear();
            Dictionary<string, int> words = new Dictionary<string, int>();
            string line;
            for (int i = 0; i < textBox1.Lines.Length; i++)
            {
                line = textBox1.Lines[i];
                line = Regex.Replace(line, @"[^a-zA-Z ]", String.Empty);
                string[] word = line.Split();
                foreach (string s in word)
                {
                    if (s.Length < 2) continue;
                    if (words.ContainsKey(s) == true)
                    {
                        words[s]++;
                    }
                    else
                    {
                        words.Add(s, 1);
                    }
                }

            }
            var newList = words.OrderByDescending(x => x.Value).ToList();
            textBox2.Text = "Riječ : Broj Pojavljivanja" + Environment.NewLine;
            textBox2.Text += "_______________________" + Environment.NewLine;
            textBox2.Text += Environment.NewLine;
            foreach (KeyValuePair<string, int> pair in newList) 
            {
                string par = $"{pair.Key} : {pair.Value}";
                textBox2.Text += par + Environment.NewLine;
            }
        }

        private void file_Click(object sender, EventArgs e)
        {//Otvaranje filea pomoću botuna
            OpenFile();
        }
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {//Otvaranje filea iz menu-a
            OpenFile();
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {//zatvaranje prozora iz menu-a
            Close();
        }

        private void OpenFile()
        {
            OpenFileDialog openfile = new OpenFileDialog
            {
                Filter = "Text File|*.txt",
                Title = "Open a Text File"
            };
            if (openfile.ShowDialog() == DialogResult.OK)
            {
                string path = openfile.FileName;
                StreamReader reader = new StreamReader(path);
                string text = reader.ReadToEnd();
                textBox1.Text = text;
            }
        }

        private void saveOutputToolStripMenuItem_Click(object sender, EventArgs e)
        {//Spremanje outputa u tekstualnu datoteku

            try
            {
                SaveFile();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK);
            }

        }

        private void SaveFile()
        {
            SaveFileDialog saveFile = new SaveFileDialog
            {
                Filter = "Text File|*.txt",
                Title = "Save an Text File"
            };
            if (textBox2.Text.Length == 0) 
            {
                throw new Exception(String.Format("Datoteka se ne može spremiti, textbox je prazan"));
            }
            if (saveFile.ShowDialog() == DialogResult.OK)
            {
                string path = saveFile.FileName;
                File.WriteAllText(path, textBox2.Text);
                throw new Exception(String.Format("Datoteka je spremljena"));
            }

        }           

        private void aboutToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            AboutBox1 about = new AboutBox1();
            about.Show();
        }        
    }
}
