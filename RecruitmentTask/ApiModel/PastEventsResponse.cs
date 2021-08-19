using Newtonsoft.Json;

namespace RecruitmentTask
{

    public class PastEvent
    {
        [JsonProperty("ID")]
        public int Id { get; set; }
        [JsonProperty("post_author")]
        public string PostAuthor { get; set; }
        [JsonProperty("post_date")]
        public string PostDate { get; set; }
        [JsonProperty("post_title")]
        public string PostTitle { get; set; }
        [JsonProperty("acf")]
        public Acf Acf { get; set; }
        [JsonProperty("permalink")]
        public string Permalink { get; set; }
    }

    public class Acf
    {
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("location")]
        public string Location { get; set; }
        [JsonProperty("location_city")]
        public string LocationCity { get; set; }
        [JsonProperty("starts_at")]
        public string StartsAt { get; set; }
        [JsonProperty("link")]
        public string Link { get; set; }
        [JsonProperty("navbar_theme")]
        public string NavBarTheme { get; set; }
    }
}
