using Nest;
using Unite.Cache.Configuration.Options;
using Unite.Cache.Repositories;
using Unite.Specimens.Feed.Web.Models.Specimens;

namespace Unite.Specimens.Feed.Web.Submissions.Repositories;

public class LinesSubmissionRepository : CacheRepository<LineModel[]>
{
    public override string DatabaseName => "submissions";
    public override string CollectionName => "lne";

    public LinesSubmissionRepository(IMongoOptions options) : base(options)
    {
    }
}