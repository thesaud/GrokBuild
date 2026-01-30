var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls("http://+:80"); // يستمع على كل IP على المنفذ 80 داخل الحاوية

// إضافة الخدمات
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

var app = builder.Build();

// تفعيل Swagger دائمًا داخل Docker
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Grok30 API V1");
    c.RoutePrefix = "swagger"; // يمكن فتحه عبر /swagger/index.html
});

// يمكن تعطيل HTTPS داخل Docker لتجنب مشاكل الشهادات
// app.UseHttpsRedirection();

// خرائط الـ APIs
app.MapControllers();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
