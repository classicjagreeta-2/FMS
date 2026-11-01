using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebAppStartUp.Models
{
    public class Model_Layout
    {


        public Model_Layout()
        {
            ManuDetails = "";
        }
        public string ManuDetails { get; set; }
        [Required]
        [Display(Name = "Select From Date")]
        [DataType(DataType.Date)]
        public DateTime? Fdate { get; set; }

        [Required]
        [Display(Name = "Select To Date")]
        [DataType(DataType.Date)]
        public DateTime? Ldate { get; set; }

        [Required]
        [Display(Name = "Select as on Date")]
        [DataType(DataType.Date)]
        public DateTime? Adate { get; set; }

        [Display(Name = "Year Type")]
        public ReportYearType ReportYearType { get; set; }

        [Display(Name = "Frequency")]
        public Frequency Frequency { get; set; }

        [Display(Name = "OutPut Type")]
        public OutPutType OutPutType { get; set; }

        [Display(Name = "OutPut")]
        public OutPut OutPut { get; set; }

        [Display(Name = "Invoice")]
        public Invoice Invoice { get; set; }

        [Display(Name = "YesNo1")]
        public YesNo1 YesNo1 { get; set; }

        [Display(Name = "YesNo2")]
        public YesNo2 YesNo2 { get; set; }

        [Display(Name = "YesNo3")]
        public YesNo3 YesNo3 { get; set; }

        [Display(Name = "Select Report Option")]
        public ReportOpt ReportOpt { get; set; }

        [Display(Name = "Based On")]
        public ReportOpt1 ReportOpt1 { get; set; }

        [Display(Name = "Report Value")]
        public ReportOpt2 ReportOpt2 { get; set; }
        [Display(Name = "Report Value")]
        public ReportOpt ReportOpt3 { get; set; }

        [Display(Name = "Formats :")]
        public Formats Formats { get; set; }
    }
    public enum ReportYearType
    {
        FinancialYear,
        CalendarYear
    }
    public enum Frequency
    {
        Daily,
        Weekly
    }
    public enum OutPutType
    {
        Graph,
        Excel,
        Chart
    }
    public enum OutPut
    {
        Detail,
        Summary
    }
    public enum Invoice
    {
        All,
        Challan,
        Tax
    }
    public enum YesNo1
    {
        Yes,
        No
    }

    public enum YesNo2
    {
        Yes,
        No
    }

    public enum YesNo3
    {
        Yes,
        No
    }
    public enum ReportOpt
    {
        Opt1,
        Opt2,
        Opt3,
        Opt4
    }

    public enum ReportOpt1
    {
        Opt1,
        Opt2,
        Opt3
    }
    public enum ReportOpt2
    {
        Opt1,
        Opt2,
        Opt3
    }
    public enum Formats
    {
        Format1,
        Format2
    }

}