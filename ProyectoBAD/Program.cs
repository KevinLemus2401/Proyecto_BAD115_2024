using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProyectoBAD.Models;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();



builder.Services.AddDbContext<sisencuestasContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("PubContext")));

builder.Services.AddIdentity<Usuario, Roles>(
        options =>
        {
            options.Password.RequireNonAlphanumeric = false;
            options.SignIn.RequireConfirmedEmail = false;
            options.SignIn.RequireConfirmedPhoneNumber = false;
            options.User.RequireUniqueEmail = true;
        }
    )
    .AddEntityFrameworkStores<sisencuestasContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("PermissionReadUser", policy => policy.RequireAssertion(context =>
        context.User.HasClaim("Permission", "ReadUser") || context.User.HasClaim("Permission", "UserRoot")));
    options.AddPolicy("PermissionWriteUser", policy => policy.RequireAssertion(context =>
        context.User.HasClaim("Permission", "WriteUser") || context.User.HasClaim("Permission", "UserRoot")));
    options.AddPolicy("PermissionDeleteUser", policy => policy.RequireAssertion(context =>
            context.User.HasClaim("Permission", "DeleteUser") || context.User.HasClaim("Permission", "UserRoot")));
    options.AddPolicy("PermissionRoleUser", policy => policy.RequireAssertion(context =>
            context.User.HasClaim("Permission", "UserRoles") || context.User.HasClaim("Permission", "UserRoot")));
    options.AddPolicy("PermissionReadRole", policy => policy.RequireAssertion(context =>
            context.User.HasClaim("Permission", "ReadRole") || context.User.HasClaim("Permission", "UserRoot")));
    options.AddPolicy("PermissionWriteRole", policy => policy.RequireAssertion(context =>
            context.User.HasClaim("Permission", "WriteRole") || context.User.HasClaim("Permission", "UserRoot")));
    options.AddPolicy("PermissionDeleteRole", policy => policy.RequireAssertion(context =>
            context.User.HasClaim("Permission", "DeleteRole") || context.User.HasClaim("Permission", "UserRoot")));
});

builder.Services.ConfigureApplicationCookie(options =>
{
    // Cookie settings
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(30);

    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/AccessDenied";
    options.SlidingExpiration = true;
});

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


using (var scope = app.Services.CreateScope())
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<Usuario>>();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Roles>>();

    // Crear el rol de administrador si no existe
    var adminRoleExists = await roleManager.RoleExistsAsync("Administrador");
    if (!adminRoleExists)
    {
        await roleManager.CreateAsync(new Roles("Administrador", "Administrador de la pagina web"));
    }

    var userExists = await userManager.FindByNameAsync("admin");
    if (userExists == null)
    {
        var user = new Usuario { UserName = "admin", Email = "admin@example.com", PrimerNombreUsuario = "default", PrimerApellidoUsuario = "default", EmailUsuario = "admin@example.com", GenUsuario = "M" };
        var result = await userManager.CreateAsync(user, "defaultPassword123");

        if (result.Succeeded)
        {
            // Agregar el claim al usuario
            await userManager.AddClaimAsync(user, new Claim("Permission", "UserRoot"));

            // Asignar el rol de administrador al usuario
            await userManager.AddToRoleAsync(user, "Administrador");

            // Agregar claims al rol de administrador
            var adminRole = await roleManager.FindByNameAsync("Administrador");
            if (adminRole != null)
            {
                await roleManager.AddClaimAsync(adminRole, new Claim("Permission", "UserRoot"));
                // Agregar más claims según sea necesario
            }
        }
    }
}
app.Run();