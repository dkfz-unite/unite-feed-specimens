using Unite.Data.Entities.Specimens.Enums;
using Unite.Specimens.Feed.Web.Models.Base;
using Unite.Specimens.Feed.Web.Models.Base.Converters;

namespace Unite.Specimens.Feed.Web.Models.Converters;

public class SpecimenDataModelsConverter : BaseConverter
{
    private readonly MaterialModelsConverter _materialModelConverter;
    private readonly LineModelsConverter _lineModelConverter;
    private readonly OrganoidModelsConverter _organoidModelConverter;
    private readonly XenograftModelsConverter _xenograftModelConverter;
    private readonly MolecularDataModelsConverter _molecularDataModelConverter;
    private readonly InterventionModelsConverter _interventionModelConverter;
    private readonly DrugScreeningModelsConverter _drugScreeningModelConverter;


    public SpecimenDataModelsConverter()
    {
        _materialModelConverter = new MaterialModelsConverter();
        _lineModelConverter = new LineModelsConverter();
        _organoidModelConverter = new OrganoidModelsConverter();
        _xenograftModelConverter = new XenograftModelsConverter();
        _molecularDataModelConverter = new MolecularDataModelsConverter();
        _interventionModelConverter = new InterventionModelsConverter();
        _drugScreeningModelConverter = new DrugScreeningModelsConverter();
    }


    public Data.Models.SpecimenModel Convert(in SpecimenDataModel models)
    {
        if (models == null)
            return null;
            
        var target = GetSpecimenModel(models);
        target.Donor = GetDonorModel(models.DonorId);
        target.Parent = GetSpecimenModel(models.ParentId, models.ParentType);
        target.MolecularData = GetMolecularDataModel(models.MolecularData);
        target.Interventions = GetInterventionModels(models.Interventions);
        target.DrugScreenings = GetDrugScreeningModels(models.DrugScreenings);
        target.CreationDate = models.CreationDate;
        target.CreationDay = models.CreationDay;

        return target;
    }

    public Data.Models.SpecimenModel[] Convert(in SpecimenDataModel[] models)
    {
        if (models == null)
            return null;

        return models.Select(model => Convert(model)).ToArray();
    }


    private Data.Models.SpecimenModel GetSpecimenModel(SpecimenDataModel models)
    {
        if (models.Material != null)
            return _materialModelConverter.Convert(models.Id, models.Material);
        else if (models.Line != null)
            return _lineModelConverter.Convert(models.Id, models.Line);
        else if (models.Organoid != null)
            return _organoidModelConverter.Convert(models.Id, models.Organoid);
        else if (models.Xenograft != null)
            return _xenograftModelConverter.Convert(models.Id, models.Xenograft);
        else
            throw new NotImplementedException("Specimen type is not supported yet");
    }

    protected Data.Models.SpecimenModel GetSpecimenModel(string id, SpecimenType? type)
    {
        if (string.IsNullOrWhiteSpace(id) || type == null)
            return null;

        return base.GetSpecimenModel(id, type.Value);
    }

    private Data.Models.MolecularDataModel GetMolecularDataModel(MolecularDataModel source)
    {
        return _molecularDataModelConverter.Convert(source);
    }

    private Data.Models.InterventionModel[] GetInterventionModels(InterventionModel[] sources)
    {
        return _interventionModelConverter.Convert(sources);
    }

    private Data.Models.DrugScreeningModel[] GetDrugScreeningModels(DrugScreeningModel[] sources)
    {
        return _drugScreeningModelConverter.Convert(sources);
    }
}
