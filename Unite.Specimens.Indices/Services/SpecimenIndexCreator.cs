using Microsoft.EntityFrameworkCore;
using Unite.Data.Context;
using Unite.Data.Context.Repositories;
using Unite.Data.Context.Repositories.Constants;
using Unite.Data.Context.Repositories.Extensions.Queryable;
using Unite.Data.Entities.Donors;
using Unite.Data.Entities.Donors.Clinical;
using Unite.Data.Entities.Images;
using Unite.Data.Entities.Images.Enums;
using Unite.Data.Entities.Omics.Analysis;
using Unite.Data.Entities.Specimens;
using Unite.Data.Entities.Specimens.Analysis.Drugs;
using Unite.Data.Entities.Specimens.Enums;
using Unite.Essentials.Extensions;
using Unite.Indices.Entities;
using Unite.Indices.Entities.Specimens;
using Unite.Specimens.Indices.Services.Mapping;

using SM = Unite.Data.Entities.Omics.Analysis.Dna.Sm;
using CNV = Unite.Data.Entities.Omics.Analysis.Dna.Cnv;
using SV = Unite.Data.Entities.Omics.Analysis.Dna.Sv;


namespace Unite.Specimens.Indices.Services;

public class SpecimenIndexCreator
{
    private readonly IDbContextFactory<DomainDbContext> _dbContextFactory;
    private readonly DonorsRepository _donorsRepository;
    private readonly SpecimensRepository _specimensRepository;
    private readonly SamplesRepository _samplesRepository;


    public SpecimenIndexCreator(IDbContextFactory<DomainDbContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
        _donorsRepository = new DonorsRepository(dbContextFactory);
        _specimensRepository = new SpecimensRepository(dbContextFactory);
        _samplesRepository = new SamplesRepository(dbContextFactory);
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

        return CreateSpecimenIndex(specimen, specimen.Donor.ClinicalData?.EnrollmentDate);
    }

    private SpecimenIndex CreateSpecimenIndex(Specimen specimen, DateOnly? enrollmentDate)
    {
        var type = specimen.TypeId;
        var canHaveMrs = Predicates.IsImageRelatedSpecimen.Compile()(specimen);
        var canHaveCts = Predicates.IsImageRelatedSpecimen.Compile()(specimen);

        var index = new SpecimenIndex();

        SpecimenIndexMapper.Map(specimen, index, enrollmentDate);
        
        index.Parent = CreateParentIndex(specimen.Id);
        index.Donor = CreateDonorIndex(specimen.DonorId);
        index.Images = CreateImageIndices(index.Donor.Id, enrollmentDate, canHaveMrs);
        index.Samples = CreateSampleIndices(specimen.Id, enrollmentDate);
        index.Stats = CreateStatsIndex(specimen.Id);
        index.Data = CreateDataIndex(specimen.Id, specimen.Donor.Id, type, canHaveMrs, canHaveCts);
        
        return index;
    }

    private Specimen LoadSpecimen(int specimenId)
    {
        using var dbContext = _dbContextFactory.CreateDbContext();

        return dbContext.Set<Specimen>()
            .AsNoTracking()
            .Include(specimen => specimen.Donor.ClinicalData) // Required for enrollment date
            .IncludeMaterial()
            .IncludeLine()
            .IncludeOrganoid()
            .IncludeXenograft()
            .IncludeTumorClassification()
            .IncludeMolecularData()
            .IncludeInterventions()
            .IncludeDrugScreenings()
            .FirstOrDefault(specimen => specimen.Id == specimenId);
    }


    private SampleIndex[] CreateSampleIndices(int specimenId, DateOnly? enrollmentDate)
    {
        var samples = LoadSamples(specimenId);

        return samples.Select(sample => CreateSampleIndex(sample, enrollmentDate)).ToArrayOrNull();
    }

    private SampleIndex CreateSampleIndex(Sample sample, DateOnly? enrollmentDate)
    {
        var index = SampleIndexMapper.CreateFrom<SampleIndex>(sample, enrollmentDate);

        var availability = _samplesRepository.HasRelatedOmicsResources(sample.Id).Result;

        if (availability != null)
        {
            index.Data = new Unite.Indices.Entities.Basic.Analysis.SampleDataIndex
            {
                Sm = availability.Sm,
                Cnv = availability.Cnv,
                Sv = availability.Sv,
                Cnvp = availability.Cnvp,
                Meth = availability.Meth,
                Exp = availability.GeneExp,
                ExpSc = availability.GeneExpSc,
                Prot = availability.ProtExp
            };
        }

        index.Resources = sample.Resources?.Select(resource => ResourceIndexMapper.CreateFrom<Unite.Indices.Entities.Basic.Analysis.ResourceIndex>(resource)).ToArray();

        return index;
    }

