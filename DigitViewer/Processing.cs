using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

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
            Euler(digit);
            Intersections(digit);
            Lines(digit);
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
        #region Extremes
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
        #endregion
        #region Intersection
        public static void Intersections(Digit digit)
        {
            //if ((i + 4) % 7 == 0)
            // horizontal intersections
            int horizontals = 0;
            int tmp = 0;
            for (int y = 0; y < 28; y++)
            {
                if ((y + 4) % 7 != 0) continue;
                for (int x = 1; x < 28; x++)
                {
                    if (!digit.pixels[y, x - 1] && digit.pixels[y, x])
                    {
                        tmp = 1;
                    }
                    else if (digit.pixels[y, x])
                    {
                        tmp++;
                    }
                    else if (tmp < 10 && tmp != 0)
                    {
                        horizontals++;
                        tmp = 0;
                    }
                }
            }

            // vertical intersections
            int verticals = 0;
            for (int x = 0; x < 28; x++)
            {
                if ((x + 4) % 7 != 0) continue;
                for (int y = 1; y < 28; y++)
                {
                    if (!digit.pixels[y - 1, x] && digit.pixels[y, x])
                    {
                        tmp = 1;
                    }
                    else if (digit.pixels[y, x])
                    {
                        tmp++;
                    }
                    else if (tmp < 10 && tmp != 0)
                    {
                        verticals++;
                        tmp = 0;
                    }
                }
            }
            Attributes.HorizontalIntersections = horizontals;
            Attributes.VerticalIntersections = verticals;
        }
        #endregion
        #region DistanceVariable
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
            var topLeftToBotRight = (int)(Math.Sqrt(Math.Pow(topLeft.X - botRight.X, 2) + Math.Pow(topLeft.Y - botRight.Y, 2)));

            var topRightToBotLeft = (int)(Math.Sqrt(Math.Pow(topRight.X - botLeft.X, 2) + Math.Pow(topRight.Y - botLeft.Y, 2)));

            Attributes.TopLeftToBotRight = topLeftToBotRight;
            Attributes.TopRightToBotLeft = topRightToBotLeft;
        }
        #endregion
        #region Euler
        public static void Euler(Digit digit)
        {
            double eulerNumber4 = 0.0;
            double eulerNumber8 = 0.0;
            bool[,] pixels = digit.pixels;

            bool[,] S1 = setS1();
            bool[,] S3 = setS3();
            bool[,] X = setX();

            int numOfS1 = 0;
            int numOfS3 = 0;
            int numOfX = 0;


            for (int i = 0; i < 28; i++)
            {
                for (int j = 0; j < 28; j++)
                {
                    if (pixels[i, j] == true)
                    {
                        bool[,] pxlneigbors = GetNeighbours(i, j, pixels);

                        for (int k = 1; k <= 4; k++)
                        {
                            var subset = SubsetNeigbours(k, pxlneigbors);
                            for (int index = 0; index <= 3; index++)
                            {
                                bool[] rowS1 = GetRow(S1, index);
                                bool[] rowS3 = GetRow(S3, index);

                                if (subset.SequenceEqual(rowS1)) numOfS1++;
                                if (subset.SequenceEqual(rowS3)) numOfS3++;

                            }

                            for (int index = 0; index < 2; index++)
                            {
                                bool[] rowX = GetRow(X, index);
                                if (subset.SequenceEqual(rowX)) numOfX++;

                            }
                        }
                    }

                }
            }

            eulerNumber4 = 0.25 * (numOfS1 - numOfS3 + 2 * numOfX);
            eulerNumber8 = 0.25 * (numOfS1 - numOfS3 - 2 * numOfX);

            Attributes.Euler4 = Convert.ToInt32(eulerNumber4);
            Attributes.Euler8 = Convert.ToInt32(eulerNumber8);
        }

        private static bool[,] GetNeighbours(int i, int j, bool[,] pixels)
        {

            var neigbours = new bool[3, 3];

            if (i > 0 && j < 27) neigbours[0, 2] = (pixels[i - 1, j + 1]);
            if (i > 0) neigbours[0, 1] = (pixels[i - 1, j]);
            if (i > 0 && j > 0) neigbours[0, 0] = (pixels[i - 1, j - 1]);

            if (j < 27) neigbours[1, 2] = (pixels[i, j + 1]);
            neigbours[1, 1] = (pixels[i, j]);
            if (j > 0) neigbours[1, 0] = (pixels[i, j - 1]);


            if (i < 27 && j < 27) neigbours[2, 2] = (pixels[i + 1, j + 1]);
            if (i < 27) neigbours[2, 1] = (pixels[i + 1, j]);
            if (i < 27 && j > 0) neigbours[2, 0] = (pixels[i + 1, j - 1]);

            return neigbours;
        }

        private static bool[,] setS1()
        {
            var S1 = new[,]
            {
                {true, false, false, false},
                {false, true, false, false},
                {false, false, true, false},
                {false, false, false, true},
            };
            return S1;
        }

        private static bool[,] setS3()
        {
            var S3 = new[,]
            {
                {false, true, true, true},
                {true, false, true, true},
                {true, true, false, true},
                {true, true, true, false},
            };
            return S3;
        }

        private static bool[,] setX()
        {
            var X = new[,]
            {
                {false, true, true, false},
                {true, false, false, true},
            };
            return X;
        }
        private static bool[] SubsetNeigbours(int type, bool[,] pxlneigbors)
        {
            var neighbours = new bool[4];


            switch (type)
            {
                case 1:
                    for (int i = 0; i < 3; i++)
                        for (int j = 0; j < 3; j++)
                        {
                            if (i > 0 && j < 2) neighbours[0] = pxlneigbors[i - 1, j + 1];
                            if (j < 2) neighbours[1] = pxlneigbors[i, j + 1];
                            if (i > 0) neighbours[2] = pxlneigbors[i - 1, j];
                            neighbours[3] = pxlneigbors[i, j];
                        }
                    break;
                case 2:
                    for (int i = 0; i < 3; i++)
                        for (int j = 0; j < 3; j++)
                        {
                            if (j < 2) neighbours[0] = pxlneigbors[i, j + 1];
                            if (i < 2 && j < 2) neighbours[1] = pxlneigbors[i + 1, j + 1];
                            neighbours[2] = pxlneigbors[i, j];
                            if (i < 2 && j > 0) neighbours[3] = pxlneigbors[i + 1, j - 1];

                        }
                    break;
                case 3:
                    for (int i = 0; i < 3; i++)
                        for (int j = 0; j < 3; j++)
                        {
                            if (i > 0) neighbours[0] = pxlneigbors[i - 1, j];
                            neighbours[1] = pxlneigbors[i, j];
                            if (i > 0 && j > 0) neighbours[2] = pxlneigbors[i - 1, j - 1];
                            if (j > 0) neighbours[3] = pxlneigbors[i, j - 1];

                        }
                    break;
                case 4:
                    for (int i = 0; i < 3; i++)
                        for (int j = 0; j < 3; j++)
                        {
                            neighbours[0] = pxlneigbors[i, j];
                            if (i < 2) neighbours[1] = pxlneigbors[i + 1, j];
                            if (j > 0) neighbours[2] = pxlneigbors[i, j - 1];
                            if (i < 2 && j > 0) neighbours[3] = pxlneigbors[i + 1, j - 1];

                        }
                    break;

            }

            return neighbours;
        }
        public static T[] GetRow<T>(T[,] matrix, int row)
        {
            var columns = matrix.GetLength(1);
            var array = new T[columns];
            for (int i = 0; i < columns; ++i)
                array[i] = matrix[row, i];
            return array;
        }
        #endregion
        #region Lines
        public static void Lines(Digit digit)
        {
            // Horizontals
            Attributes.Horizontal1 = TraverseHoriz1(digit);
            Attributes.Horizontal2 = TraverseHoriz2(digit);
            // Verticals
            Attributes.Vertical1 = TraverseVert1(digit);
            Attributes.Vertical2 = TraverseVert2(digit);
            // Main diagonal
            Attributes.MainDiagonal1 = TraverseMainDiag1(digit);
            Attributes.MainDiagonal2 = TraverseMainDiag2(digit);
            // Other diagonal
            Attributes.Diagonal1 = TraverseDiag1(digit);
            Attributes.Diagonal2 = TraverseDiag2(digit);
        }

        private static Line ReplaceIfLonger(int currL, Point newS, Point newE, int newL)
        {
            if (newL <= currL) return null;
            var l = new Line
            {
                start = new Point(newS.X, newS.Y),
                end = new Point(newE.X, newE.Y),
                length = newL
            };
            return l;
        }

        private static Line TraverseHoriz1(Digit digit)
        {
            var line = new Line();
            var currLength = 0;
            var start1 = new Point();
            var j = 0;
            var pixels = digit.pixels;
            for (var i = 0; i < 14; i++)
            {
                for (j = 0; j < 28; j++)
                {
                    if (!pixels[i, j])
                    {
                        line = ReplaceIfLonger(line.length, start1, new Point(i, j - 1), currLength) ?? line;
                        currLength = 0;
                    }
                    else
                    {
                        if (currLength == 0)
                            start1 = new Point(i, j);
                        currLength++;
                    }
                }
                line = ReplaceIfLonger(line.length, start1, new Point(i, j - 1), currLength) ?? line;
            }
            return line;
        }
        private static Line TraverseHoriz2(Digit digit)
        {
            var line = new Line();
            var currLength = 0;
            var start1 = new Point();
            var j = 0;
            var pixels = digit.pixels;
            for (var i = 14; i < 28; i++)
            {
                for (j = 0; j < 28; j++)
                {
                    if (!pixels[i, j])
                    {
                        line = ReplaceIfLonger(line.length, start1, new Point(i, j - 1), currLength) ?? line;
                        currLength = 0;
                    }
                    else
                    {
                        if (currLength == 0)
                            start1 = new Point(i, j);
                        currLength++;
                    }
                }
                line = ReplaceIfLonger(line.length, start1, new Point(i, j - 1), currLength) ?? line;
            }
            return line;
        }
        private static Line TraverseVert1(Digit digit)
        {
            var line = new Line();
            var currLength = 0;
            var start1 = new Point();
            var j = 0;
            var pixels = digit.pixels;
            for (var i = 0; i < 28; i++)
            {
                for (j = 0; j < 14; j++)
                {
                    if (!pixels[j, i])
                    {
                        line = ReplaceIfLonger(line.length, start1, new Point(j - 1, i), currLength) ?? line;
                        currLength = 0;
                    }
                    else
                    {
                        if (currLength == 0)
                            start1 = new Point(j, i);
                        currLength++;
                    }
                }
                line = ReplaceIfLonger(line.length, start1, new Point(j - 1, i), currLength) ?? line;
            }
            return line;
        }
        private static Line TraverseVert2(Digit digit)
        {
            var line = new Line();
            var currLength = 0;
            var start1 = new Point();
            var j = 0;
            var pixels = digit.pixels;
            for (var i = 0; i < 28; i++)
            {
                for (j = 14; j < 28; j++)
                {
                    if (!pixels[j, i])
                    {
                        line = ReplaceIfLonger(line.length, start1, new Point(j - 1, i), currLength) ?? line;
                        currLength = 0;
                    }
                    else
                    {
                        if (currLength == 0)
                            start1 = new Point(j, i);
                        currLength++;
                    }
                }
                line = ReplaceIfLonger(line.length, start1, new Point(j - 1, i), currLength) ?? line;
            }
            return line;
        }
        private static Line TraverseMainDiag1(Digit digit)
        {
            var line = new Line();
            var currLength = 0;
            var start1 = new Point();
            var pixels = digit.pixels;
            for (var i = 0; i < 28; i++)
            {
                var x = i;
                var y = 0;
                while (x >= 0 && y >= 0 && x < 28 && y < 28)
                {
                    if (!pixels[y, x])
                    {
                        line = ReplaceIfLonger(line.length, start1, new Point(y - 1, x + 1), currLength) ?? line;
                        currLength = 0;
                    }
                    else
                    {
                        if (currLength == 0)
                            start1 = new Point(y, x);
                        currLength++;
                    }

                    y++;
                    x--;
                }
                line = ReplaceIfLonger(line.length, start1, new Point(y - 1, x + 1), currLength) ?? line;
            }
            return line;
        }
        private static Line TraverseMainDiag2(Digit digit)
        {
            var line = new Line();
            var currLength = 0;
            var start1 = new Point();
            var pixels = digit.pixels;
            for (var i = 0; i < 28; i++)
            {
                var x = i;
                var y = 27;
                while (x >= 0 && y >= 0 && x < 28 && y < 28)
                {
                    if (!pixels[y, x])
                    {
                        line = ReplaceIfLonger(line.length, start1, new Point(y + 1, x - 1), currLength) ?? line;
                        currLength = 0;
                    }
                    else
                    {
                        if (currLength == 0)
                            start1 = new Point(y, x);
                        currLength++;
                    }

                    y--;
                    x++;
                }
                line = ReplaceIfLonger(line.length, start1, new Point(y + 1, x - 1), currLength) ?? line;
            }
            return line;
        }
        private static Line TraverseDiag1(Digit digit)
        {
            var line = new Line();
            var currLength = 0;
            var start1 = new Point();
            var pixels = digit.pixels;
            for (var i = 0; i < 28; i++)
            {
                var x = i;
                var y = 27;
                while (x >= 0 && y >= 0 && x < 28 && y < 28)
                {
                    if (!pixels[y, x])
                    {
                        line = ReplaceIfLonger(line.length, start1, new Point(y + 1, x + 1), currLength) ?? line;
                        currLength = 0;
                    }
                    else
                    {
                        if (currLength == 0)
                            start1 = new Point(y, x);
                        currLength++;
                    }

                    y--;
                    x--;
                }
                line = ReplaceIfLonger(line.length, start1, new Point(y + 1, x + 1), currLength) ?? line;
            }
            return line;
        }
        private static Line TraverseDiag2(Digit digit)
        {
            var line = new Line();
            var currLength = 0;
            var start1 = new Point();
            var pixels = digit.pixels;
            for (var i = 0; i < 28; i++)
            {
                var x = 27;
                var y = i;
                while (x >= 0 && y >= 0 && x < 28 && y < 28)
                {
                    if (!pixels[y, x])
                    {
                        line = ReplaceIfLonger(line.length, start1, new Point(y + 1, x + 1), currLength) ?? line;
                        currLength = 0;
                    }
                    else
                    {
                        if (currLength == 0)
                            start1 = new Point(y, x);
                        currLength++;
                    }

                    y--;
                    x--;
                }
                line = ReplaceIfLonger(line.length, start1, new Point(y + 1, x + 1), currLength) ?? line;
            }
            return line;
        }
        #endregion
    }
}