# Notice Board Extension Points

## Purpose

Propose the safest first extension point for Stewards of Calradia's Notice Board based on the vanilla campaign behavior, game menu, settlement menu, and tournament patterns.

## High-Level Relationships

```text
StewardsOfCalradia.SubModule
  -> MBSubModuleBase.OnGameStart
  -> CampaignGameStarter
  -> AddBehavior(NoticeBoardCampaignBehavior)

NoticeBoardCampaignBehavior
  -> RegisterEvents
  -> CampaignEvents.OnSessionLaunchedEvent
  -> AddGameMenus
  -> NoticeBoardGameMenu.RegisterMenus
  -> AddGameMenuOption("town", "stewards_notice_board", ...)
  -> AddGameMenu("stewards_notice_board", ...)

Vanilla pattern mirrored
  -> PlayerTownVisitCampaignBehavior owns town
  -> TournamentCampaignBehavior augments town_arena
  -> Stewards notice board should augment town without replacing vanilla menus
```

## Confirmed Findings

- Confirmed: Current Stewards module registers `NoticeBoardCampaignBehavior` from `SubModule.OnGameStart`.
- Confirmed: `NoticeBoardCampaignBehavior.RegisterEvents()` subscribes to `CampaignEvents.OnSessionLaunchedEvent`.
- Confirmed: `NoticeBoardCampaignBehavior.AddGameMenus(CampaignGameStarter)` is the right lifecycle point for menu registration.
- Confirmed: `NoticeBoardGameMenu.RegisterMenus(CampaignGameStarter)` currently adds an option to the vanilla `town` menu.
- Confirmed: Vanilla `PlayerTownVisitCampaignBehavior` owns the root `town` menu.
- Confirmed: Vanilla `TournamentCampaignBehavior` shows a safe augmentation pattern: it adds tournament options to `town_arena` and creates a tournament submenu.

## Relationship Map

```text
First safe spike
  -> AddGameMenuOption("town", "stewards_notice_board", ...)
  -> consequence shows placeholder / switches to custom menu

Next safe shape
  -> AddGameMenu("stewards_notice_board", ...)
  -> AddGameMenuOption("stewards_notice_board", "stewards_notice_board_back", ...)
  -> consequence returns to "town"

Later feature layer
  -> NoticeBoardCampaignBehavior owns board state
  -> NoticeBoardGameMenu owns menu registration
  -> issue/contracts/jobs systems provide entries
```

## Relevant Types

- `StewardsOfCalradia.SubModule`
- `StewardsOfCalradia.NoticeBoardCampaignBehavior`
- `StewardsOfCalradia.GameMenus.NoticeBoardGameMenu`
- `TaleWorlds.CampaignSystem.CampaignGameStarter`
- `TaleWorlds.CampaignSystem.CampaignEvents`
- `TaleWorlds.CampaignSystem.GameMenus.GameMenu`
- `TaleWorlds.CampaignSystem.GameMenus.GameMenuOption`
- `TaleWorlds.CampaignSystem.GameMenus.MenuCallbackArgs`

## Extension Points

- Confirmed: The first extension point should be the existing `town` menu.
- Inferred: The first custom option id should be `stewards_notice_board`, not `town_notice_board`, to avoid looking vanilla-owned.
- Inferred: The first custom submenu id should also be `stewards_notice_board` or a clear variant such as `stewards_notice_board_menu`.
- Inferred: `NoticeBoardCampaignBehavior` should own campaign state and event registration.
- Inferred: `NoticeBoardGameMenu` should own menu and option registration.
- Inferred: Condition delegates should stay simple at first: set `optionLeaveType`, return true, and later add gates for settlement type, player state, or board availability.
- Inferred: Consequence delegates should either switch to the custom menu or call a behavior method that opens the board.

## Out Of Scope For First Spike

- Generating real contracts/jobs.
- Persisting notice board entries.
- Integrating with tournament rewards, settlement economics, crime, or workshops.
- Replacing vanilla town menus.
- Adding village/castle notice boards before the town board is proven.
- Complex condition gating beyond visibility in the root town menu.

## Open Questions

- Open Question: Should the first submenu id be `stewards_notice_board` or `stewards_notice_board_menu`.
- Open Question: Should notice entries be campaign behavior state, save data, generated on menu init, or generated daily.
- Open Question: Should villages and castles get their own board later, or should the system start town-only.
- Open Question: Which menu overlay gives the best vanilla-feeling notice board presentation.
- Open Question: Whether `relatedObject` should point to the behavior for later cleanup/removal or remain null for static registration.

## Source Paths

- `src/StewardsOfCalradia/SubModule.cs`
- `src/StewardsOfCalradia/CampaignBehaviors/NoticeBoardCampaignBehaviors.cs`
- `src/StewardsOfCalradia/GameMenus/NoticeBoardGameMenu.cs`
- `docs/taleworlds-api/TaleWorlds.CampaignSystem.md`
- `docs/discovery/campaign-behaviors.md`
- `docs/discovery/game-menus.md`
- `docs/discovery/settlement-menus.md`
- `docs/discovery/tournaments.md`

