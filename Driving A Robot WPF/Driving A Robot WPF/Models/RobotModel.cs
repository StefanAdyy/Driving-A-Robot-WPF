using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Driving_A_Robot_WPF.Models
{
    public class RobotModel
    {
        private double _xCoordinate;
        private double _yCoordinate;
        private double _zCoordinate;

        public double XCoordinate
        {
            get { return _xCoordinate; }
            set
            {
                _xCoordinate = value;
                AreCoordinatesSet = true; 
            }
        }

        public double YCoordinate
        {
            get { return _yCoordinate; }
            set
            {
                _yCoordinate = value;
                AreCoordinatesSet = true;
            }
        }

        public double ZCoordinate
        {
            get { return _zCoordinate; }
            set
            {
                _zCoordinate = value;
                AreCoordinatesSet = true;
            }
        }

        public bool AreCoordinatesSet { get; private set; }

        public RobotModel(double xCoordinate, double yCoordinate, double zCoordinate)
        {
            XCoordinate = xCoordinate;
            YCoordinate = yCoordinate;
            ZCoordinate = zCoordinate;
            AreCoordinatesSet = true;
        }

        public override string ToString()
        {
            string s;
            
            if (AreCoordinatesSet)
            {
                s = XCoordinate.ToString() + " " + YCoordinate.ToString() + " " + ZCoordinate.ToString();
            }
            else
            {
                s = "??? ??? ???";   
            }

            return s;
        }
    }
}
