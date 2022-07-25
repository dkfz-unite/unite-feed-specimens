using Unite.Specimens.Feed.Web.Models.Base.Enums;

namespace Unite.Specimens.Feed.Web.Models;

public record DrugScreeningDataModel
{
    private string _donorId;
    private string _specimenId;
    private SpecimenType _specimenType;


    public string DonorId { get => _donorId?.Trim(); init => _donorId = value; }
    public string SpecimenId { get => _specimenId?.Trim(); init => _specimenId = value; }
    public SpecimenType SpecimenType { get => _specimenType; init => _specimenType = value; }

    public Base.DrugScreeningModel[] Data { get; init; }
}
