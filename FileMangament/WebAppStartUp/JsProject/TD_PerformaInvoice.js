$(".RQty").keyup(function () {

    var TakenQty = $(this).closest('tr').find('input.RQty').val();
    var Qty = $(this).closest('tr').find('input.Qty').val()
    debugger;
    if (Number(Qty) >= Number(TakenQty)) {


        CalcNoPacke(TakenQty, $(this));

    } else {
        msgError("Quantity Should be Less then or equal to " + Qty);
        $(this).closest('tr').find('input.RQty').val(Qty);

        CalcNoPacke(Qty, $(this));
    }

});

$(".CustmDty").keyup(function () {
    debugger;
    var Qty = $(this).closest('tr').find('input.RQty').val();
    CalcNoPacke(Qty, $(this));
})


$(".VatRate").keyup(function () {
    debugger;
    var Qty = $(this).closest('tr').find('input.RQty').val();
    CalcNoPacke(Qty, $(this));
})

$(".cRate").keyup(function () {
    debugger;
    var Qty = $(this).closest('tr').find('input.RQty').val();
    CalcNoPacke(Qty, $(this));
})

function CalcNoPacke(BillQty, obj) {
    debugger;
    var Amount = 0.00;
    var PNetWt = $(obj).closest('tr').find('.PNetWtQty').val();
    var P205_PER = parseFloat(parseFloat($(obj).closest('tr').find('.P205_PER').val()) / 100).toFixed(3);
    var NoPackage = (parseFloat(BillQty) / parseFloat(PNetWt));
    $(obj).closest('tr').find('input.PQty').val(NoPackage.toFixed(3));
    

    var Rc = parseFloat($(obj).closest('tr').find('.Rate').val());

    if (Number(P205_PER) > 0) {
        Amount = (parseFloat(BillQty) * Rc * P205_PER).toFixed(2);
    } else {
        Amount = (parseFloat(BillQty) * Rc).toFixed(2);
    }

    
    $(obj).closest('tr').find('.Amount').val(Amount);

    var Proj = $("#IdHiddenProject").val();
    var TranType = $("#IdTransType").val();

    if (Proj == "VEC" && (TranType == "LC" || TranType == "PG")) {
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


function ResetVal(obj) {
    $(obj).closest('tr').find('.PQty').val("");
    $(obj).closest('tr').find('.GrossWtQty').val("");
    $(obj).closest('tr').find('.PGrossWtQty').val("");
}