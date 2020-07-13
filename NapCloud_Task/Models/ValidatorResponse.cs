using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NapCloud_Task.Models
{
    class ValidatorResponse
    {
        public ValidatorResponse()
        {
            Status = 0;
        }
        public int Status { get; set; }
        public string Message { get; set; }
    }
}
