using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace DigitalVoltmeter
{
    public partial class GraphExpandingForm : Form
    {
        public Chart Chart { get { return chart; } }

        public GraphExpandingForm()
        {
            InitializeComponent();
        }

    }
}
