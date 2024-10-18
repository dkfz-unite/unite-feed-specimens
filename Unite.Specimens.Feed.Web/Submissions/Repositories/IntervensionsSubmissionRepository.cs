using Unite.Cache.Configuration.Options;
using Unite.Cache.Repositories;
using Unite.Specimens.Feed.Web.Models.Specimens;

namespace Unite.Specimens.Feed.Web.Submissions.Repositories;

public class IntervensionsSubmissionRepository : CacheRepository<InterventionsModel[]>
{
    public override string DatabaseName => "submissions";
    public override string CollectionName => "spe-int";

    public IntervensionsSubmissionRepository(IMongoOptions options) : base(options)
    {
    }
}