# Contributing to Stewards of Calradia

Stewards of Calradia is a public, early-development Bannerlord mod. Contributions are welcome, but the project is still in its foundations phase, so small, focused changes are easiest to review and maintain.

## Project Status

The mod is currently in Phase 0: proving out module loading, DLL loading, campaign behavior registration, and a reliable local debugging workflow. Larger gameplay systems should stay aligned with the design pillars in [README.md](README.md) and the phased plan in [docs/ROADMAP.md](docs/ROADMAP.md).

## Local Development

This repository is expected to live inside a local Mount & Blade II: Bannerlord module directory:

```text
Mount & Blade II Bannerlord/Modules/StewardsOfCalradia
```

The C# project targets `.NET Framework 4.7.2`, uses C# 10, and references TaleWorlds assemblies from the local Bannerlord install. Build output is written to:

```text
bin/Win64_Shipping_Client/
```

To build the project from the repository root:

```powershell
.\scripts\build.ps1
```

If PowerShell blocks local scripts, run:

```powershell
powershell -NoProfile -ExecutionPolicy Bypass -File .\scripts\build.ps1
```

To validate the module manifest without building:

```powershell
.\scripts\validate-manifest.ps1
```

You can also build the project directly:

```powershell
dotnet build .\src\StewardsOfCalradia\StewardsOfCalradia.csproj
```

After building, launch Bannerlord with the `StewardsOfCalradia` module enabled and verify the behavior in game. Additional debugging notes live in [docs/DEBUGGING.md](docs/DEBUGGING.md).

## Contribution Guidelines

- Keep changes small and tied to one purpose.
- Prefer clear, boring code over clever abstractions.
- Preserve the module identity in `SubModule.xml` unless the change is specifically about packaging or release metadata.
- Keep Bannerlord-generated files, build outputs, logs, and local IDE state out of commits.
- Update documentation when behavior, setup, or project direction changes.
- Avoid adding new dependencies unless they are clearly necessary for the mod.
- Follow the formatting defaults in [.editorconfig](.editorconfig).

## Pull Requests

When opening a pull request, include:

- A short summary of what changed.
- The reason for the change.
- How you tested it, including whether Bannerlord was launched locally.
- Any limitations, follow-up work, or known risks.

For gameplay or architecture changes, mention which roadmap milestone or design pillar the work supports.

## Current Priorities

Before proposing larger systems, check [docs/ROADMAP.md](docs/ROADMAP.md). Near-term work should support the foundation and observation milestones rather than jumping straight to full institution, registry, or simulation features.
