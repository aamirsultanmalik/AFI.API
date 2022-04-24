using AFI.Application.Services.CustomerReg;
using AFI.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AFI.Tests;

public class DependencySetupFixture
{
    public DependencySetupFixture()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddDbContext<IAFIDbContext, AFIDbContext>(options => options.UseSqlite("Data Source=AFI.db"));
        serviceCollection.AddTransient<ICustomerService, CustomerService>();


        ServiceProvider = serviceCollection.BuildServiceProvider();
    }

    public ServiceProvider ServiceProvider { get; private set; }
}