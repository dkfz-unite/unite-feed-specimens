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
- post:[api/entries/meterial/{type?}](#post-apientriesmaterialtype) - submit donor materials data in given type.
- post:[api/entries/line/{type?}](#post-apientrieslinetype) - submit cell lines data in given type.
- post:[api/entries/organoid/{type?}](#post-apientriesorganoidtype) - submit organoids data in given type.
- post:[api/entries/xenograft/{type?}](#post-apientriesxenografttype) - submit xenografts data in given type.
- post:[api/interventions/{type?}](#post-apiinterventionstype) - submit interventions data in given type.
- post:[api/analysis/drugs/{type?}](#post-apianalysisdrugs) - submit drugs screening data.
- delete:[api/entry/{id}](#delete-apientryid) - delete specimen data.

> [!Note]
> **Json** is default data type for all requests and will be used if no data type is specified.


## GET: [api](http://localhost:5104/api)
Health check.

### Responses
`"2022-03-17T09:45:10.9359202Z"` - Current UTC date and time in JSON format, if service is up and running


## POST: [api/entries/material/{type?}](http://localhost:5104/api/entries/material)
Submit donor materials data.

### Body
Supported formats are:
- `json` (**empty**) - application/json
- `tsv` - text/tab-separated-values

#### json - application/json
```json
[
    {
        "id": "Material1",
        "donor_id": "Donor1",
        "creation_date": "2020-01-15",
        "type": "Normal",
        "source": "Blood"
    },
    {
        "id": "Material2",
        "donor_id": "Donor1",
        "creation_date": "2020-01-15",
        "type": "Tumor",
        "tumor_type": "Primary",
        "source": "Tissue",
        "molecular_data": {
            "mgmt_status": "Methylated",
            "idh_status": "Wild Type",
            "gene_expression_subtype": "Classical",
            "methylation_subtype": "H3-K27"
        }
    },
    {
        "id": "Material1",
        "donor_id": "Donor2",
        "creation_date": "2020-01-15",
        "type": "Normal",
        "source": "Blood"
    },
    {
        "id": "Material2",
        "donor_id": "Donor2",
        "creation_date": "2020-01-15",
        "type": "Tumor",
        "tumor_type": "Primary",
        "source": "Tissue",
        "molecular_data": {
            "mgmt_status": "Unmethylated",
            "idh_status": "Mutant",
            "idh_mutation": "IDH1 R132H",
            "gcimp_methylation": false
        }
    },
    {
        "id": "Material3",
        "donor_id": "Donor2",
        "creation_date": "2020-03-01",
        "type": "Tumor",
        "tumor_type": "Recurrent",
        "source": "CSF",
        "molecular_data": {
            "mgmt_status": "Unmethylated",
            "idh_status": "Mutant",
            "idh_mutation": "IDH1 R132H",
            "gcimp_methylation": false
        }
    }
]
```

##### tsv - text/tab-separated-values
```tsv
id	donor_id	parent_id	parent_type	creation_date	creation_day	type	tumor_type	source	mgmt_status	idh_status	idh_mutation	gene_expression_subtype	methylation_subtype	gcimp_methylation
Material1	Donor1			2020-01-15		Normal		Blood						
Material2	Donor1			2020-01-15		Tumor	Primary	Tissue	Methylated	Wild Type		Classical	H3-K27	true
Material1	Donor2			2020-01-15		Normal		Blood						
Material2	Donor2			2020-01-15		Tumor	Primary	Tissue	Unmethylated	Mutant	IDH1 R132H			false
Material3	Donor2			2020-03-01		Tumor	Recurrent	Tissue	Unmethylated	Mutant	IDH1 R132H			false
```

Fields description can be found [here](api-models-material.md).

### Responses
- `200` - request was processed successfully
- `400` - request data didn't pass validation
- `401` - missing JWT token
- `403` - missing required permissions


## POST: [api/entries/line/{type?}](http://localhost:5104/api/entries/line)
Submit cell lines data.

### Body
Supported formats are:
- `json` (**empty**) - application/json
- `tsv` - text/tab-separated-values

#### json - application/json
```json
[
    {
        "id": "Line1",
        "donor_id": "Donor1",
        "parent_id": "Material2",
        "parent_type": "Material",
        "creation_date": "2020-02-01",
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
        },
        "molecular_data": {
            "mgmt_status": "Methylated",
            "idh_status": "Wild Type",
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
        },
        "molecular_data": {
            "mgmt_status": "Unmethylated",
            "idh_status": "Mutant",
            "idh_mutation": "IDH1 R132H",
            "gcimp_methylation": false
        }
    }
]
```

##### tsv - text/tab-separated-values
```tsv
id	donor_id	parent_id	parent_type	creation_date	creation_day	cells_species	cells_type	cells_culture_type	name	depositor_name	depositor_establishment	establishment_date	pubmed_link	atcc_link	expasy_link	mgmt_status	idh_status	idh_mutation	gene_expression_subtype	methylation_subtype	gcimp_methylation
Line1	Donor1	Material2	Material	2020-02-01		Human	Stem Cell	Suspension	D1M2L1	Depositor Golden	Line Depositor Centre	2020-02-01	https://pubmed.ncbi.nlm.nih.gov	https://www.atcc.org	https://www.expasy.org	Methylated	Wild Type		Classical	H3-K27	true
Line1	Donor2	Material2	Material	2020-02-01		Human	Differentiated	Adherent	D2M2L1	Depositor Golden	Line Depositor Centre	2020-02-01	https://pubmed.ncbi.nlm.nih.gov	https://www.atcc.org	https://www.expasy.org	Unmethylated	Mutant	IDH1 R132H			false
```

Fields description can be found [here](api-models-line.md).

### Responses
- `200` - request was processed successfully
- `400` - request data didn't pass validation
- `401` - missing JWT token
- `403` - missing required permissions


## POST: [api/entries/organoid/{type?}](http://localhost:5104/api/entries/organoid)
Submit organoids data.

### Body
Supported formats are:
- `json` (**empty**) - application/json
- `tsv` - text/tab-separated-values

#### json - application/json
```json
[
    {
        "id": "Organoid1",
        "donor_id": "Donor1",
        "parent_id": "Line1",
        "parent_type": "Line",
        "creation_date": "2020-02-05",
        "medium": "Medium1",
        "implanted_cells_number": 10500,
        "tumorigenicity": true,
        "molecular_data": {
            "mgmt_status": "Methylated",
            "idh_status": "Wild Type",
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
        "medium": "Medium2",
        "implanted_cells_number": 45000,
        "tumorigenicity": true,
        "molecular_data": {
            "mgmt_status": "Unmethylated",
            "idh_status": "Mutant",
            "idh_mutation": "IDH1 R132H",
            "gcimp_methylation": false
        }
    }
]
```

#### tsv - text/tab-separated-values
```tsv
id	donor_id	parent_id	parent_type	creation_date	creation_day	medium	implanted_cells_number	tumorigenicity	mgmt_status	idh_status	idh_mutation	gene_expression_subtype	methylation_subtype	gcimp_methylation
Organoid1	Donor1	Line1	Line	2020-02-05		Medium1	10500	true	Methylated	Wild Type		Classical	H3-K27	true
Organoid1	Donor2	Line1	Line	2020-02-05		Medium2	45000	true	Unmethylated	Mutant	IDH1 R132H			false
```

Fields description can be found [here](api-models-organoid.md).

### Responses
- `200` - request was processed successfully
- `400` - request data didn't pass validation
- `401` - missing JWT token
- `403` - missing required permissions


## POST: [api/entries/xenograft/{type?}](http://localhost:5104/api/entries/xenograft)
Submit xenografts data.

### Body
Supported formats are:
- `json` (**empty**) - application/json
- `tsv` - text/tab-separated-values

##### json - application/json
```json
[
    {
        "id": "Xenograft1",
        "donor_id": "Donor1",
        "parent_id": "Line1",
        "parent_type": "Line",
        "creation_date": "2020-02-05",
        "mouse_strain": "Nude",
        "group_size": 20,
        "implant_type": "Orhtotopical",
        "implant_location": "Striatal",
        "implanted_cells_number": 750000,
        "tumorigenicity": true,
        "tumor_growth_form": "Invasive",
        "survival_days": "20-30",
        "molecular_data": {
            "mgmt_status": "Methylated",
            "idh_status": "Wild Type",
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
        "mouse_strain": "Nude",
        "group_size": 30,
        "implant_type": "Orhtotopical",
        "implant_location": "Cortical",
        "implanted_cells_number": 550000,
        "tumorigenicity": true,
        "tumor_growth_form": "Invasive",
        "survival_days": "15-20",
        "molecular_data": {
            "mgmt_status": "Unmethylated",
            "idh_status": "Mutant",
            "idh_mutation": "IDH1 R132H",
            "gcimp_methylation": false
        }
    }
]
```

#### tsv - text/tab-separated-values
```tsv
id	donor_id	parent_id	parent_type	creation_date	creation_day	mouse_strain	group_size	implant_type	implant_location	implanted_cells_number	tumorigenicity	tumor_growth_form	survival_days	mgmt_status	idh_status	idh_mutation	gene_expression_subtype	methylation_subtype	gcimp_methylation
Xenograft1	Donor1	Line1	Line	2020-02-05		Nude	20	Orhtotopical	Striatal	750000	true	Invasive	20-30	Methylated	Wild Type		Classical	H3-K27	true
Xenograft1	Donor2	Line1	Line	2020-02-05		Nude	30	Orhtotopical	Cortical	550000	true	Invasive	15-20	Unmethylated	Mutant	IDH1 R132H			false
```

Fields description can be found [here](./api-models-xenograft.md).

### Responses
- `200` - request was processed successfully
- `400` - request data didn't pass validation
- `401` - missing JWT token
- `403` - missing required permissions


## POST: [api/interventions/{type?}](http://localhost:5104/api/specimens/interventions)
Submit interventions data. Donors and specimens should be present in the system.

> [!Note]
> Donor materials can't have interventions.

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
                "end_date": "2020-02-10",
                "results": "Specimen specific intervention results"
            },
            {
                "type": "Drug2",
                "details": "Specimen specific intervention details",
                "start_date": "2020-02-10",
                "end_date": "2020-02-15",
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
                "end_date": "2020-02-10",
                "results": "Specimen specific intervention results"
            },
            {
                "type": "Drug2",
                "details": "Specimen specific intervention details",
                "start_date": "2020-02-10",
                "end_date": "2020-02-15",
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
                "end_date": "2020-02-10",
                "results": "Specimen specific intervention results"
            },
            {
                "type": "Drug2",
                "details": "Specimen specific intervention details",
                "start_date": "2020-02-10",
                "end_date": "2020-02-15",
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
                "end_date": "2020-02-10",
                "results": "Specimen specific intervention results"
            },
            {
                "type": "Drug2",
                "details": "Specimen specific intervention details",
                "start_date": "2020-02-10",
                "end_date": "2020-02-15",
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


## POST: [api/analysis/drugs](http://localhost:5104/api/analysis/drugs)
Submit drugs screening data (including analysis data).

> [!Note]
> Donor materials can't have drug screenings.

### Body
Supported formats are:
- `json` (**empty**) - application/json
- `tsv` - text/tab-separated-values

#### json - application/json
```json
{
    "sample": {
        "donor_id": "Donor1",
        "specimen_id": "Line1",
        "specimen_type": "Line",
        "analysis_type": "DSA",
        "analysis_date": "2020-02-05"
    },
    "entries": [
        {
            "drug": "Drug1",
            "gof": 95,
            "dss": 35,
            "dsss": 15,
            "min_dose": 1,
            "max_dose": 10000,
            "dose_25": 5.25,
            "dose_50": 10.50,
            "dose_75": 50.75,
            "doses": [1, 10, 100, 1000, 10000],
            "responses": [2.25, 25.50, 85.25, 95.25, 95.50]
        },
        {
            "drug": "Drug2",
            "gof": 75,
            "dss": 30,
            "dsss": 10,
            "min_dose": 1,
            "max_dose": 10000,
            "dose_25": 4.25,
            "dose_50": 8.50,
            "dose_75": 47.75,
            "doses": [1, 10, 100, 1000, 10000],
            "responses": [2.05, 20.50, 75.25, 90.25, 90.50]
        },
        {
            "drug": "Drug3",
            "gof": 55,
            "dss": 25,
            "dsss": 5,
            "min_dose": 1,
            "max_dose": 10000,
            "dose_25": 2.25,
            "dose_50": 4.50,
            "dose_75": 25.75,
            "doses": [1, 10, 100, 1000, 10000],
            "responses": [0.05, 10.50, 35.25, 65.25, 65.50]
        },
        {
            "drug": "Drug4",
            "gof": 35,
            "dss": 15,
            "dsss": -5,
            "min_dose": 1,
            "max_dose": 10000,
            "dose_25": 0.25,
            "dose_50": 2.50,
            "dose_75": 15.75,
            "doses": [1, 10, 100, 1000, 10000],
            "responses": [0.00, 5.50, 17.25, 55.25, 55.50]
        },
        {
            "drug": "Drug5",
            "gof": 25,
            "dss": 10,
            "dsss": -10,
            "min_dose": 1,
            "max_dose": 10000,
            "dose_25": 0.05,
            "dose_50": 0.50,
            "dose_75": 7.75,
            "doses": [1, 10, 100, 1000, 10000],
            "responses": [0.00, 0.50, 5.25, 45.25, 45.50]
        }
    ]
}
```

#### tsv - text/tab-separated-values
```tsv
# donor_id: Donor1
# specimen_id: Line1
# specimen_type: Line
# analysis_type: DSA
# analysis_date: 2020-02-05
drug    gof dss dsss min_dose   max_dose    donse_25    donse_50    donse_75    doses   responses
Drug1	95	35	15	1	10000	5.25	10.50	50.75	1,10,100,1000,10000	2.25,25.50,85.25,95.25,95.50
Drug2	75	30	10	1	10000	4.25	8.50	47.75	1,10,100,1000,10000	2.05,20.50,75.25,90.25,90.50
Drug3	55	25	5	1	10000	2.25	4.50	25.75	1,10,100,1000,10000	0.05,10.5,35.25,65.25,65.5
Drug4	35	15	-5	1	10000	0.25	2.5	15.75	1,10,100,1000,10000	0,5.5,17.25,55.25,55.5
Drug5	25	10	-10	1	10000	0.05	0.5	7.75	1,10,100,1000,10000	0,0.5,5.25,45.25,45.5
```

Fields description can be found [here](api-models-drugs.md).

### Responses
- `200` - request was processed successfully
- `400` - request data didn't pass validation
- `401` - missing JWT token
- `403` - missing required permissions


## DELETE: [api/entry/{id}](http://localhost:5104/api/entry/{id})
Delete specimen data.

### Parameters
- `id` - ID of the specimen to delete.

### Responses
- `200` - request was processed successfully
- `401` - missing JWT token
- `403` - missing required permissions
- `404` - specimen not found
