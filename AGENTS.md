# UNITE repository instructions

Before changing this repository, read the shared UNITE instructions and
relevant project documentation maintained in
[dkfz-unite/.github](https://github.com/dkfz-unite/.github/blob/main/README.md).

1. Prefer the local shared checkout: `../unite-meta/AGENTS.md`, relative to this
   repository root. A sibling `.github/AGENTS.md` or a user-provided path is also
   valid. Resolve its document links relative to that shared checkout.
2. Read the shared `AGENTS.md`, project overview, repository map, and topic
   documents relevant to the task, following the shared README's task index.
3. If no shared checkout is accessible, use the GitHub versions beginning with
   [shared AGENTS.md](https://github.com/dkfz-unite/.github/blob/main/AGENTS.md).
   If those are unavailable too, report the missing context and proceed only
   with what local documentation/code establishes; do not invent shared rules.
4. Read this repository's README and relevant local docs; inspect Git status.
   More specific instructions here supplement the shared guidance.

Keep common rules in `.github`; keep component details in this repository.

## Specimens feed

Status: reviewed with the owner (2026-10-12).

The specimens feed ingests specimens (materials, cell lines, organoids,
xenografts) with their derivation hierarchy, interventions and drug screenings,
writes them to the domain database and builds the specimens index. It follows
the same pattern as the [omics feed](../unite-feed-omics/AGENTS.md)
(submission staging, review, SQL writers, indexing workers), without
annotation or resources. Background:
[Data ingestion](../unite-meta/docs/data-ingestion.md),
[Processing pipeline](../unite-meta/docs/processing-pipeline.md),
[Domain model](../unite-meta/docs/domain.md#images-and-biological-specimens).

### Projects

| Project | Contents |
| --- | --- |
| `Unite.Specimens.Feed.Web` | Controllers, submission models (per specimen type, interventions, drugs), Mongo staging, workers and submission handlers |
| `Unite.Specimens.Feed` | SQL writers (`SpecimensWriter`, `InterventionsWriter`, `AnalysisWriter` for drug screenings), `SpecimensRemover`, repositories |
| `Unite.Specimens.Indices` | Specimen index creator and mapping |

There are no test projects.

### Build and run

```sh
dotnet build Unite.Specimens.Feed.sln
dotnet run --project Unite.Specimens.Feed.Web --launch-profile Unite.Specimens.Feed.Web
```

- Local URL `http://localhost:5104`; Docker `specimens.feed.unite.net`. Needs
  PostgreSQL, MongoDB and Elasticsearch.
- To test against the running platform, stop `unite.feed.specimens` first; see
  [Running a service locally](../unite-meta/docs/development-validation.md#running-a-service-locally-instead-of-its-container).

### Configuration and API

Environment variables are in the [README](README.md); endpoints and models in
[Docs/api.md](Docs/api.md). Routes, task types and write semantics:

| Route | Task type | Notes |
| --- | --- | --- |
| `api/entries/{material,line,organoid,xenograft}[/tsv]` | `MAT`, `LNE`, `ORG`, `XEN` | Specimen metadata, upsert; creates missing donors |
| `api/interventions[/tsv]` | `SPE_INT` | Interventions, replaced per specimen on re-upload |
| `api/analysis/drugs[/tsv]` | `SPE_DRG` | Drug screenings, replaced per sample on re-upload |
| `api/entry/{id}` (DELETE) | – | Deletes a specimen with its data and index entry |
| `api/indexing` (POST) | – | Administrator reindexing |

`review=false` creates the submission task as `Prepared`.

**Parent specimens** (owner, 2026-10-12): a specimen's `parent` must exist in
the database or in the same submission. Rows should be written parent-first;
a submission whose parent exists nowhere must fail. Never create placeholder
parents (unlike donors), because parents can have parents. Current code writes
rows in file order and `SpecimenRepositoryBase.FindParent` throws
`NotFoundException`, so the submission is retried every cycle and blocks the
queue (Jira UND-1073). Parents of another type arrive through another task
type; `SubmissionsWorker` runs MAT, LNE, ORG, XEN in this order, one task per
type per cycle.

### Processing

`SubmissionsWorker` runs one handler per task type; `SpecimensIndexingWorker`
builds specimen documents. Writers create missing donors in the default
project `Other` (public), as in the other feeds.

### Shared libraries

`Unite.Data.Context` 12.10.33, `Unite.Indices.Context` 13.10.0 (latest
13.10.20), `Unite.Cache` 1.0.0 (latest 1.3.0), `Unite.Essentials` 1.4.0 (latest
1.6.0), `Unite.Essentials.Tsv` 4.2.0.

### CI

`docker-images.yml` builds amd64 and arm64 images under one manifest on pushes
to `main`.

### Known issues

Defects are tracked in the private Jira; see the shared
[Track defects](../unite-meta/AGENTS.md#track-defects).
