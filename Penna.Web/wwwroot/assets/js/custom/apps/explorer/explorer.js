$(document).ready(function () {
    $(".alert").hide();

    $(document).on("click", ".delete-file", function (e) {
        debugger
        var element = $(this);
        var realUrl = element.data("realurl");
        var fileName = element.data("name");
        var postData = { realUrl: realUrl, fileName: fileName };
        $.ajax({
            url: "/Explorer/DeleteFile",
            type: "POST",
            data: postData,
            success: function (response) {
                console.log(response);
                if (response.success) {
                    window.location.reload();
                }
            },
            error: function (err) {
                console.log(err);
            }
        });
    });

    $(document).on("click", ".delete-folder", function (e) {
        debugger
        var element = $(this);
        var realUrl = element.data("realurl");
        var folderName = element.data("name");
        var postData = { realUrl: realUrl, folderName: folderName };
        $.ajax({
            url: "/Explorer/DeleteFolder",
            type: "POST",
            data: postData,
            success: function (response) {
                console.log(response);
                if (response.success) {
                    window.location.reload();
                } else {
                    $("#msgTitle").html("Hata!");
                    $("#message").html(response.message);
                    $(".alert").fadeIn();
                }
            },
            error: function (err) {
                console.log(err);
            }
        });
    });

    $(document).on("click", ".create-new-folder", function (e) {
        debugger
        var element = $(this);
        var realUrl = element.data("realurl");
        var folderName = $("#new-folder-name").val();
        var postData = { realUrl: realUrl, folderName: folderName };
        $.ajax({
            url: "/Explorer/CreateFolder",
            type: "POST",
            data: postData,
            success: function (response) {
                console.log(response);
                if (response.success) {
                    window.location.reload();
                }
            },
            error: function (err) {
                console.log(err);
            }
        });
    });

});