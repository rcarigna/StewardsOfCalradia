using System.Collections.Generic;
using System.Linq;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.GameMenus;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.CampaignSystem.TournamentGames;
using TaleWorlds.Library;

namespace StewardsOfCalradia.GameMenus
{
  internal static class TournamentNoticeProvider
  {
    public static IReadOnlyList<NoticeBoardNotice> GetActiveTournamentNotices()
    {
      List<NoticeBoardNotice> notices = new List<NoticeBoardNotice>();
      int index = 0;

      foreach (Town town in GetTownsWithActiveTournaments().OrderBy(town => town.Name.ToString()))
      {
        Town tournamentTown = town;

        notices.Add(
          new NoticeBoardNotice(
            NoticeBoardOptionIds.TournamentNotice(index),
            tournamentTown.Name.ToString(),
            args => OpenTournamentSettlementPage(args, tournamentTown)
          )
        );

        index++;
      }

      return notices;
    }

    private static void OpenTournamentSettlementPage(MenuCallbackArgs args, Town town)
    {
      Debug.Print($"Opening tournament settlement from Notice Board: {town.Name}.");
      Campaign.Current.EncyclopediaManager.GoToLink(town.Settlement.EncyclopediaLink);
    }

    private static List<Town> GetTownsWithActiveTournaments()
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
  }
}
