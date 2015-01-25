using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using MLP_approximation;

namespace DigitViewer
{
    public partial class Form1 : Form
    {
        private static List<Digit> digits;
        private DigitImage[] trainImages;
        public Form1()
        {
            InitializeComponent();
        }

        public static DigitImage[] LoadMnistData(string pixelFile, string labelFile)
        {
            byte[][] pixels = new byte[28][];
            for (int i = 0; i < pixels.Length; ++i)
                pixels[i] = new byte[28];
            FileStream ifsPixels = new FileStream(pixelFile, FileMode.Open);
            FileStream ifsLabels = new FileStream(labelFile, FileMode.Open);
            BinaryReader brImages = new BinaryReader(ifsPixels);
            BinaryReader brLabels = new BinaryReader(ifsLabels);
            int magic1 = brImages.ReadInt32(); // stored as big endian
            int imageCount = brImages.ReadInt32();
            imageCount = ReverseBytes(imageCount);
            DigitImage[] result = new DigitImage[imageCount];
            int numRows = brImages.ReadInt32();
            numRows = ReverseBytes(numRows);
            int numCols = brImages.ReadInt32();
            numCols = ReverseBytes(numCols);
            int magic2 = brLabels.ReadInt32();
            magic2 = ReverseBytes(magic2);
            int numLabels = brLabels.ReadInt32();
            numLabels = ReverseBytes(numLabels);

            digits = new List<Digit>();
            for (int di = 0; di < imageCount; ++di)
            {
                var digit = new Digit();
                for (int i = 0; i < 28; ++i) // get 28x28 pixel values
                {
                    for (int j = 0; j < 28; ++j)
                    {
                        byte b = brImages.ReadByte();
                        pixels[i][j] = b;
                        digit.pixels[i, j] = b > 1;
                    }
                }
                byte lbl = brLabels.ReadByte(); // get the label
                var dImage = new DigitImage(28, 28, pixels, lbl);
                result[di] = dImage;
                digits.Add(digit);
            } // Each image
            ifsPixels.Close();
            brImages.Close();
            ifsLabels.Close();
            brLabels.Close();
            return result;
        }

        public static int ReverseBytes(int v)
        {
            byte[] intAsBytes = BitConverter.GetBytes(v);
            Array.Reverse(intAsBytes);
            return BitConverter.ToInt32(intAsBytes, 0);
        }

        private void openFileDialogTrain_Click(object sender, EventArgs e)
        {
            var openFileDialogTrain = new OpenFileDialog();
            if (openFileDialogTrain.ShowDialog() != DialogResult.OK) return;
            string pixelFile = openFileDialogTrain.FileName;
            string labelFile = Path.GetDirectoryName(pixelFile) + @"\train-labels.idx1-ubyte";
            trainImages = LoadMnistData(pixelFile, labelFile);
            Display(0);
        }

        private void Display(int index)
        {
            if (trainImages == null || index >= trainImages.Length) return;
            var size = 14;
            var justMade = false;
            if (Holder.Controls.Count == 0)
            {
                Holder.Controls.Clear();
                Holder.ColumnCount = 28;
                Holder.RowCount = 28;
                justMade = true;
            }
            var digit = digits[index];
            for (int j = 0; j < 28; j++)
            {
                for (int i = 0; i < 28; i++)
                {
                    var pb = justMade
                        ? new PictureBox
                          {
                              Width = size,
                              Height = size
                          }
                        : Holder.Controls[j * 28 + i];
                    if(justMade) pb.MouseHover += pb_MouseHover;

                    pb.BackColor = digit.pixels[i, j] ? Color.Black : Color.White;
                    if (justMade) Holder.Controls.Add(pb, j, i);
                    else pb.Update();
                }
            }
            FillLabels(digit);
            Holder.Update();
        }

        private void FillLabels(Digit digit)
        {
            Processing.ProcessAll(digit);
            MinMaxX.Text = "Min X: " + Processing.Attributes.MinX + " Max X: " + Processing.Attributes.MaxX;
            MinMaxY.Text = "Min Y: " + Processing.Attributes.MinY + " Max Y: " + Processing.Attributes.MaxY;
            NorthLabel.Text = Processing.Attributes.North.ToString();
            SouthLabel.Text = Processing.Attributes.South.ToString();
            EastLabel.Text = Processing.Attributes.East.ToString();
            WestLabel.Text = Processing.Attributes.West.ToString();
        }

        void pb_MouseHover(object sender, EventArgs eventArgs)
        {
            var obj = (sender as PictureBox);
            var index = Holder.Controls.GetChildIndex(obj);
            var x = index / 28;
            var y = index % 28;
            Cords.Text = x + " " + y;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            var control = (NumericUpDown)sender;
            Display((int)control.Value);
        }
    }

}
