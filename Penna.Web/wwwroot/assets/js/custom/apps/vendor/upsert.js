"use strict";
var KTVendorUpsert = function () {
    var co, ci, t, f, v;

    var getCityData = (cid) => {
        return $.ajax({
            url: "/Vendor/GetCityData/",
            type: "GET",
            data: { cid: cid }
        });
    };

    var setCityData = (countryId) => {
        getCityData(countryId)
            .done(function (response) {
                var d = response.data;
                ci.options.length = 1;
                for (var i = 0; i < d.length; i++) {
                    var option = document.createElement("option");
                    option.text = d[i].text;
                    option.value = d[i].value;
                    ci.add(option);
                }
            })
            .fail(function (err) {
                Swal.fire({
                    text: `${err.status} - ${err.statusText}`,
                    icon: "error",
                    buttonsStyling: !1,
                    confirmButtonText: "Tamam!",
                    customClass: { confirmButton: "btn fw-bold btn-primary" }
                })
            });

    };

    var getTownData = (cid) => {
        return $.ajax({
            url: "/Vendor/GetTownData/",
            type: "GET",
            data: { cid: cid }
        });
    };

    var setTownData = (cityId) => {
        getTownData(cityId)
            .done(function (response) {
                var d = response.data;
                t.options.length = 1;
                for (var i = 0; i < d.length; i++) {
                    var option = document.createElement("option");
                    option.text = d[i].text;
                    option.value = d[i].value;
                    t.add(option);
                }
            })
            .fail(function (err) {
                Swal.fire({
                    text: `${err.status} - ${err.statusText}`,
                    icon: "error",
                    buttonsStyling: !1,
                    confirmButtonText: "Tamam!",
                    customClass: { confirmButton: "btn fw-bold btn-primary" }
                })
            });

    };



    return {
        init: function () {
            Inputmask({ "mask": "(999) 999-9999" }).mask("#CurrentAccount_Phone1"),
                Inputmask({ "mask": "(999) 999-9999" }).mask("#CurrentAccount_Phone2"),
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
                }).mask("#CurrentAccount_Email"),
                f = document.querySelector("#kt_upsert_vendors_form"),
                co = f.querySelector("#CurrentAccount_CountryId"),
                ci = f.querySelector("#CurrentAccount_CityId"),
                t = f.querySelector("#CurrentAccount_TownId"),
                $(f.querySelector('[name="CurrentAccount.CountryId"]')).on("change", (function () {
                    var s = this;
                    setCityData(s.options[s.selectedIndex].value);
                })),
                $(f.querySelector('[name="CurrentAccount.CityId"]')).on("change", (function () {
                    var ts = this;
                    setTownData(ts.options[ts.selectedIndex].value);
                })),
                v = FormValidation.formValidation(f, {
                    fields: {
                        "CurrentAccount.TaxId": {
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

KTUtil.onDOMContentLoaded((function () { KTVendorUpsert.init() }));
