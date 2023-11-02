using DAO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PruebaAPI.Services;

namespace PruebaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacturaController : ControllerBase
    {
        private IFacturaSvc _crudService;

        public FacturaController(IFacturaSvc crudService)
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
        [AllowAnonymous]
        [HttpGet("GetAllByCliente/{id}/{numero}")]
        public IActionResult GetAllByCliente(int id,int numero)
        {
            var result = _crudService.GetAll(id,numero);
            return Ok(result);
        }
        [AllowAnonymous]
        [HttpGet("GetNewInstance")]
        public IActionResult GetNewInstance()
        {
            var result = new Factura()
            {
                fh_Factura=DateTime.Now,
                Id=0,
                id_Cliente=0,
                nu_Factura=0,
                nu_Articulos=0,
                detalles = new List<DAO.FacturaDetalle>(),
                imp_Subtotal=0,
                imp_Total=0,
                imp_TotalImpuestos=0,
            };
            return Ok(result);
        }

        [HttpGet("Get/{id}")]
        public IActionResult Get(int id)
        {
            var result = _crudService.Get(id);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult UpSert([FromBody] Factura data)
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
