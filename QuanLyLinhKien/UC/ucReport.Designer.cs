namespace QuanLyLinhKien.UC
{
    partial class ucReport
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.crBaoCao = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.SuspendLayout();
            // 
            // crBaoCao
            // 
            this.crBaoCao.ActiveViewIndex = -1;
            this.crBaoCao.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crBaoCao.Cursor = System.Windows.Forms.Cursors.Default;
            this.crBaoCao.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crBaoCao.Location = new System.Drawing.Point(0, 0);
            this.crBaoCao.Name = "crBaoCao";
            this.crBaoCao.Size = new System.Drawing.Size(1111, 610);
            this.crBaoCao.TabIndex = 0;
            this.crBaoCao.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
            // 
            // ucReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.crBaoCao);
            this.Name = "ucReport";
            this.Size = new System.Drawing.Size(1111, 610);
            this.ResumeLayout(false);

        }

        #endregion

        private CrystalDecisions.Windows.Forms.CrystalReportViewer crBaoCao;
    }
}
