using System;
using System.Drawing;
using System.Text;

namespace DigitViewer
{
    public class Line
    {
        public Point start = new Point();
        public Point end = new Point();
        public int length = 0;

        public String ToString()
        {
            var sb = new StringBuilder();
            sb.Append(start.Y + ",");
            sb.Append(start.X + ",");
            sb.Append(length + ",");
            return sb.ToString();
        }
    }
}
