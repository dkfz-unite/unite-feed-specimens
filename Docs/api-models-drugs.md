# Drugs Screening Analysis Data Model
Includes information about analysed sample and drugs screening data.

**`sample`*** - Sample data.
- Type: _[Object](./api-models-base-sample.md)_
- Example: `"{...}"`

**`entries`*** - Drug screening entries.
- Type: _Array_
- Element type: _Object([DrugScreening](#drug-screening))_
- Limitations: Should contain at least one element
- Example: `[{...}, {...}]`


## Drug Screening
Includes drug screening data.

**`drug`*** - Tested drug name.
- Type: _String_
- Limitations: Maximum length 255
- Example: `"Drug1"`

**`gof`** - Goodness of fit.
- Type: _Number_
- Limitations: Double, should be in range [0, 1]
- Example: `0.99`

**`dss`** - Asymmetric Drug Sensitivity Score (DSS).
- Type: _Number_
- Limitations: Double, should be in range [0, 1]
- Example: `0.33`

**`dsss`** - Selective 'dss' (difference between DSS of samples and controls).
- Type: _Number_
- Limitations: Double, should be in range [-1, 1]
- Example: `0.28`

**`dose_min`** - Minimum tested concentration (in nM).
- Type: _Number_
- Limitations: Double, greater than 0, less than **`dose_max`**
- Example: `1`

**`dose_max`** - Maximum tested concentration (in nM).
- Type: _Number_
- Limitations: Double, greater than 0, greater than **`dose_max`**
- Example: `10000`

**`dose_25`** - Concentration at 25% inhibition (based on the fitted dose-response curve).
- Type: _Number_
- Limitations: Double, greater than 0, less than **`dose_50`**, in range [**`dose_min`**, **`dose_max`**]
- Example: `2.82`

**`dose_50`** - Concentration at 50% inhibition (based on the fitted dose-response curve).
- Type: _Number_
- Limitations: Double, greater than 0, less than **`dose_75`**, in range [**`dose_min`**, **`dose_max`**]
- Example: `9.86`

**`dose_75`** - Concentration at 75% inhibition (based on the fitted dose-response curve).
- Type: _Number_
- Limitations: Double, greater than 0, in range [**`dose_min`**, **`dose_max`**]
- Example: `48.74`

**`doses`** - Concentration (dose) at corresponding inhibition (response) percent from **`responses`** array.
- Note: Used to draw drug response curve.
- Type: _Array_
- Limitations: Should be the same length as **`responses`** array
- Element type: _Number_
- Element limitations: Double, should be greater than 0
- Example: `[1, 10, 100, 1000, 10000]`

**`responses`** - Percent inhibition (response) at corresponding concentration (dose) from **`doses`** array.
- Note: Used to draw drug response curve.
- Type: _Array_
- Limitations: Should be the same length as **`doses`** array
- Element type: _Number_
- Element limitations: Double, should be in range [0, 1]
- Example: `[0.06, 0.52, 0.82, 0.94, 0.97]`


##
**`*`** - Required fields
