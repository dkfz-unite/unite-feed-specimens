# Line Data Model
Includes cell line data and public cell line information.

## Line
Inlcudes cell line data.

_At least one field has to be set_

**`cells_species`*** - Species of the cells in the line.
- Type: _String_
- Possible values: `"Human"`, `"Mouse"`
- Example: `"Human"`

**`cells_type`*** - Type of the cells in the line.
- Type: _String_
- Possible values: `"Stem Cell"`, `"Differentiated"`
- Example: `"Stem Cell"`

**`cells_culture_type`*** - Way of cells harvesting.
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
- Example: `"CLP_CL1TI1"`

**`depositor_name`** - Author of the study.
- Type: _String_
- Example: `"Erica Polden"`

**`depositor_establishment`** - Establishment where the study was held.
- Type: _String_
- Example: `"Colonord Research Centre"`

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
