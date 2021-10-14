using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xeptions;

namespace SallyLibrary.App.Models.Books.Exceptions
{
    public class FailedBookServiceException : Xeption
    {
        public FailedBookServiceException(Exception innerException)
            : base("Failed book service error occurred, contact support.", innerException)
        { }
    }
}
