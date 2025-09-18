using MudBlazor.Services;
using BackOffice.Components;
using BackOffice.Services;
using Database.Data;
using Microsoft.EntityFrameworkCore;
using Bycript;
using Bucket;
using Minio;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
var builder = WebApplication.CreateBuilder(args);

// Add MudBlazor services
builder.Services.AddMudServices();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();


builder.Services.AddDbContext<DatabseContext>(opt => opt.UseMySQL("Server=interchange.proxy.rlwy.net;Port=49952;Database=railway;Uid=root;Pwd=aRcZJTlisNyoJXrKRLmaQceAwtQgGfiw;"));

// Add custom servicesServe
builder.Services.AddScoped<IAlimentoService, AlimentoService>();
builder.Services.AddScoped<IAnimalService, AnimalService>();
builder.Services.AddScoped<IBitacoraService, BitacoraService>();
builder.Services.AddScoped<IBitacoraPesoService, BitacoraPesoService>();
builder.Services.AddScoped<IBitacoraVacunaService, BitacoraVacunaService>();
builder.Services.AddScoped<ICompraAlimentoService, CompraAlimentoService>();
builder.Services.AddScoped<IEstadoAnimalService, EstadoAnimalService>();
builder.Services.AddScoped<IEmpleadoService, EmpleadoService>();
builder.Services.AddScoped<IHistorialAlimenticioService, HistorialAlimenticioService>();
builder.Services.AddScoped<IReporteService, ReporteService>();
builder.Services.AddScoped<IRazaService, RazaService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITiposAlimentoService, TiposAlimentoService>();
builder.Services.AddScoped<IUnidadesDeMedidaAlimentoService, UnidadesDeMedidaAlimentoService>();
builder.Services.AddScoped<IVacunaService, VacunaService>();
builder.Services.AddScoped<BackOffice.Mapper.Mapa>();
builder.Services.AddScoped<IBCryptService, BCryptService>();

// Minio configuration
builder.Services.AddScoped<IMinioClient>(provider =>
{
    return new MinioClient()
        .WithEndpoint("localhost", 9000)
        .WithCredentials("minioadmin", "minioadmin")
        .Build();
});

builder.Services.AddHttpContextAccessor();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddHttpClient();
builder.Services.AddScoped<IMinioService>(provider =>
{
    var minioClient = provider.GetRequiredService<IMinioClient>();
    return new MinioService(minioClient, "localhost", 9000, false);
});

//
// Configurar autenticación con cookies
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.Name = "auth_token";
        options.LoginPath = "/login";
        options.LogoutPath = "/logout";
        options.Cookie.MaxAge = TimeSpan.FromDays(7);
         options.AccessDeniedPath = "/AccessDenied";
    });

builder.Services.AddAuthorizationBuilder()
    .AddPolicy("AdminOnly", policy => policy.RequireRole("Administrador"))
    .AddPolicy("UserOnly", policy => policy.RequireRole("Usuario", "Administrador"));
var app = builder.Build();

//
app.MapPost("/api/auth/login", async (HttpContext context,BackOffice.Services.IUserService usuarioService) =>
{
    var username = context.Request.Form["username"].ToString();
    var password = context.Request.Form["password"].ToString();
    
    var user = await usuarioService.AuthenticateAsync(username, password);

    if (user != null)
    {
       
        
         var claims = new List<System.Security.Claims.Claim>
        {
            new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Name, user.Email),
            new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.NameIdentifier, user.Id.ToString()),
            new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Role,user.IsAdmin?"Administrador":"Usuario")
        };

        if (user.Empleado != null)
        {
            claims.Add(new System.Security.Claims.Claim("EmpleadoId", user.Empleado.Id.ToString()));
            claims.Add(new System.Security.Claims.Claim("EmpleadoNombre", $"{user.Empleado.PrimerNombre} {user.Empleado.PrimerApellido}"));
        }

        var identity = new System.Security.Claims.ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var principal = new System.Security.Claims.ClaimsPrincipal(identity);
        
        await context.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
        
        context.Response.Redirect("/");
       
    }
    else
    {
        context.Response.Redirect("/login?error=true");
    }
});

app.MapPost("/api/auth/logout", async (HttpContext context) =>
{
    await context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    context.Response.Redirect("/");
});

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
