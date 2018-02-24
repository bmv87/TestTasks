$(function () {

    var currentDate = moment().format('DD.MM.YYYY');
    var laterDate = moment().add(1, 'months').format('DD.MM.YYYY');
    $.datepicker.setDefaults($.datepicker.regional["ru"]);

    $("#DateBegin,#DateEnd").datepicker({
        inline: true,
        constrainInput: true,
        dateFormat: "dd.mm.yy",
        firstDay: 1
    });

    //$("#DateEnd").datepicker({
    //    inline: true,
    //    constrainInput: true,
    //    dateFormat: "dd.mm.yy",
    //    firstDay: 1
    //});


    $("#DateBegin").val(currentDate);
    $("#DateEnd").val(laterDate);


    $("#DateBegin, #DateEnd").change(function (event) {
        ValidateForm();
    });
    //$("#DateEnd").change(function (event) {
    //    ValidateForm();
    //});

    $("#submitParam").click(function (event) {
        event.preventDefault();
        var getReport1Url = "api/DataApi/GetReport1";
        var getReport2Url = "api/DataApi/GetReport2";
        var getReport3Url = "api/DataApi/GetReport3";
        var isValid = ValidateForm();
        if (!isValid) {
            return;
        }

        var data = JSON.stringify(GetData());
        var sections = $(".section");
        sections.html("");
        SendRequest(getReport1Url, data, "#panel-2");
        SendRequest(getReport2Url, data, "#panel-3");
        SendRequest(getReport3Url, null, "#panel-4");
    });

    function GetData() {
        var dBegin = moment($("#DateBegin").val(), "DD.MM.YYYY");
        var dEnd = moment($("#DateEnd").val(), "DD.MM.YYYY");
        var param1 = $("#Param1").is(':checked');
        var param2 = $("#Param2").is(':checked');
        var param3 = $("#Param3").is(':checked');

        return {
            DateBegin: dBegin.format(),
            DateEnd: dEnd.format(),
            Param1: param1,
            Param2: param2,
            Param3: param3
        }
    }
    function ValidateForm() {
        var isValid = true;
        var dBegin = $("#DateBegin").val();
        var dEnd = $("#DateEnd").val();
        var pattern = /^(\d{2}[.]){2}\d{4}$/;

        if (!dBegin || !moment(dBegin, 'DD.MM.YYYY').isValid() || !pattern.test(dBegin)) {
            $("#DateBegin").addClass("is-invalid");
            isValid = false;
        }
        else {
            $("#DateBegin").removeClass("is-invalid");
        }
        if (!dEnd || !moment(dEnd, 'DD.MM.YYYY').isValid() || !pattern.test(dEnd)) {
            $("#dateEndError").html("Дата должна быть задана в формате DD.MM.YYYY");
            $("#DateEnd").addClass("is-invalid");
            isValid = false;
        }
        else {
            $("#DateEnd").removeClass("is-invalid");
        }
        if (moment(dBegin, 'DD.MM.YYYY').isAfter(moment(dEnd, 'DD.MM.YYYY'))) {
            $("#dateEndError").html("Дата 2 должна быть больше Дата1 ");
            $("#DateEnd").addClass("is-invalid");
            isValid = false;
        }
        else if (pattern.test(dEnd)) {
            $("#DateEnd").removeClass("is-invalid");
        }
        return isValid;
    }

    $(document).ajaxStart(function () {
        var selector = ".progress-bar";
        var progressbar = $(selector);
        progressbar.parent().show();
        progressbar.css("width", "25%");
    });

    //$(document).ajaxSuccess(function () {
    //    var selector = ".progress-bar";
    //    var progressbar = $(selector);
    //    progressbar.parent().hide(2000, "linear");
    //});

    function Progress(xhr, targetBlockSelector) {
        xhr.addEventListener("progress", function (evt) {
            var selector = targetBlockSelector + " .progress-bar";
            var progressbar = $(selector);
            progressbar.parent().show();
            if (evt.lengthComputable) {
                if (targetBlockSelector == "#panel-4")
                    progressbar.parent().parent().find("h5").show();
                var percentComplete = (evt.loaded / evt.total) * 100;
                console.log(percentComplete);
                progressbar.css("width", percentComplete + '%');
            }
        }, false);
        return xhr;
    }

    function SendRequest(url, data, targetBlockSelector) {
        $.ajax({
            xhr: function () {
                var xhr = new window.XMLHttpRequest();
                return Progress(xhr, targetBlockSelector);
            },
            type: 'POST',
            url: url,
            data: data,
            success: function (data) {
                AddSuccessData(data, targetBlockSelector);
            },
            error: function (data) {
                AddErrorData(data, targetBlockSelector);
            },
            dataType: "json",
            contentType: "application/json"
        });
    }

    function AddSuccessData(data, targetBlockSelector) {
        var selector = targetBlockSelector + " .section";
        var target = $(selector);
        BuildTable(data, target);

        if (targetBlockSelector == "#panel-4")
            target.parent().find("h5").hide();
        target.parent().find(".progress").hide();
    }

    function AddErrorData(data, targetBlockSelector) {
        var selector = targetBlockSelector + " .section";
        var target = $(selector);

        if (data.responseJSON) {
            target.html(data.responseJSON.Message);
        }
        else if (data.responseText) {
            try {

                target.html(data.responseText);
                target.find("style").remove();
            } catch (ex) {
                console.error("ошибка при разборе ответа");
            }
        }
        if (targetBlockSelector == "#panel-4") {
            target.parent().find("h5").hide();
        }
        target.parent().find(".progress").hide();
    }

    //function BuildMarkup(data, target) {
    //    target.html(data.message);
    //}

    function BuildTable(reports, target) {
        var trCount = 0;
        var TableTitle = ["#", "Поле 1", "Поле 2", "Поле 3", "Поле 4", "Поле 5"];
     
        var mytable = $('<table/>', {
            class: 'table table-bordered table-striped text-center'
        }).append(
            $('<thead/>', {
                class: 'thead-dark'
            }),
            $('<tfoot/>'),
            $('<tbody/>')
            );

        var TitleCell = $('<tr/>');
        $.each(TableTitle, function (myIndex, myData) {
            TitleCell.append(
                $('<th/>', {
                    text: myData,
                    scope: "col"
                })
            );
        });
        $("thead", mytable).append(TitleCell);

        $.each(reports, function (i, myData) {
            $("tbody", mytable).append(
                $('<tr/>').append(
                    $('<th/>', { text: ++trCount, scope: "row" }),
                    $('<td/>', { text: myData.Field1 }),
                    $('<td/>', { text: myData.Field2 }),
                    $('<td/>', { text: myData.Field3 }),
                    $('<td/>', { text: myData.Field4 }),
                    $('<td/>', { text: myData.Field5 })
                )
            );
        });
        target.append(mytable);
    }
});


