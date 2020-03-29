using Mapack;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapeTransformation
{
    class LLSOTransform
    {
        public Transformation ComputeTransformation(List<Point> Shape1, List<Point> Shape2)
        {
            Matrix A = new Matrix(4,4);
            Matrix b = new Matrix(4,1);
            for(int i = 0; i < Shape1.Count; i++)
            {

            } 
            return null;
        }        
    }
}
