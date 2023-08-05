"use strict";
var KTFileUpload = function () {
    var s, f, v, mim, sta, mek, elk, pey, mes;
    let res;

    var send = () => {
        return $.ajax({
            url: "/Explorer/Upload",
            type: "post",
            data: $('#fileUploadForm').serialize()
        });
    };

    let uploadFilesAsync = async () => {
        let formData = new FormData()
        formData.append("Mimari", mim.files[0])
        formData.append("Statik", sta.files[0])
        formData.append("Mekanik", mek.files[0])
        formData.append("Elektrik", elk.files[0])
        formData.append("Peyzaj", pey.files[0])
        formData.append("Message", mes.value)
        formData.append("RealUrl", document.querySelector('[name="RealUrl"]').value)
        formData.append("URL", document.querySelector('[name="URL"]').value)
        let response = await fetch('/Explorer/Upload/', {
            method: "POST",
            body: formData
        })
        return response.json()
    }

    var messageCheck = (ac) => {
        return new Promise((resolve, reject) => {
            if (ac && mes.value == "") {
                Swal.fire({ title: "Mesaj bulunamadı!", text: "Lütfen yüklenen dosya(lar) hakkında bildirim mesajınızı yazınız.", icon: "warning", buttonsStyling: !1, confirmButtonText: "Tamam", customClass: { confirmButton: "btn btn-primary" } })
                reject
            }
            else
                resolve(true)
        })
    }

    var fileUploadCheck = () => {
        return new Promise((resolve, reject) => {
            if (mim.value == "" && sta.value == "" && mek.value == "" && elk.value == "" && pey.value == "") {
                Swal.fire({ title: "Yüklenecek dosya bulunamadı!", text: "Lütfen enaz bir dosya yükleyiniz.", icon: "warning", buttonsStyling: !1, confirmButtonText: "Tamam", customClass: { confirmButton: "btn btn-primary" } })
                reject
            }
            else
                resolve(true)
        })
    }

    var projectActiveCheck = () => {
        return new Promise((resolve, reject) => {
            // proje aktif edilmişmi check edecek.
            $.getJSON({
                url: `/Explorer/ProjectActiveCheck/`,
                success: resolve,
                error: reject
            })
        })
    }

    var validateCheck = async () => {
        //proje aktif edilmiş ise mesaj alanı ve en az bir dosya yükleme zorunlu olacak.
        const acCheck = await projectActiveCheck()
        const fuCheck = await fileUploadCheck()
        const mesCheck = await messageCheck(acCheck.success)
    }


    return {
        init: function () {
            (f = document.querySelector("#fileUploadForm"),
                mim = f.querySelector('[name="Mimari"]'),
                sta = f.querySelector('[name="Statik"]'),
                mek = f.querySelector('[name="Mekanik"]'),
                elk = f.querySelector('[name="Elektrik"]'),
                pey = f.querySelector('[name="Peyzaj"]'),
                mes = f.querySelector('[name="Message"]'),
                s = f.querySelector("#sendMessageSubmit")

            ) && (
                    s.addEventListener("click", (async function (e) {
                        e.preventDefault()
                        try {
                            await validateCheck()
                            res = await uploadFilesAsync()
                            if (res.success) {
                                Swal.fire({ text: "Form basariyla gonderildi!", icon: "success", buttonsStyling: !1, confirmButtonText: "Tamam", customClass: { confirmButton: "btn btn-primary" } })
                                    .then(function (e) { location.reload() })
                            } else {
                                Swal.fire({ title: "Kayıt gerçekleşmedi!", text: res.message, icon: "warning", buttonsStyling: !1, confirmButtonText: "Tamam", customClass: { confirmButton: "btn btn-primary" } })
                            }
                        } catch (e) {
                            Swal.fire({ title: "Bir hata oldu!", text: `${e.status}-${e.statusText}`, icon: "warning", buttonsStyling: !1, confirmButtonText: "Tamam", customClass: { confirmButton: "btn btn-primary" } })
                        }
                        //e.preventDefault() // Bunu başa koyunca serialize file upload null gönderiyor, formdata çalışıyor
                    }))
                )
        }
    }
}();

KTUtil.onDOMContentLoaded((function () { KTFileUpload.init() }));