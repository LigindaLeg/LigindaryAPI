namespace Ligindary.Wrappers;

public class Appearance
{
    private static Player _player;

    public Appearance(Player player)
    {
        _player = player;
    }
    /// <summary>
    /// Сменить игроку видимую роль.
    /// </summary>
    public void ChangeAppearance(RoleTypeId role)
    {
        if (role is RoleTypeId.Spectator or RoleTypeId.None or RoleTypeId.Filmmaker or RoleTypeId.Overwatch)
        {
            throw new Exception($"Невозможно присвоить игроку видимую роль {role.ToString()}");
        }

        if (_player.Appearance().CheckAppearance())
        {
            Lists.Appearance[_player] = role;
            return;
        }
        Lists.Appearance.Add(_player, role);
    }
    
    /// <summary>
    /// Получить видимую роль игрока.
    /// </summary>
    public RoleTypeId GetAppearance() => Lists.Appearance[_player];
    
    /// <summary>
    /// Проверить, есть ли у игрока видимая роль.
    /// </summary>
    public bool CheckAppearance() => Lists.Appearance.ContainsKey(_player);
    
    /// <summary>
    /// Убрать видимую роль игрока.
    /// </summary>
    public void ResetAppearance() => Lists.Appearance.Remove(_player);
}