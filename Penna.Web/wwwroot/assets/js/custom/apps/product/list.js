"use strict";
var KTProductList = function () {
    var t, e, dt, n, el;

    var businessGroupEnumList = () => {
        return $.ajax({
            url: "/Product/GetBusinessGroupEnumList/",
            type: "GET",
        });
    };

    var del = (id) => {
        return $.ajax({
            url: "/Product/Delete/",
            type: "DELETE",
            data: { id: id }
        });
    };
    
    var setDeleteEvents = () => {
        n.querySelectorAll('[data-kt-products-table-filter="delete_row"]').forEach((e => {
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

    var getInOutListEvents = () => {
        n.querySelectorAll('[data-kt-products-table-filter="list_row"]').forEach((e => {
            e.addEventListener("click", (async function (e) {
                e.preventDefault();
                var id = this.dataset["link"];
                const o = e.target.closest("tr"), n = o.querySelectorAll("td")[0].innerText + ' ' + o.querySelectorAll("td")[1].innerText;

                await fetch(`/Product/GetInOutList/${id}`)
                    .then(response => response.json())
                    .then(data => {
                        // tabloya import yap
                        // modal("show")
                    })
                    .catch(err => console.log(`Zimmet iadesi yapılırken hata: ${err}`));

                Swal.fire({ text: n, icon: "info", buttonsStyling: !1, confirmButtonText: "Tamam!", customClass: { confirmButton: "btn fw-bold btn-primary" } })   

            }))
        }))
    };
    
    const f = () => {
        const t = document.querySelector('[data-kt-products-table-toolbar="base"]')
    };

    const loadDataTable = () => {
        return $(n).DataTable({
            "ajax": {
                "url": "/Product/GetAllData",
                "type": "GET",
                "datatype": "json"
            },
            "columns": [
                { "data": "name", "width": "12%" },
                { "data": "brand", "width": "12%" },
                { "data": "unit.name", "width": "10%" },
                { "data": "quantity", "width": "7%" },
                { "data": "dimensions", "width": "11%" },
                { "data": "thickness", "width": "10%" },
                { "data": "species", "width": "12%" },
                {
                    "data": "businessGroupId",
                    "render": function (data) {
                        return `<span class="badge badge-primary">${el[data-1].text}</span>`;
                    },
                    "width": "7%"
                },
                {
                    "data": "status",
                    "render": function (data, type, row) {
                        if (data == 1) return `<span class="badge badge-success">Aktif</span>`;
                        if (data == 0) return `<span class="badge badge-danger">Pasif</span>`;
                    }, "width": "7%"
                },
                {
                    "data": "id",
                    orderable: false,
                    className: 'text-end ',
                    "render": function (data) { 
                        return `<a href="#" class="btn btn-icon btn-sm btn-primary" data-link="${data}" title="giriş-çıkış kayıtları" data-kt-products-table-filter="list_row"><i class="fas fa-list"></i></a>
                                <a href="/Product/Upsert/${data}" class="btn btn-icon btn-sm btn-warning" title="düzenle"><i class="far fa-edit"></i></a>
                                <a href="#" class="btn btn-icon btn-sm btn-danger" data-link="${data}" title="sil" data-kt-products-table-filter="delete_row"><i class="far fa-trash-alt"></i></a>`;
                    }, "width": "12%"
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
            columnDefs: [{ orderable: !1, targets: 9 }],
            "width": "100%"
        });
    };

    return {
        init: function () {
            (n = document.querySelector("#kt_products_table"), businessGroupEnumList().done(function (data) { el = data })) && (

                dt = loadDataTable().on("draw", function () { setDeleteEvents(), getInOutListEvents() }),

                document.querySelector('[data-kt-products-table-filter="search"]').addEventListener("keyup", (function (e) {
                    dt.search(e.target.value).draw()
                }))
            )
        }
    }
}();

KTUtil.onDOMContentLoaded((function () { KTProductList.init() }));