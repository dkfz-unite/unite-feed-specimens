# Available Data Index

Describes all the data connected to a specimen.

All fields are booleand and indicate whether the data is available or not.

- **`Clinical`** - Specimen donor clinical data
- **`Treatments`** - Specimen donor treatments data
- **`Molecular`** - Molecular data
- **`Drugs`** - Drugs screening data (not available for Tissues)
- **`Interventions`** - Interventions data (available for Orgaoids and Xenografts)
- **`Mris`** - MRI images data (available for tumor Tissues)
- **`Cts`** - CT images data (available for tumor Tissues and Xenografts) (Currently not implementd)
- **`Ssms`** - Simple somatic mutations data (SSM)
- **`Cnvs`** - Copy number variants data (CNV)
- **`Svs`** - Structural variants data (SV)
- **`GeneExp`** - Bulk gene expression data (RNA-Seq)
- **`GeneExpSc`** - Single cell gene expression data (scRNA-Seq) (Currently not implemented)

These values are satatic and calculated once during specimen index creation.
They do not change upon filtering data during search process.