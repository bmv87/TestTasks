$(document).ready(function () {

    var quartz = $.connection.notificationHub;
    
  
    var tableFactory = function (reports) {
        var target = this;
        var trCount = 0;
        var TableTitle = ["Наименование", "Хост", "Адрес", "Статус", "Время ответа"];

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
                    $('<td/>', { text: myData.Name }),
                    $('<td/>', { text: myData.Url }),
                    $('<td/>', { text: myData.Address }),
                    $('<td/>', { text: myData.Status }),
                    $('<td/>', { text: myData.RoundtripTime })
                )
            );
        });
        target.empty();
        target.append(mytable);
    };

    jQuery.fn.extend({
        buildTable: tableFactory
    });

    quartz.client.displaySiteList = function (list) {
        console.log(list);
        if (!$("#managment-table-container").length) {
            return;
        }
      
        $("#managment-table-container").buildTable(list);
    };

    $.connection.hub.start()
        .done(function () { console.log('Now connected, connection ID=' + $.connection.hub.id); })
        .fail(function () { console.log('Could not Connect!'); });

});