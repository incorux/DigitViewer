using System;
using System.Collections.Generic;
using System.Drawing;

namespace DigitViewer
{
    class DigitAttributes
    {
        public int MinX;
        public int MaxX;
        public int MinY;
        public int MaxY;
        public double TopLeftToBotRight;
        public double TopRightToBotLeft;
        public Point North, West, South, East;

        public List<int> ToList()
        {
            var list = new List<int>();
            list.AddRange(new[] { 
                MinX, MaxX, MinY, MaxY,
                North.X, North.Y, East.X, East.Y, South.X, South.Y, West.X, West.Y
            });
            return list;
        }
    }
}
