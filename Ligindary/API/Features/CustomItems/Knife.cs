using InventorySystem.Items.Scp1509;

namespace Ligindary.API.Features.CustomItems;

public class Knife : CustomItem
{
    public string Name => "Нож";

    public string Description => "Просто обычный нож, ничего особенного.";

    public ItemType ItemType => ItemType.SCP1509;

    public void OnGive(Player player)
    {
        
    }

    public void OnRemove(Player player)
    {
        
    }

    public void RegisterEvents()
    {
        PlayerEvents.Dying += Dying;
        PlayerEvents.Hurting += Hurting;
    }

    public void UnregisterEvents()
    {
        PlayerEvents.Dying -= Dying;
        PlayerEvents.Hurting -= Hurting;
    }

    private void Dying(PlayerDyingEventArgs args)
    {
        if (args.Attacker == null)
            return;
        if (args.Attacker!.CurrentItem!.CustomItem() != this)
            return;
        args.IsAllowed = false;
        args.Player.Kill("Ножевые ранения");
    }
    
    private void Hurting(PlayerHurtingEventArgs args)
    {
        if (args.Attacker == null)
            return;
        if (args.Attacker!.CurrentItem!.CustomItem() != this)
            return;
        args.IsAllowed = false;
        args.Attacker.SendHitMarker();
        args.Player.Damage(UnityEngine.Random.Range(10f, 30f), args.Attacker);
    }
}