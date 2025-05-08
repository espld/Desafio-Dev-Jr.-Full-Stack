using AnalisisValoresApi.Models;
using AnalisisValoresApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AnalisisValoresApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnalisisController : ControllerBase
    {
        private readonly AnalisisService _analisisService;

        public AnalisisController(AnalisisService analisisService)
        {
            _analisisService = analisisService;
        }

        [HttpPost]
        public IActionResult PostAnalisis(Analisis analisis)
        {
            
            var resultado = _analisisService.Analizar(analisis);
            
            return Ok(resultado);
        }
    }
}
