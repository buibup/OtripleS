using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OtripleS.Web.Api.Models.Students.Exceptions
{
    public class LockedStudentException : Exception
    {
        public LockedStudentException(Exception innerException)
            : base("Locked student record exception, please try again later.", innerException) { }
    }
}
