# Tissue Data Model
Includes tissue data.

**`Type`*** - Tissue type.
- Type: _String_
- Possible values: `"Control"`, `"Tumor"`
- Example: `"Tumor"`

**`TumorType`** - Tissue tumor type (if tissue is tumor tissue).
- Type: _String_
- Possible values: `"Primary"`, `"Metastasis"`, `"Recurrent"`
- Example: `"Primary"`

**`Source`** - Tissue source.
- Type: _String_
- Limitations: Maximum length 100
- Example: `"Solid tissue"`

**`MolecularData`** - Tissue molecular data.
- Type: _Object([MolecularData](https://github.com/dkfz-unite/unite-specimens-feed/blob/main/Docs/api-specimens-models-molecular.md))_
- Limitations: Can be set for tumor tissues only 
- Example: `{...}`

##
**`*`** - Required fields
