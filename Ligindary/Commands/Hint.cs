using System.Linq;

namespace Ligindary.Commands;

[CommandHandler(typeof(RemoteAdminCommandHandler))]
public class Hint : ICommand
{
    public string Command => "hint";

    public string[] Aliases => [];

    public string Description => "Отправляет хинт игроку";

    public bool Execute(ArraySegment<string> args, ICommandSender sender, out string response)
    {
        if (!sender.HasAnyPermission($"lapi.{Command}"))
        {
            response = $"У вас нет разрешения, на выполнение данной команды. Требуется: lapi.{Command}.";
            return false;
        }
        if (args.Count < 5)
        {
            response = "Использование: hint (ID Игрока) (Длительность) (X) (Y) (Текст)";
            return false;
        }
        if (!int.TryParse(args.At(0), out int playerId))
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
        
        if (!int.TryParse(args.At(1), out int duration))
        {
            response = "Некорректная длительность. Принимается только целое число.";
            return false;
        }
        
        if (!float.TryParse(args.At(2), out float xCoord))
        {
            response = "Некорректная X Координата. Принимаются только дробные и целые числа.";
            return false;
        }
        
        if (!float.TryParse(args.At(3), out float yCoord))
        {
            response = "Некорректная Y Координата. Принимаются только дробные и целые числа.";
            return false;
        }
        
        string text = args.Count > 4 ? string.Join(" ", args.Skip(4)) : null;
        
        player.Hint(text, duration, yCoord, xCoord);
        response = "Успешно!";
        return true;
    }
}