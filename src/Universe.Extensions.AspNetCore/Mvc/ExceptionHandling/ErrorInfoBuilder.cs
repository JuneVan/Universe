namespace Universe.Extensions.AspNetCore.Mvc.ExceptionHandling
{
    public class ErrorInfoBuilder : IErrorInfoBuilder
    {
        public ErrorInfo BuildForException(Exception exception)
        {
            var errorInfo = CreateErrorInfo(exception);

            return errorInfo;
        }
        private ErrorInfo CreateErrorInfo(Exception exception)
        {
            if (exception is AggregateException aggregateException && aggregateException.InnerException != null)
            {
                exception = aggregateException.InnerException;
            }
            if (exception is EntityNotFoundException entityNotFoundException && entityNotFoundException != null)
            {
                if (entityNotFoundException?.EntityType != null)
                {
                    return new ErrorInfo(
                        $"无实体记录. 实体类型` {entityNotFoundException?.EntityType.FullName}`, id`{entityNotFoundException?.Id}`", 404
                    );
                }

                return new ErrorInfo(entityNotFoundException?.Message, 404);
            }
            if (exception is IHasErrorCode hasErrorCodeException)
            {
                return new ErrorInfo(exception.Message, hasErrorCodeException.Code);
            }
            return new ErrorInfo(exception.Message, -1);
        }

    }
}
