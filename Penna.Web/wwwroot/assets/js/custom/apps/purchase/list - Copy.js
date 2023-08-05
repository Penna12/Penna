"use strict";
var KTPurchaseList = function () {
    var dt, n, p;

    //var priorityEnumListAsync = async () => {
    //    await fetch("/Purchase/GetPriorityEnumList/")
    //        .then(response => response.json() )
    //        .then(d => { p = d.list, console.log("> list:", p) })
    //        .catch(err => console.log(`GetPriorityEnumList biligisi alırkken hata: ${err}`));
    //};

    var priorityEnumList = () => {
        return $.ajax({
            url: "/Purchase/GetPriorityEnumList/",
            type: "GET",
        });
    };

    var del = (id) => {
        return $.ajax({
            url: "/Purchase/Delete/",
            type: "DELETE",
            data: { id: id }
        });
    };

    var setDeleteEvents = () => {
        n.querySelectorAll('[data-kt-purchases-table-filter="delete_row"]').forEach((e => {
            e.addEventListener("click", (function (e) {
                e.preventDefault();
                var id = this.dataset["link"];
                const o = e.target.closest("tr"), n = o.querySelectorAll("td")[0].innerText + ', ' + o.querySelectorAll("td")[1].innerText + ', ' + o.querySelectorAll("td")[6].innerText;
                Swal.fire({ text: n + "'ı silmek istediğine eminmisin ?", icon: "warning", showCancelButton: !0, buttonsStyling: !1, confirmButtonText: "Evet, sil!", cancelButtonText: "Hayır, vazgeç", customClass: { confirmButton: "btn fw-bold btn-danger", cancelButton: "btn fw-bold btn-active-light-primary" } })
                    .then((function (e) {
                        e.value
                            ? del(id).then((function (res) {
                                if (res.success) {
                                    Swal.fire({ text: n + " silindi !.", icon: "success", buttonsStyling: !1, confirmButtonText: "Tamam!", customClass: { confirmButton: "btn fw-bold btn-primary" } })
                                        .then((function () { dt.ajax.reload(); /*dt.row($(o)).remove().draw()*/ }))
                                } else {
                                    Swal.fire({ text: n + " silinmedi.", icon: "error", buttonsStyling: !1, confirmButtonText: "Tamam!", customClass: { confirmButton: "btn fw-bold btn-primary" } })
                                }
                            }))
                            : "cancel" === e.dismiss && Swal.fire({ text: n + " silinmedi.", icon: "error", buttonsStyling: !1, confirmButtonText: "Tamam!", customClass: { confirmButton: "btn fw-bold btn-primary" } })
                    }))
            }))
        }))
    };

    var set_flatpickr_format = () => {
        $("#Purchase_PurchaseDate").flatpickr({
            onReady: function () {
                this.jumpToDate(moment().format("DD-MM-YYYY"))
            },
            locale: Turkish,
            dateFormat: "d-m-Y",
            defaultDate: moment().format("DD-MM-YYYY")
        });
        $("#Purchase_RequestedDeliveryDate").flatpickr({
            onReady: function () {
                this.jumpToDate(moment().format("DD-MM-YYYY"))
            },
            locale: Turkish,
            dateFormat: "d-m-Y",
            defaultDate: moment().format("DD-MM-YYYY")
        });
        $("#Purchase_InvoiceDate").flatpickr({
            onReady: function () {
                this.jumpToDate(moment().format("DD-MM-YYYY"))
            },
            locale: Turkish,
            dateFormat: "d-m-Y",
            defaultDate: moment().format("DD-MM-YYYY")
        });
        $("#Purchase_Deadline").flatpickr({
            onReady: function () {
                this.jumpToDate(moment().format("DD-MM-YYYY"))
            },
            locale: Turkish,
            dateFormat: "d-m-Y",
            defaultDate: moment().format("DD-MM-YYYY")
        });
    }

    var setProductSelected = (productId) => {
        $("#Purchase_ProductId").find(`option[value=${productId}]`).prop("selected", true);
    }

    var setPurchaseEvents = () => {
        n.querySelectorAll('[data-kt-purchases-table-filter="purchase_row"]').forEach((e => {
            e.addEventListener("click", (function (e) {
                e.preventDefault();
                var id = this.dataset["link"];
                var productId = this.dataset["productId"];
                var remaining = this.dataset["remaining"];
                var type = this.dataset["type"];

                console.log(e.currentTarget, this);

                if (type == 1) {
                    console.log("doğrudan satış seçildi.");
                    document.querySelector("#offer_tender_div").style.display = "none";
                    document.querySelector("#direct_purchase_div").style.display = "block";
                    document.querySelector("#kt_modal_add_direct_purchase_form h2").innerText = "Doğrudan Satın Alma Yap";
                } else
                    if (type == 2) {
                        console.log("teklif usulü satış seçildi.");
                        document.querySelector("#offer_tender_div").style.display = "block";
                        document.querySelector("#direct_purchase_div").style.display = "none";
                        document.querySelector("#kt_modal_add_direct_purchase_form h2").innerText = "Teklif Usulü Satın Alma Yap";
                        document.querySelector("#offer_tender_span").innerText = "Teklif";
                    } else {
                        console.log("ihale usulü satış seçildi.");
                        document.querySelector("#offer_tender_div").style.display = "block";
                        document.querySelector("#direct_purchase_div").style.display = "none";
                        document.querySelector("#kt_modal_add_direct_purchase_form h2").innerText = "İhale Usulü Satın Alma Yap";
                        document.querySelector("#offer_tender_span").innerText = "İhale";
                    }

                document.querySelector('#kt_modal_add_direct_purchase_form [name="Purchase.PurchaseRequestId"]').value = id;
                setProductSelected(productId);
                document.getElementById("Purchase_Quantity").value = remaining;
                document.getElementById("RemainingQuantity").value = remaining;
                set_flatpickr_format();
                $("#kt_modal_add_direct_purchase").modal("show");
            }))
        }))
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

                //priorityEnumListAsync(), console.log("priority list geçti."),

                (dt = loadDataTable()),
                   // .on("draw", function () { setDeleteEvents(), setPurchaseEvents() }),

                document.querySelector('[data-kt-purchases-table-filter="search"]').addEventListener("keyup", (function (e) {
                    dt.search(e.target.value).draw()
                })),

                document.querySelector('#Purchase_Quantity').addEventListener("change", (function (e) {
                    var t = this;
                    var r = document.getElementById("RemainingQuantity").value;
                    var q = t.value;
                    if (parseInt(q) > parseInt(r)) {
                        t.value = r;
                        Swal.fire({ text: "Talep miktarından fazla tutar giremezsiniz.", icon: "warning", buttonsStyling: !1, confirmButtonText: "Tamam!", customClass: { confirmButton: "btn fw-bold btn-primary" } })
                            .then((function () { t.focus() }))
                    }
                }))
            )
        }
    }
}();

KTUtil.onDOMContentLoaded((function () { KTPurchaseList.init() }));