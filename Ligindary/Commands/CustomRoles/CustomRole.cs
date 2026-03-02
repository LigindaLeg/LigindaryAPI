using System.Linq;
using Ligindary.API.Features.NBT;

namespace Ligindary.Commands.CustomRoles;

public class CustomRole : ICommand
{
    public string Command => "cr";

    public string[] Aliases => ["customrole", "crole"];

    public string Description => "Команда управления кастомными ролями.";

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
                       "lapi cr give (ID Игрока) (Роль) - Выдаёт роль игроку.\n" +
                       "lapi ci list - Выводит список всех доступных кастомных ролей.\n" +
                       "lapi ci rm (ID Игрока) (Роль) - Забирает кастомную роль у игрока.";
            return false;
        }
        switch (args.At(0))
        {
            case "give":
            {
                const string usage = "Использование: lapi cr give (ID Игрока) (Роль)";
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

                string roleName = args.At(2);
                if (!roleName.IsCustomRole())
                {
                    response = $"Некорректная роль: '{roleName}'.";
                    return false;
                }

                player.GiveCustomRole(roleName.ToCustomRole());
                response = $"Роль '{roleName}' успешна выдана игроку {player.Nickname}.";
                break;
            }
            case "list":
            {
                if (!Lists.CustomRoles.Any())
                {
                    response = "Доступные роли отсутствуют.";
                    break;
                }
                var roleLines = Lists.CustomRoles.Select(role => $"{role.Name} - {role.Description}");
                response = "Доступные роли:\n" + string.Join("\n", roleLines);
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
                
                string roleName = args.At(2);
                if (!roleName.IsCustomRole())
                {
                    response = $"Несуществующий предмет: '{roleName}'.";
                    return false;
                }
                var role = roleName.ToCustomRole();

                if (!NbtTagStorage.GetTag<bool>(player1, "HasCustomRole"))
                {
                    response = "У игрока нет кастомной роли.";
                    return false;
                }
                player1.RemoveCustomRole(role);
                response = $"Роль '{role.Name}' успешно снята с игрока {player1.Nickname}.";
                break;
            }
            default:
                response = "Использование: \n" +
                           "lapi cr give (ID Игрока) (Роль) - Выдаёт роль игроку.\n" +
                           "lapi ci list - Выводит список всех доступных кастомных ролей.\n" +
                           "lapi ci rm (ID Игрока) (Роль) - Забирает кастомную роль у игрока.";
                return false;
        }
        
        return true;
    }
}