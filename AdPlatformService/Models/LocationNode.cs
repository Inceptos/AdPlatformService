namespace AdPlatformService.Models
{
    public class LocationNode
    {
        public HashSet<string> AdPlatforms { get; set; } = new();
        public Dictionary<string, LocationNode> Children { get; set; } = new();
    }
}
