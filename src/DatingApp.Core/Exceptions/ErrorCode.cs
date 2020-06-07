using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace DatingApp.Core.Exceptions
{
    public class ErrorCode
    {
        public HttpStatusCode StatusCode { get; set; }

        public string Message { get; set; }

        public ErrorCode(string message, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        {
            Message = message;
            StatusCode = statusCode;
        }

        public static ErrorCode DomainValidationError => new ErrorCode(nameof(DomainValidationError), HttpStatusCode.UnprocessableEntity);

        public static ErrorCode NoPermission => new ErrorCode(nameof(NoPermission), HttpStatusCode.UnprocessableEntity);

        public static ErrorCode FaultWhileSavingToDatabase => new ErrorCode(nameof(FaultWhileSavingToDatabase), HttpStatusCode.InternalServerError);

        public static ErrorCode EntityValidationException => new ErrorCode(nameof(EntityValidationException), HttpStatusCode.UnprocessableEntity);

        public static ErrorCode AlreadyExists => new ErrorCode(nameof(AlreadyExists), HttpStatusCode.Conflict);

        public static ErrorCode NotFound => new ErrorCode(nameof(NotFound), HttpStatusCode.NotFound);

        public static ErrorCode Empty(string propertyName) => new ErrorCode($"{propertyName} is empty", HttpStatusCode.UnprocessableEntity);
    }
}
