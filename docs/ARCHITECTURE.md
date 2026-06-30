# Architecture

Stewards of Calradia is an early-stage Mount & Blade II: Bannerlord module. The current architecture is intentionally small while the project proves out loading, debugging, and basic campaign integration.

## Module Entry Point

`SubModule.xml` declares the module identity, Bannerlord dependencies, XML data registrations, and the managed entry point:

```text
StewardsOfCalradia.SubModule
```

The entry point lives in `src/StewardsOfCalradia/SubModule.cs` and derives from `MBSubModuleBase`.

## Campaign Integration

`SubModule.OnGameStart` registers campaign behavior when the game starter is a `CampaignGameStarter`. The current `StewardsDebugBehavior` is a foundation behavior that proves the mod can subscribe to campaign events and display an in-game message.

Future campaign systems should usually enter through small `CampaignBehaviorBase` implementations. Keep each behavior focused on one system or responsibility so later milestones can grow without turning the submodule into a catch-all coordinator.

## Project Layout

```text
SubModule.xml                         Bannerlord module manifest
src/StewardsOfCalradia/               C# source project
ModuleData/                           Bannerlord XML data
docs/                                 Project planning and development notes
scripts/                              Local development guardrails
bin/Win64_Shipping_Client/            Local build output
```

## Dependency Policy

The project references TaleWorlds assemblies from the local Bannerlord install and does not currently use NuGet package dependencies. New dependencies should be rare, intentional, and justified by the feature they support.

## Nullable Policy

Nullable reference types are currently disabled in the C# project. This is intentional for the foundation phase while integrating with Bannerlord APIs. If nullable is enabled later, it should be done as a focused migration rather than mixed into gameplay work.
