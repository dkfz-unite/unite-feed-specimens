using Unite.Specimens.Feed.Web.Models.Base;
using Unite.Specimens.Feed.Web.Models.Base.Converters;
using Unite.Specimens.Feed.Web.Models.Base.Enums;
using DataModels = Unite.Specimens.Feed.Data.Specimens.Models;

namespace Unite.Specimens.Feed.Web.Models.Converters;


public class DrugScreeningDataFlatModelConverter
{
    private readonly DrugScreeningModelConverter _drugScreeningModelConverter;


    public DrugScreeningDataFlatModelConverter()
    {
        _drugScreeningModelConverter = new DrugScreeningModelConverter();
    }


    public DataModels.SpecimenModel Convert(in DrugScreeningDataFlatModel source)
    {
        var target = GetSpecimenModel(source.SpecimenId, source.SpecimenType);

        target.Donor = GetDonorModel(source.DonorId);
        target.DrugsScreeningData = GetDrugScreeningModels(new DrugScreeningModel[]
        {
            new DrugScreeningModel {
                Drug = source.Drug,
                Dss = source.Dss,
                DssSelective = source.DssSelective,
                Gof = source.Gof,
                MinConcentration = source.MinConcentration,
                MaxConcentration = source.MaxConcentration,
                AbsIC25 = source.AbsIC25,
                AbsIC50 = source.AbsIC50,
                AbsIC75 = source.AbsIC75,
                Concentration = source.Concentration,
                Inhibition = source.Inhibition,
                ConcentrationLine = source.ConcentrationLine,
                InhibitionLine = source.InhibitionLine
            }
        });

        return target;
    }

    private DataModels.SpecimenModel GetSpecimenModel(string id, SpecimenType? type)
    {
        if (string.IsNullOrWhiteSpace(id) || type == null)
        {
            return null;
        }

        if (type == SpecimenType.Tissue)
        {
            return new DataModels.TissueModel { ReferenceId = id };
        }
        else if (type == SpecimenType.CellLine)
        {
            return new DataModels.CellLineModel { ReferenceId = id };
        }
        else if (type == SpecimenType.Organoid)
        {
            return new DataModels.OrganoidModel { ReferenceId = id };
        }
        else if (type == SpecimenType.Xenograft)
        {
            return new DataModels.XenograftModel { ReferenceId = id };
        }
        else
        {
            throw new NotImplementedException("Specimen type is not supported yet");
        }
    }

    private DataModels.DonorModel GetDonorModel(string id)
    {
        return new DataModels.DonorModel { ReferenceId = id };
    }

    private DataModels.DrugScreeningModel[] GetDrugScreeningModels(DrugScreeningModel[] sources)
    {
        return _drugScreeningModelConverter.Convert(sources);
    }
}
