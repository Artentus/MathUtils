using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Artentus.Utils.Math;

namespace MathUtilsTester
{
    public partial class Example3D : Form
    {
        Matrix4x4 rotationX;
        Matrix4x4 rotationY;
        Matrix4x4 rotationZ;
        Matrix4x4 scalation;
        Matrix4x4 matrix;
        Vector4[] vertices;
        Matrix4x4 cubeCenter;
        Matrix4x4 invertedCubeCenter;

        public Example3D()
        {
            InitializeComponent();
            rotationX = Matrix4x4.GetIdentity();
            rotationY = Matrix4x4.GetIdentity();
            rotationZ = Matrix4x4.GetIdentity();
            scalation = Matrix4x4.GetIdentity();
            vertices = new Vector4[] { new Vector4(-0.5, 0.5, 1.5, 1), new Vector4(0.5, 0.5, 1.5, 1), new Vector4(0.5, -0.5, 1.5, 1), new Vector4(-0.5, -0.5, 1.5, 1),
                new Vector4(-0.5, 0.5, 2.5, 1), new Vector4(0.5, 0.5, 2.5, 1), new Vector4(0.5, -0.5, 2.5, 1), new Vector4(-0.5, -0.5, 2.5, 1) };
            cubeCenter = Matrix4x4.Translation(0, 0, 2);
            invertedCubeCenter = Matrix4x4.Translation(0, 0, -2);
            matrix = Matrix4x4.GetIdentity();
        }

        private void xTrackBar_Scroll(object sender, EventArgs e)
        {
            rotationX = Matrix4x4.RotationX(xTrackBar.Value / 500.0 * Math.PI);
            RefreshDisplay();
        }

        private void yTrackBar_Scroll(object sender, EventArgs e)
        {
            rotationY = Matrix4x4.RotationY(yTrackBar.Value / 500.0 * Math.PI);
            RefreshDisplay();
        }

        private void zTrackBar_Scroll(object sender, EventArgs e)
        {
            rotationZ = Matrix4x4.RotationZ(zTrackBar.Value / 500.0 * Math.PI);
            RefreshDisplay();
        }

        private void scalingTrackBar_Scroll(object sender, EventArgs e)
        {
            scalation = Matrix4x4.Scalation(scalingTrackBar.Value / 1000.0 + 0.5);
            RefreshDisplay();
        }

        private void RefreshDisplay()
        {
            matrix = cubeCenter * scalation * rotationX * rotationY * rotationZ * invertedCubeCenter;
            outputPanel.Invalidate();
        }

        private void outputPanel_Paint(object sender, PaintEventArgs e)
        {
            PointF[] displayCoordinates = new PointF[vertices.Length];
            for (int i = 0; i < vertices.Length; i++)
            {
                Vector3 v = (vertices[i] * matrix).ProjectPerspective(outputPanel.Size);
                displayCoordinates[i] = new PointF((float)v.X, (float)v.Y);
            }
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            using (Pen p = new Pen(Color.White, 3))
            {
                for (int i = 0, j = 3; i < 4; j = i, i++)
                    g.DrawLine(p, displayCoordinates[i], displayCoordinates[j]);
                for (int i = 4, j = 7; i < 8; j = i, i++)
                    g.DrawLine(p, displayCoordinates[i], displayCoordinates[j]);
                for (int i = 0, j = 4; i < 4; i++, j++)
                    g.DrawLine(p, displayCoordinates[i], displayCoordinates[j]);
            }
        }
    }
}
