using Newtonsoft.Json;

namespace InFurSecDen.Utils.ConfigurationValidator
{
    internal class ConfigurationPropertyRules
    {
        [JsonProperty(PropertyName = "required")]
        internal bool? Required { get; set; }
    }
}
