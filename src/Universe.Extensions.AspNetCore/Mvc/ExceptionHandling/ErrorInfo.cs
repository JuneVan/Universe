namespace Universe.Extensions.AspNetCore.Mvc.ExceptionHandling
{ 
    public class ErrorInfo
    {
        public long Code { get; set; }
        public string? Message { get; set; }
        public ErrorInfo()
        {

        }
        public ErrorInfo(string? message, long code)
        {
            Message = message;
            Code = code;
        }
    }
}
