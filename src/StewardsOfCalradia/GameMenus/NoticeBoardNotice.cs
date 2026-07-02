using System;
using TaleWorlds.CampaignSystem.GameMenus;

namespace StewardsOfCalradia.GameMenus
{
  internal sealed class NoticeBoardNotice
  {
    public NoticeBoardNotice(string id, string title, Action<MenuCallbackArgs> select)
    {
      Id = id;
      Title = title;
      Select = select;
    }

    public string Id { get; }

    public string Title { get; }

    public Action<MenuCallbackArgs> Select { get; }
  }
}
