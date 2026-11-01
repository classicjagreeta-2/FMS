$("#ddlcountryOrigin").change(function () {
    debugger;
    var data = { CountryCode: $("#ddlcountryOrigin").val() }
    ddlLoadOnchangePOST("/Entry/GetPortName", "select[id=ddlLaodOrginPort]", data, "SELECT");
    // ddlRest("select[id=ddlCustomerBank]", "SELECT")
});

$("#ddlcountryOrigin_").change(function () {
    debugger;
    var data = { CountryCode: $("#ddlcountryOrigin_").val() }
    ddlLoadOnchangePOST("/Entry/GetPortName", "select[id=ddlLaodOrginPort_]", data, "SELECT");
    // ddlRest("select[id=ddlCustomerBank]", "SELECT")
});

$("#ddlcountryDischarge").change(function () {

    var data = { CountryCode: $("#ddlcountryDischarge").val() }
    ddlLoadOnchangePOST("/Entry/GetPortName", "select[id=ddlLaodDischargePort]", data, "SELECT");
    // ddlRest("select[id=ddlCustomerBank]", "SELECT")
});

$("#ddlcountryDischarge_").change(function () {

    var data = { CountryCode: $("#ddlcountryDischarge_").val() }
    ddlLoadOnchangePOST("/Entry/GetPortName", "select[id=ddlLaodDischargePort_]", data, "SELECT");
    // ddlRest("select[id=ddlCustomerBank]", "SELECT")
});


$(".ddlcountryOrigin").change(function () {
    debugger;
    var RowIndex = $(this).attr('id').split('_')[1];
    var SelectVal = $("#ddlcountryOrigin_" + RowIndex).val();

    var data = { CountryCode: SelectVal }
    ddlLoadOnchangePOST("/Entry/GetPortName", "select[id=ddlLaodOrginPort_" + RowIndex +"]", data, "SELECT");
    // ddlRest("select[id=ddlCustomerBank]", "SELECT")
});

$(".ddlcountryDischarge").change(function () {
    var RowIndex = $(this).attr('id').split('_')[1];
    var SelectVal = $("#ddlcountryDischarge_" + RowIndex).val();

    var data = { CountryCode: SelectVal }
    ddlLoadOnchangePOST("/Entry/GetPortName", "select[id=ddlLaodDischargePort_"+ RowIndex +"]", data, "SELECT");
    // ddlRest("select[id=ddlCustomerBank]", "SELECT")
});


$("#ddlCustomer").change(function () {

    var data = { Id: $("#ddlCustomer").val() }
    ddlLoadOnchangePOST("/Entry/GetCustomerBankName", "select[id=ddlCustomerBank]", data, "SELECT");

    // ddlRest("select[id=ddlCustomerBank]", "SELECT")
});

$("#ddlBankName").change(function () {
    debugger;
    var data = {
        Id: $("#ddlBankName option:selected").text(),
        dllType:1
    }
    ddlLoadOnchangePOST("/TD_MastCommon/GetBranchName", "select[id=ddlbranchName]", data, "SELECT");
    // ddlRest("select[id=ddlCustomerBank]", "SELECT")
});

//$(".ddlcurrency").change(function () {
//    debugger;
  
//    var RowIndex = $(this).attr('id').split('_')[1];
//    var SelectVal = $("#ddlcurrency_" + RowIndex).val();
//    var CuryDate = $(".currencyDate").val();
//    var data = {
//        Dt: CuryDate,
//        CurrencyCode: SelectVal
//    }
//    ddlLoadOnchangePOST("/Entry/GetExchangeRata", "", data, "SELECT");

//});

function GetExchangeRate(crCode, RowIndex) {
    debugger;
    var CuryDate = $(".currencyDate").val();
    var JsParmaterdata = {
        Dt: CuryDate,
        CurrencyCode: crCode
    }
    var JsData = httpPOST("/Entry/GetExchangeRata", JsParmaterdata, true);
    debugger;
    $("#HidExRATE_" + RowIndex).val(JsData.data);
    $("#txtPrice_" + RowIndex).val(0);
    CalAmount(RowIndex);
}
function StockDetailsModelPopup(Index) {
    debugger;

    blockUIStart();

    var RowIndex = Index;
    var BtnValue = $("#IdItemStock_" + RowIndex).text().trim();
    if (Number(BtnValue) > 0) {
        var val_ItemCode = $("#txtProductcode_" + RowIndex).val();
        var val_ItemAliasCode = $("#ddlProductAlias_" + RowIndex).val();
        var val_ItemChecked = $("#IdCheckedDetails_" + RowIndex).val();
        var Val_ProductName = $("#ddlProduct_" + RowIndex + " option:selected").text();
        var Val_SoAutoid = $("#IdHiddenAutoId_" + RowIndex).val();
        var val_FmType = $("#TD_Model_EntryFormHead_FORMTYPE").val();
        var JsParmaterdata = {
            ItemCode: val_ItemCode,
            ItemAlisCode: val_ItemAliasCode, RowIndex: Index,
            ItemSelected: val_ItemChecked,
            ProductName: Val_ProductName,
            SoAutoid: Val_SoAutoid,
            FmType: val_FmType
        }

        var JsData = httpPOST("/Entry/GetItemStockDetails", JsParmaterdata, true);
        debugger;
        $('#IdStockModal').empty().append(JsData.data).modal('toggle');
    }

    $.unblockUI();
}

