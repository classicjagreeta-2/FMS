function LoadCheckList(Id, FmType, RViewId,sFileName) {
    debugger;
    var ObjData = {
        ViewId: RViewId,
        FmType: FmType,
        FileName: sFileName
    };

    var AjxReturn = httpPOST("../common/LoadReportCheckList", ObjData, true);
    debugger;

    $("#lblCheckBoxList").empty().append(AjxReturn.data);
    $('#default-Modal').modal('show');

    $("#HAutoId").val(Id);
    $("#HFmId").val(FmType);
    $("#HFileName").val(sFileName);
}

function LoadCheckListPreview(Id, FmType, RViewId) {
    var ObjData = {
        ViewId: RViewId
    };

    //var AjxReturn = httpPOST("../common/LoadReportCheckListPrview", ObjData, true);
    //debugger;

    var AjxReturn = httpPOST("../common/LoadReportCheckList", ObjData, true);
    debugger;

    $("#lblCheckBoxList").empty().append(AjxReturn.data);
    $('#default-Modal').modal('show');

    $("#HAutoId").val(Id);
    $("#HFmId").val(FmType);

}

function LoadCheckListEmail(Id, FmType, RViewId) {
    var ObjData = {
        ViewId: RViewId
    };

    var AjxReturn = httpPOST("../common/LoadReportCheckListEmail", ObjData, true);
    debugger;

    $('textarea#txtemailId').val()

    $("#lblCheckBoxListEmail").empty().append(AjxReturn.data);

    $('#default-ModalEmail').modal('show');

    $("#HAutoId").val(Id);
    $("#HFmId").val(FmType);
    //$("#HEmail").val($('textarea#txtemailId').val());

}


$(".btnpdfdownload").click(function () {
    debugger;
    var CheckArray = []
    $("input:checkbox[class=ReportCheck]:checked").each(function () {
        CheckArray.push($(this).attr('id'));
    });


    var ObjData = {
        AutoId: $("#HAutoId").val(),
        FmType: $("#HFmId").val(),
        CheckList: CheckArray
    };

    var link = document.createElement('a');
    link.href = "/FMS_Report/Report_DetailsAjax?Autoid=" + $("#HAutoId").val() + "&FmType=" + $("#HFmId").val() + "&CheckList=" + CheckArray + " ";
    link.target = "_blank";
    document.body.appendChild(link);
    link.click();

});

$(".btnpdfpreview").click(function () {

    debugger;
    var CheckArray = []
    $("input:checkbox[class=ReportCheck]:checked").each(function () {
        CheckArray.push($(this).attr('id'));
    });

    var ObjData = {
        AutoId: $("#HAutoId").val(),
        FmType: $("#HFmId").val(),
        CheckList: CheckArray
    };

    var link = document.createElement('a');
    link.href = "/FMS_Report/Report_DetailsAjax?Autoid=" + $("#HAutoId").val() + "&FmType=" + $("#HFmId").val() + "&CheckList=" + CheckArray + " ";
    link.target = "_blank";
    document.body.appendChild(link);
    link.click();

});

 

//$("#btnsendmail").click(function () {
$(".btnmessage").click(function () {
    debugger;

  

    var Data = {
        MobileNumber: $("#txtWhatsappNumber").val(),
        IdNumber: $("#HAutoId").val(),
    }
    debugger;


    $.ajax({
        type: "POST",
        url: "/common/SendWhatsAppMsg",
        data: JSON.stringify(Data),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        beforeSend: function () {
            $(".btnSendMail").buttonLoader('start', 'Sending..');
        },
        complete: function (e) {
            debugger;
            $(".btnSendMail").buttonLoader('stop', '');



            //   OnSuccess(e.responseJSON);

        },
        //  async: false,
        success: function (data) {
            debugger;
            $(".btnSendMail").buttonLoader('stop', '');
            $('textarea#txtemailId').val("");


            if (data.success == true && data.Id > 0) {


                $('textarea#txtemailId').val("");
                msgSuccess(data.message);

            } else {
                msgError(data.details);



            }

            // data;
        },
        error: function (e, x, settings, exception) {
            //var message;
            var statusErrorMap = {
                '400': "Server understood the request, but request content was invalid.",
                '401': "Unauthorized access.",
                '403': "Forbidden resource can't be accessed.",
                '500': "Internal server error.",
                '503': "Service unavailable."
            };

            if (e.status == 200) {
                debugger;
                //return response["data"] = e.responseText;
                msgError("Email Sending Failed..");
            }
            if (x.status) {
                message = statusErrorMap[x.status];
                if (!message) {
                    message = "Unknown Error \n.";
                }
            } else if (exception == 'parsererror') {
                message = "Error.\nParsing JSON Request failed.";
            } else if (exception == 'timeout') {
                message = "Request Time out.";
            } else if (exception == 'abort') {
                message = "Request was aborted by the server";
            } else {
                message = "Unknown Error \n.";
            }
        }
    });

    // var Rtn = httpPOSTLoader("/common/SendEmail", Data, true, "divcontainer2", "Processing..");
    // var Rtn = httpPOST("/common/SendEmail", Data, true);
    debugger;
    //if (Rtn.data.Id > 0) {
    //    OnSuccess(Rtn);
    //} else {

    //    msgError(Rtn.data.message + "\n" + Rtn.data.details);
    //}

    //function OnSuccess(result) {

    //    debugger;
    //    if (result.data.success == false) {

    //        msgError(result.data.details);
    //        //$.notify(result.message, {
    //        //    globalPosition: "top center",
    //        //    className: "Errror"
    //        //})

    //    } else {
    //        $('textarea#txtemailId').val("");
    //        msgSuccess(result.data.message);



    //    }

    //}

    //function OnFailure(result) {

    //}


})


