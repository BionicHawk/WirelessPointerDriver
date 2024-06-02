namespace WirelessPointerDriver;

public static class CommandSystem
{

    public static void InterpretCommand(string command)
    {
        string[] splitCommand = command.Split(' ');
        string petition = splitCommand[0];

        switch (petition)
        {
            case "/set_pos":
                if (splitCommand.Length != 4)
                {
                    Console.WriteLine("Command lacks arguments {x:number} {y:number} {z:number}");
                    break;
                }
                var positionsList = splitCommand.ToList();
                var position = new Position3D(positionsList.GetRange(1, 3));
                MouseController.MoveMouse(position);
                break;
            case "/primary_button":
                MouseController.Click();
                break;
            case "/secondary_button":
                MouseController.SecondaryClick();
                break;
            case "/set_sensibility":
                if (splitCommand.Length != 2)
                {
                    Console.WriteLine("Command lacks arguments {sensibility:number[min:1, max:200]}");
                    break;
                }
                if (!ushort.TryParse(splitCommand[1], out ushort value))
                {
                    Console.WriteLine("The value given in /set_sensibility is not an unsigned short number");
                    break;
                }
                MouseController.Sensibility = value;
                break;
            default:
                break;
        }
    }

}