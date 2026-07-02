# Quality Checks

Use this checklist before considering a code change ready. The mod depends on Bannerlord runtime state, so local build checks and focused in-game smoke tests are the main quality gates for now.

## Build Gate

Run from the repository root:

```powershell
powershell -NoProfile -ExecutionPolicy Bypass -File .\scripts\build.ps1
```

The build must finish with:

```text
0 Warning(s)
0 Error(s)
```

The build script also validates `SubModule.xml` unless `-SkipManifestValidation` is passed.

## Manifest-Only Gate

For manifest-only edits, run:

```powershell
powershell -NoProfile -ExecutionPolicy Bypass -File .\scripts\validate-manifest.ps1
```

This checks the module id, DLL name, submodule class, and required Bannerlord dependencies.

## Notice Board Smoke Test

After building, launch Mount & Blade II: Bannerlord with the `StewardsOfCalradia` module enabled.

1. Start or load a campaign.
2. Enter a town.
3. Confirm the town menu includes `Visit the notice board`.
4. Open the Notice Board.
5. Select `View active tournaments`.
6. If tournaments are active, confirm each listed town opens its settlement encyclopedia page.
7. If no tournaments are active, confirm the menu shows that no active tournaments are posted.
8. Return from the tournament list to the Notice Board.
9. Return from the Notice Board to the town menu.

## Log Check

If something does not appear in game, check the latest Bannerlord logs under:

```text
C:\ProgramData\Mount and Blade II Bannerlord\logs
```

Look for `StewardsOfCalradia` and Notice Board debug messages such as:

```text
Adding game menus for Notice Board.
Positioning Notice Board town menu option.
```

## Test Policy

Do not add a mocked TaleWorlds test harness until there is enough non-engine logic to justify it. Prefer extracting plain C# logic when it naturally appears, then add tests around that extracted logic instead of mocking Bannerlord runtime objects directly.
