"use strict";
var KTBlockList = function () {
    var dt, n, t, p, f, el;

    var getPhoneData = (uid) => {
        return $.ajax({
            url: "/Block/GetCurrentAccountData/",
            type: "GET",
            data: { accountId: uid }
        });
    };

    var setPhoneData = (accountId) => {
        if (accountId == "")
            return;
        getPhoneData(accountId)
            .done(function (response) {
                if (response.success) {
                    var d = response.data;
                    p.value = d.phone1;
                    t.value = d.authorizedPerson;
                } else {
                    Swal.fire({
                        text: "Tanımlanmış bir taşeron bulunamadı.",
                        icon: "warning",
                        buttonsStyling: !1,
                        confirmButtonText: "Tamam!",
                        customClass: { confirmButton: "btn fw-bold btn-primary" }
                    })
                }
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
            url: "/Block/DeleteSubcontractor/",
            type: "POST",
            data: { id: id }
        });
    };
    
    var setDeleteEvents = () => {
        n.querySelectorAll('[data-kt-block-subcontractors-table-filter="delete_row"]').forEach((e => {
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
                "url": "/Block/GetAllBlockSubcontractorsData",
                "type": "GET",
                "datatype": "json"
            },
            "columns": [
                { "data": "currentAccount.companyName", "width": "25%" },
                { "data": "companyRepresentative", "width": "25%" },
                { "data": "phone", "width": "15%" },
                {
                    "data": "blockSubcontractorBusinessGroups",
                    "render": function (data) {
                        var st = ""; 
                        if (el) {
                            data.forEach(function (e, i) { st += el[e.businessGroupId-1].text + ", "; });
                        }
                        return `<span class="badge  badge-primary">${st.substring(0, st.length-2)}</span>`;
                    }, "width": "15%"
                },
                {
                    "data": "id",
                    orderable: false,
                    className: 'text-end ',
                    "render": function (data) { 
                        return `<a href="#" class="btn btn-icon btn-sm btn-danger" data-link="${data}" title="sil" data-kt-block-subcontractors-table-filter="delete_row" data-bs-toggle="tooltip" data-bs-custom-class="tooltip-dark" data-bs-placement="top"><i class="far fa-trash-alt"></i></a>`;
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
            columnDefs: [{ orderable: !1, targets: 4 }],
            "width": "100%"
        });
    };


    function getBusinessGroupEnumList() {
        return new Promise((resolve, reject) => {
            $.getJSON({
                url: '/Block/GetBusinessGroupEnumList/',
                success: resolve,
                error: reject
            })
        })
    }

    async function setEnumList() {
        el = await getBusinessGroupEnumList()
    }


    return {
        init: function () {
            (n = document.querySelector("#kt_block_subcontractors_table"),
                f = document.querySelector("#kt_add_subcontractor_form"),
                p = document.querySelector('[name="Phone"]'),
                t = document.querySelector('[name="CompanyRepresentative"]')
            ) && (

                $(f.querySelector('[name="CurrentAccountId"]')).on("change", (function () {
                    var s = this;
                    setPhoneData(s.options[s.selectedIndex].value);
                })),

                setEnumList(),

                dt = loadDataTable().on("draw", function () { setDeleteEvents() })
                
            )
        }
    }
}();

KTUtil.onDOMContentLoaded((function () { KTBlockList.init() }));