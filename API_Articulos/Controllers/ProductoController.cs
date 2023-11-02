using DAO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PruebaAPI.Services;

namespace PruebaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private IProductoSvc _crudService;

        public ProductoController(IProductoSvc crudService)
        {
            _crudService = crudService;
        }

        [AllowAnonymous]
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var result = _crudService.GetAll();
            return Ok(result);
        }

        [HttpGet("Get/{id}")]
        public IActionResult Get(int id)
        {
            var result = _crudService.Get(id);
            return Ok(result);
        }
        [HttpPost("Upload"), DisableRequestSizeLimit]
        public async Task<IActionResult> Upload()
        {
            try
            {
                var producto = JsonConvert.DeserializeObject<Producto>(Request.Form["producto"].ToString());
                var fileForm = Request.Form.Files[0];

                if (fileForm.Length > 0)
                {
                    using var dataStream = new MemoryStream();
                    await fileForm.CopyToAsync(dataStream);
                    byte[] imageBytes = dataStream.ToArray();

                    producto.img_Producto = imageBytes; // Asignando directamente la imagen como byte[]

                    var response = _crudService.UpSert(producto);
                    if (response.Exception != null) return Ok(new { Message = response.Exception.InnerException.Message });
                    return Ok(response);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        [HttpPost]
        public IActionResult UpSert([FromBody] Producto data)
        {
            try
            {
                var response = _crudService.UpSert(data);
                if (response.Exception != null) return Ok(new { Message = response.Exception.InnerException.Message });
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(new { Message = ex.Message });
            }
        }

        [HttpDelete("Delete/{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var response = _crudService.Delete(id);
                if (response.Exception != null) return Ok(new { Message = response.Exception.InnerException.Message });
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(new { Message = ex.Message });
            }
        }
    }

}
