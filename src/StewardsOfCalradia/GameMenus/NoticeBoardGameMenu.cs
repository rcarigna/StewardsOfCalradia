using System.Linq;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.GameMenus;
using TaleWorlds.Library;

namespace StewardsOfCalradia.GameMenus
{
  public sealed class NoticeBoardGameMenu
  {
    private readonly NoticeBoardCampaignBehavior _noticeBoardBehavior;

    public NoticeBoardGameMenu(NoticeBoardCampaignBehavior noticeBoardBehavior)
    {
      _noticeBoardBehavior = noticeBoardBehavior;
    }

    public void RegisterMenus(CampaignGameStarter gameStarter)
    {
      AddNoticeBoardMenu(gameStarter);
    }

    private void AddNoticeBoardMenu(CampaignGameStarter gameStarter)
    {
      gameStarter.AddGameMenu(
        "notice_board",
        "Notice Board",
        null,
        GameMenu.MenuOverlayType.SettlementWithBoth,
        GameMenu.MenuFlags.None,
        null
      );
      gameStarter.AddGameMenuOption(
        "notice_board",
        "notice_board_back",
        "Return to town",
        new GameMenuOption.OnConditionDelegate(NoticeBoardBackCondition),
        new GameMenuOption.OnConsequenceDelegate(NoticeBoardBackConsequence),
        true,
        -1,
        false,
        null
      );
    }

    public void EnsureTownOption(CampaignGameStarter gameStarter, GameMenu townMenu)
    {
      int insertIndex = GetTownNoticeBoardInsertIndex(townMenu);

      gameStarter.AddGameMenuOption(
        "town",
        "notice_board",
        "Visit the notice board",
        new GameMenuOption.OnConditionDelegate(NoticeBoardCondition),
        new GameMenuOption.OnConsequenceDelegate(NoticeBoardConsequence),
        false,
        insertIndex,
        false,
        _noticeBoardBehavior
      );
    }

    private static int GetTownNoticeBoardInsertIndex(GameMenu townMenu)
    {
      var options = townMenu.MenuOptions.ToList();
      int leaveIndex = options.FindIndex(option => option.IsLeave || option.IdString == "town_leave");

      return leaveIndex >= 0 ? leaveIndex : -1;
    }

    private static bool NoticeBoardInitialization(MenuCallbackArgs args)
    {
      Debug.Print("Initializing Notice Board menu.");
      return true;
    }

    private bool NoticeBoardCondition(MenuCallbackArgs args)
    {
      Debug.Print("Checking Notice Board condition.");
      args.optionLeaveType = GameMenuOption.LeaveType.Continue;
      return _noticeBoardBehavior != null;
    }

    private void NoticeBoardConsequence(MenuCallbackArgs args)
    {
      Debug.Print("Switching to Notice Board menu.");
      GameMenu.SwitchToMenu("notice_board");
    }

    private bool NoticeBoardBackCondition(MenuCallbackArgs args)
    {
      Debug.Print("Checking Notice Board back condition.");
      args.optionLeaveType = GameMenuOption.LeaveType.Leave;
      return true;
    }

    private void NoticeBoardBackConsequence(MenuCallbackArgs args)
    {
      Debug.Print("Returning to town menu.");
      GameMenu.SwitchToMenu("town");
    }
  }
}
