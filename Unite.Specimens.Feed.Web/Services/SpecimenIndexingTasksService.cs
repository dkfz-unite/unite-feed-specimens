using Unite.Data.Entities.Donors;
using Unite.Data.Entities.Genome.Mutations;
using Unite.Data.Entities.Images;
using Unite.Data.Entities.Specimens;
using Unite.Data.Services;
using Unite.Data.Services.Tasks;

namespace Unite.Specimens.Feed.Web.Services;

public class SpecimenIndexingTasksService : IndexingTaskService<Donor, int>
{
    protected override int BucketSize => 1000;


    public SpecimenIndexingTasksService(DomainDbContext dbContext) : base(dbContext)
    {
    }


    public override void CreateTasks()
    {
        IterateEntities<Specimen, int>(specimen => true, specimen => specimen.Id, specimens =>
        {
            CreateSpecimenIndexingTasks(specimens);
        });
    }

    public override void CreateTasks(IEnumerable<int> specimenIds)
    {
        IterateEntities<Specimen, int>(specimen => specimenIds.Contains(specimen.Id), specimen => specimen.Id, specimens =>
        {
            CreateSpecimenIndexingTasks(specimens);
        });
    }

    public override void PopulateTasks(IEnumerable<int> specimenIds)
    {
        IterateEntities<Specimen, int>(specimen => specimenIds.Contains(specimen.Id), specimen => specimen.Id, specimens =>
        {
            CreateDonorIndexingTasks(specimens);
            CreateImageIndexingTasks(specimens);
            CreateSpecimenIndexingTasks(specimens);
            CreateMutationIndexingTasks(specimens);
            CreateGeneIndexingTasks(specimens);
        });
    }


    protected override IEnumerable<int> LoadRelatedDonors(IEnumerable<int> keys)
    {
        var donorIds = _dbContext.Set<Specimen>()
            .Where(specimen => keys.Contains(specimen.Id))
            .Select(specimen => specimen.DonorId)
            .Distinct()
            .ToArray();

        return donorIds;
    }

    protected override IEnumerable<int> LoadRelatedImages(IEnumerable<int> keys)
    {
        var donorIds = _dbContext.Set<Specimen>()
            .Where(specimen => keys.Contains(specimen.Id))
            .Select(specimen => specimen.DonorId)
            .Distinct()
            .ToArray();

        var imageIds = _dbContext.Set<Image>()
            .Where(image => donorIds.Contains(image.DonorId))
            .Select(image => image.DonorId)
            .Distinct()
            .ToArray();

        return imageIds;
    }

    protected override IEnumerable<int> LoadRelatedSpecimens(IEnumerable<int> keys)
    {
        var donorIds = _dbContext.Set<Specimen>()
            .Where(specimen => keys.Contains(specimen.Id))
            .Select(specimen => specimen.DonorId)
            .Distinct()
            .ToArray();

        var relatedSpecimenIds = _dbContext.Set<Specimen>()
            .Where(specimen => donorIds.Contains(specimen.DonorId))
            .Select(specimen => specimen.Id)
            .Distinct()
            .ToArray();

        return relatedSpecimenIds;
    }

    protected override IEnumerable<int> LoadRelatedGenes(IEnumerable<int> keys)
    {
        var mutationIds = _dbContext.Set<MutationOccurrence>()
            .Where(mutationOccurrence => keys.Contains(mutationOccurrence.AnalysedSample.Sample.SpecimenId))
            .Select(mutationOccurrence => mutationOccurrence.MutationId)
            .Distinct()
            .ToArray();

        var geneIds = _dbContext.Set<AffectedTranscript>()
            .Where(affectedTranscript => mutationIds.Contains(affectedTranscript.MutationId))
            .Where(affectedTranscript => affectedTranscript.Transcript.GeneId != null)
            .Select(affectedTranscript => affectedTranscript.Transcript.GeneId.Value)
            .Distinct()
            .ToArray();

        return geneIds;
    }

    protected override IEnumerable<long> LoadRelatedMutations(IEnumerable<int> keys)
    {
        var mutationIds = _dbContext.Set<MutationOccurrence>()
            .Where(mutationOccurrence => keys.Contains(mutationOccurrence.AnalysedSample.Sample.SpecimenId))
            .Select(mutationOccurrence => mutationOccurrence.MutationId)
            .Distinct()
            .ToArray();

        return mutationIds;
    }
}
