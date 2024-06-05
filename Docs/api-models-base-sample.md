# Sample Model
Includes information about analysed sample.

>[!NOTE]
> All exact dates are hiddent and protected. Relative dates are shown instead, if calculation was possible.

**`donor_id`*** - Sample donor identifier.
- Type: _String_
- Limitations: Maximum length 255
- Example: `"DO1"`

**`specimen_id`*** - Identifier of the specimen the sample was created from.
- Type: _String_
- Limitations: Maximum length 255
- Example: `"TI1"`

**`specimen_type`*** - Type of the specimen the sample was created from.
- Type: _String_
- Possible values: `"Material"`, `"Line"`, `"Organoid"`, `"Xenograft"`
- Example: `"Material"`

**`analysis_type`*** - Type of the analysis performed on the sample.
- Type: _String_
- Possible values: `"DSA"`
- Example: `"DSA"`

**`analysis_date`** - Date when the analysis was performed.
- Type: _Date_
- Limitations: Either 'analysis_date' or 'analysis_day' should be set.
- Format: "YYYY-MM-DD"
- Example: `2023-12-01`

**`analysis_day`** - Relative number of days since diagnosis statement when the analysis was performed.
- Type: _Integer_
- Limitations: Integet, greater than or equal to 1, either 'date' or 'day' should be set.
- Example: `22`


#### Specimen Type
Specimen can be of the following types:
- `"Material"` - all donor derived materials
- `"Line"` - cell lines
- `"Organoid"` - organoids
- `"Xenograft"` - xenografts

#### Analysis Type
Analysis can be of the following types:
- `"DSA"` - drugs screening analysis

##
**`*`** - Required fields
