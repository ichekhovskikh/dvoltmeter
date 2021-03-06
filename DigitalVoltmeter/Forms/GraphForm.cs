﻿using DigitalVoltmeter.DAC;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace DigitalVoltmeter.Forms
{
    public partial class GraphForm : Form
    {
        DigitalVoltmeterForm parrent;

        Color xColor = Color.Red;
        Color yColor = Color.Blue;
        Color zColor = Color.Green;
        int axisWidth = 2;

        private double level = 0; double alpha = 0, beta = 0, gamma = 0, pi = Math.PI / 180;
        private Point startPosition;
        private Point movePosition = new Point(200, 150);

        public Point3D SelectedPoint { get; set; }

        public List<Point3D> Area { get; set; }
        public List<ParamsPolygon> Polygons { get; set; }
        private double k = 0;

        public GraphicsService GraphicsService { get; set; }
        public ParamsModel3D ParamsModel { get; set; }

        //public GraphForm() : this(null, null) { }
        public GraphForm(List<ParamsPolygon> Polygons) : this(Polygons, null) { }
        public GraphForm(List<ParamsPolygon> Polygons, DigitalVoltmeterForm parrent)
        {
            InitializeComponent();
            this.parrent = parrent;
            this.Polygons = Polygons;
            k = parrent.K;

            InitializeDataGrid();

            RefreshGraph();
        }

        public GraphForm(ParamsModel3D paramsModel3D, DigitalVoltmeterForm parrent)
        {
            InitializeComponent();
            this.parrent = parrent;
            k = parrent.K;
            GraphicsService = new GraphicsService();
            ParamsModel = paramsModel3D;
            InitializeDataGrid();
            trackBarZoom.Value = (int)(level * 1000);
            RefreshGraph();
        }

        private void InitializeDataGrid()
        {
            dataGridViewVect.Columns.Clear();

            DataGridViewTextBoxColumn ind = new DataGridViewTextBoxColumn
            {
                Name = "№",
                SortMode = DataGridViewColumnSortMode.Automatic,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            };
            DataGridViewTextBoxColumn x = new DataGridViewTextBoxColumn
            {
                Name = "ΔK",
                SortMode = DataGridViewColumnSortMode.Automatic,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            };
            DataGridViewTextBoxColumn y = new DataGridViewTextBoxColumn
            {
                Name = "δсм",
                SortMode = DataGridViewColumnSortMode.Automatic,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            };
            DataGridViewTextBoxColumn z = new DataGridViewTextBoxColumn
            {
                Name = "Δi",
                SortMode = DataGridViewColumnSortMode.Automatic,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            };

            x.DefaultCellStyle.ForeColor = xColor;
            y.DefaultCellStyle.ForeColor = yColor;
            z.DefaultCellStyle.ForeColor = zColor;

            dataGridViewVect.Columns.Add(ind);
            dataGridViewVect.Columns.Add(x);
            dataGridViewVect.Columns.Add(y);
            dataGridViewVect.Columns.Add(z);

            dataGridViewVect.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            List<Point3D> uniquePoints = new List<Point3D>() { };
            foreach (Point3D p3d in ParamsModel.PointsOfArea)
                if (uniquePoints.FirstOrDefault(delegate(Point3D ppp) { return ppp.X == p3d.X && ppp.Y == p3d.Y && ppp.Z == p3d.Z; }) == null)
                    uniquePoints.Add(p3d);
            for (int i = 0; i < uniquePoints.Count; i++)
                dataGridViewVect.Rows.Add(new object[] { i, uniquePoints[i].X, uniquePoints[i].Y, uniquePoints[i].Z });

            toolStripMenuItemCopy.Visible = true;
        }

        class Polygon
        {
            internal Point[] p = null;
            internal int[] z = null;

            public Polygon(Point3D point1, Point3D point2, Point3D point3, Point3D point4)
            {
                p = new Point[4]; z = new int[4];
                p[0] = new Point((int)point1.X, (int)point1.Y); z[0] = (int)point1.Z;
                p[1] = new Point((int)point2.X, (int)point2.Y); z[1] = (int)point2.Z;
                p[2] = new Point((int)point3.X, (int)point3.Y); z[2] = (int)point3.Z;
                p[3] = new Point((int)point4.X, (int)point4.Y); z[3] = (int)point4.Z;
            }

            public Polygon(Point3D point1, Point3D point2, Point3D point3)
            {
                p = new Point[3]; z = new int[3];
                p[0] = new Point((int)point1.X, (int)point1.Y); z[0] = (int)point1.Z;
                p[1] = new Point((int)point2.X, (int)point2.Y); z[1] = (int)point2.Z;
                p[2] = new Point((int)point3.X, (int)point3.Y); z[2] = (int)point3.Z;
            }

            public Polygon(float X1, float Y1, float Z1, float X2, float Y2, float Z2, float X3, float Y3, float Z3, float X4, float Y4, float Z4)
            {
                p = new Point[4]; z = new int[4];
                p[0] = new Point((int)X1, (int)Y1); z[0] = (int)Z1;
                p[1] = new Point((int)X2, (int)Y2); z[1] = (int)Z2;
                p[2] = new Point((int)X3, (int)Y3); z[2] = (int)Z3;
                p[3] = new Point((int)X4, (int)Y4); z[3] = (int)Z4;
            }

            public Polygon(float X1, float Y1, float Z1, float X2, float Y2, float Z2, float X3, float Y3, float Z3)
            {
                p = new Point[3]; z = new int[3];
                p[0] = new Point((int)X1, (int)Y1); z[0] = (int)Z1;
                p[1] = new Point((int)X2, (int)Y2); z[1] = (int)Z2;
                p[2] = new Point((int)X3, (int)Y3); z[2] = (int)Z3;
            }
        }

        #region x, y, R, sin, cos, RotationMatrix
        double sin(double x)
        {
            return Math.Sin(x * pi);
        }
        double cos(double x)
        {
            return Math.Cos(x * pi);
        }
        double l1()
        {
            return cos(alpha) * cos(gamma) - cos(beta) * sin(alpha) * sin(gamma);
        }
        double m1()
        {
            return sin(alpha) * cos(gamma) + cos(beta) * cos(alpha) * sin(gamma);
        }
        double n1()
        {
            return sin(beta) * sin(gamma);
        }
        double l2()
        {
            return -cos(alpha) * sin(gamma) + cos(beta) * sin(alpha) * cos(gamma);
        }
        double m2()
        {
            return -sin(alpha) * sin(gamma) + cos(beta) * cos(alpha) * cos(gamma);
        }
        double n2()
        {
            return sin(beta) * cos(gamma);
        }
        double l3()
        {
            return sin(beta) * sin(alpha);
        }
        double m3()
        {
            return -sin(beta) * cos(alpha);
        }
        double n3()
        {
            return cos(beta);
        }
        #endregion

        private List<double[]> SortArt(List<Polygon> polygons)
        {
            List<double[]> sort = new List<double[]>();
            for (int i = 0; i < polygons.Count; i++)
                if (polygons[i].p.Length == 4)
                    sort.Add(new double[2] { (polygons[i].z[0] + polygons[i].z[1] + polygons[i].z[2] + polygons[i].z[3]) / 4, i });
                else sort.Add(new double[2] { (polygons[i].z[0] + polygons[i].z[1] + polygons[i].z[2]) / 3, i });
            sort.Sort((x, y) => x[0].CompareTo(y[0]));
            return sort;
        }

        public void RefreshGraph()
        {
            if (GraphicsService == null || ParamsModel == null)
                return;

            double size = Math.Pow(2, level);
            List<Polygon> polygons = new List<Polygon>();
            Bitmap bmp = new Bitmap(Properties.Resources.background, pictureBox.Size);
            Graphics g = Graphics.FromImage(bmp);
            List<Point3D> pointsFor2D = new List<Point3D>(), circle_down = new List<Point3D>();

            TransformTo2DAndDrawPoints(g, Pens.Black, ParamsModel.Edges, size);
            DrawSelectedPoint(g, new SolidBrush(Color.Yellow), SelectedPoint);
            //TransformTo2DAndDrawPoints(g, Pens.Black, ParamsModel.PointsOfArea, size);
            //TransformTo2DAndDrawPoints(g, Pens.Black, ParamsModel.PointsOfWalls, size);
            DrawAxis(g);
            
            /* Как использовать художника
            List<double[]> order = SortArt(polygons);
            for (int i = 0; i < polygons.Count; i++)
            {
                order[i][0] = (order[i][0] < 0) ? 0 : order[i][0];
                    g.FillPolygon(new SolidBrush(Color.FromArgb(alfa.Value, 
                                                                red.Value + (int)((255 - red.Value) * order[i][0]) / 1500,
                                                                green.Value + +(int)((255 - green.Value) * order[i][0] / 1500),
                                                                blue.Value + +(int)((255 - blue.Value) * order[i][0]) / 1500)), 
                                                 polygons[(int)order[i][1]].p);
            }
            */
            pictureBox.Image = bmp;
            pictureBox.Refresh();
        }

        private void TransformTo2DAndDrawPoints(Graphics g, Pen pen, List<Point3D> points, double size)
        {
            List<Point3D> pointsFor2D = new List<Point3D>();
            for (int i = 0; i < points.Count; i++)
            {
                pointsFor2D.Add(new Point3D((int)((points[i].X * l1() + points[i].Y * l2() + (points[i].Z * l3())) / (size*0.001)) + movePosition.X,
                                            (int)((points[i].X * m1() + points[i].Y * m2() + (points[i].Z * m3())) / (size * 0.001)) + movePosition.Y,
                                            (int)((points[i].X * n1() + points[i].Y * n2() + (points[i].Z * n3())) / (size * 0.001))));

            }
            GraphicsService.DrawSurface(g, pen, pointsFor2D);
        }

        private void TransformTo2DAndDrawPoints(Graphics g, Pen pen, List<List<Point3D>> edges, double size)
        {
            List<List<Point3D>> pointsFor2D = new List<List<Point3D>>();
            foreach (var edge in edges)
            {
                List<Point3D> points = new List<Point3D>();
                for (int i = 0; i < edge.Count; i++)
                {
                    points.Add(new Point3D((int)((edge[i].X * l1() + edge[i].Y * l2() + (edge[i].Z * l3())) / (size * 0.001)) + movePosition.X,
                                                (int)((edge[i].X * m1() + edge[i].Y * m2() + (edge[i].Z * m3())) / (size * 0.001)) + movePosition.Y,
                                                (int)((edge[i].X * n1() + edge[i].Y * n2() + (edge[i].Z * n3())) / (size * 0.001))));
                }
                pointsFor2D.Add(points);
            }
            GraphicsService.DrawSurface(g, pen, pointsFor2D);
        }

        private void DrawSelectedPoint(Graphics g, Brush brush, Point3D point)
        {
            if (point == null)
                return;

            double size = Math.Pow(2, level);
            float width = 7 / (float)size;
            float height = 7 / (float)size;
            var selectedPoint = new Point3D((int)((point.X * l1() + point.Y * l2() + (point.Z * l3())) / (size * 0.001)) + movePosition.X,
                                                (int)((point.X * m1() + point.Y * m2() + (point.Z * m3())) / (size * 0.001)) + movePosition.Y,
                                                (int)((point.X * n1() + point.Y * n2() + (point.Z * n3())) / (size * 0.001)));
            g.FillEllipse(new SolidBrush(Color.Black), (int)(selectedPoint.X - width / 2), (int)(selectedPoint.Y - height / 2), width, height);
            g.FillEllipse(brush, (int)(selectedPoint.X - width * 0.5 / 2), (int)(selectedPoint.Y - height * 0.5 / 2), (int)(width * 0.5), (int)(height * 0.5));


        }

        public PointF getVertexPoint(Point3D point, double size)
        {
            return getVertexPoint(point.X, point.Y, point.Z, size);
        }
        public PointF getVertexPoint(float x, float y, float z, double size)
        {
            return new PointF((float)(((x * l1() + y * l2() + (z * 1000 * l3())) / size) + movePosition.X), (float)(((x * m1() + y * m2() + (z * 1000 * m3())) / size) + movePosition.Y));
        }
        void DrawAxis(Graphics g)
        {
            float arrowSpread = 5;
            float arrowHalfSpread = arrowSpread / 2F;
            double size = Math.Pow(2, level) * k;
            Point3D vertex = new Point3D(0, 0, 0);
            Point3D notVertex = new Point3D(0, 0, 0);
            vertex.X = 100;
            notVertex.X = -100;
            Font font = new Font("Arial", 10, FontStyle.Bold);

            g.DrawString("ΔK", font, new SolidBrush(xColor), getVertexPoint(vertex, size));
            g.DrawLine(new Pen(xColor, axisWidth), getVertexPoint(notVertex, size), getVertexPoint(vertex, size));
            g.DrawLine(new Pen(xColor, axisWidth), getVertexPoint(vertex.X * 0.9F, vertex.Y - arrowSpread, vertex.Z - arrowSpread / 1000, size), getVertexPoint(vertex, size));
            g.DrawLine(new Pen(xColor, axisWidth), getVertexPoint(vertex.X * 0.9F, vertex.Y + arrowSpread, vertex.Z - arrowSpread / 1000, size), getVertexPoint(vertex, size));
            g.DrawLine(new Pen(xColor, axisWidth), getVertexPoint(vertex.X * 0.9F, vertex.Y - arrowSpread, vertex.Z + arrowSpread / 1000, size), getVertexPoint(vertex, size));
            g.DrawLine(new Pen(xColor, axisWidth), getVertexPoint(vertex.X * 0.9F, vertex.Y + arrowSpread, vertex.Z + arrowSpread / 1000, size), getVertexPoint(vertex, size));
            for (int i = 1; i < 20; i++)
                if (i != 10) g.DrawLine(new Pen(xColor, axisWidth), getVertexPoint(notVertex.X + (vertex.X - notVertex.X) / 20F * i, notVertex.Y - arrowHalfSpread, notVertex.Z - arrowHalfSpread / 1000, size), getVertexPoint(notVertex.X + (vertex.X - notVertex.X) / 20F * i, notVertex.Y + arrowHalfSpread, notVertex.Z + arrowHalfSpread / 1000, size));
            vertex.X = 0;
            vertex.Y = 100;
            notVertex.X = 0;
            notVertex.Y = -100;
            g.DrawString("δсм", font, new SolidBrush(yColor), getVertexPoint(vertex, size));
            g.DrawLine(new Pen(yColor, axisWidth), getVertexPoint(notVertex, size), getVertexPoint(vertex, size));
            g.DrawLine(new Pen(yColor, axisWidth), getVertexPoint(vertex.X - arrowSpread, vertex.Y * 0.9F, vertex.Z - arrowSpread / 1000, size), getVertexPoint(vertex, size));
            g.DrawLine(new Pen(yColor, axisWidth), getVertexPoint(vertex.X + arrowSpread, vertex.Y * 0.9F, vertex.Z - arrowSpread / 1000, size), getVertexPoint(vertex, size));
            g.DrawLine(new Pen(yColor, axisWidth), getVertexPoint(vertex.X - arrowSpread, vertex.Y * 0.9F, vertex.Z + arrowSpread / 1000, size), getVertexPoint(vertex, size));
            g.DrawLine(new Pen(yColor, axisWidth), getVertexPoint(vertex.X + arrowSpread, vertex.Y * 0.9F, vertex.Z + arrowSpread / 1000, size), getVertexPoint(vertex, size));
            for (int i = 1; i < 20; i++)
                if (i != 10) g.DrawLine(new Pen(yColor, axisWidth), getVertexPoint(notVertex.X - arrowHalfSpread, notVertex.Y + (vertex.Y - notVertex.Y) / 20F * i, notVertex.Z - arrowHalfSpread / 1000, size), getVertexPoint(notVertex.X + arrowHalfSpread, notVertex.Y + (vertex.Y - notVertex.Y) / 20F * i, notVertex.Z + arrowHalfSpread / 1000, size));
            vertex.Y = 0;
            vertex.Z = 0.1F;
            notVertex.Y = 0;
            notVertex.Z = -0.1F;
            g.DrawString("Δi", font, new SolidBrush(zColor), getVertexPoint(vertex, size));
            g.DrawLine(new Pen(zColor, axisWidth), getVertexPoint(notVertex, size), getVertexPoint(vertex, size));
            g.DrawLine(new Pen(zColor, axisWidth), getVertexPoint(vertex.X - arrowSpread, vertex.Y - arrowSpread, vertex.Z * 0.9F, size), getVertexPoint(vertex, size));
            g.DrawLine(new Pen(zColor, axisWidth), getVertexPoint(vertex.X + arrowSpread, vertex.Y - arrowSpread, vertex.Z * 0.9F, size), getVertexPoint(vertex, size));
            g.DrawLine(new Pen(zColor, axisWidth), getVertexPoint(vertex.X - arrowSpread, vertex.Y + arrowSpread, vertex.Z * 0.9F, size), getVertexPoint(vertex, size));
            g.DrawLine(new Pen(zColor, axisWidth), getVertexPoint(vertex.X + arrowSpread, vertex.Y + arrowSpread, vertex.Z * 0.9F, size), getVertexPoint(vertex, size));
            for (int i = 1; i < 20; i++)
                if (i != 10) g.DrawLine(new Pen(zColor, axisWidth), getVertexPoint(notVertex.X - arrowHalfSpread, notVertex.Y - arrowHalfSpread, notVertex.Z + (vertex.Z - notVertex.Z) / 20F * i, size), getVertexPoint(notVertex.X + arrowHalfSpread, notVertex.Y + arrowHalfSpread, notVertex.Z + (vertex.Z - notVertex.Z) / 20F * i, size));
        }

        private void trackBarY_Scroll(object sender, EventArgs e)
        {
            beta = trackBarY.Value;
            RefreshGraph();
        }

        private void trackBarX_Scroll(object sender, EventArgs e)
        {
            gamma = trackBarX.Value;
            RefreshGraph();
        }

        private void dataGridViewVect_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || parrent == null) return;
            parrent.DeltaK = Convert.ToDouble(dataGridViewVect.Rows[e.RowIndex].Cells["ΔK"].Value);
            parrent.DeltaUsm = Convert.ToDouble(dataGridViewVect.Rows[e.RowIndex].Cells["δсм"].Value);
            parrent.DeltaI = Convert.ToDouble(dataGridViewVect.Rows[e.RowIndex].Cells["Δi"].Value);
            SelectedPoint = new Point3D((float)parrent.DeltaK, (float)parrent.DeltaUsm, (float)parrent.DeltaI);
            RefreshGraph();
            parrent.GetModelPerformClick();
        }

        private void toolStripMenuItemCopy_Click(object sender, EventArgs e)
        {
            if (dataGridViewVect.GetCellCount(DataGridViewElementStates.Selected) > 0)
            {
                try
                {
                    Clipboard.SetDataObject(dataGridViewVect.GetClipboardContent());
                }
                catch (System.Runtime.InteropServices.ExternalException)
                {
                    MessageBox.Show("Копирование невозможно");
                }
            }
        }

        private void trackBarZoom_Scroll(object sender, EventArgs e)
        {
            level = trackBarZoom.Value/1000d;
            RefreshGraph();
        }

        private void pictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            /*
            if (e.Button == MouseButtons.Left)
            {
                if ((startPosition.Y - e.Y) < 0) beta -= 2;
                else if ((startPosition.Y - e.Y) > 0) beta += 2;
                if ((startPosition.X - e.X) < 0) gamma -= 2;
                else if ((startPosition.X - e.X) > 0) gamma += 2;
                if (beta > 359) beta = 0;
                else if (beta < 0) beta = 359;
                if (gamma > 359) gamma = 0;
                else if (gamma < 0) gamma = 359;
                RefreshGraph();
            }
            */
            if (e.Button == MouseButtons.Right)
            {
                movePosition = e.Location;
                RefreshGraph();
            }
        }

        private void pictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            /*
            if (e.Button == MouseButtons.Left)
                startPosition = e.Location;
            */
            if (e.Button == MouseButtons.Right)
            {
                startPosition = e.Location;
                movePosition = e.Location;
                RefreshGraph();
            }
        }
    }
}
