$(".btnpdfdownload").click(function () {

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
    link.href = "/TD_Report/TD_Report_DetailsAjax?Autoid=" + $("#HAutoId").val() + "&FmType=" + $("#HFmId").val() + "&CheckList=" + CheckArray + " ";
    link.target = "_blank";
    document.body.appendChild(link);
    link.click();

});

//function LoadCheckList(Id, FmType) {
//    var ObjData = {

//    };

//    var AjxReturn = httpPOST("../common/LoadReportCheckList", ObjData, true);
//    debugger;

//    $("#lblCheckBoxList").empty().append(AjxReturn.data);
//    $('#default-Modal').modal('show');

//    $("#HAutoId").val(Id);
//    $("#HFmId").val(FmType);

//}