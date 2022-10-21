using Microsoft.EntityFrameworkCore;
using Unite.Data.Entities.Donors;
using Unite.Data.Entities.Genome.Variants;
using Unite.Data.Entities.Images;
using Unite.Data.Entities.Specimens;
using Unite.Data.Entities.Specimens.Tissues.Enums;
using Unite.Data.Services;
using Unite.Data.Services.Extensions;
using Unite.Indices.Entities.Specimens;
using Unite.Indices.Services;
using Unite.Specimens.Indices.Services.Extensions;
using Unite.Specimens.Indices.Services.Mappers;

using CNV = Unite.Data.Entities.Genome.Variants.CNV;
using SSM = Unite.Data.Entities.Genome.Variants.SSM;
using SV = Unite.Data.Entities.Genome.Variants.SV;

namespace Unite.Specimens.Indices.Services;

public class SpecimenIndexCreationService : IIndexCreationService<SpecimenIndex>
{
    private readonly DomainDbContext _dbContext;
    private readonly VariantIndexMapper _variantIndexMapper;
    private readonly DonorIndexMapper _donorIndexMapper;
    private readonly ImageIndexMapper _imageIndexMapper;
    private readonly SpecimenIndexMapper _specimenIndexMapper;


    public SpecimenIndexCreationService(DomainDbContext dbContext)
    {
        _dbContext = dbContext;
        _variantIndexMapper = new VariantIndexMapper();
        _donorIndexMapper = new DonorIndexMapper();
        _imageIndexMapper = new ImageIndexMapper();
        _specimenIndexMapper = new SpecimenIndexMapper();
    }


    public SpecimenIndex CreateIndex(object key)
    {
        var specimenId = (int)key;

        return CreateSpecimenIndex(specimenId);
    }


    private SpecimenIndex CreateSpecimenIndex(int specimenId)
    {
        var specimen = LoadSpecimen(specimenId);

        var diagnosisDate = specimen.Donor.ClinicalData?.DiagnosisDate;

        if (specimen == null)
        {
            return null;
        }

        var index = CreateSpecimenIndex(specimen, diagnosisDate);

        return index;
    }

    private SpecimenIndex CreateSpecimenIndex(Specimen specimen, DateOnly? diagnosisDate)
    {
        var isTumorTissue = specimen.Tissue?.TypeId == TissueType.Tumor;

        var index = new SpecimenIndex();

        _specimenIndexMapper.Map(specimen, index, diagnosisDate);

        index.Parent = CreateParentSpecimenIndex(specimen.Id, diagnosisDate);

        index.Children = CreateChildSpecimenIndices(specimen.Id, diagnosisDate);

        index.Donor = CreateDonorIndex(specimen.DonorId);

        index.Images = CreateImageIndices(index.Donor.Id, diagnosisDate, isTumorTissue);

        index.Variants = CreateVariantIndices(specimen.Id);

        index.NumberOfGenes += index.Variants
            .Where(mutation => mutation.AffectedTranscripts != null)
            .SelectMany(mutation => mutation.AffectedTranscripts)
            .DistinctBy(affectedTranscript => affectedTranscript.Gene.Id)
            .Count();

        index.NumberOfMutations = index.Variants
            .Where(variant => variant.Mutation != null)
            .DistinctBy(variant => variant.Id)
            .Count();

        index.NumberOfCopyNumberVariants = index.Variants
            .Where(variant => variant.CopyNumberVariant != null)
            .DistinctBy(variant => variant.Id)
            .Count();

        index.NumberStructuralVariants = index.Variants
            .Where(variant => variant.StructuralVariant != null)
            .DistinctBy(variant => variant.Id)
            .Count();

        index.NumberOfDrugs = _dbContext.Set<DrugScreening>()
            .Where(screening => screening.SpecimenId == specimen.Id)
            .DistinctBy(screening => screening.DrugId)
            .Count();

        return index;
    }

    private Specimen LoadSpecimen(int specimenId)
    {
        var specimen = _dbContext.Set<Specimen>()
            .Include(specimen => specimen.Donor).ThenInclude(donor => donor.ClinicalData) // Required for diagnosis date
            .IncludeTissue()
            .IncludeCellLine()
            .IncludeOrganoid()
            .IncludeXenograft()
            .IncludeMolecularData()
            .IncludeDrugScreeningData()
            .FirstOrDefault(specimen => specimen.Id == specimenId);

        return specimen;
    }


    private SpecimenIndex CreateParentSpecimenIndex(int specimeId, DateOnly? diagnosisDate)
    {
        var specimen = LoadParentSpecimen(specimeId);

        if (specimen == null)
        {
            return null;
        }

        var index = CreateParentSpecimenIndex(specimen, diagnosisDate);

        return index;
    }

    private SpecimenIndex CreateParentSpecimenIndex(Specimen specimen, DateOnly? diagnosisDate)
    {
        var index = new SpecimenIndex();

        _specimenIndexMapper.Map(specimen, index, diagnosisDate);

        return index;
    }

