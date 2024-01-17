# Material Data Model
Includes materials data.

_At least one field has to be set_

**`type`*** - Material type.
- Type: _String_
- Possible values: `"Normal"`, `"Tumor"`
- Example: `"Tumor"`

**`tumor_type`** - Material tumor type (if material is tumor cells).
- Type: _String_
- Possible values: `"Primary"`, `"Metastasis"`, `"Recurrent"`
- Example: `"Primary"`

**`source`** - Material source (e.g. solid tissue, plasma, brain liquid etc.).
- Type: _String_
- Limitations: Maximum length 100
- Example: `"Solid tissue"`
