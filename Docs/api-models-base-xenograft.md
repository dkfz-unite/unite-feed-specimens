# Xenograft Data Models
Includes xenograft and xenograft interventions data.

## Xenograft
Includes xenograft data.

_At least one field has to be set_

**`mouse_strain`** - Strain of the mice used in xenograft model.
- Type: _String_
- Example: `"Nude"`

**`group_size`** - Number of mice in the group.
- Type: _Number_
- Limitations: Integer, greater than 0
- Example: `9`

**`implant_type`** - Type of tissue implantation.
- Type: _String_
- Possible values: `"Orhtotopical"`, `"Other"`
- Example: `"Other"`

**`implant_location`** - Location of the implanted tissue.
- Type: _String_
- Possible values: `"Striatal"`, `"Cortical"`, `"Other"`
- Example: `"Cortical"`

**`implanted_cells_number`** - Number of implanted cells.
- Type: _Number_
- Limitations: Integer, greater than 0
- Example: `750000`

**`tumorigenicity`** - The ability to give rise to either benign or malignant progressively growing tumors.
- Type: _Boolean_
- Example: `true`

**`tumor_growth_form`** - Characterizing the growth form of tumor cells.
- Type: _String_
- Possible values: `"Encapsulated"`, `"Invasive"`
- Example: `"Invasive"`

**`survival_days`** - Average survival length of the mice.
- Type: _String_
- Format: "20" or "20-30"
- Limitations: Integer, greater than or equal to 0
- Example: `"20-30"`

**`interventions`** - Interventions applied.
- Type: _Array_
- Element type: _Object([XenograftIntervention](api-specimens-models-xenograft.md#xenograft-intervention))_
- Example: `[{...}, {...}]`

## Xenograft Intervention
Includes xenograft intervention data.

**`type`*** - Intervention type.
- Type: _String_
- Limitations: Maximum length 100
- Example: `"Intervention type"`

**`details`** - Intervention details.
- Type: _String_
- Example: `"Intervention details."`

**`start_date`** - Date, when intervention has started.
- Note: It's hidden and protected. Relative date is shown instead, if calculation was possible.
- Type: _String_
- Format: "YYYY-MM-DD"
- Limitations: Only either `start_date` or `start_day` can be set at once, not both
- Example: `"2020-04-05"`

**`Start_day`** - Relative number of days since specimen creation, when intervention has started.
- Type: _Number_
- Limitations: Integer, greater or equal to 0, only either `start_date` or `start_day` can be set at once, not both
- Example: `5`

**`end_date`** - Date, when intervention has ended.
- Note: It's hidden and protected. Relative date is shown instead, if calculation was possible.
- Type: _String_
- Format: "YYYY-MM-DD"
- Limitations: Integer, greater or equal to `start_date`, only either `end_date` or `duration_days` can be set at once, not both
- Example: `"2020-04-10"`

**`duration_days`** - Intervention duration in days.
- Type: _Number_
- Limitations: Integer, greater or equal to 0, only either `end_date` or `duration_days` can be set at once, not both
- Example: `5`

**`results`** - Intervention results.
- Type: _String_
- Example: `"Intervention results."`

##
**`*`** - Required fields
