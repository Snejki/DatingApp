using System;
using System.Collections.Generic;
using System.Text;

namespace DatingApp.Core.Exceptions
{
    public class DatingAppException : Exception
    {
        public ErrorCode ErrorCode { get; set; }

        public DatingAppException(ErrorCode errorCode)
            : this(errorCode, string.Empty)
        { }

        public DatingAppException(ErrorCode errorCode, string message)
            : this(errorCode, message, null)
        { }

        public DatingAppException(ErrorCode errorCode, string message, Exception innerException)
            : base(message, innerException)
        {
            ErrorCode = errorCode;
        }
    }
}
