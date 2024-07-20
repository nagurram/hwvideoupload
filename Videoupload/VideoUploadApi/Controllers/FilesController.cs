using Microsoft.AspNetCore.Mvc;
using VideoUploadApi.UIModels;

namespace VideoUploadApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        long maxsize;
        public FilesController() {

            maxsize = 200000;
        }



        [HttpPost]
        [Route("UploadVideo")]
        [RequestFormLimits(ValueLengthLimit = int.MaxValue, MultipartBodyLengthLimit = int.MaxValue)]
        public IActionResult UploadVideo()
        {
            if (!Request.Form.Files.Any())
                return Ok("No files to process"); // No files to process

            
            foreach (var file in Request.Form.Files)
            {
                if (file.ContentType != "video/mp4")
                {
                    return BadRequest("Only MP4 files are allowed.");
                }
                //if any file is largerthan 300MB
                if (file.Length.ConvertToKB() > maxsize)
                {
                    return StatusCode(StatusCodes.Status413RequestEntityTooLarge, "File size exceeds the allowed limit.");
                }

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
            List<Keyvalue> files = new List<Keyvalue>();
            var filesInUploads = Directory.GetFiles(Path.Combine(Directory.GetCurrentDirectory(), "Uploads"),"*.mp4");
            foreach (var filePath in filesInUploads)
            {
                var fileSize = new FileInfo(filePath).Length;
                var fileSizeInKB = fileSize.ConvertToKB();
                files.Add(new Keyvalue() { key = Path.GetFileName(filePath), value = Convert.ToString(fileSizeInKB) });
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


