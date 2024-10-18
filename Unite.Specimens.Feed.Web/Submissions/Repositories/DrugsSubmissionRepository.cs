using Unite.Cache.Configuration.Options;
using Unite.Cache.Repositories;
using Unite.Specimens.Feed.Web.Models.Base;
using Unite.Specimens.Feed.Web.Models.Drugs;

namespace Unite.Specimens.Feed.Web.Submissions.Repositories;

public class DrugsSubmissionRepository : CacheRepository<AnalysisModel<DrugScreeningModel>>
{
    public override string DatabaseName => "submissions";
    public override string CollectionName => "spe-drg";

    public DrugsSubmissionRepository(IMongoOptions options) : base(options)
    {
    }
}