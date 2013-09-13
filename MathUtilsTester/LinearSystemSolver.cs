using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Artentus.Utils.Math;

namespace MathUtilsTester
{
    public partial class LinearSystemSolver : Form
    {
        LinearEquationControl[] equationControls;

        public LinearSystemSolver()
        {
            InitializeComponent();
            GenerateControls();
        }

        private void GenerateControls()
        {
            flowLayoutPanel1.Controls.Clear();
            equationControls = new LinearEquationControl[(int)numericUpDown1.Value];
            for (int i = 0; i < (int)numericUpDown1.Value; i++)
            {
                LinearEquationControl lec = new LinearEquationControl((int)numericUpDown1.Value);
                equationControls[i] = lec;
                flowLayoutPanel1.Controls.Add(lec);
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            flowLayoutPanel3.Controls.Clear();
            GenerateControls();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            flowLayoutPanel3.Controls.Clear();
            LinearSystem ls = new LinearSystem();
            for (int i = 0; i < equationControls.Length; i++)
                ls.Equations.Add(equationControls[i].Equation);
            char[] chars = new char[] { 'a', 'b', 'c', 'd', 'e' };
            double[] result = ls.Solve();
            for (int i = 0; i < result.Length; i++)
            {
                Label lbl = new Label();
                lbl.Margin = new Padding(12, i == 0 ? 8 : 0, 12, 0);
                lbl.Text = chars[i] + " = " + result[i];
                flowLayoutPanel3.Controls.Add(lbl);
            }
        }
    }
}
