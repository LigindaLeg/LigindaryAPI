using Ligindary.API.Spawning;
using Ligindary.API;

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
    /// Имя игрока за эту роль.
    /// </summary>
    string DisplayName { get;}
    
    /// <summary>
    /// Кастом инфо игрока за эту роль.
    /// </summary>
    string CustomInfo { get;}
    
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
    /// Локация для спавна.
    /// </summary>
    SpawnLocation SpawnLocation { get;}
    
    /// <summary>
    /// Шанс спавна.
    /// </summary>
    int SpawnChance { get;}
    
    /// <summary>
    /// Флаги для спавна.
    /// </summary>
    RoleSpawnFlags SpawnFlags { get;}
    
    /// <summary>
    /// Вызывается при спавне.
    /// </summary>
    void OnGive(Player player);
    
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