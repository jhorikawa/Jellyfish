using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhino;
using Rhino.Geometry;

namespace Jellyfish.Functions
{
    class JellyUtility
    {
        public static double Remap(double val, double smin, double smax, double tmin, double tmax)
        {
            return (val - smin) / (smax - smin) * (tmax - tmin) + tmin;
        }
    }
}
