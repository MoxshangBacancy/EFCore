using EFCore.data; 
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
string ScopedConnectionStrings = "Server=MOXSHANGSHAH\\SQLEXPRESS;Database=ScopedDatabase;Trusted_Connection=True;TrustServerCertificate=True;";
string SingletonConnectionStrings = "Server=MOXSHANGSHAH\\SQLEXPRESS;Database=SingletonDatabase;Trusted_Connection=True;TrustServerCertificate=True;";
string TransientConnectionStrings = "Server=MOXSHANGSHAH\\SQLEXPRESS;Database=TransientDatabase;Trusted_Connection=True;TrustServerCertificate=True;";


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//Scoped
//builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(ScopedConnectionStrings), ServiceLifetime.Scoped);

//Singleton
//builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(SingletonConnectionStrings), ServiceLifetime.Singleton);

//Transient
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(TransientConnectionStrings), ServiceLifetime.Transient);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//app.MapGet("/", () => "EF Core with OnConfiguring() Setup Running!");

//app.MapGet("/", () =>
//{
//    string? env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";
//    return $"Running in {env} mode!";
//});

app.Run();
