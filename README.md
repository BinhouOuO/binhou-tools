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

## Installation

### Via OpenUPM (Recommended)

To install packages via OpenUPM, open Unity and navigate to **Edit -> Project Settings -> Package Manager**. Under **Scoped Registries**, add the following registry:

- **Name**: `Binhou`
- **URL**: `https://package.openupm.com`
- **Scope(s)**: `com.binhou`

After adding the registry, open the Unity Package Manager window (**Window -> Package Manager**), switch the selector to **My Registries**, choose the package, and click **Install**.

### Via Git URL

Open the Package Manager window (**Window -> Package Manager**), click the **"+"** icon, select **Add package from git URL**, and enter:

```
https://github.com/BinhouOuO/binhou-tools.git?path=/Packages/com.binhou.batchrenamer#rename/v2.1.2
```

- `?path=` specifies the exact package directory to import.
- `#rename/v2.0.0` is the scoped tag ensuring independent version control for each package in this repository.
- *(Please replace `BinhouOuO/binhou-tools.git` with your actual repository URL if necessary)*

## Git Tagging Rules (For Maintenance)

Git tags are shared repository-wide. To maintain separate versioning for each tool, tags must include the package-specific prefix:

- Batch Renamer: `rename/v2.0.0`, `rename/v2.1.0` ...
