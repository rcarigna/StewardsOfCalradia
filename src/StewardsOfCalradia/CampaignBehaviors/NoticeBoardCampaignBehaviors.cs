using System.Collections.Generic;
using StewardsOfCalradia.GameMenus;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.GameMenus;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.CampaignSystem.TournamentGames;
using TaleWorlds.Library;

namespace StewardsOfCalradia;

public sealed class NoticeBoardCampaignBehavior : CampaignBehaviorBase
{
  private CampaignGameStarter _campaignGameStarter;

  public override void RegisterEvents()
  {
    CampaignEvents.OnSessionLaunchedEvent.AddNonSerializedListener(this, AddGameMenus);
    CampaignEvents.BeforeGameMenuOpenedEvent.AddNonSerializedListener(this, BeforeGameMenuOpened);
  }

  private void AddGameMenus(CampaignGameStarter starter)
  {
    Debug.Print("Adding game menus for Notice Board.");
    _campaignGameStarter = starter;
    new NoticeBoardGameMenu(this).RegisterMenus(starter);
  }

  private static GameMenu GetSettlementMenu(CampaignGameStarter starter, string settlementId)
  {
    string[] settlementTypes = { "town", "village", "castle" };

    foreach (string settlementType in settlementTypes)
    {
      GameMenu settlementMenu = starter.GetPresumedGameMenu(settlementType);

      if (settlementMenu != null)
      {
        return settlementMenu;
      }
    }

    Debug.Print($"Settlement menu '{settlementId}' not found.");
    return null;
  }

  public IReadOnlyList<Town> GetTownsWithActiveTournaments()
  {
    List<Town> townsWithTournaments = new List<Town>();

    foreach (Town town in Town.AllTowns)
    {
      TournamentGame tournament = Campaign.Current.TournamentManager.GetTournamentGame(town);

      if (tournament == null)
      {
        continue;
      }

      townsWithTournaments.Add(town);
    }

    return townsWithTournaments;
  }

  private void BeforeGameMenuOpened(MenuCallbackArgs args)
  {
    if (_campaignGameStarter == null || args.MenuContext?.GameMenu == null)
    {
      return;
    }

    GameMenu townMenu = GetSettlementMenu(_campaignGameStarter, "town");

    if (!ReferenceEquals(args.MenuContext.GameMenu, townMenu))
    {
      return;
    }

    Debug.Print("Positioning Notice Board town menu option.");
    Campaign.Current.GameMenuManager.RemoveRelatedGameMenuOptions(this);
    new NoticeBoardGameMenu(this).EnsureTownOption(_campaignGameStarter, townMenu);
  }

  public override void SyncData(IDataStore dataStore) { }
}
