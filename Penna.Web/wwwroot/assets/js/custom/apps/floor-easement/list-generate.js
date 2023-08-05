"use strict";
var KTFloorEasementList = function () {
    var dt;


    const loadDataTable = () => {
        return $(dt).DataTable({
            "ajax": {
                "url": "/FloorEasement/GetAllData",
                "type": "GET",
                "datatype": "json"
            },
            "columns": [
                { "data": "floor", "width": "15%" },
                { "data": "sectionName", "width": "30%" },
                { "data": "gross", "width": "15%" },
                { "data": "net", "width": "15%" },
                { "data": "gabari", "width": "15%" }
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
    };

    return {
        init: function () {
            (dt = document.querySelector("#kt_floor_table")) && (

                dt = loadDataTable().on("draw", function () {  })

            )
        }
    }
}();

KTUtil.onDOMContentLoaded((function () { KTFloorEasementList.init() }));