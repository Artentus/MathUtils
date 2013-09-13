using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MathUtilsTester
{
    public partial class Form1 : Form
    {
        Calculator calculator;
        LinearSystemSolver solver;
        Example3D example3D;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (calculator == null || calculator.IsDisposed)
                calculator = new Calculator();
            calculator.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (solver == null || solver.IsDisposed)
                solver = new LinearSystemSolver();
            solver.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (example3D == null || example3D.IsDisposed)
                example3D = new Example3D();
            example3D.Show();
        }
    }
}
