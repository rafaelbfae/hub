;
(function ($) {

    $.crmhub = $.crmhub || {};
    $.crmhub.logger = {};
    $.oTable = {};

    var ViewModel = function () {
        var self = this;
        self.Id = ko.observable();
        self.Crm = ko.observable().extend({ required: true });
        self.Send = ko.observable().extend({ required: true });
        self.Type = ko.observable().extend({ required: true });
        self.Entity = ko.observable().extend({ required: true });
        self.Method = ko.observable().extend({ required: true });
        self.Response = ko.observable().extend({ required: true });
        self.CreatedAt = ko.observable().extend({ required: true });
        self.UpdatedAt = ko.observable().extend({ required: true });
        self.Parameters = ko.observable().extend({ required: true });

        self.Errors = ko.validation.group(self);
        self.isDisabled = ko.observable(false);

        self.clearModel = function () {
            var reg = Object.assign({}, this);
            delete reg.isDisabled;
            delete reg.Errors;
            return ko.toJSON(reg);
        }

        self.submit = function (event) {
            if (self.Errors().length > 0) {
                self.Errors.showAllMessages();
                return;
            }

            $.crmhub.core.ajax("PUT", `Logger?id=${self.Id()}`, self.clearModel(),
                function (response) {
                    if (method === "POST") {
                        //self.Companies.push(response);
                        //$.crmhub.core.updateDataTableRow($.oTable);
                    }

                    $("#viewLogger").modal("hide");
                    self.reset();
                },
                function (xhr) {
                    alert(xhr.status + ': ' + xhr.statusText);
                },
                function () {
                    self.Errors.showAllMessages(false);
                });
        }

        self.ident = function (data) {
            var obj = JSON.parse(data);
            return JSON.stringify(obj, undefined, 4);
        }

        self.jsons = function (id) {
            $.crmhub.core.ajax("GET",
                   `Logger/SendResponse?id=${id}`,
                   null,
                   function (data) {
                       self.Send(self.ident(data.send));
                       self.Response(self.ident(data.response));
                       $("#viewLogger").modal("show");
                   });
        }

        self.view = function (selected) {
            self.setValues(selected, true);
            self.jsons(self.Id())
            
        }

        self.edit = function (selected) {
            self.setValues(selected, false);
            self.jsons(self.Id())
        }

        self.reset = function () {
            self.setValues({}, false);
        }

        self.load = function () {
            var columns = [{ "data": "id" }, { "data": "crm" }, { "data": "entity" }, { "data": "method" },
                            { "data": "type" }, { "data": "createdAt" }, { "data": "updatedAt" }, { "defaultContent": 
                            '<div class="action-buttons">' +
                                '<a class="table-actions" href="#"><i class="fa fa-eye"></i></a>' +
                                '<a class="table-actions" href="#"><i class="fa fa-pencil"></i></a>' +
                            '</div>'}]
            $.oTable = $.crmhub.core.initDataTableServeSide("Logger", -1, "Logger/All", columns, [[6, "desc"]]);

            $.oTable.on('click', 'a > i.fa-eye', function () {
                var data = $.oTable.row($(this).parents('tr')).data();
                self.view(data);
            });

            $.oTable.on('click', 'a > i.fa-pencil', function () {
                var data = $.oTable.row($(this).parents('tr')).data();
                self.edit(data);
            });
        }

        self.setValues = function (selected, disabled) {
            self.Id(selected.id);
            self.Crm(selected.crm);
            self.Send(selected.send);
            self.Type(selected.type);
            self.Entity(selected.entity);
            self.Method(selected.method);
            self.Response(selected.response);
            self.CreatedAt(selected.createdAt);
            self.UpdatedAt(selected.updatedAt);
            self.Parameters(selected.parameters);

            self.isDisabled(disabled);
            self.Errors.showAllMessages(false);
        }

        self.postProcessing = function () {
            console.log(elements);
        }
    }

    $(function () {
        var viewModel = new ViewModel();
        ko.applyBindings(viewModel);
        viewModel.load();
    });
})(jQuery);
