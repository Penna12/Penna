"use strict";
var KTProductUpsert = function () {
    var f, q1, q2, t1, t2, m, l, c, v, dt;

    var setDeleteEvents = () => {
        t2.querySelectorAll('[data-kt-embezzled-table-filter="delete_row"]').forEach((e => {
            e.addEventListener("click", async function (evt) {
                evt.preventDefault();
                var fixtureId = this.dataset["embezzledLink"];
                console.log("fixtureId:", fixtureId);
                await fetch(`/Fixture/DeleteEmbezzled/${fixtureId}`)
                    .then(response => response.json())
                    .then(data => { console.log(data.success), dt.ajax.reload(); })
                    .catch(err => console.log(`Zimmet iadesi yapılırken hata: ${err}`));
            })
        }))
    };

    var setReturnedEvents = () => {
        t2.querySelectorAll('[data-kt-embezzled-table-filter="returned"]').forEach((e => {
            e.addEventListener("click", async function (evt) {
                evt.preventDefault();
                var fixtureId = this.dataset["embezzledLink"];
                console.log("fixtureId:", fixtureId);
                await fetch(`/Fixture/SetReturnEmbezzled/${fixtureId}`)
                    .then(response => response.json())
                    .then(data => { console.log(data.success), dt.ajax.reload(); })
                    .catch(err => console.log(`Zimmet iadesi yapılırken hata: ${err}`));
            })
        }))
    };

    const loadDataTable = (url) => {
        return $(t2).DataTable({
            ajax: {
                url: url,
                type: "GET",
                datatype: "json"
            },
            "columns": [
                {
                    "data": "embezzledDate",
                    "render": function (data, type, row) {
                        return moment(data).format("DD-MM-YYYY");
                    },"width": "15%"
                },
                { "data": "appUser.fullName", "width": "15%" },
                { "data": "quantity", "width": "10%" },
                { "data": "description", "width": "30%" },
                //{
                //    "data": "returnDate",
                //    "render": function (data, type, row) {
                //        return data == null ? "" : moment(data).format("DD-MM-YYYY");
                //    },
                //    "width": "15%"
                //},
                {
                    "data": "id",
                    orderable: false,
                    className: 'text-end ',
                    "render": function (data) {
                        return `<a href="#" class="btn btn-icon btn-sm btn-primary" data-embezzled-link="${data}" title="iade al" data-kt-embezzled-table-filter="returned" data-bs-toggle="tooltip" data-bs-custom-class="tooltip-dark" data-bs-placement="top"><i class="fas fa-tasks"></i></a>
                                <a href="#" class="btn btn-icon btn-sm btn-danger" data-embezzled-link="${data}" title="sil" data-kt-embezzled-table-filter="delete_row" data-bs-toggle="tooltip" data-bs-custom-class="tooltip-dark" data-bs-placement="top"><i class="far fa-trash-alt"></i></a>`;
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
            columnDefs: [{ orderable: !1, targets: 4 }],
            "width": "100%"
        });
    };

    return {
        init: function () {
            (m = document.querySelector("#kt_modal_list_embezzled"),
                t1 = document.querySelector("#fixtureTable"),
                t2 = document.querySelector("#kt_modal_list_embezzled_table"),
                l = t1.querySelectorAll('[data-kt-embezzled-table-filter="embezzled_list"]'),
                c = m.querySelector("#kt_modal_list_embezzled_close"),
                v = m.querySelector("#kt_modal_list_embezzled_cancel"),
                f = document.querySelector("#embezzledForm"),
                q1 = f.querySelector('[name="FixtureEmbezzled.Quantity"]'),
                q2 = f.querySelector('[name="Fixture.Quantity"]')) && (

                $(q1).on("change", function (e) {
                    if (parseInt(q2.value) < parseInt(q1.value)) {
                        q1.value = q2.value
                    } else
                        if (parseInt(q1.value) < 0) {
                            q1.value = 0
                        }
                }),

                // Demirbaş zimmetliler listesi - trigger
                l.forEach(e => {
                    e.addEventListener("click", async function (evt) {
                        evt.preventDefault();

                        var fixtureId = this.dataset["fixtureLink"];
                        console.log("fixtureId:", fixtureId);
                        if (!(dt == undefined || dt == null)) {
                            dt.destroy();
                        }
                        dt = loadDataTable(`/Fixture/GetEmbezzledList/${fixtureId}`).on("draw", function () { setDeleteEvents(), setReturnedEvents() });
                        $(m).modal("show");
                    })
                }),

                // Modal cancel triggeri
                c.addEventListener("click", function () { $(m).modal("hide"); window.location.reload(); }),
                // Modal close triggeri
                v.addEventListener("click", function () { $(m).modal("hide"); window.location.reload(); })

            )
        }
    }
}();

KTUtil.onDOMContentLoaded((function () { KTProductUpsert.init() }));
