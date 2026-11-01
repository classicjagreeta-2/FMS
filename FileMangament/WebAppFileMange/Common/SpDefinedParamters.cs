using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static WebAppStartUp.Models.Model_Common;

namespace WebAppFileMange.Common
{
    public class SpDefinedParamters
    {
        public  Model_MasterDynamicSp GetFileList(Model_MasterSpParamters model_MasterSpParamters)
        {
            //string[,] paramsArray = new string[,]
            //                       {
            //                                {"@pITCD",model_MasterSpParamters.ItemCode},
            //                                 {"@pAliasID",model_MasterSpParamtersItemStock.AliasID},
            //                                 {"@pPOAutoId", model_MasterSpParamtersItemStock.pPOAutoId},

            //                       };

            Model_MasterDynamicSp model_MasterDynamicSp = new Model_MasterDynamicSp();
            //model_MasterDynamicSp.paramsArray = paramsArray;
            //model_MasterDynamicSp.FrmType = model_MasterSpParamtersItemStock.FrmName;
            model_MasterDynamicSp.SpId = 1;
            return model_MasterDynamicSp;
        }

       

    }
}