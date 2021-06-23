using System.Linq;
using Microsoft.EntityFrameworkCore;
using Unite.Data.Entities.Specimens.Organoids;
using Unite.Data.Services;
using Unite.Specimens.Feed.Data.Specimens.Models;

namespace Unite.Specimens.Feed.Data.Specimens.Repositories
{
    internal class OrganoidInterventionRepository
    {
        private readonly UniteDbContext _dbContext;


        public OrganoidInterventionRepository(UniteDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public OrganoidIntervention Find(int specimenId, OrganoidInterventionModel interventionModel)
        {
            var intervention = _dbContext.OrganoidInterventions
                .Include(intervention => intervention.Type)
                .FirstOrDefault(intervention =>
                    intervention.SpecimenId == specimenId &&
                    intervention.Type.Name == interventionModel.Type &&
                    intervention.StartDay == interventionModel.StartDay
                );

            return intervention;
        }

        public OrganoidIntervention Create(int specimenId, OrganoidInterventionModel interventionModel)
        {
            var intervention = new OrganoidIntervention
            {
                SpecimenId = specimenId
            };

            Map(interventionModel, intervention);

            _dbContext.OrganoidInterventions.Add(intervention);
            _dbContext.SaveChanges();

            return intervention;
        }

        public void Update(OrganoidIntervention intervention, OrganoidInterventionModel interventionModel)
        {
            Map(interventionModel, intervention);

            _dbContext.OrganoidInterventions.Update(intervention);
            _dbContext.SaveChanges();
        }


        private void Map(OrganoidInterventionModel interventionModel, OrganoidIntervention intervention)
        {
            intervention.Type = GetInterventionType(interventionModel.Type);
            intervention.Details = interventionModel.Details;
            intervention.StartDay = interventionModel.StartDay;
            intervention.DurationDays = interventionModel.DurationDays;
            intervention.Results = interventionModel.Results;
        }

        private OrganoidInterventionType GetInterventionType(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return null;
            }

            var interventionType = _dbContext.OrganoidInterventionTypes.FirstOrDefault(interventionType =>
                interventionType.Name == name
            );

            if (interventionType == null)
            {
                interventionType = new OrganoidInterventionType { Name = name };

                _dbContext.OrganoidInterventionTypes.Add(interventionType);
                _dbContext.SaveChanges();
            }

            return interventionType;
        }
    }
}
