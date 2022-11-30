using Universe.Extensions.AspNetCore;
using Universe.Extensions.AspNetCore.Mvc.Result;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(configure =>
{
    configure.Filters.Add<UniverseResultFilter>();
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// 自定义路由路径
var routePrefix = "testwebapi";
// 增加Universe
builder.Services
    .AddUniverse(AppDomain.CurrentDomain.GetAssemblies())
         .AddAspNetCoreMvc(routePrefix);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{ 
    app.UseSwagger(c => {
        c.RouteTemplate = routePrefix + "/swagger/{documentName}/swagger.json";
    });
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint($"/{routePrefix}/swagger/v1/swagger.json", "Test WebApi v1");
        c.RoutePrefix = routePrefix + "/swagger";
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
