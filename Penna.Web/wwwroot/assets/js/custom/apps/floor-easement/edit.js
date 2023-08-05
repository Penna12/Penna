"use strict";
var KTModalFloorEasementEdit = function () {
    var s, c, o, v, f, m, table;

    var save = () => {
        return $.ajax({
            url: "/FloorEasement/Update/",
            type: "POST",
            data: $('#kt_modal_edit_floors_form').serialize()
        });
    };

    return {
        init: function () {
            table = $("#kt_floor_table").DataTable(),
                m = new bootstrap.Modal(document.querySelector("#kt_modal_edit_floors")),
                f = document.querySelector("#kt_modal_edit_floors_form"),
                s = f.querySelector("#kt_modal_edit_floors_submit"),
                c = f.querySelector("#kt_modal_edit_floors_cancel"),
                o = f.querySelector("#kt_modal_edit_floors_close"),
                v = FormValidation.formValidation(f, {
                    fields: {
                        Floor: {
                            validators: {
                                notEmpty: { message: "Bulunduğu Kat no girin" },
                                regexp: {
                                    regexp: /^[-0-9,0-9_]+$/,
                                    message: 'Sadece sayı girebilirsiniz'
                                }
                            }
                        },
                        SectionName: { validators: { notEmpty: { message: "Bölüm Adını girin" } } },
                        Gross: {
                            validators: {
                                notEmpty: { message: "Brüt m2 girin" },
                                regexp: {
                                    regexp: /^[0-9_]+$/,
                                    message: 'Sadece sayı girebilirsiniz'
                                }
                            }
                        },
                        Net: {
                            validators: {
                                notEmpty: { message: "Net m2 girin" },
                                regexp: {
                                    regexp: /^[0-9_]+$/,
                                    message: 'Sadece sayı girebilirsiniz'
                                }
                            }
                        },
                        Gabari: {
                            validators: {
                                notEmpty: { message: "Gabari m2 girin" },
                                regexp: {
                                    regexp: /^[0-9_]+$/,
                                    message: 'Sadece sayı girebilirsiniz'
                                }
                            }
                        }
                    },  
                    plugins: {
                        trigger: new FormValidation.plugins.Trigger,
                        bootstrap: new FormValidation.plugins.Bootstrap5({
                            rowSelector: ".fv-row", eleInvalidClass: "", eleValidClass: ""
                        })
                    }
                }),
                // #kt_modal_edit_floors_submit button tıklandı
                s.addEventListener("click", (function (e) {
                    e.preventDefault(),
                        v && v.validate().then((function (e) {
                            console.log("edit validated!"),
                                "Valid" == e
                                    ? (s.setAttribute("data-kt-indicator", "on"),
                                        s.disabled = !0,
                                        setTimeout((function () {
                                            s.removeAttribute("data-kt-indicator"),
                                                save().done((function (res) {
                                                    if (res.success) {
                                                        Swal.fire({ text: "Form basariyla kaydedildi!", icon: "success", buttonsStyling: !1, confirmButtonText: "Tamam", customClass: { confirmButton: "btn btn-primary" } })
                                                            .then((function (e1) { e1.isConfirmed && (m.hide(), s.disabled = !1, table.ajax.reload()) }))
                                                    } else {
                                                        Swal.fire({ title: "Kayıt gerçekleşmedi!", text: "Lütfen hataları düzeltiniz.", icon: "warning", buttonsStyling: !1, confirmButtonText: "Tamam", customClass: { confirmButton: "btn btn-primary" } })
                                                            .then((function (e2) { e2.isConfirmed && (s.disabled = !1) }))
                                                    }
                                                })).fail((function (err) { console.log(err.status + " " + err.statusText) }))
                                        }), 2e3))
                                    : Swal.fire({ text: "Maalesef bazi alanların doldurulmadığı tespit edildi, lutfen zorunlu alanları doldurun.", icon: "error", buttonsStyling: !1, confirmButtonText: "Tamam", customClass: { confirmButton: "btn btn-primary" } })
                        }))
                })),
                // #kt_modal_edit_floors_cancel button tıklandı
                c.addEventListener("click", (function (e) {
                    e.preventDefault(), f.reset(),
                        Swal.fire({ text: "Iptal etmek istediginizden emin misiniz??", icon: "warning", showCancelButton: !0, buttonsStyling: !1, confirmButtonText: "Evet, iptal et!", cancelButtonText: "Hayir, geri don", customClass: { confirmButton: "btn btn-primary", cancelButton: "btn btn-active-light" } })
                            .then((function (e) { e.value ? (f.reset(), m.hide()) : "cancel" === e.dismiss && Swal.fire({ text: "Formunuz iptal edilmedi!.", icon: "error", buttonsStyling: !1, confirmButtonText: "Tamam", customClass: { confirmButton: "btn btn-primary" } }) }))
                })),
                // #kt_modal_edit_floors_close button tıklandı
                o.addEventListener("click", (function (e) {
                    e.preventDefault(),
                        Swal.fire({ text: "Iptal etmek istediginizden emin misiniz??", icon: "warning", showCancelButton: !0, buttonsStyling: !1, confirmButtonText: "Evet, iptal et!", cancelButtonText: "Hayir, geri don", customClass: { confirmButton: "btn btn-primary", cancelButton: "btn btn-active-light" } })
                            .then((function (e) { e.value ? (f.reset(), m.hide()) : "cancel" === e.dismiss && Swal.fire({ text: "Formunuz iptal edilmedi!.", icon: "error", buttonsStyling: !1, confirmButtonText: "Tamam", customClass: { confirmButton: "btn btn-primary" } }) }))
                }))
        }
    }
}();

KTUtil.onDOMContentLoaded((function () { KTModalFloorEasementEdit.init() }));