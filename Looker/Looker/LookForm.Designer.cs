namespace Looker
{
    partial class LookForm
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
            this.components = new System.ComponentModel.Container();
            this.FIO = new System.Windows.Forms.Label();
            this.Guild = new System.Windows.Forms.Label();
            this.Position = new System.Windows.Forms.Label();
            this.Pic = new System.Windows.Forms.PictureBox();
            this.Timer = new System.Windows.Forms.Timer(this.components);
            this.Direction = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.Pic)).BeginInit();
            this.SuspendLayout();
            // 
            // FIO
            // 
            this.FIO.AutoSize = true;
            this.FIO.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FIO.Location = new System.Drawing.Point(8, 29);
            this.FIO.Margin = new System.Windows.Forms.Padding(0);
            this.FIO.Name = "FIO";
            this.FIO.Size = new System.Drawing.Size(42, 20);
            this.FIO.TabIndex = 0;
            this.FIO.Text = "фио";
            // 
            // Guild
            // 
            this.Guild.AutoSize = true;
            this.Guild.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Guild.Location = new System.Drawing.Point(8, 69);
            this.Guild.Margin = new System.Windows.Forms.Padding(0);
            this.Guild.Name = "Guild";
            this.Guild.Size = new System.Drawing.Size(130, 20);
            this.Guild.TabIndex = 1;
            this.Guild.Text = "подразделение";
            // 
            // Position
            // 
            this.Position.AutoSize = true;
            this.Position.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Position.Location = new System.Drawing.Point(8, 49);
            this.Position.Margin = new System.Windows.Forms.Padding(0);
            this.Position.Name = "Position";
            this.Position.Size = new System.Drawing.Size(94, 20);
            this.Position.TabIndex = 2;
            this.Position.Text = "должность";
            // 
            // Pic
            // 
            this.Pic.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Pic.ErrorImage = null;
            this.Pic.InitialImage = null;
            this.Pic.Location = new System.Drawing.Point(0, 100);
            this.Pic.Margin = new System.Windows.Forms.Padding(0);
            this.Pic.Name = "Pic";
            this.Pic.Size = new System.Drawing.Size(354, 273);
            this.Pic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Pic.TabIndex = 3;
            this.Pic.TabStop = false;
            // 
            // Timer
            // 
            this.Timer.Interval = 5000;
            this.Timer.Tick += new System.EventHandler(this.Timer_Tick);
            // 
            // Direction
            // 
            this.Direction.AutoSize = true;
            this.Direction.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Direction.Location = new System.Drawing.Point(8, 9);
            this.Direction.Margin = new System.Windows.Forms.Padding(0);
            this.Direction.Name = "Direction";
            this.Direction.Size = new System.Drawing.Size(120, 20);
            this.Direction.TabIndex = 4;
            this.Direction.Text = "направление";
            // 
            // LookForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(354, 371);
            this.Controls.Add(this.Direction);
            this.Controls.Add(this.Pic);
            this.Controls.Add(this.Position);
            this.Controls.Add(this.Guild);
            this.Controls.Add(this.FIO);
            this.MaximizeBox = false;
            this.Name = "LookForm";
            this.ShowIcon = false;
            this.Text = "Проходная";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.LookForm_Load);
            this.ResizeEnd += new System.EventHandler(this.LookForm_ResizeEnd);
            ((System.ComponentModel.ISupportInitialize)(this.Pic)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label FIO;
        public System.Windows.Forms.Label Guild;
        public System.Windows.Forms.Label Position;
        public System.Windows.Forms.PictureBox Pic;
        public System.Windows.Forms.Label Direction;
        public System.Windows.Forms.Timer Timer;
    }
}