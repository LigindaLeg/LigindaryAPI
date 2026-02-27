namespace Ligindary.API.Features.CustomItems;

public class NVG : CustomItem
{
    public string Name => "ПНВ";

    public string Description => "Прибор Ночного Видения. Позволяет видеть в темноте.";

    public ItemType ItemType => ItemType.SCP1344;

    public void OnGive(Player player)
    {
        
    }

    public void OnRemove(Player player)
    {
        
    }

    public void RegisterEvents()
    {
        PlayerEvents.UsingItem += UsingItem;
        PlayerEvents.ChangedItem += ChangedItem;
        PlayerEvents.DroppedItem += DroppedItem;
        PlayerEvents.Cuffed += Cuffed;
    }

    public void UnregisterEvents()
    {
        PlayerEvents.UsingItem -= UsingItem;
        PlayerEvents.ChangedItem -= ChangedItem;
        PlayerEvents.DroppedItem -= DroppedItem;
        PlayerEvents.Cuffed -= Cuffed;
    }

    private void UsingItem(PlayerUsingItemEventArgs args)
    {
        if (args.UsableItem.CustomItem() != this)
            return;
        args.IsAllowed = false;
        args.Player.CurrentItem = null;
        args.Player.EnableEffect<NightVision>(7);
        args.Player.EnableEffect<FogControl>(7);
        args.Player.EnableEffect<Blindness>(30);
    }

    private void ChangedItem(PlayerChangedItemEventArgs args)
    {
        if (args.NewItem == null)
            return;
        if (args.NewItem!.CustomItem() != this)
            return;
        args.Player.DisableEffect<NightVision>();
        args.Player.DisableEffect<FogControl>();
        args.Player.DisableEffect<Blindness>();
    }

    private void DroppedItem(PlayerDroppedItemEventArgs args)
    {
        if (args.Pickup.CustomItem() != this)
            return;
        args.Player.DisableEffect<NightVision>();
        args.Player.DisableEffect<FogControl>();
        args.Player.DisableEffect<Blindness>();
    }

    private void Cuffed(PlayerCuffedEventArgs args)
    {
        args.Player.DisableEffect<NightVision>();
        args.Player.DisableEffect<FogControl>();
        args.Player.DisableEffect<Blindness>();
    }
}