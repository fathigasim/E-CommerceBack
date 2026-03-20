using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceApplication.Exceptions
{
    // Application/Common/Exceptions/ApplicationException.cs

    public class ApplicationException : Exception
    {
        public ApplicationException(string message) : base(message) { }
    }

    //public class NotFoundException : ApplicationException
    //{
    //    public NotFoundException(string entityName, object key)
    //        : base($"{entityName} with key '{key}' was not found.") { }
    //}

    public class ValidationException : ApplicationException
    {
        public List<ValidationError> Errors { get; }

        public ValidationException(List<ValidationError> errors)
            : base("One or more validation errors occurred.")
        {
            Errors = errors;
        }
    }

    public class ValidationError
    {
        public string PropertyName { get; set; } = default!;
        public string ErrorMessage { get; set; } = default!;
    }




}
  


