using System.Collections.Generic;
using System.Linq;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.GameMenus;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.Library;
using TaleWorlds.Localization;

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
        "notice_board_tournaments",
        "View active tournaments",
        new GameMenuOption.OnConditionDelegate(ActiveTournamentsCondition),
        new GameMenuOption.OnConsequenceDelegate(ActiveTournamentsConsequence),
        false,
        -1,
        false,
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
      gameStarter.AddGameMenu(
        "notice_board_tournaments",
        "{SOC_ACTIVE_TOURNAMENTS_TEXT}",
        new OnInitDelegate(ActiveTournamentsInitialization),
        GameMenu.MenuOverlayType.SettlementWithBoth,
        GameMenu.MenuFlags.None,
        null
      );
      gameStarter.AddGameMenuOption(
        "notice_board_tournaments",
        "notice_board_tournaments_back",
        "Return to notice board",
        new GameMenuOption.OnConditionDelegate(NoticeBoardBackCondition),
        new GameMenuOption.OnConsequenceDelegate(ActiveTournamentsBackConsequence),
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
      int leaveIndex = options.FindIndex(option =>
        option.IsLeave || option.IdString == "town_leave"
      );

      return leaveIndex >= 0 ? leaveIndex : -1;
    }

    private bool NoticeBoardInitialization(MenuCallbackArgs args)
    {
      Debug.Print("Initializing Notice Board menu.");
      ActiveTournamentsInitialization(args);
      return true;
    }

    private void ActiveTournamentsInitialization(MenuCallbackArgs args)
    {
      IReadOnlyList<Town> towns = _noticeBoardBehavior.GetTownsWithActiveTournaments();

      string text =
        towns.Count == 0
          ? "No active tournaments are posted right now."
          : "Active tournaments are posted in: "
            + string.Join(", ", towns.Select(town => town.Name.ToString()));

      MBTextManager.SetTextVariable("SOC_ACTIVE_TOURNAMENTS_TEXT", text);
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

    private bool ActiveTournamentsCondition(MenuCallbackArgs args)
    {
      args.optionLeaveType = GameMenuOption.LeaveType.Continue;
      return true;
    }

    private void ActiveTournamentsConsequence(MenuCallbackArgs args)
    {
      GameMenu.SwitchToMenu("notice_board_tournaments");
    }

    private void ActiveTournamentsBackConsequence(MenuCallbackArgs args)
    {
      GameMenu.SwitchToMenu("notice_board");
    }

    public IReadOnlyList<Town> GetActiveTournamentTowns()
    {
      return _noticeBoardBehavior.GetTownsWithActiveTournaments();
    }
  }
}
