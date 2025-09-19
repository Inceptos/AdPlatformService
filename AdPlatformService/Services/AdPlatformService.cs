using AdPlatform.Models;

namespace AdPlatform.Services
{
    public class AdPlatformService : IAdPlatformService
    {
        private LocationNode root = new LocationNode();

        public int LoadFromFile(IFormFile file)
        {
            root = new LocationNode();

            using var stream = file.OpenReadStream();
            using var reader = new StreamReader(stream);

            var uniquePlatforms = new HashSet<string>();

            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                
                if (string.IsNullOrWhiteSpace(line)) continue;

                var parts = line.Split( ':' , 2);
                if (parts.Length < 2) continue;

                var platform = parts[0].Trim();
                var locationsString = parts[1];
                var locations = locationsString.Split(',');

                foreach (var loc in locations)
                {
                    AddPlatformToLocation(loc.Trim(), platform);
                }

                uniquePlatforms.Add(platform);
            }

            return uniquePlatforms.Count;            
        }

        private void AddPlatformToLocation(string location, string platform)
        {
            var segments = location.Split('/');
            var current = root;

            foreach (var seg in segments)
            {
                if (!current.Children.ContainsKey(seg))
                    current.Children[seg] = new LocationNode();

                current = current.Children[seg];
            }
            current.AdvertisingPlatforms.Add(platform);
        }

        public List<string> FindAdPlatforms(string location)
        {
            var segments = location.Split('/');
            var current = root;
            var result = new HashSet<string>();

            result.UnionWith(current.AdvertisingPlatforms);
            foreach (var seg in segments)
            {
                if (!current.Children.ContainsKey(seg))
                    break;
                current = current.Children[seg];
                result.UnionWith(current.AdvertisingPlatforms);
            }

            return result.ToList();
        }
    }
}
