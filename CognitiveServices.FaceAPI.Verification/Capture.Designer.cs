namespace CognitiveServices.FaceAPI.Verification
{
    partial class Capture
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
            this.Stop = new System.Windows.Forms.Button();
            this.TakePhoto = new System.Windows.Forms.Button();
            this.Start = new System.Windows.Forms.Button();
            this.Devices = new System.Windows.Forms.ComboBox();
            this.FaceIn = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.FaceIn)).BeginInit();
            this.SuspendLayout();
            // 
            // Stop
            // 
            this.Stop.Location = new System.Drawing.Point(184, 409);
            this.Stop.Name = "Stop";
            this.Stop.Size = new System.Drawing.Size(75, 23);
            this.Stop.TabIndex = 9;
            this.Stop.Text = "Stop";
            this.Stop.UseVisualStyleBackColor = true;
            this.Stop.Click += new System.EventHandler(this.Stop_Click_1);
            // 
            // TakePhoto
            // 
            this.TakePhoto.Location = new System.Drawing.Point(103, 409);
            this.TakePhoto.Name = "TakePhoto";
            this.TakePhoto.Size = new System.Drawing.Size(75, 23);
            this.TakePhoto.TabIndex = 8;
            this.TakePhoto.Text = "Take Photo";
            this.TakePhoto.UseVisualStyleBackColor = true;
            this.TakePhoto.Click += new System.EventHandler(this.TakePhoto_Click_1);
            // 
            // Start
            // 
            this.Start.Location = new System.Drawing.Point(22, 409);
            this.Start.Name = "Start";
            this.Start.Size = new System.Drawing.Size(75, 23);
            this.Start.TabIndex = 7;
            this.Start.Text = "Start";
            this.Start.UseVisualStyleBackColor = true;
            this.Start.Click += new System.EventHandler(this.Start_Click_1);
            // 
            // Devices
            // 
            this.Devices.FormattingEnabled = true;
            this.Devices.Location = new System.Drawing.Point(22, 382);
            this.Devices.Name = "Devices";
            this.Devices.Size = new System.Drawing.Size(237, 21);
            this.Devices.TabIndex = 6;
            // 
            // FaceIn
            // 
            this.FaceIn.Location = new System.Drawing.Point(22, 22);
            this.FaceIn.Name = "FaceIn";
            this.FaceIn.Size = new System.Drawing.Size(752, 354);
            this.FaceIn.TabIndex = 5;
            this.FaceIn.TabStop = false;
            this.FaceIn.Click += new System.EventHandler(this.FaceIn_Click);
            // 
            // Capture
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.Stop);
            this.Controls.Add(this.TakePhoto);
            this.Controls.Add(this.Start);
            this.Controls.Add(this.Devices);
            this.Controls.Add(this.FaceIn);
            this.Name = "Capture";
            this.Text = "Capture";
            ((System.ComponentModel.ISupportInitialize)(this.FaceIn)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Stop;
        private System.Windows.Forms.Button TakePhoto;
        private System.Windows.Forms.Button Start;
        private System.Windows.Forms.ComboBox Devices;
        private System.Windows.Forms.PictureBox FaceIn;
    }
}