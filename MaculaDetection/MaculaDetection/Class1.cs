using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EyeDiseaseRecognition.Core;
using System.Drawing;
using System.Xml;
using System.Xml.Serialization;

namespace EyeDiseaseRecognition.Core
{
    class Class1
    {
        static void Main(string[] args)
        {
            MaculaExtraction me = new MaculaExtraction();

            string photo = @"C:\Users\kuba\Desktop\IW\baza_zdjec\PRAWIDLOWE_AF\15\198985_0m29.8s.jpg";

            Bitmap photo1 = new Bitmap(photo);

            me.AddImage(photo1);

            me.Process();

            XmlDocument document = me.SaveMetadata();

            document.Save("test.xml");
        }
    }
}
