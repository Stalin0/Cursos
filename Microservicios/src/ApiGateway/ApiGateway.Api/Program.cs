var builder = WebApplication.CreateBuilder(args);

builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/openapi/users/v1.json", "UserService API");
    options.SwaggerEndpoint("/openapi/products/v1.json", "ProductService API");
    options.SwaggerEndpoint("/openapi/sales/v1.json", "SalesService API");
});

app.MapReverseProxy();

app.Run();
