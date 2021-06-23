using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Unite.Data.Entities.Donors;
using Unite.Data.Entities.Mutations;
using Unite.Data.Entities.Specimens;
using Unite.Data.Services;
using Unite.Indices.Entities.Specimens;
using Unite.Indices.Services;
using Unite.Specimens.Indices.Services.Mappers;

namespace Unite.Specimens.Indices.Services
{
    public class SpecimenIndexCreationService : IIndexCreationService<SpecimenIndex>
    {
        private readonly UniteDbContext _dbContext;
        private readonly MutationIndexMapper _mutationIndexMapper;
        private readonly DonorIndexMapper _donorIndexMapper;
        private readonly SpecimenIndexMapper _specimenIndexMapper;


        public SpecimenIndexCreationService(UniteDbContext dbContext)
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
                .Select(affectedTranscript => affectedTranscript.Gene.Id)
                .Distinct()
                .Count();

            return index;
        }

        private Specimen LoadSpecimen(int specimenId)
        {
            var specimen = _dbContext.Specimens
                .Include(specimen => specimen.Tissue)
                    .ThenInclude(tissue => tissue.Source)
                .Include(specimen => specimen.CellLine)
                    .ThenInclude(cellLine => cellLine.Info)
                .Include(specimen => specimen.Organoid)
                    .ThenInclude(organoid => organoid.Interventions)
                        .ThenInclude(intervention => intervention.Type)
                .Include(specimen => specimen.Xenograft)
                    .ThenInclude(xenograft => xenograft.Interventions)
                        .ThenInclude(intervention => intervention.Type)
                .Include(specimen => specimen.MolecularData)
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
                .Include(specimen => specimen.Parent)
                    .ThenInclude(specimen => specimen.Tissue)
                        .ThenInclude(tissue => tissue.Source)
                .Include(specimen => specimen.Parent)
                    .ThenInclude(specimen => specimen.CellLine)
                        .ThenInclude(cellLine => cellLine.Info)
                .Include(specimen => specimen.Parent)
                    .Include(specimen => specimen.Organoid)
                        .ThenInclude(organoid => organoid.Interventions)
                            .ThenInclude(intervention => intervention.Type)
                .Include(specimen => specimen.Parent)
                    .Include(specimen => specimen.Xenograft)
                        .ThenInclude(xenograft => xenograft.Interventions)
                            .ThenInclude(intervention => intervention.Type)
                .Include(specimen => specimen.Parent)
                    .ThenInclude(specimen => specimen.MolecularData)
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
                .Include(specimen => specimen.Tissue)
                    .ThenInclude(tissue => tissue.Source)
                .Include(specimen => specimen.CellLine)
                    .ThenInclude(cellLine => cellLine.Info)
                .Include(specimen => specimen.Organoid)
                    .ThenInclude(organoid => organoid.Interventions)
                        .ThenInclude(intervention => intervention.Type)
                .Include(specimen => specimen.Xenograft)
                    .ThenInclude(xenograft => xenograft.Interventions)
                        .ThenInclude(intervention => intervention.Type)
                .Include(specimen => specimen.MolecularData)
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
                .Include(donor => donor.ClinicalData)
                    .ThenInclude(clinicalData => clinicalData.PrimarySite)
                .Include(donor => donor.ClinicalData)
                    .ThenInclude(clinicalData => clinicalData.Localization)
                .Include(donor => donor.Treatments)
                    .ThenInclude(treatment => treatment.Therapy)
                .Include(donor => donor.DonorWorkPackages)
                    .ThenInclude(workPackageDonor => workPackageDonor.WorkPackage)
                .Include(donor => donor.DonorStudies)
                    .ThenInclude(studyDonor => studyDonor.Study)
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
                .Include(mutation => mutation.AffectedTranscripts)
                    .ThenInclude(affectedTranscript => affectedTranscript.Gene)
                        .ThenInclude(gene => gene.Info)
                .Include(mutation => mutation.AffectedTranscripts)
                    .ThenInclude(affectedTranscript => affectedTranscript.Gene)
                        .ThenInclude(gene => gene.Biotype)
                .Include(mutation => mutation.AffectedTranscripts)
                    .ThenInclude(affectedTranscript => affectedTranscript.Transcript)
                        .ThenInclude(transcript => transcript.Info)
                .Include(mutation => mutation.AffectedTranscripts)
                    .ThenInclude(affectedTranscript => affectedTranscript.Transcript)
                        .ThenInclude(transcript => transcript.Biotype)
                .Include(mutation => mutation.AffectedTranscripts)
                    .ThenInclude(affectedTranscript => affectedTranscript.Consequences)
                        .ThenInclude(affectedTranscriptConsequence => affectedTranscriptConsequence.Consequence)
                .Where(mutation => mutationIds.Contains(mutation.Id))
                .ToArray();

            return mutations;
        }
    }
}
