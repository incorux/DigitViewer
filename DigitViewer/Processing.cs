using System.Collections.Generic;

namespace DigitViewer
{
    static class Processing
    {
        /// <summary>
        /// Finds minimal x,y and maximum x,y
        /// </summary>
        /// <param name="digit"></param>
        /// <param name="attributes"></param>
        public static void MinMax(Digit digit, List<int> attributes)
        {
            int minX = 0, maxX = 0;
            var pixels = digit.pixels;
            bool gotmin = false, gotmax = false;
            for (var x = 0; x < 28; x++)
            {
                for (var y = 0; y < 28; y++)
                {
                    if (!gotmin && pixels[x, y])
                    {
                        minX = x;
                        gotmin = true;
                    }
                    if (!gotmax && pixels[27 - x, y])
                    {
                        maxX = 27 - x;
                        gotmax = true;
                    }
                }
                if (gotmax && gotmin) break;
            }

            int minY = 0, maxY = 0;
            gotmin = false; gotmax = false;
            for (var y = 0; y < 28; y++)
            {
                for (var x = 0; x < 28; x++)
                {
                    if (!gotmin && pixels[x, y])
                    {
                        minY = y;
                        gotmin = true;
                    }
                    if (!gotmax && pixels[x, 27 - y])
                    {
                        maxY = 27 - y;
                        gotmax = true;
                    }
                }
                if (gotmax && gotmin) break;
            }

            attributes.AddRange(new[] { minX, maxX, minY, maxY });
        }
    }
}
