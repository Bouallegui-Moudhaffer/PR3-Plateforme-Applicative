using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PA.ApplicationCore;
using PA.ApplicationCore.Interfaces;
using PA.ApplicationCore.Services;
using PA.DataPoint.Server;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<AuthenticationService>();
builder.Services.AddScoped<TokenService>();
builder.Services.AddScoped<IPostes, PostesService>();
builder.Services.AddScoped<IEtablissement, EtablissementService>();
builder.Services.AddScoped<ILog, LogService>();
builder.Services.AddScoped<IUtilisateur, UtilisateurService>();
builder.Services.AddScoped<ISalles, SallesService>();
builder.Services.AddScoped<ITypes, TypeService>();
builder.Services.AddScoped<IStatus, StatusService>();


builder.Services.AddControllers();
builder.Services.AddSignalR();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

    // Define the BearerAuth scheme that's in use
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme."
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
            new string[] {}
        }
    });
});

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(@"Data Source=(localdb)\mssqllocaldb;
                                          Initial Catalog=PR3;
                                          Integrated Security=true;
                                          MultipleActiveResultSets=true"));
builder.Services.AddSingleton<Type>(t => typeof(GenericRepository<>));

// Add JWT authentication services
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}
).AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
{
    ValidateIssuer = false,
    ValidateAudience = false,
    ValidateLifetime = true,
    ValidateIssuerSigningKey = true,
    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("6cFgsF/zQjtPVqVtoAliQ7ToN941oKprEy+cKsHqnic="))
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorOrigin",
        builder => builder.WithOrigins("https://localhost:7113")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials());
});

var app = builder.Build();

app.UseRouting();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowBlazorOrigin");

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapHub<MachinesHub>("/machinesHub"); // Map the SignalR Hub
});

app.MapControllers();

app.Run();