$(".ddlUnitLoad").change(function () {

    var RowIndex = $(this).attr('id').split('_')[1];
    var SelectVal = $("#ddlProductAlias_" + RowIndex).val();
    var data = { Id: "EDIT", IdVal: SelectVal, FmType: $("#TD_Model_EntryFormHead_FORMTYPE").val() }

    var JsData = httpPOST("/Entry/GetItemAlias", data, true);
    debugger;
    $("#txtunit_" + RowIndex).val(JsData.data.Unit);

    $("#IdItemStock_" + RowIndex).text(JsData.data.TotalStock);

    if (JsData.data.TotalStock <= 0) {
        $("#BtnItemStock_" + RowIndex).attr("disabled", "disabled");
    } else {
        $("#BtnItemStock_" + RowIndex).removeAttr("disabled");
    }

    $("#IdSelectCount_" + RowIndex).text(0);
    $("#IdCheckedDetails_" + RowIndex).val("");


    ddlLoadOnchangePOSTWithData(JsData.data.LstReturnUnit, "select[id=ddlUnit_" + RowIndex + "]", data, "SELECT");
    $("#ddlUnit_" + RowIndex).val(JsData.data.Unit).change();
});

$(".CalcAmount").keyup(function () {
    debugger;
    var RowIndex = $(this).attr('id').split('_')[1];

    CalAmount(RowIndex);


});


function ReArrangeIdex() {
    var tableRows = $('#tblProduct tbody .RowTable');
    var i = 0;
    tableRows.each(function () {
        // $('#tblProduct tbody tr td:second').html(i + 1);
        $(this).find('td').eq(1).find('input').val(i + 1);
        i++;
    })
}
function CalAmount(RowIndex) {
    debugger;
    var qty = $("#txtbillqty_" + RowIndex).val();
    var Price = $("#txtPrice_" + RowIndex).val();

    var ExRate = $("#HidExRATE_" + RowIndex).val();
        //$("#txtExgRate").val();
    //alert(qty + " --"  +Price);

    var TP205_PER = $("#txtp205_" + RowIndex).val();
    var Proj = $("#IdHiddenProject").val();

    if (Proj == "AGR") {
        debugger;
        if (Number(TP205_PER) > 0) {
            var P205 = parseFloat(qty * (Number(TP205_PER) / 100)).toFixed(3);
            //Taken_InvQty = Taken_InvQty - P205;
            qty = P205;
        }

    }


    var TotalAmount = Number(qty) * Number(Price);
    $("#txtAmount_" + RowIndex).val(parseFloat(TotalAmount).toFixed(2));

    var Price2 = $("#txtPrice2_" + RowIndex).val();
    if (Number(Price2) >= 0) {
        var TotalAmount2 = Number(qty) * Number(Price2);
        $("#txtAmount2_" + RowIndex).val(parseFloat(TotalAmount2).toFixed(2));
    }
   

    if (Proj == "VEC") {
       
        
       // var FcAmount = $("#txtAmount_" + RowIndex).val();

        var ExTotalRate = Number(Price) * Number(ExRate);
        var ExTotalAmount = Number(ExTotalRate) * Number(qty);

        $("#txtExchnagePrice_" + RowIndex).val(parseFloat(ExTotalRate).toFixed(2));
        $("#txtExchnageAmount_" + RowIndex).val(parseFloat(ExTotalAmount).toFixed(2));


        
        var ContractPrice = $("#txtContractPrice_" + RowIndex).val();
        var ContractAmount = Number(qty) * Number(ContractPrice);

        $("#txtContractAmount_" + RowIndex).val(parseFloat(ContractAmount).toFixed(2));
    }
}

