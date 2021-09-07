using System;

namespace Unite.Specimens.Feed.Web.Services.Specimens
{
    public class CellLineInfoModel
    {
        public string Name { get; set; }
        public string DepositorName { get; set; }
        public string DepositorEstablishment { get; set; }
        public DateTime? EstablishmentDate { get; set; }

        public string PubMedLink { get; set; }
        public string AtccLink { get; set; }
        public string ExPasyLink { get; set; }


        public void Sanitise()
        {
            Name = Name?.Trim();
            DepositorName = DepositorName?.Trim();
            DepositorEstablishment = DepositorEstablishment?.Trim();

            PubMedLink = PubMedLink?.Trim();
            AtccLink = AtccLink?.Trim();
            ExPasyLink = ExPasyLink?.Trim();
        }
    }
}
