//using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceChat.Model;
using System;
using System.IO;
using System.Net.Http.Headers;
//using RouteAttribute = Microsoft.AspNetCore.Components.RouteAttribute;

namespace ServiceChat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        [HttpPost]
        public ActionResult Post([FromForm] IFormFile formFile, [FromForm] int roomId)
        {
            try
            {
                if (formFile.Length > 0)
                {
                    var randomTexto = Guid.NewGuid().ToString();
                    var nomeFicheiro = roomId + "/" + randomTexto + Path.GetExtension(formFile.FileName);
                    string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", nomeFicheiro);
                    
                    string diretorio = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", roomId.ToString());

                    if (!Directory.Exists(diretorio))
                    {
                        DirectoryInfo di = Directory.CreateDirectory(diretorio);
                    }
                       
                    var fileName = ContentDispositionHeaderValue.Parse(formFile.ContentDisposition).FileName.Trim('"');
                    using (Stream stream = new FileStream(path, FileMode.Create))

                    {
                        formFile.CopyTo(stream);
                    }
                    return Ok(nomeFicheiro);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

    }
}
