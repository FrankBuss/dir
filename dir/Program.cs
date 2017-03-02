using System;

// there is only one world in this game, so everything is static
class World
{
    // the world is running as long as the universe is alive
    private static bool TheUniverseIsAlive = true;

    // this is called at program start
    public static void BigBang()
    {
        // create rooms
        Root root = new Root();
        Windows windows = new Windows();
        Temp temp = new Temp();

        // link them
        root.Windows = windows;
        root.Temp = temp;
        windows.Root = root;
        temp.Root = root;

        // now enter the start room
        Enter(root);

        // and run the world
        while (TheUniverseIsAlive)
        {
            string key = Console.ReadKey().Key.ToString().ToUpper();
            Console.WriteLine();
            char c = key[0];
            if (!CurrentRoom.KeyboardOverride(c))
            {
                switch (key[0])
                {
                    case 'N':
                        CurrentRoom.OnNorth();
                        break;
                    case 'S':
                        CurrentRoom.OnSouth();
                        break;
                    case 'W':
                        CurrentRoom.OnWest();
                        break;
                    case 'E':
                        CurrentRoom.OnEast();
                        break;
                    default:
                        Info("I don't understand you.");
                        break;
                }
            }
        }
    }

    // enter a new room
    public static void Enter(Room room)
    {
        Console.Clear();
        CurrentRoom = room;
        CurrentRoom.Enter();
        Console.WriteLine();
        CurrentRoom.Directions();
        Console.WriteLine();
    }

    // show some info text with a delay after each line
    public static void Info(String info, ConsoleColor color = ConsoleColor.Cyan)
    {
        Console.ForegroundColor = color;
        Console.WriteLine(info);
        System.Threading.Thread.Sleep(500);
    }

    // this method is called on game over and it stops the universe
    public static void GameOver()
    {
        Console.WriteLine();
        TheUniverseIsAlive = false;
    }

    // the current room where the player is
    public static Room CurrentRoom { get; set; }
}

// base class for all rooms
abstract class Room
{
    // shows some text in derived classes for info about the room
    public abstract void Enter();

    // shows the possible directions and other commands in this room
    public abstract void Directions();

    // overrides the default N/S/W/E commands, return true if a command was executed
    public virtual bool KeyboardOverride(char c)
    {
        return false;
    }

    // default action on invalid directions
    public void InvalidDirection()
    {
        World.Info("You can't go in this direction.");
        Directions();
    }

    // default implemention calls InvalidDirection
    public virtual void OnNorth()
    {
        InvalidDirection();
    }

    // default implemention calls InvalidDirection
    public virtual void OnSouth()
    {
        InvalidDirection();
    }

    // default implemention calls InvalidDirection
    public virtual void OnWest()
    {
        InvalidDirection();
    }

    // default implemention calls InvalidDirection
    public virtual void OnEast()
    {
        InvalidDirection();
    }
}

class Root : Room
{
    public override void Enter()
    {
        World.Info("You are at the root of the harddisk.");
        World.Info("North you you see the Windows folder,");
        World.Info("West the Documents folder");
        World.Info("and East you can see the temp folder.");
    }

    public override void Directions()
    {
        World.Info("Please enter 'N', 'E' or 'W' to move.");
    }

    public override void OnNorth()
    {
        World.Enter(Windows);
    }

    public override void OnWest()
    {
        World.Info("The door is locked, you have to get the private key first.", ConsoleColor.Red);
        World.Info("Please chose another direction.");
    }

    public override void OnEast()
    {
        World.Enter(Temp);
    }

    public Windows Windows{ get; set; }
    public Temp Temp { get; set; }
}

class Windows : Room
{
    public override void Enter()
    {
        World.Info("You enter the Windows folder.");
        World.Info("Suddenly the mighty MS Defender Dragon appears", ConsoleColor.Red);
        World.Info("and casts a firewall on the Windows folder path.", ConsoleColor.Red);
        World.Info("Do you want to use your fire extinguisher?");
    }

    public override void Directions()
    {
        World.Info("Please enter 'S' to move, or 'Y' for yes or 'N' for no.");
    }

    public override bool KeyboardOverride(char c)
    {
        switch (c)
        {
            case 'Y':
                World.Info("The dragon sees what you are trying to do and eats you, game over scammer!", ConsoleColor.Green);
                World.GameOver();
                return true;
            case 'N':
                World.Info("The fire kills you, game over scammer!", ConsoleColor.Green);
                World.GameOver();
                return true;
        }
        return false;
    }

    public override void OnSouth()
    {
                World.Enter(Root);
    }

    public Root Root { get; set; }
}

class Temp : Room
{
    public override void Enter()
    {
        World.Info("You are in the temp folder.");
    }

    public override void Directions()
    {
        World.Info("Please enter 'W' to move or 'L' to look around.");
    }

    public override bool KeyboardOverride(char c)
    {
        switch (c)
        {
            case 'L':
                if (Junk == 1)
                {
                    World.Info("You see " + Junk + " pile of junk.");
                } else {
                    World.Info("You see " + Junk + " piles of junk.");
                }
                Junk = Junk * 2;
                return true;
        }
        return false;
    }

    public override void OnWest()
    {
        World.Enter(Root);
    }

    public Root Root { get; set; }

    private int Junk = 1;
}

class Program
{
    static void Main(string[] args)
    {
        World.BigBang();
    }
}
