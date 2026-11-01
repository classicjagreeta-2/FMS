$('.TextEditor').summernote({
    toolbar: [
        //  ['insert', ['picture', 'link', 'table', 'hr', 'summernoteGallery']],

        ['font style', ['fontname', 'fontsize', 'color', 'bold', 'italic',
            'underline', 'strikethrough', 'superscript', 'subscript', 'clear']],
        ['paragraph style', ['style', 'ol', 'ul', 'paragraph', 'height']],
        ['misc', ['codeview', 'undo', 'redo', 'help']],
        ['insert', ['table']],


        // ['extensions', ['summernoteGallery']],
    ],
   // loadOnScroll: true,
    // width: 610,
    //   width: 610, //a4
    //  height: 842,                 // set editor height
    // height: 300,                 // set editor height
   // minHeight: null,             // set minimum height of editor
   // maxHeight: null,             // set maximum height of editor
    focus: true,
   // followingToolbar: false,
  //  dialogsInBody: true
    //height: 600,                 // set editor height
    //minHeight: 600,             // set minimum height of editor
    //maxHeight: 600,
    disableResizeEditor: true

});

function FnLoadTemPlate(Index) {
    debugger;
    var RwIndex = Index;
    var JsParmaterdata = {
        RowIndex: RwIndex,
        PackingMode: $("#Hd_PackingMode").val()
    }
    var JsData = httpPOST("/TD_MastCommon/ListPatialView", JsParmaterdata, true);
    $('#IdTemplateModal').empty().append(JsData.data).modal('toggle');
}
$(".ModelTemplate").click(function () {

    var RwIndex = $(this).attr('id').split('_')[1];

    var JsParmaterdata = {
        RowIndex: RwIndex
    }
    var JsData = httpPOST("/TD_MastCommon/ListPatialView", JsParmaterdata, true);

    $('#IdTemplateModal').empty().append(JsData.data).modal('toggle');
});
$(".ModelClose").click(function () {
    $('#IdTemplateModal').empty().modal('hide');
});

function FnTemplate(e, Index, Autoid) {
    debugger;
   
    var TmpText = $("#" + e.id).html();
    //  console.log(TmpText);
   
    $('#txtTemplateText_' + Index).summernote('reset');
    var IsFixedVal = Number($("#HdFixedId_" + Autoid).val());

    if (IsFixedVal > 0) {
        var JsParmaterdata = {
            Id: IsFixedVal,
            AutoId: $("#HdOrderId").val()
        }
        var JsData = httpPOST("/TD_Contract/GetFixedData", JsParmaterdata, true);
        console.log("Index :" + Index);
       // $('#txtTemplateText_' + Index).summernote('pasteHTML', JsData.data);
        $('#txtTemplateText_' + Index).summernote('code', JsData.data);
        $('#txtTemplateText_' + Index).summernote('disable');
        debugger;
    } else {
        var JsParmaterdata2 = {            
            AutoId: Autoid
        }
        var JsData = httpPOST("/TD_MastCommon/ConvertToHtml", JsParmaterdata2, true);
        debugger;
       // $('#txtTemplateText_' + Index).html(JsData.data.HtmlDesc);
     //   $('#txtTemplateText_' + Index).summernote('code', JsData.data.HtmlDesc);
        $('#txtTemplateText_' + Index).summernote('code', JsData.data.HtmlDesc);
        
     //  $('#txtTemplateText_' + Index).summernote(JsData.data.HtmlDesc);
        $('#txtTemplateText_' + Index).summernote('enable');



      //  $('#divTemplateText_' + Index).summernote('code', JsData.data.HtmlDesc);
        
        
         
    }



    var Sub = $("#txtsubject_" + Autoid).text().trim() + " [Edit Template] "
    debugger;
    $("#IdModelTemplate_" + Index).text(Sub);
    $("#txthiddenHead_" + Index).val(Sub);

    //  $('#txtTemplateText').html(TmpText);
    // $(".summernote").summernote('code', TmpText);
    $('#IdTemplateModal').modal('hide');
}


function PdfDownLaod(element) {
    debugger;

    //html2pdf(element, {
    //    //margin: 0.5, //[1, 0, 0, 0] [T,L,B,R]
    //    margin: [0.5, 0.5, 0.5, 0.5], //[T,L,B,R]
    //    filename: 'MyContract.pdf',
    //    image: { type: 'jpeg', quality: 0.98 },
    //    html2canvas: { dpi: 192, letterRendering: true },
    //    pagebreak: { mode: 'avoid-all' },
    //    jsPDF: { unit: 'in', format: 'letter', orientation: 'portrait' }
    //});
    var ContractFileName = "ContractNote_" + $.now() + ".pdf";
    const config2 = {

        margin: [0.5, 0.5, 0.5, 0.5], //[T,L,B,R]
        filename: ContractFileName,
        image: { type: 'jpeg', quality: 1.0 },
        // html2canvas: { dpi: 192, letterRendering: true },
        html2canvas: { dpi: 192, scale: 2, letterRendering: true },
        pagebreak: { after: 'header-repeat', mode: ['avoid-all', 'css', 'legacy'] },
        jsPDF: { unit: 'in', format: 'letter', orientation: 'portrait' },
        pdfCallback: myCallback,
    };   
 

    html2pdf().from(element).set(config2).toPdf().get('pdf').then((pdf) => {
        var totalPages = pdf.internal.getNumberOfPages();

        for (i = 1; i <= totalPages; i++) {
            debugger;
            pdf.setPage(i);
            pdf.setFontSize(10);
            pdf.setTextColor(150);
            //  pdf.setTextColor("");
            //   pdf.text('This is the header text', (pdf.internal.pageSize.getWidth() / 2.40), (pdf.internal.pageSize.getHeight() - 10.5));
            pdf.text('Page ' + i + ' of ' + totalPages, (pdf.internal.pageSize.getWidth() / 2.25), pdf.internal.pageSize.getHeight() - 0.5);
           // pdf.addImage('<img src="../libraries/assets/images/Agrifields_logo3.png">', 'JPEG', pdf.internal.pageSize.getWidth() - 1.1, pdf.internal.pageSize.getHeight() - 0.25, 1, 0.2);
        }
      
    }).save();
    
            // this.elementPDF.clear();
}

function myCallback() {
    alert('');
}