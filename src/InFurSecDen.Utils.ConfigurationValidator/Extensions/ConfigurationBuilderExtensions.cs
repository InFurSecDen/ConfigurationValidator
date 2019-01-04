using System;
using InFurSecDen.Utils.ConfigurationValidator;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.Configuration
{
    public static class ConfigurationBuilderExtensions
    {
        public static ValidationErrors Validate(this IConfigurationBuilder configurationBuilder, string schemaJson, bool throwOnError = false)
        {
            return ConfigurationValidator.Validate(configurationBuilder.Build(), schemaJson, throwOnError);
        }
    }
}
