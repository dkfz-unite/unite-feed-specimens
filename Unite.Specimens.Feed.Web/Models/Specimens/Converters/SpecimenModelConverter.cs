using System;
using System.Linq;
using Unite.Specimens.Feed.Web.Models.Specimens.Enums;

namespace Unite.Specimens.Feed.Web.Models.Specimens.Converters
{
    public class SpecimenModelConverter
    {
        public Data.Specimens.Models.SpecimenModel Convert(SpecimenModel source)
        {
            var specimenModel = GetSpecimenModel(source);

            specimenModel.Donor = GetDonorModel(source.DonorId);

            specimenModel.Parent = GetSpecimenModel(source.ParentId, source.ParentType);

            specimenModel.MolecularData = GetMolecularDataModel(source.MolecularData);

            return specimenModel;
        }


        private Data.Specimens.Models.SpecimenModel GetSpecimenModel(SpecimenModel source)
        {
            if (source.Tissue != null)
            {
                return GetTissueModel(source);
            }
            else if (source.CellLine != null)
            {
                return GetCellLineModel(source);
            }
            else if (source.Organoid != null)
            {
                return GetOrganoidModel(source);
            }
            else if (source.Xenograft != null)
            {
                return GetXenograftModel(source);
            }
            else
            {
                throw new NotImplementedException("Specimen type is not supported yet");
            }
        }

        private Data.Specimens.Models.SpecimenModel GetSpecimenModel(string id, SpecimenType? type)
        {
            if (string.IsNullOrWhiteSpace(id) || type == null)
            {
                return null;
            }

            if (type == SpecimenType.Tissue)
            {
                return new Data.Specimens.Models.TissueModel { ReferenceId = id };
            }
            else if (type == SpecimenType.CellLine)
            {
                return new Data.Specimens.Models.CellLineModel { ReferenceId = id };
            }
            else if (type == SpecimenType.Organoid)
            {
                return new Data.Specimens.Models.OrganoidModel { ReferenceId = id };
            }
            else if (type == SpecimenType.Xenograft)
            {
                return new Data.Specimens.Models.XenograftModel { ReferenceId = id };
            }
            else
            {
                throw new NotImplementedException("Specimen type is not supported yet");
            }
        }

        private Data.Specimens.Models.DonorModel GetDonorModel(string id)
        {
            var target = new Data.Specimens.Models.DonorModel
            {
                ReferenceId = id
            };

            return target;
        }

        private Data.Specimens.Models.TissueModel GetTissueModel(SpecimenModel source)
        {
            var target = new Data.Specimens.Models.TissueModel
            {
                ReferenceId = source.Id,
                Type = source.Tissue.Type.Value,
                TumorType = source.Tissue.TumorType,
                ExtractionDay = source.Tissue.ExtractionDay,
                Source = source.Tissue.Source
            };

            return target;
        }

        private Data.Specimens.Models.CellLineModel GetCellLineModel(SpecimenModel source)
        {
            var target = new Data.Specimens.Models.CellLineModel
            {
                ReferenceId = source.Id,
                Species = source.CellLine.Species,
                Type = source.CellLine.Type,
                CultureType = source.CellLine.CultureType
            };

            if (source.CellLine.Info != null)
            {
                target.Info = new Data.Specimens.Models.CellLineInfoModel
                {
                    Name = source.CellLine.Info.Name,
                    DepositorName = source.CellLine.Info.DepositorName,
                    DepositorEstablishment = source.CellLine.Info.DepositorEstablishment,
                    EstablishmentDate = source.CellLine.Info.EstablishmentDate,
                    PubMedLink = source.CellLine.Info.PubMedLink,
                    AtccLink = source.CellLine.Info.AtccLink,
                    ExPasyLink = source.CellLine.Info.ExPasyLink
                };
            }

            return target;
        }

        private Data.Specimens.Models.OrganoidModel GetOrganoidModel(SpecimenModel source)
        {
            var target = new Data.Specimens.Models.OrganoidModel
            {
                ReferenceId = source.Id,
                ImplantedCellsNumber = source.Organoid.ImplantedCellsNumber,
                Tumorigenicity = source.Organoid.Tumorigenicity,
                Medium = source.Organoid.Medium,

                Interventions = source.Organoid.Interventions?.Select(intervention =>
                {
                    return new Data.Specimens.Models.OrganoidInterventionModel
                    {
                        Type = intervention.Type,
                        Details = intervention.Details,
                        StartDay = intervention.StartDay,
                        DurationDays = intervention.DurationDays,
                        Results = intervention.Results
                    };

                }).ToArray()
            };

            return target;
        }

        private Data.Specimens.Models.XenograftModel GetXenograftModel(SpecimenModel source)
        {
            var target = new Data.Specimens.Models.XenograftModel
            {
                ReferenceId = source.Id,
                MouseStrain = source.Xenograft.MouseStrain,
                GroupSize = source.Xenograft.GroupSize,
                ImplantType = source.Xenograft.ImplantType,
                TissueLocation = source.Xenograft.TissueLocation,
                ImplantedCellsNumber = source.Xenograft.ImplantedCellsNumber,
                Tumorigenicity = source.Xenograft.Tumorigenicity,
                TumorGrowthForm = source.Xenograft.TumorGrowthForm,
                SurvivalDaysFrom = ParseDuration(source.Xenograft.SurvivalDays)?.From,
                SurvivalDaysTo = ParseDuration(source.Xenograft.SurvivalDays)?.To,

                Interventions = source.Xenograft.Interventions?.Select(intervention =>
                {
                    return new Data.Specimens.Models.XenograftInterventionModel
                    {
                        Type = intervention.Type,
                        Details = intervention.Details,
                        StartDay = intervention.StartDay,
                        DurationDays = intervention.DurationDays,
                        Results = intervention.Results
                    };

                }).ToArray()

            };

            return target;
        }

        private Data.Specimens.Models.MolecularDataModel GetMolecularDataModel(MolecularDataModel source)
        {
            if (source == null)
            {
                return null;
            }

            var target = new Data.Specimens.Models.MolecularDataModel();

            target.MgmtStatus = source.MgmtStatus;
            target.IdhStatus = source.IdhStatus;
            target.IdhMutation = source.IdhMutation;
            target.GeneExpressionSubtype = source.GeneExpressionSubtype;
            target.MethylationSubtype = source.MethylationSubtype;
            target.GcimpMethylation = source.GcimpMethylation;

            return target;
        }


        private (int From, int To)? ParseDuration(string duration)
        {
            if (string.IsNullOrWhiteSpace(duration))
            {
                return null;
            }

            if (duration.Contains('-'))
            {
                var parts = duration.Split('-');

                var start = int.Parse(parts[0]);
                var end = int.Parse(parts[1]);

                return (start, end);
            }
            else
            {
                var start = int.Parse(duration);
                var end = int.Parse(duration);

                return (start, end);
            }
        }
    }
}
