using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Unite.Data.Entities.Donors;
using Unite.Data.Entities.Mutations;
using Unite.Data.Entities.Specimens;
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
        private readonly SpecimenIndexMapper _specimenIndexMapper;


        public SpecimenIndexCreationService(DomainDbContext dbContext)
        {
            _dbContext = dbContext;
            _mutationIndexMapper = new MutationIndexMapper();
            _donorIndexMapper = new DonorIndexMapper();
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

            if (specimen == null)
            {
                return null;
            }

            var index = CreateSpecimenIndex(specimen);

            return index;
        }

        private SpecimenIndex CreateSpecimenIndex(Specimen specimen)
        {
            var index = new SpecimenIndex();

            _specimenIndexMapper.Map(specimen, index);

            index.Donor = CreateDonorIndex(specimen.DonorId);

            index.Parent = CreateParentSpecimenIndex(specimen.Id);

            index.Children = CreateChildSpecimenIndices(specimen.Id);

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
            var specimen = _dbContext.Specimens
                .IncludeTissue()
                .IncludeCellLine()
                .IncludeOrganoid()
                .IncludeXenograft()
                .IncludeMolecularData()
                .FirstOrDefault(specimen => specimen.Id == specimenId);

            return specimen;
        }


        private SpecimenIndex CreateParentSpecimenIndex(int specimeId)
        {
            var specimen = LoadParentSpecimen(specimeId);

            if (specimen == null)
            {
                return null;
            }

            var index = CreateParentSpecimenIndex(specimen);

            return index;
        }

        private SpecimenIndex CreateParentSpecimenIndex(Specimen specimen)
        {
            var index = new SpecimenIndex();

            _specimenIndexMapper.Map(specimen, index);

            return index;
        }

        private Specimen LoadParentSpecimen(int specimeId)
        {
            var specimen = _dbContext.Specimens
                .IncludeParentTissue()
                .IncludeParentCellLine()
                .IncludeParentOrganoid()
                .IncludeParentXenograft()
                .IncludeParentMolecularData()
                .FirstOrDefault(specimen => specimen.Id == specimeId).Parent;

            return specimen;
        }


        private SpecimenIndex[] CreateChildSpecimenIndices(int specimenId)
        {
            var specimens = LoadChildSpecimens(specimenId);

            if (specimens == null)
            {
                return null;
            }

            var indices = specimens
                .Select(CreateChildSpecimenIndex)
                .ToArray();

            return indices;
        }

        private SpecimenIndex CreateChildSpecimenIndex(Specimen specimen)
        {
            var index = new SpecimenIndex();

            _specimenIndexMapper.Map(specimen, index);

            index.Children = CreateChildSpecimenIndices(specimen.Id);

            return index;
        }

        private Specimen[] LoadChildSpecimens(int specimenId)
        {
            var specimens = _dbContext.Specimens
                .IncludeTissue()
                .IncludeCellLine()
                .IncludeOrganoid()
                .IncludeXenograft()
                .IncludeMolecularData()
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

            _donorIndexMapper.Map(donor, index);

            return index;
        }

        private Donor LoadDonor(int donorId)
        {
            var donor = _dbContext.Donors
                .IncludeClinicalData()
                .IncludeTreatments()
                .IncludeWorkPackages()
                .IncludeStudies()
                .FirstOrDefault(donor => donor.Id == donorId);

            return donor;
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
            var mutationIds = _dbContext.MutationOccurrences
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
