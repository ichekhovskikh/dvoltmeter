using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

namespace DigitalVoltmeter.DAC
{
    public class GraphicsService
    {
        private List<PointsNeighboor4> ListOfPointsNeighboor4;
        public GraphicsService()
        {

        }

        public void DrawSurface(Graphics g, Pen pen, List<Point3D> points)
        {
            ListOfPointsNeighboor4 = SearchPointsNeighbor4(points);
            //foreach (var pointsNeighboor4 in ListOfPointsNeighboor4)
            //{
            //    foreach (var point in pointsNeighboor4.PointsNeighboor)
            //        g.DrawLine(pen, pointsNeighboor4.GeneralPoint.X, pointsNeighboor4.GeneralPoint.Y, point.X, point.Y);
            //}

            List<double[]> order = SortArt(ListOfPointsNeighboor4.Select((x) => { return x.GeneralPoint; }).ToList());
            for (int i = 0; i < ListOfPointsNeighboor4.Count; i++)
            {
                int index = (int)order[i][1];
                order[i][0] = (order[i][0] < 0) ? 0 : order[i][0];
                double diapason = Math.Abs(order[i][0] - order[order.Count - 1][0]);
                int alpha = (int)((float)i / (float)ListOfPointsNeighboor4.Count * 255);
                //int alpha = (int)(((order[i][0] - order[0][0]) / diapason)*255);
                if (alpha > 255) alpha = 255;
                if (alpha <20) alpha = 20;

                int size = (int)((float)i / (float)ListOfPointsNeighboor4.Count * 3);
                foreach (var point in ListOfPointsNeighboor4[index].PointsNeighboor)
                    g.DrawLine(new Pen(new SolidBrush(Color.FromArgb(alpha, 0, 0, 0)), size),
                        (int)ListOfPointsNeighboor4[index].GeneralPoint.X,
                        (int)ListOfPointsNeighboor4[index].GeneralPoint.Y,
                        (int)point.X, (int)point.Y);

                //Какая то странная ошибка переполнения
                try
                {
                    g.DrawRectangle(new Pen(new SolidBrush(Color.FromArgb(alpha, 0, 0, 0)), size + 1),
                            (int)ListOfPointsNeighboor4[index].GeneralPoint.X,
                            (int)ListOfPointsNeighboor4[index].GeneralPoint.Y, size, size);
                }
                catch { }
            }
        }

        private List<double[]> SortArt(List<Point3D> points)
        {
            List<double[]> sort = new List<double[]>();
            for (int i = 0; i < points.Count; i++)
                    sort.Add(new double[2] { points[i].Z, i });
            //sort.Sort((x, y) => x[0].CompareTo(y[0]));
            sort = sort.OrderBy(x => x[0]).ToList();
            return sort;
        }

        public List<PointsNeighboor4> SearchPointsNeighbor4(List<Point3D> points)
        {
            List<PointsNeighboor4> pointsNeighboor4 = new List<PointsNeighboor4>();

            foreach (var point in points)
            {
                List<PairOfPoints> pairOfPoints = new List<PairOfPoints>();
                foreach (var point2 in points)
                    if (point != point2)
                        pairOfPoints.Add(new PairOfPoints(point, point2));
                pairOfPoints = pairOfPoints.OrderBy(p => p.Distance).Reverse().ToList();
                int lengthOfPairOfPoints = pairOfPoints.Count;
                List<Point3D> recorderPoints = new List<Point3D>();
                pointsNeighboor4.Add(new PointsNeighboor4(new List<Point3D>() { 
                    pairOfPoints[lengthOfPairOfPoints-1].SecondPoint,
                    pairOfPoints[lengthOfPairOfPoints-2].SecondPoint,   
                    pairOfPoints[lengthOfPairOfPoints-3].SecondPoint, 
                    pairOfPoints[lengthOfPairOfPoints-4].SecondPoint
                    },
                    point));
            }
            return pointsNeighboor4;
        }



        public class PointsNeighboor4
        {
            public Point3D GeneralPoint { get; set; }
            public List<Point3D> PointsNeighboor { get; set; }
            public PointsNeighboor4(List<Point3D> pointsNeighboor, Point3D generalPoint)
            {
                PointsNeighboor = pointsNeighboor;
                GeneralPoint = generalPoint;
            }
        }

        private class PairOfPoints
        {
            public Point3D FirstPoint { get; set; }
            public Point3D SecondPoint { get; set; }

            public double Distance { get; protected set; }
            public PairOfPoints(Point3D firstPoint, Point3D secondPoint)
            {
                FirstPoint = firstPoint;
                SecondPoint = secondPoint;
                Distance = GetDistance(firstPoint, secondPoint);
            }

            public static double GetDistance(Point3D p1, Point3D p2)
            {
                return Math.Sqrt(Math.Pow(p2.X - p1.X, 2) + Math.Pow(p2.Y - p1.Y, 2) + Math.Pow(p2.Z - p1.Z, 2));
            }
        }
    }
}
