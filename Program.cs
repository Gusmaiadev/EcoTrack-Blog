using EcoTrack.Blog.Configuration;
using EcoTrack.Blog.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add MVC services with views
builder.Services.AddControllersWithViews()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    })
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
        options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
    });

// Configuração da Sessão
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
});

// Configure DbContext for Oracle
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseOracle(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        options => options.UseOracleSQLCompatibility("11")
    ));

// Configure JWT Authentication
var jwtSection = builder.Configuration.GetSection("Jwt");
builder.Services.Configure<JwtConfig>(jwtSection);

var jwtSettings = jwtSection.Get<JwtConfig>();
var key = Encoding.ASCII.GetBytes(jwtSettings.Key);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidIssuer = jwtSettings.Issuer,
        ValidAudience = jwtSettings.Audience,
        RequireExpirationTime = true,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };
    options.Events = new JwtBearerEvents
    {
        OnAuthenticationFailed = context =>
        {
            if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
            {
                context.Response.Headers.Add("Token-Expired", "true");
            }
            return Task.CompletedTask;
        },
        OnMessageReceived = context =>
        {
            var token = context.Request.Cookies["JWTToken"];
            if (!string.IsNullOrEmpty(token))
            {
                context.Token = token;
            }
            return Task.CompletedTask;
        }
    };
});

// Configure AutoMapper
builder.Services.AddAutoMapper(typeof(Program).Assembly);

// Add DependencyInjection Configuration
builder.Services.AddDependencyInjectionConfig();

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});

// Configure Swagger
builder.Services.AddSwaggerGen(c =>
{
c.SwaggerDoc("v1", new OpenApiInfo
{
Title = "EcoTrack Blog API",
Version = "v1",
Description = "API para o blog EcoTrack de dicas sustentáveis",
Contact = new OpenApiContact
{
Name = "Seu Nome",
Email = "rm553270@fiap.com.br"
}
});

c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
{
Description = "JWT Authorization header using the Bearer scheme.",
Name = "Authorization",
In = ParameterLocation.Header,
Type = SecuritySchemeType.ApiKey,
Scheme = "Bearer"
});

c.AddSecurityRequirement(new OpenApiSecurityRequirement
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

// Add Response Compression
builder.Services.AddResponseCompression(options =>
{
options.EnableForHttps = true;
});

// Add Distributed Cache
builder.Services.AddDistributedMemoryCache();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
app.UseSwagger();
app.UseSwaggerUI(c =>
{
c.SwaggerEndpoint("/swagger/v1/swagger.json", "EcoTrack Blog API v1");
});
app.UseDeveloperExceptionPage();
}
else
{
app.UseExceptionHandler("/Home/Error");
app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseCors("AllowAll");
app.UseResponseCompression();

// Adicione UseSession antes de UseAuthentication
app.UseSession();

// Middleware para processar o token JWT
app.Use(async (context, next) =>
{
var token = context.Request.Cookies["JWTToken"];
if (!string.IsNullOrEmpty(token))
{
context.Request.Headers.Add("Authorization", $"Bearer {token}");
}
await next();
});

app.UseAuthentication();
app.UseAuthorization();

// Global error handling middleware
app.UseMiddleware<ErrorHandlingMiddleware>();

// Redirecionamentos para Home
app.MapGet("/", async context =>
{
context.Response.Redirect("/home", permanent: false);
await Task.CompletedTask;
});

app.MapGet("/index.html", async context =>
{
context.Response.Redirect("/home", permanent: false);
await Task.CompletedTask;
});

// Configure MVC routes
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Map API controllers
app.MapControllers();

// Database Migration
using (var scope = app.Services.CreateScope())
{
var services = scope.ServiceProvider;
try
{
var context = services.GetRequiredService<ApplicationDbContext>();
context.Database.Migrate();
}
catch (Exception ex)
{
var logger = services.GetRequiredService<ILogger<Program>>();
logger.LogError(ex, "An error occurred while migrating the database.");
}
}

app.Run();

// Classes auxiliares
public class ErrorResponse
{
    public string Message { get; set; }
    public string StackTrace { get; set; }
}

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorHandlingMiddleware> _logger;
    private readonly IWebHostEnvironment _env;

    public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger, IWebHostEnvironment env)
    {
        _next = next;
        _logger = logger;
        _env = env;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unhandled exception occurred.");

            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/json";

            var response = new ErrorResponse
            {
                Message = _env.IsDevelopment() ? ex.Message : "An error occurred processing your request.",
                StackTrace = _env.IsDevelopment() ? ex.StackTrace : null
            };

            await context.Response.WriteAsJsonAsync(response);
        }
    }
}