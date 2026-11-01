

using System;

namespace WebAppFileMange.Models.Master
{
   
    public class Mast_File_Hierarchy
    {
        public string LOCA { get; set; }
        public string UNIT { get; set; }
        public int AUTOID { get; set; }
        public string PK_CODE { get; set; }
        public int PARENTID { get; set; }
        public string HIERARCHY_NAME { get; set; }
        public string HIERARCHY_DESCN { get; set; }
        public bool HIERARCHY_GROUP { get; set; }
        public string user_name { get; set; }
        public DateTime user_entdt { get; set; }
        public string user_name_new { get; set; }
        public DateTime? user_entdt_new { get; set; }
        public string os_user { get; set; }
        public string terminal_id { get; set; }
        public string dtag { get; set; }

    }
    public class Mast_ParentHierarchy
    {
        public string LOCA { get; set; }
        public string UNIT { get; set; }
        public int AUTOID { get; set; }
        public string PK_CODE { get; set; }
        public int PARENTID { get; set; }
        public string HIERARCHY_NAME { get; set; }
        public string HIERARCHY_DESCN { get; set; }
        public Nullable<bool>  HIERARCHY_GROUP { get; set; }
        public string user_name { get; set; }
        public DateTime user_entdt { get; set; }
        public string user_name_new { get; set; }
        public DateTime? user_entdt_new { get; set; }
        public string os_user { get; set; }
        public string terminal_id { get; set; }
        public string dtag { get; set; }

    }
}