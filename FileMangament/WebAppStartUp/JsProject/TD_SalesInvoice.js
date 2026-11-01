$(".ddlPackageUnit").change(function () {

    SetAllValues(this);
    CalValues(this);
})

function SetAllValues(obj) {
    debugger;

    var RowIndex = $(obj).closest('tr').attr('id').split('_')[1];
    var ToUnit = $("#ddlPackageUnit_" + RowIndex).val();
    var FromUnit = $("#txtfromunit_" + RowIndex).val();
    if (ToUnit != "") {
        var data = {
            FUnit: FromUnit,
            Tunit: ToUnit
        }

        var JsData = httpPOST("/TD_SalesInvoice/GetUnitConvertion", data, true);
        debugger;

        $("#Id_Convert_" + RowIndex).val(JsData.data);

        $(obj).closest('tr').find('input.Given_TotalNetQty').val(0);
        $(obj).closest('tr').find('input.Amount').val(0);
    } else {
        msgError("Please Select Package Unint");
    }
}

    
function CalValues(obj) {
    debugger;
    var RowIndex = $(obj).closest('tr').attr('id').split('_')[1];


    var Convertionval = $("#Id_Convert_" + RowIndex).val();
    if (Convertionval == null || Convertionval == "") {
        SetAllValues(obj);
    }

    var Cval = $("#Id_Convert_" + RowIndex).val();


    var Qty = $(obj).closest('tr').find('input.Qty').val();
    var TotalGrossQty = parseFloat(Cval) * parseFloat(Qty) //Convertion Value
  //  $("#Id_Qty_" + RowIndex).val(TotalGrossQty);

   // $(obj).closest('tr').find('input.TotalGrossWtQty').val(TotalGrossQty);

    var PackageNetWt = $("#Id_DafultPackage_" + RowIndex).val(); // $(obj).closest('tr').find('input.PNetWtQty').val();
    var NoOfPackage = parseFloat(TotalGrossQty) / parseFloat(PackageNetWt);
     



   // $(obj).closest('tr').find('input.NoofPackage').val(NoOfPackage);
    $(obj).closest('tr').find('input.PNetWtQty').val(PackageNetWt);
}



function SetNoofPakage(obj) {
    var TotalGrossQty = $(obj).closest('tr').find('input.TotalGrossWtQty').val();
    var PackageNetWt = $(obj).closest('tr').find('input.PNetWtQty').val();

    var NoOfPackage = parseFloat(TotalGrossQty) / parseFloat(PackageNetWt);
    $(obj).closest('tr').find('input.NoofPackage').val(NoOfPackage);
   
}
function SetInvQty(obj) {
    //debugger;
    //var RowIndex = $(obj).closest('tr').attr('id').split('_')[1];
    //var Convertionval = $("#Id_Convert_" + RowIndex).val();
    //if (Convertionval == null || Convertionval == "") {
    //    SetAllValues(obj);
    //}

    //var Cval = $("#Id_Convert_" + RowIndex).val();
    //var TotalGrossQty = $(obj).closest('tr').find('input.TotalGrossWtQty').val();
    //var InvQty = parseFloat(TotalGrossQty) / parseFloat(Cval);
    //$(obj).closest('tr').find('input.InvQty').val(InvQty.toFixed(3));

}
$(".NoofPackage").keyup(function () {
    debugger;
    SetGrossWt(this);
    SetInvQty(this);
    SetNoofPakage(this);
    CheckQty(this);

})

$(".PNetWtQty").keyup(function () {
    //debugger;
    //SetInvQty(this);
    //SetGrossWt(this);
    SetNoofPakage(this);
    //CheckQty(this);
})

 
$(".TotalGrossWtQty").keyup(function () {
    debugger;
    SetNoofPakage(this);
    SetInvQty(this);
    CheckQty(this);
})

$(".P205_PER").keyup(function () {
    CheckQty(this);
})

$(".InvQty").keyup(function () {
    debugger;
    var RowIndex = $(this).closest('tr').attr('id').split('_')[1];
    var Convertionval = $("#Id_Convert_" + RowIndex).val();
    if (Convertionval == null || Convertionval == "") {
        SetAllValues(this);
    }
    var Cval = parseFloat( $("#Id_Convert_" + RowIndex).val());

    var InvQty = $(this).closest('tr').find('input.InvQty').val();
   

        
    var CalGrosswt = parseFloat(Cval) * parseFloat(InvQty);
    $(this).closest('tr').find('input.TotalGrossWtQty').val(CalGrosswt.toFixed(3));
    SetNoofPakage(this);
    CheckQty(this);
})

function SetGrossWt(obj) {  
    debugger;
    var NoofPackages = $(obj).closest('tr').find('input.NoofPackage').val();
    var PackageNetWt = $(obj).closest('tr').find('input.PNetWtQty').val();


    var CalGrosswt = parseFloat(NoofPackages) * parseFloat(PackageNetWt);
    $(obj).closest('tr').find('input.TotalGrossWtQty').val(CalGrosswt.toFixed(3));
    CheckQty(obj);
}

