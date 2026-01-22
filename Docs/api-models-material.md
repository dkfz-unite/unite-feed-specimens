# Material Data Model
Includes material and all basic [specimen](./api-models-specimen.md) information.

_At least one field has to be set_

**`fixation_type`** - Material preservation type.
- Type: _String_
- Possible values: `"FFPE"`, `"Fresh Frozen"`
- Example: `"FFPE"`

**`source`** - Material source (e.g. solid tissue, plasma, brain liquid etc.).
- Type: _String_
- Limitations: Maximum length 100
- Example: `"Tissue"`
