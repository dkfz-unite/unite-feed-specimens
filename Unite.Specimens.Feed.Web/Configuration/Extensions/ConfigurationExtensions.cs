using FluentValidation;
using Unite.Data.Context.Configuration.Extensions;
using Unite.Data.Context.Configuration.Options;
using Unite.Data.Context.Services.Tasks;
using Unite.Indices.Context.Configuration.Extensions;
using Unite.Indices.Context.Configuration.Options;
using Unite.Specimens.Indices.Services;
using Unite.Specimens.Feed.Data;
using Unite.Specimens.Feed.Web.Configuration.Options;
using Unite.Specimens.Feed.Web.Handlers;
using Unite.Specimens.Feed.Web.HostedServices;
using Unite.Specimens.Feed.Web.Models;
using Unite.Specimens.Feed.Web.Models.Validators;
using Unite.Specimens.Feed.Web.Services;


namespace Unite.Specimens.Feed.Web.Configuration.Extensions;

public static class ConfigurationExtensions
{
    public static void Configure(this IServiceCollection services)
    {
        var sqlOptions = new SqlOptions();

        services.AddOptions();
        services.AddDatabase();
        services.AddDatabaseFactory(sqlOptions);
        services.AddRepositories();
        services.AddIndexServices();
        services.AddValidators();

        services.AddTransient<SpecimensDataWriter>();
        services.AddTransient<SpecimensDataRemover>();
        services.AddTransient<InterventionsDataWriter>();
        services.AddTransient<DrugScreeningsDataWriter>();

        services.AddTransient<SpecimenIndexingTasksService>();
        services.AddTransient<TasksProcessingService>();

        services.AddHostedService<SpecimensIndexingHostedService>();
        services.AddTransient<SpecimensIndexingOptions>();
        services.AddTransient<SpecimensIndexingHandler>();
        services.AddTransient<SpecimenIndexCreationService>();
        services.AddTransient<SpecimenIndexRemovalService>();
    }


    private static IServiceCollection AddOptions(this IServiceCollection services)
    {
        services.AddTransient<ApiOptions>();
        services.AddTransient<ISqlOptions, SqlOptions>();
        services.AddTransient<IElasticOptions, ElasticOptions>();

        return services;
    }

    private static IServiceCollection AddValidators(this IServiceCollection services)
    {
        services.AddTransient<IValidator<SpecimenDataModel[]>, SpecimenModelsValidator>();
        services.AddTransient<IValidator<InterventionsDataModel[]>, InterventionsDataModelsValidator>();
        services.AddTransient<IValidator<InterventionDataFlatModel[]>, InterventionDataFlatModelsValidator>();
        services.AddTransient<IValidator<DrugScreeningsDataModel[]>, DrugScreeningsDataModelsValidator>();
        services.AddTransient<IValidator<DrugScreeningDataFlatModel[]>, DrugScreeningDataFlatModelsValidator>();

        return services;
    }
}
