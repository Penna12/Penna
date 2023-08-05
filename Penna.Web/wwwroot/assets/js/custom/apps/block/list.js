"use strict";
var KTBlockList = function () {
    var t, e, o, dt, n;

    var getBlockById = (id) => {
        return $.ajax({
            url: "/Block/GetById/" + id,
            type: "GET"
        });
    };

    var del = (id) => {
        return $.ajax({
            url: "/Block/Delete/",
            type: "POST",
            data: { id: id }
        });
    };
    
    var setDeleteEvents = () => {
        n.querySelectorAll('[data-kt-blocks-table-filter="delete_row"]').forEach((e => {
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
        n.querySelectorAll('[data-kt-blocks-table-filter="edit_row"]').forEach((e => {
            e.addEventListener("click", (function (e) {
                e.preventDefault();
                var id = this.dataset["link"];
                getBlockById(id)
                    .done((data) => {
                        setEditFields(data);
                        $("#kt_modal_edit_blocks").modal("show");
                    })
                    .fail((err) => {
                        Swal.fire({ title:"işlem başarısız oldu.", text:` ${err.status} - ${err.statusText}`, icon: "error", buttonsStyling: !1, confirmButtonText: "Tamam, anladım!", customClass: { confirmButton: "btn fw-bold btn-primary" } })
                    });
            }))
        }))
    };

    var setEditFields = (data) => {
        document.querySelector("#kt_modal_edit_blocks_form [name='Id']").value = data.data.id;
        document.querySelector("#kt_modal_edit_blocks_form [name='ProjectId']").value = data.data.projectId;
        document.querySelector("#kt_modal_edit_blocks_form [name='Name']").value = data.data.name;
        document.querySelector("#kt_modal_edit_blocks_form [name='TypeId']").value = data.data.typeId;
        document.querySelector("#kt_modal_edit_blocks_form [name='FloorCount']").value = data.data.floorCount;
        document.querySelector("#kt_modal_edit_blocks_form [name='BasementCount']").value = data.data.basementCount;
        document.querySelector("#kt_modal_edit_blocks_form [name='ApartmentCount']").value = data.data.apartmentCount;
        document.querySelector("#kt_modal_edit_blocks_form [name='BlockCostCalculation']").value = data.data.blockCostCalculation;
        document.querySelector("#kt_modal_edit_blocks_form [name='ApartmentCostCalculation']").value = data.data.apartmentCostCalculation;
        document.querySelector("#kt_modal_edit_blocks_form [name='PublicAreaCostCalculation']").value = data.data.publicAreaCostCalculation;
        document.querySelector("#kt_modal_edit_blocks_form [name='CreatedBy']").value = data.data.createdBy;
        document.querySelector("#kt_modal_edit_blocks_form [name='CreatedDate']").value = data.data.createdDate;
    };

    var possibleNewBlockCreate = () => {
        return $.ajax({
            url: "/Block/PossibleNewBlockCreate/",
            type: "GET",
        });
    };

    var checkBlockCountEvent = () => {
        var f = document.querySelector('#kt_modal_add_blocks_form')
        $(f.querySelector('[name="TypeId"]')).on("change", function () {
            var s = this;
            var v = s.options[s.selectedIndex].value;
            if (s.selectedIndex == 1 || v == 1) {
                possibleNewBlockCreate()
                    .done((data) => {
                        if (data.success == false) {
                            s.selectedIndex = 2;
                            Swal.fire({ title: "BLOK açma sayısı aşılıyor.", text: "Bu proje için BLOK açma sayısını aşıyorsunuz.", icon: "info", buttonsStyling: !1, confirmButtonText: "Tamam, anladım!", customClass: { confirmButton: "btn fw-bold btn-primary" } })
                        }
                    })
                    .fail((err) => {
                        Swal.fire({ title: "Blok sayısı kontrol edilirken, bir hata oldu.", text: ` ${err.status} - ${err.statusText}`, icon: "error", buttonsStyling: !1, confirmButtonText: "Tamam, anladım!", customClass: { confirmButton: "btn fw-bold btn-primary" } })
                    });
            }
        });
    };

    const f = () => {
        const t = document.querySelector('[data-kt-blocks-table-toolbar="base"]')
    };

    const loadDataTable = () => {
        return $(n).DataTable({
            createdRow: function (row, data, dataIndex) {
                $(row).find('td:eq(3)').attr('data-filter', data.typeId == 1 ? 'blok' : 'publicArea');
            },
            "ajax": {
                "url": "/Block/GetAllData",
                "type": "GET",
                "datatype": "json"
            },
            "columns": [
                { "data": "name", "width": "25%" },
                { "data": "floorCount", "width": "10%" },
                { "data": "basementCount", "width": "10%" },
                { "data": "apartmentCount", "width": "10%" },
                {
                    "data": "typeId",
                    "render": function (data, type, row) {
                        if (data == 1) return `<span class="badge badge-primary">Blok</span>`;
                        if (data == 2) return `<span class="badge badge-primary">Ortak Alan</span>`;
                    }, "width": "10%"
                },
                {
                    "data": "id",
                    orderable: false,
                    className: 'text-end ',
                    "render": function (data) { 
                        return `<a href="/FloorEasement/Manage/${data}" class="btn btn-icon btn-sm btn-primary" title="kat irtifakı işlemleri" data-kt-blocks-table-filter="kat_irtifaki"><i class="far fa-building"></i></a>
                                <a href="#" class="btn btn-icon btn-sm btn-warning" data-link="${data}" title="düzenle" data-kt-blocks-table-filter="edit_row" data-bs-toggle="modal" data-bs-target="#kt_modal_edit_blocks"><i class="far fa-edit"></i></a>
                                <a href="#" class="btn btn-icon btn-sm btn-danger" data-link="${data}" title="sil" data-kt-blocks-table-filter="delete_row" data-bs-toggle="tooltip" data-bs-custom-class="tooltip-dark" data-bs-placement="top"><i class="far fa-trash-alt"></i></a>`;
                    }, "width": "20%"
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
            order: [[0, "asc"]],
            columnDefs: [{ orderable: !1, targets: 4 }],
            "width": "100%"
        });
    };

    return {
        init: function () {
            (n = document.querySelector("#kt_blocks_table")) && (

                dt = loadDataTable().on("draw", function () { setDeleteEvents(), setEditEvents() }),

                checkBlockCountEvent(),

                document.querySelector('[data-kt-blocks-table-filter="search"]').addEventListener("keyup", (function (e) {
                    dt.search(e.target.value).draw()
                }))
            )
        }
    }
}();

KTUtil.onDOMContentLoaded((function () { KTBlockList.init() }));