using AutoMapper;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SocialMedia.Core.Interfaces;
using SocialMedia.Core.Services;
using SocialMedia.Infraestructure.Data;
using SocialMedia.Infraestructure.Filter;
using SocialMedia.Infraestructure.Interfaces;
using SocialMedia.Core.Options;
using SocialMedia.Infraestructure.Repositories;
using SocialMedia.Infraestructure.Services;
using Microsoft.OpenApi.Models;
using System.Reflection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using SocialMedia.Infraestructure.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddControllers(options=>{
    options.Filters.Add<GlobalExceptionFilter>();
}).AddNewtonsoftJson(optons =>
{
    optons.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    optons.SerializerSettings.NullValueHandling=Newtonsoft.Json.NullValueHandling.Ignore;
}).ConfigureApiBehaviorOptions(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

// Configurar Swagger para la documentación de la API (opcional pero recomendado).
builder.Services.AddDbContext<SocialMediaContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SocialMedia"));
});

builder.Services.AddTransient<IPublicacionService, PublicacionServices>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddTransient<IPublicacionRepository,PublicacionRepository>();
builder.Services.AddTransient<ISeguridadService, SeguridadService>();
builder.Services.AddTransient<ISeguridadRepository, SeguridadRepository>();
builder.Services.AddSingleton<IPasswordService, PasswordService>();

builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<IUriServices>(provider =>
{
    var accesor=provider.GetRequiredService<IHttpContextAccessor>();
    var request = accesor.HttpContext?.Request;
    var absoluteUri = string.Concat(request?.Scheme, "://", request?.Host.ToUriComponent());
    return new UriServices(absoluteUri);
});
builder.Services.Configure<PaginationsOptions>(builder.Configuration.GetSection("Paginations"));
builder.Services.Configure<PasswordOption>(builder.Configuration.GetSection("PassworOptions"));
builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(option =>
{
    option.TokenValidationParameters = new TokenValidationParameters
    {

        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Authentification:Issuer"],
        ValidAudience = builder.Configuration["Authentification:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Authentification:SecretKey"]))


    };
});
builder.Services.AddMvc(options =>
{
    options.Filters.Add<ValidationsFilter>();
}).AddFluentValidation(options =>
{
    options.RegisterValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
});

builder.Services.AddSwaggerGen(doc =>
{
    doc.SwaggerDoc("v1",new OpenApiInfo { Title="SOCIAL MEDIA API",Version="v1"});
    var XmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var XmlPath=Path.Combine(AppContext.BaseDirectory,XmlFile);
    doc.IncludeXmlComments(XmlPath);
});


var app = builder.Build();

// Configurar el pipeline HTTP.

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();

}

app.UseHttpsRedirection();
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("../swagger/v1/swagger.json","Social Media Api");
    //options.RoutePrefix=string.Empty;
});
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
