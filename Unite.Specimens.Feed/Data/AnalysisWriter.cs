using Microsoft.EntityFrameworkCore;
using Unite.Data.Context;
using Unite.Data.Context.Services;
using Unite.Essentials.Extensions;
using Unite.Specimens.Feed.Data.Models;
using Unite.Specimens.Feed.Data.Models.Drugs;
using Unite.Specimens.Feed.Data.Repositories;
using Unite.Specimens.Feed.Data.Repositories.Drugs;

namespace Unite.Specimens.Feed.Data;

public class AnalysisWriter : DataWriter<SampleModel, AnalysisWriteAudit>
{
    private SampleRepository _sampleRepository;
    private DrugRepository _drugRepository;
    private DrugScreeningRepository _drugScreeningRepository;


    public AnalysisWriter(IDbContextFactory<DomainDbContext> dbContextFactory) : base(dbContextFactory)
    {
    }


    protected override void Initialize(DomainDbContext dbContext)
    {
        _sampleRepository = new SampleRepository(dbContext);
        _drugRepository = new DrugRepository(dbContext);
        _drugScreeningRepository = new DrugScreeningRepository(dbContext);
    }

    protected override void ProcessModel(SampleModel model, ref AnalysisWriteAudit audit)
    {
        var sample = _sampleRepository.FindOrCreate(model);

        if (model.DrugScreenings.IsNotEmpty())
            WriteDrugScreenings(sample.Id, model.DrugScreenings, ref audit);
    }


    private void WriteDrugScreenings(int sampleId, IEnumerable<DrugScreeningModel> models, ref AnalysisWriteAudit audit)
    {
        var names = models.Select(model => model.Drug);
        var entities = _drugRepository.CreateMissing(names);
        audit.DrugsCreated += entities.Count();

        var entries = _drugScreeningRepository.RecreateAll(sampleId, models);
        audit.DrugScreeningsCreated += entries.Count();

        if (audit.DrugScreeningsCreated > 0)
            audit.Samples.Add(sampleId);
    }
}
