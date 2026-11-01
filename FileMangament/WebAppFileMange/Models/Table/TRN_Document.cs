using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppFileMange.Models.Table
{
    public class TRN_Document
    {
       
        public int Autoid { get; set; }
        public int? FK_Autoid { get; set; }        
        public string FilePath { get; set; }         
        public string FileName { get; set; }
        public string FullPath { get; set; }
    }
}