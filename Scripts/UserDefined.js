
var rootDir = "@Url.Content('~/')";
$("#Market").on("change", function () {
    showValue($(this).val());
});
$("#Producer").on("change", function () {
    showValueRest($(this).val());
});

function EnvironmentValues(model) {
    $.getJSON('../Home/GetDropdownList_Env', function (result) {
        var Env_data = result.Env_Data;
        for (var i = 0; i < Env_data.length; i++) {
            if (Env_data[i] == "INT2") {

                $("#Environment").append("<option value='" + Env_data[i] + "' class='hidden' disabled>" + Env_data[i] + "</option>")
            } else if (Env_data[i] == "PROD") {
                if (model.userlogindetails.Role == "PowerUser") {
                    $("#Environment").append("<option value='" + Env_data[i] + "'>" + Env_data[i] + "</option>")
                }
                else {
                    $("#Environment").append("<option value='" + Env_data[i] + "' class='hidden' disabled>" + Env_data[i] + "</option>")
                }
            }
                else {
                $("#Environment").append("<option value='" + Env_data[i] + "'>" + Env_data[i] + "</option>")
                }
            }
    });
    return false;
}
function MarketValues(model) {
    $.getJSON('../Home/GetDropdownList_Market', function (result) {
        var Market_data = result.Market_Data;
        for (var i = 0; i < Market_data.length; i++) {
            $("#Market").append("<option value='" + Market_data[i] + "'>" + Market_data[i] + "</option>");
        }
    });
    return false;
}
function ProducerValues(model) {
    $.getJSON('../Home/GetDropdownList_Producer', function (result) {
        var Producer_data = result.Producer_Data;
        for (var i = 0; i < Producer_data.length; i++) {
            $("#Producer").append("<option value='" + Producer_data[i] + "'>" + Producer_data[i] + "</option>");
        }
    });
    return false;
}
function showValue(val) {
    $.getJSON('../Home/GetDropdownList' + "?value=" + val, function (result) {
        $("#Source").html("Select Source"); // makes select null before filling process
        var source_data = result.source_data;
        $("#Source").append("<option class='hidden' selected disabled>Select Source</option>")
        for (var i = 0; i < source_data.length; i++) {
            $("#Source").append("<option value='" + source_data[i]+"'>" + source_data[i] + "</option>")
        }
        $("#Application").html("Select Application"); // makes select null before filling process
        var app_data = result.app_data;
        $("#Application").append("<option class='hidden' selected disabled>Select Application</option>")
        for (var i = 0; i < app_data.length; i++) {
           
                $("#Application").append("<option value='" + app_data[i] + "'>" + app_data[i] + "</option>")
            
        }

    });
}
$("#Source").on("change", function () {
    showValueApp($(this).val());
});
function showValueApp(val) {
    $.getJSON('../Home/GetDropdownList_App' + "?value=" + val, function (result) {
        $("#Application").html("Select Application"); // makes select null before filling process
        var app_data = result.app_data;
        $("#Application").append("<option class='hidden' selected disabled>Select Application</option>")
        for (var i = 0; i < app_data.length; i++) {
            if ($("#Source").val() == "TRUCK" && app_data[i] == "shorttest") {
                $("#Application").append("<option value='" + app_data[i] + "' class='hidden' disabled>" + app_data[i] + "</option>")
            } else {
                $("#Application").append("<option value='" + app_data[i] + "'>" + app_data[i] + "</option>")
            }
        }

    });
}
function showValueRest(val) {
    $.getJSON('../Home/GetDropdownList_Rest' + "?value=" + val, function (result) {
        if (val == 'DaiVB/Mmc') {
            $("#Region").removeAttr("disabled");
            $("#Region").html("Select Region"); // makes select null before filling process
            var region_data = result.region_data;
            $("#Region").append("<option class='hidden' selected disabled>Select Region</option>")
            for (var i = 0; i < region_data.length; i++) {
                if (region_data[i] == "AMAP" || region_data[i] == "CN") {

                    $("#Region").append("<option value='" + region_data[i] + "' class='hidden' disabled>" + region_data[i] + "</option>")
                } else {
                    $("#Region").append("<option value='" + region_data[i] + "'>" + region_data[i] + "</option>")
                }
            }
        } else {
            $("#Region").append("<option class='hidden' selected disabled>Select Region</option>")
            $("#Region").attr("disabled", "disabled");
        }
       
        $("#UseCase").html("Select UseCase"); // makes select null before filling process
        var usecase_data = result.usecase_data;
        $("#UseCase").append("<option class='hidden' selected disabled>Select UseCase</option>")
        for (var i = 0; i < usecase_data.length; i++) {
            if (val == "TD USA" || val == "TRUCKS" || val == "Xentry Diagnosis") {

                $("#UseCase").append("<option value='" + usecase_data[i] + "' class='hidden' disabled>" + usecase_data[i] + "</option>")
            }
            else {
                $("#UseCase").append("<option value='" + usecase_data[i] + "'>" + usecase_data[i] + "</option>")
            }
        }


    });
}
$("#Region").on("change", function () {
    showValueECE($(this).val());
});
function showValueECE(val) {
    $.getJSON('../Home/GetDropdownList_ECE' + "?value=" + val, function (result) {
        $("#UseCase").html("Select Use Case"); // makes select null before filling process
        var usecase_data = result.usecase_data;
        $("#UseCase").append("<option class='hidden' selected disabled>Select UseCase</option>")
        for (var i = 0; i < usecase_data.length; i++) {
            $("#UseCase").append("<option value='" + usecase_data[i] + "'>" + usecase_data[i] + "</option>")
        }

    });
}
$('input#Submit_Rest').on("click", function () {
    showError();
});
function showError() {
    if ($('#Environment').children("option:selected").val() == "Select Environment"||$('#Environment').val()=="") {
        $('#Environment').addClass("error_val");    }
    else {
        $('#Environment').removeClass("error_val");
    }
    if ($('#Producer').children("option:selected").val() == "Select Producer" || $('#Producer').val()=="") {
        $('#Producer').addClass("error_val");
    }
    else {
        $('#Producer').removeClass("error_val");
    }
    if (($('#Region').children("option:selected").val() == "Select Region" || $('#Region').val()=="") && !($('#Region').is(':disabled'))) {
        $('#Region').addClass("error_val");
    }
    else {
        $('#Region').removeClass("error_val");
    }
    if (($('#UseCase').children("option:selected").val() == "Select UseCase" || $('#UseCase').val()=="") && !($('#UseCase').is(':disabled'))) {
        $('#UseCase').addClass("error_val");
    }
    else {
        $('#UseCase').removeClass("error_val");
    }
    if ($('input[id="FIN/VIN"]').val() == "") {
        $('input[id="FIN/VIN"]').addClass("error_val");
    }
    else {
        $('input[id="FIN/VIN"]').removeClass("error_val");
    }
    if ($('textarea[id="inputfile"]').val() == "") {
        $('textarea[id="inputfile"]').addClass("error_val");
    }
    else {
        $('textarea[id="inputfile"]').removeClass("error_val");
    }
    if ($('textarea[id="inputfile"]').hasClass('error_val') || $('input[id="FIN/VIN"]').hasClass('error_val') || $('#UseCase').hasClass('error_val') || $('#Region').hasClass('error_val') || $('#Producer').hasClass('error_val') || $('#Environment').hasClass('error_val')) {
        err = false;
    }
    else {

        err = true;
    }
    return err;
}
$('input#Submit_Mq').on("click", function () {
    showError_mq();
});
function showError_mq() {
    var err = true;
    if ($('#Environment').children("option:selected").val() == "Select Environment" || $('#Environment').val()=="") {
        $('#Environment').addClass("error_val"); 
    }
    else {
        $('#Environment').removeClass("error_val");
    }
    if ($('#Market').children("option:selected").val() == "Select Market" || $('#Market').val()=="") {
        $('#Market').addClass("error_val"); 
    }
    else {
        $('#Market').removeClass("error_val"); 
    }
    if (($('#Source').children("option:selected").val() == "Select Source" || $('#Source').val()=="") && !($('#Source').is(':disabled'))) {
        $('#Source').addClass("error_val");
    }
    else {
        $('#Source').removeClass("error_val"); 
    }
    if (($('#Application').children("option:selected").val() == "Select Application" || $('#Application').val()=="") && !($('#Application').is(':disabled'))) {
        $('#Application').addClass("error_val"); 
    }
    else {
        $('#Application').removeClass("error_val"); 
    }
    if ($('textarea[id="inputfile"]').val() == "") {
        $('textarea[id="inputfile"]').addClass("error_val"); 
    }
    else {
        
        $('textarea[id="inputfile"]').removeClass("error_val"); 
    }
    if ($('textarea[id="inputfile"]').hasClass('error_val') || $('#Application').hasClass('error_val') || $('#Source').hasClass('error_val') || $('#Market').hasClass('error_val') || $('#Environment').hasClass('error_val')) {
        err = false;
    }
    else {
        err = true;
    }
    return err;
}
$('.btnReset.mq').on("click", function () {
    ResetFields_mq();
});

