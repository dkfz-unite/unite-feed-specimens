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
- post:[api/tissues/{type?}](#post-apitissuestype) - submit tissues data in given type.
- post:[api/organoids/{type?}](#post-apiorganoidstype) - submit organoids data in given type.
- post:[api/organoid-interventions/{type?}](#post-apiorganoid-interventionstype) - submit organoid interventions data in given type.
- post:[api/xenografts/{type?}](#post-apixenograftstype) - submit xenografts data in given type.
- post:[api/xenograft-interventions/{type?}](#post-apixenograft-interventionstype) - submit xenograft interventions data in given type.
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
        "tissue": {
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
        "id": "CL1TI1",
        "donor_id": "DO1",
        "parent_id": "TI1",
        "parent_type": "Tissue",
        "creation_date": "2020-02-05",
        "creation_day": null,
        "cell_line": {
            "species": "Human",
            "type": "Stem Cell",
            "cultureType": "Both",
            "info": {
                "name": "CLP_CL1TI1",
                "depositor_name": "Erica Polden",
                "depositor_establishment": "Colonord Research Centre",
                "establishment_date": "2020-02-05",
                "pubmed_link": "https://pubmed.ncbi.nlm.nih.gov",
                "atcc_link": "https://www.atcc.org/",
                "expasy_link": "https://www.expasy.org/"
            }
        },
        "drugs_screening_data": [
            {
                "drug": "A-1155463",
                "dss": 33.56,
                "dss_selective": 28.03,
                "gof": 0.99,
                "abs_ic_25": 2.82,
                "abs_ic_50": 9.86,
                "abs_ic_75": 48.74,
                "min_concentration": 1,
                "max_concentration": 10000,
                "concentration": [1, 10, 100, 1000, 10000],
                "inhibition": [6.76, 50.25, 82.32, 94.10, 97.42],
                "concentration_line": [1, 10, 100, 1000, 10000],
                "inhibitions_line": [6.76, 50.25, 82.32, 94.10, 97.42]
            }
        ],
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
            "tissue_location": "Cortical",
            "implanted_cells_number": 750000,
            "tumorigenicity": true,
            "tumor_growth_form": "Invasive",
            "survival_days": "20-30",
            "interventions": [
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
        },
        "drugs_screening_data": null,
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
        "id": "OR1CL1TI1",
        "donor_id": "DO1",
        "parent_id": "CL1TI1",
        "parent_type": "CellLine",
        "creation_date": "2020-04-01",
        "creation_day": null,
        "organoid": {
            "medium": "Medium 1",
            "implanted_cells_number": 1500000,
            "tumorigenicity": false,
            "interventions": [
                {
                    "type": "Intervention type",
                    "details": "Intervention details",
                    "start_date": "2020-04-05",
                    "start_day": null,
                    "end_date": "2020-04-10",
                    "duration_days": null,
                    "results": "Intervention results"
                }
            ]
        },
        "drugs_screening_data": null,
        "molecular_data": {
            "mgmt_status": "Methylated",
            "idh_status": "Mutant",
            "idh_mutation": "IDH1 R132C",
            "gene_expression_subtype": null,
            "methylation_subtype": null,
            "gcimp_methylation": true
        }
    },
]
```

Fields description can be found [here](api-models-specimens.md).

### Responses
- `200` - request was processed successfully
- `400` - request data didn't pass validation
- `401` - missing JWT token
- `403` - missing required permissions


## POST: [api/tissues/{type?}](http://localhost:5104/api/tissues)
Submit tissues data.

### Body
Supported formats are:
- `tsv` - text/tab-separated-values

##### tsv - text/tab-separated-values
```tsv
id	donor_id	parent_id	parent_type	creation_date	creation_day	type	tumor_type	source	mgmt_status	idh_status	idh_mutation	gene_expression_subtype	methylation_subtype	gcimp_methylation
TI1	DO1			2020-02-01		Tumor	Primary	Solid tissue	Methylated	Wild Type		Classical	H3-K27	true
```

Fields description can be found [here](./api-models-specimens.md) and [here](./api-models-base-tissue.md).

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

#### tsv - text/tab-separated-values
```tsv
donor_id    specimen_id medium  implanted_cells_number  tumorigenicity  mgmt_status	idh_status	idh_mutation	gene_expression_subtype	methylation_subtype	gcimp_methylation
DO1	OR1CL1TI1	Medium 1	1500000	false	Methylated	Wild Type		Classical	H3-K27	true
```

Fields description can be found [here](./api-models-specimens.md) and [here](./api-models-base-organoid.md).

### Responses
- `200` - request was processed successfully
- `400` - request data didn't pass validation
- `401` - missing JWT token
- `403` - missing required permissions


## POST: [api/organoid-interventions/{type?}](http://localhost:5104/api/organoid-interventions)
Submit organoid interventions data. Donors and specimens should be present in the system.

### Body
Supported formats are:
- `json` (**empty**) - application/json
- `tsv` - text/tab-separated-values

#### json - application/json
```json
[
    {
        "donor_id": "DO1",
        "specimen_id": "OR1CL1TI1",
        "data": [
            {
                "type": "Intervention type",
                "details": "Intervention details",
                "start_date": "2020-04-05",
                "start_day": null,
                "end_date": "2020-04-10",
                "duration_days": null,
                "results": "Intervention results"
            }
        ]
    }
]
```

#### tsv - text/tab-separated-values
```tsv
donor_id    specimen_id type    details start_date  start_day   end_date    duration_days   results
DO1	OR1CL1TI1	Intervention_Type   Intervention_details    2020-04-05	2020-04-10	Intervention_results
```

Fields description can be found [here](./api-models-specimens.md) and [here](./api-models-base-organoid.md).

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

### tsv - text/tab-separated-values
```tsv
donor_id    specimen_id mouse_strain    group_size  implant_type    tissue_location implanted_cells_number  tumorigenicity  tumor_growth_form   survival_days   mgmt_status	idh_status	idh_mutation	gene_expression_subtype	methylation_subtype	gcimp_methylation
DO1	XE1CL1TI1	Nude	9	Other	Cortical	750000	true	Invasive	20-30	Methylated	Wild Type		Classical	H3-K27	true
```

Fields description can be found [here](./api-models-specimens.md) and [here](./api-models-base-xenograft.md).

### Responses
- `200` - request was processed successfully
- `400` - request data didn't pass validation
- `401` - missing JWT token
- `403` - missing required permissions


## POST: [api/xenograft-interventions/{type?}](http://localhost:5104/api/xenograft-interventions)
Submit xenograft interventions data. Donors and specimens should be present in the system.

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
donor_id    specimen_id type    details start_date  start_day   end_date    duration_days   results
DO1	XE1CL1TI1	Intervention_Type   Intervention_details    2020-03-05	2020-03-10	Intervention_results
```

Fields description can be found [here](./api-models-specimens.md) and [here](./api-models-base-xenograft.md).

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
        "specimen_type": "CellLine",
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

Fields description can be found [here](api-models-drugs.md).

### Responses
- `200` - request was processed successfully
- `400` - request data didn't pass validation
- `401` - missing JWT token
- `403` - missing required permissions
