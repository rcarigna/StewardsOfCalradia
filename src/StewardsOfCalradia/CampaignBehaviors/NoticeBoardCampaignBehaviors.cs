using StewardsOfCalradia.GameMenus;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.GameMenus;
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
