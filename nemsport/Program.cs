using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using nemsport.Data;
using System.Text;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<nemsportContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("nemsportContext") ?? throw new InvalidOperationException("Connection string 'nemsportContext' not found.")));

// Add services to the container.
builder.Services.AddScoped<UserService>();
builder.Services.AddControllers();

builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    // Configure Newtonsoft.Json options here, for example:
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("MyAllowSpecificOrigins",
        builder =>
        {
            builder.WithOrigins("http://localhost:5173") // Adjust the allowed origin as necessary
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtKey"])),
            ValidateIssuer = false,
            ValidateAudience = false,
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("MyAllowSpecificOrigins");

app.UseAuthorization();

app.MapControllers();

app.UseStaticFiles();

app.Run();
