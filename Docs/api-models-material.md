# Material Data Model
Includes material and all basic [specimen](./api-models-specimen.md) information.

**`type`** - Material type.
- Type: _String_
- Possible values: `"Normal"`, `"Tumor"`
- Example: `"Tumor"`

**`fixation_type`** - Material preservation type.
- Type: _String_
- Possible values: `"FFPE"`, `"Fresh_Frozen"`
- Example: `"FFPE"`

**`tumor_type`** - Material tumor type.
- Type: _String_
- Possible values: `"Primary"`, `"Metastasis"`, `"Recurrent"`
- Limitations: Can be set only if `type` is `"Tumor"`
- Example: `"Primary"`

**`tumor_grade`** - Material tumor grade.
- Type: _Number_
- Limitations: Integer, greater than 0
- Example: `3`

**`source`** - Material source (e.g. solid tissue, plasma, brain liquid etc.).
- Type: _String_
- Limitations: Maximum length 100
- Example: `"Tissue"`
