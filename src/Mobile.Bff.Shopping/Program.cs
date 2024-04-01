using Mobile.Bff.Shopping.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.AddApplicationServices();

var app = builder.Build();

app.UseHttpsRedirection();

app.MapDefaultEndpoints();

app
    .MapCatalogEndpoints()
    .MapOrderEndpoints()
    .MapIdentityEndpoints();

await app.RunAsync();
