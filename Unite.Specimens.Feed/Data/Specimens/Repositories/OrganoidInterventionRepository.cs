using System.Linq;
using Microsoft.EntityFrameworkCore;
using Unite.Data.Entities.Specimens.Organoids;
using Unite.Data.Services;
using Unite.Specimens.Feed.Data.Specimens.Models;

namespace Unite.Specimens.Feed.Data.Specimens.Repositories
{
    internal class OrganoidInterventionRepository
    {
        private readonly DomainDbContext _dbContext;


        public OrganoidInterventionRepository(DomainDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public Intervention Find(int specimenId, OrganoidInterventionModel model)
        {
            var entity = _dbContext.Set<Intervention>()
                .Include(entity => entity.Type)
                .FirstOrDefault(entity =>
                    entity.SpecimenId == specimenId &&
                    entity.Type.Name == model.Type &&
                    entity.StartDay == model.StartDay
                );

            return entity;
        }

        public Intervention Create(int specimenId, OrganoidInterventionModel model)
        {
            var entity = new Intervention
            {
                SpecimenId = specimenId
            };

            Map(model, ref entity);

            _dbContext.Add(entity);
            _dbContext.SaveChanges();

            return entity;
        }

        public void Update(Intervention entity, OrganoidInterventionModel model)
        {
            Map(model, ref entity);

            _dbContext.Update(entity);
            _dbContext.SaveChanges();
        }


        private void Map(in OrganoidInterventionModel model, ref Intervention entity)
        {
            entity.Type = GetInterventionType(model.Type);
            entity.Details = model.Details;
            entity.StartDate = model.StartDate;
            entity.StartDay = model.StartDay;
            entity.EndDate = model.EndDate;
            entity.DurationDays = model.DurationDays;
            entity.Results = model.Results;
        }

        private InterventionType GetInterventionType(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return null;
            }

            var entity = _dbContext.Set<InterventionType>()
                .FirstOrDefault(entity =>
                    entity.Name == name
                );

            if (entity == null)
            {
                entity = new InterventionType { Name = name };

                _dbContext.Add(entity);
                _dbContext.SaveChanges();
            }

            return entity;
        }
    }
}
