using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.Util;
using Emgu.CV.CvEnum;
using Emgu.CV.UI;
using Emgu.CV.Features2D;

using System.Xml;
using System.Xml.Serialization;

namespace EyeDiseaseRecognition.Core
{
    class MaculaExtraction : IFeaturesExtraction
    {
        private List<Bitmap> bitmaps = new List<Bitmap>();
        private List<Result> results = new List<Result>();

        public Examination Examination
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public Image Image
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public void Process()
        {
            ///margines aby wykluczyc pozostale znalezione punkty
            float selectionMargin = 0.25f;
            List<Point> points = new List<Point>();
            int minArea = 1000;
            float maxSize = 0f;
            float resultX = 0f;
            float resultY = 0f;
            float resultR = 0f;
            float sumX = 0f, sumY = 0f;


            foreach (Bitmap bitmap in bitmaps)
            {
                points.Clear();
                resultX = 0f;
                resultY = 0f;
                resultR = 0f;
                sumX = 0f;
                sumY = 0f;

                using (Image<Gray, Byte> img = new Image<Gray, byte>(bitmap))
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



                    foreach (Point p in points)
                    {
                        sumX += p.X; sumY += p.Y;

                    }

                    resultX = sumX / points.Count;
                    resultY = sumY / points.Count;
                    resultR = maxSize / 2;

                    results.Add(new Result(resultX, resultY, resultR));

                }
            }
        }

        public void AddImage(Bitmap image)
        {
            bitmaps.Add(image);
        }

        public void AddImageSequence(List<Bitmap> images)
        {
            bitmaps.AddRange(images);
        }

        public System.Xml.XmlDocument SaveMetadata()
        {
            XmlDocument xmlDoc = new XmlDocument();
            XmlNode plamkaZolta;
            foreach (Result result in results)
            {
                plamkaZolta = xmlDoc.CreateElement("plamkaZolta");
                xmlDoc.AppendChild(plamkaZolta);

                XmlNode elementNode = xmlDoc.CreateElement("X");
                elementNode.InnerText = result.x.ToString();
                plamkaZolta.AppendChild(elementNode);

                elementNode = xmlDoc.CreateElement("Y");
                elementNode.InnerText = result.y.ToString();
                plamkaZolta.AppendChild(elementNode);

                elementNode = xmlDoc.CreateElement("promien");
                elementNode.InnerText = result.r.ToString();
                plamkaZolta.AppendChild(elementNode);

            }

            return xmlDoc;
        }

        public void GetDependencies()
        {
            throw new NotImplementedException();
        }
    }
}
