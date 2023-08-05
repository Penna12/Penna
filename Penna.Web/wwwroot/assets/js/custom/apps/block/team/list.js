"use strict";
var KTBlockList = function () {
    var dt, n, u, p, f;

    var getPhoneData = (uid) => {
        return $.ajax({
            url: "/Block/GetUserData/",
            type: "GET",
            data: { userId: uid }
        });
    };

    var setPhoneData = (userId) => {
        getPhoneData(userId)
            .done(function (response) {
                var d = response.data;
                p.value = d.phoneNumber;
            })
            .fail(function (err) {
                Swal.fire({
                    text: `${err.status} - ${err.statusText}`,
                    icon: "error",
                    buttonsStyling: !1,
                    confirmButtonText: "Tamam!",
                    customClass: { confirmButton: "btn fw-bold btn-primary" }
                })
            });

    };

    var del = (id) => {
        return $.ajax({
            url: "/Block/DeleteTeam/",
            type: "POST",
            data: { id: id }
        });
    };
    
    var setDeleteEvents = () => {
        n.querySelectorAll('[data-kt-block-teams-table-filter="delete_row"]').forEach((e => {
            e.addEventListener("click", (function (e) {
                e.preventDefault();
                var id = this.dataset["link"];
                const o = e.target.closest("tr"), n = o.querySelectorAll("td")[0].innerText;
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


    const loadDataTable = () => {
        return $(n).DataTable({
            "ajax": {
                "url": "/Block/GetAllBlockTeamsData",
                "type": "GET",
                "datatype": "json"
            },
            "columns": [
                { "data": "user.fullName", "width": "30%" },
                { "data": "phone", "width": "20%" },
                { "data": "userPosition.name", "width": "20%" },
                {
                    "data": "id",
                    orderable: false,
                    className: 'text-end ',
                    "render": function (data) { 
                        return `<a href="#" class="btn btn-icon btn-sm btn-danger" data-link="${data}" title="sil" data-kt-block-teams-table-filter="delete_row" data-bs-toggle="tooltip" data-bs-custom-class="tooltip-dark" data-bs-placement="top"><i class="far fa-trash-alt"></i></a>`;
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
            order: [[0, "asc"]],
            columnDefs: [{ orderable: !1, targets: 3 }],
            "width": "100%"
        });
    };

    return {
        init: function () {
            (n = document.querySelector("#kt_block_teams_table"),
                f = document.querySelector("#kt_add_block_teams_form"),
                p = document.querySelector('[name="Phone"]')) && (

                dt = loadDataTable().on("draw", function () { setDeleteEvents() }),

                $(f.querySelector('[name="UserId"]')).on("change", (function () {
                    var s = this;
                    setPhoneData(s.options[s.selectedIndex].value);
                }))
            )
        }
    }
}();

KTUtil.onDOMContentLoaded((function () { KTBlockList.init() }));