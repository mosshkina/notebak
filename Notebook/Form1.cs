using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Printing;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System;

namespace Notebook
{
    public partial class Form1 : Form
    {
        private string _openFile;
        public Form1()
        {
            InitializeComponent();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void DarkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.ForeColor = Color.White;
            richTextBox1.BackColor = Color.DimGray;
            menuStrip1.BackColor = Color.DimGray;
            menuStrip1.ForeColor = Color.White;


        }

        private void WhiteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.ForeColor = Color.Black;
            richTextBox1.BackColor = Color.White;
            menuStrip1.BackColor = Color.White;
            menuStrip1.ForeColor = Color.Black;
        }

        private void FontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontDialog myFont = new FontDialog();
            if (myFont.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.Font = myFont.Font;
            }
        }

        private void TimeAndDateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Text += DateTime.Now;
        }

        private void NewWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog Fdialog = new OpenFileDialog();
            Fdialog.Filter = "all(*.*) |*.*";
            if (Fdialog.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.Text = File.ReadAllText(Fdialog.FileName);
                _openFile = Fdialog.FileName;
            }
        }

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var Form2 = new Form1();
            Form2.Show();
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog Sdialog = new SaveFileDialog();
            Sdialog.Filter = "all(*.*) |*.*";
            if (Sdialog.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(Sdialog.FileName, richTextBox1.Text);
                _openFile = Sdialog.FileName;
            }
        }

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                File.WriteAllText(_openFile, richTextBox1.Text);
            }
            catch (ArgumentException)
            {
                MessageBox.Show("save error");
            }

        }

        private void SealToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PrintDocument pDocument = new PrintDocument();
            pDocument.PrintPage += PrintPageH;
            PrintDialog pDialog = new PrintDialog();
            pDialog.Document = pDocument;
            if (pDialog.ShowDialog() == DialogResult.OK)
            {
                pDialog.Document.Print();
            }
        }
        public void PrintPageH(object sender, PrintPageEventArgs e)
        {
            e.Graphics.DrawString(richTextBox1.Text, richTextBox1.Font, Brushes.Black, 0, 0);
        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {
            int index = richTextBox1.SelectionStart;
            int currentLine = richTextBox1.GetLineFromCharIndex(index);
            int currentColumn = index - richTextBox1.GetFirstCharIndexFromLine(currentLine);
            toolStripStatusLabel1.Text = $"Cтрока:{currentLine + 1} Столбец:{currentColumn + 1}";
        }

        private void CreateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";

        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBox1.SelectedText.Length != 0)
            {
                Clipboard.SetText(richTextBox1.SelectedText);
            }
            else
            {
                MessageBox.Show("Выделите текст, который хотите скопировать");
            }

        }

        public void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBox1.SelectedText.Length != 0)
            {
                Clipboard.SetText(richTextBox1.Text.Substring(richTextBox1.SelectionStart, richTextBox1.SelectionLength));
                richTextBox1.Text = richTextBox1.Text.Remove(richTextBox1.SelectionStart, richTextBox1.SelectionLength);
            }
            else
            {
                MessageBox.Show("Выделите текст, который хотите вырезать");
            }


        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = richTextBox1.Text.Substring(0, richTextBox1.SelectionStart) + Clipboard.GetText() + richTextBox1.Text.Substring(richTextBox1.SelectionStart, richTextBox1.Text.Length - richTextBox1.SelectionStart);
        }

        protected virtual void AboutProgramToolStripMenuItem_Click(object sender, System.EventArgs e)
        {

            AboutProgram open_form = new AboutProgram();
            open_form.Show();
        }
    }
}