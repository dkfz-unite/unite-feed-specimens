using Unite.Data.Entities.Specimens.Enums;
using Unite.Specimens.Feed.Web.Models.Base;
using Unite.Specimens.Feed.Web.Models.Base.Converters;

using DataModels = Unite.Specimens.Feed.Data.Specimens.Models;

namespace Unite.Specimens.Feed.Web.Models.Converters;


public class SpecimenDataModelConverter : BaseConverter
{
    private readonly MaterialModelConverter _materialModelConverter;
    private readonly LineModelConverter _lineModelConverter;
    private readonly OrganoidModelConverter _organoidModelConverter;
    private readonly XenograftModelConverter _xenograftModelConverter;
    private readonly MolecularDataModelConverter _molecularDataModelConverter;
    private readonly InterventionModelConverter _interventionModelConverter;
    private readonly DrugScreeningModelConverter _drugScreeningModelConverter;


    public SpecimenDataModelConverter()
    {
        _materialModelConverter = new MaterialModelConverter();
        _lineModelConverter = new LineModelConverter();
        _organoidModelConverter = new OrganoidModelConverter();
        _xenograftModelConverter = new XenograftModelConverter();
        _molecularDataModelConverter = new MolecularDataModelConverter();
        _interventionModelConverter = new InterventionModelConverter();
        _drugScreeningModelConverter = new DrugScreeningModelConverter();
    }


    public DataModels.SpecimenModel Convert(in SpecimenDataModel source)
    {
        var target = GetSpecimenModel(source);

        target.Donor = GetDonorModel(source.DonorId);
        target.Parent = GetSpecimenModel(source.ParentId, source.ParentType);
        target.MolecularData = GetMolecularDataModel(source.MolecularData);
        target.Interventions = GetInterventionModels(source.Interventions);
        target.DrugScreenings = GetDrugScreeningModels(source.DrugScreenings);

        target.CreationDate = source.CreationDate;
        target.CreationDay = source.CreationDay;

        return target;
    }


    private DataModels.SpecimenModel GetSpecimenModel(SpecimenDataModel source)
    {
        if (source.Material != null)
            return _materialModelConverter.Convert(source.Id, source.Material);
        else if (source.Line != null)
            return _lineModelConverter.Convert(source.Id, source.Line);
        else if (source.Organoid != null)
            return _organoidModelConverter.Convert(source.Id, source.Organoid);
        else if (source.Xenograft != null)
            return _xenograftModelConverter.Convert(source.Id, source.Xenograft);
        else
            throw new NotImplementedException("Specimen type is not supported yet");
    }

    protected DataModels.SpecimenModel GetSpecimenModel(string id, SpecimenType? type)
    {
        if (string.IsNullOrWhiteSpace(id) || type == null)
            return null;

        return base.GetSpecimenModel(id, type.Value);
    }

    private DataModels.MolecularDataModel GetMolecularDataModel(MolecularDataModel source)
    {
        return _molecularDataModelConverter.Convert(source);
    }

    private DataModels.InterventionModel[] GetInterventionModels(InterventionModel[] sources)
    {
        return _interventionModelConverter.Convert(sources);
    }

    private DataModels.DrugScreeningModel[] GetDrugScreeningModels(DrugScreeningModel[] sources)
    {
        return _drugScreeningModelConverter.Convert(sources);
    }
}
