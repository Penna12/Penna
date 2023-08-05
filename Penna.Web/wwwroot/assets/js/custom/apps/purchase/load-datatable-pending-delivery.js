var dataTable;

async function loadDataTable() {
    dataTable = $("#kt_tender_table").DataTable({
        "ajax": {
            "url": "/Purchase/GetPendingDeliveryList",
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
            { "data": "description", "width": "15%" }
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
        "width": "100%"
    });
}


$(document).ready(async function () {

    //await loadDataTable(); //otomatik oluşturma için burayı açabiliriz. ozaman alt satırı kapatırız. tooltip title görünsün diye manuel oluşturdum
    dataTable = $("#kt_pending_delivery_table").DataTable({ "pageLength": 10, info: false, language: { sLengthMenu: "Sayfada _MENU_ kayıt göster", } }); // info:false, page:false, "lengthMenu": [[10, 25, 50, -1], [10, 25, 50, "All"]]

    // filtreleme triggeri
    document.querySelector('[data-kt-pending-delivery-table-filter="search"]').addEventListener("keyup", (function (e) {
        dataTable.search(e.target.value).draw()
    }));

    // sayfalama triggeri
    document.querySelector('[name="kt_datatable_pending_delivery_length"]').addEventListener("change", (function (e) {
        dataTable.page.len(this.value).draw();
    }));
});




