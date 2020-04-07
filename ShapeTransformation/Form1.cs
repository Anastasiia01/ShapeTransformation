﻿using Mapack;
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
            foreach(Point point in Shp)
            {
                g.DrawEllipse(pen, new Rectangle(point.X - 2, point.Y-2, 4, 4));
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
            List <Point> Transformed = new List<Point>();
            foreach(Point p in ShapeToTranform)
            {
                xPrime = T.A * p.X + T.B * p.Y + T.T1;
                yPrime = -1*T.B * p.X + T.A * p.Y + T.T2;
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
            List<Point> Shape1Temp;
            List<Point> Shape2Temp;
            List<Point> Shape1NoOutliers = new List<Point>(Shape1);
            List<Point> Shape2NoOutliers = new List<Point>(Shape2);
            Transformation transform = LLSOTransform.ComputeTransformation(Shape1, Shape2);
            double prevcost = LLSOTransform.ComputeCost(Shape1, Shape2, transform);
            double costwithoutI = 0;
            double count = Shape1.Count;
            for (int i = 0; i < count; i++)
            {
                Shape1Temp = new List<Point>(Shape1);
                Shape2Temp = new List<Point>(Shape2);
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
                    //prevcost = costwithoutI;
                    MessageBox.Show("New Cost = " + costwithoutI.ToString());
                }
            }
            Pen pRed = new Pen(Brushes.Red, 1);
            Pen pBlue = new Pen(Brushes.Blue, 1);
            Graphics g = panNoOutliers.CreateGraphics();
            DisplayShape(Shape1NoOutliers, pBlue, g);
            DisplayShape(Shape2NoOutliers, pRed, g);

        }
    }
}
