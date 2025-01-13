using employee.Repository;
using ProductData;
using ProductData.Common;
using ProductData.Repository;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
// Add services to the container.
services.AddTransient<ProductRepository>(_ => new ProductRepository(builder.Configuration.GetConnectionString("DefaultConnection")));
services.AddTransient<ShoppingCartRepository>(_ => new ShoppingCartRepository(builder.Configuration.GetConnectionString("DefaultConnection")));
services.AddSingleton<IGlobalErrorInstance, GlobalErrorInstance>();

#region CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
         builder => 
              builder.SetIsOriginAllowed(_ => true)
             .AllowAnyMethod()
             .AllowAnyHeader()
             .AllowCredentials()
             .Build());
});

#endregion


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseMiddleware<ExceptionHandler>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseAuthorization();
app.UseCors("CorsPolicy");

app.MapControllers();

app.Run();
