using AnimeVerse.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
// Här registreras AnimeService som en HttpClient-tjänst via Dependency Injection. Det gör att alla API-anrop hanteras på ett effektivt och säkert sätt.
builder.Services.AddHttpClient<IAnimeService, AnimeService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Anime}/{action=Index}/{id?}");

app.Run();