$(".Given_TotalNetQty").keyup(function () {

    debugger
    var TotalGrossQty = $(this).closest('tr').find('input.TotalGrossWtQty').val();
    var TotalNetWt = $(this).closest('tr').find('input.Given_TotalNetQty').val();
    CheckQty(this);
    //if (Number(TotalGrossQty) >= Number(TotalNetWt)) {
    //    CheckQty(this);
    //} else {
    //    msgError("Quantity Should be Less then or equal to " + TotalGrossQty);
    //    $(this).closest('tr').find('input.Given_TotalNetQty').val(0);
    //    $(this).closest('tr').find('input.Amount').val(0);
    //}

})


function CheckQty(obj) {
    debugger;
    var RowIndex = $(obj).closest('tr').attr('id').split('_')[1];
  //  var Qty = parseFloat($("#Id_Qty_" + RowIndex).val()).toFixed(3);
    
  //  var TotalGrossQty = parseFloat($(obj).closest('tr').find('input.Given_TotalNetQty').val()).toFixed(3);
    //if (TotalGrossQty != 'NaN') {
    //    if (Number(Qty) >= Number(TotalGrossQty)) {

    //        CalcAmount(obj);

    //    } else {
    //        msgError("Quantity Should be Less then or equal to " + Qty);
    //        ResetDefult(obj);
    //        // $(obj).closest('tr').find('input.TotalGrossWtQty').val(Qty);    

    //        // SetNoofPakage(obj);

    //    }
    //}

    //var BillQty = parseFloat($(obj).closest('tr').find('input.BILL_QUANTITY').val()).toFixed(2);
    //var BalQty = parseFloat($(obj).closest('tr').find('input.BALANCE_QTY').val()).toFixed(2);
    //if (Number(BillQty) >= Number(BalQty)) {
    //    CalcAmount(obj);
    //}
    CalcAmount(obj);
}

function ResetDefult(obj) {
    debugger;
    //var RowIndex = $(obj).closest('tr').attr('id').split('_')[1];
    //var Convertionval = $("#Id_Convert_" + RowIndex).val();
    //if (Convertionval == null || Convertionval == "") {
    //    SetAllValues(obj);
    //}
    //var Cval = $("#Id_Convert_" + RowIndex).val();

    //var Qty = $(obj).closest('tr').find('input.Qty').val();


    //var CalGrosswt = parseFloat(Cval) * parseFloat(Qty);
    //$(obj).closest('tr').find('input.TotalGrossWtQty').val(CalGrosswt.toFixed(3));
    //$(obj).closest('tr').find('input.InvQty').val(Qty);
    //var Defultpakage = $("#Id_DafultPackage_" + RowIndex).val();
    $(obj).closest('tr').find('input.NoofPackage').val("");
    $(obj).closest('tr').find('input.InvQty').val("");
    $(obj).closest('tr').find('input.TotalGrossWtQty').val("");
    $(obj).closest('tr').find('input.Given_TotalNetQty').val("");
    $(obj).closest('tr').find('input.PNetWtQty').val(Defultpakage);  
    $(obj).closest('tr').find('input.Amount').val("");
   
}



function CalcAmount(obj) {
    debugger;

    

    var Rate = $(obj).closest('tr').find('.Rate').val();
    var Taken_InvQty = parseFloat($(obj).closest('tr').find('input.InvQty').val()).toFixed(3);
    var TP205_PER = $(obj).closest('tr').find('input.P205_PER').val();
    var Proj = $("#IdHiddenProject").val();
    
    if (Proj == "AGR") {
        debugger;
        if (Number(TP205_PER) > 0) {
            var P205 =parseFloat(Taken_InvQty *  (Number(TP205_PER) / 100)).toFixed(3);
            //Taken_InvQty = Taken_InvQty - P205;
            Taken_InvQty = P205;
        } 
       
    }

    var Amount = parseFloat(Rate) * Number(Taken_InvQty);
    $(obj).closest('tr').find('input.Amount').val(parseFloat(Amount).toFixed(2));
    var TranType = $("#IdTransType").val();
    if ( TranType == "LC") {
        debugger;
        var CustmDty = 0.00;
        var CustAmount = 0.00;
        var VatRate = 0.00;

        CustmDty = $(obj).closest('tr').find('.CustmDty').val();
        CustAmount = parseFloat((CustmDty / 100) * Amount).toFixed(2);
        VatRate = $(obj).closest('tr').find('.VatRate').val();


        var VatAmount = parseFloat((VatRate / 100) * (parseFloat(Amount) + parseFloat(CustAmount))).toFixed(2);
        $(obj).closest('tr').find('.VatAmount').val(VatAmount);
        $(obj).closest('tr').find('.CustmDtyAmount').val(CustAmount);
        var TotalAmount = parseFloat(parseFloat(Amount) + parseFloat(CustAmount) + parseFloat(VatAmount)).toFixed(2);
        $(obj).closest('tr').find('.TotalAmount').val(TotalAmount);
    }



}


