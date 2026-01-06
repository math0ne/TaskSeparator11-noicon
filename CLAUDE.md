# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

Task Separator 11 is a Windows 11 utility that adds visual separators to the Windows taskbar, allowing users to organize pinned applications into groups. The solution contains three projects:

1. **TaskSplitter11** - Main application that creates new splitter instances
2. **Splitter** - The actual splitter executable that appears on the taskbar
3. **Installer** - Visual Studio installer project (.vdproj)

## Prerequisites

To build and develop this project, you need:

- **.NET 6.0 SDK** - Download from https://dotnet.microsoft.com/download/dotnet/6.0
  - The SDK is required (not just the runtime)
  - Verify installation with: `dotnet --version`
- **Windows OS** - Required for Windows Forms and platform-specific APIs
- **Visual Studio 2019 or later** (optional but recommended)
  - Required for editing Windows Forms designer files
  - Required for building the Installer project (.vdproj)
  - Install with ".NET desktop development" workload

## Architecture

### Two-Application Design

The application uses a dual-executable architecture:

- **TaskSplitter11.exe**: Launcher application that:
  - Copies `Splitter.exe` and its dependencies to `%USERPROFILE%\Documents\TaskSeparator11\Shortcuts\`
  - Creates uniquely-named copies of `Splitter.exe` (using underscore characters: `_.exe`, `__.exe`, etc.)
  - Generates Windows shortcuts (.lnk) to each splitter instance (using invisible separator characters)
  - Launches the splitter with `--gui` flag to show usage instructions
  - Exits immediately after creating a splitter

- **Splitter.exe**: The actual taskbar splitter that:
  - Only shows UI when launched with `--gui` argument (MainForm.cs:78)
  - Otherwise runs silently as a minimal window meant to be pinned to the taskbar
  - Uses DWM (Desktop Window Manager) APIs for drop shadow effects via `DropShadow.cs`

### Key Workflow

1. User runs TaskSplitter11.exe
2. Application ensures Splitter files exist in `Documents\TaskSeparator11\Shortcuts\`
3. Creates a new uniquely-named copy of Splitter.exe
4. Creates a shortcut to that copy with a unique name
5. Launches the shortcut with `--gui` flag
6. Splitter shows instructions: pin to taskbar, drag to position, close window
7. User pins the splitter, creating a visual separator on taskbar

### Unique Naming Strategy

To support multiple splitters, the application generates unique filenames:
- **Executables**: Uses underscores (`_`, `__`, `___`, etc.) - see MainForm.cs:105
- **Shortcuts**: Uses invisible separator character (U+FFFD) - see MainForm.cs:105

## Build Commands

### Building the Solution

```bash
# Build entire solution in Release mode
dotnet build TaskSplitter11.sln -c Release

# Build specific project
dotnet build TaskSplitter11/TaskSplitter11.csproj -c Release
dotnet build Splitter/Splitter.csproj -c Release
```

### Running the Application

```bash
# Run the main launcher (creates a new splitter)
dotnet run --project TaskSplitter11/TaskSplitter11.csproj

# Run a splitter directly with GUI instructions
dotnet run --project Splitter/Splitter.csproj -- --gui
```

## Technology Stack

- **.NET 6.0 Windows Forms** (net6.0-windows)
- **NuGet Package**: WindowsShortcutFactory 1.1.0 (TaskSplitter11 project only)
- **Win32 APIs**:
  - DWM (Desktop Window Manager) for shadow effects
  - Shell32 for ShellExecute

## Important Implementation Details

### File Paths

The application stores splitter instances in:
```
%USERPROFILE%\Documents\TaskSeparator11\Shortcuts\
```

This directory contains:
- Multiple copies of `Splitter.exe` with unique names
- Corresponding `.lnk` shortcuts
- Supporting DLLs (`Splitter.dll`, etc.)
- A `ReadMe.txt` file (must not be removed)

### Windows Forms Designer

Both projects use Windows Forms with designer files:
- `MainForm.Designer.cs` / `FormSplitter.Designer.cs` contain designer-generated code
- `.resx` files contain embedded resources and form layout data
- Do not manually edit designer files - use the Visual Studio designer

### Platform-Specific Code

This is a Windows-only application that relies on:
- Windows shortcuts (.lnk files)
- Windows taskbar pinning behavior
- DWM composition APIs (Windows Vista+)
- Shell execution APIs
