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
        "id": "TI1",
        "donor_id": "DO1",
        "parent_id": null,
        "parent_type": null,
        "creation_date": "2020-02-01",
        "creation_day": null,
        "material": {
            "type": "Tumor",
            "tumor_type": "Primary",
            "source": "Solid tissue"
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
        "id": "OR1CL1TI1",
        "donor_id": "DO1",
        "parent_id": "CL1TI1",
        "parent_type": "CellLine",
        "creation_date": "2020-04-01",
        "creation_day": null,
        "organoid": {
            "medium": "Medium 1",
            "implanted_cells_number": 1500000,
            "tumorigenicity": false
        },
        "molecular_data": {
            "mgmt_status": "Methylated",
            "idh_status": "Mutant",
            "idh_mutation": "IDH1 R132C",
            "gene_expression_subtype": null,
            "methylation_subtype": null,
            "gcimp_methylation": true
        },
        "interventions": null,
        "drug_screenings": null
    },
    {
        "id": "XE1CL1TI1",
        "donor_id": "DO1",
        "parent_id": "CL1TI1",
        "parent_type": "CellLine",
        "creation_date": "2020-03-01",
        "creation_day": null,
        "xenograft": {
            "mouse_strain": "Nude",
            "group_size": 9,
            "implant_type": "Other",
            "implant_location": "Cortical",
            "implanted_cells_number": 750000,
            "tumorigenicity": true,
            "tumor_growth_form": "Invasive",
            "survival_days": "20-30",
        },
        "molecular_data": {
            "mgmt_status": "Methylated",
            "idh_status": "Wild Type",
            "idh_mutation": null,
            "gene_expression_subtype": "Classical",
            "methylation_subtype": "H3-K27",
            "gcimp_methylation": true
        },
        "interventions": null,
        "drug_screenings": null
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
TI1	DO1			2020-02-01		Tumor	Primary	Solid tissue	Methylated	Wild Type		Classical	H3-K27	true
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
donor_id    specimen_id medium  implanted_cells_number  tumorigenicity  mgmt_status	idh_status	idh_mutation	gene_expression_subtype	methylation_subtype	gcimp_methylation
DO1	OR1CL1TI1	Medium 1	1500000	false	Methylated	Wild Type		Classical	H3-K27	true
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
donor_id    specimen_id mouse_strain    group_size  implant_type    implant_location implanted_cells_number  tumorigenicity  tumor_growth_form   survival_days   mgmt_status	idh_status	idh_mutation	gene_expression_subtype	methylation_subtype	gcimp_methylation
DO1	XE1CL1TI1	Nude	9	Other	Cortical	750000	true	Invasive	20-30	Methylated	Wild Type		Classical	H3-K27	true
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
        "donor_id": "DO1",
        "specimen_id": "XE1CL1TI1",
        "specimen_type": "Organoid",
        "data": [
            {
                "type": "Intervention type",
                "details": "Intervention details",
                "start_date": "2020-03-05",
                "start_day": null,
                "end_date": "2020-03-10",
                "duration_days": null,
                "results": "Intervention results"
            }
        ]
    }
]
```

#### tsv - text/tab-separated-values
```tsv
donor_id    specimen_id specimen_type type    details start_date  start_day   end_date    duration_days   results
DO1	XE1CL1TI1   Organoid	Intervention_Type   Intervention_details    2020-03-05	2020-03-10	Intervention_results
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
        "donor_id": "DO1",
        "specimen_id": "CL1TI1",
        "specimen_type": "Line",
        "data": [
            {
                "drug": "A-1155463",
                "dss": 33.56,
                "dss_selective": 28.03,
                "gof": 0.99,
                "min_concentration": 1,
                "max_concentration": 10000,
                "abs_ic_25": 2.82,
                "abs_ic__50": 9.86,
                "abs_ic_75": 48.74,
                "concentration": [1, 10, 100, 1000, 10000],
                "inhibition": [6.76, 50.25, 82.32, 94.10, 97.42],
                "concentration_line": [1, 10, 100, 1000, 10000],
                "inhibition_line": [6.76, 50.25, 82.32, 94.10, 97.42]
            }
        ]
    }
]
```

#### tsv - text/tab-separated-values
```tsv
donor_id	specimen_id	specimen_type	drug	dss	dss_selective	gof	abs_ic_25	abs_ic_50	abs_ic_75	min_concentration	max_concentration	concentration	inhibition	concentration_line	inhibition_line
Donor2	Line1	Line	Drug1	33.56	28.03	0.99	2.82	9.86	48.74	1	10000	1, 10, 100, 1000, 10000	6.76, 50.25, 82.32, 94.10, 97.42	6.76, 50.25, 82.32, 94.10, 97.42	6.76, 50.25, 82.32, 94.10, 97.42
```

Fields description can be found [here](api-models-drugs.md).

### Responses
- `200` - request was processed successfully
- `400` - request data didn't pass validation
- `401` - missing JWT token
- `403` - missing required permissions
