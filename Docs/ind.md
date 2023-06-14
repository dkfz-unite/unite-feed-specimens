# Specimens Index

Specimens index has specimen-centric structure and includes information about specimens and their connected data.

Specimens index does not include information about:
- genes - information about genes can be retrieved from [genes]() index
- variants - information about variants can be retrieved from [variants]() index

If a search query to specimens index includes gene and/or variant filters, genes and/or variants indices are queried first and aggregated results are used to subsequently query specimens index.

## Index structure

Index entry includes:
- specimen base data - this data is also part of other types of indices (e.g. [genes](), [variants]())
- specimen index data - this data is specific to specimens index
- [available data](./ind-available-data.md) - information about all connected data