using Microsoft.AspNetCore.Http;

namespace AdPlatformService.Services
{
    public interface IAdPlatformService
    {
        int LoadFromFile(IFormFile file);
        List<string> FindAdPlatforms(string location);
    }
}