function ResetFields_mq() {
    //removing errors for all the fields
    $('#Environment').removeClass("error_val");
    $('#Market').removeClass("error_val");
    $('#Source').removeClass("error_val");
    $('#Application').removeClass("error_val");
    $('input[id="FilePath"]').removeClass("error_val");
    $('textarea[id="inputfile"]').removeClass("error_val");

    //reset the values to default
    $('#Environment').val("Select Environment");
    $('#Market').val("Select Market");
    $("#Source").empty();
    $("#Source").append("<option class='hidden' selected disabled>Select Source</option>");
    $("#Application").empty();
    $("#Application").append("<option class='hidden' selected disabled>Select Application</option>");
    $('input[id="FilePath"]').val("");
    $('textarea[id="inputfile"]').val("");
    $('textarea[id="Result_MQ"]').val("");
}
$('.btnReset.rest').on("click", function () {
    ResetFields_rest();
});

function ResetFields_rest() {
    //removing errors for all the fields
    $('#Environment').removeClass("error_val");
    $('#Producer').removeClass("error_val");
    $('#Region').removeClass("error_val");
    $('#UseCase').removeClass("error_val");
    $('input[id="FIN/VIN"]').removeClass("error_val");
    $('input[id="FilePath"]').removeClass("error_val");
    $('textarea[id="inputfile"]').removeClass("error_val");

    //reset the values to default
    $('#Environment').val("Select Environment");
    $('#Producer').val("Select Producer");
    $("#Region").empty();
    $("#Region").append("<option class='hidden' selected disabled>Select Region</option>");
    $("#UseCase").empty();
    $("#UseCase").append("<option class='hidden' selected disabled>Select UseCase</option>");
    $('input[id="FIN/VIN"]').val("");
    $('input[id="TID"]').val("");
    $('input[id="FilePath"]').val("");
    $('textarea[id="inputfile"]').val("");
    $('textarea[id="Result_REST"]').val("");
}

