# Xenograft Data Model
Includes xenograft and xenograft interventions data.

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