$(".btnSendMail").click(function () {
    debugger;

    var CheckArray = []
    $("input:checkbox[class=ReportCheck]:checked").each(function () {
        CheckArray.push($(this).attr('id'));
    });

    var Data = {
        ToEmail: $('textarea#txtemailId').val(),
        Autoid: $("#HAutoId").val(),
        FmType: $("#HFmId").val(),
        //CheckList: CheckArray,
        FileName: $("#HFileName").val(),
        Sub: $("#txtsubject").val()
    }
    debugger;


    $.ajax({
        type: "POST",
        url: "/common/SendEmail",
        data: JSON.stringify(Data),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        beforeSend: function () {
            $(".btnSendMail").buttonLoader('start', 'Sending..');
        },
        complete: function (e) {
            debugger;
            $(".btnSendMail").buttonLoader('stop', '');
            //   OnSuccess(e.responseJSON);
        },
        //  async: false,
        success: function (data) {
            debugger;
            $(".btnSendMail").buttonLoader('stop', ''); 
            $('textarea#txtemailId').val("");
            if (data.success == true && data.Id > 0) {             
                $('textarea#txtemailId').val("");
                msgSuccess(data.message);

            } else {
                msgError(data.details);
            }

            // data;
        },
        error: function (e, x, settings, exception) {
            //var message;
            var statusErrorMap = {
                '400': "Server understood the request, but request content was invalid.",
                '401': "Unauthorized access.",
                '403': "Forbidden resource can't be accessed.",
                '500': "Internal server error.",
                '503': "Service unavailable."
            };

            if (e.status == 200) {
                debugger;
                //return response["data"] = e.responseText;
                msgError("Email Sending Failed..");
            }
            if (x.status) {
                message = statusErrorMap[x.status];
                if (!message) {
                    message = "Unknown Error \n.";
                }
            } else if (exception == 'parsererror') {
                message = "Error.\nParsing JSON Request failed.";
            } else if (exception == 'timeout') {
                message = "Request Time out.";
            } else if (exception == 'abort') {
                message = "Request was aborted by the server";
            } else {
                message = "Unknown Error \n.";
            }
        }
    });

    // var Rtn = httpPOSTLoader("/common/SendEmail", Data, true, "divcontainer2", "Processing..");
    // var Rtn = httpPOST("/common/SendEmail", Data, true);
    debugger;
    //if (Rtn.data.Id > 0) {
    //    OnSuccess(Rtn);
    //} else {

    //    msgError(Rtn.data.message + "\n" + Rtn.data.details);
    //}

    //function OnSuccess(result) {

    //    debugger;
    //    if (result.data.success == false) {

    //        msgError(result.data.details);
    //        //$.notify(result.message, {
    //        //    globalPosition: "top center",
    //        //    className: "Errror"
    //        //})

    //    } else {
    //        $('textarea#txtemailId').val("");
    //        msgSuccess(result.data.message);



    //    }

    //}

    //function OnFailure(result) {

    //}


})
$(".HeadModelClose").click(function () {
    debugger;
    $('#default-Modal').modal('hide');
});