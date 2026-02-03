public class ASCII
{
    public static void PrintName()
    {
        string name = @"
                    __________ ________  _________  ____  __.______________________
                    \______   \\_____  \ \_   ___ \|    |/ _|\_   _____/\__    ___/
                    |       _/ /   |   \/    \  \/|      <   |    __)_   |    |   
                    |    |   \/    |    \     \___|    |  \  |        \  |    |   
                    |____|_  /\_______  /\______  /____|__ \/_______  /  |____|   
                           \/         \/        \/        \/        \/            ";

        Console.WriteLine(name);
    }
    public static void PrintArt()
    {
         string art = @"
        ___________              __     ___________                     __                 
        \__    ___/____    _____|  | __ \__    ___/___________    ____ |  | __ ___________ 
          |    |  \__  \  /  ___/  |/ /   |    |  \_  __ \__  \ _/ ___\|  |/ // __ \_  __ \
          |    |   / __ \_\___ \|    <    |    |   |  | \// __ \\  \___|    <\  ___/|  | \/
          |____|  (____  /____  >__|_ \   |____|   |__|  (____  /\___  >__|_ \\___  >__|   
                       \/     \/     \/                       \/     \/     \/    \/       ";

        Console.WriteLine(art);
    }

    public static void PrintWelcome()
    {
        string welcome = @"
                                                Welcome
                    =========================================================================";
        Console.WriteLine(welcome);
    }

    public static void PrintGoodbye()
    {
        string goodbye = @"
                                            Thank you for using
                                                TaskTracker
                    =========================================================================";
        Console.WriteLine(goodbye);
    }
}