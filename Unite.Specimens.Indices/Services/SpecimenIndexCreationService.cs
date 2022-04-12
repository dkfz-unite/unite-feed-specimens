using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Unite.Data.Entities.Donors;
using Unite.Data.Entities.Genome.Mutations;
using Unite.Data.Entities.Images;
using Unite.Data.Entities.Specimens;
using Unite.Data.Entities.Specimens.Tissues.Enums;
using Unite.Data.Services;
using Unite.Data.Services.Extensions;
using Unite.Indices.Entities.Specimens;
using Unite.Indices.Services;
using Unite.Specimens.Indices.Services.Extensions;
using Unite.Specimens.Indices.Services.Mappers;

namespace Unite.Specimens.Indices.Services
{
    public class SpecimenIndexCreationService : IIndexCreationService<SpecimenIndex>
    {
        private readonly DomainDbContext _dbContext;
        private readonly MutationIndexMapper _mutationIndexMapper;
        private readonly DonorIndexMapper _donorIndexMapper;
        private readonly ImageIndexMapper _imageIndexMapper;
        private readonly SpecimenIndexMapper _specimenIndexMapper;


        public SpecimenIndexCreationService(DomainDbContext dbContext)
        {
            _dbContext = dbContext;
            _mutationIndexMapper = new MutationIndexMapper();
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

        private SpecimenIndex CreateSpecimenIndex(Specimen specimen, DateTime? diagnosisDate)
        {
            var isTumorTissue = specimen.Tissue?.TypeId == TissueType.Tumor;

            var index = new SpecimenIndex();
            
            _specimenIndexMapper.Map(specimen, index, diagnosisDate);

            index.Donor = CreateDonorIndex(specimen.DonorId, isTumorTissue);

            index.Parent = CreateParentSpecimenIndex(specimen.Id, diagnosisDate);

            index.Children = CreateChildSpecimenIndices(specimen.Id, diagnosisDate);

            index.Mutations = CreateMutationIndices(specimen.Id);

            index.NumberOfMutations = index.Mutations
                .Select(mutation => mutation.Id)
                .Distinct()
                .Count();

            index.NumberOfGenes = index.Mutations
                .Where(mutation => mutation.AffectedTranscripts != null)
                .SelectMany(mutation => mutation.AffectedTranscripts)
                .Select(affectedTranscript => affectedTranscript.Transcript.Gene.Id)
                .Distinct()
                .Count();

            return index;
        }

        private Specimen LoadSpecimen(int specimenId)
        {
            var specimen = _dbContext.Set<Specimen>()
                .Include(specimen => specimen.Donor)
                    .ThenInclude(donor => donor.ClinicalData)
                .IncludeTissue()
                .IncludeCellLine()
                .IncludeOrganoid()
                .IncludeXenograft()
                .IncludeMolecularData()
                .FirstOrDefault(specimen => specimen.Id == specimenId);

            return specimen;
        }


        private SpecimenIndex CreateParentSpecimenIndex(int specimeId, DateTime? diagnosisDate)
        {
            var specimen = LoadParentSpecimen(specimeId);

            if (specimen == null)
            {
                return null;
            }

            var index = CreateParentSpecimenIndex(specimen, diagnosisDate);

            return index;
        }

        private SpecimenIndex CreateParentSpecimenIndex(Specimen specimen, DateTime? diagnosisDate)
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
                .IncludeParentMolecularData()
                .FirstOrDefault(specimen => specimen.Id == specimeId).Parent;

            return specimen;
        }


        private SpecimenIndex[] CreateChildSpecimenIndices(int specimenId, DateTime? diagnosisDate)
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

        private SpecimenIndex CreateChildSpecimenIndex(Specimen specimen, DateTime? diagnosisDate)
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
                .IncludeMolecularData()
                .Where(specimen => specimen.ParentId == specimenId)
                .ToArray();

            return specimens;
        }


        private DonorIndex CreateDonorIndex(int donorId, bool isTumorTissue)
        {
            var donor = LoadDonor(donorId);

            if (donor == null)
            {
                return null;
            }

            var index = CreateDonorIndex(donor, isTumorTissue);

            return index;
        }

        private DonorIndex CreateDonorIndex(Donor donor, bool isTumorTissue)
        {
            var index = new DonorIndex();

            var diagnosisDate = donor.ClinicalData?.DiagnosisDate;

            _donorIndexMapper.Map(donor, index);

            // Images can be associated only with tumor tissues
            if (isTumorTissue)
            {
                index.Images = CreateImageIndices(donor.Id, diagnosisDate);
            }

            return index;
        }

        private Donor LoadDonor(int donorId)
        {
            var donor = _dbContext.Set<Donor>()
                .IncludeClinicalData()
                .IncludeTreatments()
                .IncludeWorkPackages()
                .IncludeStudies()
                .FirstOrDefault(donor => donor.Id == donorId);

            return donor;
        }


        private ImageIndex[] CreateImageIndices(int donorId, DateTime? diagnosisDate)
        {
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

        private ImageIndex CreateImageIndex(Image image, DateTime? diagnosisDate)
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


        private MutationIndex[] CreateMutationIndices(int specimenId)
        {
            var mutations = LoadMutations(specimenId);

            if (mutations == null)
            {
                return null;
            }

            var indices = mutations
                .Select(CreateMutationIndex)
                .ToArray();

            return indices;
        }

        private MutationIndex CreateMutationIndex(Mutation mutation)
        {
            var index = new MutationIndex();

            _mutationIndexMapper.Map(mutation, index);

            return index;
        }

        private Mutation[] LoadMutations(int specimenId)
        {
            var mutationIds = _dbContext.Set<MutationOccurrence>()
                .Where(mutationOccurrence => mutationOccurrence.AnalysedSample.Sample.SpecimenId == specimenId)
                .Select(mutationOccurrence => mutationOccurrence.MutationId)
                .Distinct()
                .ToArray();

            var mutations = _dbContext.Mutations
                .IncludeAffectedTranscripts()
                .Where(mutation => mutationIds.Contains(mutation.Id))
                .ToArray();

            return mutations;
        }
    }
}
