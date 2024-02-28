# Intervention Upload Data Model
Intervention upload data model.

**`donor_id`*** - Donor pseudonymised identifier.
- Note: Donor should be present in the system.
- Type: _String_
- Limitations: Maximum length 255
- Example: `"Donor1"`

**`specimen_id`*** - Specimen identifier.
- Note: Specimen should be present in the system.
- Type: _String_
- Limitations: Maximum length 255
- Example: `"Organoid1"`

**`specimen_type`*** - Specimen type.
- Note: Intervention data can not be uploaded for donor materials and cell lines.
- Type: _String_
- Possible values: `"Organoid"`, `"Xenograft"`
- Example: `"Organoid"`

**`data`*** - Interventions data.
- Type: _Array_
- Element type: _Object([Intervention](api-models-base-intervention.md))_
- Example: `[{...}, {...}]`

##
**`*`** - Required fields
