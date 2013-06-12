using System;

namespace EyeDiseaseRecognition.Core
{

    public class Result
    {
        public float x { get; set; }
        public float y { get; set; }
        public float r { get; set; }

        public Result(float x, float y, float r)
        {
            this.x = x;
            this.y = y;
            this.r = r;
        }
    }
}