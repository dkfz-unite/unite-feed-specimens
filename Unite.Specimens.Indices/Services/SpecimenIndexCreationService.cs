using Microsoft.EntityFrameworkCore;
using Unite.Data.Context;
using Unite.Data.Context.Extensions.Queryable;
using Unite.Data.Context.Repositories;
using Unite.Data.Context.Repositories.Constants;
using Unite.Data.Entities.Donors;
using Unite.Data.Entities.Genome.Analysis;
using Unite.Data.Entities.Genome.Analysis.Dna;
using Unite.Data.Entities.Genome.Analysis.Enums;
using Unite.Data.Entities.Genome.Analysis.Rna;
using Unite.Data.Entities.Donors.Clinical;
using Unite.Data.Entities.Images;
using Unite.Data.Entities.Images.Enums;
using Unite.Data.Entities.Specimens;
using Unite.Data.Entities.Specimens.Analysis.Drugs;
using Unite.Data.Entities.Specimens.Enums;
using Unite.Essentials.Extensions;
using Unite.Indices.Entities;
using Unite.Indices.Entities.Specimens;
using Unite.Mapping;

using SSM = Unite.Data.Entities.Genome.Analysis.Dna.Ssm;
using CNV = Unite.Data.Entities.Genome.Analysis.Dna.Cnv;
using SV = Unite.Data.Entities.Genome.Analysis.Dna.Sv;


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

        if (specimen == null)
            return null;

        return CreateSpecimenIndex(specimen, specimen.Donor.ClinicalData?.DiagnosisDate);
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
        index.Samples = CreateSampleIndices(specimen.Id, diagnosisDate);
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


    private SampleIndex[] CreateSampleIndices(int specimenId, DateOnly? diagnosisDate)
    {
        var samples = LoadSamples(specimenId);

        return samples.Select(sample => CreateSampleIndex(sample, diagnosisDate)).ToArrayOrNull();
    }

    private static SampleIndex CreateSampleIndex(Sample sample, DateOnly? diagnosisDate)
    {
        var index = SampleIndexMapper.CreateFrom<SampleIndex>(sample, diagnosisDate);

        index.Resources = sample.Resources?.Select(resource => ResourceIndexMapper.CreateFrom<ResourceIndex>(resource)).ToArray();

        return index;
    }

    private Sample[] LoadSamples(int specimenId)
    {
        using var dbContext = _dbContextFactory.CreateDbContext();

        return dbContext.Set<Sample>()
            .AsNoTracking()
            .Include(analysis => analysis.Analysis)
            .Include(analysis => analysis.Resources)
            .Where(analysis => analysis.SpecimenId == specimenId)
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
            return null;

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
            return null;

        var images = LoadImages(donorId);

        return images.Select(image => CreateImageIndex(image, diagnosisDate)).ToArrayOrNull();
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

        index.Donors = true;
        index.Clinical = CheckClinicalData(donorId);
        index.Treatments = CheckTreatments(donorId);

        if (canHaveMris)
            index.Mris = CheckImages(donorId, ImageType.MRI);

        if (canHaveCts)
            index.Cts = CheckImages(donorId, ImageType.CT);

        if (typeId == SpecimenType.Material)
        {
            index.Materials = true;
            index.MaterialsMolecular = CheckMolecularData(specimenId, typeId);
        }
        else if (typeId == SpecimenType.Line)
        {
            index.Lines = true;
            index.LinesMolecular = CheckMolecularData(specimenId, typeId);
            index.LinesInterventions = CheckInterventions(specimenId, typeId);
            index.LinesDrugs = CheckDrugScreenings(specimenId, typeId);
        }
        else if (typeId == SpecimenType.Organoid)
        {
            index.Organoids = true;
            index.OrganoidsMolecular = CheckMolecularData(specimenId, typeId);
            index.OrganoidsInterventions = CheckInterventions(specimenId, typeId);
            index.OrganoidsDrugs = CheckDrugScreenings(specimenId, typeId);
        }
        else if (typeId == SpecimenType.Xenograft)
        {
            index.Xenografts = true;
            index.XenograftsMolecular = CheckMolecularData(specimenId, typeId);
            index.XenograftsInterventions = CheckInterventions(specimenId, typeId);
            index.XenograftsDrugs = CheckDrugScreenings(specimenId, typeId);
        }

        index.Ssms = CheckVariants<SSM.Variant, SSM.VariantEntry>(specimenId);
        index.Cnvs = CheckVariants<CNV.Variant, CNV.VariantEntry>(specimenId);
        index.Svs = CheckVariants<SV.Variant, SV.VariantEntry>(specimenId);
        index.GeneExp = CheckGeneExp(specimenId);
        index.GeneExpSc = CheckGeneExpSc(specimenId);

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

    private bool CheckClinicalData(int donorId)
    {
        using var dbContext = _dbContextFactory.CreateDbContext();

        return dbContext.Set<ClinicalData>()
            .AsNoTracking()
            .Where(clinical => clinical.DonorId == donorId)
            .Any();
    }

    private bool CheckTreatments(int donorId)
    {
        using var dbContext = _dbContextFactory.CreateDbContext();

        return dbContext.Set<Treatment>()
            .AsNoTracking()
            .Where(treatment => treatment.DonorId == donorId)
            .Any();
    }

    private bool CheckImages(int donorId, ImageType type)
    {
        using var dbContext = _dbContextFactory.CreateDbContext();

        return dbContext.Set<Image>()
            .AsNoTracking()
            .Where(image => image.DonorId == donorId)
            .Where(image => image.TypeId == type)
            .Any();
    }

    private bool CheckMolecularData(int specimenId, SpecimenType type)
    {
        using var dbContext = _dbContextFactory.CreateDbContext();

        return dbContext.Set<Specimen>()
            .AsNoTracking()
            .IncludeMolecularData()
            .Where(specimen => specimen.Id == specimenId)
            .Where(specimen => specimen.TypeId == type)
            .Where(specimen => specimen.MolecularData != null)
            .Any();
    }

    private bool CheckInterventions(int specimenId, SpecimenType type)
    {
        using var dbContext = _dbContextFactory.CreateDbContext();

        return dbContext.Set<Intervention>()
            .AsNoTracking()
            .Where(intervention => intervention.SpecimenId == specimenId)
            .Where(intervention => intervention.Specimen.TypeId == type)
            .Any();
    }

    private bool CheckDrugScreenings(int specimenId, SpecimenType type)
    {
        using var dbContext = _dbContextFactory.CreateDbContext();

        return dbContext.Set<DrugScreening>()
            .AsNoTracking()
            .Where(entry => entry.Sample.SpecimenId == specimenId)
            .Where(entry => entry.Sample.Specimen.TypeId == type)
            .Any();
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
            .Where(entry => entry.Sample.SpecimenId == specimenId)
            .Select(entry => entry.EntityId)
            .Distinct()
            .Any();
    }

    /// <summary>
    /// Checks if bulk gene expression data is available for given specimen.
    /// </summary>
    /// <param name="specimenId">Specimen identifier.</param>
    /// <returns>'true' if bulk gene expression data exists or 'false' otherwise.</returns>
    private bool CheckGeneExp(int specimenId)
    {
        using var dbContext = _dbContextFactory.CreateDbContext();

        return dbContext.Set<GeneExpression>()
            .AsNoTracking()
            .Any(expression => expression.Sample.SpecimenId == specimenId);
    }

    /// <summary>
    /// Checks if single cell gene expression data is available for given specimen.
    /// </summary>
    /// <param name="specimenId">Specimen identifier.</param>
    /// <returns>'true' if single cell gene expression data exists or 'false' otherwise.</returns>
    private bool CheckGeneExpSc(int specimenId)
    {
        using var dbContext = _dbContextFactory.CreateDbContext();

        return dbContext.Set<Sample>()
            .AsNoTracking()
            .Any(analysedSample => analysedSample.SpecimenId == specimenId && analysedSample.Analysis.TypeId == AnalysisType.RNASeqSc);
    }
}
