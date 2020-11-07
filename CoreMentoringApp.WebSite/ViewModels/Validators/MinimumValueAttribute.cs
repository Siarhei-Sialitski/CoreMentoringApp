using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace CoreMentoringApp.WebSite.ViewModels.Validators
{
    [AttributeUsage(AttributeTargets.Property)]
    public class MinimumValueAttribute : ValidationAttribute, IClientModelValidator
    {

        public MinimumValueAttribute(int minValue)
        {
            MinValue = minValue;
        }

        public int MinValue { get; }

        public void AddValidation(ClientModelValidationContext context)
        {
            MergeAttribute(context.Attributes, "data-val", "true");
            MergeAttribute(context.Attributes, "data-val-minimumvalue", GetErrorMessage(context.ModelMetadata.DisplayName));
            MergeAttribute(context.Attributes, "data-val-minimumvalue-minvalue", Convert.ToString(MinValue));
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            try
            {
                if (Convert.ToInt32(value) < MinValue)
                {
                    return new ValidationResult(GetErrorMessage(validationContext.DisplayName));
                }
            }
            catch (Exception)
            {}
            return ValidationResult.Success;
        }

        private bool MergeAttribute(IDictionary<string, string> attributes, string key, string value)
        {
            if (attributes.ContainsKey(key))
            {
                return false;
            }

            attributes.Add(key, value);
            return true;
        }

        private string GetErrorMessage(string displayName) => $"{displayName} should be greater than or equal to {MinValue}.";
        
    }
}
