using System.Collections.Generic;

namespace KooliProjekt.PublicAPI.Api
{
    public class Result
    {
        public Dictionary<string, List<string>> Errors { get; set; } = new();

        public string Error { get; set; }

        public bool HasError => Errors.Count > 0 || !string.IsNullOrEmpty(Error);

        public void AddError(string propertyName, string errorMessage)
        {
            if (!Errors.ContainsKey(propertyName))
                Errors[propertyName] = new List<string>();

            Errors[propertyName].Add(errorMessage);
        }
    }
}
