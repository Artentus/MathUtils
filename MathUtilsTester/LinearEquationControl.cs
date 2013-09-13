using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Artentus.Utils.Math;

namespace MathUtilsTester
{
    public partial class LinearEquationControl : UserControl
    {
        LinearEquation equation;
        double[] coefficients;

        public LinearEquationControl(int variableCount)
        {
            InitializeComponent();
            char[] chars = new char[] { 'a', 'b', 'c', 'd', 'e' };
            equation = new LinearEquation();
            coefficients = new double[variableCount];
            int x = 10;
            for (int i = 0; i < variableCount; i++)
            {
                int index = i;
                NumericUpDown nud = new NumericUpDown();
                nud.TextAlign = HorizontalAlignment.Right;
                nud.Width = 70;
                nud.Minimum = decimal.MinValue;
                nud.Maximum = decimal.MaxValue;
                Controls.Add(nud);
                nud.ValueChanged += (e, sender) => coefficients[index] = (double)(e as NumericUpDown).Value;
                if (i > 0)
                {
                    Label lbl = new Label();
                    lbl.Text = chars[i - 1] + "    +";
                    lbl.AutoSize = false;
                    lbl.Size = new Size(30, nud.Height);
                    lbl.Location = new Point(x, 5);
                    lbl.TextAlign = ContentAlignment.MiddleCenter;
                    Controls.Add(lbl);
                    x += 32;
                }
                nud.Location = new Point(x, 5);
                x += 72;
            }
            NumericUpDown nudConstant = new NumericUpDown();
            nudConstant.Width = 70;
            nudConstant.TextAlign = HorizontalAlignment.Right;
            nudConstant.Minimum = decimal.MinValue;
            nudConstant.Maximum = decimal.MaxValue;
            Controls.Add(nudConstant);
            Label lblEquals = new Label();
            lblEquals.Text = chars[variableCount - 1] + "    =";
            lblEquals.AutoSize = false;
            lblEquals.Size = new Size(30, nudConstant.Height);
            lblEquals.Location = new Point(x, 5);
            lblEquals.TextAlign = ContentAlignment.MiddleCenter;
            Controls.Add(lblEquals);
            x += 32;
            nudConstant.Location = new Point(x, 5);
            nudConstant.ValueChanged += (e, sender) => equation.Constant = (double)nudConstant.Value;
            Size = new Size(nudConstant.Location.X + nudConstant.Width + 10, nudConstant.Height + 10);
        }

        public LinearEquation Equation
        {
            get
            {
                equation.Coefficients.Clear();
                equation.Coefficients.AddRange(coefficients);
                return equation;
            }
        }
    }
}
