
using System.Text;

namespace AdPlatformService.Services
{
    public record AdPlatform(string Name, List<string> Locations);
    public class AdPlatformService : IAdPlatformService
    {
        private readonly List<AdPlatform> _ads = new();
        
        public List<string> FindAdPlatforms(string location)
        {
            var platforms = new List<string>();
            do
            {
                var lastIndex = location.LastIndexOf('/');
                platforms.AddRange(_ads.Where(x => x.Locations.Contains(location)).Select(x => x.Name));
                location = location.Remove(lastIndex, location.Length - lastIndex);
            }
            while (location.LastIndexOf('/') >= 0);
            return platforms;
        }

        public int LoadFromFile(IFormFile file)
        {
            _ads.Clear();
            using (var reader = new StreamReader(file.OpenReadStream(), Encoding.UTF8))
            {
                string line;

                while ((line = reader.ReadLine()) != null)
                {
                    var splitLine = line.Split(':');
                    var namePlatform = splitLine[0].Trim();
                    var locations = splitLine[1].Split(',')
                        .Select(x => x.Trim()).ToList();

                    _ads.Add(new AdPlatform(namePlatform, locations));
                }
            }

            return _ads.Count;
        }
    }
}