    private Specimen LoadParentSpecimen(int specimeId)
    {
        var specimen = _dbContext.Set<Specimen>()
            .IncludeParentTissue()
            .IncludeParentCellLine()
            .IncludeParentOrganoid()
            .IncludeParentXenograft()
            .FirstOrDefault(specimen => specimen.Id == specimeId).Parent;

        return specimen;
    }


    private SpecimenIndex[] CreateChildSpecimenIndices(int specimenId, DateOnly? diagnosisDate)
    {
        var specimens = LoadChildSpecimens(specimenId);

        if (specimens == null)
        {
            return null;
        }

        var indices = specimens
            .Select(specimen => CreateChildSpecimenIndex(specimen, diagnosisDate))
            .ToArray();

        return indices;
    }

    private SpecimenIndex CreateChildSpecimenIndex(Specimen specimen, DateOnly? diagnosisDate)
    {
        var index = new SpecimenIndex();

        _specimenIndexMapper.Map(specimen, index, diagnosisDate);

        index.Children = CreateChildSpecimenIndices(specimen.Id, diagnosisDate);

        return index;
    }

    private Specimen[] LoadChildSpecimens(int specimenId)
    {
        var specimens = _dbContext.Set<Specimen>()
            .IncludeTissue()
            .IncludeCellLine()
            .IncludeOrganoid()
            .IncludeXenograft()
            .Where(specimen => specimen.ParentId == specimenId)
            .ToArray();

        return specimens;
    }


    private DonorIndex CreateDonorIndex(int donorId)
    {
        var donor = LoadDonor(donorId);

        if (donor == null)
        {
            return null;
        }

        var index = CreateDonorIndex(donor);

        return index;
    }

    private DonorIndex CreateDonorIndex(Donor donor)
    {
        var index = new DonorIndex();

        var diagnosisDate = donor.ClinicalData?.DiagnosisDate;

        _donorIndexMapper.Map(donor, index);

        return index;
    }

    private Donor LoadDonor(int donorId)
    {
        var donor = _dbContext.Set<Donor>()
            .IncludeClinicalData()
            .IncludeTreatments()
            .IncludeProjects()
            .IncludeStudies()
            .FirstOrDefault(donor => donor.Id == donorId);

        return donor;
    }


    private ImageIndex[] CreateImageIndices(int donorId, DateOnly? diagnosisDate, bool isTumorTissue)
    {
        if (!isTumorTissue)
        {
            return null;
        }

        var images = LoadImages(donorId);

        if (images == null)
        {
            return null;
        }

        var indices = images
            .Select(image => CreateImageIndex(image, diagnosisDate))
            .ToArray();

        return indices;
    }

    private ImageIndex CreateImageIndex(Image image, DateOnly? diagnosisDate)
    {
        var index = new ImageIndex();

        _imageIndexMapper.Map(image, index, diagnosisDate);

        return index;
    }

    private Image[] LoadImages(int donorId)
    {
        var images = _dbContext.Set<Image>()
            .Include(image => image.MriImage)
            .Where(image => image.DonorId == donorId)
            .ToArray();

        return images;
    }


    private VariantIndex[] CreateVariantIndices(int specimenId)
    {
        var mutations = LoadVariants<SSM.Variant, SSM.VariantOccurrence>(specimenId);
        var copyNumberVariants = LoadVariants<CNV.Variant, CNV.VariantOccurrence>(specimenId);
        var structuralVariants = LoadVariants<SV.Variant, SV.VariantOccurrence>(specimenId);

        var indices = new List<VariantIndex>();

        if (mutations != null)
        {
            indices.AddRange(mutations.Select(variant => CreateVariantIndex(variant)));
        }

        if (copyNumberVariants != null)
        {
            indices.AddRange(copyNumberVariants.Select(variant => CreateVariantIndex(variant)));
        }

        if (structuralVariants != null)
        {
            indices.AddRange(structuralVariants.Select(variant => CreateVariantIndex(variant)));
        }

        return indices.Any() ? indices.ToArray() : null;
    }

    private VariantIndex CreateVariantIndex<TVariant>(TVariant variant)
        where TVariant : Variant
    {
        var index = new VariantIndex();

        _variantIndexMapper.Map(variant, index);

        return index;
    }

    private TVariant[] LoadVariants<TVariant, TVariantOccurrence>(int specimenId)
        where TVariant : Variant
        where TVariantOccurrence : VariantOccurrence<TVariant>
    {
        var variantIds = _dbContext.Set<TVariantOccurrence>()
            .Where(occurrence => occurrence.AnalysedSample.Sample.SpecimenId == specimenId)
            .Select(occurrence => occurrence.VariantId)
            .Distinct()
            .ToArray();

        var variants = _dbContext.Set<TVariant>()
            .IncludeAffectedTranscripts()
            .Where(variant => variantIds.Contains(variant.Id))
            .ToArray();

        return variants;
    }
}
