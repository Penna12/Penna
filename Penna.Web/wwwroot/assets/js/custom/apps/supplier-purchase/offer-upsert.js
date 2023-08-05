"use strict";
var KTJoinOfferUpsert = function () {
    var f, v, s, t;


    return {
        init: function () {
            f = document.querySelector("#add_join_offer_form"),
                s = f.querySelector("#add_join_offer_submit"),
                t = f.querySelector("#OfferTime"),
                v = FormValidation.formValidation(f, {
                    fields: {
                        "OfferTime": { validators: { notEmpty: { message: "Teklif tarihi gereklidir" } } },
                        "Amount": {
                            validators: {
                                notEmpty: { message: "Miktar gereklidir" },
                                greaterThan: { min: 1, message: "Miktar sıfırdan büyük olmalıdır." },
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

                t.value = moment().format("DD-MM-YYYY"), t.disabled = true,

                // #add_join_offer_submit button tıklandı
                s.addEventListener("click", (function (e) {
                    e.preventDefault(),
                        v && v.validate().then((function (e) {
                            console.log("add validated!"),
                                "Valid" == e
                                    ? $(f).submit()
                                    : Swal.fire({ text: "Maalesef bazi alanların doldurulmadığı tespit edildi, lutfen zorunlu alanları doldurun.", icon: "error", buttonsStyling: !1, confirmButtonText: "Tamam", customClass: { confirmButton: "btn btn-primary" } })
                        }))
                }))
        }
    }
}();

KTUtil.onDOMContentLoaded((function () { KTJoinOfferUpsert.init() }));
