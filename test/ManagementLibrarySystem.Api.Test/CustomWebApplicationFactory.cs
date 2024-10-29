using ManagementLibrarySystem.Infrastructure.EFCore.DB;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ManagementLibrarySystem.Api.Test;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {

            ServiceDescriptor? descriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<DbAppContext>));
            if (descriptor != null)
            {
                services.Remove(descriptor);
            }

            services.AddDbContext<DbAppContext>(options =>
            {
                options.UseInMemoryDatabase("InMemoryDbForTesting");
            });


            ServiceProvider sp = services.BuildServiceProvider();
            using (IServiceScope scope = sp.CreateScope())
            {
                DbAppContext db = scope.ServiceProvider.GetRequiredService<DbAppContext>();
                db.Database.EnsureCreated();
            }
        });
    }

}
