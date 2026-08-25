#nullable disable

using System.Text.Json.Serialization;

namespace WPILibInstaller.Models
{
    public class FileAssociation
    {
        [JsonPropertyName("ext")]
        public string Ext { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("mimeType")]
        public string MimeType { get; set; }

        [JsonPropertyName("icon")]
        public string Icon { get; set; }
    }
}
