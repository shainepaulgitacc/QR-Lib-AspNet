using AutoMapper;
using HR_Application_Asp.Net.Models.Infrastructure.Services;
using LibraryManagement.Data;
using LibraryManagement.Model.Domain;
using LibraryManagement.Model.Infrastracture.Contracts;
using LibraryManagement.Model.Infrastracture.Implementations;
using LibraryManagement.Model.Infrastracture.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
internal class Program
{
    private static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        var EmailSettingsConfig = builder.Configuration.GetSection("EmailSettings");
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString));
        builder.Services.AddAutoMapper(typeof(ModelMapper));
        builder.Services.AddSignalR();
        builder.Services.Configure<EmailSettings>(EmailSettingsConfig);
        builder.Services.AddScoped<EmailSender>();
        builder.Services.AddScoped<QRCode_Generator>();
        builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(DbBaseRepository<>));
        builder.Services.AddScoped<IUserRepository, DbUserRepository>();
        builder.Services.AddScoped<IUserAttendanceRepository, DbUserAttendanceRepository>();
        builder.Services.AddScoped<IEmployeeRepository, DbEmployeeRepository>();
        builder.Services.AddScoped<IEmployeeAttendanceRepository, DbEmployeeAttendanceRepository>();
        builder.Services.AddScoped<IBookCategoryRepository, DbBookCategoryRepository>();
        builder.Services.AddScoped<IBorrowBookRepository, DbBorrowBookRepository>();
        builder.Services.AddScoped<IBookRepository, BookRepository>();
        builder.Services.AddDatabaseDeveloperPageExceptionFilter();
        builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddEntityFrameworkStores<ApplicationDbContext>();
        builder.Services.AddRazorPages();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseMigrationsEndPoint();
        }

        else
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        using (var  scope = app.Services.CreateScope())
        {
            var user = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var accountRepo = scope.ServiceProvider.GetRequiredService<IBaseRepository<IdentityUser>>();
            var accounts = await accountRepo.GetAllRecords();

            var admin = new IdentityUser()
            {
                EmailConfirmed = true,
                Email = "admin@gmail.com",
                UserName = "admin@gmail.com",
            };
            if(accounts.Count() == 0)
            {
                await user.CreateAsync(admin,"@AdminPassword01");
            }
        }
        app.MapHub<BaseHub>("/basehub");
        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthorization();
        app.MapRazorPages();
        app.Run();
    }
}