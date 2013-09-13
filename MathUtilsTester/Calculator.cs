using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using Artentus.Utils.Math;

namespace MathUtilsTester
{
    public partial class Calculator : Form
    {
        public Calculator()
        {
            InitializeComponent();
            decimalButton.Text = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
        }

        private void zeroButton_Click(object sender, EventArgs e)
        {
            if (textBox1.SelectionLength > 0)
                textBox1.SelectedText = "0";
            else
            {
                int selectionStart = textBox1.SelectionStart;
                textBox1.Text = textBox1.Text.Insert(selectionStart, "0");
                textBox1.SelectionStart = selectionStart + 1;
            }
        }

        private void oneButton_Click(object sender, EventArgs e)
        {
            if (textBox1.SelectionLength > 0)
                textBox1.SelectedText = "1";
            else
            {
                int selectionStart = textBox1.SelectionStart;
                textBox1.Text = textBox1.Text.Insert(selectionStart, "1");
                textBox1.SelectionStart = selectionStart + 1;
            }
        }

        private void twoButton_Click(object sender, EventArgs e)
        {
            if (textBox1.SelectionLength > 0)
                textBox1.SelectedText = "2";
            else
            {
                int selectionStart = textBox1.SelectionStart;
                textBox1.Text = textBox1.Text.Insert(selectionStart, "2");
                textBox1.SelectionStart = selectionStart + 1;
            }
        }

        private void threeButton_Click(object sender, EventArgs e)
        {
            if (textBox1.SelectionLength > 0)
                textBox1.SelectedText = "3";
            else
            {
                int selectionStart = textBox1.SelectionStart;
                textBox1.Text = textBox1.Text.Insert(selectionStart, "3");
                textBox1.SelectionStart = selectionStart + 1;
            }
        }

        private void fourButton_Click(object sender, EventArgs e)
        {
            if (textBox1.SelectionLength > 0)
                textBox1.SelectedText = "4";
            else
            {
                int selectionStart = textBox1.SelectionStart;
                textBox1.Text = textBox1.Text.Insert(selectionStart, "4");
                textBox1.SelectionStart = selectionStart + 1;
            }
        }

        private void fiveButton_Click(object sender, EventArgs e)
        {
            if (textBox1.SelectionLength > 0)
                textBox1.SelectedText = "5";
            else
            {
                int selectionStart = textBox1.SelectionStart;
                textBox1.Text = textBox1.Text.Insert(selectionStart, "5");
                textBox1.SelectionStart = selectionStart + 1;
            }
        }

        private void sixButton_Click(object sender, EventArgs e)
        {
            if (textBox1.SelectionLength > 0)
                textBox1.SelectedText = "6";
            else
            {
                int selectionStart = textBox1.SelectionStart;
                textBox1.Text = textBox1.Text.Insert(selectionStart, "6");
                textBox1.SelectionStart = selectionStart + 1;
            }
        }

        private void sevenButton_Click(object sender, EventArgs e)
        {
            if (textBox1.SelectionLength > 0)
                textBox1.SelectedText = "7";
            else
            {
                int selectionStart = textBox1.SelectionStart;
                textBox1.Text = textBox1.Text.Insert(selectionStart, "7");
                textBox1.SelectionStart = selectionStart + 1;
            }
        }

        private void eightButton_Click(object sender, EventArgs e)
        {
            if (textBox1.SelectionLength > 0)
                textBox1.SelectedText = "8";
            else
            {
                int selectionStart = textBox1.SelectionStart;
                textBox1.Text = textBox1.Text.Insert(selectionStart, "8");
                textBox1.SelectionStart = selectionStart + 1;
            }
        }

        private void nineButton_Click(object sender, EventArgs e)
        {
            if (textBox1.SelectionLength > 0)
                textBox1.SelectedText = "9";
            else
            {
                int selectionStart = textBox1.SelectionStart;
                textBox1.Text = textBox1.Text.Insert(selectionStart, "9");
                textBox1.SelectionStart = selectionStart + 1;
            }
        }

