$(document).ready(function () {
    getfilelist();
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
                    $('#pills-Catalouge-tab').click();
                },
                error: function (error) {
                    console.error("Error uploading file:", error);
                }
            });
        }
    });

    $("#btnUpload").click(function () {
        getfilelist();
    });

    function getfilelist() {
        $.ajax({
            url: 'https://localhost:7132/api/Files/GetFileList',
            method: 'GET',
            success: function (response) {
                response.forEach(function (item) {
                    $('#videoTable tbody').append(`
                    <tr>
                        <td>${item.key}</td>
                        <td>${item.value}</td>
                    </tr>
                `);
                });
            },
            error: function (error) {
                console.error('Error fetching data:', error);
            }
        });
    }
});