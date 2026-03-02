namespace Ligindary.Commands;

public class GetLocalRoomPosition : ICommand
{
    public string Command => "getlocalroomposition";

    public string[] Aliases => ["glrp", "localpos"];

    public string Description => "Команда которая преобразует вашу текущую позицию в локальную.";

    public bool Execute(ArraySegment<string> args, ICommandSender sender, out string response)
    {
        var player = Player.Get(sender);
        var p = player.Room.Transform.InverseTransformPoint(player.Position);
        response = $"Комната: {Player.Get(sender).Room.Name} | Локальная Позиция: {p.x},{p.y},{p.z}";
        return true;
    }
}