using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NapCloud_Task
{
    class Constants
    {
        public const string csvSeparator = "^";
        public const int maxRowsPerCsv = 10000;
        public const string fileNameCsv = "ProductList.csv";
        public readonly static string[] headerTitleXlsx = { "PID", "Product Id", "Mfr Name", "Vendor Name", "MfrPN", "Vendor PN", "Cost", "Coo", "Short Description", "UPC", "UOM", "Sale Start Date", "Sale End Date", "Sales Price" };
        public readonly static string[] headerTitleCsv = { "PID", "Contract", "Product Id", "Mfr P/N", "Mfr Name", "Vendor Name", "Vendor P/N", "Price", "Coo", "Short Description", "Long Description", "UPC", "UOM", "Sale Start Date", "Sale End Date", "Sale Price" };
    }
}
