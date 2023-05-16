using Abp.Runtime.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GameShop.Utilities.Exceptions
{
    public class ExceptionCustom : AbpValidationException
    {
        public ExceptionCustom(string message) : base(message, new List<ValidationResult>() { new ValidationResult(message) })
        {


        }


    }
}
