namespace Ligindary.API.Interfaces;

public interface CustomEffect
{
    /// <summary>
    /// Название эффекта. (На Английском языке).
    /// </summary>
    string Name { get; }
    
    /// <summary>
    /// Описание эффекта.
    /// </summary>
    string Description { get;}

    /// <summary>
    /// Вызывается при выдаче эффекта.
    /// </summary>
    void OnGive(Player player, float duration);
    
    /// <summary>
    /// Вызывается при отмене эффекта.
    /// </summary>
    void OnRemove(Player player);
    
}

class DurationWorkspace
{
    public static IEnumerator<float> Duration(Player player, float duration, CustomEffect effect)
    {
        yield return Timing.WaitForSeconds(duration);
        player.RemoveCustomEffect(effect);
    }
}