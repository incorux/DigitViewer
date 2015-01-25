namespace DigitViewer
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.openFileDialogTrain = new System.Windows.Forms.Button();
            this.Holder = new System.Windows.Forms.TableLayoutPanel();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.Cords = new System.Windows.Forms.Label();
            this.MinMaxX = new System.Windows.Forms.Label();
            this.MinMaxY = new System.Windows.Forms.Label();
            this.NorthLabel = new System.Windows.Forms.Label();
            this.EastLabel = new System.Windows.Forms.Label();
            this.WestLabel = new System.Windows.Forms.Label();
            this.SouthLabel = new System.Windows.Forms.Label();
            this.Euler4Label = new System.Windows.Forms.Label();
            this.Euler8Label = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // openFileDialogTrain
            // 
            this.openFileDialogTrain.Location = new System.Drawing.Point(565, 2);
            this.openFileDialogTrain.Name = "openFileDialogTrain";
            this.openFileDialogTrain.Size = new System.Drawing.Size(143, 23);
            this.openFileDialogTrain.TabIndex = 0;
            this.openFileDialogTrain.Text = "Load";
            this.openFileDialogTrain.UseVisualStyleBackColor = true;
            this.openFileDialogTrain.Click += new System.EventHandler(this.openFileDialogTrain_Click);
            // 
            // Holder
            // 
            this.Holder.ColumnCount = 1;
            this.Holder.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.Holder.Location = new System.Drawing.Point(-1, 2);
            this.Holder.Name = "Holder";
            this.Holder.RowCount = 1;
            this.Holder.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.Holder.Size = new System.Drawing.Size(560, 560);
            this.Holder.TabIndex = 1;
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(579, 31);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            60000,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(120, 20);
            this.numericUpDown1.TabIndex = 2;
            this.numericUpDown1.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // Cords
            // 
            this.Cords.AutoSize = true;
            this.Cords.Location = new System.Drawing.Point(579, 541);
            this.Cords.Name = "Cords";
            this.Cords.Size = new System.Drawing.Size(0, 13);
            this.Cords.TabIndex = 3;
            // 
            // MinMaxX
            // 
            this.MinMaxX.AutoSize = true;
            this.MinMaxX.Location = new System.Drawing.Point(582, 74);
            this.MinMaxX.Name = "MinMaxX";
            this.MinMaxX.Size = new System.Drawing.Size(125, 13);
            this.MinMaxX.TabIndex = 4;
            this.MinMaxX.Text = "Minimum and maximum X";
            // 
            // MinMaxY
            // 
            this.MinMaxY.AutoSize = true;
            this.MinMaxY.Location = new System.Drawing.Point(582, 96);
            this.MinMaxY.Name = "MinMaxY";
            this.MinMaxY.Size = new System.Drawing.Size(125, 13);
            this.MinMaxY.TabIndex = 5;
            this.MinMaxY.Text = "Minimum and maximum Y";
            // 
            // NorthLabel
            // 
            this.NorthLabel.AutoSize = true;
            this.NorthLabel.Location = new System.Drawing.Point(566, 124);
            this.NorthLabel.Name = "NorthLabel";
            this.NorthLabel.Size = new System.Drawing.Size(33, 13);
            this.NorthLabel.TabIndex = 6;
            this.NorthLabel.Text = "North";
            // 
            // EastLabel
            // 
            this.EastLabel.AutoSize = true;
            this.EastLabel.Location = new System.Drawing.Point(642, 124);
            this.EastLabel.Name = "EastLabel";
            this.EastLabel.Size = new System.Drawing.Size(28, 13);
            this.EastLabel.TabIndex = 7;
            this.EastLabel.Text = "East";
            // 
            // WestLabel
            // 
            this.WestLabel.AutoSize = true;
            this.WestLabel.Location = new System.Drawing.Point(566, 147);
            this.WestLabel.Name = "WestLabel";
            this.WestLabel.Size = new System.Drawing.Size(32, 13);
            this.WestLabel.TabIndex = 8;
            this.WestLabel.Text = "West";
            // 
            // SouthLabel
            // 
            this.SouthLabel.AutoSize = true;
            this.SouthLabel.Location = new System.Drawing.Point(642, 147);
            this.SouthLabel.Name = "SouthLabel";
            this.SouthLabel.Size = new System.Drawing.Size(35, 13);
            this.SouthLabel.TabIndex = 9;
            this.SouthLabel.Text = "South";
            // 
            // Euler4Label
            // 
            this.Euler4Label.AutoSize = true;
            this.Euler4Label.Location = new System.Drawing.Point(566, 171);
            this.Euler4Label.Name = "Euler4Label";
            this.Euler4Label.Size = new System.Drawing.Size(37, 13);
            this.Euler4Label.TabIndex = 10;
            this.Euler4Label.Text = "Euler4";
            // 
            // Euler8Label
            // 
            this.Euler8Label.AutoSize = true;
            this.Euler8Label.Location = new System.Drawing.Point(642, 171);
            this.Euler8Label.Name = "Euler8Label";
            this.Euler8Label.Size = new System.Drawing.Size(37, 13);
            this.Euler8Label.TabIndex = 11;
            this.Euler8Label.Text = "Euler8";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(711, 566);
            this.Controls.Add(this.Euler8Label);
            this.Controls.Add(this.Euler4Label);
            this.Controls.Add(this.SouthLabel);
            this.Controls.Add(this.WestLabel);
            this.Controls.Add(this.EastLabel);
            this.Controls.Add(this.NorthLabel);
            this.Controls.Add(this.MinMaxY);
            this.Controls.Add(this.MinMaxX);
            this.Controls.Add(this.Cords);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.Holder);
            this.Controls.Add(this.openFileDialogTrain);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button openFileDialogTrain;
        private System.Windows.Forms.TableLayoutPanel Holder;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label Cords;
        private System.Windows.Forms.Label MinMaxX;
        private System.Windows.Forms.Label MinMaxY;
        private System.Windows.Forms.Label NorthLabel;
        private System.Windows.Forms.Label EastLabel;
        private System.Windows.Forms.Label WestLabel;
        private System.Windows.Forms.Label SouthLabel;
        private System.Windows.Forms.Label Euler4Label;
        private System.Windows.Forms.Label Euler8Label;
    }
}

