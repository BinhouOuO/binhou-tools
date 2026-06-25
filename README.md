# BinhouTools

Binhou's Unity tool repository (mono-repo). Each tool is structured as an independent UPM (Unity Package Manager) package that can be imported individually.

## Repository Structure

```
BinhouTools/
└── Packages/
    └── com.binhou.batchrenamer/     ← Batch Renamer Tool
```

| Package | Description | Tag Prefix |
|---|---|---|
| `com.binhou.batchrenamer` | Batch Renamer Tool for Unity objects & assets | `rename/` |

## Importing a Package

In Unity, open the Package Manager (Window -> Package Manager), click the **"+"** icon, select **Add package from git URL**, and enter the URL with the `?path=` query parameter pointing to the specific tool folder:

```
https://github.com/BinhouOuO/binhou-tools.git?path=/Packages/com.binhou.batchrenamer#rename/v2.0.0
```

- `?path=` specifies the exact package directory to import.
- `#rename/v2.0.0` is the scoped tag ensuring independent version control for each package in this repository.
- *(Please replace `BinhouOuO/binhou-tools.git` with your actual repository URL if necessary)*

## Git Tagging Rules (For Maintenance)

Git tags are shared repository-wide. To maintain separate versioning for each tool, tags must include the package-specific prefix:

- Batch Renamer: `rename/v2.0.0`, `rename/v2.1.0` ...
