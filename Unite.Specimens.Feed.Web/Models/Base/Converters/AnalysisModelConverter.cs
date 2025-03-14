using Unite.Data.Entities.Specimens.Enums;
using DataModels = Unite.Specimens.Feed.Data.Models;

namespace Unite.Specimens.Feed.Web.Models.Base.Converters;

public abstract class AnalysisModelConverter<TEntry> where TEntry : class, new()
{
    public virtual DataModels.SampleModel Convert(AnalysisModel<TEntry> analysisModel)
    {
        var sample = ConvertSample(analysisModel.Sample);

        MapEntries(analysisModel, sample);

        return sample;
    }


    private static DataModels.SampleModel ConvertSample(SampleModel sampleModel)
    {
        return new DataModels.SampleModel
        {
            Specimen = ConvertSpecimen(sampleModel),
            Analysis = ConvertAnalysis(sampleModel)
        };
    }

    private static DataModels.SpecimenModel ConvertSpecimen(SampleModel sampleModel)
    {
        DataModels.SpecimenModel specimen;

        if (sampleModel.SpecimenType == SpecimenType.Material)
            specimen = new DataModels.MaterialModel { ReferenceId = sampleModel.SpecimenId };
        else if (sampleModel.SpecimenType == SpecimenType.Line)
            specimen = new DataModels.LineModel { ReferenceId = sampleModel.SpecimenId };
        else if (sampleModel.SpecimenType == SpecimenType.Organoid)
            specimen = new DataModels.OrganoidModel { ReferenceId = sampleModel.SpecimenId };
        else if (sampleModel.SpecimenType == SpecimenType.Xenograft)
            specimen = new DataModels.XenograftModel { ReferenceId = sampleModel.SpecimenId };
        else
            throw new NotSupportedException($"Specimen type {sampleModel.SpecimenType} is not supported.");

        specimen.Donor = new DataModels.DonorModel { ReferenceId = sampleModel.DonorId };

        return specimen;
    }

    private static DataModels.AnalysisModel ConvertAnalysis(SampleModel sampleModel)
    {
        return new DataModels.AnalysisModel
        {
            Type = sampleModel.AnalysisType.Value,
            Date = sampleModel.AnalysisDate,
            Day = sampleModel.AnalysisDay
        };
    }

    protected abstract void MapEntries(AnalysisModel<TEntry> analysisModel, DataModels.SampleModel sample);
}
