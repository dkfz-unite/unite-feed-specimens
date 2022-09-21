using FluentValidation.AspNetCore;
using Unite.Specimens.Feed.Web.Configuration.Extensions;


var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();

builder.Logging.AddConsole();

builder.Services.Configure();

builder.Services.AddCors();

builder.Services.AddControllers(options => options.AddMvcOptions())
                .AddJsonOptions(options => options.AddJsonOptions())
                .AddFluentValidation();


var app = builder.Build();

app.UseRouting();

app.UseCors(builder => builder
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true)
    .AllowCredentials()
);

app.UseAuthorization();

app.MapControllers();

app.Run();
