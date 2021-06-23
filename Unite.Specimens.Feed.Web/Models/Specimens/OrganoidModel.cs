using Unite.Specimens.Feed.Web.Models.Specimens.Extensions;

namespace Unite.Specimens.Feed.Web.Models.Specimens
{
    public class OrganoidModel
    {
        public int? ImplantedCellsNumber { get; set; }
        public bool? Tumorigenicity { get; set; }
        public string Medium { get; set; }

        public OrganoidInterventionModel[] Interventions { get; set; }


        public void Sanitise()
        {
            Medium = Medium?.Trim();

            Interventions?.ForEach(intervention => intervention.Sanitise());
        }
    }
}
