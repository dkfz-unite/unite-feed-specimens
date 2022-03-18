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

##
**`*`** - Required fields
