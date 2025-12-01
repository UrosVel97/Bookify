using Bookify.Application.Abstractions.Behaviors;
using Bookify.Domain.Bookings;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookify.Application;

public static class DependecyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(typeof(DependecyInjection).Assembly);

            configuration.AddOpenBehavior(typeof(LoggingBehavior<,>));

        });

        services.AddTransient<PricingService>();


        return services;
    }

}
