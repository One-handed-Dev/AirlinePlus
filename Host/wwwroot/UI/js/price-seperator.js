function setComma(pVar) {
    var x = $('#' + pVar).val();
    var re = numberWithCommas(parseInt(x.replace(/,/gi, "")));
    $('#' + pVar).val(re);
}

function numberWithCommas(x) {
    return x.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
}