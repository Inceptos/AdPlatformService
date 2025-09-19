using AdPlatform.Services;
using Microsoft.AspNetCore.Mvc;

namespace AdPlatform.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdPlatformController : ControllerBase
    {
        private readonly ILogger<AdPlatformController> _logger;
        private readonly IAdPlatformService _adService;

        public AdPlatformController(ILogger<AdPlatformController> logger, IAdPlatformService adService)
        {
            _logger = logger;
            _adService = adService;
        }

        [HttpPost("update")]
        public ActionResult UploadFile([FromForm] IFormFile? file)
        {
            try
            {
                int count = _adService.LoadFromFile(file);
                return Ok(new { countPlatforms = count });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ќшибка при загрузке файла");
                return StatusCode(500, "¬нутренн€€ ошибка сервера");
            }
        }

        [HttpGet("search/{*location}")]
        public ActionResult Search(string location)
        {
            try
            {
                location = "/" + location;
                var platforms = _adService.FindAdPlatforms(location);
                return Ok(platforms);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ќшибка при поиске по локации");
                return StatusCode(500, "¬нутренн€€ ошибка сервера");
            }
        }
    }
}
