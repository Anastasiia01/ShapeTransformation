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
        public static Transformation ComputeTransformation(List<Point> Shape1, List<Point> Shape2)
        {
            Transformation T = new Transformation();
            Matrix A = new Matrix(4, 4);
            Matrix b = new Matrix(4, 1);
            double sumX2 = 0;
            double sumY2 = 0;
            double squareSumX2 = 0;
            double squareSumY2 = 0;

            for (int i = 0; i < Shape1.Count; i++)
            {
                sumX2 += Shape2[i].X;
                sumY2 += Shape2[i].Y;
                squareSumX2 += Shape2[i].X * Shape2[i].X;
                squareSumY2 += Shape2[i].Y * Shape2[i].Y;
                b[0, 0] += Shape1[i].X * Shape2[i].X + Shape1[i].Y * Shape2[i].Y;
                b[1, 0] += Shape1[i].X * Shape2[i].Y - Shape2[i].X * Shape1[i].Y;
                b[2, 0] += Shape1[i].X;
                b[3, 0] += Shape1[i].Y;
            }
            //Fill matrix A 
            A[0, 0] = A[1, 1] = squareSumX2 + squareSumY2;
            //MessageBox.Show(A[1,1].ToString());
            A[0, 1] = A[1, 0] = 0;
            A[0, 2] = A[2, 0] = sumX2;
            A[0, 3] = A[3, 0] = sumY2;
            A[1, 2] = A[2, 1] = sumY2;
            A[1, 3] = A[3, 1] = -sumX2;
            A[2, 2] = A[3, 3] = Shape1.Count;
            A[2, 3] = A[3, 2] = 0;

            //Getting Transformation Matrix from matrix equation
            Matrix Ainv = A.Inverse;
            Matrix Tr = Ainv * b;
            T.A = Tr[0, 0]; 
            T.B = Tr[1, 0];
            T.T1 = Tr[2, 0]; 
            T.T2 = Tr[3, 0];
            return T;
        }
        public static double ComputeCost(List<Point> List1, List<Point> List2, Transformation T)
        {
            double xPrime;
            double yPrime;
            double cost = 0;
            for(int i=0;i<List1.Count;i++)
            {
                xPrime = T.A * List2[i].X + T.B * List2[i].Y + T.T1;
                yPrime = -1 * T.B * List2[i].X + T.A * List2[i].Y + T.T2;
                cost +=Math.Sqrt((List1[i].X - xPrime)* (List1[i].X - xPrime) +(List1[i].Y - yPrime)* (List1[i].Y - yPrime));//square of Euclidean distance
            }
            return cost;
        }
        public static double ComputeCost(Point p1, Point p2, Transformation T)
        {
            double xPrime;
            double yPrime;
            xPrime = T.A * p2.X + T.B * p2.Y + T.T1;
            yPrime = -1 * T.B * p2.X + T.A * p2.Y + T.T2;
            double cost = Math.Sqrt((p1.X - xPrime) * (p1.X - xPrime) + (p1.Y - yPrime) * (p1.Y - yPrime));//square of Euclidean distance
            return cost;
        }

    }
}
