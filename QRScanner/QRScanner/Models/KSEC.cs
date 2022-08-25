using System;
using System.Collections.Generic;
using System.Text;
using QRScanner.Abstractions;

namespace QRScanner.Models
{
    public class KSEC : TableData
    {
        public string name { get; set; }
        public string lastname { get; set; }
        public string rut { get; set; }
    }
}
