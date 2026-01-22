# Molecular Data Model
Includes molecular data.

_At least one field has to be set_

**`mgmt_status`** - Indicates whether specimen is MGMT methylated.
- Type: _Boolean_
- Example: `"true"`

**`idh_status`** - Indicates whether IDH is mutated in the specimen.
- Type: _Boolean_
- Example: `false`

**`idh_mutation`** - IDH mutation of the specimen.
- Type: _String_
- Possible values: `"IDH2 R172S"`, `"IDH2 R172M"`, `"IDH2 R172T"`, `"IDH2 R172W"`, `"IDH2 R172G"`, `"IDH1 R132G"`, `"IDH2 R172K"`, `"IDH1 R132C"`, `"IDH1 R132H"`, `"IDH1 R132L"`, `"IDH1 R132S"`
- Limitations: Can be set only if `idh_status` is `true`
- Example: `null`

**`tert_status`** - Indicates whether TERT is mutated in the specimen.
- Type: _Boolean_
- Example: `false`

**`tert_mutation`** - TERT mutation of the specimen.
- Type: _String_
- Possible values: `"C228T"`, `"C250T"`
- Limitations: Can be set only if `tert_status` is `true`
- Example: `null`

**`expression_subtype`** - Gene expression subtype of the specimen.
- Type: _String_
- Possible values: `"Mesenchymal"`, `"Proneural"`, `"Classical"`
- Limitations: Can be set only if `idh_status` is `"Wild Type"`
- Example: `"Mesenchymal"`

**`methylation_subtype`** - Methylation subtype of the specimen.
- Type: _String_
- Possible values: `"RTKI"`, `"RTKII"`, `"Mesenchymal"`, `"H3-K27"`, `"H3-G34"`
- Limitations: Can be set only if `IdhStatus` is `"Wild Type"`
- Example: `"RTKI"`

**`gcimp_methylation`** - Indicates whether specimen has G-CIMP methylation.
- Type: _Boolean_
- Example: `"true"`

**`gene_knockouts`** - List of genes knocked out in the specimen.
- Type: _Array_
- Elemet type: _String_
- Element limitations: Maximum length 100
- Limitations: Should contain at least one element
- Example: `["EGFR", "PTEN"]`