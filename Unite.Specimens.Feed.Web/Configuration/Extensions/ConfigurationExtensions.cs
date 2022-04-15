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
using Unite.Specimens.Feed.Web.Services;
using Unite.Specimens.Feed.Web.Services.Specimens;
using Unite.Specimens.Feed.Web.Services.Specimens.Validators;
using Unite.Specimens.Indices.Services;

namespace Unite.Specimens.Feed.Web.Configuration.Extensions
{
    public static class ConfigurationExtensions
    {
        public static void Configure(this IServiceCollection services)
        {
            services.AddTransient<ISqlOptions, SqlOptions>();
            services.AddTransient<IElasticOptions, ElasticOptions>();

            services.AddTransient<IValidator<SpecimenModel[]>, SpecimenModelsValidator>();

            services.AddTransient<DomainDbContext>();
            services.AddTransient<SpecimenDataWriter>();

            services.AddTransient<SpecimenIndexingTasksService>();
            services.AddTransient<TasksProcessingService>();

            services.AddHostedService<SpecimensIndexingHostedService>();
            services.AddTransient<SpecimensIndexingOptions>();
            services.AddTransient<SpecimensIndexingHandler>();
            services.AddTransient<IIndexCreationService<SpecimenIndex>, SpecimenIndexCreationService>();
            services.AddTransient<IIndexingService<SpecimenIndex>, SpecimensIndexingService>();
        }
    }
}
