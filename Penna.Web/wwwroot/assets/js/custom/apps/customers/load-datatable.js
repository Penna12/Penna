var dataTable;

$(document).ready(function () {
    loadDataTable();
});


function loadDataTable() {
    dataTable = $("#kt_tenants_table").DataTable({
        "ajax": {
            "url": "/Tenants/GetAllData",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            //{
            //    "data": "id",
            //    "render": `
            //        <div class="form-check form-check-sm form-check-custom form-check-solid">
            //            <input class="form-check-input" type="checkbox" value="1" />
            //        </div>`, "width": "5%"
            //},
            { "data": "id", "width": "5%" },
            { "data": "title", "width": "15%" },
            { "data": "fullName", "width": "15%" },
            { "data": "email", "width": "10%" },
            {
                "data": "phone",
                "render": function (data, type, row) {
                    return `${row.countryDialCode}-${data}`;
                }, "width": "10%"
            },
            //{ "data": "cityName", "width": "10%" },
            //{
            //    "data": "createdDate",
            //    "render": function (data) {
            //        return moment(`${data}`).format("DD.MM.YYYY hh:mm:ss");
            //    }, "width": "15%"
            //},
            {
                "data": "isActive",
                "render": function (data) {
                    if (data == true) return `<span class="badge badge-success">Aktif</span>`;
                    if (data == false) return `<span class="badge badge-danger">Pasif</span>`;
                }, "width": "10%"
            },
            {
                "data": "id",
                "render": function (data) {
                    return `<a href="#" class="btn btn-sm btn-light btn-active-light-primary" data-kt-menu-trigger="click" data-kt-menu-placement="bottom-end" data-kt-menu-flip="top-end">
                            İşlemler
                            <span class="svg-icon svg-icon-5 m-0">
                                <svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" width="24px" height="24px" viewBox="0 0 24 24" version="1.1">
                                    <g stroke="none" stroke-width="1" fill="none" fill-rule="evenodd">
                                        <polygon points="0 0 24 0 24 24 0 24" />
                                        <path d="M6.70710678,15.7071068 C6.31658249,16.0976311 5.68341751,16.0976311 5.29289322,15.7071068 C4.90236893,15.3165825 4.90236893,14.6834175 5.29289322,14.2928932 L11.2928932,8.29289322 C11.6714722,7.91431428 12.2810586,7.90106866 12.6757246,8.26284586 L18.6757246,13.7628459 C19.0828436,14.1360383 19.1103465,14.7686056 18.7371541,15.1757246 C18.3639617,15.5828436 17.7313944,15.6103465 17.3242754,15.2371541 L12.0300757,10.3841378 L6.70710678,15.7071068 Z" fill="#000000" fill-rule="nonzero" transform="translate(12.000003, 11.999999) rotate(-180.000000) translate(-12.000003, -11.999999)" />
                                    </g>
                                </svg>
                            </span>
                        </a>
                        <div class="menu menu-sub menu-sub-dropdown menu-column menu-rounded menu-gray-600 menu-state-bg-light-primary fw-bold fs-7 w-125px py-4" data-kt-menu="true">
                            <div class="menu-item px-3">
                                <a href="/Tenants/Detail/${data}" class="menu-link px-3">İncele</a>
                            </div>
                            <div class="menu-item px-3">
                                <a href="#" class="menu-link px-3" data-kt-customer-table-filter="delete_row">Sil</a>
                            </div>
                        </div>`;
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
        "width": "100%"
    });
}

