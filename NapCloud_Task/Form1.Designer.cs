namespace NapCloud_Task
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
            this.lblProductCatalog = new System.Windows.Forms.Label();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.errorBox = new System.Windows.Forms.ListBox();
            this.lblProgressBar = new System.Windows.Forms.Label();
            this.btnReset = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblProductCatalog
            // 
            this.lblProductCatalog.AutoSize = true;
            this.lblProductCatalog.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblProductCatalog.Location = new System.Drawing.Point(27, 83);
            this.lblProductCatalog.Name = "lblProductCatalog";
            this.lblProductCatalog.Size = new System.Drawing.Size(44, 13);
            this.lblProductCatalog.TabIndex = 0;
            this.lblProductCatalog.Text = "file path";
            this.lblProductCatalog.Visible = false;
            this.lblProductCatalog.Click += new System.EventHandler(this.lblProductCatalog_Click);
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(30, 373);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(401, 47);
            this.btnSubmit.TabIndex = 1;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(30, 46);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(401, 34);
            this.btnBrowse.TabIndex = 2;
            this.btnBrowse.Text = "Select Product Catalog..";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(30, 118);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(401, 47);
            this.progressBar.TabIndex = 3;
            // 
            // errorBox
            // 
            this.errorBox.FormattingEnabled = true;
            this.errorBox.Location = new System.Drawing.Point(30, 209);
            this.errorBox.Name = "errorBox";
            this.errorBox.Size = new System.Drawing.Size(401, 134);
            this.errorBox.TabIndex = 4;
            // 
            // lblProgressBar
            // 
            this.lblProgressBar.AutoSize = true;
            this.lblProgressBar.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblProgressBar.Location = new System.Drawing.Point(27, 193);
            this.lblProgressBar.Name = "lblProgressBar";
            this.lblProgressBar.Size = new System.Drawing.Size(59, 13);
            this.lblProgressBar.TabIndex = 5;
            this.lblProgressBar.Text = "Processing";
            this.lblProgressBar.Visible = false;
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(30, 438);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(401, 47);
            this.btnReset.TabIndex = 6;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(467, 522);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.lblProgressBar);
            this.Controls.Add(this.errorBox);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.lblProductCatalog);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CSV Converter";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblProductCatalog;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.ListBox errorBox;
        private System.Windows.Forms.Label lblProgressBar;
        private System.Windows.Forms.Button btnReset;
    }
}

