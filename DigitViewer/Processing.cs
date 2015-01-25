using System;
using System.Collections.Generic;
using System.Drawing;
using System.Security.Policy;

namespace DigitViewer
{
    static class Processing
    {
        public static DigitAttributes Attributes;

        public static void ProcessAll(Digit digit)
        {
            Attributes = new DigitAttributes();
            MinMax(digit);
            Extremes(digit);
            DistanceVariable(digit);
        }
        /// <summary>
        /// Finds minimal x,y and maximum x,y
        /// </summary>
        /// <param name="digit"></param>
        /// <param name="attributes"></param>
        public static void MinMax(Digit digit)
        {
            int minX = 0, maxX = 0;
            var pixels = digit.pixels;
            bool gotmin = false, gotmax = false;
            int minY = 0, maxY = 0;
            for (var y = 0; y < 28; y++)
            {
                for (var x = 0; x < 28; x++)
                {
                    if (!gotmin && pixels[y, x])
                    {
                        minY = y;
                        gotmin = true;
                    }
                    if (!gotmax && pixels[27 - y, x])
                    {
                        maxY = 27 - y;
                        gotmax = true;
                    }
                }
                if (gotmax && gotmin) break;
            }
            gotmin = false; gotmax = false;
            for (var x = 0; x < 28; x++)
            {
                for (var y = 0; y < 28; y++)
                {
                    if (!gotmin && pixels[y, x])
                    {
                        minX = x;
                        gotmin = true;
                    }
                    if (!gotmax && pixels[y, 27 - x])
                    {
                        maxX = 27 - x;
                        gotmax = true;
                    }
                }
                if (gotmax && gotmin) break;
            }

            Attributes.MinX = minX;
            Attributes.MaxX = maxX;
            Attributes.MinY = minY;
            Attributes.MaxY = maxY;
        }
        public static void Extremes(Digit digit)
        {
            var north = Attributes.MinY;
            var east = Attributes.MaxX;
            var west = Attributes.MinX;
            var south = Attributes.MaxY;
            Point northPoint = new Point(), westPoint = new Point(), eastPoint = new Point(), southPoint = new Point();
            bool gotNorth = false, gotWest = false, gotEast = false, gotSouth = false;
            #region Mainloop
            for (int i = 0; i < 14 && (!gotNorth || !gotEast || !gotWest || !gotSouth); i++)
            {
                // WEST
                if (!gotWest && digit.pixels[13 + i, west])
                {
                    westPoint = new Point(west, 13 + i);
                    gotWest = true;
                }
                if (!gotWest && digit.pixels[13 - i, west])
                {
                    westPoint = new Point(west, 13 - i);
                    gotWest = true;
                }
                // NORTH
                if (!gotNorth && digit.pixels[north, 13 + i])
                {
                    northPoint = new Point(13 + i, north);
                    gotNorth = true;
                }
                if (!gotNorth && digit.pixels[north, 13 - i])
                {
                    northPoint = new Point(13 - i, north);
                    gotNorth = true;
                }
                // EAST
                if (!gotEast && digit.pixels[13 + i, east])
                {
                    eastPoint = new Point(east, 13 + i);
                    gotEast = true;
                }
                if (!gotEast && digit.pixels[13 - i, east])
                {
                    eastPoint = new Point(east, 13 - i);
                    gotEast = true;
                }
                // SOUTH
                if (!gotSouth && digit.pixels[south, 13 + i])
                {
                    southPoint = new Point(13 + i, south);
                    gotSouth = true;
                }
                if (!gotSouth && digit.pixels[south, 13 - i])
                {
                    southPoint = new Point(13 - i, south);
                    gotSouth = true;
                }
            }
            #endregion
            Attributes.North = northPoint;
            Attributes.South = southPoint;
            Attributes.West = westPoint;
            Attributes.East = eastPoint;
        }
        public static void DistanceVariable(Digit digit)
        {
            var pixels = digit.pixels;
            var topLeft = new Point(Attributes.MinX, Attributes.MinY);
            var topRight = new Point(Attributes.MaxX, Attributes.MinY);
            var botLeft = new Point(Attributes.MinX, Attributes.MaxY);
            var botRight = new Point(Attributes.MaxX, Attributes.MaxY);
            bool gotTopLeft = false, gotTopRight = false, gotBotLeft = false, gotBotRight = false;
            #region Main Loop
            for (int i = 0; i < 28 && (!gotTopLeft || !gotTopRight || !gotBotLeft || !gotBotRight); i++)
            {
                // TOPLEFT
                if (!gotTopLeft && pixels[topLeft.Y + i, topLeft.X + i])
                {
                    topLeft.Y += i;
                    topLeft.X += i;
                    gotTopLeft = true;
                }
                if (!gotTopLeft && pixels[topLeft.Y + i + 1, topLeft.X + i])
                {
                    topLeft.Y += i + 1;
                    topLeft.X += i;
                    gotTopLeft = true;
                }
                if (!gotTopLeft && pixels[topLeft.Y + i, topLeft.X + i + 1])
                {
                    topLeft.Y += i;
                    topLeft.X += i + 1;
                    gotTopLeft = true;
                }
                // BOTRIGHT
                if (!gotBotRight && pixels[botRight.Y - i, botRight.X - i])
                {
                    botRight.Y -= i;
                    botRight.X -= i;
                    gotBotRight = true;
                }
                if (!gotBotRight && pixels[botRight.Y - i - 1, botRight.X - i])
                {
                    botRight.Y -= i + 1;
                    botRight.X -= i;
                    gotBotRight = true;
                }
                if (!gotBotRight && pixels[botRight.Y - i, botRight.X - i - 1])
                {
                    botRight.Y -= i;
                    botRight.X -= i + 1;
                    gotBotRight = true;
                }
                // TOPRIGHT
                if (!gotTopRight && pixels[topRight.Y + i, topRight.X - i])
                {
                    topRight.Y += i;
                    topRight.X -= i;
                    gotTopRight = true;
                }
                if (!gotTopRight && pixels[topRight.Y + i + 1, topRight.X - i])
                {
                    topRight.Y += i + 1;
                    topRight.X -= i;
                    gotTopRight = true;
                }
                if (!gotTopRight && pixels[topRight.Y + i, topRight.X - i - 1])
                {
                    topRight.Y += i;
                    topRight.X -= i + 1;
                    gotTopRight = true;
                }
                // BOTLEFT
                if (!gotBotLeft && pixels[botLeft.Y - i, botLeft.X + i])
                {
                    botLeft.Y -= i;
                    botLeft.X += i;
                    gotBotLeft = true;
                }
                if (!gotBotLeft && pixels[botLeft.Y - i - 1, botLeft.X + i])
                {
                    botLeft.Y -= i + 1;
                    botLeft.X += i;
                    gotBotLeft = true;
                }
                if (!gotBotLeft && pixels[botLeft.Y - i, botLeft.X + i + 1])
                {
                    botLeft.Y -= i;
                    botLeft.X += i + 1;
                    gotBotLeft = true;
                }
            }
            #endregion
            var TopLeftToBotRight = Math.Sqrt(Math.Pow(topLeft.X - botRight.X, 2) + Math.Pow(topLeft.Y - botRight.Y, 2));

            var TopRightToBotLeft = Math.Sqrt(Math.Pow(topRight.X - botLeft.X, 2) + Math.Pow(topRight.Y - botLeft.Y, 2));

            Attributes.TopLeftToBotRight = TopLeftToBotRight;
            Attributes.TopRightToBotLeft = TopRightToBotLeft;
        }
    }
}