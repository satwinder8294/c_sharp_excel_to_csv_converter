using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NapCloud_Task.Models
{
    class InputDataXlsx
    {
        public InputDataXlsx()
        {
            ContractNumber = "XXXX";
            Coo = "TW";
            LongDescription = ShortDescription;
            UOM = "EA";
        }
        public int PID { get; set; }

        [DisplayName("Product Id")]
        public string ProductId { get; set; }
        
        
        [DisplayName("Mfr Name")]
        public string MfrName { get; set; }

        [DisplayName("Vendor Name")] 
        public string VendorName { get; set; }
        public string MfrPN { get; set; }
        [DisplayName("Vendor PN")]
        public string VendorPN { get; set; }
        public Double Cost { get; set; }
        public string Coo { get; set; }
        [DisplayName("Short Description")]
        public string ShortDescription { get; set; }
        [DisplayName("Long Description")]
        
        public string UPC { get; set; }
        public string UOM { get; set; }
        [DisplayName("Sale Start Date")]
        public string SaleStartDate { get; set; }
        [DisplayName("Sale End Date")]
        public string SaleEndDate { get; set; }
        [DisplayName("Sales Price")]
        public Double SalesPrice { get; set; }

        [DisplayName("Contract Number")]
        public string ContractNumber { get; set; }
        public string LongDescription { get; set; }
    }
}
