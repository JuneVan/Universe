namespace Universe.Extensions.AspNetCore.Mvc.Validation
{
    public class ModelStateValidationActionFilter : IAsyncActionFilter,IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
             
        }

        public  async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context?.ModelState.ErrorCount > 0)
            {
                StringBuilder messageBuilder = new();
                messageBuilder.Append("发生验证错误：");
                foreach (KeyValuePair<string, Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateEntry> state in context.ModelState)
                {
                    foreach (Microsoft.AspNetCore.Mvc.ModelBinding.ModelError error in state.Value.Errors)
                    {
                        messageBuilder.AppendFormat("{0}({1})；", error.ErrorMessage, state.Key);
                    }
                }
                throw new ValidationException(messageBuilder.ToString());
            }
            await next();
        }
    }
}
