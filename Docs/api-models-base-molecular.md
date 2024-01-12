# Molecular Data Model
Includes molecular data.

**`mgmt_status`** - MGMT status of the specimen.
- Type: _String_
- Possible values: `"Methylated"`, `"Unmethylated"`
- Example: `"Methylated"`

**`idh_status`** - IDH status of the specimen.
- Type: _String_
- Possible values: `"Wild Type"`, `"Mutant"`
- Example: `"Wild Type"`

**`idh_mutation`** - IDH mutation of the specimen.
- Type: _String_
- Possible values: `"IDH2 R172S"`, `"IDH2 R172M"`, `"IDH2 R172T"`, `"IDH2 R172W"`, `"IDH2 R172G"`, `"IDH1 R132G"`, `"IDH2 R172K"`, `"IDH1 R132C"`, `"IDH1 R132H"`, `"IDH1 R132L"`, `"IDH1 R132S"`
- Limitations: Can be set only if `idh_status` is `"Mutant"`
- Example: `null`

**`gene_expression_subtype`** - Gene expression subtype of the specimen.
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
- Example: `"True"`

##
At least one filed has to be set
