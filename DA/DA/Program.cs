using DA.DataAccess;
using DA.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Add services to the container.
builder.Services.AddControllersWithViews();

// Thêm IConfiguration vào dịch vụ
builder.Services.AddSingleton(builder.Configuration);

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
        .AddDefaultTokenProviders()
        .AddDefaultUI()
        .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddRazorPages();

builder.Services.AddScoped<IProductRepository, EFProductRepository>();
builder.Services.AddScoped<ICategoryRepository, EFCategoryRepository>();
builder.Services.AddScoped<IShoppingCartRepository, EFShoppingCartRepository>();
builder.Services.AddScoped<IBannerRepository, EFBannerRepository>();
builder.Services.AddScoped<IVoucherRepository, EFVoucherRepository>();
builder.Services.AddScoped<IHotlineRepository, EFHotlineRepository>();

builder.Services.AddAuthentication()
    .AddGoogle(googleOptions =>
    {
    // Đọc thông tin Authentication:Google từ appsettings.json
    IConfigurationSection googleAuthNSection = builder.Configuration.GetSection("Authentication:Google");

    // Thiết lập ClientID và ClientSecret để truy cập API google
    googleOptions.ClientId = googleAuthNSection["ClientId"];
    googleOptions.ClientSecret = googleAuthNSection["ClientSecret"];
    /*    // Cấu hình Url callback lại từ Google (không thiết lập thì mặc định là /signin-google)
        googleOptions.CallbackPath = "/signin-google";*/

    })
    .AddFacebook(facebookOptions => {
    // Đọc cấu hình
    IConfigurationSection facebookAuthNSection = builder.Configuration.GetSection("Authentication:Facebook");
    facebookOptions.AppId = facebookAuthNSection["AppId"];
    facebookOptions.AppSecret = facebookAuthNSection["AppSecret"];
   /* // Thiết lập đường dẫn Facebook chuyển hướng đến
    facebookOptions.CallbackPath = "/signin-facebook";*/
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
}


app.UseStaticFiles();
app.UseSession();
app.UseRouting();

app.UseAuthorization();
app.MapRazorPages();

app.MapControllerRoute(
        name: "admin",
        pattern: "{area:exists}/{controller=Product}/{action=Index}/{id?}"
    );

app.UseEndpoints(endpoints =>
{
    _ = endpoints.MapControllerRoute(
        name: "Chuot",
         pattern: "Chuot",
        defaults: new { controller = "Product", action = "ChuotList" });
    _ = endpoints.MapControllerRoute(
       name: "TaiNghe",
        pattern: "TaiNghe",
       defaults: new { controller = "Product", action = "TaiNgheList" });
    _ = endpoints.MapControllerRoute(
       name: "LapTop",
        pattern: "LapTop",
       defaults: new { controller = "Product", action = "LapTopList" });
    _ = endpoints.MapControllerRoute(
       name: "ManHinh",
        pattern: "ManHinh",
       defaults: new { controller = "Product", action = "ManHinhList" });
    _ = endpoints.MapControllerRoute(
      name: "BanPHim",
       pattern: "BanPHim",
      defaults: new { controller = "Product", action = "BanPhimList" });
    _ =endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});



app.Run();

