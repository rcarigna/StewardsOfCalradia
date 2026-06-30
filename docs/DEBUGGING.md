# Debugging

This mod is developed in place under a local Bannerlord module directory:

```text
Mount & Blade II Bannerlord/Modules/StewardsOfCalradia
```

## Build

From the repository root:

```powershell
.\scripts\build.ps1
```

If local PowerShell execution policy blocks scripts, run the same helper with:

```powershell
powershell -NoProfile -ExecutionPolicy Bypass -File .\scripts\build.ps1
```

The script validates `SubModule.xml`, checks that the expected TaleWorlds assemblies are available in the local Bannerlord install, and builds `src/StewardsOfCalradia/StewardsOfCalradia.csproj`.

Build output is written to:

```text
bin/Win64_Shipping_Client/
```

## Manual Build

If you prefer to build directly:

```powershell
dotnet build .\src\StewardsOfCalradia\StewardsOfCalradia.csproj
```

## In-Game Check

1. Launch Mount & Blade II: Bannerlord.
2. Enable the `StewardsOfCalradia` module.
3. Start or load a campaign.
4. Confirm that the debug message appears when the session launches.

The current smoke-test message is:

```text
Stewards of Calradia session launched.
```

## Manifest Check

To validate the module manifest without building:

```powershell
.\scripts\validate-manifest.ps1
```

If script execution is blocked:

```powershell
powershell -NoProfile -ExecutionPolicy Bypass -File .\scripts\validate-manifest.ps1
```

This checks the module id, DLL name, submodule class, and required Bannerlord dependencies.
