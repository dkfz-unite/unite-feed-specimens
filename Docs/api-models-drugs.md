# Drug ScreeningS Upload Data Model
Drug screening upload data model.

**`donor_id`*** - Donor pseudonymised identifier.
- Note: Donor should be present in the system.
- Type: _String_
- Limitations: Maximum length 255
- Example: `"DO1"`

**`specimen_id`*** - Specimen identifier.
- Note: Specimen should be present in the system.
- Type: _String_
- Limitations: Maximum length 255
- Example: `"CL1TI1"`

**`specimen_type`*** - Specimen type.
- Note: Drugs screening data can not be uploaded for donor materials.
- Type: _String_
- Possible values: `"Line"`, `"Organoid"`, `"Xenograft"`
- Example: `"Line"`

**`data`*** - Drugs screening data.
- Type: _Array_
- Element type: _Object([DrugScreening](api-models-base-drug.md))_
- Example: `[{...}, {...}]`

##
**`*`** - Required fields
