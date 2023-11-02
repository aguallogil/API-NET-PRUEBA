using PruebaAPI.Services;
using DAO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PruebaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private IClienteSvc _crudService;

        public ClienteController(IClienteSvc crudService)
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

        [HttpPost]
        public IActionResult UpSert([FromBody] Cliente data)
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
