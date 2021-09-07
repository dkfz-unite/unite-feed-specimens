using System.Linq;
using Unite.Data.Entities.Donors;
using Unite.Data.Services;
using Unite.Specimens.Feed.Data.Specimens.Models;

namespace Unite.Specimens.Feed.Data.Specimens.Repositories
{
    internal class DonorRepository
    {
        private readonly DomainDbContext _dbContext;


        public DonorRepository(DomainDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public Donor Find(string referenceId)
        {
            var donor = _dbContext.Donors.FirstOrDefault(donor =>
                donor.ReferenceId == referenceId
            );

            return donor;
        }

        public Donor Create(DonorModel donorModel)
        {
            var donor = new Donor
            {
                ReferenceId = donorModel.ReferenceId
            };

            Map(donorModel, donor);

            _dbContext.Donors.Add(donor);
            _dbContext.SaveChanges();

            return donor;
        }


        private void Map(DonorModel donorModel, Donor donor)
        {
            
        }
    }
}
