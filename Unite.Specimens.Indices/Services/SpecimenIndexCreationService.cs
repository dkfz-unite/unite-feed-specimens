using Microsoft.EntityFrameworkCore;
using Unite.Data.Context;
using Unite.Data.Context.Extensions.Queryable;
using Unite.Data.Context.Repositories;
using Unite.Data.Context.Repositories.Constants;
using Unite.Data.Entities.Donors;
using Unite.Data.Entities.Donors.Clinical;
using Unite.Data.Entities.Genome.Analysis;
using Unite.Data.Entities.Genome.Transcriptomics;
using Unite.Data.Entities.Genome.Variants;
using Unite.Data.Entities.Images;
using Unite.Data.Entities.Images.Enums;
using Unite.Data.Entities.Specimens;
using Unite.Data.Entities.Specimens.Enums;
using Unite.Essentials.Extensions;
using Unite.Indices.Entities;
using Unite.Indices.Entities.Specimens;
using Unite.Mapping;

using CNV = Unite.Data.Entities.Genome.Variants.CNV;
using SSM = Unite.Data.Entities.Genome.Variants.SSM;
using SV = Unite.Data.Entities.Genome.Variants.SV;

namespace Unite.Specimens.Indices.Services;

public class SpecimenIndexCreationService
{
    private record GenomicStats(int NumberOfGenes, int NumberOfSsms, int NumberOfCnvs, int NumberOfSvs);

    private readonly IDbContextFactory<DomainDbContext> _dbContextFactory;
    private readonly DonorsRepository _donorsRepository;
    private readonly SpecimensRepository _specimensRepository;


    public SpecimenIndexCreationService(IDbContextFactory<DomainDbContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
        _donorsRepository = new DonorsRepository(dbContextFactory);
        _specimensRepository = new SpecimensRepository(dbContextFactory);
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

        return CreateSpecimenIndex(specimen, diagnosisDate);
    }

    private SpecimenIndex CreateSpecimenIndex(Specimen specimen, DateOnly? diagnosisDate)
    {
        var type = specimen.TypeId;
        var canHaveMris = Predicates.IsImageRelatedSpecimen.Compile()(specimen);
        var canHaveCts = Predicates.IsImageRelatedSpecimen.Compile()(specimen);
        var parent = LoadParentSpecimen(specimen.Id);
        var stats = LoadGenomicStats(specimen.Id);

        var index = new SpecimenIndex();

        SpecimenIndexMapper.Map(specimen, index, diagnosisDate);

        index.DonorId = specimen.DonorId;
        index.ParentId = parent?.Id;
        index.ParentReferenceId = parent?.ReferenceId;
        index.ParentType = parent?.TypeId.ToDefinitionString();
        
        index.Donor = CreateDonorIndex(specimen.DonorId);
        index.Images = CreateImageIndices(index.Donor.Id, diagnosisDate, canHaveMris);
        index.Analyses = CreateAnalysisIndices(specimen.Id, diagnosisDate);
        index.Data = CreateDataIndex(specimen.Id, specimen.Donor.Id, type, canHaveMris, canHaveCts);

        index.NumberOfGenes = stats.NumberOfGenes;
        index.NumberOfSsms = stats.NumberOfSsms;
        index.NumberOfCnvs = stats.NumberOfCnvs;
        index.NumberOfSvs = stats.NumberOfSvs;
        
        return index;
    }

    private Specimen LoadSpecimen(int specimenId)
    {
        using var dbContext = _dbContextFactory.CreateDbContext();

        return dbContext.Set<Specimen>()
            .AsNoTracking()
            .Include(specimen => specimen.Donor.ClinicalData) // Required for diagnosis date
            .IncludeMaterial()
            .IncludeLine()
            .IncludeOrganoid()
            .IncludeXenograft()
            .IncludeMolecularData()
            .IncludeInterventions()
            .IncludeDrugScreenings()
            .FirstOrDefault(specimen => specimen.Id == specimenId);
    }


