namespace Universe.Extensions.AspNetCore.Mvc.ExceptionHandling
{
    public interface IErrorInfoBuilder
    {
        ErrorInfo BuildForException(Exception exception);
    }
}
