"use strict";
var KTProjectList = function () {
    var n, bu, m, t, dt, s, c, o, f, i, v;

    const saveInstallments = () => {
        return $.ajax({
            url: "/ProjectBalance/SaveInstallments/",
            type: "POST",
            data: $(f).serialize()
        });
    }

    const taksitHesaplaGetir = () => {
        $.ajax({
            url: "/ProjectBalance/TaksitHesaplaGetir",
            type: "GET",
            datatype: "json",
            success: function (response) {
                if (response.success) {
                    console.log("Liste yüklendi.")
                    taksitleriYerlestir(response.data)
                    $('[name="TaksitTarihi"]').flatpickr({
                        locale: Turkish,
                        dateFormat: "d-m-Y"
                    })
                }
            },
            error: function (err) {
                console.log("Yükleme gerçekleşmedi. "  +  err)
            },
        });
    }

    const taksitleriYerlestir = (data) => {
        console.log(data)
        let _tbody = t.querySelector('tbody')
        $(_tbody).empty()
        for (let i = 0; i < data.length; i++) {
            let _tr = `
                <tr>
                    <td><div class="fv-row"><input type="text" class="form-control form-control-sm w-75px" name="installments[${i}].TaksitNo" value="${data[i].taksitNo}" /></div></td>
                    <td><div class="fv-row"><input type="text" class="form-control form-control-sm w-125px ps-12 flatpickr-input active" name="installments[${i}].TaksitTarihi" value="${moment(data[i].taksitTarihi).format("DD-MM-YYYY")}" /></div></td>
                    <td><div class="fv-row"><input type="text" class="form-control form-control-sm w-225px" name="installments[${i}].Aciklama" value="${data[i].aciklama}" /></div></td>
                    <td><div class="fv-row"><input type="text" class="form-control form-control-sm w-100px ps-12" name="installments[${i}].TaksitTutari" value="${data[i].taksitTutari}" /></div></td>
                </tr>`
            let _row = t.insertRow()
            _row.innerHTML = _tr
            _tbody.append(_row)
        }
        
    }

    const loadDataTable = () => {
        return $(n).DataTable({
            ajax: {
                url: "/ProjectBalance/GetCurrentAccountDetails/",
                type: "GET",
                datatype: "json"
            },
            "columns": [
                { "data": "projectInstallmentNo", "width": "15%" },
                {
                    "data": "curDate",
                    "render": function (data, type, row) {
                        return moment(data).format("DD-MM-YYYY");
                    }, "width": "15%"
                },
                { "data": "description", "width": "40%" },
                {
                    "data": "debit",
                    "render": function (data) {
                        return '<span class="d-flex justify-content-end">'+Intl.NumberFormat('tr-TR', { style: 'decimal' }).format(data)+'</span>'
                    }, "width": "15%"
                },
                {
                    "data": "credit",
                    "render": function (data) {
                        return '<span class="d-flex justify-content-end">' + Intl.NumberFormat('tr-TR', { style: 'decimal' }).format(data) + '</span>'
                    }, "width": "15%"
                }
            ],
            "footerCallback": function (row, data, start, end, display) {
                var api = this.api(),
                    data;

                // Remove the formatting to get integer data for summation
                var intVal = function (i) {
                    return typeof i === "string" ?
                        i.replace(/[\$,]/g, "") * 1 :
                        typeof i === "number" ?
                            i : 0;
                };

                // Total debit over all pages
                var totalB = api
                    .column(3)
                    .data()
                    .reduce(function (a, b) {
                        return intVal(a) + intVal(b);
                    }, 0);

                // Total over this page
                var pageTotalB = api
                    .column(3, {
                        page: "current"
                    })
                    .data()
                    .reduce(function (a, b) {
                        return intVal(a) + intVal(b);
                    }, 0);

                // Update footer
                // const number = new Intl.NumberFormat('tr-TR', { style: 'decimal' }).format(123456.789)
                pageTotalB = new Intl.NumberFormat('tr-TR', { style: 'decimal' }).format(pageTotalB);
                totalB = new Intl.NumberFormat('tr-TR', { style: 'decimal' }).format(totalB);
                $(api.column(3).footer()).html(
                    '<span class="d-flex justify-content-end">' + pageTotalB + " (Toplam:" + totalB + ")" + '</span>'
                );

                // Total credit over all pages
                var totalA = api
                    .column(4)
                    .data()
                    .reduce(function (a, b) {
                        return intVal(a) + intVal(b);
                    }, 0);

                // Total over this page
                var pageTotalA = api
                    .column(4, {
                        page: "current"
                    })
                    .data()
                    .reduce(function (a, b) {
                        return intVal(a) + intVal(b);
                    }, 0);

                // Update footer
                pageTotalA = new Intl.NumberFormat('tr-TR', { style: 'decimal' }).format(pageTotalA);
                totalA = new Intl.NumberFormat('tr-TR', { style: 'decimal' }).format(totalA);
                $(api.column(4).footer()).html(
                    '<span class="d-flex justify-content-end">' + pageTotalA + " (Toplam:" + totalA + ")" + '</span>'
                );


            },
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
            //columnDefs: [{ targets: [3], className: "d-flex justify-content-end" }],
            "width": "100%"
        });
    };

    return {
        init: function () {
            (n = document.querySelector("#kt_project_balance_table"),
                bu = document.querySelector("#kt_installment_calculate_button"),
                m = document.querySelector("#kt_modal_calculate_installment"),
                t = document.querySelector("#kt_calculate_installment_table"),
                f = document.querySelector("#kt_modal_calculate_installment_form"),
                s = f.querySelector("#kt_modal_calculate_installment_submit"),
                c = f.querySelector("#kt_modal_calculate_installment_cancel"),
                o = f.querySelector("#kt_modal_calculate_installment_close"),
                i = new bootstrap.Modal(document.querySelector("#kt_modal_calculate_installment")),
                v = FormValidation.formValidation(f, {
                    fields: {
                        TaksitTarihi: { validators: { notEmpty: { message: "Tarih boş geçilemez" } } },
                        Aciklama: { validators: { notEmpty: { message: "Açıklama boş geçilemez" } } },
                        TaksitTutari: { validators: { notEmpty: { message: "Tutar boş geçilemez" } } }
                    },
                    plugins: {
                        trigger: new FormValidation.plugins.Trigger,
                        bootstrap: new FormValidation.plugins.Bootstrap5({
                            rowSelector: ".fv-row", eleInvalidClass: "", eleValidClass: ""
                        })
                    }
                })
                ) && (

                dt = loadDataTable(),

                bu.addEventListener("click", (function (e) {
                    e.preventDefault()
                    taksitHesaplaGetir()
                    $(m).modal("show")
                })),

                // #kt_modal_add_currentaccounts_submit button tıklandı
                s.addEventListener("click", (function (e) {
                    e.preventDefault(),
                        v && v.validate().then((function (e) {
                                "Valid" == e
                                    ? (s.setAttribute("data-kt-indicator", "on"),
                                        s.disabled = !0,
                                        setTimeout((function () {
                                            s.removeAttribute("data-kt-indicator"),
                                                saveInstallments().done((function (res) {
                                                    if (res.success) {
                                                            Swal.fire({ text: "Form basariyla kayıt edildi!", icon: "success", buttonsStyling: !1, confirmButtonText: "Tamam", customClass: { confirmButton: "btn btn-primary" } })
                                                                .then((function (e1) { e1.isConfirmed && (i.hide(), s.disabled = !1, dt.ajax.reload() /*window.location = r.getAttribute("data-kt-redirect")*/) }))
                                                    } else {
                                                        Swal.fire({ title: "Kayıt gerçekleşmedi!", text: "Lütfen hataları düzeltiniz.", icon: "warning", buttonsStyling: !1, confirmButtonText: "Tamam", customClass: { confirmButton: "btn btn-primary" } })
                                                            .then((function (e2) { e2.isConfirmed && (s.disabled = !1) }))
                                                    }
                                                })).fail((function (err) {
                                                    console.log(err.status + " " + err.statusText),
                                                        Swal.fire({ title: "Kayıt gerçekleşmedi!", text: "Lütfen hataları düzeltiniz.", icon: "warning", buttonsStyling: !1, confirmButtonText: "Tamam", customClass: { confirmButton: "btn btn-primary" } })
                                                            .then((function (e2) { e2.isConfirmed && (s.disabled = !1) }))
                                                }))
                                        }), 2e3))
                                    : Swal.fire({ text: "Maalesef bazi alanların doldurulmadığı tespit edildi, lutfen zorunlu alanları doldurun.", icon: "error", buttonsStyling: !1, confirmButtonText: "Tamam", customClass: { confirmButton: "btn btn-primary" } })
                        }))
                })),

                // #kt_modal_add_currentaccounts_cancel button tıklandı
                c.addEventListener("click", (function (e) {
                    e.preventDefault(), f.reset(),
                        Swal.fire({ text: "Iptal etmek istediginizden emin misiniz??", icon: "warning", showCancelButton: !0, buttonsStyling: !1, confirmButtonText: "Evet, iptal et!", cancelButtonText: "Hayir, geri don", customClass: { confirmButton: "btn btn-primary", cancelButton: "btn btn-active-light" } })
                            .then((function (e) { e.value ? (f.reset(), i.hide()) : "cancel" === e.dismiss }))
                })),

                // #kt_modal_add_currentaccounts_close button tıklandı
                o.addEventListener("click", (function (e) {
                    e.preventDefault(),
                        Swal.fire({ text: "Iptal etmek istediginizden emin misiniz??", icon: "warning", showCancelButton: !0, buttonsStyling: !1, confirmButtonText: "Evet, iptal et!", cancelButtonText: "Hayir, geri don", customClass: { confirmButton: "btn btn-primary", cancelButton: "btn btn-active-light" } })
                            .then((function (e) { e.value ? (f.reset(), i.hide()) : "cancel" === e.dismiss }))
                }))
            )
        }
    }
}();

KTUtil.onDOMContentLoaded((function () { KTProjectList.init() }));