    private AnalysisIndex[] CreateAnalysisIndices(int specimenId, DateOnly? diagnosisDate)
    {
        var analyses = LoadAnalyses(specimenId);

        var indices = analyses.Select(analysis => CreateAnalysisIndex(analysis, diagnosisDate));

        return indices.Any() ? indices.ToArray() : null;
    }

    private static AnalysisIndex CreateAnalysisIndex(AnalysedSample analysis, DateOnly? diagnosisDate)
    {
        return AnalysisIndexMapper.CreateFrom<AnalysisIndex>(analysis, diagnosisDate);
    }

    private AnalysedSample[] LoadAnalyses(int specimenId)
    {
        using var dbContext = _dbContextFactory.CreateDbContext();

        return dbContext.Set<AnalysedSample>()
            .AsNoTracking()
            .Include(analysis => analysis.Analysis)
            .Where(analysis => analysis.TargetSampleId == specimenId)
            .ToArray();
    }


    private Specimen LoadParentSpecimen(int specimeId)
    {
        using var dbContext = _dbContextFactory.CreateDbContext();

        return dbContext.Set<Specimen>()
            .AsNoTracking()
            .Include(specimen => specimen.Parent)
            .FirstOrDefault(specimen => specimen.Id == specimeId).Parent;
    }


    private DonorIndex CreateDonorIndex(int donorId)
    {
        var donor = LoadDonor(donorId);

        if (donor == null)
        {
            return null;
        }

        return CreateDonorIndex(donor);
    }

    private static DonorIndex CreateDonorIndex(Donor donor)
    {
        return DonorIndexMapper.CreateFrom<DonorIndex>(donor);
    }

    private Donor LoadDonor(int donorId)
    {
        using var dbContext = _dbContextFactory.CreateDbContext();

        return dbContext.Set<Donor>()
            .AsNoTracking()
            .IncludeClinicalData()
            .IncludeTreatments()
            .IncludeProjects()
            .IncludeStudies()
            .FirstOrDefault(donor => donor.Id == donorId);
    }


    private ImageIndex[] CreateImageIndices(int donorId, DateOnly? diagnosisDate, bool canHaveMris)
    {
        if (!canHaveMris)
        {
            return null;
        }

        var images = LoadImages(donorId);

        var indices = images.Select(image => CreateImageIndex(image, diagnosisDate));

        return indices.Any() ? indices.ToArray() : null;
    }

    private static ImageIndex CreateImageIndex(Image image, DateOnly? diagnosisDate)
    {
        return ImageIndexMapper.CreateFrom<ImageIndex>(image, diagnosisDate);
    }

    private Image[] LoadImages(int donorId)
    {
        using var dbContext = _dbContextFactory.CreateDbContext();

        var imageIds = _donorsRepository.GetRelatedImages([donorId]).Result;

        return dbContext.Set<Image>()
            .AsNoTracking()
            .Include(image => image.MriImage)
            .Where(image => imageIds.Contains(image.Id))
            .ToArray();
    }


