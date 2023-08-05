"use strict";
var KTConcreteRequestList = function () {
    var dt, n, a;


    const f = () => {
        const t = document.querySelector('[data-kt-purchases-table-toolbar="base"]')
    };

    const loadDataTable = () => {
        return $(n).DataTable({
            "ajax": {
                "url": "/Concrete/GetAllData",
                "type": "GET",
                "datatype": "json"
            },
            "columns": [
                {
                    "data": "transactionDate",
                    "render": function (data, type, row) {
                        return moment(data).format("DD-MM-YYYY");
                    }, "width": "10%"
                },
                {
                    "data": "productId",
                    "render": function (data, type, row) {
                        return row.product.name;
                    }, "width": "15%"
                },
                {
                    "data": "quantity",
                    "render": function (data, type, row) {
                        return `<span>${data} ${row.product.unit.name}</span>`;
                    }, "width": "10%"
                },
                {
                    "data": "blockId",
                    "render": function (data, type, row) {
                        return `<span>${row.block.name}</span>`;
                    }, "width": "15%"
                },
                { "data": "description", "width": "20%" },
                {
                    "data": "requestedDate",
                    "render": function (data, type, row) {
                        a = 0;
                        row.concreteCastings.forEach((e, i) => { a += parseInt(e.quantity) })
                        return `<span>${a}/${row.quantity} ${row.product.unit.name}</span>`;
                    }, "width": "15%"
                },
                {
                    "data": "id",
                    orderable: false,
                    className: 'text-end ',
                    "render": function (data, type, row) {
                        return `<a href="/Concrete/ConcreteCasting/${data}" class="btn btn-icon btn-sm btn-primary" title="beton döküm işlemi"><i class="fas fa-truck"></i></a>`;
                    }, "width": "15%"
                }
            ],
            language: {
                sDecimal: ",",
                sEmptyTable: "Tabloda herhangi bir veri mevcut değil",
                sInfo:
                    "_TOTAL_ kayıttan _START_ - _END_ arasındaki kayıtlar gösteriliyor",
                sInfoEmpty: "Kayıt yok",
                sInfoFiltered: "(_MAX_ kayıt içerisinden bulunan)",
                sInfoPostFix: "",
                sInfoThousands: ".",
                sLengthMenu: "Sayfada _MENU_ kayıt göster",
                sLoadingRecords: "Yükleniyor...",
                sProcessing: "İşleniyor...",
                sSearch: "Ara:",
                sZeroRecords: "Eşleşen kayıt bulunamadı",
                oPaginate: {
                    sFirst: "İlk",
                    sLast: "Son",
                    sNext: "Sonraki",
                    sPrevious: "Önceki",
                },
                oAria: {
                    sSortAscending: ": artan sütun sıralamasını aktifleştir",
                    sSortDescending: ": azalan sütun sıralamasını aktifleştir",
                },
                select: {
                    rows: {
                        _: "%d kayıt seçildi",
                        0: "",
                        1: "1 kayıt seçildi",
                    },
                },
            },
            info: !1,
            order: [[0, "asc"]],
            columnDefs: [{ orderable: !1, targets: 6 }],
            "width": "100%"
        });
    };

    return {
        init: function () {
            (n = document.querySelector("#kt_concrete_request_table")) && (

                (dt = loadDataTable()),
                   // .on("draw", function () { setDeleteEvents(), setPurchaseEvents() }),

                document.querySelector('[data-kt-concrete_request-table-filter="search"]').addEventListener("keyup", (function (e) {
                    dt.search(e.target.value).draw()
                }))
            )
        }
    }
}();

KTUtil.onDOMContentLoaded((function () { KTConcreteRequestList.init() }));