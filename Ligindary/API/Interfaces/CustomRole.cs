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
    /// Тип Роли.
    /// </summary>
    RoleTypeId RoleType { get;}
    
    /// <summary>
    /// Кол-во здоровья.
    /// </summary>
    ushort Health { get;}
    
    /// <summary>
    /// Кол-во максимального здоровья.
    /// </summary>
    ushort MaxHealth { get;}
    
    /// <summary>
    /// Инвентарь.
    /// </summary>
    ItemType[] Inventory { get;}
    
    /// <summary>
    /// Кол-во щита.
    /// </summary>
    ushort AltHealth { get;}
    
    /// <summary>
    /// Вызывается при спавне.
    /// </summary>
    void OnGive(Player player, float duration);
    
    /// <summary>
    /// Вызывается при деспавне.
    /// </summary>
    void OnRemove(Player player);
    
    /// <summary>
    /// Регистрирует ивенты.
    /// </summary>
    void RegisterEvents();
    
    /// <summary>
    /// Дерегистрирует ивенты.
    /// </summary>
    void UnregisterEvents();
}