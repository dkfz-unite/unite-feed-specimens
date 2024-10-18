using Unite.Cache.Configuration.Options;
using Unite.Cache.Repositories;
using Unite.Specimens.Feed.Web.Models.Specimens;

namespace Unite.Specimens.Feed.Web.Submissions.Repositories;

public class MaterialsSubmissionRepository : CacheRepository<MaterialModel[]>
{
    public override string DatabaseName => "submissions";
    public override string CollectionName => "mat";

    public MaterialsSubmissionRepository(IMongoOptions options) : base(options)
    {
    }
}