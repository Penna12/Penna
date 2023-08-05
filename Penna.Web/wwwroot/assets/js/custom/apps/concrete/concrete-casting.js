"use strict";
var KTConcreteCastingAdd = function () {
    var f, q, t, r;

    return {
        init: function () {
            (//Inputmask({ "mask": "99 ### 9999" }).mask("#ConcreteCasting_CarPlate"),
                f = document.querySelector("#add_concrete_casting_form"),
                q = f.querySelector("#ConcreteCasting_Quantity"),
                t = f.querySelector("#TotalCasting"),
                r = f.querySelector("#requestedQuantity")) && (

                q.addEventListener("keyup", (function (e) {
                    if (parseInt(q.value) > parseInt(r.value)) {
                        return false
                    }
                    t.value = parseInt(r.value) - parseInt(q.value)
                }))
            )
        }
    }
}();

KTUtil.onDOMContentLoaded((function () { KTConcreteCastingAdd.init() }));