using Unite.Data.Entities.Donors;
using Unite.Data.Entities.Images;
using Unite.Data.Entities.Specimens;
using Unite.Data.Services;
using Unite.Data.Services.Tasks;

using CNV = Unite.Data.Entities.Genome.Variants.CNV;
using SSM = Unite.Data.Entities.Genome.Variants.SSM;
using SV = Unite.Data.Entities.Genome.Variants.SV;

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
            CreateVariantIndexingTasks(specimens);
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
            .Select(image => image.Id)
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
        var ssmAffectedGeneIds = _dbContext.Set<SSM.VariantOccurrence>()
            .Where(occurrence => keys.Contains(occurrence.AnalysedSample.Sample.SpecimenId))
            .SelectMany(occurrence => occurrence.Variant.AffectedTranscripts)
            .Where(affectedTranscript => affectedTranscript.Feature.GeneId != null)
            .Select(affectedTranscript => affectedTranscript.Feature.GeneId.Value)
            .Distinct()
            .ToArray();

        var cnvAffectedGeneIds = _dbContext.Set<CNV.VariantOccurrence>()
            .Where(occurrence => keys.Contains(occurrence.AnalysedSample.Sample.SpecimenId))
            .SelectMany(occurrence => occurrence.Variant.AffectedTranscripts)
            .Where(affectedTranscript => affectedTranscript.Feature.GeneId != null)
            .Select(affectedTranscript => affectedTranscript.Feature.GeneId.Value)
            .Distinct()
            .ToArray();

        var svAffectedGeneIds = _dbContext.Set<SV.VariantOccurrence>()
            .Where(occurrence => keys.Contains(occurrence.AnalysedSample.Sample.SpecimenId))
            .SelectMany(occurrence => occurrence.Variant.AffectedTranscripts)
            .Where(affectedTranscript => affectedTranscript.Feature.GeneId != null)
            .Select(affectedTranscript => affectedTranscript.Feature.GeneId.Value)
            .Distinct()
            .ToArray();

        var geneIds = ssmAffectedGeneIds.Union(cnvAffectedGeneIds).Union(svAffectedGeneIds).ToArray();

        return geneIds;
    }

    protected override IEnumerable<long> LoadRelatedMutations(IEnumerable<int> keys)
    {
        var variantIds = _dbContext.Set<SSM.VariantOccurrence>()
            .Where(occurrence => keys.Contains(occurrence.AnalysedSample.Sample.SpecimenId))
            .Select(occurrence => occurrence.VariantId)
            .Distinct()
            .ToArray();

        return variantIds;
    }

    protected override IEnumerable<long> LoadRelatedCopyNumberVariants(IEnumerable<int> keys)
    {
        var variantIds = _dbContext.Set<CNV.VariantOccurrence>()
           .Where(occurrence => keys.Contains(occurrence.AnalysedSample.Sample.SpecimenId))
           .Select(occurrence => occurrence.VariantId)
           .Distinct()
           .ToArray();

        return variantIds;
    }

    protected override IEnumerable<long> LoadRelatedStructuralVariants(IEnumerable<int> keys)
    {
        var variantIds = _dbContext.Set<SV.VariantOccurrence>()
           .Where(occurrence => keys.Contains(occurrence.AnalysedSample.Sample.SpecimenId))
           .Select(occurrence => occurrence.VariantId)
           .Distinct()
           .ToArray();

        return variantIds;
    }
}
