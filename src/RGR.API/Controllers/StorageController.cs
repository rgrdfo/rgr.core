using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using RGR.API.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using System.IO;
using RGR.Core.Models;
using Microsoft.AspNetCore.Authorization;

namespace RGR.API.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class StorageController : Controller
    {
        private IHostingEnvironment env;
        private rgrContext db;

        public StorageController(IHostingEnvironment env, rgrContext db)
        {
            this.db = db;
            this.env = env;
        }

        [HttpPost]
        [Route("upload")]
        public async Task<IActionResult> UploadMedia ([FromBody] IEnumerable<IFormFile> Files)
        {
            foreach (var file in Files)
            {
                var uploaded = await TryUpload(file, FilePurpose.ObjectPhoto);
                if (!uploaded)
                    return new StatusCodeResult(422);
            }

            return new OkResult();
        }

        private async Task<bool> TryUpload(IFormFile File, FilePurpose Purpose, long TargetEstateId = -1)
        {
            var filename = ContentDispositionHeaderValue
                            .Parse(File.ContentDisposition)
                            .FileName
                            .Trim('"');

            string path = Path.Combine(env.WebRootPath, "images", Purpose.ToString(), filename);
            var size = File.Length;

            using (FileStream fs = System.IO.File.Create(path))
            {
                File.CopyTo(fs);
                await fs.FlushAsync();
            }

            var parts = filename.Split('.');
            var ext = (parts.Length > 1) ? parts.Last() : "";
            var name = Path.GetRandomFileName();

            var mime = "";

            switch(ext.ToUpper())
            {
                case "JPG": case "JPEG":
                    mime = "image/jpeg";
                    break;

                case "PNG":
                    mime = "image/png";
                    break;

                default:
                    mime = "file/unknown";
                    break;
            }

            try
            {
                var storageEntry = new StoredFiles();
                storageEntry.ContentSize = size;
                storageEntry.DateCreated = DateTime.UtcNow;
                storageEntry.CreatedBy = HttpContext.Session.GetUserId();
                storageEntry.MimeType = mime;
                storageEntry.OriginalFilename = filename;
                storageEntry.ServerFilename = $"{Purpose}\\{name}";
                db.StoredFiles.Add(storageEntry);

                await db.SaveChangesAsync();

                if (TargetEstateId > 0)
                {
                    var media = new ObjectMedias();
                    media.ObjectId = TargetEstateId;
                    media.MediaType = (storageEntry.MimeType.StartsWith("image")) ? (short)1 : (short)0;
                    media.MediaUrl = $"file://{storageEntry.Id}";
                    media.DateCreated = DateTime.UtcNow;
                    media.CreatedBy = HttpContext.Session.GetUserId();
                    await db.SaveChangesAsync();
                }
            }
            catch(Exception e)
            {
                return false;
            }
            
            return true;
        }

    }
}
