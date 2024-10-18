using Unite.Cache.Configuration.Options;
using Unite.Cache.Repositories;
using Unite.Specimens.Feed.Web.Models.Specimens;

namespace Unite.Specimens.Feed.Web.Submissions.Repositories;

public class OrganoidsSubmissionRepository : CacheRepository<OrganoidModel[]>
{
    public override string DatabaseName => "submissions";
    public override string CollectionName => "org";

    public OrganoidsSubmissionRepository(IMongoOptions options) : base(options)
    {
    }
}