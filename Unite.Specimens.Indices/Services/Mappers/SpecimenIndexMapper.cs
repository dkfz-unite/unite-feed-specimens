﻿using System;
using System.Collections.Generic;
using System.Linq;
using Unite.Data.Entities.Specimens;
using Unite.Data.Entities.Specimens.Cells;
using Unite.Data.Entities.Specimens.Organoids;
using Unite.Data.Entities.Specimens.Tissues;
using Unite.Data.Entities.Specimens.Xenografts;
using Unite.Data.Extensions;
using Unite.Indices.Entities.Basic.Specimens;

using OrganoidIntervention = Unite.Data.Entities.Specimens.Organoids.Intervention;
using XenograftIntervention = Unite.Data.Entities.Specimens.Xenografts.Intervention;

namespace Unite.Specimens.Indices.Services.Mappers
{
    internal class SpecimenIndexMapper
    {
        internal void Map(in Specimen specimen, SpecimenIndex index, DateTime? diagnosisDate)
        {
            if (specimen == null)
            {
                return;
            }

            index.Id = specimen.Id;
            index.ParentId = specimen.ParentId;
            index.CreationDay = specimen.CreationDate.RelativeFrom(diagnosisDate) ?? specimen.CreationDay;

            index.Tissue = CreateFrom(specimen.Tissue, specimen.MolecularData);
            index.CellLine = CreateFrom(specimen.CellLine, specimen.MolecularData);
            index.Organoid = CreateFrom(specimen.Organoid, specimen.MolecularData, specimen.CreationDate);
            index.Xenograft = CreateFrom(specimen.Xenograft, specimen.MolecularData, specimen.CreationDate);
        }


        private static TissueIndex CreateFrom(in Tissue tissue, in MolecularData molecularData)
        {
            if (tissue == null)
            {
                return null;
            }

            var index = new TissueIndex();

            index.ReferenceId = tissue.ReferenceId;

            index.Type = tissue.TypeId?.ToDefinitionString();
            index.TumorType = tissue.TumorTypeId?.ToDefinitionString();
            index.Source = tissue.Source?.Value;

            index.MolecularData = CreateFrom(molecularData);

            return index;
        }

        private static CellLineIndex CreateFrom(in CellLine cellLine, in MolecularData molecularData)
        {
            if (cellLine == null)
            {
                return null;
            }

            var index = new CellLineIndex();

            index.ReferenceId = cellLine.ReferenceId;

            index.Species = cellLine.SpeciesId?.ToDefinitionString();
            index.Type = cellLine.TypeId?.ToDefinitionString();
            index.CultureType = cellLine.CultureTypeId?.ToDefinitionString();

            index.Name = cellLine.Info?.Name;
            index.DepositorName = cellLine.Info?.DepositorName;
            index.DepositorEstablishment = cellLine.Info?.DepositorEstablishment;
            index.EstablishmentDate = cellLine.Info?.EstablishmentDate;

            index.PubMedLink = cellLine.Info?.PubMedLink;
            index.AtccLink = cellLine.Info?.AtccLink;
            index.ExPasyLink = cellLine.Info?.ExPasyLink;

            index.MolecularData = CreateFrom(molecularData);

            return index;
        }

        private static OrganoidIndex CreateFrom(in Organoid organoid, in MolecularData molecularData, DateTime? specimenCreationDate)
        {
            if (organoid == null)
            {
                return null;
            }

            var index = new OrganoidIndex();

            index.ReferenceId = organoid.ReferenceId;
            index.ImplantedCellsNumber = organoid.ImplantedCellsNumber;
            index.Tumorigenicity = organoid.Tumorigenicity;
            index.Medium = organoid.Medium;

            index.MolecularData = CreateFrom(molecularData);

            index.Interventions = CreateFrom(organoid.Interventions, specimenCreationDate);

            return index;
        }

        private static OrganoidInterventionIndex[] CreateFrom(in IEnumerable<OrganoidIntervention> interventions, DateTime? specimenCreationDate)
        {
            if (interventions == null || !interventions.Any())
            {
                return null;
            }

            var indices = interventions.Select(intervention =>
            {
                var index = new OrganoidInterventionIndex();

                index.Type = intervention.Type.Name;
                index.Details = intervention.Details;
                index.StartDay = intervention.StartDate.RelativeFrom(specimenCreationDate) ?? intervention.StartDay;
                index.DurationDays = intervention.EndDate.RelativeFrom(intervention.StartDate) ?? intervention.DurationDays;
                index.Results = intervention.Results;

                return index;

            }).ToArray();

            return indices;
        }

        private static XenograftIndex CreateFrom(in Xenograft xenograft, in MolecularData molecularData, DateTime? specimenCreationDate)
        {
            if (xenograft == null)
            {
                return null;
            }

            var index = new XenograftIndex();

            index.ReferenceId = xenograft.ReferenceId;

            index.MouseStrain = xenograft.MouseStrain;
            index.GroupSize = xenograft.GroupSize;
            index.ImplantType = xenograft.ImplantTypeId?.ToDefinitionString();
            index.TissueLocation = xenograft.TissueLocationId?.ToDefinitionString();
            index.ImplantedCellsNumber = xenograft.ImplantedCellsNumber;
            index.Tumorigenicity = xenograft.Tumorigenicity;
            index.TumorGrowthForm = xenograft.TumorGrowthFormId?.ToDefinitionString();
            index.SurvivalDaysFrom = xenograft.SurvivalDaysFrom;
            index.SurvivalDaysTo = xenograft.SurvivalDaysTo;

            index.MolecularData = CreateFrom(molecularData);

            index.Interventions = CreateFrom(xenograft.Interventions, specimenCreationDate);

            return index;
        }

        private static XenograftInterventionIndex[] CreateFrom(in IEnumerable<XenograftIntervention> interventions, DateTime? specimenCreationDate)
        {
            if (interventions == null || !interventions.Any())
            {
                return null;
            }

            var indices = interventions.Select(intervention =>
            {
                var index = new XenograftInterventionIndex();

                index.Type = intervention.Type.Name;
                index.Details = intervention.Details;
                index.StartDay = intervention.StartDate.RelativeFrom(specimenCreationDate) ?? intervention.StartDay;
                index.DurationDays = intervention.EndDate.RelativeFrom(intervention.StartDate) ?? intervention.DurationDays;
                index.Results = intervention.Results;

                return index;

            }).ToArray();

            return indices;
        }

        private static MolecularDataIndex CreateFrom(in MolecularData molecularData)
        {
            if (molecularData == null)
            {
                return null;
            }

            var index = new MolecularDataIndex();

            index.MgmtStatus = molecularData.MgmtStatusId?.ToDefinitionString();
            index.IdhStatus = molecularData.IdhStatusId?.ToDefinitionString();
            index.IdhMutation = molecularData.IdhMutationId?.ToDefinitionString();
            index.GeneExpressionSubtype = molecularData.GeneExpressionSubtypeId?.ToDefinitionString();
            index.MethylationSubtype = molecularData.MethylationSubtypeId?.ToDefinitionString();
            index.GcimpMethylation = molecularData.GcimpMethylation;

            return index;
        }
    }
}
