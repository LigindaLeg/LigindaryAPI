namespace Ligindary.API.Features.NBT;

public static class NbtTagStorage
{
    private static readonly Dictionary<string, Dictionary<string, object>> _data
        = new();

    public static void AddOrSetTag(Player player, string key, object value)
    {
        if (!_data.ContainsKey(player.UserId))
            _data[player.UserId] = new Dictionary<string, object>();

        _data[player.UserId][key] = value;
    }

    public static T GetTag<T>(Player player, string key)
    {
        if (_data.TryGetValue(player.UserId, out var tags) &&
            tags.TryGetValue(key, out var value))
        {
            return (T)value;
        }

        return default;
    }

    public static bool HasTag(Player player, string key)
    {
        return _data.ContainsKey(player.UserId) &&
               _data[player.UserId].ContainsKey(key);
    }

    public static void RemoveTag(Player player, string key)
    {
        if (_data.TryGetValue(player.UserId, out var tags))
            tags.Remove(key);
    }

    public static void Clear(Player player)
    {
        _data.Remove(player.UserId);
    }
}