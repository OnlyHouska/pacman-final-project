using BasicFunctionsLibrary;

namespace PR_Projekt
{
    internal class Debug
    {
        private static char _wallTop = '═';

        internal static void ShowLeftDots()
        {
            if (Settings.DebugMode)
            {
                Console.SetCursorPosition(Console.WindowWidth - 6, Console.WindowHeight - 1);
                for (int i = 0; i < 6; i++)
                {
                    Color.Blue();
                    Console.Write(_wallTop);
                }
                Console.SetCursorPosition(Console.WindowWidth - 6, Console.WindowHeight - 1);
                Color.Red();
                Console.Write(Game.TotalDots);

                Console.SetCursorPosition(Console.WindowWidth - 9, Console.WindowHeight - 1);
                for (int i = 0; i < 3; i++)
                {
                    Color.Blue();
                    Console.Write(_wallTop);
                }
                Console.SetCursorPosition(Console.WindowWidth - 9, Console.WindowHeight - 1);
                Color.Red();
                Console.Write(Game.TotalFruits);

                Color.Yellow();



                Color.Yellow();
            }
        }

        internal static void ShowGhostsVulnerabilityState(Player p, Ghost g)
        {
            if (Settings.DebugMode)
            {
                Console.SetCursorPosition(1, 0);
                for (int i = 0; i < 10; i++)
                {
                    Color.Blue();
                    Console.Write(_wallTop);
                }
                Console.SetCursorPosition(1, Console.WindowHeight - 1);
                Color.Red();
                Console.Write("V: " + g.Vulnerable + " VM: " + g.VulnerableMove);

                Color.Yellow();
            }
        }

        internal static void ShowTileState(Player p, TileState[,] _map)
        {
            if (Settings.DebugMode)
            {
                Console.SetCursorPosition(1, 0);
                for (int i = 0; i < 11; i++)
                {
                    Color.Blue();
                    Console.Write(_wallTop);
                }
                Console.SetCursorPosition(1, 0);
                Color.Red();
                Console.Write(_map[p.Y, p.X]);

                Color.Yellow();
            }
        }

        internal static void ShowNextTileState(Player p, TileState[,] _map)
        {
            if (Settings.DebugMode)
            {
                int t = 0;
                int r = 0;

                switch (p.direction)
                {
                    case Player.Direction.Right:
                        r = 1;
                        t = 0;
                        break;
                    case Player.Direction.Left:
                        r = -1;
                        t = 0;
                        break;
                    case Player.Direction.Down:
                        r = 0;
                        t = -1;
                        break;
                    case Player.Direction.Up:
                        r = 0;
                        t = 1;
                        break;
                    default:
                        break;
                }


                Console.SetCursorPosition(Console.WindowWidth - 12, 0);
                for (int i = 0; i < 11; i++)
                {
                    Color.Blue();
                    Console.Write(_wallTop);
                }
                Console.SetCursorPosition(Console.WindowWidth - 12, 0);
                Color.Red();
                Console.Write(_map[p.Y + t, p.X + r]);

                Color.Yellow();
            }
        }

        internal static void ShowPlayerPosition(Player p, Ghost g)
        {
            if (Settings.DebugMode)
            {
                Console.SetCursorPosition(Console.WindowWidth / 2 - 15, Console.WindowHeight - 1);
                Console.Write("P: " + p.X + "," + p.Y + "  G: " + g.X + "," + g.Y + " ");
                Color.Yellow();
            }
        }
    }
}
