# Line Data Model
Includes cell line and all basic [specimen](./api-models-specimen.md) information.

**`cells_species`** - Species of the cells in the line.
- Type: _String_
- Possible values: `"Human"`, `"Mouse"`
- Example: `"Human"`

**`cells_type`** - Type of the cells in the line.
- Type: _String_
- Possible values: `"Stem Cell"`, `"Differentiated"`
- Example: `"Stem Cell"`

**`cells_culture_type`** - Way of cells harvesting.
- Type: _String_
- Possible values: `"Suspension"`, `"Adherent"`, `"Both"`
- Example: `"Both"`

**`info`** - public information about the cell line.
- Type: _Object([CellLineInfo](api-models-base-line.md#line-info))_
- Example: `{...}`

## Line Info
Includes public information about the cell line.

_At least one field has to be set_

**`name`** - Name given to the cell line at publication.
- Type: _String_
- Example: `"D1M2L1"`

**`depositor_name`** - Author of the study.
- Type: _String_
- Example: `"Depositor Golden"`

**`depositor_establishment`** - Establishment where the study was held.
- Type: _String_
- Example: `"Line Deposition Centre"`

**`establishment_date`** - Date of the cell line establishment.
- Type: _String_
- Format: "YYYY-MM-DD"
- Example: `"2020-02-05"`

**`pubmed_link`** - [PubMed](https://pubmed.ncbi.nlm.nih.gov/) publication link.
- Type: _String_
- Example: `"https://pubmed.ncbi.nlm.nih.gov"`

**`atcc_link`** - [ATCC](https://www.lgcstandards-atcc.org/) publication link.
- Type: _String_
- Example: `"https://www.atcc.org/"`

**`expasy_link`** - [ExPasy](https://web.expasy.org/) publication link.
- Type: _String_
- Example: `"https://www.expasy.org/"`
