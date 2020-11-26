namespace QuanLyLinhKien
{
    partial class FormPlashScreen
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPlashScreen));
            this.circleLoading = new DevComponents.DotNetBar.Controls.CircularProgress();
            this.timerLoading = new System.Windows.Forms.Timer(this.components);
            this.lbTitle1 = new System.Windows.Forms.Label();
            this.lbTitle2 = new System.Windows.Forms.Label();
            this.pnTxt1 = new System.Windows.Forms.Panel();
            this.pnTxt2 = new System.Windows.Forms.Panel();
            this.pnTxt1.SuspendLayout();
            this.pnTxt2.SuspendLayout();
            this.SuspendLayout();
            // 
            // circleLoading
            // 
            this.circleLoading.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.circleLoading.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.circleLoading.Enabled = false;
            this.circleLoading.Location = new System.Drawing.Point(636, 371);
            this.circleLoading.Name = "circleLoading";
            this.circleLoading.ProgressBarType = DevComponents.DotNetBar.eCircularProgressType.Spoke;
            this.circleLoading.ProgressColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.circleLoading.Size = new System.Drawing.Size(117, 115);
            this.circleLoading.Style = DevComponents.DotNetBar.eDotNetBarStyle.OfficeXP;
            this.circleLoading.TabIndex = 0;
            // 
            // timerLoading
            // 
            this.timerLoading.Interval = 1000;
            this.timerLoading.Tick += new System.EventHandler(this.timerLoading_Tick);
            // 
            // lbTitle1
            // 
            this.lbTitle1.AutoSize = true;
            this.lbTitle1.BackColor = System.Drawing.Color.Transparent;
            this.lbTitle1.Font = new System.Drawing.Font("Segoe UI Light", 50.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTitle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.lbTitle1.Location = new System.Drawing.Point(-2, -6);
            this.lbTitle1.Name = "lbTitle1";
            this.lbTitle1.Size = new System.Drawing.Size(239, 89);
            this.lbTitle1.TabIndex = 1;
            this.lbTitle1.Text = "Sunrise";
            // 
            // lbTitle2
            // 
            this.lbTitle2.AutoSize = true;
            this.lbTitle2.BackColor = System.Drawing.Color.Transparent;
            this.lbTitle2.Font = new System.Drawing.Font("Segoe UI Light", 50.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTitle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.lbTitle2.Location = new System.Drawing.Point(-4, -12);
            this.lbTitle2.Name = "lbTitle2";
            this.lbTitle2.Size = new System.Drawing.Size(323, 89);
            this.lbTitle2.TabIndex = 2;
            this.lbTitle2.Text = "Computer";
            // 
            // pnTxt1
            // 
            this.pnTxt1.Controls.Add(this.lbTitle1);
            this.pnTxt1.Location = new System.Drawing.Point(531, 51);
            this.pnTxt1.Name = "pnTxt1";
            this.pnTxt1.Size = new System.Drawing.Size(274, 84);
            this.pnTxt1.TabIndex = 3;
            // 
            // pnTxt2
            // 
            this.pnTxt2.Controls.Add(this.lbTitle2);
            this.pnTxt2.Location = new System.Drawing.Point(453, 154);
            this.pnTxt2.Name = "pnTxt2";
            this.pnTxt2.Size = new System.Drawing.Size(313, 84);
            this.pnTxt2.TabIndex = 4;
            // 
            // FormPlashScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(760, 490);
            this.Controls.Add(this.pnTxt2);
            this.Controls.Add(this.pnTxt1);
            this.Controls.Add(this.circleLoading);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormPlashScreen";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormPlashScreen";
            this.Load += new System.EventHandler(this.FormPlashScreen_Load);
            this.pnTxt1.ResumeLayout(false);
            this.pnTxt1.PerformLayout();
            this.pnTxt2.ResumeLayout(false);
            this.pnTxt2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.CircularProgress circleLoading;
        private System.Windows.Forms.Timer timerLoading;
        private System.Windows.Forms.Label lbTitle1;
        private System.Windows.Forms.Label lbTitle2;
        private System.Windows.Forms.Panel pnTxt1;
        private System.Windows.Forms.Panel pnTxt2;
    }
}