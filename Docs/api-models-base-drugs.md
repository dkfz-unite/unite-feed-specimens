# Drug Screening Model
Includes pre-processed drug screening data.

**`Drug`*** - Tested drug name.
- Type: _String_
- Limitations: Maximum length 255
- Example: `"A-1155463"`

**`Dss`*** - Asymmetric Drug Sensitivity Score (DSS).
- Type: _Number_
- Limitations: Double, should be in range [0, 100]
- Example: `33.56601234`

**`DssSelective`** - Selective 'DSS' (difference between DSS of samples and controls).
- Type: _Number_
- Limitations: Double, should be in range [-100, 100]
- Example: `28.034167491`

**`Gof`** - Goodness of fit.
- Type: _Number_
- Limitations: Double, should be in range [0.0, 1.0]
- Example: `0.995628526049492`

**`MinConcentration`** - Minimum tested concentration (in nM).
- Type: _Number_
- Limitations: Double, greater or equal to 0
- Example: `1`

**`MaxConcentration`** - Maximum tested concentration (in nM).
- Type: _Number_
- Limitations: Double, greater or equal to 0
- Example: `10000`

**`Concentrations`** - Concentration at corresponding inhibition percent from 'Inhibitions' array.
- Note: Used to plot drug response curve, the more numbers are in the array, the more precise the plot is.
- Type: _Array_
_ Limitations: Should be the same length as 'Inhibitions' array
- Element type: _Number_
- Element limitations: Double, should be greater or equal to 0
- Example: `[1, 10, 100, 1000, 10000]`

**`Inhibitions`** - Percent inhibition at corresponding concentration from 'Concentrations' array.
- Note: Used to plot drug response curve, the more numbers are in the array, the more precise the plot is.
- Type: _Array_
_ Limitations: Should be the same length as 'Concentrations' array
- Element type: _Number_
- Element limitations: Double, should be in range [-150, 150]
- Example: `[6.76, 50.25, 82.32, 94.10, 97.42]`

**`InhibitionsControl`** - Percent inhibition at (N)th concentration from 'MinConcentration' to 'MaxConcentration' in **control** sample.
- Note: Used to draw points on drug response curve.
- Type: _Array_
_ Limitations: Should be the same length as 'InhibitionsSample' array
- Element type: _Number_
- Element limitations: Double, should be in range [-150, 150]
- Example: `[6.76, 50.25, 82.32, 94.10, 97.42]`

**`InhibitionsSample`** - Percent inhibition at (N)th concentration from 'MinConcentration' to 'MaxConcentration' in **target** sample.
- Note: Used to draw points on drug response curve.
- Type: _Array_
_ Limitations: Should be the same length as 'InhibitionsControl' array
- Element type: _Number_
- Element limitations: Double, should be in range [-150, 150]
- Example: `[6.76, 50.25, 82.32, 94.10, 97.42]`

**`AbsIC25`** - Concentration at 25% inhibition (based on the fitted dose-response curve).
- Type: _Number_
- Limitations: Double, greater or equal to 0
- Example: `2.82359483783524`

**`AbsIC50`** - Concentration at 50% inhibition (based on the fitted dose-response curve).
- Type: _Number_
- Limitations: Double, greater or equal to 0
- Example: `9.86839437413983`

**`AbsIC75`** - Concentration at 75% inhibition (based on the fitted dose-response curve).
- Type: _Number_
- Limitations: Double, greater or equal to 0
- Example: `48.7482797871684`

##
**`*`** - Required fields
