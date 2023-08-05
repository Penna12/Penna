"use strict";
var KTVendorList = function () {
    var dt, n, el;

    var businessGroupEnumList = () => {
        return $.ajax({
            url: "/Vendor/GetBusinessGroupEnumList/",
            type: "GET"
        });
    };

    var del = (id) => {
        return $.ajax({
            url: "/Vendor/Delete/",
            type: "DELETE",
            data: { id: id }
        });
    };
    
    var setDeleteEvents = () => {
        n.querySelectorAll('[data-kt-vendors-table-filter="delete_row"]').forEach((e => {
            e.addEventListener("click", (function (e) {
                e.preventDefault();
                var id = this.dataset["link"];
                const o = e.target.closest("tr"), n = o.querySelectorAll("td")[1].innerText;
                Swal.fire({ text: n + "'ı silmek istediğine eminmisin ?", icon: "warning", showCancelButton: !0, buttonsStyling: !1, confirmButtonText: "Evet, sil!", cancelButtonText: "Hayır, vazgeç", customClass: { confirmButton: "btn fw-bold btn-danger", cancelButton: "btn fw-bold btn-active-light-primary" } })
                    .then((function (e) {
                        e.value
                            ? del(id).done((function (res) {
                                if (res.success) {
                                    Swal.fire({ text: n + " silindi !.", icon: "success", buttonsStyling: !1, confirmButtonText: "Tamam!", customClass: { confirmButton: "btn fw-bold btn-primary" } })
                                        .then((function () {
                                            dt.ajax.reload();
                                            //dt.row($(o)).remove().draw()
                                        }))
                                } else {
                                    Swal.fire({ text: n + " silinmedi.", icon: "error", buttonsStyling: !1, confirmButtonText: "Tamam!", customClass: { confirmButton: "btn fw-bold btn-primary" } })
                                }
                            })).fail(function (err) { console.log(`${err.status} - ${err.statusText}`) }) 
                            : "cancel" === e.dismiss && Swal.fire({ text: n + " silinmedi.", icon: "error", buttonsStyling: !1, confirmButtonText: "Tamam!", customClass: { confirmButton: "btn fw-bold btn-primary" } })
                    }))
            }))
        }))
    };

    var getUrl = (url) => {
        return $.ajax({
            url: url,
            type: "GET"
        });
    }

    var sendNewPasswordEvents = () => {
        n.querySelectorAll('[data-kt-vendors-table-filter="send_password"]').forEach((e => {
            e.addEventListener("click", (function (e) {
                e.preventDefault();
                var id = this.dataset["link"];
                const o = e.target.closest("tr"), n = o.querySelectorAll("td")[1].innerText;
                Swal.fire({ text: n + "'a şifre gönderilecek ?", icon: "warning", showCancelButton: !0, buttonsStyling: !1, confirmButtonText: "Gönder!", cancelButtonText: "Vazgeç", customClass: { confirmButton: "btn fw-bold btn-danger", cancelButton: "btn fw-bold btn-active-light-primary" } })
                    .then((function (e) {0
                        e.value
                            ? getUrl(`/Vendor/SendPassword/${id}`).done((function (res) {
                                if (res.success) {
                                    Swal.fire({ text: res.message, icon: "success", buttonsStyling: !1, confirmButtonText: "Tamam!", customClass: { confirmButton: "btn fw-bold btn-primary" } })
                                        .then((function () {
                                            dt.ajax.reload();
                                        }))
                                } else {
                                    Swal.fire({ text: res.message, icon: "error", buttonsStyling: !1, confirmButtonText: "Tamam!", customClass: { confirmButton: "btn fw-bold btn-primary" } })
                                }
                            })).fail(function (err) { console.log(`${err.status} - ${err.statusText}`) })
                            : "cancel" === e.dismiss && Swal.fire({ text: n + " mesaj gönderilemedi.", icon: "error", buttonsStyling: !1, confirmButtonText: "Tamam!", customClass: { confirmButton: "btn fw-bold btn-primary" } })
                    }))
            }))
        }))
    };

    var fillVariables = () => {
        return new Promise((resolve) => {
            n = document.querySelector("#kt_vendors_table");
            resolve();
        })
    }

    function setBusinessGroup() {
        return new Promise((resolve) => {
            businessGroupEnumList()
                .done(function (data) {
                    el = data,
                        dt = loadDataTable().on("draw", function () { setDeleteEvents(), sendNewPasswordEvents() });
                })
            resolve();
        });
    }

    async function fn_InitializeAsync() {

        await fillVariables();

        document.querySelector('[data-kt-vendors-table-filter="search"]').addEventListener("keyup", (function (e) {
            dt.search(e.target.value).draw()
        }));

        await setBusinessGroup();
    }

    const f = () => {
        const t = document.querySelector('[data-kt-vendors-table-toolbar="base"]')
    };

    const loadDataTable = () => {
        return $(n).DataTable({
            "ajax": {
                "url": "/Vendor/GetAllData",
                "type": "GET",
                "datatype": "json"
            },
            "columns": [
                { "data": "companyName", "width": "13%" },
                { "data": "authorizedPerson", "width": "12%" },
                {
                    "data": "businessGroupId",
                    "render": function (data) {
                        return `<span class="badge badge-primary">${el[data-1].text}</span>`;
                    },
                    "width": "10%"
                },
                { "data": "phone1", "width": "7%" },
                { "data": "email", "width": "15%" },
                {
                    "data": "companyStatusId",
                    "render": function (data, type, row) {
                        if (data == 1) return `<span class="badge badge-success">Beyaz Liste</span>`;
                        if (data == 2) return `<span class="badge badge-danger">Kara Liste</span>`;
                    }, "width": "8%"
                },
                {
                    "data": "id",
                    orderable: false,
                    className: 'text-end ',
                    "render": function (data) { 
                        return `<a href="#" class="btn btn-icon btn-sm btn-primary" data-link="${data}" title="şifre gönder" data-kt-vendors-table-filter="send_password"><i class="fas fa-key"></i></a>
                                <a href="/Vendor/Upsert/${data}" class="btn btn-icon btn-sm btn-warning" title="düzenle"><i class="far fa-edit"></i></a>
                                <a href="#" class="btn btn-icon btn-sm btn-danger" data-link="${data}" title="sil" data-kt-vendors-table-filter="delete_row" data-bs-toggle="tooltip" data-bs-custom-class="tooltip-dark" data-bs-placement="top"><i class="far fa-trash-alt"></i></a>`;
                    }, "width": "10%"
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
            order: [[1, "asc"]],
            columnDefs: [{ orderable: !1, targets: 6 }],
            "width": "100%"
        });
    };

    return {
        init: function () {
            fn_InitializeAsync();
        }
    }
}();

KTUtil.onDOMContentLoaded((function () { KTVendorList.init() }));