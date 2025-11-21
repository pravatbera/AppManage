using System.Net;

namespace AppManage.Model.System
{

        public class ExceptionModelBase
        {
            public string Code { get; set; }
            public string Message { get; set; }
        }
        //ER401
        public class AuthenticationException : AppException
        {
            public AuthenticationException() : base("Authentication Required", "ER401")
            {
                ExceptionData = new ExceptionModelBase()
                {
                    Code = "ER401",
                    Message = "Authentication Required"
                };
                this.HttpStatus = HttpStatusCode.Unauthorized;
            }
            public AuthenticationException(string msg) : base(msg, "ER401")
            {
                ExceptionData = new ExceptionModelBase()
                {
                    Code = "ER401",
                    Message = msg
                };
                this.HttpStatus = HttpStatusCode.Unauthorized;
            }
        }
        //ER500
        public class AppException : Exception
        {
            HttpStatusCode _httpStatus = HttpStatusCode.InternalServerError;
            public HttpStatusCode HttpStatus { get { return _httpStatus; } protected set { _httpStatus = value; } }
            public ExceptionModelBase ExceptionData { get; protected set; }
            public AppException(string msg = "Internal server Error", string code = "ER500", HttpStatusCode HttpStatus = HttpStatusCode.InternalServerError) : base(msg)
            {
                ExceptionData = new ExceptionModelBase()
                {
                    Code = code,
                    Message = msg
                };
                HttpStatus = HttpStatusCode.InternalServerError;
            }
            public AppException(ExceptionModelBase exceptionModel) : base(exceptionModel.Message)
            {
                ExceptionData = exceptionModel;
                HttpStatus = HttpStatusCode.InternalServerError;
            }
            public AppException(ExceptionModelData exceptionModel) : base(exceptionModel.Message)
            {
                ExceptionData = exceptionModel;
                HttpStatus = HttpStatusCode.InternalServerError;
            }
        }
        public class ExceptionModelData : ExceptionModelBase
        {
            public List<KeyMessage<string, string>> ErrList { get; set; }
            public ExceptionModelData()
            {
                ErrList = new List<KeyMessage<string, string>>();
            }
        }
    }

