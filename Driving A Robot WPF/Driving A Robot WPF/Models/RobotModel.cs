using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Driving_A_Robot_WPF.Models
{
    public class RobotModel
    {
        private PointModel _coordinates;
        public PointModel Coordinates
        {
            get { return _coordinates; }
            set
            {
                _coordinates = value;
            }
        }

        public RobotModel(PointModel coordinates)
        {
            Coordinates = coordinates;
        }

        public RobotModel() { }

        public override string ToString()
        {
            return Coordinates.X.ToString() + ", " + Coordinates.Y.ToString() + ", " + Coordinates.Z.ToString();
        }
    }
}
