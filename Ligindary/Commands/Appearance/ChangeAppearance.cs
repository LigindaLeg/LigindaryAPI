namespace Ligindary.Commands.Appearance;

public class ChangeAppearance : ICommand
{
    public string Command => "ca";

    public string[] Aliases => ["changeappearance", "cappearance"];

    public string Description => "Меняет игроку видимую роль.";

    public bool Execute(ArraySegment<string> args, ICommandSender sender, out string response)
    {
        if (!sender.HasAnyPermission($"lapi.{Command}"))
        {
            response = $"У вас нет разрешения, на выполнение данной команды. Требуется: lapi.{Command}.";
            return false;
        }
        if (args.Count < 2)
        {
            response = "Использование: lapi ca (Айди Игрока) (Роль).";
            return false;
        }
        if (Player.Get(Convert.ToInt32(args.At(0))) == null)
        {
            response = "Не найдено игрока.";
            return false;
        }
        if (!args.At(1).IsRole())
        {
            response = "Введите корректную роль.";
            return false;
        }
        if (!args.At(1).ToRole().IsFpc())
        {
            response = "Роль должна быть человеческой!";
            return false;
        }
        if (!Player.Get(Convert.ToInt32(args.At(0)))!.Role.IsFpc())
        {
            response = "Игрок должен играть за человеческую роль!";
            return false;
        }
        Player.Get(Convert.ToInt32(args.At(0))).Appearance().ChangeAppearance(args.At(1).ToRole());
        response = "Успешно!";
        return true;
    }
}