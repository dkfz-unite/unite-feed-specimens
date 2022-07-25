using Unite.Specimens.Feed.Web.Models.Base.Enums;

using DataModels = Unite.Specimens.Feed.Data.Specimens.Models;

namespace Unite.Specimens.Feed.Web.Models.Base.Converters;


public class SpecimenDataModelConverter
{
    private readonly TissueModelConverter _tissueModelConverter;
    private readonly CellLineModelConverter _cellLineModelConverter;
    private readonly OrganoidModelConverter _organoidModelConverter;
    private readonly XenograftModelConverter _xenograftModelConverter;
    private readonly MolecularDataModelConverter _molecularDataModelConverter;
    private readonly DrugScreeningModelConverter _drugScreeningModelConverter;


    public SpecimenDataModelConverter()
    {
        _tissueModelConverter = new TissueModelConverter();
        _cellLineModelConverter = new CellLineModelConverter();
        _organoidModelConverter = new OrganoidModelConverter();
        _xenograftModelConverter = new XenograftModelConverter();
        _molecularDataModelConverter = new MolecularDataModelConverter();
        _drugScreeningModelConverter = new DrugScreeningModelConverter();
    }


    public DataModels.SpecimenModel Convert(in SpecimenDataModel source)
    {
        var target = GetSpecimenModel(source);

        target.Donor = GetDonorModel(source.DonorId);
        target.Parent = GetSpecimenModel(source.ParentId, source.ParentType);
        target.MolecularData = GetMolecularDataModel(source.MolecularData);
        target.DrugScreeningData = GetDrugScreeningModels(source.DrugScreeningData);

        target.CreationDate = FromDateTime(source.CreationDate);
        target.CreationDay = source.CreationDay;

        return target;
    }


    private DataModels.SpecimenModel GetSpecimenModel(SpecimenDataModel source)
    {
        if (source.Tissue != null)
        {
            return _tissueModelConverter.Convert(source.Id, source.Tissue);
        }
        else if (source.CellLine != null)
        {
            return _cellLineModelConverter.Convert(source.Id, source.CellLine);
        }
        else if (source.Organoid != null)
        {
            return _organoidModelConverter.Convert(source.Id, source.Organoid);
        }
        else if (source.Xenograft != null)
        {
            return _xenograftModelConverter.Convert(source.Id, source.Xenograft);
        }
        else
        {
            throw new NotImplementedException("Specimen type is not supported yet");
        }
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

    private DataModels.MolecularDataModel GetMolecularDataModel(MolecularDataModel source)
    {
        return _molecularDataModelConverter.Convert(source);
    }

    private DataModels.DrugScreeningModel[] GetDrugScreeningModels(DrugScreeningModel[] sources)
    {
        return _drugScreeningModelConverter.Convert(sources);
    }


    private static DateOnly? FromDateTime(DateTime? date)
    {
        return date != null ? DateOnly.FromDateTime(date.Value) : null;
    }
}
