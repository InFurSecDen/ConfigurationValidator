using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace InFurSecDen.Utils.ConfigurationValidator
{
    public static class ConfigurationValidator
    {
        public static ValidationErrors Validate(IConfiguration configuration, string schemaJson, bool throwOnError = false)
        {
            var schema = GetConfigurationSchema(schemaJson); // TODO: Get an IConfigurationSchema instead of a string, expose a GetSchema() method or something
            var validationErrors = new ValidationErrors();

            // Add additional rules to this list:
            validationErrors += CheckRequiredValuesArePresent(schema, configuration);

            if (throwOnError && validationErrors.Count > 0)
            {
                throw validationErrors.AsException();
            }

            return validationErrors;
        }

        private static ValidationErrors CheckRequiredValuesArePresent(Dictionary<string, ConfigurationPropertyRules> schema, IConfiguration configuration)
        {
            var requiredItemsValidationError = new ValidationErrors();

            foreach (var schemaItem in schema)
            {
                if (schemaItem.Value.Required.HasValue && // Schema has "required" property
                    schemaItem.Value.Required.Value && // Schema "required" property is "true"
                    configuration[schemaItem.Key] == null) // Configuration value is null
                {
                    requiredItemsValidationError.Add(new ValidationError
                    {
                        Key = schemaItem.Key,
                        Value = null,
                        Type = ValidationErrorType.RequiredPropertyIsNull,
                    });
                }
            }

            return requiredItemsValidationError;
        }

        private static Dictionary<string, ConfigurationPropertyRules> GetConfigurationSchema(string json)
        {
            return JsonConvert.DeserializeObject<Dictionary<string, ConfigurationPropertyRules>>(json);
        }
    }
}
