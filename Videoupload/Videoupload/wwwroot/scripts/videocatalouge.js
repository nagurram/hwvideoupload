$(document).ready(function () {
    $("#btnUpload").click(function () {
        var fileInput = document.getElementById("fileupload");
        var files = fileInput.files;

        if (files.length > 0) {
            var formData = new FormData();
            for (var i = 0; i < files.length; i++) {
                formData.append("files[]", files[i]);
            }

            $.ajax({
                type: "POST", 
                url: "https://localhost:7132/api/Files/UploadVideo",
                data: formData,
                processData: false,
                contentType: false,
                success: function (response) {
                    console.log("File uploaded successfully:", response);
                },
                error: function (error) {
                    console.error("Error uploading file:", error);
                }
            });
        }
    });
});