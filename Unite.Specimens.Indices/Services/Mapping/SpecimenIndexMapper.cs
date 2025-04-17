using Unite.Data.Entities.Specimens;
using Unite.Data.Entities.Specimens.Analysis.Drugs;
using Unite.Data.Entities.Specimens.Analysis.Enums;
using Unite.Essentials.Extensions;
using Unite.Indices.Entities.Basic.Specimens;
using Unite.Indices.Entities.Basic.Specimens.Drugs;

namespace Unite.Specimens.Indices.Services.Mapping;

public class SpecimenIndexMapper
{
    /// <summary>
    /// Creates an index from the entity. Returns null if entity is null.
    /// </summary>
    /// <param name="entity">Entity.</param>
    /// <param name="enrollmentDate">Enrollment date (anchor date for calculation of relative days).</param>
    /// <typeparam name="T">Type of the index.</typeparam>
    /// <returns>Index created from the entity.</returns>
    public static T CreateFrom<T>(in Specimen entity, DateOnly? enrollmentDate) where T : SpecimenIndex, new()
    {
        if (entity == null)
        {
            return null;
        }

        var index = new T();

        Map(entity, index, enrollmentDate);

        return index;
    }

    /// <summary>
    /// Maps entity to index. Does nothing if either entity or index is null.
    /// </summary>
    /// <param name="entity">Entity.</param>
    /// <param name="index">Index.</param>
    /// <param name="enrollmentDate">Enrollment date (anchor date for calculation of relative days).</param> 
    public static void Map(in Specimen entity, SpecimenIndex index, DateOnly? enrollmentDate)
    {
        if (entity == null || index == null)
        {
            return;
        }

        index.Id = entity.Id;
        index.ReferenceId = entity.ReferenceId;
        index.Type = entity.TypeId.ToDefinitionString();

        index.Material = CreateFromMaterial(entity, enrollmentDate);
        index.Line = CreateFromLine(entity, enrollmentDate);
        index.Organoid = CreateFromOrganoid(entity, enrollmentDate);
        index.Xenograft = CreateFromXenograft(entity, enrollmentDate);
    }


    private static MaterialIndex CreateFromMaterial(in Specimen entity, DateOnly? enrollmentDate)
    {
        if (entity.Material == null)
        {
            return null;
        }

        return new MaterialIndex
        {
            Id = entity.Id,
            ReferenceId = entity.ReferenceId,
            CreationDay = entity.CreationDay ?? entity.CreationDate?.RelativeFrom(enrollmentDate),

            Type = entity.Material.TypeId?.ToDefinitionString(),
            FixationType = entity.Material.FixationTypeId?.ToDefinitionString(),
            TumorType = entity.Material.TumorTypeId?.ToDefinitionString(),
            TumorGrade = entity.Material.TumorGrade,
            Source = entity.Material.Source?.Value,

            MolecularData = CreateFrom(entity.MolecularData)
        };
    }

    private static LineIndex CreateFromLine(in Specimen entity, DateOnly? enrollmentDate)
    {
        if (entity.Line == null)
        {
            return null;
        }

        return new LineIndex
        {
            Id = entity.Id,
            ReferenceId = entity.ReferenceId,
            CreationDay = entity.CreationDay ?? entity.CreationDate?.RelativeFrom(enrollmentDate),

            CellsSpecies = entity.Line.CellsSpeciesId?.ToDefinitionString(),
            CellsType = entity.Line.CellsTypeId?.ToDefinitionString(),
            CellsCultureType = entity.Line.CellsCultureTypeId?.ToDefinitionString(),

            Name = entity.Line.Info?.Name,
            DepositorName = entity.Line.Info?.DepositorName,
            DepositorEstablishment = entity.Line.Info?.DepositorEstablishment,
            EstablishmentDate = entity.Line.Info?.EstablishmentDate,

            PubmedLink = entity.Line.Info?.PubmedLink,
            AtccLink = entity.Line.Info?.AtccLink,
            ExpasyLink = entity.Line.Info?.ExpasyLink,

            MolecularData = CreateFrom(entity.MolecularData),
            Interventions = CreateFrom(entity.Interventions, entity.CreationDate),
            DrugScreenings = CreateFrom(entity.SpecimenSamples?.FirstOrDefault(sample => sample.Analysis.TypeId == AnalysisType.DSA)?.DrugScreenings)
        };
    }

