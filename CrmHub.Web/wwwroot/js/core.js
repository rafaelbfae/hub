ko.bindingHandlers.inputmask =
{
    init: function (element, valueAccessor, allBindingsAccessor) {
        var mask = valueAccessor();
        var observable = mask.value;

        if (ko.isObservable(observable)) {
            $(element).on('focusout change', function () {
                if ($(element).inputmask('isComplete')) {
                    observable($(element).val());
                } else {
                    observable(null);
                }
            });
        }

        $(element).inputmask(mask);
    },
    update: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
        var mask = valueAccessor();
        var observable = mask.value;
        if (ko.isObservable(observable)) {
            var valuetoWrite = observable();
            $(element).val(valuetoWrite);
        }
    }
};

//ko.validation.locale("pt-BR");
ko.validation.rules.pattern.message = "Inválido.";
ko.validation.init({
    registerExtenders: true,
    messagesOnModified: true,
    insertMessages: true,
    parseInputAttributes: true,
    messageTemplate: null,
    errorClass: "error"
}, true);

; (function ($) {

    $.crmhub = $.crmhub || {};
    $.crmhub.core = {};

    var ajax = function (type, url, data, onSuccess, onError, onFinish) {
        if (url.indexOf('?') > -1) {
            url = type === "GET" ? url + "&r=" + Math.random() : url;
        } else {
            url = type === "GET" ? url + "?r=" + Math.random() : url;
        }
        $.ajax({
            type: type,
            dataType: "json",
            url: $.crmhub.url + url,
            data: data,
            //headers: headers
            contentType: "application/json; charset=utf-8",
            complete: onFinish,
            success: function (response) {
                return onSuccess(response);
            },
            error: onError || function (xhr) { alert(xhr.status + ': ' + xhr.statusText); }
        });
    }

    var initDataTable = function (title, noSort, options, selector) {
        selector = selector || ".table";
        var oTable = $(selector).dataTable({
            dom: "<'row'<'col-sm-5'l><'col-sm-3 text-center'B><'col-sm-4'f>><t><'row'<'col-sm-3'i><'col-sm-9'p>>",
            "lengthMenu": [[10, 25, 50, -1], [10, 25, 50, "All"]],
            buttons: [
                { extend: "copy", className: "btn-sm" },
                { extend: "csv", title: "title", className: "btn-sm" },
                { extend: "pdf", title: "title", className: "btn-sm" },
                { extend: "print", className: "btn-sm" }
            ],
            "aoColumnDefs": [{ "bSortable": false, "aTargets": [noSort] }],
            "language": {
                "url": "https://cdn.datatables.net/plug-ins/1.10.10/i18n/Portuguese-Brasil.json"
            },
            options
        });

        return oTable;
    }

    var initDataTableServeSide = function (title, noSort, url, columns, order, selector) {
        selector = selector || ".table";
        order = order || [[0, "desc"]]
        var oTable = $(selector).DataTable({
            dom: "<'row'<'col-sm-5'l><'col-sm-3 text-center'B><'col-sm-4'f>><t><'row'<'col-sm-3'i><'col-sm-9'p>>",
            "lengthMenu": [[10, 25, 50], [10, 25, 50]],
            buttons: [
                { extend: "copy", className: "btn-sm" },
                { extend: "csv", title: "title", className: "btn-sm" },
                { extend: "pdf", title: "title", className: "btn-sm" },
                { extend: "print", className: "btn-sm" }
            ],
            "language": {
                "url": "https://cdn.datatables.net/plug-ins/1.10.10/i18n/Portuguese-Brasil.json"
            },
            "aoColumnDefs": [{ "bSortable": false, "aTargets": [noSort] }],
            "order": order,
            "columns": columns,
            "processing": true,
            "serverSide": true,
            "ajax": {
                "url": $.crmhub.url + url,
                "data": function (d) {
                    var q =
                    {
                        pDraw: d.draw,
                        pLength: d.length,
                        pStart: d.start,
                        pOrder: d.order[0].column,
                        pDir: d.order[0].dir,
                        pSearch: d.search.value
                    }
                    return q;
                }
            },
            "initComplete": function () {
                var input = $('.dataTables_filter input').unbind(),
                    self = this.api(),
                    $searchButton = $('<button class="btn btn-default btn-circle">')
                               .click(function () {
                                   self.search(input.val()).draw();
                               }).append('<i class="fa fa-search">'),
                    $clearButton = $('<button class="btn btn-default btn-circle">')
                               .click(function () {
                                   input.val('');
                                   $searchButton.click();
                               }).append('<i class="fa fa-close">')
                $('.dataTables_filter').append($searchButton, $clearButton);
            }
        });

        return oTable;
    }

    var alertConfirm = function (actionConfirm) {
        swal({
            title: "Você tem certeza?",
            text: "Não será mais possível recuperar este registro.",
            type: "warning",
            showCancelButton: true,
            confirmButtonColor: "#DD6B55",
            confirmButtonText: "Sim, apague o registro!"
        },
        function (isConfirm) {
            if (isConfirm) {
                actionConfirm();
            }
        });
    };

    $.crmhub.core.ajax = ajax;
    $.crmhub.core.initDataTable = initDataTable;
    $.crmhub.core.initDataTableServeSide = initDataTableServeSide;
    $.crmhub.core.alertConfirm = alertConfirm;
})(jQuery);

