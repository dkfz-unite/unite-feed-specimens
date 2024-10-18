using FluentValidation;
using Unite.Cache.Configuration.Options;
using Unite.Data.Context.Configuration.Extensions;
using Unite.Data.Context.Configuration.Options;
using Unite.Data.Context.Services.Tasks;
using Unite.Indices.Context.Configuration.Extensions;
using Unite.Indices.Context.Configuration.Options;
using Unite.Specimens.Indices.Services;
using Unite.Specimens.Feed.Data;
using Unite.Specimens.Feed.Web.Configuration.Options;
using Unite.Specimens.Feed.Web.Handlers;
using Unite.Specimens.Feed.Web.Workers;
using Unite.Specimens.Feed.Web.Services;
using Unite.Specimens.Feed.Web.Models.Specimens.Validators;
using Unite.Specimens.Feed.Web.Models.Specimens;
using Unite.Specimens.Feed.Web.Models.Base;
using Unite.Specimens.Feed.Web.Models.Base.Validators;


using DrugScreeningModel = Unite.Specimens.Feed.Web.Models.Drugs.DrugScreeningModel;
using DrugScreeningModelValidator = Unite.Specimens.Feed.Web.Models.Drugs.Validators.DrugScreeningModelValidator;
using Unite.Specimens.Feed.Web.Handlers.Submission;
using Unite.Specimens.Feed.Web.Submissions;

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

        services.AddTransient<SpecimensWriter>();
        services.AddTransient<SpecimensRemover>();
        services.AddTransient<InterventionsWriter>();
        services.AddTransient<AnalysisWriter>();

        services.AddTransient<SpecimensSubmissionService>();
        services.AddTransient<TasksProcessingService>();

        services.AddTransient<SubmissionTaskService>();
        services.AddTransient<SpecimenIndexingTasksService>();

        services.AddHostedService<SpecimensIndexingWorker>();
        services.AddTransient<SpecimensIndexingOptions>();
        services.AddTransient<SpecimensIndexingHandler>();
        services.AddTransient<SpecimenIndexCreator>();
        services.AddTransient<SpecimenIndexRemover>();

        // Submissions hosted services
        services.AddHostedService<SubmissionsWorker>();
        services.AddTransient<MaterialsSubmissionHandler>();
        services.AddTransient<LinesSubmissionHandler>();
        services.AddTransient<OrganoidsSubmissionHandler>();
        services.AddTransient<XenograftsSubmissionHandler>();
        services.AddTransient<IntervensionsSubmissionHandler>();
        services.AddTransient<DrugsSubmissionHandler>();
    }


    private static IServiceCollection AddOptions(this IServiceCollection services)
    {
        services.AddTransient<ApiOptions>();
        services.AddTransient<ISqlOptions, SqlOptions>();
        services.AddTransient<IMongoOptions, MongoOptions>();
        services.AddTransient<IElasticOptions, ElasticOptions>();

        return services;
    }

    private static IServiceCollection AddValidators(this IServiceCollection services)
    {
        services.AddTransient<IValidator<MaterialModel[]>, MaterialModelsValidator>();
        services.AddTransient<IValidator<LineModel[]>, LineModelsValidator>();
        services.AddTransient<IValidator<OrganoidModel[]>, OrganoidModelsValidator>();
        services.AddTransient<IValidator<XenograftModel[]>, XenograftModelsValidator>();
        services.AddTransient<IValidator<InterventionsModel[]>, InterventionsModelsValidator>();
        services.AddTransient<IValidator<AnalysisModel<DrugScreeningModel>>, AnalysisModelValidator<DrugScreeningModel, DrugScreeningModelValidator>>();

        return services;
    }
}