    private Sample[] LoadSamples(int specimenId)
    {
        using var dbContext = _dbContextFactory.CreateDbContext();

        return dbContext.Set<Sample>()
            .AsNoTracking()
            .Include(sample => sample.Analysis)
            .Include(sample => sample.Resources)
            .Where(sample => sample.SpecimenId == specimenId)
            .Where(sample =>
                sample.SmEntries.Any() ||
                sample.CnvEntries.Any() ||
                sample.SvEntries.Any() ||
                sample.GeneExpressions.Any() ||
                sample.ProteinExpressions.Any() ||
                sample.Resources.Any())
            .ToArray();
    }

    private ParentIndex CreateParentIndex(int specimenId)
    {
        var specimen = LoadParentSpecimen(specimenId);

        if (specimen == null)
            return null;

        return CreateParentIndex(specimen);
    }

    private ParentIndex CreateParentIndex(Specimen specimen)
    {
        if (specimen == null)
            return null;

        return new ParentIndex
        {
            Id = specimen.Id,
            ReferenceId = specimen.ReferenceId,
            Type = specimen.TypeId.ToDefinitionString()
        };
    }

    private Specimen LoadParentSpecimen(int specimenId)
    {
        using var dbContext = _dbContextFactory.CreateDbContext();

        return dbContext.Set<Specimen>()
            .AsNoTracking()
            .Include(specimen => specimen.Parent)
            .FirstOrDefault(specimen => specimen.Id == specimenId).Parent;
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
            .FirstOrDefault(donor => donor.Id == donorId);
    }


    private ImageIndex[] CreateImageIndices(int donorId, DateOnly? enrollmentDate, bool canHaveMrs)
    {
        if (!canHaveMrs)
            return null;

        var images = LoadImages(donorId);

        return images.Select(image => CreateImageIndex(image, enrollmentDate)).ToArrayOrNull();
    }

    private static ImageIndex CreateImageIndex(Image image, DateOnly? enrollmentDate)
    {
        return ImageIndexMapper.CreateFrom<ImageIndex>(image, enrollmentDate);
    }

    private Image[] LoadImages(int donorId)
    {
        using var dbContext = _dbContextFactory.CreateDbContext();

        var imageIds = _donorsRepository.GetRelatedImages([donorId]).Result;

        return dbContext.Set<Image>()
            .AsNoTracking()
            .Where(image => imageIds.Contains(image.Id))
            .ToArray();
    }


    private StatsIndex CreateStatsIndex(int specimenId)
    {
        var smIds = _specimensRepository.GetRelatedVariants<SM.Variant>([specimenId]).Result;
        var cnvIds = _specimensRepository.GetRelatedVariants<CNV.Variant>([specimenId]).Result;
        var svIds = _specimensRepository.GetRelatedVariants<SV.Variant>([specimenId]).Result;
        var geneIds = _specimensRepository.GetVariantRelatedGenes([specimenId]).Result;

        return new StatsIndex
        {
            Genes = geneIds.Length,
            Sms = smIds.Length,
            Cnvs = cnvIds.Length,
            Svs = svIds.Length
        };
    }

    private DataIndex CreateDataIndex(int specimenId, int donorId, SpecimenType typeId, bool canHaveMrs = false, bool canHaveCts = false)
    {
        var index = new DataIndex();

        index.Donors = true;
        index.Clinical = CheckClinicalData(donorId);
        index.Treatments = CheckTreatments(donorId);

        if (canHaveMrs)
            index.Mrs = CheckImages(donorId, ImageType.MR);

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

        index.Sms = _specimensRepository.HaveVariants<SM.VariantEntry, SM.Variant>([specimenId]).Result;
        index.Cnvs = _specimensRepository.HaveVariants<CNV.VariantEntry, CNV.Variant>([specimenId]).Result;
        index.Svs = _specimensRepository.HaveVariants<SV.VariantEntry, SV.Variant>([specimenId]).Result;
        index.Cnvps = _specimensRepository.HaveProfiles([specimenId]).Result;
        index.Meth = _specimensRepository.HaveMethylation([specimenId]).Result;
        index.Exp = _specimensRepository.HaveGeneExpressions([specimenId]).Result;
        index.ExpSc = _specimensRepository.HaveGeneExpressionsPerCells([specimenId]).Result;
        index.Prot = _specimensRepository.HaveProteinExpressions([specimenId]).Result;

        return index;
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
}
