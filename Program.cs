using DataLibrary.Data;
using DataLibrary.Db;

var builder = WebApplication.CreateBuilder(args);
Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton(new ConnectionStringData
{
    postgresConnectionName = "Default"
});
builder.Services.AddSingleton<IDataAccess, PostgresDb>();
builder.Services.AddSingleton<IFoodData, FoodData>();
builder.Services.AddSingleton<IOrderData, OrderData>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();