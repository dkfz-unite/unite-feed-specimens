using System.Linq;
using Microsoft.EntityFrameworkCore;
using Unite.Data.Entities.Specimens.Xenografts;
using Unite.Data.Services;
using Unite.Specimens.Feed.Data.Specimens.Models;

namespace Unite.Specimens.Feed.Data.Specimens.Repositories
{
    internal class XenograftInterventionRepository
    {
        private readonly UniteDbContext _dbContext;


        public XenograftInterventionRepository(UniteDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public XenograftIntervention Find(int specimenId, XenograftInterventionModel interventionModel)
        {
            var intervention = _dbContext.XenograftInterventions
                .Include(intervention => intervention.Type)
                .FirstOrDefault(intervention =>
                    intervention.SpecimenId == specimenId &&
                    intervention.Type.Name == interventionModel.Type &&
                    intervention.StartDay == interventionModel.StartDay
                );

            return intervention;
        }

        public XenograftIntervention Create(int specimenId, XenograftInterventionModel interventionModel)
        {
            var intervention = new XenograftIntervention
            {
                SpecimenId = specimenId
            };

            Map(interventionModel, intervention);

            _dbContext.XenograftInterventions.Add(intervention);
            _dbContext.SaveChanges();

            return intervention;
        }

        public void Update(XenograftIntervention intervention, XenograftInterventionModel interventionModel)
        {
            Map(interventionModel, intervention);

            _dbContext.XenograftInterventions.Update(intervention);
            _dbContext.SaveChanges();
        }


        private void Map(XenograftInterventionModel interventionModel, XenograftIntervention intervention)
        {
            intervention.Type = GetInterventionType(interventionModel.Type);
            intervention.Details = interventionModel.Details;
            intervention.StartDay = interventionModel.StartDay;
            intervention.DurationDays = interventionModel.DurationDays;
            intervention.Results = interventionModel.Results;
        }

        private XenograftInterventionType GetInterventionType(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return null;
            }

            var interventionType = _dbContext.XenograftInterventionTypes.FirstOrDefault(interventionType =>
                interventionType.Name == name
            );

            if (interventionType == null)
            {
                interventionType = new XenograftInterventionType { Name = name };

                _dbContext.XenograftInterventionTypes.Add(interventionType);
                _dbContext.SaveChanges();
            }

            return interventionType;
        }
    }
}
