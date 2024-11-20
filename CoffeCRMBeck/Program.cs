using CoffeCRMBeck.Common;
using CoffeCRMBeck.DAL;
using CoffeCRMBeck.DAL.Context;
using CoffeCRMBeck.DAL.@interface;
using CoffeCRMBeck.Model;
using CoffeCRMBeck.Service;
using CoffeCRMBeck.Services;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

var authOption = builder.Configuration.GetSection("Auth");
builder.Services.Configure<AuthOptions>(authOption);

builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);

builder.Services.AddCors(options => options.AddPolicy("CorsPolicy",
    builder =>
    {
        builder
        .AllowAnyHeader()
        .AllowAnyMethod()
        .SetIsOriginAllowed((host) => true)
        .AllowCredentials();
    }));



//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//    .AddJwtBearer(option =>
//    {
//        option.RequireHttpsMetadata = false; // ��������� ����������� ����� �� http � �� �� https
//        option.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
//        {
//            ValidateIssuer = true, // �������� �������� ������
//            ValidIssuer = authOption.Get<AuthOptions>().Issuer,

//            ValidateAudience = true, // �������� ���������� ������
//            ValidAudience = authOption.Get<AuthOptions>().Audience,

//            ValidateLifetime = true,

//            IssuerSigningKey = authOption.Get<AuthOptions>().GetSymmetricSecurityKey(), //HS256
//            ValidateIssuerSigningKey = true,
//        };
//    });


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql("Host=localhost;Port=5433;Database=CRMSystem;Username=postgres;Password=1234567890").LogTo(Console.WriteLine));

//Service
builder.Services.AddTransient<MailService>();
builder.Services.AddTransient<CheckService>();
builder.Services.AddTransient<WorkerService>();
builder.Services.AddTransient<ProductService>();
builder.Services.AddTransient<ProductCatalogService>();


//Repository
builder.Services.AddScoped<IRepository<Menu>, ProductCatalogRepository>();
builder.Services.AddScoped<IRepository<Order>, ProductRepository>();
builder.Services.AddScoped<IRepository<Staff>, WorkerRepository>();
builder.Services.AddScoped<IRepository<Bill>, CheckRepository>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CorsPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();


app.MapControllers();

app.Run();
