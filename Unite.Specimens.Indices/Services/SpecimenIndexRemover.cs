using Unite.Indices.Context;
using Unite.Indices.Entities.Specimens;

namespace Unite.Specimens.Indices.Services;

public class SpecimenIndexRemover
{
    private readonly IIndexService<SpecimenIndex> _indexService;


    public SpecimenIndexRemover(IIndexService<SpecimenIndex> indexService)
    {
        _indexService = indexService;
    }


    public void DeleteIndex(object key)
    {
        var id = key.ToString();
        
        _indexService.Delete(id);
    }
}
