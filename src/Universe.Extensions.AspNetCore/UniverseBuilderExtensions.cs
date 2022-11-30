namespace Universe.Extensions.AspNetCore
{
    public static class UniverseBuilderExtensions
    { 

        public static UniverseBuilder AddAspNetCoreMvc(this UniverseBuilder builder, string? routePrefix = null)
        {
            builder.Services.AddHttpContextAccessor();
            builder.Services.Replace(new ServiceDescriptor(typeof(ISignal), typeof(HttpContextSignal), ServiceLifetime.Scoped));
            builder.Services.AddScoped<IErrorInfoBuilder, ErrorInfoBuilder>();
            builder.Services.Configure<MvcOptions>(configure =>
            {
                configure.Filters.Add<UniverseExceptionFilter>();
                configure.Filters.Add<UniverseResultFilter>();
                if (!routePrefix.IsNullOrEmpty())
#pragma warning disable CS8604
                    configure.AddRoutePrefix(routePrefix);
#pragma warning restore CS8604 
            });
            return builder;
        }
    }
}