    private DataIndex CreateDataIndex(int specimenId, int donorId, SpecimenType typeId, bool canHaveMris = false, bool canHaveCts = false)
    {
        using var dbContext = _dbContextFactory.CreateDbContext();

        var index = new DataIndex();

        index.Clinical = dbContext.Set<ClinicalData>()
            .AsNoTracking()
            .Where(data => data.DonorId == donorId)
            .Any();

        index.Treatments = dbContext.Set<Treatment>()
            .Where(data => data.DonorId == donorId)
            .Any();

        if (canHaveMris)
        {
            index.Mris = dbContext.Set<Image>()
                .AsNoTracking()
                .Where(image => image.DonorId == donorId)
                .Where(image => image.TypeId == ImageType.MRI)
                .Any();
        }

        if (canHaveCts)
        {
            index.Cts = false;
        }

        if (typeId == SpecimenType.Material)
        {
            index.Materials = true;

            index.MaterialsMolecular = dbContext.Set<MolecularData>()
                .AsNoTracking()
                .Where(data => data.SpecimenId == specimenId)
                .Any();
        }
        else if (typeId == SpecimenType.Line)
        {
            index.Lines = true;

            index.LinesMolecular = dbContext.Set<MolecularData>()
                .AsNoTracking()
                .Where(data => data.SpecimenId == specimenId)
                .Any();

            index.LinesDrugs = dbContext.Set<DrugScreening>()
                .AsNoTracking()
                .Where(data => data.SpecimenId == specimenId)
                .Any();
        }
        else if (typeId == SpecimenType.Organoid)
        {
            index.Organoids = true;

            index.OrganoidsMolecular = dbContext.Set<MolecularData>()
                .AsNoTracking()
                .Where(data => data.SpecimenId == specimenId)
                .Any();

            index.OrganoidsInterventions = dbContext.Set<Intervention>()
                .AsNoTracking()
                .Where(intervention => intervention.SpecimenId == specimenId)
                .Any();

            index.OrganoidsDrugs = dbContext.Set<DrugScreening>()
                .AsNoTracking()
                .Where(data => data.SpecimenId == specimenId)
                .Any();
        }
        else if (typeId == SpecimenType.Xenograft)
        {
            index.Xenografts = true;

            index.XenograftsMolecular = dbContext.Set<MolecularData>()
                .AsNoTracking()
                .Where(data => data.SpecimenId == specimenId)
                .Any();

            index.XenograftsInterventions = dbContext.Set<Intervention>()
                .AsNoTracking()
                .Where(intervention => intervention.SpecimenId == specimenId)
                .Any();

            index.XenograftsDrugs = dbContext.Set<DrugScreening>()
                .AsNoTracking()
                .Where(data => data.SpecimenId == specimenId)
                .Any();
        }

        index.Ssms = CheckVariants<SSM.Variant, SSM.VariantEntry>(specimenId);

        index.Cnvs = CheckVariants<CNV.Variant, CNV.VariantEntry>(specimenId);

        index.Svs = CheckVariants<SV.Variant, SV.VariantEntry>(specimenId);

        index.GeneExp = CheckGeneExp(specimenId);

        index.GeneExpSc = false;

        return index;
    }


    private GenomicStats LoadGenomicStats(int specimenId)
    {
        var ssmIds = _specimensRepository.GetRelatedVariants<SSM.Variant>([specimenId]).Result;
        var cnvIds = _specimensRepository.GetRelatedVariants<CNV.Variant>([specimenId]).Result;
        var svIds = _specimensRepository.GetRelatedVariants<SV.Variant>([specimenId]).Result;
        var geneIds = _specimensRepository.GetVariantRelatedGenes([specimenId]).Result;

        return new GenomicStats(geneIds.Length, ssmIds.Length, cnvIds.Length, svIds.Length);
    }

    /// <summary>
    /// Checks if variants data of given type is available for given specimen.
    /// </summary>
    /// <param name="specimenId">Specimen identifier.</param>
    /// <typeparam name="TVariant">Variant type.</typeparam>
    /// <typeparam name="TVariantEntry">Variant occurrence type.</typeparam>
    /// <returns>'true' if variants data exists or 'false' otherwise.</returns>
    private bool CheckVariants<TVariant, TVariantEntry>(int specimenId)
        where TVariant : Variant
        where TVariantEntry : VariantEntry<TVariant>
    {
        using var dbContext = _dbContextFactory.CreateDbContext();

        return dbContext.Set<TVariantEntry>()
            .AsNoTracking()
            .Where(entry => entry.AnalysedSample.TargetSampleId == specimenId)
            .Select(entry => entry.EntityId)
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
        using var dbContext = _dbContextFactory.CreateDbContext();

        return dbContext.Set<BulkExpression>()
            .AsNoTracking()
            .Any(expression => expression.AnalysedSample.TargetSampleId == specimenId);
    }
}
