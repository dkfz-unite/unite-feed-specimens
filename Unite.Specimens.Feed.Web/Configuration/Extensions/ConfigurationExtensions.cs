using System.Collections.Generic;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Unite.Data.Services;
using Unite.Data.Services.Configuration.Options;
using Unite.Indices.Entities.Specimens;
using Unite.Indices.Services;
using Unite.Indices.Services.Configuration.Options;
using Unite.Specimens.Feed.Data.Specimens;
using Unite.Specimens.Feed.Web.Configuration.Options;
using Unite.Specimens.Feed.Web.Handlers;
using Unite.Specimens.Feed.Web.HostedServices;
using Unite.Specimens.Feed.Web.Services.Specimens;
using Unite.Specimens.Feed.Web.Services.Specimens.Validators;
using Unite.Specimens.Feed.Web.Services.Validation;
using Unite.Specimens.Feed.Web.Services;
using Unite.Specimens.Indices.Services;

namespace Unite.Specimens.Feed.Web.Configuration.Extensions
{
    public static class ConfigurationExtensions
    {
        public static void Configure(this IServiceCollection services)
        {
            AddOptions(services);
            AddDatabases(services);
            AddValidation(services);
            AddServices(services);
            AddHostedServices(services);
        }


        private static void AddOptions(IServiceCollection services)
        {
            services.AddTransient<ISqlOptions, SqlOptions>();
            services.AddTransient<IElasticOptions, ElasticOptions>();
        }

        private static void AddDatabases(IServiceCollection services)
        {
            services.AddTransient<DomainDbContext>();
        }

        private static void AddValidation(IServiceCollection services)
        {
            services.AddTransient<IValidationService, ValidationService>();

            services.AddTransient<IValidator<IEnumerable<SpecimenModel>>, SpecimenModelsValidator>();
        }

        private static void AddServices(IServiceCollection services)
        {
            services.AddTransient<SpecimenDataWriter>();

            services.AddTransient<TasksProcessingService>();
            services.AddTransient<DonorIndexingTasksService>();
            services.AddTransient<IIndexCreationService<SpecimenIndex>, SpecimenIndexCreationService>();
            services.AddTransient<IIndexingService<SpecimenIndex>, SpecimensIndexingService>();
        }

        private static void AddHostedServices(IServiceCollection services)
        {
            services.AddTransient<IndexingOptions>();
            services.AddHostedService<DonorsIndexingHostedService>();
            services.AddTransient<DonorsIndexingHandler>();
        }
    }
}
