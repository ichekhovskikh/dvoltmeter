using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

namespace DigitalVoltmeter.DAC
{
    public class ParamsPolygon
    {
        public List<Point3D> Vertexs { get; set; }
        public ParamsPolygon(List<Point3D> vertexs)
        {
            Vertexs = vertexs;
        }

        public ParamsPolygon() 
        {
            Vertexs = new List<Point3D>();
        }
    }
}
