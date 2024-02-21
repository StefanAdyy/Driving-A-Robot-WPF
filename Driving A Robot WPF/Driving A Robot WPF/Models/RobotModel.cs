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
                AreCoordinatesSet = true;
            }
        }

        public bool AreCoordinatesSet { get; private set; }

        public RobotModel(PointModel coordinates)
        {
            Coordinates = coordinates;
            AreCoordinatesSet = true;
        }

        public override string ToString()
        {
            string s;
            
            if (AreCoordinatesSet)
            {
                s = Coordinates.X.ToString() + " " + Coordinates.Y.ToString() + " " + Coordinates.Z.ToString();
            }
            else
            {
                s = "??? ??? ???";   
            }

            return s;
        }
    }
}
