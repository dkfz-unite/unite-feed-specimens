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
    private readonly DonorIndexMapper _donorIndexMapper;
    private readonly ImageIndexMapper _imageIndexMapper;
    private readonly SpecimenIndexMapper _specimenIndexMapper;


    public SpecimenIndexCreationService(DomainDbContext dbContext)
    {
        _dbContext = dbContext;
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

        var stats = LoadGenomicStats(specimen.Id);

        var index = new SpecimenIndex();

        _specimenIndexMapper.Map(specimen, index, diagnosisDate);

        index.Parent = CreateParentSpecimenIndex(specimen.Id, diagnosisDate);
        index.Donor = CreateDonorIndex(specimen.DonorId);
        index.Images = CreateImageIndices(index.Donor.Id, diagnosisDate, isTumorTissue);
        index.NumberOfGenes = stats.NumberOfGenes;
        index.NumberOfMutations = stats.NumberOfMutations;
        index.NumberOfCopyNumberVariants = stats.NumberOfCopyNumberVariants;
        index.NumberOfStructuralVariants = stats.NumberOfStructuralVariants;
        
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


    private record GenomicStats(int NumberOfGenes, int NumberOfMutations, int NumberOfCopyNumberVariants, int NumberOfStructuralVariants);

    private GenomicStats LoadGenomicStats(int specimenId)
    {
        var ssmIds = LoadVariantIds<SSM.Variant, SSM.VariantOccurrence>(specimenId);
        var cnvIds = LoadVariantIds<CNV.Variant, CNV.VariantOccurrence>(specimenId);
        var svIds = LoadVariantIds<SV.Variant, SV.VariantOccurrence>(specimenId);
        var ssmGeneIds = LoadGeneIds<SSM.Variant, SSM.AffectedTranscript>(ssmIds);
        var cnvGeneIds = LoadGeneIds<CNV.Variant, CNV.AffectedTranscript>(ssmIds);
        var svGeneIds = LoadGeneIds<SV.Variant, SV.AffectedTranscript>(ssmIds);
        var geneIds = ssmGeneIds.Union(cnvGeneIds).Union(svGeneIds).ToArray();

        return new GenomicStats(geneIds.Length, ssmIds.Length, cnvIds.Length, svIds.Length);
    }

    private int[] LoadGeneIds(int specimenId)
    {
        var ssmIds = LoadVariantIds<SSM.Variant, SSM.VariantOccurrence>(specimenId);
        var cnvIds = LoadVariantIds<CNV.Variant, CNV.VariantOccurrence>(specimenId);
        var svIds = LoadVariantIds<SV.Variant, SV.VariantOccurrence>(specimenId);

        var ssmAffectedGeneIds = LoadGeneIds<SSM.Variant, SSM.AffectedTranscript>(ssmIds);
        var cnvAffectedGeneIds = LoadGeneIds<SSM.Variant, SSM.AffectedTranscript>(cnvIds);
        var svAffectedGeneIds = LoadGeneIds<SSM.Variant, SSM.AffectedTranscript>(svIds);

        return ssmAffectedGeneIds.Union(cnvAffectedGeneIds).Union(svAffectedGeneIds).ToArray();
    }

    private int[] LoadGeneIds<TVariant, TAffectedTranscript>(long[] variantIds)
        where TVariant : Variant
        where TAffectedTranscript : VariantAffectedFeature<TVariant, Data.Entities.Genome.Transcript>
    {
        var ids = _dbContext.Set<TAffectedTranscript>()
            .Where(affectedTranscript => variantIds.Contains(affectedTranscript.VariantId))
            .Select(affectedTranscript => affectedTranscript.Feature.GeneId.Value)
            .Distinct()
            .ToArray();

        return ids;
    }

    private long[] LoadVariantIds<TVariant, TVariantOccurrence>(int specimenId)
        where TVariant : Variant
        where TVariantOccurrence : VariantOccurrence<TVariant>
    {
        var ids = _dbContext.Set<TVariantOccurrence>()
            .Where(occurrence => occurrence.AnalysedSample.Sample.SpecimenId == specimenId)
            .Select(occurrence => occurrence.VariantId)
            .Distinct()
            .ToArray();

        return ids;
    }
}
