"use strict";
var KTModalBlockEdit = function () {
    var t, e, o, n, r, i, table;

    var save = () => {
        return $.ajax({
            url: "/Block/Update/",
            type: "POST",
            data: $('#kt_modal_edit_blocks_form').serialize()
        });
    };

    return {
        init: function () {
            table = $("#kt_blocks_table").DataTable(),
                i = new bootstrap.Modal(document.querySelector("#kt_modal_edit_blocks")),
                r = document.querySelector("#kt_modal_edit_blocks_form"),
                t = r.querySelector("#kt_modal_edit_blocks_submit"),
                e = r.querySelector("#kt_modal_edit_blocks_cancel"),
                o = r.querySelector("#kt_modal_edit_blocks_close"),
                n = FormValidation.formValidation(r, {
                    fields: {
                        Name: { validators: { notEmpty: { message: "Blok adı gereklidir" } } },
                        TypeId: { validators: { notEmpty: { message: "Blok tipini seçiniz" } } },
                        FloorCount: {
                            validators: {
                                notEmpty: { message: "Zemin + Kat sayısı gereklidir" },
                                regexp: {
                                    regexp: /^[0-9_]+$/,
                                    message: 'Sadece sayı girebilirsiniz'
                                }
                            }
                        },
                        BasementCount: {
                            validators: {
                                notEmpty: { message: "Bodrum Kat sayısı gereklidir" },
                                regexp: {
                                    regexp: /^[0-9_]+$/,
                                    message: 'Sadece sayı girebilirsiniz'
                                }
                            }
                        },
                        ApartmentCount: {
                            validators: {
                                notEmpty: { message: "Kat başına, bağımsız bölüm sayısı gereklidir" },
                                regexp: {
                                    regexp: /^[0-9_]+$/,
                                    message: 'Sadece sayı girebilirsiniz'
                                }
                            }
                        },
                        BlockCostCalculation: { validators: { notEmpty: { message: "Blok hesaplama birimini seçiniz" } } },
                        ApartmentCostCalculation: { validators: { notEmpty: { message: "Blok hesaplama birimini seçiniz" } } },
                        PublicAreaCostCalculation: { validators: { notEmpty: { message: "Blok hesaplama birimini seçiniz" } } }
                    },
                    plugins: {
                        trigger: new FormValidation.plugins.Trigger,
                        bootstrap: new FormValidation.plugins.Bootstrap5({
                            rowSelector: ".fv-row", eleInvalidClass: "", eleValidClass: ""
                        })
                    }
                }),
                // #kt_modal_edit_blocks_submit button tıklandı
                t.addEventListener("click", (function (e) {
                    e.preventDefault(),
                        n && n.validate().then((function (e) {
                            console.log("edit validated!"),
                                "Valid" == e
                                    ? (t.setAttribute("data-kt-indicator", "on"),
                                        t.disabled = !0,
                                        setTimeout((function () {
                                            t.removeAttribute("data-kt-indicator"),
                                                save().done((function (res) {
                                                    if (res.success) {
                                                        Swal.fire({ text: "Form basariyla kaydedildi!", icon: "success", buttonsStyling: !1, confirmButtonText: "Tamam", customClass: { confirmButton: "btn btn-primary" } })
                                                            .then((function (e1) { e1.isConfirmed && (r.reset(), i.hide(), t.disabled = !1, table.ajax.reload()) }))
                                                    } else {
                                                        Swal.fire({ title: "Kayıt gerçekleşmedi!", text: "Lütfen hataları düzeltiniz.", icon: "warning", buttonsStyling: !1, confirmButtonText: "Tamam", customClass: { confirmButton: "btn btn-primary" } })
                                                            .then((function (e2) { e2.isConfirmed && (t.disabled = !1) }))
                                                    }
                                                })).fail((function (err) { console.log(err.status + " " + err.statusText) }))
                                        }), 2e3))
                                    : Swal.fire({ text: "Maalesef bazi alanların doldurulmadığı tespit edildi, lutfen zorunlu alanları doldurun.", icon: "error", buttonsStyling: !1, confirmButtonText: "Tamam", customClass: { confirmButton: "btn btn-primary" } })
                        }))
                })),
                // #kt_modal_edit_blocks_cancel button tıklandı
                e.addEventListener("click", (function (t) {
                    t.preventDefault(), r.reset(),
                        Swal.fire({ text: "Iptal etmek istediginizden emin misiniz??", icon: "warning", showCancelButton: !0, buttonsStyling: !1, confirmButtonText: "Evet, iptal et!", cancelButtonText: "Hayir, geri don", customClass: { confirmButton: "btn btn-primary", cancelButton: "btn btn-active-light" } })
                            .then((function (t) { t.value ? (r.reset(), i.hide()) : "cancel" === t.dismiss && Swal.fire({ text: "Formunuz iptal edilmedi!.", icon: "error", buttonsStyling: !1, confirmButtonText: "Tamam", customClass: { confirmButton: "btn btn-primary" } }) }))
                })),
                // #kt_modal_edit_blocks_close button tıklandı
                o.addEventListener("click", (function (t) {
                    t.preventDefault(),
                        Swal.fire({ text: "Iptal etmek istediginizden emin misiniz??", icon: "warning", showCancelButton: !0, buttonsStyling: !1, confirmButtonText: "Evet, iptal et!", cancelButtonText: "Hayir, geri don", customClass: { confirmButton: "btn btn-primary", cancelButton: "btn btn-active-light" } })
                            .then((function (t) { t.value ? (r.reset(), i.hide()) : "cancel" === t.dismiss && Swal.fire({ text: "Formunuz iptal edilmedi!.", icon: "error", buttonsStyling: !1, confirmButtonText: "Tamam", customClass: { confirmButton: "btn btn-primary" } }) }))
                }))
        }
    }
}();

KTUtil.onDOMContentLoaded((function () { KTModalBlockEdit.init() }));