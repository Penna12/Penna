"use strict";
var KTPurchaseList = function () {
    var dt, n, p;

    var priorityEnumList = () => {
        return $.ajax({
            url: "/Purchase/GetPriorityEnumList/",
            type: "GET",
        });
    };

    const f = () => {
        const t = document.querySelector('[data-kt-purchases-table-toolbar="base"]')
    };

    const loadDataTable = () => {
        return $(n).DataTable({
            "ajax": {
                "url": "/Purchase/GetAllData",
                "type": "GET",
                "datatype": "json"
            },
            "columns": [
                {
                    "data": "createdDate",
                    "render": function (data, type, row) {
                        return moment(data).format("DD-MM-YYYY");
                    }, "width": "10%"
                },
                { "data": "product.name", "width": "15%" },
                {
                    "data": "quantity",
                    "render": function (data, type, row) {
                        return `<span>${data} ${row.product.unit.name}</span>`;
                    }, "width": "10%"
                },
                {
                    "data": "remainingQuantity",
                    "render": function (data, type, row) {
                        return `<span>${data} ${row.product.unit.name}</span>`;
                    }, "width": "10%"
                },
                {
                    "data": "deadline",
                    "render": function (data, type, row) {
                        return moment(data).format("DD-MM-YYYY");
                    }, "width": "15%"
                },
                { "data": "description", "width": "15%" },
                {
                    "data": "priority",
                    "render": function (data, type, row) {
                        return p != undefined ? `<span class="badge badge-danger">${p[data - 1].text}</span>` : '';
                    }, "width": "10%"
                },
                {
                    "data": "id",
                    orderable: false,
                    className: 'text-end ',
                    "render": function (data, type, row) {
                        return `<a href="/Purchase/DirectPurchase/${data}" class="btn btn-icon btn-sm btn-primary" data-link="${data}" data-product-id="${row.productId}" data-remaining="${row.remainingQuantity}" data-type="1" data-kt-purchases-table-filter="purchase_row" data-bs-toggle="tooltip" data-bs-custom-class="tooltip-dark" data-bs-placement="left" title="doğrudan satın alma"><i class="fas fa-shopping-basket"></i></a>
                                <a href="/Purchase/OfferPurchase/${data}" class="btn btn-icon btn-sm btn-warning" data-link="${data}" data-product-id="${row.productId}" data-remaining="${row.remainingQuantity}" data-type="2" data-kt-purchases-table-filter="purchase_row" data-bs-toggle="tooltip" data-bs-custom-class="tooltip-dark" data-bs-placement="left" title="teklif usulü satın alma"><i class="fas fa-shopping-basket"></i></a>
                                <a href="/Purchase/TenderPurchase/${data}" class="btn btn-icon btn-sm btn-danger" data-link="${data}" data-product-id="${row.productId}" data-remaining="${row.remainingQuantity}" data-type="3" data-kt-purchases-table-filter="purchase_row" data-bs-toggle="tooltip" data-bs-custom-class="tooltip-dark" data-bs-placement="left" title="ihale usulü satın alma"><i class="fas fa-shopping-basket"></i></a>`;
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
            columnDefs: [{ orderable: !1, targets: 7 }],
            "width": "100%"
        });
    };

    return {
        init: function () {
            (n = document.querySelector("#kt_purchase_requested_table"), priorityEnumList().done(function (data) { p = data.list })) && (

                (dt = loadDataTable()),
                   // .on("draw", function () { setDeleteEvents(), setPurchaseEvents() }),

                document.querySelector('[data-kt-purchases-table-filter="search"]').addEventListener("keyup", (function (e) {
                    dt.search(e.target.value).draw()
                }))
            )
        }
    }
}();

KTUtil.onDOMContentLoaded((function () { KTPurchaseList.init() }));