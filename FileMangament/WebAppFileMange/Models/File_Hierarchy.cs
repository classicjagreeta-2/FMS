using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebAppFileMange.Models.Table;

namespace WebAppFileMange.Models
{
    public class File_Hierarchy
    {

        public Head Head { get; set; }
        public List<TRN_Document> LstDocument { get; set; }

    }
    public class Head : WebAppStartUp.Models.Model_Common.DataTablePageing
    {
        public string NoUse { get; set; }
        public string Hierarchy { get; set; }
        public string Hierarchy2 { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string VoucherId { get; set; }
        public string Remarks { get; set; }
        public string Yyear { get; set; }
        public int SpId { get; set; }
        public string Unit { get; set; }
        public string UploadSessionId { get; set; }
        public int? Autoid { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date)]
        public DateTime? FromDate { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date)]
        public DateTime? ToDate { get; set; }
        public DateTime dtDate { get; set; }

    }
    public class SearchHead
    {
        public string Floor { get; set; }

    }

}