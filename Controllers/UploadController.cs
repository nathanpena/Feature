using Features.Common;
using Features.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Features.Controllers
{

    [RoutePrefix("Upload")]
    public class UploadController : ApiController
    {
        //IUtils _utils;
        private UTRGVAppContext db = new UTRGVAppContext();

        //public UploadController(IUtils utils)
        //{
        //    _utils = utils;
        //}
        [Authorize]
        [HttpPost]
        public async Task<List<Models.File>> PostAsync()
        {
            
            if (Request.Content.IsMimeMultipartContent())
            {
                string uploadPath = HttpContext.Current.Server.MapPath("~/uploads");

                

                MyStreamProvider streamProvider = new MyStreamProvider(uploadPath);

                await Request.Content.ReadAsMultipartAsync(streamProvider);
                var user = await db.Users.Where(u => u.Cn == User.Identity.Name).FirstOrDefaultAsync();
                List<string> messages = new List<string>();
                List<Models.File> files = new List<Models.File>();
                foreach (var file in streamProvider.FileData)
                {
                    
                    FileInfo fi = new FileInfo(file.LocalFileName);
                    var modelFile = new Models.File() { Name = fi.Name , User = user };
                    messages.Add("File uploaded as " + fi.FullName + " (" + fi.Length + " bytes)");
                    files.Add(modelFile);
                }

                db.Files.AddRange(files);
                await db.SaveChangesAsync();

                return files;
            }
            else
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid Request!");
                throw new HttpResponseException(response);
            }
        }
    }
    public class MyStreamProvider : MultipartFormDataStreamProvider
    {
        public MyStreamProvider(string uploadPath)
            : base(uploadPath)
        {

        }

        public override string GetLocalFileName(HttpContentHeaders headers)
        {
            
            string fileName = headers.ContentDisposition.FileName;
            if (string.IsNullOrWhiteSpace(fileName))
            {
                fileName = Guid.NewGuid().ToString() + ".data";
            }else
            {
                var extension = fileName.Split('.')[fileName.Split('.').Length - 1];
                fileName = Guid.NewGuid().ToString() + "."+ extension;
            }
            

            return fileName.Replace("\"", string.Empty);
        }
    }
}