window.onload = function () {
    MarketValues();
    ProducerValues();
    document.getElementById('btnUpload').onclick = function () {
        var fileInput = document.getElementById('fileInput');
        var xhr = new XMLHttpRequest();
        xhr.open('POST', '../Home/Upload');
        xhr.setRequestHeader('Content-type', 'multipart/form-data');
        //Appending file information in Http headers
        xhr.setRequestHeader('X-File-Name', fileInput.files[0].name);
        xhr.setRequestHeader('X-File-Type', fileInput.files[0].type);
        xhr.setRequestHeader('X-File-Size', fileInput.files[0].size);
        //Sending file in XMLHttpRequest
        xhr.send(fileInput.files[0]);
        xhr.onreadystatechange = function () {
            if (xhr.readyState == 4 && xhr.status == 200) {
                $('textarea#inputfile').val(xhr.responseText);
            }
        }
    }
}
$(function () {
    $('#form_REST').on("submit", function (e) {
        if (showError()) {
            $.ajax({
                type: this.method,
                url: this.action,
                async: false,
                cache: false,
                data: $(this).serialize(),
                success: function (result) {
                    $('textarea#Result_REST').val("Result:: " + result.Result[0] + "\n");
                    $('textarea#Result_REST').val($('textarea#Result_REST').val() + "Tracking ID::" + result.Result[1] + "\n");
                    $('textarea#Result_REST').val($('textarea#Result_REST').val() + "Endpoint::" + result.Result[2]);

                }

            });
        }
        e.stopImmediatePropagation();
        return false;
    });
});
$(function () {
    $('#form_MQ').on("submit", function (e) {
        if (showError_mq()) {
            $.ajax({
                type: this.method,
                url: this.action,
                async: false,
                cache: false,
                data: $(this).serialize(),
                success: function (result) {
                    $('textarea#Result_MQ').val("Result:: " + result.Result[0] + "\n");
                    $('textarea#Result_MQ').val($('textarea#Result_MQ').val() + "Tracking ID:: " + result.Result[1] + "\n");

                }

            });
        }
        e.stopImmediatePropagation();
        return false;
    });
});
$('#Environment').on('change', function () {
    if ($(this).val() != "" && $(this).hasClass('error_val')) {
        $(this).removeClass('error_val');
    }
});
$('#Market').on('change', function () {
    if ($(this).val() != "" && $(this).hasClass('error_val')) {
        $(this).removeClass('error_val');
    }
});
$('#Source').on('change', function () {
    if ($(this).val() != "" && $(this).hasClass('error_val')) {
        $(this).removeClass('error_val');
    }
});
$('#Application').on('change', function () {
    if ($(this).val() != "" && $(this).hasClass('error_val')) {
        $(this).removeClass('error_val');
    }
});
$('#inputfile').on('change', function () {
    if ($(this).val() != "" && $(this).hasClass('error_val')) {
        $(this).removeClass('error_val');
    }
});
$('#Producer').on('change', function () {
    if ($(this).val() != "" && $(this).hasClass('error_val')) {
        $(this).removeClass('error_val');
    }
});
$('#Region').on('change', function () {
    if ($(this).val() != "" && $(this).hasClass('error_val')) {
        $(this).removeClass('error_val');
    }
});
$('#UseCase').on('change', function () {
    if ($(this).val() != "" && $(this).hasClass('error_val')) {
        $(this).removeClass('error_val');
    }
});
$('input[id="FIN/VIN"]').on('change', function () {
    if ($(this).val() != "" && $(this).hasClass('error_val')) {
        $(this).removeClass('error_val');
    }
});

