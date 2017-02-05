using Sample.Items;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sample
{
    public partial class Form1 : Form
    {
        private Levenshtein levenshtein;
        private List<string> dictionary;
        private string inputWord, lastword;
        private char[] delimiters = new char[] { '\n', ' ', '.', ',', ';', '?', '!' };
        public Form1()
        {
            InitializeComponent();
            levenshtein = new Levenshtein();
            dictionary = new List<string>(File.ReadAllLines(@"D:\Projects\words"));
            dictionary.ForEach(word => word = word.Trim());
        }

        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                inputWord = TextBox.Text.ToString();
                int position = TextBox.SelectionStart;
                StringBuilder outputString = new StringBuilder();
                position--;
                if (position > 1 && delimiters.Contains(inputWord[position]))
                {
                    for (int i = position - 1; i >= 0; i--)
                    {
                        if (delimiters.Contains(inputWord[i])) { break; }
                        outputString.Append(inputWord[i]);
                    }
                    inputWord = Reverse(outputString.ToString());
                    if (!Check(inputWord))
                    {
                        List<Tuple<int, string>> comparator = new List<Tuple<int, string>>();
                        foreach (string word in dictionary)
                        {
                            comparator.Add(new Tuple<int, string>(levenshtein.calculate(inputWord, word), word));
                        }
                        comparator.Sort((x, y) => x.Item1.CompareTo(y.Item1));
                        lastword = comparator[0].Item2.ToString();
                        TextBox.Text = TextBox.Text.Replace(inputWord, lastword);
                    }
                }
                TextBox.SelectionStart = TextBox.Text.Length;
            } catch(Exception exception)
            {
                MessageBox.Show("Something went wrong:\n" + exception.ToString());
            }
            
        }
        public static string Reverse(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

        public Boolean Check (string word)
        {
            foreach (string archiveWord in dictionary)
            {
                if (archiveWord.Equals(inputWord))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
