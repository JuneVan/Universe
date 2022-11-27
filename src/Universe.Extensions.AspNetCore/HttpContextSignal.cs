﻿namespace Universe.Extensions.AspNetCore.Threading
{
    public class HttpContextSignal :ISignal
    {
        private readonly IHttpContextAccessor _contextAccessor;
        public HttpContextSignal(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }
        public CancellationToken Token => _contextAccessor?.HttpContext?.RequestAborted ?? CancellationToken.None; 
    }
}
