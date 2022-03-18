# Xenograft Data Models
Includes xenograft and xenograft interventions data.

## Xenograft
Includes xenograft data.

_At least one field has to be set_

**`MouseStrain`** - Strain of the mice used in xenograft model.
- Type: _String_
- Example: `"Nude"`

**`GroupSize`** - Number of mice in the group.
- Type: _Number_
- Limitations: Integer, greater than 0
- Example: `9`

**`ImplantType`** - Type of tissue implantation.
- Type: _String_
- Possible values: `"Orhtotopical"`, `"Other"`
- Example: `"Other"`

**`TissueLocation`** - Location of the implanted tissue.
- Type: _String_
- Possible values: `"Striatal"`, `"Cortical"`, `"Other"`
- Example: `"Cortical"`

**`ImplantedCellsNumber`** - Number of implanted cells.
- Type: _Number_
- Limitations: Integer, greater than 0
- Example: `750000`

**`Tumorigenicity`** - The ability to give rise to either benign or malignant progressively growing tumors.
- Type: _Boolean_
- Example: `true`

**`TumorGrowthForm`** - Characterizing the growth form of tumor cells.
- Type: _String_
- Possible values: `"Encapsulated"`, `"Invasive"`
- Example: `"Invasive"`

**`SurvivalDays`** - Average survival length of the mice.
- Type: _String_
- Format: "20" or "20-30"
- Limitations: Integer, greater than or equal to 0
- Example: `"20-30"`

**`Interventions`** - Interventions applied.
- Type: _Array_
- Element type: _Object([XenograftIntervention](https://github.com/dkfz-unite/unite-specimens-feed/blob/main/Docs/api-specimens-models-xenograft.md#xenograft-intervention))_
- Example: `[{...}, {...}]`

## Xenograft Intervention
Includes xenograft intervention data.

**`Type`*** - Intervention type.
- Type: _String_
- Limitations: Maximum length 100
- Example: `"Intervention type"`

**`Details`** - Intervention details.
- Type: _String_
- Example: `"Intervention details."`

**`StartDate`** - Date, when intervention has started.
- Note: It's hidden and protected. Relative date is shown instead, if calculation was possible.
- Type: _String_
- Format: "YYYY-MM-DDTHH-MM-SS"
- Limitations: Only either 'StartDateDate' or 'StartDay' can be set at once, not both
- Example: `"2020-01-07T00:00:00"`

**`StartDay`** - Relative number of days since specimen creation, when intervention has started.
- Type: _Number_
- Limitations: Integer, greater or equal to 0, only either 'StartDate' or 'StartDay' can be set at once, not both
- Example: `7`

**`EndDate`** - Date, when intervention has ended.
- Note: It's hidden and protected. Relative date is shown instead, if calculation was possible.
- Type: _String_
- Format: "YYYY-MM-DDTHH-MM-SS"
- Limitations: Only either 'EndDateDate' or 'DurationDays' can be set at once, not both
- Example: `"2020-01-27T00:00:00"`

**`DurationDays`** - Intervention duration in days.
- Type: _Number_
- Limitations: Integer, greater or equal to 0, only either 'EndDate' or 'DurationDays' can be set at once, not both
- Example: `20`

**`Results`** - Intervention results.
- Type: _String_
- Example: `"Intervention results."`

##
**`*`** - Required fields
