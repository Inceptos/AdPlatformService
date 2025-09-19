namespace AdPlatform.Models
{
    class LocationNode
    {
        public Dictionary<string, LocationNode> Children = new Dictionary<string, LocationNode>();
        public HashSet<string> AdvertisingPlatforms = new HashSet<string>();
    }
}
