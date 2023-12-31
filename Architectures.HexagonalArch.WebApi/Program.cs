using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using Architectures.HexagonalArch.Infra.ContextosBancoDeDados;
using Microsoft.EntityFrameworkCore;
using Architectures.HexagonalArch.WebApi.Filters;
using Architectures.HexagonalArch.Infra.Configuracoes;
using Architectures.HexagonalArch.Infra;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.Configure<ConfiguracaoArquivo>(builder.Configuration.GetSection("Arquivo"));
builder.Services.Configure<ConfiguracaoBancoDeDados>(builder.Configuration.GetSection("BancoDeDados"));
builder.Services.Configure<ConfiguracaoSeguranca>(builder.Configuration.GetSection("Seguranca"));
builder.Services.AdicionarInfraestrutura(builder.Configuration);

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

var configuracoes = builder.Configuration.GetSection("Seguranca").Get<ConfiguracaoSeguranca>() ?? throw new ApplicationException("Configura��es de seguran�a n�o configuradas");

byte[] key = Encoding.ASCII.GetBytes(configuracoes.SecretKey);

builder.Services.AddAuthentication(
    options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }
)
.AddJwtBearer(
    options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false,
        };
    }
);

builder.Services.AddControllers(x => x.Filters.Add(new FiltroExcecao()));

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

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<EntityFrameworkContexto>();
    db.Database.Migrate();
}

app.Run();
