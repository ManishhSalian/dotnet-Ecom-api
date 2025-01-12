using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using BagAPI.Data;
using BagAPI;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add JWT Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

// Add Authorization Services
builder.Services.AddAuthorization();

// Configure the database connection for PostgreSQL
builder.Services.AddDbContext<BagDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configure Swagger for JWT authentication
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid JWT token",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
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

var app = builder.Build();

// Ensure the database is created
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<BagDbContext>();
    context.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();  // Enable authentication
app.UseAuthorization();   // Enable authorization

app.MapPost("/login", async (LoginModel model, BagDbContext context, IConfiguration configuration) =>
{
    var user = await context.Users.FirstOrDefaultAsync(u => u.name == model.Username && u.password == model.Password);
    if (user == null)
    {
        // Use the Results.Problem method for providing a custom error message
        return Results.Problem("Invalid credentials", statusCode: 401);
    }

    var token = GenerateJwtToken(user, configuration);

    return Results.Ok(new { Token = token });
});


// JWT Token Generation Helper
string GenerateJwtToken(Users user, IConfiguration configuration)
{
    var claims = new[] {
        new Claim(ClaimTypes.NameIdentifier, user.id.ToString()),
        new Claim(ClaimTypes.Name, user.name),
        new Claim(ClaimTypes.Role, "User")
    };

    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

    var token = new JwtSecurityToken(
        issuer: configuration["Jwt:Issuer"],
        audience: configuration["Jwt:Audience"],
        claims: claims,
        expires: DateTime.Now.AddHours(1),
        signingCredentials: creds
    );

    return new JwtSecurityTokenHandler().WriteToken(token);
}

// Define other routes and apply authorization where necessary
/*app.MapGet("/getdata", async (BagDbContext context) =>
{
    var users = await context.Users.ToListAsync();
    return Results.Ok(users);
}).RequireAuthorization();

app.MapPost("/postdata", async (Users newUser, BagDbContext context) =>
{
    // No password hashing, plain text password
    await context.Users.AddAsync(newUser);
    await context.SaveChangesAsync();
    return Results.Created($"/users/{newUser.id}", newUser);
}).RequireAuthorization();

app.MapPut("/updatedata/{id}", async (int id, Users updatedUser, BagDbContext context) =>
{
    var user = await context.Users.FirstOrDefaultAsync(u => u.id == id);
    if (user == null)
        return Results.NotFound($"User with ID '{id}' not found.");

    user.name = updatedUser.name;
    user.age = updatedUser.age;
    user.mobile = updatedUser.mobile;

    // No password hashing, plain text password
    if (!string.IsNullOrEmpty(updatedUser.password))
    {
        user.password = updatedUser.password;
    }

    await context.SaveChangesAsync();
    return Results.Ok(user);
}).RequireAuthorization();*/

BagEndPoints.Map(app);
app.Run();
