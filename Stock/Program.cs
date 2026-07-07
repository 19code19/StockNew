using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddMemoryCache();
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    });
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:5173", "https://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Register Swagger
builder.Services.AddSwaggerGen();

// Register AutoMapper with the assembly containing MappingProfile
builder.Services.AddAutoMapper(cfg => cfg.AddProfile<MappingProfile>());

// Register DbContext factory so parallel work can create isolated contexts
builder.Services.AddDbContextFactory<StockDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        sqlOptions => sqlOptions.EnableRetryOnFailure(
            maxRetryCount: 5,
            maxRetryDelay: TimeSpan.FromSeconds(10),
            errorNumbersToAdd: null)));

// Register repositories and services as transient to avoid sharing a single instance across concurrent work
builder.Services.AddTransient<StockRepository>();
builder.Services.AddTransient<FavoritesRepository>();
builder.Services.AddTransient<AiRepository>();
builder.Services.AddTransient<AiService>();
builder.Services.AddTransient<FavoritesService>();
builder.Services.AddTransient<NSEService>();
builder.Services.AddTransient<NSEDataService>();
builder.Services.AddHttpClient<NSEDataService>();

builder.Services.AddTransient<MutualFundRepository>();
builder.Services.AddTransient<MutualFundService>();
builder.Services.AddHttpClient<MutualFundService>(client => client.BaseAddress = new Uri("https://groww.in/"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors();
app.UseAuthorization();
app.MapControllers();

app.Run();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
