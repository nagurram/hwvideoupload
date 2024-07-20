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

    $(".tablecell").click(function () {
    })


});

function viewVideo(videoname) {
    console.log('in function');
    console.log('video path: ' + 'https://localhost:7132/api/Files/GetVideoByName?filename=' + videoname)
    $("#videofileurl source").attr({
        "src": 'https://localhost:7132/api/Files/GetVideoByName?filename=' + videoname,
        "autoplay": "autoplay",
    });
}


function getfilelist() {
    $.ajax({
        url: 'https://localhost:7132/api/Files/GetFileList',
        method: 'GET',
        success: function (response) {
            $('#videoTable tbody').empty();
            response.forEach(function (item) {
                
                $('#videoTable tbody').append(`
                    <tr>
                        <td><a class="tablecell" href="javascript:viewVideo('${item.key}');"> ${item.key}</a></td>
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