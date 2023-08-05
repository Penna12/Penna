"use strict";
var KTBlockTeamAdd = function () {
    var s, v, f, table;

    var save = () => {
        return $.ajax({
            url: "/Block/CreateTeam/",
            type: "POST",
            data: $('#kt_add_block_teams_form').serialize()
        });
    };

    return {
        init: function () {
            table = $("#kt_block_teams_table").DataTable(),
                f = document.querySelector("#kt_add_block_teams_form"),
                s = f.querySelector("#kt_add_block_teams_submit"),
                v = FormValidation.formValidation(f, {
                    fields: {
                        UserId: { validators: { notEmpty: { message: "Personel seçiniz" } } },
                        UserPositionId: { validators: { notEmpty: { message: "Görev seçiniz" } } },
                        Phone: { validators: { notEmpty: { message: "Telefon gereklidir" } } }
                },
                plugins: {
                    trigger: new FormValidation.plugins.Trigger,
                    bootstrap: new FormValidation.plugins.Bootstrap5({
                        rowSelector: ".fv-row", eleInvalidClass: "", eleValidClass: ""
                    })
                }
            }),
            // #kt_add_block_teams_submit button tıklandı
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
                                                        .then((function (e) { e.isConfirmed && (f.reset(), s.disabled = !1, table.ajax.reload() /*window.location = f.getAttribute("data-kt-redirect")*/) }))
                                                } else {
                                                    Swal.fire({ title: "Kayıt gerçekleşmedi!", text: "Lütfen hataları düzeltiniz.", icon: "warning", buttonsStyling: !1, confirmButtonText: "Tamam", customClass: { confirmButton: "btn btn-primary" } })
                                                        .then((function (e) { e.isConfirmed && (s.disabled = !1) }))
                                                }
                                            })).fail((function (err) { console.log(err.status + " " + err.statusText) }))
                                    }), 2e3))
                                : Swal.fire({ text: "Maalesef bazi alanların doldurulmadığı tespit edildi, lutfen zorunlu alanları doldurun.", icon: "error", buttonsStyling: !1, confirmButtonText: "Tamam", customClass: { confirmButton: "btn btn-primary" } })
                    }))
            }))
            
        }
    }
}();

KTUtil.onDOMContentLoaded((function () { KTBlockTeamAdd.init() }));