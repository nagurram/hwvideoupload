using Microsoft.AspNetCore.Mvc;

namespace VideoUploadApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        [HttpPost]
        [Route("UploadVideo")]
        public IActionResult UploadVideo()
        {
            if (!Request.Form.Files.Any())
                return Ok("No files to process"); // No files to process

            // Handle each uploaded file
            foreach (var file in Request.Form.Files)
            {
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
                if (!Directory.Exists(pathToSave))
                    Directory.CreateDirectory(pathToSave);

                var fullPath = Path.Combine(pathToSave, file.FileName);
                using var stream = new FileStream(fullPath, FileMode.Create);
                file.CopyTo(stream);
            }

            return Ok("Files uploaded successfully!");
        }

        [HttpGet]
        [Route("GetFileList")]
        public IActionResult GetFileList()
        {
            Dictionary<string, long> files = new Dictionary<string, long>();
            var filesInUploads = Directory.GetFiles(Path.Combine(Directory.GetCurrentDirectory(), "Uploads"));
            foreach (var filePath in filesInUploads)
            {
                var fileSize = new FileInfo(filePath).Length;
                var fileSizeInKB = Convert.ToInt64(fileSize/1024); // Convert to KB
                files.Add(Path.GetFileName(filePath), fileSizeInKB);
            }
            return Ok(files);
        }

        [HttpGet]
        [Route("GetVideoByName")]
        public IActionResult GetVideoByName(string filename)
        {
            string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
            string filePath = Path.Combine(uploadsFolder, filename);
            if (System.IO.File.Exists(filePath))
            {
                var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                return new FileStreamResult(stream, "video/mp4");
            }
            else
            {
                return NotFound();
            }

        }
    }
}


