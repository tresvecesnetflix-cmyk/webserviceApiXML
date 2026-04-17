using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using webserviceApi.Datos;
using webserviceApi.Servicios;
using webserviceApi.Swagger;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
//configurar para formato XML
builder.Services.AddControllers().AddXmlSerializerFormatters(); ;
builder.Services.AddHttpContextAccessor();

builder.Services.AddTransient<IAlmacenadorDeArchivos, AlmacenadorDeArchivoLocal>();
builder.Services.AddTransient<IServicioUsuarios, ServicioUsuarios>();
//wompi
builder.Services.Configure<WompiSetting>(builder.Configuration.GetSection("Wompi"));
builder.Services.AddScoped<WompiService>();
builder.Services.AddHttpClient<WompiService>();


builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionString")));

builder.Services.AddAuthentication().AddJwtBearer(opciones => {
    opciones.MapInboundClaims = false;


    opciones.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["llavejwt"]!))
    };

}

);
builder.Services.AddSwaggerGen(opciones =>
{
    opciones.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Version = "v1",

        Title = "WebService Ecommerce/XML",
        Description = "API para el manejo de un ecommerce con respuestas en XML",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Name = "Jose Umaña",
            Email = "Joose0601@gmail.com",
            Url = new Uri("http://www.example.com")
        },
        License = new Microsoft.OpenApi.Models.OpenApiLicense
        {
            Name = "MIT",
            Url = new Uri("https://opensource.org/license/mit/")
        }

    });
    //configuramos Swagger para Json WebToken (JWT)

    opciones.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
    });
    opciones.OperationFilter<FiltroAutorizacion>();
});


builder.Services.AddIdentityCore<IdentityUser>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
builder.Services.AddScoped<UserManager<IdentityUser>>();
builder.Services.AddScoped<SignInManager<IdentityUser>>();




var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.UseStaticFiles();
app.MapControllers();
app.MapGet("/", () => "Hello World!");

app.Run();      
