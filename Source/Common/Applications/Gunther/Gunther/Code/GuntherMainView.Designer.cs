namespace Duomo.Common.Gunther
{
    partial class GuntherMainView
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
            this.JobsListDataGridView = new System.Windows.Forms.DataGridView();
            this.MainStatusStrip = new System.Windows.Forms.StatusStrip();
            this.MainToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.MenuStrip = new System.Windows.Forms.MenuStrip();
            this.FileMainMenuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LoadFileMainMenuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RunJobNowButton = new System.Windows.Forms.Button();
            this.CurrentTimeLabelLabel = new System.Windows.Forms.Label();
            this.CurrentTimeLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.JobsListDataGridView)).BeginInit();
            this.MainStatusStrip.SuspendLayout();
            this.MenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // JobsListDataGridView
            // 
            this.JobsListDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.JobsListDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.JobsListDataGridView.Location = new System.Drawing.Point(12, 61);
            this.JobsListDataGridView.Name = "JobsListDataGridView";
            this.JobsListDataGridView.Size = new System.Drawing.Size(954, 285);
            this.JobsListDataGridView.TabIndex = 0;
            // 
            // MainStatusStrip
            // 
            this.MainStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MainToolStripStatusLabel});
            this.MainStatusStrip.Location = new System.Drawing.Point(0, 349);
            this.MainStatusStrip.Name = "MainStatusStrip";
            this.MainStatusStrip.Size = new System.Drawing.Size(978, 22);
            this.MainStatusStrip.TabIndex = 1;
            this.MainStatusStrip.Text = "statusStrip1";
            // 
            // MainToolStripStatusLabel
            // 
            this.MainToolStripStatusLabel.Name = "MainToolStripStatusLabel";
            this.MainToolStripStatusLabel.Size = new System.Drawing.Size(118, 17);
            this.MainToolStripStatusLabel.Text = "toolStripStatusLabel1";
            // 
            // MenuStrip
            // 
            this.MenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileMainMenuToolStripMenuItem});
            this.MenuStrip.Location = new System.Drawing.Point(0, 0);
            this.MenuStrip.Name = "MenuStrip";
            this.MenuStrip.Size = new System.Drawing.Size(978, 24);
            this.MenuStrip.TabIndex = 2;
            this.MenuStrip.Text = "menuStrip1";
            // 
            // FileMainMenuToolStripMenuItem
            // 
            this.FileMainMenuToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.LoadFileMainMenuToolStripMenuItem});
            this.FileMainMenuToolStripMenuItem.Name = "FileMainMenuToolStripMenuItem";
            this.FileMainMenuToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.FileMainMenuToolStripMenuItem.Text = "File";
            // 
            // LoadFileMainMenuToolStripMenuItem
            // 
            this.LoadFileMainMenuToolStripMenuItem.Name = "LoadFileMainMenuToolStripMenuItem";
            this.LoadFileMainMenuToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.LoadFileMainMenuToolStripMenuItem.Text = "Load";
            this.LoadFileMainMenuToolStripMenuItem.Click += new System.EventHandler(this.LoadFileMainMenuToolStripMenuItem_Click);
            // 
            // RunJobNowButton
            // 
            this.RunJobNowButton.Location = new System.Drawing.Point(12, 27);
            this.RunJobNowButton.Name = "RunJobNowButton";
            this.RunJobNowButton.Size = new System.Drawing.Size(130, 28);
            this.RunJobNowButton.TabIndex = 3;
            this.RunJobNowButton.Text = "Run Job Now";
            this.RunJobNowButton.UseVisualStyleBackColor = true;
            this.RunJobNowButton.Click += new System.EventHandler(this.RunJobNowButton_Click);
            // 
            // CurrentTimeLabelLabel
            // 
            this.CurrentTimeLabelLabel.AutoSize = true;
            this.CurrentTimeLabelLabel.Location = new System.Drawing.Point(164, 35);
            this.CurrentTimeLabelLabel.Name = "CurrentTimeLabelLabel";
            this.CurrentTimeLabelLabel.Size = new System.Drawing.Size(66, 13);
            this.CurrentTimeLabelLabel.TabIndex = 4;
            this.CurrentTimeLabelLabel.Text = "Current time:";
            // 
            // CurrentTimeLabel
            // 
            this.CurrentTimeLabel.AutoSize = true;
            this.CurrentTimeLabel.Location = new System.Drawing.Point(236, 35);
            this.CurrentTimeLabel.Name = "CurrentTimeLabel";
            this.CurrentTimeLabel.Size = new System.Drawing.Size(16, 13);
            this.CurrentTimeLabel.TabIndex = 5;
            this.CurrentTimeLabel.Text = "...";
            // 
            // GuntherMainView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(978, 371);
            this.Controls.Add(this.CurrentTimeLabel);
            this.Controls.Add(this.CurrentTimeLabelLabel);
            this.Controls.Add(this.RunJobNowButton);
            this.Controls.Add(this.MainStatusStrip);
            this.Controls.Add(this.MenuStrip);
            this.Controls.Add(this.JobsListDataGridView);
            this.Name = "GuntherMainView";
            this.Text = "Gunther";
            ((System.ComponentModel.ISupportInitialize)(this.JobsListDataGridView)).EndInit();
            this.MainStatusStrip.ResumeLayout(false);
            this.MainStatusStrip.PerformLayout();
            this.MenuStrip.ResumeLayout(false);
            this.MenuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView JobsListDataGridView;
        private System.Windows.Forms.StatusStrip MainStatusStrip;
        private System.Windows.Forms.ToolStripStatusLabel MainToolStripStatusLabel;
        private System.Windows.Forms.MenuStrip MenuStrip;
        private System.Windows.Forms.ToolStripMenuItem FileMainMenuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem LoadFileMainMenuToolStripMenuItem;
        private System.Windows.Forms.Button RunJobNowButton;
        private System.Windows.Forms.Label CurrentTimeLabelLabel;
        private System.Windows.Forms.Label CurrentTimeLabel;
    }
}

