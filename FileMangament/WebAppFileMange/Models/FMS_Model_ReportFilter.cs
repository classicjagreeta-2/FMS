using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace WebAppFileMange.Models
{



    public class Td_ReportFilter
    {
        public List<MAST_REPORTFILTER> LstReportFilter { get; set; }
        public List<TD_Model_ReportFilter>  LstFilter { get; set; }

        public List<MAST_REPORTFILTER> LstReportCommon { get; set; }

    }
    public class MAST_REPORTFILTER
    {

        public int AutoId { get; set; }
        public string DefaultChecked { get; set; }
        public string BoolenFiled { get; set; }
        public string TransType { get; set; }
        public string DisplayName { get; set; }
        public string PageName { get; set; }
        public int? SlNo { get; set; }
        public string PgId { get; set; }
    }
    public class TD_Model_ReportFilter  
    {

        public bool IsAliasRequired { get; set; } = false;
        public bool IsHeadRequired { get; set; } = false;
        public string strHeadRequired { get; set; }
        public bool IsAnexureRequired { get; set; } = false;
        public bool IsSechuduleRequired { get; set; } = false;


        public bool IstComercialInvRequired { get; set; } = false;
        public bool IstComercial_PackRequired { get; set; } = false;
        public bool IsComercialInv_OriginRequired { get; set; } = false;
        public bool IsComercialInv_QualityRequired { get; set; } = false;
        public bool IsComercialInv_QuantityRequired { get; set; } = false;
        public bool IsComercialInv_WayBillRequired { get; set; } = false;
        public bool IsComercialInv_CertificateOfAnalaysis { get; set; } = false;
        public bool IsComercialInv_BillOfExchangeRequired { get; set; } = false;
        public bool IsPerformaInv_Both { get; set; }
        public bool IsPerformaInv_Only { get; set; }
        public bool IsPerformaInv_Sales { get; set; } = false;

        public bool IsCommericalInvoice_DeliveryAdvice { get; set; } = false;
        public bool IsCommericalInvoice_TaxInvoice { get; set; } = false;
        public bool IsCommericalInvoice_DeliveryNote { get; set; } = false;
        public bool IsCommericalInvoice_GatePass { get; set; } = false;

        public bool IsSignRequired { get; set; } = false;
        public bool IsStampRequired { get; set; } = false;
        public bool IsSign_Stamp_Required { get; set; } = false;

        public bool IsTransferOwnerShip { get; set; } = false;

    }
}