    private static OrganoidIndex CreateFromOrganoid(in Specimen entity, DateOnly? enrollmentDate)
    {
        if (entity.Organoid == null)
        {
            return null;
        }

        return new OrganoidIndex
        {
            Id = entity.Id,
            ReferenceId = entity.ReferenceId,
            CreationDay = entity.CreationDay ?? entity.CreationDate?.RelativeFrom(enrollmentDate),

            ImplantedCellsNumber = entity.Organoid.ImplantedCellsNumber,
            Tumorigenicity = entity.Organoid.Tumorigenicity,
            Medium = entity.Organoid.Medium,

            MolecularData = CreateFrom(entity.MolecularData),
            Interventions = CreateFrom(entity.Interventions, entity.CreationDate),
            DrugScreenings = CreateFrom(entity.SpecimenSamples?.FirstOrDefault(sample => sample.Analysis.TypeId == AnalysisType.DSA)?.DrugScreenings)
        };
    }

    private static XenograftIndex CreateFromXenograft(in Specimen entity, DateOnly? enrollmentDate)
    {
        if (entity.Xenograft == null)
        {
            return null;
        }

        return new XenograftIndex
        {
            Id = entity.Id,
            ReferenceId = entity.ReferenceId,
            CreationDay = entity.CreationDay ?? entity.CreationDate?.RelativeFrom(enrollmentDate),

            MouseStrain = entity.Xenograft.MouseStrain,
            GroupSize = entity.Xenograft.GroupSize,
            ImplantType = entity.Xenograft.ImplantTypeId?.ToDefinitionString(),
            ImplantLocation = entity.Xenograft.ImplantLocationId?.ToDefinitionString(),
            ImplantedCellsNumber = entity.Xenograft.ImplantedCellsNumber,
            Tumorigenicity = entity.Xenograft.Tumorigenicity,
            TumorGrowthForm = entity.Xenograft.TumorGrowthFormId?.ToDefinitionString(),
            SurvivalDaysFrom = entity.Xenograft.SurvivalDaysFrom,
            SurvivalDaysTo = entity.Xenograft.SurvivalDaysTo,

            MolecularData = CreateFrom(entity.MolecularData),
            Interventions = CreateFrom(entity.Interventions, entity.CreationDate),
            DrugScreenings = CreateFrom(entity.SpecimenSamples?.FirstOrDefault(sample => sample.Analysis.TypeId == AnalysisType.DSA)?.DrugScreenings)
        };
    }

    private static MolecularDataIndex CreateFrom(in MolecularData entity)
    {
        if (entity == null)
        {
            return null;
        }

        return new MolecularDataIndex
        {
            MgmtStatus = entity.MgmtStatusId?.ToDefinitionString(),
            IdhStatus = entity.IdhStatusId?.ToDefinitionString(),
            IdhMutation = entity.IdhMutationId?.ToDefinitionString(),
            GeneExpressionSubtype = entity.GeneExpressionSubtypeId?.ToDefinitionString(),
            MethylationSubtype = entity.MethylationSubtypeId?.ToDefinitionString(),
            GcimpMethylation = entity.GcimpMethylation
        };
    }

    private static InterventionIndex[] CreateFrom(in IEnumerable<Intervention> entities, DateOnly? creationDate)
    {
        if (entities?.Any() != true)
        {
            return null;
        }

        return entities.Select(entity =>
        {
            return new InterventionIndex
            {
                Type = entity.Type.Name,
                Details = entity.Details,
                StartDay = entity.StartDay ?? entity.StartDate?.RelativeFrom(creationDate),
                DurationDays = entity.DurationDays ?? entity.EndDate?.RelativeFrom(entity.StartDate),
                Results = entity.Results
            };

        }).ToArray();
    }

    private static DrugScreeningIndex[] CreateFrom(in IEnumerable<DrugScreening> entities)
    {
        if (entities?.Any() != true)
        {
            return null;
        }

        return entities.Select(entity =>
        {
            return new DrugScreeningIndex
            {
                Drug = entity.Entity.Name,
                Gof = entity.Gof,
                Dss = entity.Dss,
                DssS = entity.DssS,
                DoseMin = entity.DoseMin,
                DoseMax = entity.DoseMax,
                Dose25 = entity.Dose25,
                Dose50 = entity.Dose50,
                Dose75 = entity.Dose75
            };

        }).ToArray();
    }
}
