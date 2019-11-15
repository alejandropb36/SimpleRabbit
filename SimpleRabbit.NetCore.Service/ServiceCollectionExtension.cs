﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;

namespace SimpleRabbit.NetCore.Service
{
    public static class ServiceCollectionExtension
    {

        /// <summary>
        /// Register a <see cref="IHostedService"/> that will call <see cref="IMessageHandler"/> to process messages
        /// </summary>
        /// <param name="services"> the <see cref="IServiceCollection"/> to register the subscriber service to </param>
        /// <returns>the <see cref="IServiceCollection"/></returns>
        /// <remarks>
        /// Relies On <see cref="QueueConfiguration"/> to be configured before this method, 
        /// which can be done via <see cref="AddSubscriberConfiguration(IServiceCollection, IConfigurationSection)"/>
        /// </remarks>
        public static IServiceCollection AddSubscriberServices(this IServiceCollection services)
        {
            return services
                .AddHostedService<QueueFactory>()
                .AddTransient<IQueueService, QueueService>();
        }

        /// <summary>
        /// configure the List <see cref="List{T}"/> of <see cref="QueueConfiguration"/> to <see cref="IServiceCollection"/>
        /// </summary>
        /// <param name="services">the <see cref="IServiceCollection"/> to configure</param>
        /// <param name="config">the configuration section to use</param>
        /// <returns>the <see cref="IServiceCollection"/></returns>
        public static IServiceCollection AddSubscriberConfiguration(this IServiceCollection services, IConfigurationSection config, string name="")
        {
            // This will only be instaniated and NEVER updated
            services.Configure<List<Subscribers>>(s=>s.Add(new Subscribers { Name = name}));

            return services.Configure<SubscriberConfiguration>(name,config);
        }
    }
}
