namespace DigitViewer
{
    class Digit
    {
        public bool [,] pixels = new bool[28,28];

        public Digit(bool[,] pixels)
        {
            this.pixels = pixels;
        }

        public Digit()
        {
        }
    }
}