$(".bookingqty").keyup(function () {
    debugger;
    var RowIndex = $(this).attr('id').split('_')[1];
    var txtbookqty = $("#txtbookqty_" + RowIndex).val()
    $("#txtbillqty_" + RowIndex).val(parseFloat(txtbookqty).toFixed(3));
    CalAmount(RowIndex);
});
$(".P205_PER").keyup(function () {
  
    var RowIndex = $(this).attr('id').split('_')[1];   
    CalAmount(RowIndex);
});


$(".childqty").keyup(function () {

    var sum = 0;



    var TableId = $(this).closest('table').attr('id');
    var RowIndex = TableId.split('_')[1];
    var Totalqty = $("#txtbillqty_" + RowIndex).val();
    // alert(Totalqty);
    console.log(TableId);
    var Rcount = $("#" + TableId + " tr").length;
    $("#" + TableId + " tr").each(function (index) {

        //var qty = $(this).find('td:nth-child(6)').find('input').val();
        var qty = Number($(this).closest('tr').find('input.childqty').val());


        if (qty > 0) {
            sum += parseFloat(qty);

            if (Totalqty < sum) {
                msgError('Shipped  Quantity Exceeded, Should Not be more then ' + Totalqty);
                $(this).closest('tr').find('input.childqty').val(0);
                //$(this).find('td:nth-child(6)').find('input').val(0);
            }
        };

        $("#txtTotal").val(sum);
    });

});


$(".ddlAliasLoad").change(function () {

    $('div#DivTble').block({
        message: 'Loading...Please wait',
        // overlayCSS: { backgroundColor: '#2c2828f5' }
    });

    debugger;
    var RowIndex = $(this).attr('id').split('_')[1];
    var SelectVal = $("#ddlProduct_" + RowIndex).val();
    $("#txtProductcode_" + RowIndex).val(SelectVal);



    var tableRows = $('#tblProduct tbody .RowTable');
    var IsDuppicate = false;
    var arr = [];
    tableRows.each(function () {
        // $('#tblProduct tbody tr td:second').html(i + 1);
        //console.log($(this).find('td').eq(2).find('input').val());
        var value = $(this).find('td').eq(2).find('input').val();
        if (arr.indexOf(value) == -1) {
            arr.push(value);
        }

        else {
            msgError('Duplicate Product');
            $("#txtProductcode_" + RowIndex).val("");
            ddlRest("select[id=ddlProductAlias_" + RowIndex + "]", "SELECT");

            $("#ddlProduct_" + RowIndex).val($("#target option:first").val());
            IsDuppicate = true;
        }
    });

    if (IsDuppicate == false) {
        var data = { Id: SelectVal, IdVal: null, FmType: $("#TD_Model_EntryFormHead_FORMTYPE").val() }
        var JsData = httpPOST("/Entry/GetItemAlias", data, true);
        debugger;
        //if (JsData.status=="1") {

        ddlLoadOnchangePOSTWithData(JsData.data.LstReturn, "select[id=ddlProductAlias_" + RowIndex + "]", data, "SELECT");
        if (JsData.data.LstReturn != null) {
            if (JsData.data.LstReturn.length == 1) {
                ddlLoadOnchangePOSTWithData(JsData.data.LstReturnUnit, "select[id=ddlUnit_" + RowIndex + "]", data, "SELECT");
                $("#ddlUnit_" + RowIndex).val(JsData.data.Unit).change();
            } else {
                ddlRest("select[id=ddlUnit_" + RowIndex + "]", "SELECT");
            }
        }


        debugger;
        $("#txtHscode_" + RowIndex).val(JsData.data.Hscode);
        $("#txtunit_" + RowIndex).val(JsData.data.Unit);
        $("#IdItemStock_" + RowIndex).text(JsData.data.TotalStock);
        $("#IdSelectCount_" + RowIndex).text(0);
        $("#IdCheckedDetails_" + RowIndex).val("");
        $('div#DivTble').unblock();

        if (JsData.data.TotalStock <= 0) {
            $("#BtnItemStock_" + RowIndex).attr("disabled", "disabled");
        } else {
            $("#BtnItemStock_" + RowIndex).removeAttr("disabled");
        }

        if (JsData.data.P205 == true) {
            $("#txtp205_" + RowIndex).removeAttr("disabled");
        } else {
            $("#txtp205_" + RowIndex).attr("disabled", "disabled");
        }
        //} else {



        //    $('div#DivTble').unblock();
        //    msgError(JsData.error);
        //}
    }
    $('div#DivTble').unblock();


});

