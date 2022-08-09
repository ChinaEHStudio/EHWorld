using EHWorld.Data;
using Microsoft.EntityFrameworkCore;

//var MyAllowSpecificOrigins = "_myAllowSpecificOrigins"; 
var builder = WebApplication.CreateBuilder(args); 
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
                      policy =>
                      {
                          policy.AllowAnyMethod().AllowCredentials().WithOrigins("http://localhost:3000");
                          policy.AllowAnyMethod().AllowCredentials().WithOrigins("https://ehworld-web.vercel.app");

                      });
});


builder.Services.AddDbContext<EhWorldContext>(options =>
  options.UseMySql("Server=o4fj9i0lc4qk.us-east-1.psdb.cloud;Database=dblearn;user=j1u8i371zwas;password=pscale_pw_89K_ePK7QK5PArNrgUz27yQN_9FAkNS3hFgbI2H13K8;SslMode=VerifyFull;", new MySqlServerVersion(new Version(8,0,23))));
//var connectstring = builder.Configuration.GetConnectionString("EHWorldContext");

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
    app.UseMigrationsEndPoint();
}
app.UseCors();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<EhWorldContext>();
    context.Database.EnsureCreated();
    // DbInitializer.Initialize(context);
}

//app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();


app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
