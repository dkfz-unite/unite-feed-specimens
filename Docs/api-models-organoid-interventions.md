# Organoid Intervention Upload Data Model
Organoid intervention upload data model.

**`donor_id`*** - Donor pseudonymised identifier.
- Note: Donor should be present in the system.
- Type: _String_
- Limitations: Maximum length 255
- Example: `"DO1"`

**`specimen_id`*** - Specimen identifier.
- Note: Specimen should be present in the system.
- Type: _String_
- Limitations: Maximum length 255
- Example: `"OR1CL1TI1"`

**`data`** - Interventions data.
- Type: _Array_
- Element type: _Object([OrganoidIntervention](api-models-base-organoid.md#organoid-intervention))_
- Example: `[{...}, {...}]`

##
**`*`** - Required fields
