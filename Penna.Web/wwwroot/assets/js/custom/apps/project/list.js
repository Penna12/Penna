"use strict";
var KTProjectList = function () {
    var t, e, dt, n, u;

    var del = (id) => {
        return $.ajax({
            url: "/Project/Delete/",
            type: "DELETE",
            data: { id: id }
        });
    };
    
    var setDeleteEvents = () => {
        n.querySelectorAll('[data-kt-projects-table-filter="delete_row"]').forEach((e => {
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
                                        .then((function () { dt.ajax.reload(); /*dt.row($(o)).remove().draw()*/ }))
                                } else {
                                    Swal.fire({ text: n + " silinmedi.", icon: "error", buttonsStyling: !1, confirmButtonText: "Tamam!", customClass: { confirmButton: "btn fw-bold btn-primary" } })
                                }
                            })) 
                            : "cancel" === e.dismiss && Swal.fire({ text: n + " silinmedi.", icon: "error", buttonsStyling: !1, confirmButtonText: "Tamam!", customClass: { confirmButton: "btn fw-bold btn-primary" } })
                    }))
            }))
        }))
    };

    
    const f = () => {
        const t = document.querySelector('[data-kt-projects-table-toolbar="base"]')
    };

    const getActiveProject = () => {
        $.ajax({
            url: "/Project/GetActiveProjects",
            type: "GET",
            datatype: "json",
            success: function (response) {
                if (response.isSuccess) {
                    console.log("Liste yüklendi.");
                }
            },
            error: function (err) {
                console.log("Yükleme gerçekleşmedi.");
            },
        });
    }

    const getUrl = () => {
        var url = window.location.search;
        if (url.includes("active")) {
            return "GetActiveProjects";
        }
        else {
            if (url.includes("finished")) {
                return "GetFinishedProjects";
            }
            else {
                return "GetAllProjects";
            }
        }
    }

    const loadDataTable = () => {
        return $(n).DataTable({
            ajax: {
                url: "/Project/" + u,
                type: "GET",
                datatype: "json"
            },
            "columns": [
                { "data": "projectName", "width": "15%" },
                { "data": "pafta", "width": "10%" },
                { "data": "ada", "width": "10%" },
                { "data": "parsel", "width": "10%" },
                {
                    "data": "employmentDate",
                    "render": function (data, type, row) {
                        return moment(data).format("DD-MM-YYYY");
                    },
                    "width": "12%"
                },
                {
                    "data": "commitmentDate",
                    "render": function (data, type, row) {
                        return moment(data).format("DD-MM-YYYY");
                    },
                    "width": "12%"
                },
                { "data": "blockCount", "width": "10%" },
                {
                    "data": "status",
                    "render": function (data, type, row) {
                        if (data == 0) return `<span class="badge badge-danger">Kapalı</span>`;
                        if (data == 1) return `<span class="badge badge-success">Aktif</span>`;
                        if (data == 2) return `<span class="badge badge-primary">Arşivlendi</span>`;
                        if (data == 3) return `<span class="badge badge-secondary">Silindi</span>`;
                    }, "width": "6%"
                },
                {
                    "data": "id",
                    orderable: false,
                    className: 'text-end ',
                    "render": function (data) { 
                        return `<a href="/Project/ChangeStatus/${data}" class="btn btn-icon btn-sm btn-success" title="proje durum değişikliği"><i class="fas fa-check"></i></a>
                                <a href="/Project/Detail/${data}" class="btn btn-icon btn-sm btn-primary" title="proje yönetimi"><i class="fas fa-tasks"></i></a>
                                <a href="/Project/Upsert/${data}" class="btn btn-icon btn-sm btn-warning" title="düzenle"><i class="far fa-edit"></i></a>
                                <a href="#" class="btn btn-icon btn-sm btn-danger" data-link="${data}" title="sil" data-kt-projects-table-filter="delete_row" data-bs-toggle="tooltip" data-bs-custom-class="tooltip-dark" data-bs-placement="top"><i class="far fa-trash-alt"></i></a>`;
                    }, "width": "15%"
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
            columnDefs: [{ orderable: !1, targets: 8 }],
            "width": "100%"
        });
    };

    return {
        init: function () {
            (n = document.querySelector("#kt_projects_table"), u = getUrl()) && (
               
                dt = loadDataTable().on("draw", function () { setDeleteEvents() }),
                getActiveProject(),
                document.querySelector('[data-kt-projects-table-filter="search"]').addEventListener("keyup", (function (e) {
                    dt.search(e.target.value).draw()
                }))
            )
        }
    }
}();

KTUtil.onDOMContentLoaded((function () { KTProjectList.init() }));