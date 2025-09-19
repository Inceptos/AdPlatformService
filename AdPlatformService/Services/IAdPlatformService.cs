using Microsoft.AspNetCore.Http;

namespace AdPlatform.Services
{
    public interface IAdPlatformService
    {
        int LoadFromFile(IFormFile file);
        List<string> FindAdPlatforms(string location);
    }
}
