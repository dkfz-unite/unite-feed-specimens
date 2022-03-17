# Specimens Data Feed API

## GET: [api](http://localhost:5102/api)

Health check.


**Response**

`"2022-03-17T09:45:10.9359202Z"` - Current UTC date and time in JSON format, if service is up and running


## POST: [api/specimens](http://localhost:5102/api/specimens)

Submit specimens data (tissue, cell line, organoid or xenograft).

Request implements **UPSERT** logic:
- Missing data will be populated
- Existing data will be updated

**Boby** (_application/json_)
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
Fields description can be found [here](https://github.com/dkfz-unite/unite-specimens-feed/blob/main/Docs/api-specimens-models.md).

**Response**
- `200` - request was processed successfully
- `400` - request data didn't pass validation
