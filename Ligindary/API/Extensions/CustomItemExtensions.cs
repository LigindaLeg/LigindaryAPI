namespace Ligindary.Extensions;

public static class CustomItemExtensions
{
    /// <summary>
    /// Выдаёт предмет игроку.
    /// </summary>
    public static void GiveToPlayer(this CustomItem item, Player player)
    {
        player.GiveCustomItem(item);
    }
    
    /// <summary>
    /// Выдаёт предмет всем игрокам.
    /// </summary>
    public static void GiveToAllPlayers(this CustomItem item)
    {
        foreach (var player in Player.List)
        {
            player.GiveCustomItem(item);
        }
    }
    
    /// <summary>
    /// Регистрирует предмет.
    /// </summary>
    public static void RegisterItem(CustomItem item)
    {
        Lists.CustomItems.Add(item);
    }
}