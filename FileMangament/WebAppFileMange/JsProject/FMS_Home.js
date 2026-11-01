var h = 4;
var h2 = 4;

$("#btnresethierchy0").click(function () {
    $('#ddl_1').val('').trigger('change');
});

$("#btnresethierchy1").click(function () {
    $('#ddl2_1').val('').trigger('change');
});
$("#btnresethierchyAll").click(function () {
    $('#ddl_1').val('').trigger('change');
    $('#ddl2_1').val('').trigger('change');
});


$(".ddlFileHierarchy").change(function () {
    debugger;

    var RowIndex = Number($(this).attr('id').split('_')[1]) + 1;

    var Id = $(this).attr('id');
    debugger;
    var Idval = $("#" + Id).val();
    if (Number(Idval) > 0) {
        var data = { ddlCode: Idval }
        ddlLoadOnchangePOST("/Home/Loaddll", "select[id=ddl_" + RowIndex + "]", data, "SELECT");

    } else {
        Rest(RowIndex, 1)
    }

});

$(".ddlFileHierarchy2").change(function () {
    debugger;

    var RowIndex = Number($(this).attr('id').split('_')[1]) + 1;

    var Id = $(this).attr('id');
    var Idval = $("#" + Id).val();
    if (Number(Idval) > 0) {
        var data = { ddlCode: Idval }
        ddlLoadOnchangePOST("/Home/Loaddll", "select[id=ddl2_" + RowIndex + "]", data, "SELECT");

        // ddlRest("select[id=ddlCustomerBank]", "SELECT")
    } else {
        //ddlRest("select[id=ddl2_" + RowIndex + "]", "SELECT")
        Rest(RowIndex, 2)
    }




});

function ddlYearCheck(e) {

    if (Number(e) <= 0) {
        if ($("#IdFileLocation").val().length > 0) {
            $('#ddlYear').attr('disabled', false);
        } else {
            $('#ddlYear').attr('disabled', true);
            $("#ddlYear").val('').change();
        }
    }
}
$(".FileStorage").change(function () {
    var val = $(this).val();
    if (val != "")
        FileName();
})

$(".FileNameLocation").change(function () {

    var val = $(this).val();
    if (val != "")
        FileName2();
})

$(".FileNameLocation4TextBox").keyup(function () {
    FileName2();
})

function FileName2() {
    var FileName = "";
    var FileLoaction = "";

    var FileValue_Search = ""; //This Is Used for Search Data

    var FileValue = "";
    for (var i = 0; i <= h2; i++) {
        var ddlVal = $("#ddl2_" + i + " :selected").text();
        var IdVal = $("#ddl2_" + i).val()
        debugger
        if (Number(IdVal) > 0) {
            FileName += "_" + ddlVal.replace(" ", "_");
            FileLoaction += "/" + ddlVal;
            FileValue += "," + IdVal;
        }
        FileValue_Search += "," + i + "-" + IdVal;
    }

    var FileNameSub = FileName.substring(2, FileName.length) + "_" + $("#ddlYear").val();
    var FileLocationSub = FileLoaction.substring(2, FileLoaction.length) + "/" + $("#ddlYear").val();
    var FileValueSub = FileValue.substring(1, FileValue.length);// + "/" + $("#txtVoucherId").val();

    var FileValueSub_Search = FileValue_Search.substring(1, FileValue_Search.length);

    $("#IdfileName").val(FileNameSub);
    $("#IdFileLocation").val(FileLocationSub);
    $("#HId_Hierarchy2").val(FileValueSub);

    $("#HId_Hierarchy2_Search").val(FileValueSub_Search);
    ddlYearCheck(Number($("#IdFmType").val()));

}


function FileName() {
    var FileName = "";
    var FileValue = "";


    var FileValue_Search = ""; //This Is Used for Search Data

    var PhysicalStorage = "";
    for (var i = 0; i <= h; i++) {
        var ddlVal = $("#ddl_" + i + " :selected").text();
        var IdVal = $("#ddl_" + i).val()
        debugger
        if (Number(IdVal) > 0) {
            FileName += "_" + ddlVal.replace(" ", "_");
            FileValue += "," + IdVal;
            PhysicalStorage += "/" + ddlVal;
        }


        FileValue_Search += "," + i + "-" + IdVal;
    }

    var FileNameSub = FileName.substring(2, FileName.length); //+ "_" + $("#txtVoucherId").val();
    var FileValueSub = FileValue.substring(1, FileValue.length);// + "/" + $("#txtVoucherId").val();
    var PysicalStoargeSub = PhysicalStorage.substring(2, PhysicalStorage.length);// + "/" + $("#txtVoucherId").val();



    var FileValueSub_Search = FileValue_Search.substring(1, FileValue_Search.length);

    debugger;
    $("#HId_Hierarchy").val(FileValueSub);
    $("#IdPhysicalStorage").val(PysicalStoargeSub);

    $("#HId_Hierarchy_Search").val(FileValueSub_Search);


}

function Rest(RId, ddlTypeCode) {
    debugger;
    if (Number(ddlTypeCode) == 2) {
        for (var i = RId; i <= h2; i++) {
            ddlRest("select[id=ddl2_" + i + "]", "SELECT")
        }

        if (Number(RId) == 2) {
            $("#HId_Hierarchy2").val("");
            $("#IdfileName").val("");
            $("#IdFileLocation").val("");
            $("#txtVoucherId").val("");
            $("#txtRemarks").val("");
        }
        ddlYearCheck(Number($("#IdFmType").val()));
    }


    if (Number(ddlTypeCode) == 1) {
        for (var i = RId; i <= h; i++) {
            ddlRest("select[id=ddl_" + i + "]", "SELECT")
        }
        if (Number(RId) == 2) {
            $("#HId_Hierarchy").val("");
            $("#IdPhysicalStorage").val("");
        }
    }

}

