using System;
using System.Collections.Generic;
using WebAppFileMange.Models.Master;

namespace WebAppFileMange.Models
{
    public class FMS_Model_HierarchyMaster
    {
        public Mast_ParentHierarchy HierarchyHead { get; set; }
        public List<Mast_File_Hierarchy> HierarchyChlid { get; set; }
        public List<ModelTextId> LstModel { get; set; }
    }

}