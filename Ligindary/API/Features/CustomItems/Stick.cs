using InventorySystem.Items.Jailbird;
using JailbirdItem = LabApi.Features.Wrappers.JailbirdItem;

namespace Ligindary.API.Features.CustomItems;

public class Stick : CustomItem
{
    public string Name => "Дубинка";

    public string Description => "Классическая полицейская дубинка выполненная в чёрном цвете.";

    public ItemType ItemType => ItemType.Jailbird;

    public void OnGive(Player player)
    {
        
    }

    public void OnRemove(Player player)
    {
        
    }

    public void RegisterEvents()
    {
        PlayerEvents.Hurting += Hurting;
    }

    public void UnregisterEvents()
    {
        PlayerEvents.Hurting -= Hurting;
    }

    private void Hurting(PlayerHurtingEventArgs args)
    {
        if (args.Attacker == null)
            return;
        if (args.Attacker.CurrentItem.CustomItem() != this)
            return;
        args.IsAllowed = false;
        args.Attacker.SendHitMarker();
        args.Player.Damage(1f, args.Attacker);
    }
}