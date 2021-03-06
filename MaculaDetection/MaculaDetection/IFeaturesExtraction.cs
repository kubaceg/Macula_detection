﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool
//     Changes to this file will be lost if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
namespace EyeDiseaseRecognition.Core
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
    using System.Drawing;
    using System.Xml;

	public interface IFeaturesExtraction 
	{
		Examination Examination { get;set; }

		Image Image { get;set; }

		void Process();

		void AddImage(Bitmap image);

		void AddImageSequence(List<Bitmap> images);

        XmlDocument SaveMetadata();

		void GetDependencies();

	}
}

