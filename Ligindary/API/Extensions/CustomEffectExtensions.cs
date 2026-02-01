namespace Ligindary.Extensions;

public static class CustomEffectExtensions
{
    /// <summary>
    /// Выдаёт эффект игроку.
    /// </summary>
    public static void GiveToPlayer(this CustomEffect effect, Player player, float duration)
    {
        player.GiveCustomEffect(effect, duration);
    }
    
    /// <summary>
    /// Выдаёт эффект всем игрокам.
    /// </summary>
    public static void GiveToAllPlayers(this CustomEffect effect, float duration)
    {
        foreach (var player in Player.List)
        {
            player.GiveCustomEffect(effect, duration);
        }
    }
    
    /// <summary>
    /// Регистрирует эффект.
    /// </summary>
    public static void RegisterEffect(CustomEffect effect)
    {
        Lists.CustomEffects.Add(effect);
    }
}