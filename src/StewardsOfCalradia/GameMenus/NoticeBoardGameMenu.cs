using System.Collections.Generic;
using System.Linq;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.GameMenus;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace StewardsOfCalradia.GameMenus
{
  public sealed class NoticeBoardGameMenu
  {
    private readonly NoticeBoardCampaignBehavior _noticeBoardBehavior;
    private readonly object _activeTournamentOptionMarker = new object();
    private CampaignGameStarter _gameStarter;

    public NoticeBoardGameMenu(NoticeBoardCampaignBehavior noticeBoardBehavior)
    {
      _noticeBoardBehavior = noticeBoardBehavior;
    }

    public void RegisterMenus(CampaignGameStarter gameStarter)
    {
      _gameStarter = gameStarter;
      AddNoticeBoardMenu(gameStarter);
    }

    private void AddNoticeBoardMenu(CampaignGameStarter gameStarter)
    {
      gameStarter.AddGameMenu(
        NoticeBoardMenuIds.NoticeBoard,
        "Notice Board",
        null,
        GameMenu.MenuOverlayType.SettlementWithBoth,
        GameMenu.MenuFlags.None,
        null
      );
      gameStarter.AddGameMenuOption(
        NoticeBoardMenuIds.NoticeBoard,
        NoticeBoardOptionIds.Tournaments,
        "View active tournaments",
        new GameMenuOption.OnConditionDelegate(ActiveTournamentsCondition),
        new GameMenuOption.OnConsequenceDelegate(ActiveTournamentsConsequence),
        false,
        -1,
        false,
        null
      );
      gameStarter.AddGameMenuOption(
        NoticeBoardMenuIds.NoticeBoard,
        NoticeBoardOptionIds.NoticeBoardBack,
        "Return to town",
        new GameMenuOption.OnConditionDelegate(NoticeBoardBackCondition),
        new GameMenuOption.OnConsequenceDelegate(NoticeBoardBackConsequence),
        true,
        -1,
        false,
        null
      );
      gameStarter.AddGameMenu(
        NoticeBoardMenuIds.Tournaments,
        "{" + NoticeBoardTextVariables.ActiveTournaments + "}",
        new OnInitDelegate(ActiveTournamentsInitialization),
        GameMenu.MenuOverlayType.SettlementWithBoth,
        GameMenu.MenuFlags.None,
        null
      );
      gameStarter.AddGameMenuOption(
        NoticeBoardMenuIds.Tournaments,
        NoticeBoardOptionIds.TournamentsBack,
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
        NoticeBoardMenuIds.Town,
        NoticeBoardOptionIds.NoticeBoard,
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
        option.IsLeave || option.IdString == NoticeBoardMenuIds.TownLeave
      );

      return leaveIndex >= 0 ? leaveIndex : -1;
    }

    private void ActiveTournamentsInitialization(MenuCallbackArgs args)
    {
      IReadOnlyList<NoticeBoardNotice> notices =
        TournamentNoticeProvider.GetActiveTournamentNotices();
      RefreshNoticeOptions(notices);

      string text =
        notices.Count == 0
          ? "No active tournaments are posted right now."
          : "Tournament notices have been posted from the following towns.";

      MBTextManager.SetTextVariable(NoticeBoardTextVariables.ActiveTournaments, text);
    }

    private void RefreshNoticeOptions(IReadOnlyList<NoticeBoardNotice> notices)
    {
      Campaign.Current.GameMenuManager.RemoveRelatedGameMenuOptions(_activeTournamentOptionMarker);

      if (_gameStarter == null)
      {
        return;
      }

      GameMenu tournamentMenu = _gameStarter.GetPresumedGameMenu(NoticeBoardMenuIds.Tournaments);
      int insertIndex = GetLeaveOptionIndex(tournamentMenu);

      foreach (NoticeBoardNotice notice in notices)
      {
        _gameStarter.AddGameMenuOption(
          NoticeBoardMenuIds.Tournaments,
          notice.Id,
          notice.Title,
          args =>
          {
            args.optionLeaveType = GameMenuOption.LeaveType.Continue;
            return true;
          },
          args => notice.Select(args),
          false,
          insertIndex,
          false,
          _activeTournamentOptionMarker
        );

        if (insertIndex >= 0)
        {
          insertIndex++;
        }
      }
    }

    private static int GetLeaveOptionIndex(GameMenu menu)
    {
      if (menu == null)
      {
        return -1;
      }

      List<GameMenuOption> options = menu.MenuOptions.ToList();
      int leaveIndex = options.FindIndex(option => option.IsLeave);

      return leaveIndex >= 0 ? leaveIndex : -1;
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
      GameMenu.SwitchToMenu(NoticeBoardMenuIds.NoticeBoard);
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
      GameMenu.SwitchToMenu(NoticeBoardMenuIds.Town);
    }

    private bool ActiveTournamentsCondition(MenuCallbackArgs args)
    {
      args.optionLeaveType = GameMenuOption.LeaveType.Continue;
      return true;
    }

    private void ActiveTournamentsConsequence(MenuCallbackArgs args)
    {
      GameMenu.SwitchToMenu(NoticeBoardMenuIds.Tournaments);
    }

    private void ActiveTournamentsBackConsequence(MenuCallbackArgs args)
    {
      GameMenu.SwitchToMenu(NoticeBoardMenuIds.NoticeBoard);
    }
  }
}
