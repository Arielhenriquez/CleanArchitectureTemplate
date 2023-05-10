using CleanArchitectureTemplate.API.Settings;

var builder = WebApplication.CreateBuilder(args);

var setup = new AppSetup(builder.Configuration);

setup.Configure(builder.Environment);

setup.RegisterServices(builder.Services);

var app = builder.Build();

setup.SetupMiddlewares(app);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
