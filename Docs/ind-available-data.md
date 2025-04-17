# Available Data Index

Describes all the data connected to a specimen.

All fields are booleand and indicate whether the data is available or not.

- **`Clinical`** - Specimen donor clinical data
- **`Treatments`** - Specimen donor treatments data
- **`Molecular`** - Molecular data
- **`Interventions`** - Interventions data (not available for donor Materials)
- **`Drugs`** - Drugs screening data (not available for donor Materials)
- **`Mrs`** - MR images data (available for donor tumor materials)
- **`Sms`** - Simple mutations data (SM)
- **`Cnvs`** - Copy number variants data (CNV)
- **`Svs`** - Structural variants data (SV)
- **`GeneExp`** - Bulk gene expression data (RNA-Seq)
- **`GeneExpSc`** - Single cell gene expression data (scRNA-Seq) (Currently not implemented)

These values are satatic and calculated once during specimen index creation.
They do not change upon filtering data during search process.
