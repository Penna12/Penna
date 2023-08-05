"use strict";
var KTCountryList = function () {
    var t, e, o, dt, n;

    var del = (id) => {
        return $.ajax({
            url: "/Countries/Delete/",
            type: "POST",
            data: { id: id }
        });
    };
    
    var setDeleteEvents = () => {
        n.querySelectorAll('[data-kt-countries-table-filter="delete_row"]').forEach((e => {
            e.addEventListener("click", (function (e) {
                e.preventDefault();
                var id = this.dataset["link"];
                const o = e.target.closest("tr"), n = o.querySelectorAll("td")[1].innerText;
                Swal.fire({ text: n + "'ı silmek istediğine eminmisin ?", icon: "warning", showCancelButton: !0, buttonsStyling: !1, confirmButtonText: "Evet, sil!", cancelButtonText: "Hayır, vazgeç", customClass: { confirmButton: "btn fw-bold btn-danger", cancelButton: "btn fw-bold btn-active-light-primary" } })
                    .then((function (e) {
                        e.value
                            ? del(id).then((function (res) {
                                if (res.success) {
                                    Swal.fire({ text: n + " silindi !.", icon: "success", buttonsStyling: !1, confirmButtonText: "Tamam!", customClass: { confirmButton: "btn fw-bold btn-primary" } })
                                        .then((function () { dt.row($(o)).remove().draw() }))
                                } else {
                                    Swal.fire({ text: n + " silinmedi.", icon: "error", buttonsStyling: !1, confirmButtonText: "Tamam!", customClass: { confirmButton: "btn fw-bold btn-primary" } })
                                }
                            })) 
                            : "cancel" === e.dismiss && Swal.fire({ text: n + " silinmedi.", icon: "error", buttonsStyling: !1, confirmButtonText: "Tamam!", customClass: { confirmButton: "btn fw-bold btn-primary" } })
                    }))
            }))
        }))
    };

    var setEditEvents = () => {
        n.querySelectorAll('[data-kt-countries-table-filter="edit_row"]').forEach((e => {
            e.addEventListener("click", (function (e) {
                e.preventDefault();
                var id = this.dataset["link"];
                getCountryById(id)
                    .done((data) => {
                        setEditFields(data);
                        $("#kt_modal_edit_countries").modal("show");
                    })
                    .fail((err) => {
                        Swal.fire({ title:"işlem başarısız oldu.", text:` ${err.status} - ${err.statusText}`, icon: "error", buttonsStyling: !1, confirmButtonText: "Tamam, anladım!", customClass: { confirmButton: "btn fw-bold btn-primary" } })
                    });
            }))
        }))
    };



    var setEditFields = (data) => {
        document.querySelector("#kt_modal_edit_countries_form [name='Id']").value = data.data.id;
        document.querySelector("#kt_modal_edit_countries_form [name='Name']").value = data.data.name;
        document.querySelector("#kt_modal_edit_countries_form [name='Code']").value = data.data.code;
        document.querySelector("#kt_modal_edit_countries_form [name='DialCode']").value = data.data.dialCode;
        document.querySelector("#kt_modal_edit_countries_form [name='IsActive']").value = data.data.isActive;
        document.querySelector("#kt_modal_edit_countries_form [name='IsActive']").checked = true;
    };

    var getCountryById = (id) => {
        return $.ajax({
            url: "/Countries/GetById/" + id,
            type: "GET"
        });
    };

    const f = () => {
        const t = document.querySelector('[data-kt-countries-table-toolbar="base"]')
    };

    const loadDataTable = () => {
        return $(n).DataTable({
            createdRow: function (row, data, dataIndex) {
                $(row).find('td:eq(4)').attr('data-filter', data.isActive ? 'active' : 'passive');
            },
            "ajax": {
                "url": "/Countries/GetAllData",
                "type": "GET",
                "datatype": "json"
            },
            "columns": [
                { "data": "id", "width": "5%" },
                { "data": "name", "width": "25%" },
                { "data": "code", "width": "10%" },
                { "data": "dialCode", "width": "10%" },
                {
                    "data": "isActive",
                    "render": function (data, type, row) {
                        if (data == true) return `<span class="badge badge-success">Aktif</span>`;
                        if (data == false) return `<span class="badge badge-danger">Pasif</span>`;
                    }, "width": "10%"
                },
                {
                    "data": "id",
                    orderable: false,
                    className: 'text-end ',
                    "render": function (data) { 
                        return `<a href="#" class="btn btn-icon btn-sm btn-warning" data-link="${data}" title="düzenle" data-kt-countries-table-filter="edit_row"><i class="far fa-edit"></i></a>
                                <a href="#" class="btn btn-icon btn-sm btn-danger" data-link="${data}" title="sil" data-kt-countries-table-filter="delete_row" data-bs-toggle="tooltip" data-bs-custom-class="tooltip-dark" data-bs-placement="top"><i class="far fa-trash-alt"></i></a>`;
                    }, "width": "20%"
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
            columnDefs: [{ orderable: !1, targets: 5 }],
            "width": "100%"
        });
    };

    return {
        init: function () {
            (n = document.querySelector("#kt_countries_table")) && (

                dt = loadDataTable().on("draw", function () { setDeleteEvents(), setEditEvents() }),

                document.querySelector('[data-kt-countries-table-filter="search"]').addEventListener("keyup", (function (e) {
                    dt.search(e.target.value).draw()
                }))
            )
        }
    }
}();

KTUtil.onDOMContentLoaded((function () { KTCountryList.init() }));