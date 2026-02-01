namespace Ligindary.API.Interfaces;

public interface CustomRole
{
    /// <summary>
    /// Название роли.
    /// </summary>
    string Name { get; }
    
    /// <summary>
    /// Описание роли.
    /// </summary>
    string Description { get;}

    /// <summary>
    /// Вызывается при спавне.
    /// </summary>
    void OnGive(Player player, float duration);
    
    /// <summary>
    /// Вызывается при деспавне.
    /// </summary>
    void OnRemove(Player player);
    
}