"use strict";
var KTModalUnitsAdd = function () {
    var t, e, o, n, r, i, table;

    var save = () => {
        return $.ajax({
            url: "/Unit/Create/",
            type: "POST",
            data: $('#kt_modal_add_units_form').serialize()
        });
    };

    return {
        init: function () {
            table = $("#kt_units_table").DataTable(),
                i = new bootstrap.Modal(document.querySelector("#kt_modal_add_units")),
                r = document.querySelector("#kt_modal_add_units_form"),
                t = r.querySelector("#kt_modal_add_units_submit"),
                e = r.querySelector("#kt_modal_add_units_cancel"),
                o = r.querySelector("#kt_modal_add_units_close"),
                n = FormValidation.formValidation(r, {
                    fields: {
                        Name: { validators: { notEmpty: { message: "Birim adı gereklidir" } } },
                    },
                    plugins: {
                        trigger: new FormValidation.plugins.Trigger,
                        bootstrap: new FormValidation.plugins.Bootstrap5({
                            rowSelector: ".fv-row", eleInvalidClass: "", eleValidClass: ""
                        })
                    }
                }),
                // #kt_modal_add_units_submit button tıklandı
                t.addEventListener("click", (function (e) {
                    e.preventDefault(),
                        n && n.validate().then((function (e) {
                            console.log("add validated!"),
                                "Valid" == e
                                    ? (t.setAttribute("data-kt-indicator", "on"),
                                        t.disabled = !0,
                                        setTimeout((function () {
                                            t.removeAttribute("data-kt-indicator"),
                                                save().done((function (res) {
                                                    if (res.success) {
                                                        Swal.fire({ text: "Form basariyla gonderildi!", icon: "success", buttonsStyling: !1, confirmButtonText: "Tamam", customClass: { confirmButton: "btn btn-primary" } })
                                                            .then((function (e1) { e1.isConfirmed && (i.hide(), t.disabled = !1, table.ajax.reload() /*window.location = r.getAttribute("data-kt-redirect")*/) }))
                                                    } else {
                                                        Swal.fire({ title: "Kayıt gerçekleşmedi!", text: "Lütfen hataları düzeltiniz.", icon: "warning", buttonsStyling: !1, confirmButtonText: "Tamam", customClass: { confirmButton: "btn btn-primary" } })
                                                            .then((function (e2) { e2.isConfirmed && (t.disabled = !1) }))
                                                    }
                                                })).fail((function (err) {
                                                    console.log(err.status + " " + err.statusText),
                                                        Swal.fire({ title: "Kayıt gerçekleşmedi!", text: "Lütfen hataları düzeltiniz.", icon: "warning", buttonsStyling: !1, confirmButtonText: "Tamam", customClass: { confirmButton: "btn btn-primary" } })
                                                            .then((function (e2) { e2.isConfirmed && (t.disabled = !1) }))
                                                }))
                                        }), 2e3))
                                    : Swal.fire({ text: "Maalesef bazi alanların doldurulmadığı tespit edildi, lutfen zorunlu alanları doldurun.", icon: "error", buttonsStyling: !1, confirmButtonText: "Tamam", customClass: { confirmButton: "btn btn-primary" } })
                        }))
                })),
                // #kt_modal_add_units_cancel button tıklandı
                e.addEventListener("click", (function (t) {
                    t.preventDefault(), r.reset(),
                        Swal.fire({ text: "Iptal etmek istediginizden emin misiniz??", icon: "warning", showCancelButton: !0, buttonsStyling: !1, confirmButtonText: "Evet, iptal et!", cancelButtonText: "Hayir, geri don", customClass: { confirmButton: "btn btn-primary", cancelButton: "btn btn-active-light" } })
                            .then((function (t) { t.value ? (r.reset(), i.hide()) : "cancel" === t.dismiss && Swal.fire({ text: "Formunuz iptal edilmedi!.", icon: "error", buttonsStyling: !1, confirmButtonText: "Tamam", customClass: { confirmButton: "btn btn-primary" } }) }))
                })),
                // #kt_modal_add_units_close button tıklandı
                o.addEventListener("click", (function (t) {
                    t.preventDefault(),
                        Swal.fire({ text: "Iptal etmek istediginizden emin misiniz??", icon: "warning", showCancelButton: !0, buttonsStyling: !1, confirmButtonText: "Evet, iptal et!", cancelButtonText: "Hayir, geri don", customClass: { confirmButton: "btn btn-primary", cancelButton: "btn btn-active-light" } })
                            .then((function (t) { t.value ? (r.reset(), i.hide()) : "cancel" === t.dismiss && Swal.fire({ text: "Formunuz iptal edilmedi!.", icon: "error", buttonsStyling: !1, confirmButtonText: "Tamam", customClass: { confirmButton: "btn btn-primary" } }) }))
                }))
        }
    }
}();

KTUtil.onDOMContentLoaded((function () { KTModalUnitsAdd.init() }));