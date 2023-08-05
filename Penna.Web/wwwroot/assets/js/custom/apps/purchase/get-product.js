"use strict";
var KTPurchaseRequest = function () {
    var f, v, el;

    var businessGroupEnumList = async () => {
        await fetch("/Product/GetBusinessGroupEnumList/")
            .then(response => response.json())
            .then(d => { el = d, console.log(d) })
            .catch(err => console.log(`GetBusinessGroupEnumList biligisi alırkken hata: ${err}`));
    };

    return {
        init: function () {
            (f = document.querySelector("#kt_purchase_send_request_form"),
                businessGroupEnumList()),
                v = FormValidation.formValidation(f, {
                    fields: {
                        "PurchaseRequest.Quantity": { validators: { notEmpty: { message: "Lütfen miktar giriniz" } } },
                        "PurchaseRequest.Deadline": { validators: { notEmpty: { message: "Lütfen termin tarihi giriniz" } } },
                        "PurchaseRequest.Description": { validators: { notEmpty: { message: "Lütfen açıklama giriniz" } } }
                    },
                    plugins: {
                        trigger: new FormValidation.plugins.Trigger,
                        bootstrap: new FormValidation.plugins.Bootstrap5({
                            rowSelector: ".fv-row", eleInvalidClass: "", eleValidClass: ""
                        })
                    }
                }),

                $(f.querySelector("#PurchaseRequest_ProductId")).on("change", async function (e) {
                    e.preventDefault();
                    var p = this.value;
                    console.log(p);
                    await fetch(`/Product/GetProduct/${p}`)
                        .then(response => response.json())
                        .then(d => {
                            var p = d.data;
                            console.log(p);
                            document.querySelector('#Brand').value = p.brand;
                            document.querySelector('#Unit').value = p.unit.name;
                            document.querySelector('#Quantity').value = p.quantity;
                            document.querySelector('#Dimensions').value = p.dimensions;
                            document.querySelector('#Thickness').value = p.thickness;
                            document.querySelector('#Species').value = p.species;
                            document.querySelector('#BusinessGroupId').value = el[p.businessGroupId - 1].text;
                            $(q1).change();
                        })
                        .catch(err => console.log(`ürün biligisi alırkken hata: ${err}`));
                }),

                $(f).on("submit", function (e) {
                    v && v.validate().then(function (e) { console.log("add validated! ", e)})
                })
        }
    }
}();

KTUtil.onDOMContentLoaded((function () { KTPurchaseRequest.init() }));
