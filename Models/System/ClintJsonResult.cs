using System.Net;

public class ClintJsonResult
{
    public object Data { get; set; }
    public HttpStatusCode Status { get; set; }
    public string Message { get; set; }
}
public class ErrorJsonResult:ClintJsonResult
{
    public string StackTrace { get; set; }
    public string ReqestedUrl { get; set; }
}
public class DBMessage
{
    public int code { get; set; }
    public string msg { get; set; }
}