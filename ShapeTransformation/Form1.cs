using Mapack;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShapeTransformation
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        List<Point> Shape1 = new List<Point>();
        List<Point> Shape2 = new List<Point>();


        private void btnInitializeShapes_Click(object sender, EventArgs e)
        {
            Shape1.Clear();
            Shape2.Clear();
            Point p1a = new Point(20, 30);
            Point p2a = new Point(120, 50);
            Point p3a = new Point(160, 80);
            Point p4a = new Point(180, 300);
            Point p5a = new Point(100, 220);
            Point p6a = new Point(50, 280);
            Point p7a = new Point(20, 140);
            Shape1.Add(p1a);
            Shape1.Add(p2a);
            Shape1.Add(p3a);
            Shape1.Add(p4a);
            Shape1.Add(p5a);
            Shape1.Add(p6a);
            Shape1.Add(p7a);
            Transformation T = new Transformation();
            T.A = 1.05; T.B = 0.05; T.T1 = 15; T.T2 = 22;
            Shape2 = ApplyTransformation(T, Shape1);
            Shape2[2] = new Point(Shape2[2].X + 10, Shape2[2].Y + 3);// change one point
            // add outliers to both shapes 
            Point ptOutlier1 = new Point(200, 230);
            Shape1.Add(ptOutlier1);
            Point ptOutLier2 = new Point(270, 160);
            Shape2.Add(ptOutLier2);
            /*Point ptOutlier3 = new Point(100, 160);
            Shape1.Add(ptOutlier3);
            Point ptOutLier4 = new Point(80, 110);
            Shape2.Add(ptOutLier4);*/


            Pen pRed = new Pen(Brushes.Red, 1);
            Pen pBlue = new Pen(Brushes.Blue, 1);
            Graphics g = panInitial.CreateGraphics();
            DisplayShape(Shape1, pBlue, g);
            DisplayShape(Shape2, pRed, g);
        }

        void DisplayShape(List<Point> Shp, Pen pen, Graphics g)
        {
            Point? prev = null;
            foreach (Point point in Shp)
            {
                g.DrawEllipse(pen, new Rectangle(point.X - 2, point.Y - 2, 4, 4));
                if (prev != null)
                {
                    g.DrawLine(pen, point, (Point)prev);
                }
                prev = point;
            }
            g.DrawLine(pen, Shp[0], Shp[Shp.Count - 1]);
        }

        List<Point> ApplyTransformation(Transformation T, List<Point> ShapeToTranform)
        {
            double xPrime;
            double yPrime;
            List<Point> Transformed = new List<Point>();
            foreach (Point p in ShapeToTranform)
            {
                xPrime = T.A * p.X + T.B * p.Y + T.T1;
                yPrime = -1 * T.B * p.X + T.A * p.Y + T.T2;
                Transformed.Add(new Point((int)xPrime, (int)yPrime));
            }
            return Transformed;
        }

        private void btnApplyTransformation_Click(object sender, EventArgs e)
        {
            Transformation align = LLSOTransform.ComputeTransformation(Shape1, Shape2);
            MessageBox.Show("Cost = " + LLSOTransform.ComputeCost(Shape1, Shape2, align).ToString());
            List<Point> Shape2Transformed = ApplyTransformation(align, Shape2);
            Pen pRed = new Pen(Brushes.Red, 1);
            Pen pBlue = new Pen(Brushes.Blue, 1);
            Graphics g = panTransformed.CreateGraphics();
            DisplayShape(Shape1, pBlue, g);
            DisplayShape(Shape2Transformed, pRed, g);
        }

        private void btnRemoveOutliers_Click(object sender, EventArgs e)
        {
            //Outlier Removal using Exhaustive Evaluation:
            //List<Point>[] shapesNoOutliers=ExhaustiveEvaluation();
            //Outlier Removal using RANSAC:
            //Transformation T = new Transformation();
            List<Point>[] shapesNoOutliers=Ransac(((int)Shape2.Count/2),10,10,6,out Transformation T);
            List<Point> Shape1NoOutliers = shapesNoOutliers[0];
            List<Point> Shape2NoOutliers = ApplyTransformation(T,shapesNoOutliers[1]);

            Pen pRed = new Pen(Brushes.Red, 1);
            Pen pBlue = new Pen(Brushes.Blue, 1);
            Graphics g = panNoOutliers.CreateGraphics();
            DisplayShape(Shape1NoOutliers, pBlue, g);
            DisplayShape(Shape2NoOutliers, pRed, g);

        }
        List<Point>[] ExhaustiveEvaluation()
        {
            List<Point> Shape1Temp;
            List<Point> Shape2Temp;
            List<Point> Shape1NoOutliers = new List<Point>(Shape1);
            List<Point> Shape2NoOutliers = new List<Point>(Shape2);
            Transformation transform = LLSOTransform.ComputeTransformation(Shape1, Shape2);
            double prevcost = LLSOTransform.ComputeCost(Shape1, Shape2, transform);
            double costwithoutI = 0;
            int count = Shape1NoOutliers.Count;
            int i = 0;
            while (i < count)
            {
                Shape1Temp = new List<Point>(Shape1NoOutliers);
                Shape2Temp = new List<Point>(Shape2NoOutliers);
                Shape1Temp.RemoveAt(i);
                Shape2Temp.RemoveAt(i);
                transform = LLSOTransform.ComputeTransformation(Shape1Temp, Shape2Temp);
                costwithoutI = LLSOTransform.ComputeCost(Shape1Temp, Shape2Temp, transform);
                //MessageBox.Show("New Cost = " + costwithoutI.ToString());
                if (costwithoutI < (prevcost * 0.8))
                {
                    Shape1NoOutliers.RemoveAt(i);
                    Shape2NoOutliers.RemoveAt(i);
                    Shape2NoOutliers = ApplyTransformation(transform, Shape2NoOutliers);
                    i--;//since every element with greater index is moved to the rigth
                    count--;
                    prevcost = costwithoutI;
                    MessageBox.Show("New Cost = " + costwithoutI.ToString());
                }
                i++;
            }
            List<Point>[] shapesNoOutliers = { Shape1NoOutliers, Shape2NoOutliers };
            return shapesNoOutliers;

        }
        List<Point>[] Ransac(int minPointsNum,int iterNum,double threshold,int requiredPointsNum, out Transformation bestModel)
        {
            //data is Shape1, Shape2 globally accessible
            //List<Point> Shape1Temp= new List<Point>(Shape1);
            //List<Point> Shape2Temp=new List<Point>(Shape2);
            bestModel = new Transformation();
            List<Point>[] bestConsensusSet = new List<Point>[2];
            double bestError = 10000;   
            int iteration = 0;
            List<Point>[] maybeInliers = new List<Point>[2];
            List<Point>[] maybeOutliers = new List<Point>[2];
            //maybeOutliers[0] = new List<Point>(Shape1);
            //maybeOutliers[1] = new List<Point>(Shape2);
            Transformation maybeModel = new Transformation();
            List<Point>[] consensusSet = new List<Point>[2];
            Random random = new Random();
            while (iteration < iterNum)
            {
                maybeOutliers[0] = new List<Point>(Shape1);
                maybeOutliers[1] = new List<Point>(Shape2);
                maybeInliers[0] = new List<Point>();
                maybeInliers[1] = new List<Point>();
                int[] intArray = Enumerable.Range(0, Shape1.Count).OrderBy(t => random.Next()).Take(minPointsNum).ToArray();//Range(start,countOfElem)
                //Array.Sort(intArray);
                foreach (int x in intArray)
                {
                    maybeInliers[0].Add(Shape1[x]);
                    maybeInliers[1].Add(Shape2[x]);
                    maybeOutliers[0].Remove(Shape1[x]);
                    maybeOutliers[1].Remove(Shape2[x]);                    
                }
                maybeModel = LLSOTransform.ComputeTransformation(maybeInliers[0], maybeInliers[1]);
                consensusSet[0] = new List<Point>(maybeInliers[0]);
                consensusSet[1] = new List<Point>(maybeInliers[1]);
                //for every point in data not in maybe_inliers, meaning maybe_outliers

                for (int i = 0; i < maybeOutliers[0].Count; i++)
                {
                    if(LLSOTransform.ComputeCost(maybeOutliers[0][i], maybeOutliers[1][i], maybeModel) < threshold)
                    {
                        consensusSet[0].Add(maybeOutliers[0][i]);
                        consensusSet[1].Add(maybeOutliers[1][i]);
                    }
                }
                if (consensusSet[0].Count > requiredPointsNum)
                {
                    //implies that we may have found a good model, now test how good it is
                    Transformation better_model= LLSOTransform.ComputeTransformation(consensusSet[0], consensusSet[1]);
                    double thisError = LLSOTransform.ComputeCost(consensusSet[0], consensusSet[1], better_model);
                    if (thisError < bestError)
                    {
                        //we have found a model which is better than any of the previous ones
                        bestModel.A = better_model.A;
                        bestModel.B = better_model.B;
                        bestModel.T1 = better_model.T1;
                        bestModel.T2 = better_model.T2;
                        bestConsensusSet[0] = new List<Point>(consensusSet[0]);
                        bestConsensusSet[1] = new List<Point>(consensusSet[1]);
                        bestError = thisError;
                    }
                }
                iteration++;
            }
            return bestConsensusSet;
        }
        
    }
}
