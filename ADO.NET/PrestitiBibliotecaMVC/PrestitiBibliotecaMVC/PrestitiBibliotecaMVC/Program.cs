using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PrestitiBibliotecaMVC.Data;
using PrestitiBibliotecaMVC.Models;
using System;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Logging;

namespace PrestitiBibliotecaMVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            // Legge la connection string dalla variabile d'ambiente CONN_STR, altrimenti dal file di configurazione
            var connectionString = Environment.GetEnvironmentVariable("CONN_STR");
            if (string.IsNullOrEmpty(connectionString))
            {
                connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            }
            // Abilita retry per errori transitori (es. connessioni temporaneamente interrotte)
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString, sqlOptions =>
                    sqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 5,
                        maxRetryDelay: TimeSpan.FromSeconds(10),
                        errorNumbersToAdd: null)));

            //registrazione del contesto PrestitiBibliotecaContext per l'accesso al database
            builder.Services.AddDbContext<PrestitiBibliotecaContext>(options =>
                options.UseSqlServer(connectionString, sqlOptions =>
                    sqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 5,
                        maxRetryDelay: TimeSpan.FromSeconds(10),
                        errorNumbersToAdd: null)));

            // Registrazione DbContext aggiuntivo generato con Database-First (se presente)
            // Assicurarsi che la classe AnagraficaContext sia stata scaffolded nel progetto.
            builder.Services.AddDbContext<AnagraficaContext>(options =>
                options.UseSqlServer(connectionString, sqlOptions =>
                    sqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 5,
                        maxRetryDelay: TimeSpan.FromSeconds(10),
                        errorNumbersToAdd: null)));

            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Logga la connection string mascherata per diagnosticare quale valore è effettivamente usato
            try
            {
                var logger = app.Services.GetRequiredService<ILogger<Program>>();
                var masked = Regex.Replace(connectionString ?? string.Empty, "(Password|Pwd)\\s*=\\s*[^;]+", "$1=***", RegexOptions.IgnoreCase);
                logger.LogInformation("Connection string in uso (mascherata): {Conn}", masked);

                if ((connectionString ?? string.Empty).IndexOf("User Id=bad", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    logger.LogWarning("La connection string contiene l'utente 'bad' che causa l'errore di login. Verificare credenziali o variabili d'ambiente.");
                }
            }
            catch
            {
                // Non rompere l'avvio se il logger non è disponibile
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();
            app.MapRazorPages()
               .WithStaticAssets();

            app.Run();
        }
    }
}
