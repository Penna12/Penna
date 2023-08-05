"use strict";
var KTProjectActivate = function () {
    var c,d;

    var postData = (u, d) => {
        return $.ajax({
            url: u,
            type: 'POST',
            data: d
        });
    }


    return {
        init: function () {
            c = document.querySelector('[name="status"]'),
                d= document.querySelector('[name="delStatus"]'),
                $(c).on("change", function (e) {
                    postData('/Project/SetChangeStatus', { id: this.dataset["id"], status: this.value})
                        .then(data => {
                            console.log(data)
                            if (data.success) {
                                window.location.reload()
                            }
                        })
                }),
                $(d).on("change", function (e) {
                    postData('/Project/SetChangeStatus', { id: this.dataset["id"], status: this.value })
                        .then(data => {
                            console.log(data)
                            if (data.success) {
                                window.location.reload()
                            }
                        })
                })
        }
    }

}();

KTUtil.onDOMContentLoaded((function () { KTProjectActivate.init() }));
