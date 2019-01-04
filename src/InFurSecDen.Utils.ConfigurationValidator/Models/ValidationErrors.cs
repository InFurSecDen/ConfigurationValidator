using System;
using System.Collections.Generic;

namespace InFurSecDen.Utils.ConfigurationValidator
{
    public class ValidationErrors : List<ValidationError>
    {
        internal ValidationErrors()
        {
        }

        public static ValidationErrors operator+ (ValidationErrors originalList, ValidationErrors listToAdd)
        {
            var newList = new ValidationErrors();
            newList.AddRange(originalList);
            newList.AddRange(listToAdd);
            return newList;
        }

        public AggregateException AsException()
        {
            if (Count == 0) return null;
            var exceptions = new List<Exception>();

            foreach (var error in this)
            {
                switch (error.Type)
                {
                    case InFurSecDen.Utils.ConfigurationValidator.ValidationErrorType.RequiredPropertyIsNull:
                        exceptions.Add(new KeyNotFoundException($"The required configuration value \"{error.Key}\" was not found in any of the configuration sources or it was set to null."));
                        break;
                    default:
                        exceptions.Add(new Exception($"A validation error occured with the key \"{error.Key}\". It's value is \"{error.Value}\", which is not valid."));
                        break;
                }
            }
            return new AggregateException("Validation errors were found. Please refer to InnerExceptions for more details.", exceptions);
        }
    }
}
