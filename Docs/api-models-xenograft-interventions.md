# Xenograft Intervention Upload Data Model
Xenograft intervention upload data model.

**`donor_id`*** - Donor pseudonymised identifier.
- Note: Donor should be present in the system.
- Type: _String_
- Limitations: Maximum length 255
- Example: `"DO1"`

**`specimen_id`*** - Specimen identifier.
- Note: Specimen should be present in the system.
- Type: _String_
- Limitations: Maximum length 255
- Example: `"XE1CL1TI1"`

**`data`** - Interventions data.
- Type: _Array_
- Element type: _Object([XenograftIntervention](api-models-base-xenograft.md#xenograft-intervention))_
- Example: `[{...}, {...}]`

##
**`*`** - Required fields
