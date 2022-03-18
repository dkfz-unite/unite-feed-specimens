# Organoid Data Model
Includes organoid and organoid interventions data.

## Organoid
Includes organoid data.

_At least one field has to be set_

**`Medium`** - Organoid medium.
- Type: _String_
- Example: `"Some medium"`

**`ImplantedCellsNumber`** - Number of implanted cells.
- Type: _Number_
- Limitations: Integer, greater than 0
- Example: `1500000`

**`Tumorigenicity`** - The ability to give rise to either benign or malignant progressively growing tumors.
- Type: _Boolean_
- Example: `true`

**`Interventions`** - Interventions applied.
- Type: _Array_
- Element type: _Object([Organoid Intervention](https://github.com/dkfz-unite/unite-specimens-feed/blob/main/Docs/api-specimens-models-organoid.md#organoid-intervention))_
- Example: `[{...}, {...}]`

## Organoid Intervention
Includes Organoid intervention data.

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
