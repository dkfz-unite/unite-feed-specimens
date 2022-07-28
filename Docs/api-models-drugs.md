# Drug Screening Upload Data Model
Drug screening upload data model.

**`DonorId`*** - Donor pseudonymised identifier.
- Note: Donor should be present in the system.
- Type: _String_
- Limitations: Maximum length 255
- Example: `"DO1"`

**`SpecimenId`*** - Specimen identifier.
- Note: Specimen should be present in the system.
- Type: _String_
- Limitations: Maximum length 255
- Example: `"CL1TI1"`

**`SpecimenType`** - Specimen type.
- Note: Drugs screening data can not be uploaded for tissues.
- Type: _String_
- Possible values: `"CellLine"`, `"Organoid"`, `"Xenograft"`
- Example: `"CellLine"`

**`Data`** - Drugs screening data.
- Type: _Array_
- Element type: _Object([DrugScreening](https://github.com/dkfz-unite/unite-specimens-feed/blob/main/Docs/api-models-base-drugs.md))_
- Example: `[{...}, {...}]`

##
**`*`** - Required fields
