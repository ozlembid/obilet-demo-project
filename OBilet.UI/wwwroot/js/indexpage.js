$(document).ready(function () {
    debugger;
    $("#changeInfo").on('click', function (ev) {
        debugger;
        var a = $("#frombuslocation option:selected").text();
        var b = $("#tobuslocation option:selected").text();
        if (a == b) {
            alert("Hem başlangıç hem de varış yeri olarak aynı konumu seçilemez.");
        } else {
            swaper();
        }
    });
});

function swaper() {
    var co = $("#frombuslocation").val();
    var coText = $("#frombuslocation option:selected").text();
    var co_2 = $("#tobuslocation").val();
    var co_2Text = $("#tobuslocation option:selected").text();

    var toSpan = document.querySelector('#tobuslocation_chosen a span');
    var result = $(toSpan).text(coText);
    var resultValues = $(toSpan).val(co);
    var fromSpan = document.querySelector('#frombuslocation_chosen a span');
    var result1 = $(fromSpan).text(co_2Text);
    var result1Value = $(fromSpan).val(co_2);

    $("#frombuslocation option:selected").val(result1Value.val());
    $("#frombuslocation option:selected").text(result1.text());
    $("#tobuslocation option:selected").val(resultValues.val());
    $("#tobuslocation option:selected").text(result.text());
}

$(function () {
    debugger;
    document.getElementById('today').style.backgroundColor = 'white'
    document.getElementById('tomorrow').style.backgroundColor = 'grey'
    var tomorrow = new Date();
    tomorrow.setDate(tomorrow.getDate() + 1);
    document.getElementById("flatpickr").value = tomorrow.toDateString();
});

$("#flatpickr").flatpickr({
    enableTime: true,
    //dateFormat: "d F Y l",
    dateFormat: "D M d Y",
    minDate: "today"
});

$("#today").click(function () {
    debugger;
    document.getElementById('today').style.backgroundColor = 'grey'
    document.getElementById('tomorrow').style.backgroundColor = 'white'
    var date = new Date();
    document.getElementById("flatpickr").value = date.toDateString();
});

$("#tomorrow").click(function () {
    document.getElementById('today').style.backgroundColor = 'white'
    document.getElementById('tomorrow').style.backgroundColor = 'grey'
    var tomorrow = new Date();
    tomorrow.setDate(tomorrow.getDate() + 1);
    document.getElementById("flatpickr").value = tomorrow.toDateString();
});

$(function () {
    $("#frombuslocation").chosen();
    $(".chosen-single").css("border", "0");

    $("#tobuslocation").chosen();
    $(".chosen-single").css("border", "0");
});

$("#submit").click(function () {
    debugger;
    var a = $("#frombuslocation option:selected").val();
    var b = $("#tobuslocation option:selected").val();
    if (a == "" && b == "") {
        $("#frombuslocationvalidation").show();
        $("#tobuslocationvalidation").show();
        return false;
    }
    else if (a == "") {
        $("#frombuslocationvalidation").show();
        $("#tobuslocationvalidation").hide();
        return false;
    }
    else if (b == "") {
        $("#tobuslocationvalidation").show();
        $("#frombuslocationvalidation").hide();
        return false;
    }
});

$(function () {
    debugger;
    var originvaltext = $("#originlocation").val();
    var originvalId = $("#originlocationid").val();
    var destinationText = $("#destinationlocation").val();
    var destinationid = $("#destinationlocationid").val();

    if (originvaltext != null && destinationText != null && $("#departure").val() != null) {
        var toSpan = document.querySelector('#tobuslocation_chosen a span');
        var result = $(toSpan).text(originvaltext);
        var resultValues = $(toSpan).val(originvalId);
        var fromSpan = document.querySelector('#frombuslocation_chosen a span');
        var result1 = $(fromSpan).text(destinationText);
        var result1Value = $(fromSpan).val(destinationid);

        $("#frombuslocation option:selected").val(result1Value.val());
        $("#frombuslocation option:selected").text(result1.text());
        $("#tobuslocation option:selected").val(resultValues.val());
        $("#tobuslocation option:selected").text(result.text());

        var departure = $("#departure").val();
        document.getElementById("flatpickr").value = new Date(departure).toDateString();
    }
});