var dataTable;

async function loadDataTable() {
    dataTable = $("#kt_tender_table").DataTable({
        "ajax": {
            "url": "/Purchase/GetTenderList",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            {
                "data": "purchaseDate",
                "render": function (data) {
                    return moment(`${data}`).format("DD.MM.YYYY hh:mm:ss");
                }, "width": "15%"
            },
            { "data": "product.name", "width": "15%" },
            { "data": "quantity", "width": "8%" },
            {
                "data": "requestedDeliveryDate",
                "render": function (data) {
                    return moment(`${data}`).format("DD.MM.YYYY hh:mm:ss");
                }, "width": "15%"
            },
            {
                "data": "finalBidDateTime",
                "render": function (data) {
                    return moment(`${data}`).format("DD.MM.YYYY hh:mm:ss");
                }, "width": "15%"
            },
            { "data": "description", "width": "15%" },
            {
                "data": "purchaseRequest.priority",
                "render": function (data) {
                    if (data == 1) return `<span class="badge badge-success">Düşük</span>`;
                    if (data == 2) return `<span class="badge badge-primary">Normal</span>`;
                    if (data == 3) return `<span class="badge badge-warning">Yüksek</span>`;
                    if (data == 4) return `<span class="badge badge-danger">Acil</span>`;
                }, "width": "10%"
            },
            {
                "data": "id",
                "render": function (data) {
                    return `<a href="/Purchase/Tender/${data}" class="btn btn-icon btn-sm btn-primary" data-bs-toggle="tooltip" data-bs-custom-class="tooltip-dark" data-bs-placement="left" title="ihale izle"><i class="fas fa-info"></i></a>`;
                }, "width": "7%"
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
        columnDefs: [{ orderable: !1, targets: 6 }],
        "width": "100%"
    });
}


$(document).ready(async function () {

    //await loadDataTable(); //otomatik oluşturma için burayı açabiliriz. ozaman alt satırı kapatırız. tooltip title görünsün diye manuel oluşturdum
    dataTable = $("#kt_tender_table").DataTable({ "pageLength": 10, info: false, language: { sLengthMenu: "Sayfada _MENU_ kayıt göster", } }); // info:false, page:false, "lengthMenu": [[10, 25, 50, -1], [10, 25, 50, "All"]]

    // filtreleme triggeri
    document.querySelector('[data-kt-pending-offer-table-filter="search"]').addEventListener("keyup", (function (e) {
        dataTable.search(e.target.value).draw()
    }));

    // sayfalama triggeri
    document.querySelector('[name="kt_datatable_tender_length"]').addEventListener("change", (function (e) {
        dataTable.page.len(this.value).draw();
    }));
});