function addRow() {
    debugger;
    $('div#DivTble').block({
        message: 'Adding New Row. Please Wait...',
        overlayCSS: { backgroundColor: '#2c2828f5' }
    });
    var last_id = $('#hiddentablei').val();
    tablei = parseInt(last_id) + 1;

    // var rowCount = $('#tblProduct tbody .RowTable').length;
    var rowCountLen = $('#tblProduct tbody .RowTable').length;
    var MaxId = Number($("#TableMaxRow").val());
    $("#TableMaxRow").val(MaxId + 1);
    var rowCount = MaxId + 1;


    var ValProductCode = $("#tblProduct").find('tr.RowTable').last().find('td').eq(2).find('input').val();
    //  alert(ValProductCode);
    if (ValProductCode != null && ValProductCode != "") {

        var ProjectId = $("#IdHiddenProject").val();


        var html = '';

        html += '<tr id="TblInnerTable_' + rowCount + '">';
        html += '<td colspan="11" class="hiddenRow">';
        html += '<div class="accordian-body collapse" id="demoAG_' + rowCount + '">';
        html += '<div class="row justify-content-center">';
        html += '<div class="col-md-10 TableChild">';
        html += '<table class="table table-bordered mb-0" id="innertable_' + rowCount + '" data-tablelastcolumn="1">';
        html += '<thead class="classHaeder">';
        html += '<tr>';
        html += '<th class="text-center">';
      
        html += '<a type="button" class="button button-small ag-i-btn" onclick="addRowColumn(' + rowCount + ',0)">';
        html += '<i class="fa fa-plus"></i>';
        html += '</a>';
        html += '</th>';
        html += '<th class="text-center" width="3%"><label class="col-form-label">Sl No</label></th>';
        html += '<th class="text-center">Product ID</th>';
        if (ProjectId == "AGR") {
            html += '<th class="text-center">ETD</th>';
            html += '<th class="text-center">From Date</th>';
            html += '<th class="text-center">To Date</th>';
        } else {
            html += '<th class="text-center">Tentative Shipment Date</th>';
            html += '<th class="text-center">Loading Laycan Period</th>';
        }


        html += '<th class="text-center">Last Date of Shipment</th>';
        if (ProjectId == "AGR") {
            html += ' <th class="text-center" width="2%"><label class="col-form-label">ETA</label></th> ';
        }



        html += '<th class="text-center">Quantity</th>';
        html += '<th class="text-center">Remarks</th>';
        html += '</tr>';
        html += '</thead>';
        html += '</table>';
        html += '</div>';
        html += '</div>';
        html += '</div>';
        html += '</td>';
        html += '</tr>';




        //  var data = { Id: tablei }
        var data = { Id: rowCount }
        var JsReturn = httpGetWithObj("/Entry/AddNewRow", data);

        JsReturn.data += html;
        // $('#div_add_row').append(JsReturn.data);
        $("#tblProduct tbody:first").append(JsReturn.data);
        $('#hiddentablei').val(tablei);
        ReArrangeIdex();
        $('div#DivTble').unblock();
    } else {
        msgError("Please add the Product ID");
        $('div#DivTble').unblock();
    }
}

function checkRadios_Commison(ctrBasic, ctrCommision, ctrComRate, RadionBtn) {
    debugger;
    var CheckedVal = $('input:radio[Id=' + RadionBtn +']:checked').val();
    if (CheckedVal == "True") {

        $("#" + ctrBasic).removeAttr('disabled');
        $("#" + ctrCommision).removeAttr('disabled');
        $("#" + ctrComRate).removeAttr('disabled');
    }
    else if (CheckedVal == "False") {

        $("#" + ctrBasic).val("").trigger("change");
        $("#" + ctrBasic).attr('disabled', 'disabled');

        $("#" + ctrCommision).val("").trigger("change");
        $("#" + ctrCommision).attr('disabled', 'disabled');

        $("#" + ctrComRate).attr('disabled', 'disabled');
        $("#" + ctrComRate).val("");
    }
}

function checkRadios_SURVEYOR(ctrSurlPort, ctrSurDPort, RadionBtn) {
    var CheckedValCommison = $('input:radio[Id=' + RadionBtn + ']:checked').val();

    if (CheckedValCommison == "True") {

        $("#" + ctrSurlPort).removeAttr('disabled');
        $("#" + ctrSurDPort).removeAttr('disabled');

    }
    else if (CheckedValCommison == "False") {

        $("#" + ctrSurlPort).val("").trigger("change");
        $("#" + ctrSurlPort).attr('disabled', 'disabled');

        $("#" + ctrSurDPort).val("").trigger("change");
        $("#" + ctrSurDPort).attr('disabled', 'disabled');


    }
}