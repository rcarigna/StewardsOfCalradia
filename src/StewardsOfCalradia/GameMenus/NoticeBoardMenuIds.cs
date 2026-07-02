namespace StewardsOfCalradia.GameMenus
{
  internal static class NoticeBoardMenuIds
  {
    public const string Town = "town";
    public const string TownLeave = "town_leave";
    public const string NoticeBoard = "notice_board";
    public const string Tournaments = "notice_board_tournaments";
  }

  internal static class NoticeBoardOptionIds
  {
    public const string NoticeBoard = "notice_board";
    public const string NoticeBoardBack = "notice_board_back";
    public const string Tournaments = "notice_board_tournaments";
    public const string TournamentsBack = "notice_board_tournaments_back";

    public static string TournamentNotice(int index)
    {
      return "notice_board_tournament_" + index;
    }
  }

  internal static class NoticeBoardTextVariables
  {
    public const string ActiveTournaments = "SOC_ACTIVE_TOURNAMENTS_TEXT";
  }
}
