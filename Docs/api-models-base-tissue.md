# Tissue Data Model
Includes tissue data.

**`type`*** - Tissue type.
- Type: _String_
- Possible values: `"Control"`, `"Tumor"`
- Example: `"Tumor"`

**`tumor_type`** - Tissue tumor type (if tissue is tumor tissue).
- Type: _String_
- Possible values: `"Primary"`, `"Metastasis"`, `"Recurrent"`
- Example: `"Primary"`

**`source`** - Tissue source.
- Type: _String_
- Limitations: Maximum length 100
- Example: `"Solid tissue"`

##
**`*`** - Required fields
