using NapCloud_Task.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using System.Resources;
using System.Globalization;
using NPOI.SS.UserModel;

namespace NapCloud_Task
{
    class Validator
    {
        ResourceManager resourceMgr = new ResourceManager("NapCloud_Task.Messages", Assembly.GetExecutingAssembly());
        public string[] headerRowTitlesXlsx = Constants.headerTitleXlsx;

        /**
         * check various validations here for the header row
         * - check if title names match with the expected template
         * - check if number of cells count match with the expected one
         */
        public ValidatorResponse validateHeaderRow (IRow headerRow)
        {
            ValidatorResponse response = new ValidatorResponse();
            int cellCount = headerRow.LastCellNum;
            int countNumberOfCellsInXlsx = Int32.Parse(resourceMgr.GetString("CountNumberOfCellsInXlsx"));

            if (cellCount != countNumberOfCellsInXlsx)
            {
                response.Message = resourceMgr.GetString("ErrorInvalidNumberOfCells");
            } else {
                var cells = headerRow.Cells.ToArray();
                for (int i = 0; i < cells.Count(); i++)
                {
                    var colName = cells[i].ToString().Trim();
                    /**
                     * As title given in task detail and product catalog is different,
                     * i had to apply this patch to pass both "Shore Description" and "Short Description"
                     */
                    if (i == 8 && colName == "Shore Description")
                    {
                        colName = "Short Description";
                    }
                    if (colName.ToLower() != headerRowTitlesXlsx[i].ToLower())
                    {
                        response.Message = resourceMgr.GetString("ErrorInvalidCellFormat") + " " + headerRowTitlesXlsx[i];
                        break;
                    }
                }
                if (string.IsNullOrEmpty(response.Message))
                {
                    response.Status = 1;
                }
            }
            return response;
        }
        /**
         * List of validations as requested in task detail:

         * Mandatory Fields => PID, Contract Number, Product Id, MfrPN, Mfr Name, Vendor Name, Vendor PN, Cost, Short Description
         * 
         * - contract number => User XXXX for all products
         * - Coo => If empty use default value ‘TW’
         * - Long Description => If not present use Short Description
         * - UOM => If not present use ‘EA’
         * 
         */
        public ValidatorResponse validateRow(InputDataXlsx inputData)
        {
            ValidatorResponse response = new ValidatorResponse();
            int status = 0;
            string message = "";

            // . required fields
            string requiredFieldResult = validateRequiredFields(inputData);
            if (!string.IsNullOrEmpty(requiredFieldResult))
            {
                message = resourceMgr.GetString("ErrorRequiredField") + " " + requiredFieldResult;
               
            } else
            {
                // . validate attribute length
                string errorFields = validateAttributeLength(inputData);
                if (!string.IsNullOrEmpty(errorFields))
                {
                    message = resourceMgr.GetString("ErrorInvalidField") + " " + errorFields;

                }
                else
                {
                    status = 1;
                    message = resourceMgr.GetString("SuccessValidation");
                }
            }

            response.Status = status;
            response.Message = message;
            return response;
        }

        public string validateRequiredFields (InputDataXlsx inputData)
        {
            string requiredFields = "";
            if (inputData.PID == 0) {
                requiredFields = requiredFields + "PID, ";
            }
            if (inputData.Cost == 0) {
                requiredFields = requiredFields + "Cost, ";
            }
            if (string.IsNullOrEmpty(inputData.ContractNumber.Trim())) {
                requiredFields = requiredFields + "Contract Number, ";
            }
            if (string.IsNullOrEmpty(inputData.ProductId.Trim())) {
                requiredFields = requiredFields + "Product Id, ";
            }
            if (string.IsNullOrEmpty(inputData.MfrPN.Trim())) {
                requiredFields = requiredFields + "Mfr PN, ";
            }
            if (string.IsNullOrEmpty(inputData.MfrName.Trim())) {
                requiredFields = requiredFields + "Mfr Name, ";
            }
            if (string.IsNullOrEmpty(inputData.VendorName.Trim())) {
                requiredFields = requiredFields + "Vendor Name, ";
            }
            if (string.IsNullOrEmpty(inputData.VendorPN.Trim())) {
                requiredFields = requiredFields + "Vendor PN, ";
            }
            if (string.IsNullOrEmpty(inputData.ShortDescription.Trim())) {
                requiredFields = requiredFields + "Short Description";
            }
            

            return requiredFields;
        }


        public string validateAttributeLength(InputDataXlsx inputData)
        {
            string errorFields = "";
            
            if (inputData.PID.ToString().Length > 20)
            {
                errorFields = errorFields + "PID, ";
            }
            if (inputData.ContractNumber.Length > 20)
            {
                errorFields = errorFields + "Contract Number, ";
            }
            if (inputData.ProductId.Length > 50)
            {
                errorFields = errorFields + "Product Id, ";
            }
            if (inputData.MfrPN.Length > 50)
            {
                errorFields = errorFields + "Mfr PN, ";
            }
            if (inputData.MfrName.Length > 50)
            {
                errorFields = errorFields + "Mfr Name, ";
            }
            if (inputData.VendorName.Length > 50)
            {
                errorFields = errorFields + "Vendor Name, ";
            }
            if (inputData.VendorPN.Length > 50)
            {
                errorFields = errorFields + "Vendor PN, ";
            }
            if (inputData.Cost.ToString().Length > 20)
            {
                errorFields = errorFields + "Cost, ";
            }
            if (inputData.Coo.Length > 2)
            {
                errorFields = errorFields + "Coo, ";
            }
            if (inputData.ShortDescription.Length > 300)
            {
                errorFields = errorFields + "Short Description, ";
            }
            if (inputData.LongDescription.Length > 3000)
            {
                errorFields = errorFields + "Long Description, ";
            }
            if (inputData.UPC.Length > 12)
            {
                errorFields = errorFields + "UPC, ";
            }
            if (inputData.UOM.Length > 2)
            {
                errorFields = errorFields + "UOM, ";
            }
            if (inputData.SaleStartDate.Length > 20)
            {
                errorFields = errorFields + "Sale Start Date, ";
            }
            if (inputData.SaleEndDate.Length > 20)
            {
                errorFields = errorFields + "Sale End Date, ";
            }
            if (inputData.SalesPrice.ToString().Length > 20)
            {
                errorFields = errorFields + "Sales Price, ";
            }

            return errorFields;
        }
    }
}
