namespace Ligindary.API.Interfaces;

public interface CustomItem
{
    /// <summary>
    /// Название предмета.
    /// </summary>
    string Name { get; }
    
    /// <summary>
    /// Описание предмета.
    /// </summary>
    string Description { get;}
    
    /// <summary>
    /// Тип Предмета.
    /// </summary>
    ItemType ItemType { get;}
    
    /// <summary>
    /// Вызывается при получении.
    /// </summary>
    void OnGive(Player player);
    
    /// <summary>
    /// Вызывается при потере.
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