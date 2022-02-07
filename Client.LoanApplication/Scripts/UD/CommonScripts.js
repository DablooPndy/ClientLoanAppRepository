document.getElementById("Amount").addEventListener("blur", LTVCaculation);
document.getElementById("Valuation").addEventListener("blur", LTVCaculation);
function LTVCaculation() {
    var CalculatedLTV = 0;
    $('#lblLTV').html(CalculatedLTV);
    if (!isNaN((parseFloat($('#Amount').val()))) && !isNaN((parseFloat($('#Amount').val())))) {

        CalculatedLTV = ((parseFloat($('#Amount').val()) / parseFloat($('#Valuation').val())) * 100);
    }
    else {
        CalculatedLTV = 0;
    }
    $(".LTVClass").html(CalculatedLTV.toFixed(2));
}