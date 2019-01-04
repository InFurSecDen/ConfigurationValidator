namespace InFurSecDen.Utils.ConfigurationValidator
{
    public class ValidationError
    {
        internal ValidationError()
        { 
        }

        public string Key
        {
            get;
            internal set;
        }

        public string Value
        {
            get;
            internal set;
        }

        public ValidationErrorType Type
        {
            get;
            internal set;
        }
    }
}
