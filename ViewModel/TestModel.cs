using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace WpfApplication4.ViewModel
{
    public class CogoPointModel
    {
        public string Name { get; set; }
        public double PointX { get; set; }
        public double PointY { get; set; }
        public double PointZ { get; set; }
        public double Q { get; set; }
        public string PipeName { get; set; }

        public CogoPointModel() { }
        public CogoPointModel(string name, double pointX, double pointY, double pointZ)
        {
            Name = name;
            PointX = pointX;
            PointY = pointY;
            PointZ = pointZ;
        }
        public CogoPointModel(string name, double pointX, double pointY, double pointZ, string pipeDiameter)
        {
            Name = name;
            PointX = pointX;
            PointY = pointY;
            PointZ = pointZ;
            PipeName = pipeDiameter;
        }
    }
}
