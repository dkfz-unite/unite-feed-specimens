# Intervention Model
Includes model intervention data.

**`type`*** - Intervention type.
- Type: _String_
- Limitations: Maximum length 100
- Example: `"Intervention type"`

**`details`** - Intervention details.
- Type: _String_
- Example: `"Intervention details."`

**`start_date`** - Date, when intervention has started.
- Note: It's hidden and protected. Relative date is shown instead, if calculation was possible.
- Type: _String_
- Format: "YYYY-MM-DD"
- Limitations: Only either `start_date` or `start_day` can be set at once, not both
- Example: `"2020-04-05"`

**`Start_day`** - Relative number of days since specimen creation, when intervention has started.
- Type: _Number_
- Limitations: Integer, greater or equal to 0, only either `start_date` or `start_day` can be set at once, not both
- Example: `5`

**`end_date`** - Date, when intervention has ended.
- Note: It's hidden and protected. Relative date is shown instead, if calculation was possible.
- Type: _String_
- Format: "YYYY-MM-DD"
- Limitations: Integer, greater or equal to `start_date`, only either `end_date` or `duration_days` can be set at once, not both
- Example: `"2020-04-10"`

**`duration_days`** - Intervention duration in days.
- Type: _Number_
- Limitations: Integer, greater or equal to 0, only either `end_date` or `duration_days` can be set at once, not both
- Example: `5`

**`results`** - Intervention results.
- Type: _String_
- Example: `"Intervention results."`

##
**`*`** - Required fields