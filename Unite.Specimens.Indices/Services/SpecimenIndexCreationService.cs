using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Unite.Data.Entities.Donors;
using Unite.Data.Entities.Genome.Transcriptomics;
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
        var isOranoid = specimen.Organoid != null;
        var isXenograft = specimen.Xenograft != null;

        var index = new SpecimenIndex();

        _specimenIndexMapper.Map(specimen, index, diagnosisDate);

        index.Parent = CreateParentSpecimenIndex(specimen.Id, diagnosisDate);
        index.Donor = CreateDonorIndex(specimen.DonorId);
        index.Images = CreateImageIndices(index.Donor.Id, diagnosisDate, isTumorTissue);
        index.Data = CreateDataIndex(specimen.Id, specimen.Donor.Id, isTumorTissue, isOranoid, isXenograft);

        var stats = LoadGenomicStats(specimen.Id);

        index.NumberOfGenes = stats.NumberOfGenes;
        index.NumberOfSsms = stats.NumberOfSsms;
        index.NumberOfCnvs = stats.NumberOfCnvs;
        index.NumberOfSvs = stats.NumberOfSvs;
        
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


    private DataIndex CreateDataIndex(int specimenId, int donorId, bool isTumorTissue, bool isOranoid, bool isXenograft)
    {
        var index = new DataIndex();

        index.Molecular = _dbContext.Set<MolecularData>()
            .Where(data => data.SpecimenId == specimenId)
            .Any();

        index.Drugs = _dbContext.Set<DrugScreening>()
            .Where(data => data.SpecimenId == specimenId)
            .Any();

        if (isOranoid)
        {
            index.Interventions = _dbContext.Set<Data.Entities.Specimens.Organoids.Intervention>()
                .Where(intervention => intervention.SpecimenId == specimenId)
                .Any();
        }

        if (isXenograft)
        {
            index.Interventions = _dbContext.Set<Data.Entities.Specimens.Xenografts.Intervention>()
                .Where(intervention => intervention.SpecimenId == specimenId)
                .Any();
        }

        if (isTumorTissue)
        {
            index.Mris = _dbContext.Set<Image>()
                .Include(image => image.MriImage)
                .Where(image => image.DonorId == donorId)
                .Where(image => image.MriImage != null)
                .Any();
        }

        index.Ssms = CheckVariants<SSM.Variant, SSM.VariantOccurrence>(specimenId);

        index.Cnvs = CheckVariants<CNV.Variant, CNV.VariantOccurrence>(specimenId);

        index.Svs = CheckVariants<SV.Variant, SV.VariantOccurrence>(specimenId);

        index.GeneExp = CheckGeneExp(specimenId);

        return index;
    }


    private record GenomicStats(int NumberOfGenes, int NumberOfSsms, int NumberOfCnvs, int NumberOfSvs);

    private GenomicStats LoadGenomicStats(int specimenId)
    {
        var ssmIds = LoadVariantIds<SSM.Variant, SSM.VariantOccurrence>(specimenId);
        var cnvIds = LoadVariantIds<CNV.Variant, CNV.VariantOccurrence>(specimenId);
        var svIds = LoadVariantIds<SV.Variant, SV.VariantOccurrence>(specimenId);
        var ssmGeneIds = LoadGeneIds<SSM.Variant, SSM.AffectedTranscript>(ssmIds);
        var cnvGeneIds = LoadGeneIds<CNV.Variant, CNV.AffectedTranscript>(cnvIds, affectedTranscript => affectedTranscript.Variant.TypeId != CNV.Enums.CnvType.Neutral);
        var svGeneIds = LoadGeneIds<SV.Variant, SV.AffectedTranscript>(svIds);
        var geneIds = Array.Empty<int>().Union(ssmGeneIds).Union(cnvGeneIds).Union(svGeneIds).ToArray();

        return new GenomicStats(geneIds.Length, ssmIds.Length, cnvIds.Length, svIds.Length);
    }

    /// <summary>
    /// Loads identifiers of genes affected by given variants.
    /// </summary>
    /// <param name="variantIds">Varians identifiers.</param>
    /// <param name="filter">Affected transcript filter.</param>
    /// <typeparam name="TVariant">Variant type.</typeparam>
    /// <typeparam name="TAffectedTranscript">Variant affected transcript type.</typeparam>
    /// <returns>Array of genes identifiers.</returns>
    private int[] LoadGeneIds<TVariant, TAffectedTranscript>(long[] variantIds, Expression<Func<TAffectedTranscript, bool>> filter = null)
        where TVariant : Variant
        where TAffectedTranscript : VariantAffectedFeature<TVariant, Data.Entities.Genome.Transcript>
    {
        Expression<Func<TAffectedTranscript, bool>> selectorPredicate = (affectedTranscript => variantIds.Contains(affectedTranscript.VariantId));
        Expression<Func<TAffectedTranscript, bool>> filterPredicate = filter ?? (affectedTranscript => true);

        var ids = _dbContext.Set<TAffectedTranscript>()
            .Where(selectorPredicate)
            .Where(filterPredicate)
            .Select(affectedTranscript => affectedTranscript.Feature.GeneId.Value)
            .Distinct()
            .ToArray();

        return ids;
    }

    /// <summary>
    /// Loads identifiers of variants of given type occurring in given specimens.
    /// </summary>
    /// <param name="specimenIds">Specimens identifiers.</param>
    /// <param name="filter">Variant occurrence filter.</param>
    /// <typeparam name="TVariant">Variant type.</typeparam>
    /// <typeparam name="TVariantOccurrence">Variant occurrence type.</typeparam>
    /// <returns>Array of variants identifiers.</returns>
    private long[] LoadVariantIds<TVariant, TVariantOccurrence>(int specimenId, Expression<Func<TVariantOccurrence, bool>> filter = null)
        where TVariant : Variant
        where TVariantOccurrence : VariantOccurrence<TVariant>
    {
        Expression<Func<TVariantOccurrence, bool>> selectorPredicate = (occurrence => occurrence.AnalysedSample.Sample.SpecimenId == specimenId);
        Expression<Func<TVariantOccurrence, bool>> filterPredicate = filter ?? (occurrence => true);

        var ids = _dbContext.Set<TVariantOccurrence>()
            .Where(selectorPredicate)
            .Where(filterPredicate)
            .Select(occurrence => occurrence.VariantId)
            .Distinct()
            .ToArray();

        return ids;
    }

    /// <summary>
    /// Checks if variants data of given type is available for given specimen.
    /// </summary>
    /// <param name="specimenId">Specimen identifier.</param>
    /// <typeparam name="TVariant">Variant type.</typeparam>
    /// <typeparam name="TVariantOccurrence">Variant occurrence type.</typeparam>
    /// <returns>'true' if variants data exists or 'false' otherwise.</returns>
    private bool CheckVariants<TVariant, TVariantOccurrence>(int specimenId)
        where TVariant : Variant
        where TVariantOccurrence : VariantOccurrence<TVariant>
    {
        return _dbContext.Set<TVariantOccurrence>()
            .Where(occurrence => occurrence.AnalysedSample.Sample.SpecimenId == specimenId)
            .Select(occurrence => occurrence.VariantId)
            .Distinct()
            .Any();
    }

    /// <summary>
    /// Checks if gene expression data is available for given specimen.
    /// </summary>
    /// <param name="specimenId">Specimen identifier.</param>
    /// <returns>'true' if gene expression data exists or 'false' otherwise.</returns>
    private bool CheckGeneExp(int specimenId)
    {
        var hasExpressions = _dbContext.Set<GeneExpression>()
            .Any(expression => expression.AnalysedSample.Sample.SpecimenId == specimenId);

        return hasExpressions;
    }
}
