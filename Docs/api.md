# Specimens Data Feed API

## GET: [api](http://localhost:5104/api) - [api/specimens-feed](https://localhost/api/specimens-feed)
Health check.

### Responses
`"2022-03-17T09:45:10.9359202Z"` - Current UTC date and time in JSON format, if service is up and running


## POST: [api/specimens](http://localhost:5104/api/specimens) - [api/specimens-feed/specimens](https://localhost/api/specimens-feed/specimens)
Submit specimens data (tissue, cell line, organoid or xenograft).

Request implements **UPSERT** logic:
- Missing data will be populated
- Existing data will be updated
- Redundand data will be removed

### Headers
- `Authorization: Bearer [token]` - JWT token with `Data.Write` permission.

### Body - application/json
```json
[
    {
        "Id": "TI1",
        "DonorId": "DO1",
        "ParentId": null,
        "ParentType": null,
        "CreationDate": "2020-02-01T00:00:00",
        "CreationDay": null,
        "Tissue": {
            "Type": "Tumor",
            "TumorType": "Primary",
            "Source": "Solid tissue"
        },
        "MolecularData": {
            "MgmtStatus": "Methylated",
            "IdhStatus": "Wild Type",
            "IdhMutation": null,
            "GeneExpressionSubtype": "Classical",
            "MethylationSubtype": "H3-K27",
            "GcimpMethylation": true
        }
    },
    {
        "Id": "CL1TI1",
        "DonorId": "DO1",
        "ParentId": "TI1",
        "ParentType": "Tissue",
        "CreationDate": "2020-02-05T00:00:00",
        "CreationDay": null,
        "CellLine": {
            "Species": "Human",
            "Type": "Stem Cell",
            "CultureType": "Both",
            "Info": {
                "Name": "CLP_CL1TI1",
                "DepositorName": "Erica Polden",
                "DepositorEstablishment": "Colonord Research Centre",
                "EstablishmentDate": "2020-02-05T00:00:00",
                "PubMedLink": "https://pubmed.ncbi.nlm.nih.gov",
                "AtccLink": "https://www.atcc.org/",
                "ExPasyLink": "https://www.expasy.org/"
            }
        },
        "DrugsScreeningData": [
            {
                "Drug": "A-1155463",
                "Dss": 33.56,
                "DssSelective": 28.03,
                "Gof": 0.99,
                "MinConcentration": 1,
                "MaxConcentration": 10000,
                "Concentrations": [1, 10, 100, 1000, 10000],
                "Inhibitions": [6.76, 50.25, 82.32, 94.10, 97.42],
                "InhibitionsControl": [6.76, 50.25, 82.32, 94.10, 97.42],
                "InhibitionsSample": [6.76, 50.25, 82.32, 94.10, 97.42],
                "AbsIC25": 2.82,
                "AbsIC50": 9.86,
                "AbsIC75": 48.74
            }
        ],
        "MolecularData": {
            "MgmtStatus": "Methylated",
            "IdhStatus": "Wild Type",
            "IdhMutation": null,
            "GeneExpressionSubtype": "Classical",
            "MethylationSubtype": "H3-K27",
            "GcimpMethylation": true
        }
    },
    {
        "Id": "XE1CL1TI1",
        "DonorId": "DO1",
        "ParentId": "CL1TI1",
        "ParentType": "CellLine",
        "CreationDate": "2020-03-01T00:00:00",
        "CreationDay": null,
        "Xenograft": {
            "MouseStrain": "Nude",
            "GroupSize": 9,
            "ImplantType": "Other",
            "TissueLocation": "Cortical",
            "ImplantedCellsNumber": 750000,
            "Tumorigenicity": true,
            "TumorGrowthForm": "Invasive",
            "SurvivalDays": "20-30",
            "Interventions": [
                {
                    "Type": "Intervention type",
                    "Details": "Intervention details",
                    "StartDate": "2020-03-05T00:00:00",
                    "StartDay": null,
                    "EndDate": "2020-03-10T00:00:00",
                    "DurationDays": null,
                    "Results": "Intervention results"
                }
            ]
        },
        "DrugsScreeningData": null,
        "MolecularData": {
            "MgmtStatus": "Methylated",
            "IdhStatus": "Wild Type",
            "IdhMutation": null,
            "GeneExpressionSubtype": "Classical",
            "MethylationSubtype": "H3-K27",
            "GcimpMethylation": true
        }
    },
    {
        "Id": "OR1CL1TI1",
        "DonorId": "DO1",
        "ParentId": "CL1TI1",
        "ParentType": "CellLine",
        "CreationDate": "2020-04-01T00:00:00",
        "CreationDay": null,
        "Organoid": {
            "Medium": "Medium 1",
            "ImplantedCellsNumber": 1500000,
            "Tumorigenicity": false,
            "Interventions": [
                {
                    "Type": "Intervention type",
                    "Details": "Intervention details",
                    "StartDate": "2020-04-05T00:00:00",
                    "StartDay": null,
                    "EndDate": "2020-04-10T00:00:00",
                    "DurationDays": null,
                    "Results": "Intervention results"
                }
            ]
        },
        "DrugsScreeningData": null,
        "MolecularData": {
            "MgmtStatus": "Methylated",
            "IdhStatus": "Mutant",
            "IdhMutation": "IDH1 R132C",
            "GeneExpressionSubtype": null,
            "MethylationSubtype": null,
            "GcimpMethylation": true
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


## POST: [api/drugs](http://localhost:5104/api/drugs) - [api/specimens-feed/drugs](http://localhost/api/specimens-feed/drugs)
Submit drugs screening data. Donors and specimens should be present in the system.

Request implements **UPSERT** logic:
- Missing data will be populated
- Existing data will be updated
- Redundand data will be removed

### Headers
- `Authorization: Bearer [token]` - JWT token with `Data.Write` permission.

### Body - application/json
```json
[
    {
        "DonorId": "DO1",
        "SpecimenId": "CL1TI1",
        "SpecimenType": "CellLine",
        "Data": [
            {
                "Drug": "A-1155463",
                "Dss": 33.56,
                "DssSelective": 28.03,
                "Gof": 0.99,
                "MinConcentration": 1,
                "MaxConcentration": 10000,
                "Concentration": [1, 10, 100, 1000, 10000],
                "Inhibition": [6.76, 50.25, 82.32, 94.10, 97.42],
                "Dose": [1, 10, 100, 1000, 10000],
                "Response": [6.76, 50.25, 82.32, 94.10, 97.42],
                "AbsIC25": 2.82,
                "AbsIC50": 9.86,
                "AbsIC75": 48.74
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
