using System.Linq;

namespace Ligindary.API.Spawning;

/// <summary>
/// Позиция для спавна.
/// </summary>
public class SpawnLocation(string name, RoomOffset offset)
{
    /// <summary>
    /// Имя позиции.
    /// </summary>
    public string Name { get; } = name;

    /// <summary>
    /// Оффсет для спавна.
    /// </summary>
    public RoomOffset Room { get; } = offset;

    /// <summary>
    /// Получает позицию спавна по названию.
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public SpawnLocation Get(string name)
    {
        return (from r in Lists.SpawnLocations where r.Name == name select r).First();
    }
    
    /// <summary>
    /// Получает позицию спавна по оффсету.
    /// </summary>
    /// <param name="offset"></param>
    /// <returns></returns>
    public SpawnLocation Get(RoomOffset offset)
    {
        return (from r in Lists.SpawnLocations where r.Room == offset select r).First();
    }
}