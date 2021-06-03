using Unite.Data.Entities.Molecular;
using Unite.Data.Entities.Specimens;
using Unite.Data.Services;
using Unite.Specimens.Feed.Data.Specimens.Models;

namespace Unite.Specimens.Feed.Data.Specimens.Repositories
{
    internal abstract class SpecimenRepositoryBase<TModel> where TModel : SpecimenModel
    {
        protected readonly UniteDbContext _dbContext;


        public SpecimenRepositoryBase(UniteDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public abstract Specimen Find(int donorId, int? parentId, in TModel model);

        public virtual Specimen Create(int donorId, int? parentId, in TModel model)
        {
            var specimen = new Specimen
            {
                DonorId = donorId,
                ParentId = parentId
            };

            Map(model, ref specimen);

            _dbContext.Specimens.Add(specimen);
            _dbContext.SaveChanges();

            return specimen;
        }

        public virtual void Update(ref Specimen specimen, in TModel model)
        {
            Map(model, ref specimen);

            _dbContext.Specimens.Update(specimen);
            _dbContext.SaveChanges();
        }


        protected virtual void Map(in TModel model, ref Specimen specimen)
        {
            if (model.MolecularData != null)
            {
                if (specimen.MolecularData == null)
                {
                    specimen.MolecularData = new MolecularData();
                }

                specimen.MolecularData.GeneExpressionSubtypeId = model.MolecularData.GeneExpressionSubtype;
                specimen.MolecularData.IdhStatusId = model.MolecularData.IdhStatus;
                specimen.MolecularData.IdhMutationId = model.MolecularData.IdhMutation;
                specimen.MolecularData.MethylationStatusId = model.MolecularData.MethylationStatus;
                specimen.MolecularData.MethylationTypeId = model.MolecularData.MethylationType;
                specimen.MolecularData.GcimpMethylation = model.MolecularData.GcimpMethylation;
            }
        }
    }
}
