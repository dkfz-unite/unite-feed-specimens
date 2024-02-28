# Specimens Data Feed API
Allows to submit specimens data to the repository.

> [!Note]
> API is accessible for authorized users only and requires `JWT` token as `Authorization` header (read more about [Identity Service](https://github.com/dkfz-unite/unite-identity)).

API is **proxied** to main API and can be accessed at [[host]/api/specimens-feed](http://localhost/api/specimens-feed) (**without** `api` prefix).

All data submision request implement **UPSERT** logic:
- Missing data will be populated
- Existing data will be updated
- Redundand data will be removed


## Overview
- get:[api](#get-api) - health check.
- post:[api/specimens](#post-apispecimens) - submit all specimens data.
- post:[api/meterials/{type?}](#post-apimaterialstype) - submit donor materials data in given type.
- post:[api/organoids/{type?}](#post-apiorganoidstype) - submit organoids data in given type.
- post:[api/xenografts/{type?}](#post-apixenograftstype) - submit xenografts data in given type.
- post:[api/interventions/{type?}](#post-apiinterventionstype) - submit interventions data in given type.
- post:[api/drugs/{type?}](#post-apidrugs) - submit drugs screening data.

> [!Note]
> **Json** is default data type for all requests and will be used if no data type is specified.


## GET: [api](http://localhost:5104/api)
Health check.

### Responses
`"2022-03-17T09:45:10.9359202Z"` - Current UTC date and time in JSON format, if service is up and running


## POST: [api/specimens](http://localhost:5104/api/specimens)
Submit specimens data (tissue, cell line, organoid or xenograft).

### Body
Supported formats are:
- `json` (**empty**) - application/json

##### json - application/json
```json
[
    {
        "id": "Material1",
        "donor_id": "Donor1",
        "parent_id": null,
        "parent_type": null,
        "creation_date": "2020-01-15",
        "creation_day": null,
        "material": {
            "type": "Normal",
            "tumor_type": null,
            "source": "Blood"
        }
    },
    {
        "id": "Material2",
        "donor_id": "Donor1",
        "parent_id": null,
        "parent_type": null,
        "creation_date": "2020-01-15",
        "creation_day": null,
        "material": {
            "type": "Tumor",
            "tumor_type": "Primary",
            "source": "Tissue"
        },
        "molecular_data": {
            "mgmt_status": "Methylated",
            "idh_status": "Wild Type",
            "idh_mutation": null,
            "gene_expression_subtype": "Classical",
            "methylation_subtype": "H3-K27",
            "gcimp_methylation": true
        }
    },
    {
        "id": "Material1",
        "donor_id": "Donor2",
        "parent_id": null,
        "parent_type": null,
        "creation_date": "2020-01-15",
        "creation_day": null,
        "material": {
            "type": "Normal",
            "tumor_type": null,
            "source": "Blood"
        }
    },
    {
        "id": "Material2",
        "donor_id": "Donor2",
        "parent_id": null,
        "parent_type": null,
        "creation_date": "2020-01-15",
        "creation_day": null,
        "material": {
            "type": "Tumor",
            "tumor_type": "Primary",
            "source": "Tissue"
        },
        "molecular_data": {
            "mgmt_status": "Unmethylated",
            "idh_status": "Mutant",
            "idh_mutation": "IDH1 R132H",
            "gene_expression_subtype": null,
            "methylation_subtype": null,
            "gcimp_methylation": false
        }
    },
    {
        "id": "Material3",
        "donor_id": "Donor2",
        "parent_id": null,
        "parent_type": null,
        "creation_date": "2020-03-01",
        "creation_day": null,
        "material": {
            "type": "Tumor",
            "tumor_type": "Recurrent",
            "source": "CSF"
        },
        "molecular_data": {
            "mgmt_status": "Unmethylated",
            "idh_status": "Mutant",
            "idh_mutation": "IDH1 R132H",
            "gene_expression_subtype": null,
            "methylation_subtype": null,
            "gcimp_methylation": false
        }
    },
    {
        "id": "Line1",
        "donor_id": "Donor1",
        "parent_id": "Material2",
        "parent_type": "Material",
        "creation_date": "2020-02-01",
        "creation_day": null,
        "line": {
            "cells_species": "Human",
            "cells_type": "Stem Cell",
            "cells_culture_type": "Suspension",
            "info": {
                "name": "D1M2L1",
                "depositor_name": "Depositor Golden",
                "depositor_establishment": "Line Deposition Centre",
                "establishment_date": "2020-02-01",
                "pubmed_link": "https://pubmed.ncbi.nlm.nih.gov",
                "atcc_link": "https://www.atcc.org",
                "expasy_link": "https://www.expasy.org"
            }
        },
        "molecular_data": {
            "mgmt_status": "Methylated",
            "idh_status": "Wild Type",
            "idh_mutation": null,
            "gene_expression_subtype": "Classical",
            "methylation_subtype": "H3-K27",
            "gcimp_methylation": true
        }
    },
    {
        "id": "Line1",
        "donor_id": "Donor2",
        "parent_id": "Material2",
        "parent_type": "Material",
        "creation_date": "2020-02-01",
        "creation_day": null,
        "line": {
            "cells_species": "Human",
            "cells_type": "Differentiated",
            "cells_culture_type": "Adherent",
            "info": {
                "name": "D2M2L1",
                "depositor_name": "Depositor Golden",
                "depositor_establishment": "Line Deposition Centre",
                "establishment_date": "2020-02-01",
                "pubmed_link": "https://pubmed.ncbi.nlm.nih.gov",
                "atcc_link": "https://www.atcc.org",
                "expasy_link": "https://www.expasy.org"
            }
        },
        "molecular_data": {
            "mgmt_status": "Unmethylated",
            "idh_status": "Mutant",
            "idh_mutation": "IDH1 R132H",
            "gene_expression_subtype": null,
            "methylation_subtype": null,
            "gcimp_methylation": false
        }
    },
    {
        "id": "Organoid1",
        "donor_id": "Donor1",
        "parent_id": "Line1",
        "parent_type": "Line",
        "creation_date": "2020-02-05",
        "creation_day": null,
        "organoid": {
            "medium": "Medium1",
            "implanted_cells_number": 10500,
            "tumorigenicity": true
        },
        "molecular_data": {
            "mgmt_status": "Methylated",
            "idh_status": "Wild Type",
            "idh_mutation": null,
            "gene_expression_subtype": "Classical",
            "methylation_subtype": "H3-K27",
            "gcimp_methylation": true
        }
    },
    {
        "id": "Organoid1",
        "donor_id": "Donor2",
        "parent_id": "Line1",
        "parent_type": "Line",
        "creation_date": "2020-02-05",
        "creation_day": null,
        "organoid": {
            "medium": "Medium2",
            "implanted_cells_number": 45000,
            "tumorigenicity": true
        },
        "molecular_data": {
            "mgmt_status": "Unmethylated",
            "idh_status": "Mutant",
            "idh_mutation": "IDH1 R132H",
            "gene_expression_subtype": null,
            "methylation_subtype": null,
            "gcimp_methylation": false
        }
    },
    {
        "id": "Xenograft1",
        "donor_id": "Donor1",
        "parent_id": "Line1",
        "parent_type": "Line",
        "creation_date": "2020-02-05",
        "creation_day": null,
        "xenograft": {
            "mouse_strain": "Nude",
            "group_size": 20,
            "implant_type": "Orhtotopical",
            "implant_location": "Striatal",
            "implanted_cells_number": 750000,
            "tumorigenicity": true,
            "tumor_growth_form": "Invasive",
            "survival_days": "20-30"
        },
        "molecular_data": {
            "mgmt_status": "Methylated",
            "idh_status": "Wild Type",
            "idh_mutation": null,
            "gene_expression_subtype": "Classical",
            "methylation_subtype": "H3-K27",
            "gcimp_methylation": true
        }
    },
    {
        "id": "Xenograft1",
        "donor_id": "Donor2",
        "parent_id": "Line1",
        "parent_type": "Line",
        "creation_date": "2020-02-05",
        "creation_day": null,
        "xenograft": {
            "mouse_strain": "Nude",
            "group_size": 30,
            "implant_type": "Orhtotopical",
            "implant_location": "Cortical",
            "implanted_cells_number": 550000,
            "tumorigenicity": true,
            "tumor_growth_form": "Invasive",
            "survival_days": "15-20"
        },
        "molecular_data": {
            "mgmt_status": "Unmethylated",
            "idh_status": "Mutant",
            "idh_mutation": "IDH1 R132H",
            "gene_expression_subtype": null,
            "methylation_subtype": null,
            "gcimp_methylation": false
        }
    }
]
```

Fields description can be found [here](api-models-specimens.md).

### Responses
- `200` - request was processed successfully
- `400` - request data didn't pass validation
- `401` - missing JWT token
- `403` - missing required permissions


## POST: [api/materials/{type?}](http://localhost:5104/api/materials)
Submit donor materials data.

### Body
Supported formats are:
- `tsv` - text/tab-separated-values

For `json` upload see [POST: api/specimens](#post-apispecimens).

##### tsv - text/tab-separated-values
```tsv
id	donor_id	parent_id	parent_type	creation_date	creation_day	type	tumor_type	source	mgmt_status	idh_status	idh_mutation	gene_expression_subtype	methylation_subtype	gcimp_methylation
Material1	Donor1			2020-01-15		Normal		Blood						
Material2	Donor1			2020-01-15		Tumor	Primary	Tissue	Methylated	Wild Type		Classical	H3-K27	true
Material1	Donor2			2020-01-15		Normal		Blood						
Material2	Donor2			2020-01-15		Tumor	Primary	Tissue	Unmethylated	Mutant	IDH1 R132H			false
Material3	Donor2			2020-03-01		Tumor	Recurrent	Tissue	Unmethylated	Mutant	IDH1 R132H			false
```

Fields description can be found [here](api-models-base-material.md).

### Responses
- `200` - request was processed successfully
- `400` - request data didn't pass validation
- `401` - missing JWT token
- `403` - missing required permissions


## POST: [api/lines/{type?}](http://localhost:5104/api/lines)
Submit donor lines data.

### Body
Supported formats are:
- `tsv` - text/tab-separated-values

For `json` upload see [POST: api/specimens](#post-apispecimens).

##### tsv - text/tab-separated-values
```tsv
id	donor_id	parent_id	parent_type	creation_date	creation_day	cells_species	cells_type	cells_culture_type	name	depositor_name	depositor_establishment	establishment_date	pubmed_link	atcc_link	expasy_link	mgmt_status	idh_status	idh_mutation	gene_expression_subtype	methylation_subtype	gcimp_methylation
Line1	Donor1	Material2	Material	2020-02-01		Human	Stem Cell	Suspension	D1M2L1	Depositor Golden	Line Depositor Centre	2020-02-01	https://pubmed.ncbi.nlm.nih.gov	https://www.atcc.org	https://www.expasy.org	Methylated	Wild Type		Classical	H3-K27	true
Line1	Donor2	Material2	Material	2020-02-01		Human	Differentiated	Adherent	D2M2L1	Depositor Golden	Line Depositor Centre	2020-02-01	https://pubmed.ncbi.nlm.nih.gov	https://www.atcc.org	https://www.expasy.org	Unmethylated	Mutant	IDH1 R132H			false
```

Fields description can be found [here](api-models-base-material.md).

### Responses
- `200` - request was processed successfully
- `400` - request data didn't pass validation
- `401` - missing JWT token
- `403` - missing required permissions


## POST: [api/organoids/{type?}](http://localhost:5104/api/organoids)
Submit organoids data.

### Body - TSV
Supported formats are:
- `tsv` - text/tab-separated-values

For `json` upload see [POST: api/specimens](#post-apispecimens).

#### tsv - text/tab-separated-values
```tsv
id	donor_id	parent_id	parent_type	creation_date	creation_day	medium	implanted_cells_number	tumorigenicity	mgmt_status	idh_status	idh_mutation	gene_expression_subtype	methylation_subtype	gcimp_methylation
Organoid1	Donor1	Line1	Line	2020-02-05		Medium1	10500	true	Methylated	Wild Type		Classical	H3-K27	true
Organoid1	Donor2	Line1	Line	2020-02-05		Medium2	45000	true	Unmethylated	Mutant	IDH1 R132H			false
```

Fields description can be found [here](api-models-base-organoid.md).

### Responses
- `200` - request was processed successfully
- `400` - request data didn't pass validation
- `401` - missing JWT token
- `403` - missing required permissions


## POST: [api/xenografts/{type?}](http://localhost:5104/api/xenografts)
Submit xenografts data.

### Body
Supported formats are:
- `tsv` - text/tab-separated-values

For `json` upload see [POST: api/specimens](#post-apispecimens).

### tsv - text/tab-separated-values
```tsv
id	donor_id	parent_id	parent_type	creation_date	creation_day	mouse_strain	group_size	implant_type	implant_location	implanted_cells_number	tumorigenicity	tumor_growth_form	survival_days	mgmt_status	idh_status	idh_mutation	gene_expression_subtype	methylation_subtype	gcimp_methylation
Xenograft1	Donor1	Line1	Line	2020-02-05		Nude	20	Orhtotopical	Striatal	750000	true	Invasive	20-30	Methylated	Wild Type		Classical	H3-K27	true
Xenograft1	Donor2	Line1	Line	2020-02-05		Nude	30	Orhtotopical	Cortical	550000	true	Invasive	15-20	Unmethylated	Mutant	IDH1 R132H			false
```

Fields description can be found [here](./api-models-base-xenograft.md).

### Responses
- `200` - request was processed successfully
- `400` - request data didn't pass validation
- `401` - missing JWT token
- `403` - missing required permissions


## POST: [api/interventions/{type?}](http://localhost:5104/api/interventions)
Submit interventions data. Donors and specimens should be present in the system.

### Body
Supported formats are:
- `json` (**empty**) - application/json
- `tsv` - text/tab-separated-values

#### json(**empty**) - application/json
```json
[
    {
        "donor_id": "Donor1",
        "specimen_id": "Organoid1",
        "specimen_type": "Organoid",
        "data": [
            {
                "type": "Drug1",
                "details": "Specimen specific intervention details",
                "start_date": "2020-02-05",
                "start_day": null,
                "end_date": "2020-02-10",
                "duration_days": null,
                "results": "Specimen specific intervention results"
            },
            {
                "type": "Drug2",
                "details": "Specimen specific intervention details",
                "start_date": "2020-02-10",
                "start_day": null,
                "end_date": "2020-02-15",
                "duration_days": null,
                "results": "Specimen specific intervention results"
            }
        ]
    },
    {
        "donor_id": "Donor2",
        "specimen_id": "Organoid1",
        "specimen_type": "Organoid",
        "data": [
            {
                "type": "Drug1",
                "details": "Specimen specific intervention details",
                "start_date": "2020-02-05",
                "start_day": null,
                "end_date": "2020-02-10",
                "duration_days": null,
                "results": "Specimen specific intervention results"
            },
            {
                "type": "Drug2",
                "details": "Specimen specific intervention details",
                "start_date": "2020-02-10",
                "start_day": null,
                "end_date": "2020-02-15",
                "duration_days": null,
                "results": "Specimen specific intervention results"
            }
        ]
    },
    {
        "donor_id": "Donor1",
        "specimen_id": "Xenograft1",
        "specimen_type": "Xenograft",
        "data": [
            {
                "type": "Drug1",
                "details": "Specimen specific intervention details",
                "start_date": "2020-02-05",
                "start_day": null,
                "end_date": "2020-02-10",
                "duration_days": null,
                "results": "Specimen specific intervention results"
            },
            {
                "type": "Drug2",
                "details": "Specimen specific intervention details",
                "start_date": "2020-02-10",
                "start_day": null,
                "end_date": "2020-02-15",
                "duration_days": null,
                "results": "Specimen specific intervention results"
            }
        ]
    },
    {
        "donor_id": "Donor2",
        "specimen_id": "Xenograft1",
        "specimen_type": "Xenograft",
        "data": [
            {
                "type": "Drug1",
                "details": "Specimen specific intervention details",
                "start_date": "2020-02-05",
                "start_day": null,
                "end_date": "2020-02-10",
                "duration_days": null,
                "results": "Specimen specific intervention results"
            },
            {
                "type": "Drug2",
                "details": "Specimen specific intervention details",
                "start_date": "2020-02-10",
                "start_day": null,
                "end_date": "2020-02-15",
                "duration_days": null,
                "results": "Specimen specific intervention results"
            }
        ]
    }
]
```

#### tsv - text/tab-separated-values
```tsv
donor_id	specimen_id	specimen_type	type	details	start_date	start_day	end_date	duration_days	results
Donor1	Organoid1	Organoid	Drug1	Specimen specific intervention details	2020-02-05		2020-02-05		Specimen specific intervention results
Donor1	Organoid1	Organoid	Drug2	Specimen specific intervention details	2020-02-10		2020-02-15		Specimen specific intervention results
Donor2	Organoid1	Organoid	Drug1	Specimen specific intervention details	2020-02-05		2020-02-05		Specimen specific intervention results
Donor2	Organoid1	Organoid	Drug2	Specimen specific intervention details	2020-02-10		2020-02-15		Specimen specific intervention results
Donor1	Xenograft1	Xenograft	Drug1	Specimen specific intervention details	2020-02-05		2020-02-05		Specimen specific intervention results
Donor1	Xenograft1	Xenograft	Drug2	Specimen specific intervention details	2020-02-10		2020-02-15		Specimen specific intervention results
Donor2	Xenograft1	Xenograft	Drug1	Specimen specific intervention details	2020-02-05		2020-02-05		Specimen specific intervention results
Donor2	Xenograft1	Xenograft	Drug2	Specimen specific intervention details	2020-02-10		2020-02-15		Specimen specific intervention results
```

Fields description can be found [here](./api-models-interventions.md).

### Responses
- `200` - request was processed successfully
- `400` - request data didn't pass validation
- `401` - missing JWT token
- `403` - missing required permissions


## POST: [api/drugs](http://localhost:5104/api/drugs)
Submit drugs screening data. Donors and specimens should be present in the system.

### Body
Supported formats are:
- `json` (**empty**) - application/json
- `tsv` - text/tab-separated-values

#### json - application/json
```json
[
    {
        "donor_id": "Donor1",
        "specimen_id": "Line1",
        "specimen_type": "Line",
        "data": [
            {
                "drug": "Drug1",
                "dss": 35,
                "dss_selective": 15,
                "gof": 0.95,
                "abs_ic_25": 5.25,
                "abs_ic_50": 10.50,
                "abs_ic_75": 50.75,
                "min_concentration": 1,
                "max_concentration": 10000,
                "concentration": [1, 10, 100, 1000, 10000],
                "inhibition": [2.25, 25.50, 85.25, 95.25, 95.50],
                "concentration_line": null,
                "inhibition_line": null
            },
            {
                "drug": "Drug2",
                "dss": 30,
                "dss_selective": 10,
                "gof": 0.75,
                "abs_ic_25": 4.25,
                "abs_ic_50": 8.50,
                "abs_ic_75": 47.75,
                "min_concentration": 1,
                "max_concentration": 10000,
                "concentration": [1, 10, 100, 1000, 10000],
                "inhibition": [2.05, 20.50, 75.25, 90.25, 90.50],
                "concentration_line": null,
                "inhibition_line": null
            },
            {
                "drug": "Drug3",
                "dss": 25,
                "dss_selective": 5,
                "gof": 0.55,
                "abs_ic_25": 2.25,
                "abs_ic_50": 4.50,
                "abs_ic_75": 25.75,
                "min_concentration": 1,
                "max_concentration": 10000,
                "concentration": [1, 10, 100, 1000, 10000],
                "inhibition": [0.05, 10.50, 35.25, 65.25, 65.50],
                "concentration_line": null,
                "inhibition_line": null
            },
            {
                "drug": "Drug4",
                "dss": 15,
                "dss_selective": -5,
                "gof": 0.35,
                "abs_ic_25": 0.25,
                "abs_ic_50": 2.50,
                "abs_ic_75": 15.75,
                "min_concentration": 1,
                "max_concentration": 10000,
                "concentration": [1, 10, 100, 1000, 10000],
                "inhibition": [0.00, 5.50, 17.25, 55.25, 55.50],
                "concentration_line": null,
                "inhibition_line": null
            },
            {
                "drug": "Drug5",
                "dss": 10,
                "dss_selective": -10,
                "gof": 0.25,
                "abs_ic_25": 0.05,
                "abs_ic_50": 0.50,
                "abs_ic_75": 7.75,
                "min_concentration": 1,
                "max_concentration": 10000,
                "concentration": [1, 10, 100, 1000, 10000],
                "inhibition": [0.00, 0.50, 5.25, 45.25, 45.50],
                "concentration_line": null,
                "inhibition_line": null
            }
        ]
    },
    {
        "donor_id": "Donor2",
        "specimen_id": "Line1",
        "specimen_type": "Line",
        "data": [
            {
                "drug": "Drug1",
                "dss": 34,
                "dss_selective": 14,
                "gof": 0.90,
                "abs_ic_25": 5.20,
                "abs_ic_50": 10.45,
                "abs_ic_75": 50.70,
                "min_concentration": 1,
                "max_concentration": 10000,
                "concentration": [1, 10, 100, 1000, 10000],
                "inhibition": [2.20, 25.45, 85.20, 95.20, 95.45],
                "concentration_line": null,
                "inhibition_line": null
            },
            {
                "drug": "Drug2",
                "dss": 29,
                "dss_selective": 9,
                "gof": 0.70,
                "abs_ic_25": 4.20,
                "abs_ic_50": 8.45,
                "abs_ic_75": 47.70,
                "min_concentration": 1,
                "max_concentration": 10000,
                "concentration": [1, 10, 100, 1000, 10000],
                "inhibition": [2.00, 20.45, 75.20, 90.20, 90.45],
                "concentration_line": null,
                "inhibition_line": null
            },
            {
                "drug": "Drug3",
                "dss": 24,
                "dss_selective": 4,
                "gof": 0.50,
                "abs_ic_25": 2.20,
                "abs_ic_50": 4.45,
                "abs_ic_75": 25.70,
                "min_concentration": 1,
                "max_concentration": 10000,
                "concentration": [1, 10, 100, 1000, 10000],
                "inhibition": [0.00, 10.45, 35.20, 65.20, 65.45],
                "concentration_line": null,
                "inhibition_line": null
            },
            {
                "drug": "Drug4",
                "dss": 14,
                "dss_selective": -6,
                "gof": 0.30,
                "abs_ic_25": 0.20,
                "abs_ic_50": 2.45,
                "abs_ic_75": 15.70,
                "min_concentration": 1,
                "max_concentration": 10000,
                "concentration": [1, 10, 100, 1000, 10000],
                "inhibition": [0.00, 5.45, 17.20, 55.20, 55.45],
                "concentration_line": null,
                "inhibition_line": null
            },
            {
                "drug": "Drug5",
                "dss": 9,
                "dss_selective": -11,
                "gof": 0.20,
                "abs_ic_25": 0.00,
                "abs_ic_50": 0.45,
                "abs_ic_75": 7.70,
                "min_concentration": 1,
                "max_concentration": 10000,
                "concentration": [1, 10, 100, 1000, 10000],
                "inhibition": [0.00, 0.45, 5.20, 45.20, 45.45],
                "concentration_line": null,
                "inhibition_line": null
            }
        ]
    }
]
```

#### tsv - text/tab-separated-values
```tsv
donor_id	specimen_id	specimen_type	drug	dss	dss_selective	gof	abs_ic_25	abs_ic_50	abs_ic_75	min_concentration	max_concentration	concentration	inhibition
Donor1	Line1	Line	Drug1	35	15	0.95	5.25	10.50	50.75	1	10000	1,10,100,1000,10000	2.25,25.50,85.25,95.25,95.50
Donor1	Line1	Line	Drug2	30	10	0.75	4.25	8.50	47.75	1	10000	1,10,100,1000,10000	2.05,20.50,75.25,90.25,90.50
Donor1	Line1	Line	Drug3	25	5	0.55	2.25	4.5	25.75	1	10000	1,10,100,1000,10000	0.05,10.5,35.25,65.25,65.5
Donor1	Line1	Line	Drug4	15	-5	0.35	0.25	2.5	15.75	1	10000	1,10,100,1000,10000	0,5.5,17.25,55.25,55.5
Donor1	Line1	Line	Drug5	10	-10	0.25	0.05	0.5	7.75	1	10000	1,10,100,1000,10000	0,0.5,5.25,45.25,45.5
Donor2	Line1	Line	Drug1	34	14	0.9	5.2	10.45	50.7	1	10000	1,10,100,1000,10000	2.2,25.45,85.2,95.2,95.45
Donor2	Line1	Line	Drug2	29	9	0.7	4.2	8.45	47.7	1	10000	1,10,100,1000,10000	2,20.45,75.2,90.2,90.45
Donor2	Line1	Line	Drug3	24	4	0.5	2.2	4.45	25.7	1	10000	1,10,100,1000,10000	0,10.45,35.2,65.2,65.45
Donor2	Line1	Line	Drug4	14	-6	0.3	0.2	2.45	15.7	1	10000	1,10,100,1000,10000	0,5.45,17.2,55.2,55.45
Donor2	Line1	Line	Drug5	9	-11	0.2	0	0.45	7.7	1	10000	1,10,100,1000,10000	0,0.45,5.2,45.2,45.45
```

Fields description can be found [here](api-models-drugs.md).

### Responses
- `200` - request was processed successfully
- `400` - request data didn't pass validation
- `401` - missing JWT token
- `403` - missing required permissions
