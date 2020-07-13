using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NapCloud_Task.Models;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace NapCloud_Task.Helpers
{

    class FileSystem
    {
        public static ValidatorResponse convertXlsxToCsv(string filePath, ProgressBar progressBar, ListBox errorBox)
        {
            // standard response to return from each sub function
            ValidatorResponse response = new ValidatorResponse();
            try {
                // get executable directory path so that we can use it to create files
                string executablePath = System.IO.Path.GetDirectoryName(Application.ExecutablePath);
                // this is our directory where we are going to create ProductList.csv and error.xlsx
                string uploadDirectory = executablePath + "\\uploads\\" + Util.getCurrentTimestamp();
                // as mentioned in task detail maximum number of rows allowed per csv
                const int maxRowsPerCsv = Constants.maxRowsPerCsv;
                // gonna use this to switch file names while saving when record count crosses max allowed
                int rowCounter = 0;
                // default file name as requested in task detail
                string fileNameCsv = Constants.fileNameCsv;
                // get csv seperator from constants, so that we can change it easily in future (if needed)
                string csvSeparator = Constants.csvSeparator;
                // get header row titles from constants and join them by the separator
                var csvHeaderRow = String.Join(csvSeparator, Constants.headerTitleCsv);
                // gonna use this variable to identify when to insert header row for each new file
                bool flagInsertHeaderRow = true;

                /**
                 * I have used NPOI for excel operations because Interop requires actual Excel application to be installed on server, which is not always a good idea ar may even be not possible at all (i.e. if you are using Azure Web Apps). NPOI works directly with Excel files. It is much faster than Interop.
                 */
                XSSFWorkbook xssfworkbook;

                using (FileStream file = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    xssfworkbook = new XSSFWorkbook(file);
                }
                // read data
                XSSFSheet sheet = (XSSFSheet)xssfworkbook.GetSheetAt(0);

                IRow headerRow = sheet.GetRow(0);


                Validator validator = new Validator();
                // validate header row as per given details in the task description
                response = validator.validateHeaderRow(headerRow);


                if (response.Status == 1)
                {
                    var csvData = new StringBuilder();

                    progressBar.Maximum = sheet.LastRowNum;

                    // create a new workbook, as we need to store failed records in error.xlsx
                    IWorkbook errorWorkbook = new HSSFWorkbook();
                    // create new sheet to add failed records
                    ISheet errorSheet = errorWorkbook.CreateSheet("Sheet1");

                    if (sheet.LastRowNum > maxRowsPerCsv)
                    {
                        // as per requirement if supplied number of rows count exceed the max (i. e 10,000), then all file names should be different
                        fileNameCsv = Util.getCurrentTimestamp();
                    }

                    // loop through rows
                    for (int i = 1; i < sheet.LastRowNum; i++)
                    {
                        string errorMessage = "";

                        // create new directory, as we are going to save both error.xlsx and csv files in this folder.
                        Directory.CreateDirectory(uploadDirectory);

                        if (sheet.GetRow(i) != null) //null is when the row only contains empty cells 
                        {
                            IRow row = sheet.GetRow(i);

                            int cellCount = row.LastCellNum;

                            if (cellCount >= 11)
                            {
                                int pId;
                                Double salesPrice = 0;
                                Double cost;

                                Int32.TryParse(row.GetCell(0).ToString(), out pId);
                                Double.TryParse(row.GetCell(6).ToString(), out cost);

                                if (13 <= (cellCount - 1))
                                {
                                    Double.TryParse(row.GetCell(13).ToString(), out salesPrice);
                                }

                                var ProductId = row.GetCell(1) != null ? row.GetCell(1).ToString() : "";
                                var MfrName = row.GetCell(2) != null ? row.GetCell(2).ToString() : "";
                                var VendorName = row.GetCell(3) != null ? row.GetCell(3).ToString() : "";
                                var MfrPN = row.GetCell(4) != null ? row.GetCell(4).ToString() : "";
                                var VendorPN = row.GetCell(5) != null ? row.GetCell(5).ToString() : "";
                                var Coo = row.GetCell(7) != null ? row.GetCell(7).ToString() : "";
                                var ShortDescription = row.GetCell(8) != null ? row.GetCell(8).ToString() : "";
                                var UPC = (cellCount - 1) >= 9  && row.GetCell(9) != null ? row.GetCell(9).ToString() : "";
                                var UOM = (cellCount - 1) >= 10 && row.GetCell(10) != null ? row.GetCell(10).ToString() : "";
                                var SaleStartDate = (cellCount - 1) >= 11 && row.GetCell(11) != null ? row.GetCell(11).ToString() : "";
                                var SaleEndDate = (cellCount - 1) >= 12 && row.GetCell(12) != null ? row.GetCell(12).ToString() : "";

                                // note: we dont have any field for large description in the given template
                                var LongDescription = ShortDescription;
                                // Price Should be 20 % more than cost
                                var Price = cost + ((cost * 20) / 100);

                                // Coo => If empty use default value ‘TW’
                                Coo = string.IsNullOrEmpty(Coo) ? "TW" : Coo;

                                // UOM => If not present use ‘EA’
                                UOM = string.IsNullOrEmpty(UOM) ? "EA" : UOM;

                                // i like building objects, as it is difficult to identify/ remember values by index 0,1, etc
                                InputDataXlsx inputData = new InputDataXlsx
                                {
                                    PID = pId,
                                    ProductId = ProductId,
                                    MfrName = MfrName,
                                    VendorName = VendorName,
                                    MfrPN = MfrPN,
                                    VendorPN = VendorPN,
                                    Coo = Coo,
                                    ShortDescription = ShortDescription,
                                    LongDescription = LongDescription,
                                    UPC = UPC,
                                    UOM = UOM,
                                    SaleStartDate = SaleStartDate,
                                    SaleEndDate = SaleEndDate,
                                    SalesPrice = salesPrice,
                                    Cost = Price,
                                };
                                response = validator.validateRow(inputData);

                                if (response.Status == 1)
                                {
                                    if (flagInsertHeaderRow)
                                    {
                                        // this flag true means, the file is new and we need to insert new header row on top
                                        csvData.AppendLine(csvHeaderRow);
                                        // set this flag to false, so that it doesnt insert title in eah row
                                        flagInsertHeaderRow = false;
                                    }

                                    var newLine = $"{inputData.PID.ToString()}{csvSeparator}{inputData.ContractNumber}{csvSeparator}{inputData.ProductId}{csvSeparator}{inputData.MfrPN}{csvSeparator}{inputData.MfrName}{csvSeparator}{inputData.VendorName}{csvSeparator}{inputData.VendorPN}{csvSeparator}{inputData.Cost}{csvSeparator}{inputData.Coo}{csvSeparator}{inputData.ShortDescription}{csvSeparator}{inputData.LongDescription}{csvSeparator}{inputData.UPC}{csvSeparator}{inputData.UOM}{csvSeparator}{inputData.SaleStartDate}{csvSeparator}{inputData.SaleEndDate}{csvSeparator}{inputData.SalesPrice}";
                                    
                                    progressBar.Value = i;
                                    rowCounter = rowCounter + 1;

                                    // if rows counter is greater than equal to or the index reaches at the last, then we need to generate the csv
                                    if (rowCounter >= maxRowsPerCsv || i == sheet.LastRowNum)
                                    {
                                        // create and save data in csv file
                                        File.AppendAllText(uploadDirectory + "\\" + fileNameCsv, csvData.ToString());

                                        // after each csv generation we need to set a new name for csv file
                                        fileNameCsv = Util.getCurrentTimestamp() + ".csv";
                                        // reset these values for new file
                                        csvData = new StringBuilder();
                                        flagInsertHeaderRow = true;
                                        rowCounter = 0;
                                    }
                                }
                                else
                                {
                                    errorMessage = $"Error at row {i + 1}:  {response.Message}";
                                }
                            }
                            else
                            {
                                errorMessage = $"Error at row {i + 1}: Invaid number of columns";
                            }
                        }
                        else
                        {
                            errorMessage = $"Error at row {i + 1}: Invaid number of columns";

                        }

                        if (!string.IsNullOrEmpty(errorMessage))
                        {
                            /**
                             * log failed records in error.xlsx file
                             */
                            // add row titles on top
                            if (errorBox.Items.Count == 0)
                            {
                                // create title row
                                IRow errorRowTitle = errorSheet.CreateRow(0);

                                for(int k = 0; k < Constants.headerTitleXlsx.Length; k++)
                                {
                                    // get title names from the constant and insert in top row for error file
                                    ICell cellTitle = errorRowTitle.CreateCell(k);
                                    cellTitle.SetCellValue(Constants.headerTitleXlsx[k]);
                                }
                            }

                            // for each new error create a new row and replicate the failed row to this one
                            IRow errorRow = errorSheet.CreateRow(errorBox.Items.Count + 1);
                            for (int j = 0; j < sheet.GetRow(i).LastCellNum; j++)
                            {
                                ICell cell = errorRow.CreateCell(j);
                                dynamic cellValue = sheet.GetRow(i).GetCell(j) != null ? sheet.GetRow(i).GetCell(j).ToString() : "";
                                cell.SetCellValue(cellValue);
                            }

                            // add message to the error list in windows form for user readability
                            errorBox.Items.Add(errorMessage);
                        }
                    }

                    if (errorBox.Items.Count > 0)
                    {
                        // if there are errors in the uploaded file, generate error.xlsx and push failed records in it
                        using (FileStream stream = new FileStream(uploadDirectory + "\\" + "error.xlsx", FileMode.Create, FileAccess.Write))
                        {
                            errorWorkbook.Write(stream);
                        }
                    }

                }
                else
                {
                    errorBox.Items.Add("Error: " + response.Message);
                }
                return response;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                return response;
            }
}
    }
}
