"use strict";
var KTSubcontractorList = function () {
    var t, e, o, dt, n, el, cia, cie, toa, toe, fa, fe;

    var getCityData = (cid) => {
        return $.ajax({
            url: "/Subcontractor/GetCityData/",
            type: "GET",
            data: { cid: cid }
        });
    };

    var setCityData = (countryId) => {
        getCityData(countryId)
            .done(function (response) {
                var d = response.data;
                cia.options.length = 1;
                for (var i = 0; i < d.length; i++) {
                    var option = document.createElement("option");
                    option.text = d[i].text;
                    option.value = d[i].value;
                    cia.add(option);
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

    var setCityDatae = (countryId) => {
        getCityData(countryId)
            .done(function (response) {
                var d = response.data;
                cie.options.length = 1;
                for (var i = 0; i < d.length; i++) {
                    var option = document.createElement("option");
                    option.text = d[i].text;
                    option.value = d[i].value;
                    cie.add(option);
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
            url: "/Subcontractor/GetTownData/",
            type: "GET",
            data: { cid: cid }
        });
    };

    var setTownData = (cityId) => {
        getTownData(cityId)
            .done(function (response) {
                var d = response.data;
                toa.options.length = 1;
                for (var i = 0; i < d.length; i++) {
                    var option = document.createElement("option");
                    option.text = d[i].text;
                    option.value = d[i].value;
                    toa.add(option);
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

    var setTownDatae = (cityId) => {
        getTownData(cityId)
            .done(function (response) {
                var d = response.data;
                toe.options.length = 1;
                for (var i = 0; i < d.length; i++) {
                    var option = document.createElement("option");
                    option.text = d[i].text;
                    option.value = d[i].value;
                    toe.add(option);
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

    var businessGroupEnumList = () => {
        return $.ajax({
            url: "/Subcontractor/GetBusinessGroupEnumList/",
            type: "GET",
        });
    };

    var del = (id) => {
        return $.ajax({
            url: "/Subcontractor/Delete/",
            type: "POST",
            data: { id: id }
        });
    };
    
    var setDeleteEvents = () => {
        n.querySelectorAll('[data-kt-subcontractors-table-filter="delete_row"]').forEach((e => {
            e.addEventListener("click", (function (e) {
                e.preventDefault();
                var id = this.dataset["link"];
                const o = e.target.closest("tr"), n = o.querySelectorAll("td")[1].innerText;
                Swal.fire({ text: n + "'ı silmek istediğine eminmisin ?", icon: "warning", showCancelButton: !0, buttonsStyling: !1, confirmButtonText: "Evet, sil!", cancelButtonText: "Hayır, vazgeç", customClass: { confirmButton: "btn fw-bold btn-danger", cancelButton: "btn fw-bold btn-active-light-primary" } })
                    .then((function (e) {
                        e.value
                            ? del(id).then((function (res) {
                                if (res.success) {
                                    Swal.fire({ text: n + " silindi !.", icon: "success", buttonsStyling: !1, confirmButtonText: "Tamam!", customClass: { confirmButton: "btn fw-bold btn-primary" } })
                                        .then((function () { dt.ajax.reload() /*dt.row($(o)).remove().draw()*/ }))
                                } else {
                                    Swal.fire({ text: n + " silinmedi.", icon: "error", buttonsStyling: !1, confirmButtonText: "Tamam!", customClass: { confirmButton: "btn fw-bold btn-primary" } })
                                }
                            })) 
                            : "cancel" === e.dismiss && Swal.fire({ text: n + " silinmedi.", icon: "error", buttonsStyling: !1, confirmButtonText: "Tamam!", customClass: { confirmButton: "btn fw-bold btn-primary" } })
                    }))
            }))
        }))
    };

    
    var fillCityDll = async (data) => {
        var d = data.citydata;
        cie.options.length = 1;
        for (var i = 0; i < d.length; i++) {
            var option = document.createElement("option");
            option.text = d[i].text;
            option.value = d[i].value;
            cie.add(option);
        }
    };

    var fillTownDll = async (data) => {
        var d = data.towndata;
        toe.options.length = 1;
        for (var i = 0; i < d.length; i++) {
            var option = document.createElement("option");
            option.text = d[i].text;
            option.value = d[i].value;
            toe.add(option);
        }
    };


    var setCityAsync = async (data) => {
        await fillCityDll(data);
    }

    var setTownAsync = async (data) => {
        await fillTownDll(data);
    }


    var getSubcontractorById = (id) => {
        return $.ajax({
            url: "/Subcontractor/GetById/" + id,
            type: "GET"
        });
    };

    var setEditEvents = () => {
        n.querySelectorAll('[data-kt-subcontractors-table-filter="edit_row"]').forEach((e => {
            e.addEventListener("click", (function (e) {
                e.preventDefault();
                var id = this.dataset["link"];
                getSubcontractorById(id)
                    .done((data) => {

                        setCityAsync(data);

                        setTownAsync(data);

                        setEditFields(data);

                        $("#kt_modal_edit_subcontractors").modal("show");
                    })
                    .fail((err) => {
                        Swal.fire({ title:"işlem başarısız oldu.", text:` ${err.status} - ${err.statusText}`, icon: "error", buttonsStyling: !1, confirmButtonText: "Tamam, anladım!", customClass: { confirmButton: "btn fw-bold btn-primary" } })
                    });
            }))
        }))
    };

    var setEditFields = (data) => {
        document.querySelector("#kt_modal_edit_subcontractors_form [name='CurrentAccount.Id']").value = data.data.id;
        document.querySelector("#kt_modal_edit_subcontractors_form [name='CurrentAccount.CompanyName']").value = data.data.companyName;
        document.querySelector("#kt_modal_edit_subcontractors_form [name='CurrentAccount.CountryId']").value = data.data.countryId;
        document.querySelector("#kt_modal_edit_subcontractors_form [name='CurrentAccount.CityId']").value = data.data.cityId;
        document.querySelector("#kt_modal_edit_subcontractors_form [name='CurrentAccount.TownId']").value = data.data.townId;
        document.querySelector("#kt_modal_edit_subcontractors_form [name='CurrentAccount.BusinessGroupId']").value = data.data.businessGroupId;
        document.querySelector("#kt_modal_edit_subcontractors_form [name='CurrentAccount.TaxId']").value = data.data.taxId;
        document.querySelector("#kt_modal_edit_subcontractors_form [name='CurrentAccount.TaxOffice']").value = data.data.taxOffice;
        document.querySelector("#kt_modal_edit_subcontractors_form [name='CurrentAccount.AuthorizedPerson']").value = data.data.authorizedPerson;
        document.querySelector("#kt_modal_edit_subcontractors_form [name='CurrentAccount.Phone1']").value = data.data.phone1;
        document.querySelector("#kt_modal_edit_subcontractors_form [name='CurrentAccount.Phone2']").value = data.data.phone2;
        document.querySelector("#kt_modal_edit_subcontractors_form [name='CurrentAccount.Email']").value = data.data.email;
        document.querySelector("#kt_modal_edit_subcontractors_form [name='CurrentAccount.BankName']").value = data.data.bankName;
        document.querySelector("#kt_modal_edit_subcontractors_form [name='CurrentAccount.IBAN']").value = data.data.iban;
        document.querySelector("#kt_modal_edit_subcontractors_form [name='CurrentAccount.CompanyStatusId']").value = data.data.companyStatusId;
        document.querySelector("#kt_modal_edit_subcontractors_form [name='CurrentAccount.AccountTypeId']").value = data.data.accountTypeId;
        document.querySelector("#kt_modal_edit_subcontractors_form [name='CurrentAccount.TenantId']").value = data.data.tenantId;
        document.querySelector("#kt_modal_edit_subcontractors_form [name='CurrentAccount.CreatedBy']").value = data.data.createdBy;
        document.querySelector("#kt_modal_edit_subcontractors_form [name='CurrentAccount.CreatedDate']").value = data.data.createdDate;
        //document.querySelector("#kt_modal_edit_subcontractors_form [name='CurrentAccount.UpdatedBy']").value = data.data.updatedBy;
        //document.querySelector("#kt_modal_edit_subcontractors_form [name='CurrentAccount.UpdatedDate']").value = data.data.updatedDate;

        $('#kt_modal_edit_subcontractors_form select[name="CurrentAccount.BusinessGroupId"]').select2();
        $('#kt_modal_edit_subcontractors_form select[name="CurrentAccount.CountryId"]').select2();
        $('#kt_modal_edit_subcontractors_form select[name="CurrentAccount.CityId"]').select2();
        $('#kt_modal_edit_subcontractors_form select[name="CurrentAccount.TownId"]').select2();
        $('#kt_modal_edit_subcontractors_form select[name="CurrentAccount.CompanyStatusId"]').select2();
    };

    const f = () => {
        const t = document.querySelector('[data-kt-subcontractors-table-toolbar="base"]')
    };

    const loadDataTable = () => {
        return $(n).DataTable({
            "ajax": {
                "url": "/Subcontractor/GetAllData",
                "type": "GET",
                "datatype": "json"
            },
            "columns": [
                { "data": "companyName", "width": "13%" },
                { "data": "authorizedPerson", "width": "12%" },
                {
                    "data": "businessGroupId",
                    "render": function (data) {
                        return `<span class="badge badge-primary">${el[data].text}</span>`;
                    },
                    "width": "10%"
                },
                { "data": "phone1", "width": "7%" },
                { "data": "email", "width": "15%" },
                {
                    "data": "companyStatusId",
                    "render": function (data, type, row) {
                        if (data == 1) return `<span class="badge badge-success">Beyaz Liste</span>`;
                        if (data == 2) return `<span class="badge badge-danger">Kara Liste</span>`;
                    }, "width": "8%"
                },
                {
                    "data": "id",
                    orderable: false,
                    className: 'text-end ',
                    "render": function (data) { 
                        return `<a href="#" class="btn btn-icon btn-sm btn-warning" data-link="${data}" title="düzenle" data-kt-subcontractors-table-filter="edit_row"><i class="far fa-edit"></i></a>
                                <a href="#" class="btn btn-icon btn-sm btn-danger" data-link="${data}" title="sil" data-kt-subcontractors-table-filter="delete_row" data-bs-toggle="tooltip" data-bs-custom-class="tooltip-dark" data-bs-placement="top"><i class="far fa-trash-alt"></i></a>`;
                    }, "width": "10%"
                }
            ],
            language: {
                sDecimal: ",",
                sEmptyTable: "Tabloda herhangi bir veri mevcut değil",
                sInfo:
                    "_TOTAL_ kayıttan _START_ - _END_ arasındaki kayıtlar gösteriliyor",
                sInfoEmpty: "Kayıt yok",
                sInfoFiltered: "(_MAX_ kayıt içerisinden bulunan)",
                sInfoPostFix: "",
                sInfoThousands: ".",
                sLengthMenu: "Sayfada _MENU_ kayıt göster",
                sLoadingRecords: "Yükleniyor...",
                sProcessing: "İşleniyor...",
                sSearch: "Ara:",
                sZeroRecords: "Eşleşen kayıt bulunamadı",
                oPaginate: {
                    sFirst: "İlk",
                    sLast: "Son",
                    sNext: "Sonraki",
                    sPrevious: "Önceki",
                },
                oAria: {
                    sSortAscending: ": artan sütun sıralamasını aktifleştir",
                    sSortDescending: ": azalan sütun sıralamasını aktifleştir",
                },
                select: {
                    rows: {
                        _: "%d kayıt seçildi",
                        0: "",
                        1: "1 kayıt seçildi",
                    },
                },
            },
            info: !1,
            order: [[1, "asc"]],
            columnDefs: [{ orderable: !1, targets: 6 }],
            "width": "100%"
        });
    };

    return {
        init: function () {
            (n = document.querySelector("#kt_subcontractors_table"),
                businessGroupEnumList().done(function (data) { el = data })) && (

                fe = document.querySelector("#kt_modal_edit_subcontractors_form"),
                cie = fe.querySelector("#kt_modal_edit_subcontractors_form [name='CurrentAccount.CityId']"),
                toe = fe.querySelector("#kt_modal_edit_subcontractors_form [name='CurrentAccount.TownId']"),

                dt = loadDataTable().on("draw", function () { setDeleteEvents(), setEditEvents() }),

                fa = document.querySelector("#kt_modal_add_subcontractors_form"),
                cia = fa.querySelector("#kt_modal_add_subcontractors_form [name='CurrentAccount.CityId']"),
                toa = fa.querySelector("#kt_modal_add_subcontractors_form [name='CurrentAccount.TownId']"),

                

                $(fa.querySelector('[name="CurrentAccount.CountryId"]')).on("change", (function () {
                    var s = this;
                    setCityData(s.options[s.selectedIndex].value);
                    $('#kt_modal_add_subcontractors_form select[name="CurrentAccount.CountryId"]').select2();
                })),
                $(fa.querySelector('[name="CurrentAccount.CityId"]')).on("change", (function () {
                    var ts = this;
                    setTownData(ts.options[ts.selectedIndex].value);
                })),

                

                $(fe.querySelector('[name="CurrentAccount.CountryId"]')).on("change", (function () {
                    var s = this;
                    setCityDatae(s.options[s.selectedIndex].value);
                    $('#kt_modal_edit_subcontractors_form select[name="CurrentAccount.CountryId"]').select2();
                })),
                $(fe.querySelector('[name="CurrentAccount.CityId"]')).on("change", (function () {
                    var ts = this;
                    setTownDatae(ts.options[ts.selectedIndex].value);
                })),

                document.querySelector('[data-kt-subcontractors-table-filter="search"]').addEventListener("keyup", (function (e) {
                    dt.search(e.target.value).draw()
                }))
            )
        }
    }
}();

KTUtil.onDOMContentLoaded((function () { KTSubcontractorList.init() }));