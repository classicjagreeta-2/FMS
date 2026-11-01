$("#btnUplaod").click(function () {
    debugger;
    var images = [];
    $("#selectedFiles img").each(function () {
        images.push($(this).attr('src'))
    })
    var Obj = {};
    Obj.array = images;
    var SId = $("#Idsession").val();


    for (var i = 0; i < images.length; i++) {
        debugger;
        var Obj = {};
        Obj.base64 = images[i];
        Obj.SessionId = SId;
        //Obj.I = i.toString();
        $.ajax({
            type: 'POST',
            url: '../Home/FileUpload',// UploadFiles ,FileUpload
            data: JSON.stringify(Obj),
            contentType: 'application/json; charset=utf-8',
            datatype: 'json',
            success: function (result) {
                debugger;
                $("#txtUploadedfilecount").val(result.FUploaded);
                BtnSubmitProperty();  
                $.notify("File Uploaded",
                    {
                        position: "right middle",
                        className: 'superblue'
                    });
               // alert('Success ');
            },
            error: function (result) {

                alert(result.responseText);
            }
        });
    }

})