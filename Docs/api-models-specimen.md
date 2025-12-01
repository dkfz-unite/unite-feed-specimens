# Specimen Data Model
Includes basic specimen information.

>[!NOTE]
> All exact dates are hidden and protected. Relative dates are shown instead, if calculation was possible.

**`id`*** - Specimen identifier.
- Note: Specimen identifiers are namespaced and should be unique for it's donor across all specimens of the same type.
- Type: _String_
- Limitations: Maximum length 255
- Example: `"Line1"`

**`donor_id`*** - Specimen donor identifier.
- Type: _String_
- Limitations: Maximum length 255
- Example: `"Donor1"`

**`parent_id`** - Parent specimen identifier.
- Type: _String_
- Limitations: Maximum length 255
- Example: `"Material1"`

**`parent_type`** - Parent specimen type.
- Type: _String_
- Possible values: `"Material"`, `"Line"`, `"Organoid"`, `"Xenograft"`
- Example: `"Material"`

**`creation_date`** - Date when specimen was created.
- Type: _String_
- Format: "YYYY-MM-DD"
- Limitations: Only either `creation_date` or `creation_day` can be set at once, not both
- Example: `"2020-02-05"`

**`creation_day`** - Relative number of days since donor enrollment when specimen was created.
- Type: _Number_
- Limitations: Integer, greater or equal to 1, only either `creation_date` or `creation_day` can be set at once, not both
- Example: `36`

**`type`** - Material type.
- Type: _String_
- Possible values: `"Normal"`, `"Tumor"`
- Example: `"Tumor"`

**`tumor_type`** - Material tumor type.
- Type: _String_
- Possible values: `"Primary"`, `"Metastasis"`, `"Recurrent"`
- Limitations: Can be set only if `type` is `"Tumor"`
- Example: `"Primary"`

**`tumor_grade`** - Material tumor grade.
- Type: _Number_
- Limitations: Integer, greater than 0
- Example: `3`

**`tumor_superfamily`** - Material tumor superfamily.
- Type: _String_
- Limitations: Maximum length 255
- Example: `"Carcinoma"`

**`tumor_family`** - Material tumor family.
- Type: _String_
- Limitations: Maximum length 255
- Example: `"Adenocarcinoma"`

**`tumor_class`** - Material tumor class.
- Type: _String_
- Limitations: Maximum length 255
- Example: `"Colorectal Adenocarcinoma"`

**`tumor_subclass`** - Material tumor subclass.
- Type: _String_
- Limitations: Maximum length 255
- Example: `"Mucinous Adenocarcinoma"`

**`tumor_classifier`** - Classifier used to classify the specimen tumor.
- Type: _String_
- Limitations: Maximum length 255
- Example: `"OncoClassifier"`

**`tumor_classifier_version`** - Classifier version used to classify the specimen tumor.
- Type: _String_
- Limitations: Maximum length 100
- Example: `"v1.2"`

**`molecular_data`** - Specimen molecular data.
- Type: _Object([MolecularData](api-models-base-molecular.md))_
- Example: `{...}`

**`interventions`** - Specimen interventions data.
- Type: _Array_
- Element type: _Object([Intervention](api-models-base-intervention.md))_
- Limitations: Should contain at least one element
- Example: `[{...}, {...}]`


#### Specimen Type
Specimen can be of the following types:
- `"Material"` - all donor derived materials
- `"Line"` - cell lines
- `"Organoid"` - organoids
- `"Xenograft"` - xenografts


##
**`*`** - Required fields
