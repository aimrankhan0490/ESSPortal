
    $(document).ready(function () {
        $.ajax({
            url: "@Url.Action('getrequestreasons', 'Loan")",
            //  data: { selectedValue: selectedValue },
            dataType: "json",
        type: "GET",
        error: function () {
            alert(" An error occurred.");
        },
        success: function (data) {
            //  $("#reqnatutre")
            var optionhtml1 = '<option value="' + -1 + '">' + "--Select Reason--" + ' </option>';
            $("#ddlreason").append(optionhtml1);
            $.each(data, function (i) {
                debugger;
                $("#ddlreason").append($('<option>').text(data[i]).attr('value', data[i]));
                //var optionhtml = '<option value="' + data[i + 1] + '">' + data[i + 1] + '</option>';
                //$(".ddlreason").append(optionhtml);
            });
        }
    });
       
var d = new Date();
$('#fileupload').bind('change', function () {
    debugger;
    // var ext = fileName.split('.').pop();
    // alert(ext);
    var files = $("#fileupload")[0].files[0];
    var temp = "";
    temp += "<br>Filename: " + files.name;
    var ext = temp.split(".").pop();
    if (ext == "JPEG" || ext == "PNG" || ext == "Jpeg" || ext == "Png" || ext == "jpeg" || ext == "png") { }
    else
        alert("Only Jpeg and Png formats");

    //  alert('This file size is: ' + this.files[0].size / 1024 / 1024 + "MB");
});
$("#requestdate").val(d.getDate() + '/' + (d.getMonth() + 1) + '/' + d.getFullYear());
$('#btnsubmit').click(function () {
    debugger;
    if ($('#ddlreason').val() != "-1" && $('#loantype').val() != ""
        && $('#deducstarttdate').val() != "" && $('#noofinstallment').val() != ""
        && $('#loanamount').val() != "") {

        $.ajax({
            url: "@Url.Action("CreateLoanRequest", "Loan")",
            type: 'GET',
        data: { reason: $('#ddlreason').val(),
            comment: $('#comment').val(), 
            loantype: $('#loantype').val(),
            loanamount: $('#loanamount').val(),
            noofinstallment: $('#noofinstallment').val(),
            deductionistartdate: $('#deducstarttdate').val(),

            },
        contentType: "application/json; charset=utf-8",


        error: function () {
            alert(" An error occurred.");
        },
        success: function (data1) {
            alert(data1);
            // alert(data1.split(' ')[1]);
            var url = "/Loan/ViewLoanbyRequestId?loanreqid=" + data1.split(' ')[1];
            window.location.href = url;
        }

    });
}
else {
                alert("");
}

});
});
