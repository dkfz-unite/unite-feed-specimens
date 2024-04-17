using Unite.Indices.Context;
using Unite.Indices.Entities.Specimens;

namespace Unite.Specimens.Indices.Services;

public class SpecimenIndexRemovalService
{
    private readonly IIndexService<SpecimenIndex> _indexService;


    public SpecimenIndexRemovalService(IIndexService<SpecimenIndex> indexService)
    {
        _indexService = indexService;
    }


    public void DeleteIndex(object key)
    {
        var id = key.ToString();
        
        _indexService.Delete(id);
    }
}
