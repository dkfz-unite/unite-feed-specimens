# Drug Screening Model
Includes pre-processed drug screening data.

**`drug`*** - Tested drug name.
- Type: _String_
- Limitations: Maximum length 255
- Example: `"Drug1"`

**`dss`*** - Asymmetric Drug Sensitivity Score (DSS).
- Type: _Number_
- Limitations: Double, should be in range [0, 100]
- Example: `33.56`

**`dss_selective`** - Selective 'dss' (difference between DSS of samples and controls).
- Type: _Number_
- Limitations: Double, should be in range [-100, 100]
- Example: `28.03`

**`gof`** - Goodness of fit.
- Type: _Number_
- Limitations: Double, should be in range [0.0, 1.0]
- Example: `0.99`

**`abs_ic_25`** - Concentration at 25% inhibition (based on the fitted dose-response curve).
- Type: _Number_
- Limitations: Double, greater or equal to 0
- Example: `2.82`

**`abs_ic_50`** - Concentration at 50% inhibition (based on the fitted dose-response curve).
- Type: _Number_
- Limitations: Double, greater or equal to 0
- Example: `9.86`

**`abs_ic_75`** - Concentration at 75% inhibition (based on the fitted dose-response curve).
- Type: _Number_
- Limitations: Double, greater or equal to 0
- Example: `48.74`

**`min_concentration`** - Minimum tested concentration (in nM).
- Type: _Number_
- Limitations: Double, greater to 0
- Example: `1`

**`max_concentration`** - Maximum tested concentration (in nM).
- Type: _Number_
- Limitations: Double, greater to 0
- Example: `10000`

**`concentration`** - Concentration (dose) at corresponding inhibition (response) percent from **`inhibition`** array.
- Note: Used to draw visible points on drug response curve.
- Type: _Array_
- Limitations: Should be the same length as **`inhibition`** array
- Element type: _Number_
- Element limitations: Double, should be greater or equal to 0
- Example: `[1, 10, 100, 1000, 10000]`

**`inhibition`** - Percent inhibition (response) at corresponding concentration (dose) from **`concentration`** array.
- Note: Used to draw visible points on drug response curve.
- Type: _Array_
- Limitations: Should be the same length as **`concentration`** array
- Element type: _Number_
- Element limitations: Double, should be in range [-150, 150]
- Example: `[6.76, 50.25, 82.32, 94.10, 97.42]`

**`concentration_line`** - Concentration (dose) at corresponding inhibition (response) percent from **`inhibition_line`** array.
- Note: Used to draw drug response curve. The more numbers are in the array, the more precise the curve is.
- Type: _Array_
- Limitations: Should be the same length as **`inhibition_line`** array
- Element type: _Number_
- Element limitations: Double, should be greater or equal to 0
- Example: `[1, 10, 100, 1000, 10000]`

**`inhibition_line`** - Percent inhibition (response) at corresponding concentration (dose) from **`concentration_line`** array.
- Note: Used to draw drug response curve. The more numbers are in the array, the more precise the curve is.
- Type: _Array_
- Limitations: Should be the same length as **`concentration_line`** array
- Element type: _Number_
- Element limitations: Double, should be in range [-150, 150]
- Example: `[6.76, 50.25, 82.32, 94.10, 97.42]`

##
**`*`** - Required fields