        private void decimalButton_Click(object sender, EventArgs e)
        {
            if (textBox1.SelectionLength > 0)
                textBox1.SelectedText = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
            else
            {
                int selectionStart = textBox1.SelectionStart;
                textBox1.Text = textBox1.Text.Insert(selectionStart, CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);
                textBox1.SelectionStart = selectionStart + CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator.Length;
            }
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            if (textBox1.SelectionLength > 0)
                textBox1.SelectedText = "+";
            else
            {
                int selectionStart = textBox1.SelectionStart;
                textBox1.Text = textBox1.Text.Insert(selectionStart, "+");
                textBox1.SelectionStart = selectionStart + 1;
            }
        }

        private void subtractButton_Click(object sender, EventArgs e)
        {
            if (textBox1.SelectionLength > 0)
                textBox1.SelectedText = "-";
            else
            {
                int selectionStart = textBox1.SelectionStart;
                textBox1.Text = textBox1.Text.Insert(selectionStart, "-");
                textBox1.SelectionStart = selectionStart + 1;
            }
        }

        private void multiplyButton_Click(object sender, EventArgs e)
        {
            if (textBox1.SelectionLength > 0)
                textBox1.SelectedText = "*";
            else
            {
                int selectionStart = textBox1.SelectionStart;
                textBox1.Text = textBox1.Text.Insert(selectionStart, "*");
                textBox1.SelectionStart = selectionStart + 1;
            }
        }

        private void divideButton_Click(object sender, EventArgs e)
        {
            if (textBox1.SelectionLength > 0)
                textBox1.SelectedText = "/";
            else
            {
                int selectionStart = textBox1.SelectionStart;
                textBox1.Text = textBox1.Text.Insert(selectionStart, "/");
                textBox1.SelectionStart = selectionStart + 1;
            }
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            if (textBox1.SelectionLength > 0)
                textBox1.SelectedText = string.Empty;
            else if (textBox1.SelectionStart > 0)
            {
                textBox1.SelectionStart--;
                textBox1.Text = textBox1.Text.Remove(textBox1.SelectionStart, 1);
            }
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            textBox1.Text = string.Empty;
            label1.Text = string.Empty;
        }

        private void openBracketButton_Click(object sender, EventArgs e)
        {
            if (textBox1.SelectionLength > 0)
                textBox1.SelectedText = "(";
            else
            {
                int selectionStart = textBox1.SelectionStart;
                textBox1.Text = textBox1.Text.Insert(selectionStart, "(");
                textBox1.SelectionStart = selectionStart + 1;
            }
        }

        private void closeBracketButton_Click(object sender, EventArgs e)
        {
            if (textBox1.SelectionLength > 0)
                textBox1.SelectedText = ")";
            else
            {
                int selectionStart = textBox1.SelectionStart;
                textBox1.Text = textBox1.Text.Insert(selectionStart, ")");
                textBox1.SelectionStart = selectionStart + 1;
            }
        }

        private void lnButton_Click(object sender, EventArgs e)
        {
            if (textBox1.SelectionLength > 0)
                textBox1.SelectedText = "ln(";
            else
            {
                int selectionStart = textBox1.SelectionStart;
                textBox1.Text = textBox1.Text.Insert(selectionStart, "ln(");
                textBox1.SelectionStart = selectionStart + 3;
            }
        }

        private void sinButton_Click(object sender, EventArgs e)
        {
            if (textBox1.SelectionLength > 0)
                textBox1.SelectedText = "sin(";
            else
            {
                int selectionStart = textBox1.SelectionStart;
                textBox1.Text = textBox1.Text.Insert(selectionStart, "sin(");
                textBox1.SelectionStart = selectionStart + 4;
            }
        }

        private void cosButton_Click(object sender, EventArgs e)
        {
            if (textBox1.SelectionLength > 0)
                textBox1.SelectedText = "cos(";
            else
            {
                int selectionStart = textBox1.SelectionStart;
                textBox1.Text = textBox1.Text.Insert(selectionStart, "cos(");
                textBox1.SelectionStart = selectionStart + 4;
            }
        }

        private void evalButton_Click(object sender, EventArgs e)
        {
            label1.Text = textBox1.Text + " =";
            try
            {
                textBox1.Text = Parser.Eval(textBox1.Text).ToString();
            }
            catch
            {
                textBox1.Text = "Parsing Error";
            }
        }
    }
}
