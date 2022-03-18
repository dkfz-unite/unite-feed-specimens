# Specimen Data Models

## Specimen
Includes general data about the specimen.

**`Id`*** - Specimen pseudonymized identifier.
- Note: Specimen identifiers are namespaced and should be unique for it's donor across all specimens of the same type.
- Type: _String_
- Limitations: Maximum length 255
- Example: `"CL1TI1"`

**`DonorId`*** - Specimen donor identifier.
- Type: _String_
- Limitations: Maximum length 255
- Example: `"DO1"`

**`ParentId`** - Parent specimen identifier.
- Type: _String_
- Limitations: Maximum length 255
- Example: `"TI1"`

**`ParentType`** - Parent specimen type.
- Type: _String_
- Possible values: `"Tissue"`, `"CellLine"`, `"Organoid"`, `"Xenograft"`
- Example: `"Tissue"`

**`CreationDate`** - Date when specimen was created.
- Note: It's hidden and protected. Relative date is shown instead, if calculation was possible.
- Type: _String_
- Format: YYYY-MM-DDTHH:MM:SS
- Limitations: Only either 'CreationDateDate' or 'CreationDay' can be set at once, not both
- Example: `"2020-02-05T00:00:00"`

**`CreationDay`** - Relative number of days since diagnosis statement when specimen was created.
- Type: _Number_
- Limitations: Integer, greater or equal to 0, only either 'CreationDateDate' or 'CreationDateDay' can be set at once, not both
- Example: `36`

**`Tissue`** - Tissue data (if specimen is a tissue).
- Type: _Object([Tissue](https://github.com/dkfz-unite/unite-specimens-feed/blob/main/Docs/api-specimens-models-tissue.md))_
- Limitations - Only either 'Tissue' or 'CellLine' or 'Organoid' or 'Xenograft' can be set at once.
- Example: `{...}`

**`CellLine`** - Cell line data (if specimen is a cell line).
- Type: _Object([CellLine](https://github.com/dkfz-unite/unite-specimens-feed/blob/main/Docs/api-specimens-models-cellline.md))_
- Limitations - Only either 'Tissue' or 'CellLine' or 'Organoid' or 'Xenograft' can be set at once.
- Example: `{...}`

**`Organoid`** - Organoid data (if specimen is an organoid).
- Type: _Object([Organoid](https://github.com/dkfz-unite/unite-specimens-feed/blob/main/Docs/api-specimens-models-organoid.md))_
- Limitations - Only either 'Tissue' or 'CellLine' or 'Organoid' or 'Xenograft' can be set at once.
- Example: `{...}`

**`Xenograft`** - Xenograft data (if specimen is a xenograft).
- Type: _Object([Xenograft](https://github.com/dkfz-unite/unite-specimens-feed/blob/main/Docs/api-specimens-models-xenograft.md))_
- Limitations - Only either 'Tissue' or 'CellLine' or 'Organoid' or 'Xenograft' can be set at once.
- Example: `{...}`

**`MolecularData`** - Specimen molecular data.
- Type: _Object([MolecularData](https://github.com/dkfz-unite/unite-specimens-feed/blob/main/Docs/api-specimens-models-molecular.md))_
- Example: `{...}`

##
**`*`** - Required fields
