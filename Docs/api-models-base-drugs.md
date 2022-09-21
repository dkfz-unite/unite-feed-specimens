# Drug Screening Model
Includes pre-processed drug screening data.

**`Drug`*** - Tested drug name.
- Type: _String_
- Limitations: Maximum length 255
- Example: `"A-1155463"`

**`Dss`*** - Asymmetric Drug Sensitivity Score (DSS).
- Type: _Number_
- Limitations: Double, should be in range [0, 100]
- Example: `33.56`

**`DssSelective`** - Selective 'DSS' (difference between DSS of samples and controls).
- Type: _Number_
- Limitations: Double, should be in range [-100, 100]
- Example: `28.03`

**`Gof`** - Goodness of fit.
- Type: _Number_
- Limitations: Double, should be in range [0.0, 1.0]
- Example: `0.99`

**`MinConcentration`** - Minimum tested concentration (in nM).
- Type: _Number_
- Limitations: Double, greater or equal to 0
- Example: `1`

**`MaxConcentration`** - Maximum tested concentration (in nM).
- Type: _Number_
- Limitations: Double, greater or equal to 0
- Example: `10000`

**`Concentration`** - Concentration (dose) at corresponding inhibition (response) percent from **`Inhibition`** array.
- Note: Used to draw visible points on drug response curve.
- Type: _Array_
_ Limitations: Should be the same length as **`Inhibition`** array
- Element type: _Number_
- Element limitations: Double, should be greater or equal to 0
- Example: `[1, 10, 100, 1000, 10000]`

**`Inhibition`** - Percent inhibition (response) at corresponding concentration (dose) from **`Concentration`** array.
- Note: Used to draw visible points on drug response curve.
- Type: _Array_
_ Limitations: Should be the same length as **`Concentration`** array
- Element type: _Number_
- Element limitations: Double, should be in range [-150, 150]
- Example: `[6.76, 50.25, 82.32, 94.10, 97.42]`

**`Dose`** - Concentration (dose) at corresponding inhibition (response) percent from **`Response`** array.
- Note: Used to draw drug response curve. The more numbers are in the array, the more precise the curve is.
- Type: _Array_
_ Limitations: Should be the same length as **`Response`** array
- Element type: _Number_
- Element limitations: Double, should be greater or equal to 0
- Example: `[1, 10, 100, 1000, 10000]`

**`Response`** - Percent inhibition (response) at corresponding concentration (dose) from **`Dose`** array.
- Note: Used to draw drug response curve. The more numbers are in the array, the more precise the curve is.
- Type: _Array_
_ Limitations: Should be the same length as **`Dose`** array
- Element type: _Number_
- Element limitations: Double, should be in range [-150, 150]
- Example: `[6.76, 50.25, 82.32, 94.10, 97.42]`

**`AbsIC25`** - Concentration at 25% inhibition (based on the fitted dose-response curve).
- Type: _Number_
- Limitations: Double, greater or equal to 0
- Example: `2.82`

**`AbsIC50`** - Concentration at 50% inhibition (based on the fitted dose-response curve).
- Type: _Number_
- Limitations: Double, greater or equal to 0
- Example: `9.86`

**`AbsIC75`** - Concentration at 75% inhibition (based on the fitted dose-response curve).
- Type: _Number_
- Limitations: Double, greater or equal to 0
- Example: `48.74`

##
**`*`** - Required fields
