namespace SVG_from_Script
{
    partial class frmFindReplace
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtInfo = new System.Windows.Forms.TextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.txtReplace = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtFind = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.cmdFindNext = new System.Windows.Forms.Button();
            this.panel7 = new System.Windows.Forms.Panel();
            this.cmdCountAll = new System.Windows.Forms.Button();
            this.cmdReplaceAll = new System.Windows.Forms.Button();
            this.panel6 = new System.Windows.Forms.Panel();
            this.cmdReplace = new System.Windows.Forms.Button();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel8 = new System.Windows.Forms.Panel();
            this.cmdClose = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel8.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtInfo);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(288, 211);
            this.panel1.TabIndex = 0;
            // 
            // txtInfo
            // 
            this.txtInfo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtInfo.Location = new System.Drawing.Point(0, 112);
            this.txtInfo.Multiline = true;
            this.txtInfo.Name = "txtInfo";
            this.txtInfo.ReadOnly = true;
            this.txtInfo.Size = new System.Drawing.Size(288, 99);
            this.txtInfo.TabIndex = 2;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.txtReplace);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 56);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(6, 3, 6, 3);
            this.panel3.Size = new System.Drawing.Size(288, 56);
            this.panel3.TabIndex = 1;
            // 
            // txtReplace
            // 
            this.txtReplace.AcceptsReturn = true;
            this.txtReplace.AcceptsTab = true;
            this.txtReplace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtReplace.Location = new System.Drawing.Point(81, 3);
            this.txtReplace.Multiline = true;
            this.txtReplace.Name = "txtReplace";
            this.txtReplace.Size = new System.Drawing.Size(201, 50);
            this.txtReplace.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Left;
            this.label2.Location = new System.Drawing.Point(6, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 50);
            this.label2.TabIndex = 0;
            this.label2.Text = "Replace with:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.txtFind);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(6, 3, 6, 3);
            this.panel2.Size = new System.Drawing.Size(288, 56);
            this.panel2.TabIndex = 0;
            // 
            // txtFind
            // 
            this.txtFind.AcceptsReturn = true;
            this.txtFind.AcceptsTab = true;
            this.txtFind.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtFind.Location = new System.Drawing.Point(81, 3);
            this.txtFind.Multiline = true;
            this.txtFind.Name = "txtFind";
            this.txtFind.Size = new System.Drawing.Size(201, 50);
            this.txtFind.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Location = new System.Drawing.Point(6, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 50);
            this.label1.TabIndex = 0;
            this.label1.Text = "Find what:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.cmdFindNext);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(288, 0);
            this.panel4.Name = "panel4";
            this.panel4.Padding = new System.Windows.Forms.Padding(6);
            this.panel4.Size = new System.Drawing.Size(156, 35);
            this.panel4.TabIndex = 1;
            // 
            // cmdFindNext
            // 
            this.cmdFindNext.Dock = System.Windows.Forms.DockStyle.Top;
            this.cmdFindNext.Location = new System.Drawing.Point(6, 6);
            this.cmdFindNext.Name = "cmdFindNext";
            this.cmdFindNext.Size = new System.Drawing.Size(144, 23);
            this.cmdFindNext.TabIndex = 1;
            this.cmdFindNext.Text = "Find Next";
            this.cmdFindNext.UseVisualStyleBackColor = true;
            this.cmdFindNext.Click += new System.EventHandler(this.cmdFindNext_Click);
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.cmdCountAll);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel7.Location = new System.Drawing.Point(288, 105);
            this.panel7.Name = "panel7";
            this.panel7.Padding = new System.Windows.Forms.Padding(6);
            this.panel7.Size = new System.Drawing.Size(156, 35);
            this.panel7.TabIndex = 4;
            // 
            // cmdCountAll
            // 
            this.cmdCountAll.Dock = System.Windows.Forms.DockStyle.Top;
            this.cmdCountAll.Location = new System.Drawing.Point(6, 6);
            this.cmdCountAll.Name = "cmdCountAll";
            this.cmdCountAll.Size = new System.Drawing.Size(144, 23);
            this.cmdCountAll.TabIndex = 1;
            this.cmdCountAll.Text = "Count All";
            this.cmdCountAll.UseVisualStyleBackColor = true;
            this.cmdCountAll.Click += new System.EventHandler(this.cmdCountAll_Click);
            // 
            // cmdReplaceAll
            // 
            this.cmdReplaceAll.Dock = System.Windows.Forms.DockStyle.Top;
            this.cmdReplaceAll.Location = new System.Drawing.Point(6, 6);
            this.cmdReplaceAll.Name = "cmdReplaceAll";
            this.cmdReplaceAll.Size = new System.Drawing.Size(144, 23);
            this.cmdReplaceAll.TabIndex = 1;
            this.cmdReplaceAll.Text = "Replace All";
            this.cmdReplaceAll.UseVisualStyleBackColor = true;
            this.cmdReplaceAll.Click += new System.EventHandler(this.cmdReplaceAll_Click);
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.cmdReplaceAll);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel6.Location = new System.Drawing.Point(288, 70);
            this.panel6.Name = "panel6";
            this.panel6.Padding = new System.Windows.Forms.Padding(6);
            this.panel6.Size = new System.Drawing.Size(156, 35);
            this.panel6.TabIndex = 3;
            // 
            // cmdReplace
            // 
            this.cmdReplace.Dock = System.Windows.Forms.DockStyle.Top;
            this.cmdReplace.Location = new System.Drawing.Point(6, 6);
            this.cmdReplace.Name = "cmdReplace";
            this.cmdReplace.Size = new System.Drawing.Size(144, 23);
            this.cmdReplace.TabIndex = 1;
            this.cmdReplace.Text = "Replace";
            this.cmdReplace.UseVisualStyleBackColor = true;
            this.cmdReplace.Click += new System.EventHandler(this.cmdReplace_Click);
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.cmdReplace);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(288, 35);
            this.panel5.Name = "panel5";
            this.panel5.Padding = new System.Windows.Forms.Padding(6);
            this.panel5.Size = new System.Drawing.Size(156, 35);
            this.panel5.TabIndex = 2;
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.cmdClose);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel8.Location = new System.Drawing.Point(288, 176);
            this.panel8.Name = "panel8";
            this.panel8.Padding = new System.Windows.Forms.Padding(6);
            this.panel8.Size = new System.Drawing.Size(156, 35);
            this.panel8.TabIndex = 5;
            // 
            // cmdClose
            // 
            this.cmdClose.Dock = System.Windows.Forms.DockStyle.Top;
            this.cmdClose.Location = new System.Drawing.Point(6, 6);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(144, 23);
            this.cmdClose.TabIndex = 1;
            this.cmdClose.Text = "Close";
            this.cmdClose.UseVisualStyleBackColor = true;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // frmFindReplace
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(444, 211);
            this.Controls.Add(this.panel8);
            this.Controls.Add(this.panel7);
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmFindReplace";
            this.Text = "Find & Replace";
            this.TopMost = true;
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtInfo;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox txtReplace;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox txtFind;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button cmdFindNext;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Button cmdCountAll;
        private System.Windows.Forms.Button cmdReplaceAll;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Button cmdReplace;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Button cmdClose;
    }
}