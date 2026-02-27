using System.Linq;

namespace Ligindary.Commands.CustomItem;

public class CustomItem : ICommand
{
    public string Command => "ci";

    public string[] Aliases => ["customitem", "citem"];

    public string Description => "Команда управления кастомными предметами.";

    public bool Execute(ArraySegment<string> args, ICommandSender sender, out string response)
    {
        if (!sender.HasAnyPermission($"lapi.{Command}"))
        {
            response = $"У вас нет разрешения, на выполнение данной команды. Требуется: lapi.{Command}.";
            return false;
        }
        if (args.Count < 1)
        {
            response = "Использование: \n" +
                       "lapi ci give (ID Игрока) (Предмет) - Выдаёт предмет игроку.\n" +
                       "lapi ci list - Выводит список всех доступных кастомных предметов.\n" +
                       "lapi ci rm (ID Игрока) (Предмет) - Забирает кастомный предмет у игрока.";
            return false;
        }
        switch (args.At(0))
        {
            case "give":
            {
                const string usage = "Использование: lapi ci give (ID Игрока) (Предмет)";
                if (args.Count < 3)
                {
                    response = usage;
                    return false;
                }

                if (!int.TryParse(args.At(1), out int playerId))
                {
                    response = "Некорректный ID игрока. ID должен быть целым числом.";
                    return false;
                }

                var player = Player.Get(playerId);
                if (player == null)
                {
                    response = $"Игрок с ID {playerId} не найден.";
                    return false;
                }

                string itemName = args.At(2);
                if (!itemName.IsCustomItem())
                {
                    response = $"Некорректный предмет: '{itemName}'.";
                    return false;
                }

                player.GiveCustomItem(itemName.ToCustomItem());
                response = $"Предмет '{itemName}' успешно выдан игроку {player.Nickname}.";
                break;
            }
            case "list":
            {
                if (!Lists.CustomItems.Any())
                {
                    response = "Доступные эффекты отсутствуют.";
                    break;
                }
                var itemLines = Lists.CustomItems.Select(item => $"{item.Name} - {item.Description}");
                response = "Доступные эффекты:\n" + string.Join("\n", itemLines);
                break;
            }
            case "rm" or "remove":
            {
                const string usage1 = "Использование: lapi ci rm (ID Игрока) (Предмет)";
                if (args.Count < 3)
                {
                    response = usage1;
                    return false;
                }

                if (!int.TryParse(args.At(1), out int playerId1))
                {
                    response = "Некорректный ID игрока. ID должен быть целым числом.";
                    return false;
                }

                var player1 = Player.Get(playerId1);
                if (player1 == null)
                {
                    response = $"Игрок с ID {playerId1} не найден.";
                    return false;
                }
                
                string itemName = args.At(2);
                if (!itemName.IsCustomItem())
                {
                    response = $"Несуществующий предмет: '{itemName}'.";
                    return false;
                }
                var item = itemName.ToCustomItem();
                player1.RemoveCustomItem(item);
                response = $"Предмет '{item.Name}' успешно снят с игрока {player1.Nickname}.";
                break;
            }
            default:
                response = "Использование: \n" +
                           "lapi ci give (ID Игрока) (Предмет) - Выдаёт предмет игроку.\n" +
                           "lapi ci list - Выводит список всех доступных кастомных предметов.\n" +
                           "lapi ci rm (ID Игрока) (Предмет) - Забирает кастомный предмет у игрока.";
                return false;
        }
        
        return true;
    }
}