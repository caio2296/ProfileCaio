using Aplicacao.Aplicacoes;
using Aplicacao.Interfaces;
using Aplicacao.Interfaces.Generico;
using Dominio.Interfaces;
using Dominio.Interfaces.Genericos;
using Dominio.Interfaces.InterfacesServicos;
using Dominio.Servicos;
using Entidades.Entidades;
using Infraestrutura.Configuracoes;
using Infraestrutura.Repositorio;
using Infraestrutura.Repositorio.Generico;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ProfileCaio.Token;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



builder.Services.AddDbContext<Contexto>(options=>
options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
  options.SignIn.RequireConfirmedAccount=false)
    .AddEntityFrameworkStores<Contexto>();

builder.Services.AddSingleton(typeof(IGenerico<>), typeof(RepositorioGenerico<>));

builder.Services.AddSingleton<ISolicitacao, RepositorioSolicitacoes>();
builder.Services.AddSingleton<IUsuario, RepositorioUsuario>();

builder.Services.AddSingleton<ISolicitacaoServico, SolicitacaoServico>();

builder.Services.AddSingleton<IAplicacaoSolicitacoes, AplicacaoSolicitacao>();
builder.Services.AddSingleton<IAplicacaoUsuario, AplicacaoUsuario>();


builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireAdministratorRole", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim("tipo", "Administrador");
    });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(option =>
    {
        option.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = "Securiry.Bearer",
            ValidAudience = "Securiry.Bearer",

            IssuerSigningKey = JwtSecurityKey.Creater("Secret_Key-12345678")
        };

        option.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = context=>
            {
                Console.WriteLine("OnAuthenticationFailed: " + context.Exception.Message);
                return Task.CompletedTask;
            },
            OnTokenValidated = context =>
            {
                Console.WriteLine("OnTokenValidated: " + context.SecurityToken);
                return Task.CompletedTask;
            }
        };
    });

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApi", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header usando o Bearer.
                        Entre com 'Bearer ' [espaço] então coloque seu token.
                        Exemplo: 'Bearer 12345oiuytr'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
});

var app = builder.Build();

//var optionsBuilder = new DbContextOptionsBuilder<Contexto>();
//optionsBuilder.UseSqlServer(builder.Configuration.GetConnectionString("Default"));

//// Chamar o método InicializandoDb diretamente da classe estática
//await InicializaDb.InicializandoDb(optionsBuilder.Options);


    app.UseSwagger();
    app.UseSwaggerUI();


var frontClient = "http://localhost:4200";
var frontClient2 = "https://caioportifolio.azurewebsites.net/";
app.UseCors(x =>
x.AllowAnyOrigin()
.AllowAnyMethod()
.AllowAnyHeader()
.WithOrigins(frontClient,frontClient2));

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
