"use strict";
var KTModalTenantsEdit = function () {
    var t, e, o, n, r, i;

    var save = () => {
        return $.ajax({
            url: "/Tenants/Update/",
            type: "POST",
            data: $('#kt_modal_edit_tenants_form').serialize()
        });
    };

    return {
        init: function () {
            Inputmask({ "mask": "(999) 999-9999" }).mask("#Phone"),
                Inputmask({
                    mask: "*{1,20}[.*{1,20}][.*{1,20}][.*{1,20}]@*{1,20}[.*{2,6}][.*{1,2}]",
                    greedy: false,
                    onBeforePaste: function (pastedValue, opts) {
                        pastedValue = pastedValue.toLowerCase();
                        return pastedValue.replace("mailto:", "");
                    },
                    definitions: {
                        "*": {
                            validator: '[0-9A-Za-z!#$%&"*+/=?^_`{|}~\-]',
                            cardinality: 1,
                            casing: "lower"
                        }
                    }
                }).mask("#Email"),
                i = new bootstrap.Modal(document.querySelector("#kt_modal_edit_tenants")),
                r = document.querySelector("#kt_modal_edit_tenants_form"),
                t = r.querySelector("#kt_modal_edit_tenants_submit"),
                e = r.querySelector("#kt_modal_edit_tenants_cancel"),
                o = r.querySelector("#kt_modal_edit_tenants_close"),
                n = FormValidation.formValidation(r, {
                    fields: {
                        Title: { validators: { notEmpty: { message: "Ünvan gereklidir" } } },
                        FullName: { validators: { notEmpty: { message: "Ad Soyad gereklidir" } } },
                        Email: {
                            validators: {
                                notEmpty: { message: "Eposta gereklidir" },
                                emailAddress: { message: "Girilen, geçerli bir e-posta adresi değil" }
                            }
                        },
                        Phone: { validators: { notEmpty: { message: "Telefon gereklidir" } } },
                        CityId: { validators: { notEmpty: { message: "Şehir gereklidir" } } },
                        CountryId: { validators: { notEmpty: { message: "Ülke gereklidir" } } },
                        TaxId: {
                            validators: {
                                notEmpty: { message: "Vergi/TC Kimlik no gereklidir" },
                                stringLength: { min: 10, max: 11, message: "min 10, max 11 karaktar girmelisiniz" },
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
                // #kt_modal_edit_tenants_form [name=CountryId]  değiştirildi
                $(r.querySelector('[name="CountryId"]')).on("change", (function () {
                    n.revalidateField("CountryId")
                })),
                // #kt_modal_edit_tenants_submit button tıklandı
                t.addEventListener("click", (function (e) {
                    e.preventDefault(),
                        n && n.validate().then((function (e) {
                            console.log("edit validated!"),
                                "Valid" == e
                                    ? (t.setAttribute("data-kt-indicator", "on"),
                                        t.disabled = !0,
                                        setTimeout((function () {
                                            t.removeAttribute("data-kt-indicator"),
                                                save().then((function (res) {
                                                    if (res.success) {
                                                        Swal.fire({ text: "Form basariyla kaydedildi!", icon: "success", buttonsStyling: !1, confirmButtonText: "Tamam", customClass: { confirmButton: "btn btn-primary" } })
                                                            .then((function (e1) { e1.isConfirmed && (i.hide(), t.disabled = !1, window.location = r.getAttribute("data-kt-redirect")) }))
                                                    } else {
                                                        Swal.fire({ title: "Kayıt gerçekleşmedi!", text: "Lütfen hataları düzeltiniz.", icon: "warning", buttonsStyling: !1, confirmButtonText: "Tamam", customClass: { confirmButton: "btn btn-primary" } })
                                                            .then((function (e2) { e2.isConfirmed && (t.disabled = !1) }))
                                                    }
                                                })).fail((function (err) { console.log(err.status + " " + err.statusText) }))
                                        }), 2e3))
                                    : Swal.fire({ text: "Maalesef bazi alanların doldurulmadığı tespit edildi, lutfen zorunlu alanları doldurun.", icon: "error", buttonsStyling: !1, confirmButtonText: "Tamam", customClass: { confirmButton: "btn btn-primary" } })
                        }))
                })),
                // #kt_modal_edit_tenants_cancel button tıklandı
                e.addEventListener("click", (function (t) {
                    t.preventDefault(), r.reset(),
                        Swal.fire({ text: "Iptal etmek istediginizden emin misiniz??", icon: "warning", showCancelButton: !0, buttonsStyling: !1, confirmButtonText: "Evet, iptal et!", cancelButtonText: "Hayir, geri don", customClass: { confirmButton: "btn btn-primary", cancelButton: "btn btn-active-light" } })
                            .then((function (t) { t.value ? (r.reset(), i.hide()) : "cancel" === t.dismiss && Swal.fire({ text: "Formunuz iptal edilmedi!.", icon: "error", buttonsStyling: !1, confirmButtonText: "Tamam", customClass: { confirmButton: "btn btn-primary" } }) }))
                })),
                // #kt_modal_edit_tenants_close button tıklandı
                o.addEventListener("click", (function (t) {
                    t.preventDefault(),
                        Swal.fire({ text: "Iptal etmek istediginizden emin misiniz??", icon: "warning", showCancelButton: !0, buttonsStyling: !1, confirmButtonText: "Evet, iptal et!", cancelButtonText: "Hayir, geri don", customClass: { confirmButton: "btn btn-primary", cancelButton: "btn btn-active-light" } })
                            .then((function (t) { t.value ? (r.reset(), i.hide()) : "cancel" === t.dismiss && Swal.fire({ text: "Formunuz iptal edilmedi!.", icon: "error", buttonsStyling: !1, confirmButtonText: "Tamam", customClass: { confirmButton: "btn btn-primary" } }) }))
                }))
        }
    }
}();

KTUtil.onDOMContentLoaded((function () { KTModalTenantsEdit.init() }));