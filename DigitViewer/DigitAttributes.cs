using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace DigitViewer
{
    class DigitAttributes
    {
        public int MinX;
        public int MaxX;
        public int MinY;
        public int MaxY;
        public int TopLeftToBotRight;
        public int TopRightToBotLeft;
        public Point North, West, South, East;
        public int Euler4, Euler8;
        public int HorizontalIntersections;
        public int VerticalIntersections;
        public Line Horizontal1, Horizontal2, Vertical1, Vertical2;
        public Line MainDiagonal1, MainDiagonal2, Diagonal1, Diagonal2;
        public override String ToString()
        {
            var sb = new StringBuilder();
            sb.Append(TopLeftToBotRight + ",");
            sb.Append(TopRightToBotLeft + ",");
            sb.Append(North.Y + ",");
            sb.Append(North.X + ",");
            sb.Append(West.Y + ",");
            sb.Append(West.X + ",");
            sb.Append(East.Y + ",");
            sb.Append(East.X + ",");
            sb.Append(South.Y + ",");
            sb.Append(South.X + ",");
            sb.Append(Euler8 + ",");
            sb.Append(HorizontalIntersections + ",");
            sb.Append(VerticalIntersections + ",");
            sb.Append(Horizontal1.ToString());
            sb.Append(Horizontal2.ToString());
            sb.Append(Vertical1.ToString());
            sb.Append(Vertical2.ToString());
            sb.Append(MainDiagonal1.ToString());
            sb.Append(MainDiagonal2.ToString());
            sb.Append(Diagonal1.ToString());
            sb.Append(Diagonal2.ToString());
            sb.Remove(sb.Length - 1, 1);
            return sb.ToString();
        }

        public double TotalIntersections
        {
            get { return VerticalIntersections + HorizontalIntersections; }

        }
    }
}
