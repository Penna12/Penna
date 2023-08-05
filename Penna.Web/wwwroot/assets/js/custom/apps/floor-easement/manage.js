"use strict";
var KTFloorEasementList = function () {
    var dt, t, u;


    var del = (id) => {
        return $.ajax({
            url: "/FloorEasement/Delete/",
            type: "POST",
            data: { id: id }
        });
    };

    var setDeleteEvents = () => {
        t.querySelectorAll('[data-kt-floors-table-filter="delete_row"]').forEach((e => {
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
        t.querySelectorAll('[data-kt-floors-table-filter="edit_row"]').forEach((e => {
            e.addEventListener("click", (function (e) {
                e.preventDefault();
                var id = this.dataset["link"];
                getApartmentById(id)
                    .done((data) => {
                        setEditFields(data);
                        $("#kt_modal_edit_floors").modal("show");
                    })
                    .fail((err) => {
                        Swal.fire({ title: "işlem başarısız oldu.", text: ` ${err.status} - ${err.statusText}`, icon: "error", buttonsStyling: !1, confirmButtonText: "Tamam, anladım!", customClass: { confirmButton: "btn fw-bold btn-primary" } })
                    });
            }))
        }))
    };


    var setEditFields = (data) => {
        document.querySelector("#kt_modal_edit_floors_form [name='Id']").value = data.data.id;
        document.querySelector("#kt_modal_edit_floors_form [name='Floor']").value = data.data.floor;
        document.querySelector("#kt_modal_edit_floors_form [name='SectionName']").value = data.data.sectionName;
        document.querySelector("#kt_modal_edit_floors_form [name='Gross']").value = data.data.gross;
        document.querySelector("#kt_modal_edit_floors_form [name='Net']").value = data.data.net;
        document.querySelector("#kt_modal_edit_floors_form [name='Gabari']").value = data.data.gabari;
        document.querySelector("#kt_modal_edit_floors_form [name='CurrentAccountId']").value = data.data.currentAccountId ?? "";
        document.querySelector("#kt_modal_edit_floors_form [name='BlockId']").value = data.data.blockId;
        document.querySelector("#kt_modal_edit_floors_form [name='CreatedBy']").value = data.data.createdBy;
        document.querySelector("#kt_modal_edit_floors_form [name='CreatedDate']").value = data.data.createdDate;
    };

    var getApartmentById = (id) => {
        return $.ajax({
            url: "/FloorEasement/GetById/" + id,
            type: "GET"
        });
    };

    const f = () => {
        const t = document.querySelector('[data-kt-floors-table-toolbar="base"]')
    };


    const getUrl = () => {
        if (window.location.search.includes('floor')) {
            var fl = window.location.search.split("floor=")[1];
            document.querySelector('#floor-menu [data-kt-floor-menu]').classList.remove("active")
            document.querySelector(`#floor-menu [data-kt-floor-menu="${fl}"]`).classList.add("active")
            return fl;
        }
        return "null";
    }

    const loadDataTable = () => {
        return $(t).DataTable({
            "ajax": {
                "url": "/FloorEasement/GetAllData",
                "type": "GET",
                "datatype": "json",
                "data": { floor : u }
            },
            "columns": [
                { "data": "floor", "width": "8%" },
                { "data": "sectionName", "width": "10%" },
                { "data": "gross", "width": "10%" },
                { "data": "net", "width": "10%" },
                { "data": "gabari", "width": "10%" },
                {
                    "data": "currentAccount.authorizedPerson",
                    "render": function (data) {
                        if (data == null)
                            return " "
                        else return `${data}`;
                    },
                    "width": "10%"
                },
                {
                    "data": "currentAccount.taxId",
                    "render": function (data) {
                        if (data == null)
                            return " "
                        else return `${data}`;
                    },
                    "width": "10%"
                },
                {
                    "data": "currentAccount.phone1",
                    "render": function (data) {
                        if (data == null)
                            return " "
                        else return `${data}`;
                    },
                    "width": "10%"
                },
                {
                    "data": "currentAccount.email",
                    "render": function (data) {
                        if (data == null)
                            return " "
                        else return `${data}`;
                    },
                    "width": "10%"
                },
                {
                    "data": "id",
                    orderable: false,
                    className: 'text-end ',
                    "render": function (data) {
                        return `<a href="#" class="btn btn-icon btn-sm btn-warning" data-link="${data}" title="düzenle" data-kt-floors-table-filter="edit_row"><i class="far fa-edit"></i></a>
                                <a href="#" class="btn btn-icon btn-sm btn-danger" data-link="${data}" title="sil" data-kt-floors-table-filter="delete_row" data-bs-toggle="tooltip" data-bs-custom-class="tooltip-dark" data-bs-placement="top"><i class="far fa-trash-alt"></i></a>`;
                    },
                    "width":"12%"
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
            "width": "100%"
        });
    };

    return {
        init: function () {
            (t = document.querySelector("#kt_floor_table"), u = getUrl()) && (
                
                dt = loadDataTable().on("draw", function () { setDeleteEvents(), setEditEvents() }),

                document.querySelector('[data-kt-floors-table-filter="search"]').addEventListener("keyup", (function (e) {
                    dt.search(e.target.value).draw()
                }))

            )
        }
    }
}();

KTUtil.onDOMContentLoaded((function () { KTFloorEasementList.init() }));