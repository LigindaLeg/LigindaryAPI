using System.Linq;

namespace Ligindary.Commands.CustomEffect;

public class CustomEffect : ICommand
{
    public string Command => "ce";

    public string[] Aliases => ["customeffect", "ceffect"];

    public string Description => "Команда управления кастомными эффектами.";

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
                       "lapi ce give (ID Игрока) (Эффект) (Длительность) - Выдаёт роль игроку.\n" +
                       "lapi ce list - Выводит список всех доступных кастомных эффектов.\n" +
                       "lapi ce rm (ID Игрока) (Эффект) - Снимает кастомный эффект у игрока.";
            return false;
        }
        switch (args.At(0))
        {
            case "give":
            {
                const string usage = "Использование: lapi ce give (ID Игрока) (Эффект) (Длительность)";
                if (args.Count < 4)
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

                string effectName = args.At(2);
                if (!effectName.IsCustomEffect())
                {
                    response = $"Некорректный эффект: '{effectName}'.";
                    return false;
                }

                if (!int.TryParse(args.At(3), out int duration) || duration <= 0)
                {
                    response = "Некорректная длительность. Она должна быть положительным числом.";
                    return false;
                }

                player.GiveCustomEffect(effectName.ToCustomEffect(), duration);
                response = $"Эффект '{effectName}' успешно выдан игроку {player.Nickname} на {duration} секунд.";
                break;
            }
            case "list":
            {
                if (!Lists.CustomEffects.Any())
                {
                    response = "Доступные эффекты отсутствуют.";
                    break;
                }
                var effectLines = Lists.CustomEffects.Select(effect => $"{effect.Name} - {effect.Description}");
                response = "Доступные эффекты:\n" + string.Join("\n", effectLines);
                break;
            }
            case "rm" or "remove":
            {
                const string usage1 = "Использование: lapi ce rm (ID Игрока) (Эффект)";
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
                
                string effectName = args.At(2);
                if (!effectName.IsCustomEffect())
                {
                    response = $"Несуществующий эффект: '{effectName}'.";
                    return false;
                }
                var effect = effectName.ToCustomEffect();
                player1.RemoveCustomEffect(effect);
                response = $"Эффект '{effect.Name}' успешно снят с игрока {player1.Nickname}.";
                break;
            }
            default:
                response = "Использование: \n" +
                           "lapi ce give (ID Игрока) (Эффект) (Длительность) - Выдаёт роль игроку.\n" +
                           "lapi ce list - Выводит список всех доступных кастомных эффектов.\n" +
                           "lapi ce rm (ID Игрока) (Эффект) - Снимает кастомный эффект у игрока.";
                return false;
        }
        
        return true;
    }
}