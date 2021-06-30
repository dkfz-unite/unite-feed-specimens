using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Unite.Data.Entities.Specimens;
using Unite.Data.Entities.Tasks;
using Unite.Data.Entities.Tasks.Enums;
using Unite.Data.Services;

namespace Unite.Specimens.Feed.Web.Services
{
    public class IndexingTaskService
    {
        private const int BUCKET_SIZE = 1000;

        private readonly UniteDbContext _dbContext;


        public IndexingTaskService(UniteDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        /// <summary>
        /// Creates only specimen indexing tasks for all existing specimens
        /// </summary>
        public void CreateTasks()
        {
            IterateSpecimens(specimen => true, specimens =>
            {
                CreateSpecimenIndexingTasks(specimens);
            });
        }

        /// <summary>
        /// Creates only specimen indexing tasks for all specimen with given identifiers
        /// </summary>
        /// <param name="specimenIds">Identifiers of specimens</param>
        public void CreateTasks(IEnumerable<int> specimenIds)
        {
            IterateSpecimens(specimen => true, specimens =>
            {
                CreateSpecimenIndexingTasks(specimens);
            });
        }

        /// <summary>
        /// Populates all types of indexing tasks for specimens with given identifiers
        /// </summary>
        /// <param name="specimenIds">Identifiers of specimens</param>
        public void PopulateTasks(IEnumerable<int> specimenIds)
        {
            IterateSpecimens(specimen => true, specimens =>
            {
                CreateDonorIndexingTasks(specimens);
                CreateMutationIndexingTasks(specimens);
                CreateSpecimenIndexingTasks(specimens);
            });
        }


        private void CreateDonorIndexingTasks(IEnumerable<int> specimenIds)
        {
            var donorIds = _dbContext.Specimens
                .Select(specimen => specimen.DonorId)
                .Distinct()
                .ToArray();

            var tasks = donorIds
                .Select(donorId => new Task
                {
                    TypeId = TaskType.Indexing,
                    TargetTypeId = TaskTargetType.Donor,
                    Target = donorId.ToString(),
                    Date = DateTime.UtcNow
                })
                .ToArray();

            _dbContext.Tasks.AddRange(tasks);
            _dbContext.SaveChanges();
        }

        private void CreateMutationIndexingTasks(IEnumerable<int> specimenIds)
        {
            var mutationIds = _dbContext.MutationOccurrences
                .Where(mutationOccurrence => specimenIds.Contains(mutationOccurrence.AnalysedSample.Sample.SpecimenId))
                .Select(mutationOccurrence => mutationOccurrence.MutationId)
                .Distinct()
                .ToArray();

            var tasks = mutationIds
                .Select(mutationId => new Task
                {
                    TypeId = TaskType.Indexing,
                    TargetTypeId = TaskTargetType.Mutation,
                    Target = mutationId.ToString(),
                    Date = DateTime.UtcNow
                })
                .ToArray();

            _dbContext.Tasks.AddRange(tasks);
            _dbContext.SaveChanges();
        }

        private void CreateSpecimenIndexingTasks(IEnumerable<int> specimenIds)
        {
            var donorIds = _dbContext.Specimens
                .Select(specimen => specimen.DonorId)
                .Distinct()
                .ToArray();

            var relatedSpecimenIds = _dbContext.Specimens
                .Where(specimen => donorIds.Contains(specimen.DonorId))
                .Select(specimen => specimen.Id)
                .Distinct()
                .ToArray();

            var tasks = relatedSpecimenIds
                .Select(specimenId => new Task
                {
                    TypeId = TaskType.Indexing,
                    TargetTypeId = TaskTargetType.Specimen,
                    Target = specimenId.ToString(),
                    Date = DateTime.UtcNow
                })
                .ToArray();

            _dbContext.Tasks.AddRange(tasks);
            _dbContext.SaveChanges();
        }

        private void IterateSpecimens(Expression<Func<Specimen, bool>> condition, Action<IEnumerable<int>> handler)
        {
            var position = 0;

            var specimens = Enumerable.Empty<int>();

            do
            {
                specimens = _dbContext.Specimens
                    .Where(condition)
                    .Skip(position)
                    .Take(BUCKET_SIZE)
                    .Select(specimen => specimen.Id)
                    .ToArray();

                handler.Invoke(specimens);

                position += specimens.Count();

            }
            while (specimens.Count() == BUCKET_SIZE);
        }
    }
}
