"use strict";
var KTDirectPurchaseAdd = function () {
    var t, n, r, b, g, p, s, pq, pp, rq;

    var save = () => {
        return $.ajax({
            url: "/Purchase/SavePurchase/",
            type: "POST",
            data: $('#add_direct_purchase_form').serialize()
        });
    };

    async function getData(url) {
        const response = await fetch(url);
        return response.json();
    }

    return {
        init: function () {
            (r = document.querySelector("#add_direct_purchase_form"),
                t = r.querySelector("#add_direct_purchase_submit"),
                b = r.querySelector("#Purchase_RequestedBlockId"),
                p = r.querySelector("#Purchase_RequestedPlace"),
                g = r.querySelector("#BusinessGroupId"),
                s = r.querySelector("#Purchase_SupplierCurrentAccountId"),
                pq = r.querySelector("#Purchase_Quantity"),
                pp = r.querySelector("#Purchase_ProductId"),
                rq = r.querySelector("#RemainingQuantity"),
                n = FormValidation.formValidation(r, {
                    fields: {
                        "Purchase.ProductId": { validators: { notEmpty: { message: "Ürün seçimi gereklidir" } } },
                        "Purchase.PurchaseDate": { validators: { notEmpty: { message: "Tarih gereklidir" } } },
                        "Purchase.RequestedDeliveryDate": { validators: { notEmpty: { message: "İstenen teslim tarihi gereklidir" } } },
                        "Purchase.Quantity": {
                            validators: {
                                notEmpty: { message: "Lütfen miktarı giriniz" },
                                greaterThan: { min:1, message: "Miktar sıfırdan büyük olmalıdır." },
                                regexp: {
                                    regexp: /^[0-9_]+$/,
                                    message: 'Sadece sayı girebilirsiniz'
                                }
                            }
                        },
                        "Purchase.RequestedBlockId": { validators: { notEmpty: { message: "Talep edilen birimi seçiniz" } } },
                        "Purchase.SupplierCurrentAccountId": { validators: { notEmpty: { message: "Tedarikçi seçiniz" } } },
                        "Purchase.InvoiceDate": { validators: { notEmpty: { message: "Fatura tarihi gereklidir" } } },
                        "Purchase.InvoiceNo": { validators: { notEmpty: { message: "Fatura no gereklidir" } } },
                        "Purchase.InvoiceAmount": {
                            validators: {
                                notEmpty: { message: "Fatura tutarı gereklidir" },
                                greaterThan: { min: 1, message: "Fatura tutarı sıfırdan büyük olmalıdır." },
                                regexp: {
                                    regexp: /^[0-9_]+$/,
                                    message: 'Sadece sayı girebilirsiniz'
                                }
                            }
                        },
                        "Purchase.Deadline": { validators: { notEmpty: { message: "Termin tarihi gereklidir" } } },
                        "BusinessGroupId": { validators: { notEmpty: { message: "İş grubu seçiniz" } } }
                    },
                    plugins: {
                        trigger: new FormValidation.plugins.Trigger,
                        bootstrap: new FormValidation.plugins.Bootstrap5({
                            rowSelector: ".fv-row", eleInvalidClass: "", eleValidClass: ""
                        })
                    }
                }),

                // Purchase.ProductId
                pp.addEventListener("mousedown", (function (e) {
                    e.preventDefault();
                    e.stopPropagation();
                    return false
                })),

                // Purchase.Quantity
                pq.addEventListener("change", (function (e) {
                    var t = this;
                    var r = rq.value;
                    var q = t.value;
                    if (parseInt(q) > parseInt(r)) {
                        t.value = r;
                        Swal.fire({ text: "Talep miktarından fazla tutar giremezsiniz.", icon: "warning", buttonsStyling: !1, confirmButtonText: "Tamam!", customClass: { confirmButton: "btn fw-bold btn-primary" } })
                            .then((function () { t.focus() }))
                    }
                })),

                // Purchase.RequestedBlockId change trigger, Purchase.RequestedPlace içine Block.Name yazdırılacak
                b.addEventListener("change", (function (e) {
                    e.preventDefault(),
                        console.log("Purchase_RequestedBlockId clicked!", this),
                        getData(`/Purchase/GetBlockName/${b.value}`)
                            .then(d => { p.value = `${d.data.project.projectName}, ${d.data.name}` })
                            .catch(err => console.log(`GetBlockName alınırken hata: ${err}`))
                })),

                // select id=BusinessGroupId change trigger, Purchase.SupplierCurrentAccountId içine Supplier listesi oluşturulacak
                g.addEventListener("change", (function (e) {
                    e.preventDefault(),
                        console.log("BusinessGroupId clicked!", this),
                        getData(`/Purchase/GetSupplierCurrentAccount/${g.value}`)
                            .then(d => {
                                $(s).empty();
                                //s.innerHTML = "";
                                for (var i = 0; i < d.data.length; i++) {
                                    var opt = d.data[i];
                                    var el = document.createElement("option");
                                    el.textContent = opt.companyName;
                                    el.value = opt.id;
                                    s.appendChild(el);
                                };
                            })
                            .catch(err => console.log(`GetSupplierCurrentAccount alınırken hata: ${err}`))
                })),

                // #add_tenants_submit button tıklandı
                t.addEventListener("click", (function (e) {
                    e.preventDefault(),
                        n && n.validate().then((function (e) {
                            console.log("add validated!"),
                                "Valid" == e
                                    ? (t.setAttribute("data-kt-indicator", "on"),
                                        t.disabled = !0,
                                        setTimeout((function () {
                                            t.removeAttribute("data-kt-indicator"),
                                                save().done((function (res) {
                                                    if (res.success) {
                                                        Swal.fire({ text: "Form basariyla gonderildi!", icon: "success", buttonsStyling: !1, confirmButtonText: "Tamam", customClass: { confirmButton: "btn btn-primary" } })
                                                            .then((function (e1) { e1.isConfirmed && (t.disabled = !1, window.location = r.getAttribute("data-kt-redirect")) }))
                                                    } else {
                                                        Swal.fire({ title: "Kayıt gerçekleşmedi!", text: "Lütfen hataları düzeltiniz.", icon: "warning", buttonsStyling: !1, confirmButtonText: "Tamam", customClass: { confirmButton: "btn btn-primary" } })
                                                            .then((function (e2) { e2.isConfirmed && (t.disabled = !1) }))
                                                    }
                                                })).fail((function (err) { console.log(err.status + " " + err.statusText), t.disabled = !1 }))
                                        }), 2e3))
                                    : Swal.fire({ text: "Maalesef bazi alanların doldurulmadığı tespit edildi, lutfen zorunlu alanları doldurun.", icon: "error", buttonsStyling: !1, confirmButtonText: "Tamam", customClass: { confirmButton: "btn btn-primary" } })
                        }))
                }))
        )}
    }
}();

KTUtil.onDOMContentLoaded((function () { KTDirectPurchaseAdd.init() }));