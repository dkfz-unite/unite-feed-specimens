# Drug Screening Models

## Drug Screening
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

**`PI1`** - Percent inhibition at 1st (minimum) concentration.
- Type: _Number_
- Limitations: Double, should be in range [-150, 150]
- Example: `6.76124067981132`

**`PI2`** - Percent inhibition at 2nd (log10) concentration.
- Type: _Number_
- Limitations: Double, should be in range [-150, 150]
- Example: `50.2521862295519`

**`PI3`** - Percent inhibition at 3rd (log10) concentration.
- Type: _Number_
- Limitations: Double, should be in range [-150, 150]
- Example: `82.3287263864255`

**`PI4`** - Percent inhibition at 4th (log10) concentration.
- Type: _Number_
- Limitations: Double, should be in range [-150, 150]
- Example: `94.1096524252287`

**`PI4`** - Percent inhibition at 5th (maximum) concentration.
- Type: _Number_
- Limitations: Double, should be in range [-150, 150]
- Example: `97.4254716797063`

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
