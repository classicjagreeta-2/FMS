using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebAppFileMange.Models.Table
{
    [Table("TRN_FilesHead")]
    public class TRN_FilesHead
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Autoid { get; set; }
        public string Hierarchy { get; set; }
        public string Hierarchy2 { get; set; }
        public string VoucherId { get; set; }
        
    }
}