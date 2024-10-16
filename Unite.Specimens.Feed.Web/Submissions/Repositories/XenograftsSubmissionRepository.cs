using Unite.Cache.Configuration.Options;
using Unite.Cache.Repositories;
using Unite.Specimens.Feed.Web.Models.Specimens;

namespace Unite.Specimens.Feed.Web.Submissions.Repositories;

public class XenograftsSubmissionRepository : CacheRepository<XenograftModel[]>
{
    public override string DatabaseName => "submissions";
    public override string CollectionName => "xen";

    public XenograftsSubmissionRepository(IMongoOptions options) : base(options)
    {
    }
}