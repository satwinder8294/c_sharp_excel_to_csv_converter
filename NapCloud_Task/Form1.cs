using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using NapCloud_Task.Helpers;
using NapCloud_Task.Models;

namespace NapCloud_Task
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            //To where your opendialog box get starting location. My initial directory location is desktop.
            openFileDialog.InitialDirectory = "C://Desktop";
            //Your opendialog box title name.
            openFileDialog.Title = "Select product catalog to upload.";
            //which type file format you want to upload in database. just add them.
            openFileDialog.Filter = "Select Valid Document(*.xlsx;)|*.xlsx;";
            //FilterIndex property represents the index of the filter currently selected in the file dialog box.
            openFileDialog.FilterIndex = 1;
            try
            {
                if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    if (openFileDialog.CheckFileExists)
                    {
                        string path = System.IO.Path.GetFullPath(openFileDialog.FileName);
                        lblProductCatalog.Text = path;
                        lblProductCatalog.Visible = true;

                        // whenever user uploads a new file we need to reset few fields like progress bar and labels
                        refreshList(sender, e, false);
                    }
                }
                else
                {
                    MessageBox.Show("Please Upload Product Catalog.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                // whenever submit button clicks, reset progress bar, labels and error list
                progressBar.Value = 0;
                lblProgressBar.Text = "";
                errorBox.Items.Clear();

                string filename = System.IO.Path.GetFileName(openFileDialog.FileName);
                if (string.IsNullOrEmpty(filename))
                {
                    // throw error if user hasn't selected a file to upload.
                    MessageBox.Show("Please select a valid product catalog file.");
                }
                else
                {
                    // disable submit/ reset/ browse buttons so that user cant click them again by mistake or intentionally.
                    btnSubmit.Enabled = false;
                    btnReset.Enabled = false;
                    btnBrowse.Enabled = false;

                    lblProgressBar.Visible = true;
                    lblProgressBar.Text = "Processing...";

                    // open the uploaded file and validate the data
                    ValidatorResponse response = FileSystem.convertXlsxToCsv(openFileDialog.FileName, progressBar, errorBox);
                    if (response.Status != 1 && !string.IsNullOrEmpty(response.Message))
                    {
                        MessageBox.Show("Error: " + response.Message);
                    }
                    lblProgressBar.Text = "Finished !";

                    // enable these buttons again after the processing has finished
                    btnSubmit.Enabled = true;
                    btnReset.Enabled = true;
                    btnBrowse.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void lblProductCatalog_Click(object sender, EventArgs e)
        {

        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            refreshList(sender, e);
        }

        private void refreshList(object sender, EventArgs e, bool flagClearFileDialog = true)
        {
            progressBar.Value = 0;
            lblProgressBar.Text = "";
            errorBox.Items.Clear();
            lblProgressBar.Visible = false;

            if (flagClearFileDialog)
            {
                openFileDialog.Reset();
                lblProductCatalog.Text = "";
                lblProductCatalog.Visible = false;
            }
        }
    }
}
