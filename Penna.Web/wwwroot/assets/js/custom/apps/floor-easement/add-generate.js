"use strict";
var KTFloorEasementAdd = function () {
    var s, v, f, table;

    var save = () => {
        return $.ajax({
            url: "/FloorEasement/GenerateApartments/",
            type: "POST",
            data: $('#kt_floor_auto_generate_form').serialize()
        });
    };

    return {
        init: function () {
            table = $("#kt_floor_table").DataTable(),
            f = document.querySelector("#kt_floor_auto_generate_form"),
            s = f.querySelector("#kt_floor_auto_generate_submit"),
            v = FormValidation.formValidation(f, {
                fields: {
                    FloorCount: {
                        validators: {
                            notEmpty: { message: "Kat Sayısını girin" },
                            regexp: {
                                regexp: /^[-0-9,0-9_]+$/,
                                message: 'Sadece sayı girebilirsiniz'
                            }
                        }
                    },
                    StartFloorNo: {
                        validators: {
                            notEmpty: {
                                message: "Başlanacak Kat No girin" },
                            regexp: {
                                regexp: /^[-0-9,0-9_]+$/,
                                message: 'Sadece sayı girebilirsiniz'
                            }
                        }
                    },
                    NumberOfHousesOnEachFloor: {
                        validators: {
                            notEmpty: { message: "Oluşturulacak Daire Sayısını girin" },
                            regexp: {
                                regexp: /^[-0-9,0-9_]+$/,
                                message: 'Sadece sayı girebilirsiniz'
                            }
                        }
                    },
                    StartApartmentNo: {
                        validators: {
                            notEmpty: { message: "Başlanacak Daire No girin" },
                            regexp: {
                                regexp: /^[-0-9,0-9_]+$/,
                                message: 'Sadece sayı girebilirsiniz'
                            }
                        }
                    },
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
            // #kt_floor_auto_generate_submit button tıklandı
            s.addEventListener("click", (function (e) {
                e.preventDefault(),
                    v && v.validate().then((function (e) {
                        console.log("add validated!"),
                            "Valid" == e
                                ? (s.setAttribute("data-kt-indicator", "on"),
                                    s.disabled = !0,
                                    setTimeout((function () {
                                        s.removeAttribute("data-kt-indicator"),
                                            save().done((function (res) {
                                                if (res.success) {
                                                    Swal.fire({ text: "Form basariyla gonderildi!", icon: "success", buttonsStyling: !1, confirmButtonText: "Tamam", customClass: { confirmButton: "btn btn-primary" } })
                                                        .then((function (e1) { e1.isConfirmed && (s.disabled = !1, table.ajax.reload() /*window.location = f.getAttribute("data-kt-redirect")*/) }))
                                                } else {
                                                    Swal.fire({ title: "Kayıt gerçekleşmedi!", text: "Lütfen hataları düzeltiniz.", icon: "warning", buttonsStyling: !1, confirmButtonText: "Tamam", customClass: { confirmButton: "btn btn-primary" } })
                                                        .then((function (e2) { e2.isConfirmed && (s.disabled = !1) }))
                                                }
                                            })).fail((function (err) { console.log(err.status + " " + err.statusText) }))
                                    }), 2e3))
                                : Swal.fire({ text: "Maalesef bazi alanların doldurulmadığı tespit edildi, lutfen zorunlu alanları doldurun.", icon: "error", buttonsStyling: !1, confirmButtonText: "Tamam", customClass: { confirmButton: "btn btn-primary" } })
                    }))
            }))
        }
    }
}();

KTUtil.onDOMContentLoaded((function () { KTFloorEasementAdd.init() }));