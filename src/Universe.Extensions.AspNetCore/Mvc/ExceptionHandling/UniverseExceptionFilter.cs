namespace Universe.Extensions.AspNetCore.Mvc.ExceptionHandling
{
    public class UniverseExceptionFilter : IExceptionFilter
    {
        private readonly IErrorInfoBuilder _errorInfoBuilder; 
        public UniverseExceptionFilter(IErrorInfoBuilder errorInfoBuilder)
        {
            _errorInfoBuilder = errorInfoBuilder;
        }
        public void OnException(ExceptionContext context)
        {
            if (!context.ActionDescriptor.IsControllerAction())
                return;
            // 获取WrapResult的设置
            var wrapResultAttribute = ReflectionHelper.GetAttributeOrDefault(
                context.ActionDescriptor.GetMethodInfo(), WrapResultAttribute.DefaultWrapResult);

            LogException(context, wrapResultAttribute);
            // 处理标准返回值
            HandleAndWrapException(context, wrapResultAttribute);
        }
        private void LogException(ExceptionContext context, WrapResultAttribute wrapResultAttribute)
        {
            var loggerFactory = context.HttpContext.RequestServices.GetRequiredService<ILoggerFactory>();
            var logger = loggerFactory.CreateLogger(context.ActionDescriptor.AsControllerActionDescriptor().ControllerName);

            if (wrapResultAttribute.LogError)
            {
                var severity = (context.Exception as IHasLogLevel)?.Level ?? LogLevel.Error;
                logger.Log(severity, context.Exception.Message, context.Exception);
            }
        }

        protected virtual void HandleAndWrapException(ExceptionContext context, WrapResultAttribute wrapResultAttribute)
        {
            if (!ActionResultHelper.IsObjectResult(context.ActionDescriptor.GetMethodInfo().ReturnType))
                return;

            context.HttpContext.Response.StatusCode = GetStatusCode(context, wrapResultAttribute.WrapOnError);
            // 发生异常时，是否返回标准结果值
            if (!wrapResultAttribute.WrapOnError)
                return;

            HandleError(context);
        }
        private void HandleError(ExceptionContext context)
        {
            var errorInfo = _errorInfoBuilder.BuildForException(context.Exception);
            var ajaxResponse = new AjaxResponse(errorInfo.Code, errorInfo.Message);

            context.Result = new ObjectResult(ajaxResponse);
            context.Exception = null;
        }
        protected virtual int GetStatusCode(ExceptionContext context, bool wrapOnError)
        {
            if (context.Exception is AuthorizationException)
            {
                return (context.HttpContext.User.Identity?.IsAuthenticated ?? false)
                    ? (int)HttpStatusCode.Forbidden
                    : (int)HttpStatusCode.Unauthorized;
            }

            if (context.Exception is ValidationException)
            {
                return (int)HttpStatusCode.BadRequest;
            }

            if (context.Exception is EntityNotFoundException)
            {
                return (int)HttpStatusCode.NotFound;
            }

            if (wrapOnError)
            {
                return (int)HttpStatusCode.InternalServerError;
            }

            return context.HttpContext.Response.StatusCode;
        }
    }
}
