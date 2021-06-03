using System;
using System.Linq.Expressions;
using Unite.Indices.Entities.Specimens;
using Unite.Indices.Services;
using Unite.Indices.Services.Configuration.Options;

namespace Unite.Specimens.Indices.Services
{
    public class SpecimenIndexingService : IndexingService<SpecimenIndex>
    {
        protected override string DefaultIndex
        {
            get { return "specimens"; }
        }

        protected override Expression<Func<SpecimenIndex, object>> IdProperty
        {
            get { return (specimen) => specimen.Id; }
        }


        public SpecimenIndexingService(IElasticOptions options) : base(options)
        {

        }
    }
}
