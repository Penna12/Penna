"use strict";
var KTProductInOut = function () {
    var f, v, el, q1, q2, i;

    var businessGroupEnumList = async () => {
        await fetch("/Product/GetBusinessGroupEnumList/")
            .then(response => response.json())
            .then(d => { el = d, console.log(d) })
            .catch(err => console.log(`GetBusinessGroupEnumList biligisi alırkken hata: ${err}`));
    };

    return {
        init: function () {
            (f = document.querySelector("#kt_product_in_out_form"),
                q1 = f.querySelector("#ProductInOut_Quantity"),
                q2 = f.querySelector("#Quantity"),
                i = f.querySelector("#Input_fl"),
                businessGroupEnumList()),

                v = FormValidation.formValidation(f, {
                    fields: {
                        //"ProductInOut.ProductId": { validators: { notEmpty: { message: "Lütfen ürün seçiniz" } } },
                        //"ProductInOut.Quantity": { validators: { notEmpty: { message: "Lütfen miktar giriniz" } } },
                        "ProductInOut.DispatchPoint": { validators: { notEmpty: { message: "Lütfen sevk noktası giriniz" } } },
                        "ProductInOut.Description": { validators: { notEmpty: { message: "Lütfen açıklama giriniz" } } }
                    },
                    plugins: {
                        trigger: new FormValidation.plugins.Trigger,
                        bootstrap: new FormValidation.plugins.Bootstrap5({
                            rowSelector: ".fv-row", eleInvalidClass: "", eleValidClass: ""
                        })
                    }
                }),

                $(f.querySelector("#ProductInOut_ProductId")).on("change", async function (e) {
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

                $(q1).on("change", function (e) {
                    if ( i.value == "False" && (!isNaN(parseInt(q1.value))) && (!isNaN(parseInt(q2.value))) ) {
                        if (parseInt(q2.value) < parseInt(q1.value)) {
                            q1.value = q2.value
                        } else
                            if (parseInt(q1.value) < 0) {
                                q1.value = 0
                            }
                    }
                }),

                $(f).on("submit", function (e) {
                    v && v.validate().then(function (e) { console.log("add validated! ", e)})
                })
        }
    }
}();

KTUtil.onDOMContentLoaded((function () { KTProductInOut.init() }));
