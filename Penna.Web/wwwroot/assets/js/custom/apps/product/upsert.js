"use strict";
var KTProductUpsert = function () {
    var f, v;


    return {
        init: function () {
            Inputmask({ "mask": "(999) 999-9999" }).mask("#Product_Phone1"),
                Inputmask({ "mask": "(999) 999-9999" }).mask("#Product_Phone2"),
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
                }).mask("#Product_Email"),
                f = document.querySelector("#kt_upsert_products_form"),
                v = FormValidation.formValidation(f, {
                    fields: {
                        "Product.TaxId": {
                            validators: {
                                notEmpty: { message: "Vergi/TC Kimlik no gereklidir" },
                                stringLength: { min: 10, max: 11, message: "min 10, max 11 karakter girmelisiniz" },
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
                })
        }
    }
}();

KTUtil.onDOMContentLoaded((function () { KTProductUpsert.init() }));
