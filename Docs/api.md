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
- get:[api/entries/material/{id}](#get-apientriesmaterialid) - get donor materials data submission document.
- post:[api/entries/meterial/{type?}](#post-apientriesmaterialtype) - submit donor materials data in given type.
- get:[api/entries/line/{id}](#get-apientrieslineid) - get cell lines data submission document.
- post:[api/entries/line/{type?}](#post-apientrieslinetype) - submit cell lines data in given type.
- get:[api/entries/organoid/{id}](#get-apientriesorganoidid) - get organoids data submission document.
- post:[api/entries/organoid/{type?}](#post-apientriesorganoidtype) - submit organoids data in given type.
- get:[api/entries/xenograft/{id}](#get-apientriesxenograftid) - get xenografts data submission document.
- post:[api/entries/xenograft/{type?}](#post-apientriesxenografttype) - submit xenografts data in given type.
- get:[api/interventions/{id}](#get-apiinterventionsid) - get interventions data submission document.
- post:[api/interventions/{type?}](#post-apiinterventionstype) - submit interventions data in given type.
- get:[api/analysis/drugs/{id}](#get-apianalysisdrugsid) - get drugs screening data submission document.
- post:[api/analysis/drugs/{type?}](#post-apianalysisdrugs) - submit drugs screening data.
- delete:[api/entry/{id}](#delete-apientryid) - delete specimen data.

> [!Note]
> **Json** is default data type for all requests and will be used if no data type is specified.


## GET: [api](http://localhost:5104/api)
Health check.

### Responses
`"2022-03-17T09:45:10.9359202Z"` - Current UTC date and time in JSON format, if service is up and running


## GET: [api/entries/material/{id}](http://localhost:5104/api/entries/material/1)
Get donor materials data submission document.

### Parameters
- `id` - submission ID.

### Responses
- `200` - submission document in JSON format
- `401` - missing JWT token
- `403` - missing required permissions
- `404` - submission not found


## POST: [api/entries/material/{type?}](http://localhost:5104/api/entries/material)
Submit donor materials data.

### Body
The body content depends on the URL `type` parameter:  
- `json` or **empty** (application/json) - JSON array of materials ([example](https://github.com/dkfz-unite/unite/blob/main/Unite.Web/Client/public/templates/specimens/materials.json)).
- `tsv` (text/tab-separated-values) - headered TSV list of materials ([example](https://github.com/dkfz-unite/unite/blob/main/Unite.Web/Client/public/templates/specimens/materials.tsv)).

Fields description can be found [here](api-models-material.md).

### Responses
- `200` - submission ID (can be used to track submission status)
- `400` - request data didn't pass validation
- `401` - missing JWT token
- `403` - missing required permissions


## GET: [api/entries/line/{id}](http://localhost:5104/api/entries/line/1)
Get cell lines data submission document.

### Parameters
- `id` - submission ID.

### Responses
- `200` - submission document in JSON format
- `401` - missing JWT token
- `403` - missing required permissions
- `404` - submission not found


## POST: [api/entries/line/{type?}](http://localhost:5104/api/entries/line)
Submit cell lines data.

### Body
The body content depends on the URL `type` parameter:  
- `json` or **empty** (application/json) - JSON array of cell lines ([example](https://github.com/dkfz-unite/unite/blob/main/Unite.Web/Client/public/templates/specimens/lines.json)).
- `tsv` (text/tab-separated-values) - headered TSV list of cell lines ([example](https://github.com/dkfz-unite/unite/blob/main/Unite.Web/Client/public/templates/specimens/lines.tsv)).

Fields description can be found [here](api-models-line.md).

### Responses
- `200` - submission ID (can be used to track submission status)
- `400` - request data didn't pass validation
- `401` - missing JWT token
- `403` - missing required permissions


## GET: [api/entries/organoid/{id}](http://localhost:5104/api/entries/organoid/1)
Get organoids data submission document.

### Parameters
- `id` - submission ID.

### Responses
- `200` - submission document in JSON format
- `401` - missing JWT token
- `403` - missing required permissions
- `404` - submission not found


## POST: [api/entries/organoid/{type?}](http://localhost:5104/api/entries/organoid)
Submit organoids data.

### Body
The body content depends on the URL `type` parameter:  
- `json` or **empty** (application/json) - JSON array of organoids ([example](https://github.com/dkfz-unite/unite/blob/main/Unite.Web/Client/public/templates/specimens/organoids.json)).
- `tsv` (text/tab-separated-values) - headered TSV list of organoids ([example](https://github.com/dkfz-unite/unite/blob/main/Unite.Web/Client/public/templates/specimens/organoids.tsv)).

Fields description can be found [here](api-models-organoid.md).

### Responses
- `200` - submission ID (can be used to track submission status)
- `400` - request data didn't pass validation
- `401` - missing JWT token
- `403` - missing required permissions


## GET: [api/entries/xenograft/{id}](http://localhost:5104/api/entries/xenograft/1)
Get xenografts data submission document.

### Parameters
- `id` - submission ID.

### Responses
- `200` - submission document in JSON format
- `401` - missing JWT token
- `403` - missing required permissions
- `404` - submission not found


## POST: [api/entries/xenograft/{type?}](http://localhost:5104/api/entries/xenograft)
Submit xenografts data.

### Body
The body content depends on the URL `type` parameter:  
- `json` or **empty** (application/json) - JSON array of xenografts ([example](https://github.com/dkfz-unite/unite/blob/main/Unite.Web/Client/public/templates/specimens/xenografts.json)).
- `tsv` (text/tab-separated-values) - headered TSV list of xenografts ([example](https://github.com/dkfz-unite/unite/blob/main/Unite.Web/Client/public/templates/specimens/xenografts.tsv)).

Fields description can be found [here](./api-models-xenograft.md).

### Responses
- `200` - submission ID (can be used to track submission status)
- `400` - request data didn't pass validation
- `401` - missing JWT token
- `403` - missing required permissions


## GET: [api/interventions/{id}](http://localhost:5104/api/interventions/1)
Get interventions data submission document.

### Parameters
- `id` - submission ID.

### Responses
- `200` - submission document in JSON format
- `401` - missing JWT token
- `403` - missing required permissions
- `404` - submission not found


## POST: [api/interventions/{type?}](http://localhost:5104/api/specimens/interventions)
Submit interventions data. Donors and specimens should be present in the system.

> [!Note]
> Donor materials can't have interventions.

### Body
The body content depends on the URL `type` parameter:  
- `json` or **empty** (application/json) - JSON array of specimen interventions ([example](https://github.com/dkfz-unite/unite/blob/main/Unite.Web/Client/public/templates/specimens/interventions.json)).
- `tsv` (text/tab-separated-values) - headered TSV list of specimen interventions ([example](https://github.com/dkfz-unite/unite/blob/main/Unite.Web/Client/public/templates/specimens/interventions.tsv)).

Fields description can be found [here](./api-models-interventions.md).

### Responses
- `200` - submission ID (can be used to track submission status)
- `400` - request data didn't pass validation
- `401` - missing JWT token
- `403` - missing required permissions


## GET: [api/analysis/drugs/{id}](http://localhost:5104/api/analysis/drugs/1)
Get drugs screening data submission document.

### Parameters
- `id` - submission ID.

### Responses
- `200` - submission document in JSON format
- `401` - missing JWT token
- `403` - missing required permissions
- `404` - submission not found


## POST: [api/analysis/drugs](http://localhost:5104/api/analysis/drugs)
Submit drugs screening analysis data (including analysis metadata).

> [!Note]
> Donor materials can't have drugs screening data.

### Body
The body content depends on the URL `type` parameter:  
- `json` or **empty** (application/json) - JSON formatted drugs sceening analysis results ([example](https://github.com/dkfz-unite/unite/blob/main/Unite.Web/Client/public/templates/specimens/drugs.json)).
- `tsv` (text/tab-separated-values) - TSV (headered) formatted drugs sceening analysis results ([example](https://github.com/dkfz-unite/unite/blob/main/Unite.Web/Client/public/templates/specimens/drugs.tsv)).

Fields description can be found [here](api-models-drugs.md).

### Responses
- `200` - submission ID (can be used to track submission status)
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
