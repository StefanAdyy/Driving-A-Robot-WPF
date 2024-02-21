using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Driving_A_Robot_WPF.Models
{
    public class PointModel
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

        public PointModel(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }
    }
}
