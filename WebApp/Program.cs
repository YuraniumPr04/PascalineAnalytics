using BusinessLayer;
using BusinessLayer.Interfaces;
using BusinessLayer.Services;
using DataLayer;
using DataLayer.Entities;
using DataLayer.Interfaces;
using DataLayer.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Numerics;


var builder = WebApplication.CreateBuilder(args);

// ==========================================
// 1. РЕЄСТРАЦІЯ СЕРВІСІВ (Колишній ConfigureServices)
// ==========================================

// Підключення до БД (рядок підключення береться з appsettings.json)
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<_ApplicationContext>(options =>
    options.UseSqlServer(
        connectionString,
        b => b.MigrationsAssembly(typeof(_ApplicationContext).Assembly.GetName().Name)
    ));

// Налаштування Identity (Користувачі та Ролі)
builder.Services.AddIdentity<User, IdentityRole>(opts =>
{
    // Ваші налаштування пароля (слабкі, для розробки)
    opts.Password.RequiredLength = 5;
    opts.Password.RequireNonAlphanumeric = false;
    opts.Password.RequireLowercase = false;
    opts.Password.RequireUppercase = false;
    opts.Password.RequireDigit = false;
})
    .AddEntityFrameworkStores<_ApplicationContext>()
    .AddDefaultTokenProviders(); // Корисно для генерації токенів (скидання пароля тощо)

// Додаємо MVC (Контролери та представлення)
builder.Services.AddControllersWithViews();

// Додаємо Кеш та Сесії
builder.Services.AddMemoryCache();
builder.Services.AddSession();

// Тут можна додати ваші власні сервіси (Managers, Repositories)
// builder.Services.AddScoped<DataManager>();
// builder.Services.AddScoped<ServiceManager>();

//Repositories
builder.Services.AddTransient<IBaseRepository<UserSubscription>, UserSubscriptionRepository>();
builder.Services.AddTransient<IBaseRepository<SubscriptionPlan>, SubscriptionPlanRepository>();
builder.Services.AddScoped<DataManager>();

//Services
builder.Services.AddScoped<ISubscriptionService, SubscriptionService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ServiceManager>();


var app = builder.Build();

// ==========================================
// 2. ПОСІВ ДАНИХ (SEEDING) (Колишній Main)
// ==========================================

// Створюємо область видимості (Scope) для отримання сервісів
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var userManager = services.GetRequiredService<UserManager<User>>();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

        // Виклик вашого ініціалізатора ролей
        // Переконайтеся, що клас RoleInitializer доступний (додайте using)
        await RoleInitializer.InitializeAsync(userManager, roleManager);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while seeding the database.");
    }
}

// ==========================================
// 3. КОНФІГУРАЦІЯ PIPELINE (Колишній Configure)
// ==========================================

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

// Порядок важливий!
app.UseRouting();

app.UseAuthentication(); // Хто ти?
app.UseAuthorization();  // Що тобі можна?

app.UseSession(); // Підключаємо сесії

// Налаштування маршрутів (Routing)

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");



app.Run();