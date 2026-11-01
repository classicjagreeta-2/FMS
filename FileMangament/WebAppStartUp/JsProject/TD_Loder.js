
function showLoading() {

    $('div#divcontainer').block({
        message: 'Loading...Please wait',
        // overlayCSS: { backgroundColor: '#2c2828f5' }
    });

}

function showLoadingMsg(msg) {

    $('div#divcontainer').block({
        message: msg,
        // overlayCSS: { backgroundColor: '#2c2828f5' }
    });

}
$('a.aLoader').click(function () {
    debugger;
    showLoading();

});


function showLoading2(IdDiv,msg) {

    $('div#' + IdDiv).block({
        message: msg,
        // overlayCSS: { backgroundColor: '#2c2828f5' }
    });

}

function HideLoading2(IdDiv) {

    $('div#'+IdDiv).unblock();
}

function HideLoading() {

    $('div#divcontainer').unblock(); 
}