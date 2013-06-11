using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.Util;
using Emgu.CV.CvEnum;
using Emgu.CV.UI;
using Emgu.CV.Features2D;

namespace IW_Detection
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //pierwszy czarnobialy obrazek
            String photoString = "";

            //String photoPath = @"C:\Users\Michał\Downloads\" + photoString + ".jpg";
            String photoPath = null;

            ///margines aby wykluczyc pozostale znalezione punkty
            float selectionMargin = 0.25f;
            List<Point> points = new List<Point>();
            int minArea = 1000;
            float maxSize = 0f;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                photoPath = openFileDialog1.InitialDirectory + openFileDialog1.FileName;
            }

            Bitmap photo = new Bitmap(photoPath);

            using (Image<Gray, Byte> img = new Image<Gray, byte>(photo))
            {
                MSERDetector mserDetector = new MSERDetector(5, 1440000, minArea, 0.25f, 0.2f, 200, 1.01, 0.00003, 10);
                Image<Gray, Byte> img2 = img.SmoothBlur(30, 30, true);
                MKeyPoint[] keyPoints = mserDetector.DetectKeyPoints(img, null);
                foreach (MKeyPoint p in keyPoints)
                {
                    if (((p.Point.X / img.Width > selectionMargin) && (p.Point.X / img.Width < 1 - selectionMargin))
                        && ((p.Point.Y / img.Height > selectionMargin) && (p.Point.Y / img.Height < 1 - selectionMargin)))
                    {

                        points.Add(new Point((int)(p.Point.X), (int)(p.Point.Y)));
                        if (p.Size > maxSize)
                            maxSize = p.Size;
                    }
                }


                float sumX = 0f, sumY = 0f;

                foreach (Point p in points)
                {
                    sumX += p.X; sumY += p.Y;

                }

                img.Draw(new CircleF(new PointF(sumX / points.Count, sumY / points.Count), 8), new Gray(), 10); 
                
                //img.DrawPolyline(points.ToArray(), true, new Gray(), 4);
                textBox1.Clear();
                textBox1.AppendText((sumX / points.Count).ToString() + ":" + (sumY / points.Count).ToString() + ":" + (maxSize/2).ToString());



                //imgWhite.Draw(points, new Gray(0), 10);
                //Show the image using ImageViewer from Emgu.CV.UI
                pictureBox2.Image = img.ToBitmap();
                img.Save(@"C:\Users\Michał\Downloads\nowy.jpg");

            }
        }
    }
}