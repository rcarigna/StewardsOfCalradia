using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;

namespace StewardsOfCalradia;

public sealed class SubModule : MBSubModuleBase
{
    protected override void OnGameStart(Game game, IGameStarter gameStarterObject)
    {
        base.OnGameStart(game, gameStarterObject);

        Debug.Print("Stewards of Calradia game started.");

        if (gameStarterObject is CampaignGameStarter campaignGameStarter)
        {
            campaignGameStarter.AddBehavior(new StewardsDebugBehavior());
        }
    }
}

public sealed class StewardsDebugBehavior : CampaignBehaviorBase
{
    public override void RegisterEvents()
    {
        CampaignEvents.OnSessionLaunchedEvent.AddNonSerializedListener(this, OnSessionLaunched);
    }

    public override void SyncData(IDataStore dataStore) { }

    private void OnSessionLaunched(CampaignGameStarter campaignGameStarter)
    {
        Debug.Print("Stewards of Calradia session launched.");

        InformationManager.DisplayMessage(
            new InformationMessage("Stewards of Calradia session launched.")
        );
    }
}
