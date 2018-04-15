using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms.DataVisualization.Charting;

namespace DigitalVoltmeter.DAC
{
    public class ParamsModel3D
    {
        public List<Point3D> PointsOfWalls { get; set; }
        public List<Point3D> PointsOfArea { get; set; }
        public List<List<Point3D>> Edges { get; set; }
        public ParamsModel3D(List<Point3D> pointsOfWalls, List<Point3D> pointsOfArea)
        {
            PointsOfWalls = pointsOfWalls;
            PointsOfArea = pointsOfArea;
        }

        public ParamsModel3D(List<List<Point3D>> edges, List<Point3D> pointsOfArea)
        {
            Edges = edges;
            PointsOfArea = pointsOfArea;
        }
    }
}
