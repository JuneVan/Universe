namespace Universe.Extensions.AspNetCore.Mvc.Models
{
    public class AjaxResponse<TResult>
    {
        /// <summary>
        /// 消息
        /// </summary>
        public string? Message { get; set; } = "成功";

        /// <summary>
        /// 状态码
        /// </summary>
        public long Code { get; set; }

        /// <summary>
        /// 返回结果
        /// </summary>
        public TResult? Result { get; set; }

        public AjaxResponse()
        {
        }

        public AjaxResponse(TResult? result)
        {
            Code = 0;
            Result = result;
        }
        public AjaxResponse(long code, string? message)
        {
            Code = code;
            Message = message;
        }
    }

    public class AjaxResponse : AjaxResponse<object>
    {
        public AjaxResponse()
        {

        }
        public AjaxResponse(object? result)
           : base(result)
        {

        }
        public AjaxResponse(long code, string? message)
            : base(code, message)
        {

        }
    }
}
