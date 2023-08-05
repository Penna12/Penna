"use strict";
var KTTenantsList = function () {
    var t, e, o, dt, n;

    var adminform = () => {
        return `<input type="hidden" name="TenantId" value="" />
                            <!--begin::Input group FullName-->
                            <div class="fv-row mb-7">
                                <!--begin::Label-->
                                <label class="required fs-6 fw-bold mb-2">Ad Soyad</label>
                                <!--end::Label-->
                                <!--begin::Input-->
                                <input type="text" name="FullName" id="FullName" class="form-control form-control-solid" placeholder="Ad Soyad giriniz" />
                                <!--end::Input-->
                            </div>
                            <!--end::Input group FullName-->
                            <!--begin::Input group Email-->
                            <div class="fv-row mb-7">
                                <!--begin::Label-->
                                <label class="fs-6 fw-bold mb-2">
                                    <span class="required">Email</span>
                                    <i class="fas fa-exclamation-circle ms-1 fs-7" data-bs-toggle="tooltip" title="Eposta hesabı aktifleştirmek için gereklidir"></i>
                                </label>
                                <!--end::Label-->
                                <!--begin::Input-->
                                <input type="text" name="Email" id="Email" class="form-control form-control-solid" placeholder="Eposta giriniz" />
                                <!--end::Input-->
                            </div>
                            <!--end::Input group Email-->
                            <!--begin::Input group PhoneNumber-->
                            <div class="fv-row mb-15">
                                <!--begin::Label-->
                                <label class="required fs-6 fw-bold mb-2">Telefon</label>
                                <!--end::Label-->
                                <!--begin::Input-->
                                <input type="text" name="PhoneNumber" id="PhoneNumber" class="form-control form-control-solid" data-inputmax="(999) 999-9999" placeholder="Telefon no giriniz" />
                                <!--end::Input-->
                            </div>
                            <!--end::Input group PhoneNumber-->
                            <!--begin::Input group Password-->
                            <div class="fv-row mb-7">
                                <!--begin::Label-->
                                <label class="required fs-6 fw-bold mb-2">Şifre</label>
                                <!--end::Label-->
                                <!--begin::Input-->
                                <input type="password" name="Password" id="Password" class="form-control form-control-solid" placeholder="Şifre giriniz" />
                                <!--end::Input-->
                            </div>
                            <!--end::Input group Password-->
                            <!--begin::Input group ConfirmPassword-->
                            <div class="fv-row mb-7">
                                <!--begin::Label-->
                                <label class="required fs-6 fw-bold mb-2">Şifre</label>
                                <!--end::Label-->
                                <!--begin::Input-->
                                <input type="password" name="ConfirmPassword" id="ConfirmPassword" class="form-control form-control-solid" placeholder="Şifre tekrarını giriniz" />
                                <!--end::Input-->
                            </div>
                            <!--end::Input group ConfirmPassword-->`;
    }

    var del = (id) => {
        return $.ajax({
            url: "/Tenants/Delete/",
            type: "POST",
            data: { id: id }
        });
    };

    var getAdminUser = (tenantId) => {
        return $.ajax({
            url: "/Tenants/GetAdminUserByTenantId/",
            type: "GET",
            data: { tenantId : tenantId }
        });
    };
    
    var setDeleteEvents = () => {
        n.querySelectorAll('[data-kt-tenant-table-filter="delete_row"]').forEach((e => {
            e.addEventListener("click", (function (e) {
                e.preventDefault();
                var id = this.dataset["link"];
                const o = e.target.closest("tr"), n = o.querySelectorAll("td")[0].innerText;
                Swal.fire({ text: n + "'ı silmek istediğine eminmisin ?", icon: "warning", showCancelButton: !0, buttonsStyling: !1, confirmButtonText: "Evet, sil!", cancelButtonText: "Hayır, vazgeç", customClass: { confirmButton: "btn fw-bold btn-danger", cancelButton: "btn fw-bold btn-active-light-primary" } })
                    .then((function (e) {
                        e.value
                            ? del(id).then((function (res) {
                                if (res.success) {
                                    Swal.fire({ text: n + " silindi !.", icon: "success", buttonsStyling: !1, confirmButtonText: "Tamam!", customClass: { confirmButton: "btn fw-bold btn-primary" } })
                                        .then((function () { dt.row($(o)).remove().draw() }))
                                } else {
                                    Swal.fire({ text: n + " silinmedi.", icon: "error", buttonsStyling: !1, confirmButtonText: "Tamam!", customClass: { confirmButton: "btn fw-bold btn-primary" } })
                                }
                            })) 
                            : "cancel" === e.dismiss && Swal.fire({ text: n + " silinmedi.", icon: "error", buttonsStyling: !1, confirmButtonText: "Tamam!", customClass: { confirmButton: "btn fw-bold btn-primary" } })
                    }))
            }))
        }))
    };

    var setEditEvents = () => {
        n.querySelectorAll('[data-kt-tenant-table-filter="edit_row"]').forEach((e => {
            e.addEventListener("click", (function (e) {
                e.preventDefault();
                var id = this.dataset["link"];
                getTenantById(id)
                    .done((data) => {
                        //$("#tenant-edit").html(data);
                        setEditFields(data);
                        $("#kt_modal_edit_tenants").modal("show");
                    })
                    .fail((err) => {
                        Swal.fire({ title:"işlem başarısız oldu.", text:` ${err.status} - ${err.statusText}`, icon: "error", buttonsStyling: !1, confirmButtonText: "Tamam, anladım!", customClass: { confirmButton: "btn fw-bold btn-primary" } })
                    });
            }))
        }))
    };

    var setDetailEvents = () => {
        n.querySelectorAll('[data-kt-tenant-table-filter="detail_row"]').forEach((e => {
            e.addEventListener("click", (function (e) {
                e.preventDefault();
                var id = this.dataset["link"];

                GetTenantDetailViewComponent(id)
                    .done((data) => {
                        $("#tenant-content").html(data);
                        $("#kt_modal_view_tenant").modal("show");
                    })
                    .fail(() => {
                        Swal.fire({ text: "işlem başarısız oldu.", icon: "error", buttonsStyling: !1, confirmButtonText: "Tamam, anladım!", customClass: { confirmButton: "btn fw-bold btn-primary" } })
                    });
            }));
        }));
    };

    var setAdminUserEvents = () => {
        n.querySelectorAll('[data-kt-tenant-table-filter="adminuser_row"]').forEach((e => {
            e.addEventListener("click", (function (e) {
                e.preventDefault();
                var id = this.dataset["link"];
                getAdminUser(id)
                    .done((res) => {
                        if (res.found == undefined) {
                            $("#adminuser-content").html(res);
                            $("#kt_modal_admin_tenants_form button[type='submit']").addClass("d-none");
                            $("#kt_modal_admin_tenants_form button[type='reset']").text("Kapat");
                            $("#kt_modal_admin_tenants").modal("show");
                        } else {
                            $("#adminuser-content").html(adminform);
                            $("#kt_modal_admin_tenants_form button[type='submit']").removeClass("d-none");
                            $("#kt_modal_admin_tenants_form button[type='reset']").text("Vazgeç");
                            document.querySelector("#kt_modal_admin_tenants_form input[name='TenantId']").value = id;
                            $("#kt_modal_admin_tenants").modal("show");
                        }
                    })
                    .fail(function (err) {
                        console.log(`${err.status} - ${err.statusText}`);
                    });

            }));
        }));
    };

    var setEditFields = (data) => {
        document.querySelector("#kt_modal_edit_tenants_form [name='Id']").value = data.data.id;
        document.querySelector("#kt_modal_edit_tenants_form [name='CountryDialCode']").value = data.data.countryDialCode;
        document.querySelector("#kt_modal_edit_tenants_form [name='IsActive']").value = data.data.isActive;
        document.querySelector("#kt_modal_edit_tenants_form [name='IsLocked']").value = data.data.isLocked;
        document.querySelector("#kt_modal_edit_tenants_form [name='CreatedBy']").value = data.data.createdBy;
        document.querySelector("#kt_modal_edit_tenants_form [name='CreatedDate']").value = data.data.createdDate;
        document.querySelector("#kt_modal_edit_tenants_form [name='UpdatedBy']").value = data.data.updatedBy;
        document.querySelector("#kt_modal_edit_tenants_form [name='UpdatedDate']").value = data.data.updatedDate;

        document.querySelector("#kt_modal_edit_tenants_form [name='Title']").value = data.data.title;
        document.querySelector("#kt_modal_edit_tenants_form [name='FullName']").value = data.data.fullName;
        document.querySelector("#kt_modal_edit_tenants_form [name='Email']").value = data.data.email;
        document.querySelector("#kt_modal_edit_tenants_form [name='Phone']").value = data.data.phone;
        document.querySelector("#kt_modal_edit_tenants_form [name='TaxId']").value = data.data.taxId;
        document.querySelector("#kt_modal_edit_tenants_form [name='TaxOffice']").value = data.data.taxOffice;
        document.querySelector("#kt_modal_edit_tenants_form [name='Address']").value = data.data.address;
        document.querySelector("#kt_modal_edit_tenants_form [name='CountryId']").value = data.data.countryId;
        document.querySelector("#kt_modal_edit_tenants_form [name='CityId']").value = data.data.cityId;
        $('#kt_modal_edit_tenants_form select[name="CountryId"]').select2();
        $('#kt_modal_edit_tenants_form select[name="CityId"]').select2();
    };

    var getTenantById = (id) => {
        return $.ajax({
            url: "/Tenants/GetById/" + id,
            type: "GET"
        });
    }

    var GetTenantEditViewComponent = (id) => {
        return $.ajax({
            url: "/Tenants/GetTenantEditViewComponent/" + id,
            type: "GET"
        });
    }

    var GetTenantDetailViewComponent = (id) => {
        return $.ajax({
            url: "/Tenants/GetTenantDetailViewComponent/" + id,
            type: "GET"
        });
    }

    const f = () => {
        const t = document.querySelector('[data-kt-tenant-table-toolbar="base"]')
    };

    const loadDataTable = () => {
        return $(n).DataTable({
            createdRow: function (row, data, dataIndex) {
                $(row).find('td:eq(4)').attr('data-filter', data.isActive ? 'active' : 'passive');
                $(row).find('td:eq(5)').prop('style', 'padding-right:20px');
            },
            "ajax": {
                "url": "/Tenants/GetAllData",
                "type": "GET",
                "datatype": "json"
            },
            "columns": [
                { "data": "title", "width": "20%" },
                { "data": "fullName", "width": "20%" },
                { "data": "email", "width": "20%" },
                {
                    "data": "phone",
                    "render": function (data, type, row) {
                        return `${row.countryDialCode}-${data}`;
                    }, "width": "15%"
                },
                {
                    "data": "isActive",
                    "render": function (data, type, row) {
                        if (data == true) return `<span class="badge badge-success">Aktif</span>`;
                        if (data == false) return `<span class="badge badge-danger">Pasif</span>`;
                    }, "width": "8%"
                },
                {
                    "data": "id",
                    orderable: false,
                    className: 'text-end ',
                    "render": function (data) { 
                        return `<a href="#" class="btn btn-icon btn-sm btn-info" data-link="${data}" title="admin kullanıcı" data-kt-tenant-table-filter="adminuser_row" ><i class="fas fa-user-plus"></i></a>
                                <a href="#" class="btn btn-icon btn-sm btn-primary" data-link="${data}" title="incele" data-kt-tenant-table-filter="detail_row"><i class="fas fa-info"></i></a>
                                <a href="#" class="btn btn-icon btn-sm btn-warning" data-link="${data}" title="düzenle" data-kt-tenant-table-filter="edit_row"><i class="far fa-edit"></i></a>
                                <a href="#" class="btn btn-icon btn-sm btn-danger" data-link="${data}" title="sil" data-kt-tenant-table-filter="delete_row" data-bs-toggle="tooltip" data-bs-custom-class="tooltip-dark" data-bs-placement="top"><i class="far fa-trash-alt"></i></a>`;
                    }, "width": "17%"
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
            order: [],
            columnDefs: [{ orderable: !1, targets: 5 }],
            "width": "100%"
        });
    };

    return {
        init: function () {
            (n = document.querySelector("#kt_tenants_table")) && (

                dt = loadDataTable().on("draw", function () { setDeleteEvents(), setEditEvents(), setDetailEvents(), setAdminUserEvents() }),

                document.querySelector('[data-kt-tenant-table-filter="search"]').addEventListener("keyup", (function (e) {
                    dt.search(e.target.value).draw()
                })),

                //e = $('[data-kt-tenant-table-filter="month"]'),
                o = document.querySelectorAll('[data-kt-tenant-table-filter="status_type"] [name="status_type"]'),

                document.querySelector('[data-kt-tenant-table-filter="filter"]').addEventListener("click", (function () {
                    let c = "";
                    o.forEach((t => { t.checked && (c = t.value), "all" === c && (c = "") }));
                    dt.search(c).draw()
                })),

                document.querySelector('[data-kt-tenant-table-filter="reset"]').addEventListener("click", (function () {
                    //e.val(null).trigger("change"), 
                    o[0].checked = !0, dt.search("").draw()
                }))
            )
        }
    }
}();

KTUtil.onDOMContentLoaded((function () { KTTenantsList.init() }));