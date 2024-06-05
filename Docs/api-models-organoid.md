# Organoid Data Model
Includes organoid and all basic [specimen](./api-models-specimen.md) information.

**`medium`** - Organoid medium.
- Type: _String_
- Example: `"Medium1"`

**`implanted_cells_number`** - Number of implanted cells.
- Type: _Number_
- Limitations: Integer, greater than 0
- Example: `1500000`

**`tumorigenicity`** - The ability to give rise to either benign or malignant progressively growing tumors.
- Type: _Boolean_
- Example: `true`
