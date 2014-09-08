using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Hosting;
using System.Web.Http;
namespace RestFulNetDroid.Controllers
{
    public class imageController : ApiController
    {
        // GET: api/image
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/image/5
        // GET: api/Images/5

        public HttpResponseMessage Get(string id)
        {
            var result = new HttpResponseMessage(HttpStatusCode.OK);
            String filePath = HostingEnvironment.MapPath("~/Images/" + id + ".jpg");
            FileStream fileStream = new FileStream(filePath, FileMode.Open);
            Image image = Image.FromStream(fileStream);
            MemoryStream memoryStream = new MemoryStream();
            image.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Jpeg);
            result.Content = new ByteArrayContent(memoryStream.ToArray());
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");

            return result;
        }

       
        // POST: api/image
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/image/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/image/5
        public void Delete(int id)
        {
        }
    }
}
