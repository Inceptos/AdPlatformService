using AdPlatformService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace AdPlatformService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdPlatformController : ControllerBase
    {
        //private readonly ILogger<AdPlatformController> _logger;

        //public AdPlatformController(ILogger<AdPlatformController> logger)
        //{
        //    _logger = logger;
        //}

        private readonly IAdPlatformService _adService;

        public AdPlatformController(IAdPlatformService adService)
        {
            _adService = adService;            
        }


        [HttpPost("update")]
        public ActionResult UploadFile([FromForm] IFormFile? file)
        {
            return Ok(new {countPlatforms = _adService.LoadFromFile(file)});
        }

        [HttpGet("search/{*location}")]
        public ActionResult Search(string location)
        {
            location = "/" + location;
            return Ok(_adService.FindAdPlatforms(location));
        }
    }
}
