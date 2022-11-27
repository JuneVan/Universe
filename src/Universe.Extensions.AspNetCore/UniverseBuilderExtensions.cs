namespace Universe.Extensions.AspNetCore
{
    public static class UniverseBuilderExtensions
    {
        public static UniverseBuilder AddAspNetCore(this UniverseBuilder builder)
        {
            builder.Services.AddHttpContextAccessor();
            builder.Services.Replace(new ServiceDescriptor(typeof(ISignal), typeof(HttpContextSignal), ServiceLifetime.Scoped));
            builder.Services.AddScoped<IErrorInfoBuilder, ErrorInfoBuilder>();
            return builder;
        }
    }
}
