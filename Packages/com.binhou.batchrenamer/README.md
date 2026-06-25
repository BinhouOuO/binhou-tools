# Batch Renamer (com.binhou.batchrenamer)

A simple, lightweight Unity editor tool to batch rename multiple GameObjects in the Hierarchy window or Asset files in the Project window.

## Installation

### Via OpenUPM (Recommended)
Add a Scoped Registry in your Unity project settings (**Edit -> Project Settings -> Package Manager -> Scoped Registries**):

- **Name**: `Binhou`
- **URL**: `https://package.openupm.com`
- **Scope(s)**: `com.binhou`

Then open the Unity Package Manager (**Window -> Package Manager**), switch to **My Registries**, select **Batch Renamer**, and click **Install**.

### Via Git URL
Open the Unity Package Manager (Window -> Package Manager), click the "+" icon, select **Add package from git URL**, and enter:

```
https://github.com/BinhouOuO/binhou-tools.git?path=/Packages/com.binhou.batchrenamer#rename/v2.1.0
```
*(Please replace `BinhouOuO/binhou-tools.git` with your actual repository URL if necessary)*

### Via Disk (Local Development)
Open the Package Manager, click the "+" icon, select **Add package from disk**, and choose the `package.json` file in this directory.

## Features

- **Naming Settings**:
  - **Prefix**: The base name prefix for renamed objects.
  - **Separator**: Custom separator string (e.g., `_` or `-`).
  - **Index Mode**: 
    - **Numeric**: Sequential numbers (e.g., 1, 2, 3...).
    - **Alphabetic**: Sequential alphabets (e.g., A, B, C... Z, AA, AB...).
  - **Digit Padding**: Zero padding formatting for numeric indexes (e.g., `01`, `001`).
  - **Sort Order**:
    - **Selection Order**: Assigns indexes based on the order objects were manually clicked/selected.
    - **Alpha Ascending**: Order objects alphabetically from A to Z before renaming.
    - **Alpha Descending**: Order objects alphabetically from Z to A before renaming.
- **Instant Preview**: Shows the original name and the resulting name side-by-side in real-time.
- **Undo Support**: Fully integrated with Unity's standard Undo system. You can press `Ctrl+Z` (or `Cmd+Z` on macOS) to revert any rename operation.

## Usage

1. Open the tool window via **Tools -> Batch Renamer** (or use the shortcut: `Ctrl+Shift+R` / macOS: `Cmd+Shift+R`).
2. Select the targets you want to rename in either the **Hierarchy** or **Project** panel.
3. Configure your naming pattern under **Naming Settings**.
4. Check the preview list in the window.
5. Click **Rename** to apply.