using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppFileMange.Models.Master
{
    public class Mast_Permissions
    {
        public int Autoid { get; set; }
        public string FK_UserName { get; set; }
        public int? FK_Hierarchy_Autoid { get; set; }
        public string Email { get; set; }
        public string FileDownlaod { get; set; }
        public string FileView { get; set; }
    }
}
 