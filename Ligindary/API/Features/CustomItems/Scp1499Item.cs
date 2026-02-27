using Ligindary.API.CustomEffects;

namespace Ligindary.API.Features.CustomItems;

public class Scp1499Item : CustomItem
{
    public string Name => "SCP-1499";

    public string Description => "Советский противогаз ГП-5. При надевании он переносит носителя в иное измерение.";

    public ItemType ItemType => ItemType.SCP268;

    public void OnGive(Player player)
    {
        
    }

    public void OnRemove(Player player)
    {
        
    }

    public void RegisterEvents()
    {
        PlayerEvents.UsedItem += UsedItem;
    }

    public void UnregisterEvents()
    {
        PlayerEvents.UsedItem -= UsedItem;
    }

    private void UsedItem(PlayerUsedItemEventArgs args)
    {
        if (args.UsableItem.CustomItem() != this)
            return;
        if (args.Player.HasCustomEffect(new Scp1499()))
        {
            args.Player.DisableEffect<Invisible>();
            return;
        }
        
        args.Player.DisableEffect<Invisible>();
        args.Player.GiveCustomEffect(new Scp1499(), 20f);
    }
}