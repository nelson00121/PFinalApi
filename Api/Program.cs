using Api.Mapper;
using Bycript;
using Database.Data;
using Microsoft.EntityFrameworkCore;
using HotChocolate.Validation;
using AppAny.HotChocolate.FluentValidation;
using Minio;
using Bucket;
using DataAnnotatedModelValidations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

var contra = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes("Micontraseñasupersecreta"));

builder.AddGraphQL().AddTypes()
.AddProjections()
.AddFiltering()
.AddSorting()
.AddPagingArguments()
.AddFluentValidation()
.AddType<UploadType>()
.AddDataAnnotationsValidator();
builder.Services.AddDbContext<DatabseContext>(opt => opt.UseMySQL("Server=interchange.proxy.rlwy.net;Database=railway;Uid=root;Pwd=RcZJTlisNyoJXrKRLmaQceAwtQgGfiw;Port=49952;Convert Zero Datetime=True;"));

builder.Services
 .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters =
                    new TokenValidationParameters
                    {
                        ValidIssuer = "https://nelson.com",
                        ValidAudience = "https://nelson.com",
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = contra
                    };
            });
string direccion = "localhost";
int puerto = 9000;
bool ssl = false;

builder.Services.AddMinio(configureClient => configureClient
    .WithEndpoint(direccion, puerto)
    .WithCredentials("minioadmin", "minioadmin")
    .WithSSL(ssl));

builder.Services.AddScoped<IMinioService>(provider => 
{
    var minioClient = provider.GetRequiredService<IMinioClient>();
    return new MinioService(minioClient, direccion, puerto, ssl);
});
builder.Services.AddScoped<Mapa>();

builder.Services.AddScoped<IBCryptService, BCryptService>();
var app = builder.Build();


app.MapGraphQL();

app.RunWithGraphQLCommands(args);
