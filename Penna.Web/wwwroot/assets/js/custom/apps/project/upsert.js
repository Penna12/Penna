"use strict";
var KTProjectUpsert = function () {
    var co, ci, t, f, mf, mco, mci, mt, mc, ms, mo, i, cas, v;

    var getCityData = (cid) => {
        return $.ajax({
            url: "/Project/GetCityData/",
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

    var setModalCityData = (countryId) => {
        getCityData(countryId)
            .done(function (response) {
                var d = response.data;
                mci.options.length = 1;
                for (var i = 0; i < d.length; i++) {
                    var option = document.createElement("option");
                    option.text = d[i].text;
                    option.value = d[i].value;
                    mci.add(option);
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
            url: "/Project/GetTownData/",
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

    var setModalTownData = (cityId) => {
        getTownData(cityId)
            .done(function (response) {
                var d = response.data;
                mt.options.length = 1;
                for (var i = 0; i < d.length; i++) {
                    var option = document.createElement("option");
                    option.text = d[i].text;
                    option.value = d[i].value;
                    mt.add(option);
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

    var getCurrentAccountData = (caid) => {
        return $.ajax({
            url: "/Project/GetCurrentAccountData/",
            type: "GET",
            data: { caid: caid }
        });
    };

    var setCurrentAccountData = (caid) => {
        getCurrentAccountData(caid)
            .done(function (response) {
                var d = response.data;
                f.querySelector("#EmployerFullName").value = d.companyName;
                f.querySelector("#EmployerPhone").value = d.phone1;
                f.querySelector("#EmployerEmail").value = d.email;
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
    }

    var saveCurrentAccount = () => {
        return $.ajax({
            url: "/Project/SaveCurrentAccount/",
            type: "POST",
            data: $('#kt_modal_add_currentaccounts_form').serialize()
        });
    };

    var addNewCurrentAccountDataToComboBox = (data) => {
        var option = document.createElement("option");
        option.text = data.companyName;
        option.value = data.id;
        cas.add(option);
    }

    return {
        init: function () {
            Inputmask({ "mask": "(999) 999-9999" }).mask("[name='Phone1']"),
                Inputmask({ "mask": "(999) 999-9999" }).mask("[name='Phone2']"),
                //Inputmask({ "mask": "9999-9999-9999-9999-9999-9999" }).mask("[name='IBAN']"),
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
                }).mask("[name='Email']"),
                i = new bootstrap.Modal(document.querySelector("#kt_modal_add_currentaccounts")),
                mf = document.querySelector("#kt_modal_add_currentaccounts_form"),
                mco = mf.querySelector("[name='CountryId']"),
                mci = mf.querySelector("[name='CityId']"),
                mt = mf.querySelector("[name='TownId']"),
                ms = mf.querySelector("#kt_modal_add_currentaccounts_submit"),
                mc = mf.querySelector("#kt_modal_add_currentaccounts_cancel"),
                mo = mf.querySelector("#kt_modal_add_currentaccounts_close"),

                f = document.querySelector("#kt_upsert_projects_form"),
                cas = f.querySelector("#Project_CurrentAccountId"),
                co = f.querySelector("#Project_CountryId"),
                ci = f.querySelector("#Project_CityId"),
                t = f.querySelector("#Project_TownId"),

                $(f.querySelector('[name="Project.CountryId"]')).on("change", (function () {
                    var s = this;
                    setCityData(s.options[s.selectedIndex].value);
                })),
                $(f.querySelector('[name="Project.CityId"]')).on("change", (function () {
                    var ts = this;
                    setTownData(ts.options[ts.selectedIndex].value);
                })),
                $(f.querySelector('[name="Project.CurrentAccountId"]')).on("change", (function () {
                    var x = this;
                    if (x.selectedIndex == 0) {
                        f.querySelector("#EmployerFullName").value = "";
                        f.querySelector("#EmployerPhone").value = "";
                        f.querySelector("#EmployerEmail").value = "";
                    } else {
                        setCurrentAccountData(x.options[x.selectedIndex].value);
                    }
                    
                })),

                $(mf.querySelector('[name="CountryId"]')).on("change", (function () {
                    var s = this;
                    setModalCityData(s.options[s.selectedIndex].value);
                })),
                $(mf.querySelector('[name="CityId"]')).on("change", (function () {
                    var ts = this;
                    setModalTownData(ts.options[ts.selectedIndex].value);
                })),
                

                v = FormValidation.formValidation(mf, {
                    fields: {
                        CompanyName: { validators: { notEmpty: { message: "Firma adı gereklidir" } } },
                        AuthorizedPerson: { validators: { notEmpty: { message: "Firma yetkili adı gereklidir" } } },
                        Address: { validators: { notEmpty: { message: "Adres gereklidir" } } },
                        CountryId: { validators: { notEmpty: { message: "Ülke seçimi gereklidir" } } },
                        CityId: { validators: { notEmpty: { message: "Şehir seçimi gereklidir" } } },
                        TownId: { validators: { notEmpty: { message: "Kasaba seçimi gereklidir" } } },
                        TaxId: {
                            validators: {
                                notEmpty: { message: "Vergi/TC Kimlik no gereklidir" },
                                stringLength: { min: 10, max: 11, message: "min 10, max 11 karakter girmelisiniz" },
                                regexp: {
                                    regexp: /^[0-9_]+$/,
                                    message: 'Sadece sayı girebilirsiniz'
                                }
                            }
                        },
                        TaxOffice: { validators: { notEmpty: { message: "Vergi dairesi gereklidir" } } },
                        Phone1: { validators: { notEmpty: { message: "Telefon no belirtiniz" } } },
                        Email: { validators: { notEmpty: { message: "Email belirtiniz" } } },
                        //CompanyStatusId: { validators: { notEmpty: { message: "Durumunu belirtiniz" } } }
                        //AccountTypeId: { validators: { notEmpty: { message: "Cari hesap türünü seçiniz" } } }
                    },
                    plugins: {
                        trigger: new FormValidation.plugins.Trigger,
                        bootstrap: new FormValidation.plugins.Bootstrap5({
                            rowSelector: ".fv-row", eleInvalidClass: "", eleValidClass: ""
                        })
                    }
                }),

                // #kt_modal_add_currentaccounts_submit button tıklandı
                ms.addEventListener("click", (function (e) {
                    e.preventDefault(),
                        v && v.validate().then((function (e) {
                            console.log("add validated!"),
                                "Valid" == e
                                ? (ms.setAttribute("data-kt-indicator", "on"),
                                    ms.disabled = !0,
                                        setTimeout((function () {
                                            ms.removeAttribute("data-kt-indicator"),
                                                saveCurrentAccount().done((function (res) {
                                                    if (res.success) {
                                                        addNewCurrentAccountDataToComboBox(res.data),
                                                        Swal.fire({ text: "Form basariyla gonderildi!", icon: "success", buttonsStyling: !1, confirmButtonText: "Tamam", customClass: { confirmButton: "btn btn-primary" } })
                                                            .then((function (e1) { e1.isConfirmed && (i.hide(), ms.disabled = !1, table.ajax.reload() /*window.location = r.getAttribute("data-kt-redirect")*/) }))
                                                    } else {
                                                        Swal.fire({ title: "Kayıt gerçekleşmedi!", text: "Lütfen hataları düzeltiniz.", icon: "warning", buttonsStyling: !1, confirmButtonText: "Tamam", customClass: { confirmButton: "btn btn-primary" } })
                                                            .then((function (e2) { e2.isConfirmed && (ms.disabled = !1) }))
                                                    }
                                                })).fail((function (err) {
                                                    console.log(err.status + " " + err.statusText),
                                                        Swal.fire({ title: "Kayıt gerçekleşmedi!", text: "Lütfen hataları düzeltiniz.", icon: "warning", buttonsStyling: !1, confirmButtonText: "Tamam", customClass: { confirmButton: "btn btn-primary" } })
                                                        .then((function (e2) { e2.isConfirmed && (ms.disabled = !1) }))
                                                }))
                                        }), 2e3))
                                    : Swal.fire({ text: "Maalesef bazi alanların doldurulmadığı tespit edildi, lutfen zorunlu alanları doldurun.", icon: "error", buttonsStyling: !1, confirmButtonText: "Tamam", customClass: { confirmButton: "btn btn-primary" } })
                        }))
                })),
                // #kt_modal_add_currentaccounts_cancel button tıklandı
                mc.addEventListener("click", (function (ms) {
                    ms.preventDefault(), mf.reset(),
                        Swal.fire({ text: "Iptal etmek istediginizden emin misiniz??", icon: "warning", showCancelButton: !0, buttonsStyling: !1, confirmButtonText: "Evet, iptal et!", cancelButtonText: "Hayir, geri don", customClass: { confirmButton: "btn btn-primary", cancelButton: "btn btn-active-light" } })
                        .then((function (ms) { ms.value ? (mf.reset(), i.hide()) : "cancel" === ms.dismiss && Swal.fire({ text: "Formunuz iptal edilmedi!.", icon: "error", buttonsStyling: !1, confirmButtonText: "Tamam", customClass: { confirmButton: "btn btn-primary" } }) }))
                })),
                // #kt_modal_add_currentaccounts_close button tıklandı
                mo.addEventListener("click", (function (ms) {
                    ms.preventDefault(),
                        Swal.fire({ text: "Iptal etmek istediginizden emin misiniz??", icon: "warning", showCancelButton: !0, buttonsStyling: !1, confirmButtonText: "Evet, iptal et!", cancelButtonText: "Hayir, geri don", customClass: { confirmButton: "btn btn-primary", cancelButton: "btn btn-active-light" } })
                        .then((function (ms) { ms.value ? (mf.reset(), i.hide()) : "cancel" === ms.dismiss && Swal.fire({ text: "Formunuz iptal edilmedi!.", icon: "error", buttonsStyling: !1, confirmButtonText: "Tamam", customClass: { confirmButton: "btn btn-primary" } }) }))
                }))
        }
    }
}();

KTUtil.onDOMContentLoaded((function () { KTProjectUpsert.init() }));
