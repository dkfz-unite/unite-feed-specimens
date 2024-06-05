# Material Data Model
Includes material and all basic [specimen](./api-models-specimen.md) information.

**`type`** - Material type.
- Type: _String_
- Possible values: `"Normal"`, `"Tumor"`
- Example: `"Tumor"`

**`tumor_type`** - Material tumor type.
- Type: _String_
- Possible values: `"Primary"`, `"Metastasis"`, `"Recurrent"`
- Limitations: Can be set only if `type` is `"Tumor"`
- Example: `"Primary"`

**`source`** - Material source (e.g. solid tissue, plasma, brain liquid etc.).
- Type: _String_
- Limitations: Maximum length 100
- Example: `"Tissue"`
