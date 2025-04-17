namespace Unite.Specimens.Feed.Data.Models;

public class LineInfoModel
{
    public string Name { get; set; }
    public string DepositorName { get; set; }
    public string DepositorEstablishment { get; set; }
    public DateOnly? EstablishmentDate { get; set; }

    public string PubmedLink { get; set; }
    public string AtccLink { get; set; }
    public string ExpasyLink { get; set; }
}
