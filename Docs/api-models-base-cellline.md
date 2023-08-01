# Cell Line Data Models
Includes cell line data and public cell line information.

## Cell Line
Inlcudes cell line data.

_At least one field has to be set_

**`Species`** - Whom the cell line was initially taken from.
- Type: _String_
- Possible values: `"Human"`, `"Mouse"`
- Example: `"Human"`

**`Type`** - Type of the cell line.
- Type: _String_
- Possible values: `"Stem Cell"`, `"Differentiated"`
- Example: `"Stem Cell"`

**`CultureType`** - Way of cells harvesting.
- Type: _String_
- Possible values: `"Suspension"`, `"Adherent"`, `"Both"`
- Example: `"Both"`

**`Info`** - public information about the cell line.
- Type: _Object([CellLineInfo](api-specimens-models-cellline.md#cell-line-info))_
- Example: `{...}`

## Cell Line Info
Includes public information about the cell line.

_At least one field has to be set_

**`Name`** - Name given to the cell line at publication.
- Type: _String_
- Example: `"CLP_CL1TI1"`

**`DepositorName`** - Author of the study.
- Type: _String_
- Example: `"Erica Polden"`

**`DepositorEstablishment`** - Establishment where the study was held.
- Type: _String_
- Example: `"Colonord Research Centre"`

**`EstablishmentDate`** - Date of the cell line establishment.
- Type: _String_
- Format: "YYYY-MM-DDTHH:MM:SS"
- Example: `"2020-02-05T00:00:00"`

**`PubMedLink`** - [PubMed](https://pubmed.ncbi.nlm.nih.gov/) publication link.
- Type: _String_
- Example: `"https://pubmed.ncbi.nlm.nih.gov"`

**`AtccLink`** - [ATCC](https://www.lgcstandards-atcc.org/) publication link.
- Type: _String_
- Example: `"https://www.atcc.org/"`

**`ExPasyLink`** - [ExPasy](https://web.expasy.org/) publication link.
- Type: _String_
- Example: `"https://www.expasy.org/"`
