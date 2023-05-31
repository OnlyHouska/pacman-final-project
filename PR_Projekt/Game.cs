using BasicFunctionsLibrary;

namespace PR_Projekt
{
    public enum TileState
    {
        WallTop,
        WallSide,
        TopLeft,
        TopRight,
        BottomLeft,
        BottomRight,
        UpTriple,
        Dot,
        PowerPellet,
        Fruit,
        GhostSpawn,
        Blank,

        Cherry,
        Strawberry,
        Orange,
        Apple,
        Melon
    }

    internal class Game
    {
        private static readonly int _width = Console.WindowWidth - 2;
        private static readonly int _height = Console.WindowHeight - 2;

        public static TileState[,] _map = new TileState[_height + 2, _width + 2];

        public static int TotalDots = 0;
        public static int TotalFruits = 0;

        public static Fruits fruits = Fruits.Cherry;

        private static readonly char _wallTop = '═';
        private static readonly char _wallSide = '║';
        private static readonly char _topLeft = '╔';
        private static readonly char _topRight = '╗';
        private static readonly char _bottomLeft = '╚';
        private static readonly char _bottomRight = '╝';
        private static readonly char _upTriple = '╩';

        private static readonly char _dot = '°';
        private static readonly char _powerPellet = 'O';
        private static readonly char _fruit = '%';

        private static readonly int _left = 0;
        private static readonly int _top = 0;

        public async static void PlayMainTheme(Player p)
        {
            if (p.isAlive)
            {
                await Task.Run(async () =>
                {
                    Tools.PlaySound("gametheme");
                    Thread.Sleep(4200);
                    PlayMainTheme(p);
                });
            }
        }

        public static void CheckTile(Player p, Ghost g)
        {
            Debug.ShowLeftDots();
            Debug.ShowGhostsVulnerabilityState(p, g);
            Debug.ShowTileState(p, _map);
            Debug.ShowNextTileState(p, _map);
            Debug.ShowPlayerPosition(p, g);
            Color.Yellow();

            if (_map[p.Y, p.X] == TileState.Dot)
                p.CollectedDot();
            else if (_map[p.Y, p.X] == TileState.PowerPellet)
                p.PickedUpPowerPellet(p, g);

            switch (_map[p.Y, p.X])
            {
                case TileState.Cherry:
                    Tools.PlaySound("fruit");
                    p.Score += 100;
                    TotalFruits--;
                    break;
                case TileState.Strawberry:
                    Tools.PlaySound("fruit");
                    p.Score += 300;
                    TotalFruits--;
                    break;
                case TileState.Orange:
                    Tools.PlaySound("fruit");
                    p.Score += 500;
                    TotalFruits--;
                    break;
                case TileState.Apple:
                    Tools.PlaySound("fruit");
                    p.Score += 700;
                    TotalFruits--;
                    break;
                case TileState.Melon:
                    Tools.PlaySound("fruit");
                    p.Score += 1000;
                    TotalFruits--;
                    break;
                default:
                    break;
            }
            p.UpdatePlayerScore();


            if (g.X == p.X && g.Y == p.Y)
            {
                if (p.powerUp)
                {
                    p.powerUp = false;
                    g.Vulnerable = false;
                    g.SetCoordinates(Console.WindowWidth / 2, 15);
                }
                else
                    p.isAlive = false;
            }

            _map[p.Y, p.X] = TileState.Blank;
        }

        private static void GenerateMap()
        {
            try
            {
                _map = Tools.ReadBitMap(_map);
            }
            catch (Exception e)
            {
                Tools.ThrowException(e);
            }
        }


        public static void InitBorder()
        {
            Color.Blue();
            Console.SetCursorPosition(_left, _top);

            Console.Write(_topLeft);
            Console.Write(new string(_wallTop, _width));
            Console.Write(_topRight);

            for (int y = 0; y < _height; y++)
            {
                Console.SetCursorPosition(_left, _top + y + 1);
                _map[_top + y + 1, _left] = TileState.WallSide;
                Console.Write(_wallSide);
                Console.SetCursorPosition(Console.WindowWidth - 1, y + 1);
                _map[y + 1, Console.WindowWidth - 1] = TileState.WallSide;
                Console.Write(_wallSide);
            }

            Console.SetCursorPosition(_left, _top + _height + 1);

            Console.Write(_bottomLeft);
            Console.Write(new string(_wallTop, _width));
            Console.Write(_bottomRight);
        }

        public static void DrawMap()
        {
            TotalFruits = 0;
            TotalDots = 0;
            GenerateMap();

            for (int y = 0; y < _map.GetLength(0); y++)
            {
                for (int x = 0; x < _map.GetLength(1); x++)
                {
                    if (y != 0)
                    {
                        switch (_map[y, x])
                        {
                            case TileState.WallTop:
                                Color.Blue();
                                Console.SetCursorPosition(x, y);
                                Console.Write(_wallTop);
                                break;
                            case TileState.WallSide:
                                Color.Blue();
                                Console.SetCursorPosition(x, y);
                                Console.Write(_wallSide);
                                break;
                            case TileState.TopLeft:
                                Color.Blue();
                                Console.SetCursorPosition(x, y);
                                Console.Write(_topLeft);
                                break;
                            case TileState.TopRight:
                                Color.Blue();
                                Console.SetCursorPosition(x, y);
                                Console.Write(_topRight);
                                break;
                            case TileState.BottomLeft:
                                Color.Blue();
                                Console.SetCursorPosition(x, y);
                                Console.Write(_bottomLeft);
                                break;
                            case TileState.BottomRight:
                                Color.Blue();
                                Console.SetCursorPosition(x, y);
                                Console.Write(_bottomRight);
                                break;
                            case TileState.UpTriple:
                                Color.Blue();
                                Console.SetCursorPosition(x, y);
                                Console.Write(_upTriple);
                                break;
                            case TileState.PowerPellet:
                                Color.DarkYellow();
                                Console.SetCursorPosition(x, y);
                                Console.Write(_powerPellet);
                                break;
                            case TileState.Dot:
                                Color.White();
                                Console.SetCursorPosition(x, y);
                                Console.Write(_dot);
                                break;
                            case TileState.Fruit:
                                GenerateFruit(x, y);
                                Console.SetCursorPosition(x, y);
                                Console.Write(_fruit);
                                break;
                            case TileState.Blank:
                                Console.SetCursorPosition(x, y);
                                Console.Write(' ');
                                break;
                            case TileState.GhostSpawn:
                                Console.SetCursorPosition(x, y);
                                Console.Write(' ');
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
        }

        private static void GenerateFruit(int x, int y)
        {
            int rnd = Tools.rnd.Next(0, 5);
            switch (rnd)
            {
                case 0:
                    Color.DarkRed();
                    _map[y, x] = TileState.Cherry;
                    break;
                case 1:
                    Color.Red();
                    _map[y, x] = TileState.Strawberry;
                    break;
                case 2:
                    Color.DarkYellow();
                    _map[y, x] = TileState.Orange;
                    break;
                case 3:
                    Color.Green();
                    _map[y, x] = TileState.Apple;
                    break;
                case 4:
                    Color.DarkGreen();
                    _map[y, x] = TileState.Melon;
                    break;
                default:
                    break;
            }
        }

        public static void RenderPlayer(Player p)
        {
            Color.Yellow();
            Console.SetCursorPosition(p.X, p.Y);
            switch (p.direction)
            {
                case Player.Direction.Right:
                    Console.Write('►');
                    break;
                case Player.Direction.Left:
                    Console.Write('◄');
                    break;
                case Player.Direction.Down:
                    Console.Write('▼');
                    break;
                case Player.Direction.Up:
                    Console.Write('▲');
                    break;
                case Player.Direction.None:
                    break;
            }
        }

        public static void GameOver(Player p)
        {
            p.isAlive = false;
            Console.Title = "PacMan 2023 - Game Over";
            //Data.Save();
            Tools.PlaySound("gameover");

            for (int i = 0; i < _height + 2; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.Write(new string(' ', Console.WindowWidth));
                Thread.Sleep(25);
            }

            Console.SetCursorPosition(Console.WindowWidth / 2 - 3, 12);
            Color.Yellow();
            Console.Write("SCORE: " + p.Score.ToString("D5"));

            Color.Red();
            GameOverSign();
            Color.White();
            GameOverSign();
            Color.Red();
            GameOverSign();

            bool inMenu = true;

            UI.Location loc = UI.Location.None;

            string[] buttonLabels = { "Restart", "Main Menu" };
            int[] buttonOffsets = { 15, 20 };

            int selectedButton = 0;

            while (inMenu)
            {
                for (int i = 0; i < buttonLabels.Length; i++)
                {
                    if (i == selectedButton)
                    {
                        Color.Green();
                    }
                    else
                    {
                        Color.Yellow();
                    }

                    UI.CreateButton(buttonLabels[i], buttonOffsets[i]);
                }

                ConsoleKey key = Console.ReadKey(true).Key;
                switch (key)
                {
                    case ConsoleKey.Enter:
                        Tools.StopMusic();
                        Tools.PlaySound("select");
                        if (selectedButton == 0)
                        {
                            UI.ButtonBlink(buttonLabels[0], buttonOffsets[0]);
                            inMenu = false;
                            loc = UI.Location.Restart;
                        }
                        else if (selectedButton == 1)
                        {
                            UI.ButtonBlink(buttonLabels[1], buttonOffsets[1]);
                            inMenu = false;
                            Settings.loadingAnimation = true;
                            loc = UI.Location.MainMenu;
                            Tools.PlaySound("theme");
                        }
                        break;
                    case ConsoleKey.Spacebar:
                        Tools.StopMusic();
                        Tools.PlaySound("select");
                        if (selectedButton == 0)
                        {
                            UI.ButtonBlink(buttonLabels[0], buttonOffsets[0]);
                            inMenu = false;
                            loc = UI.Location.Restart;
                        }
                        else if (selectedButton == 1)
                        {
                            UI.ButtonBlink(buttonLabels[1], buttonOffsets[1]);
                            inMenu = false;
                            loc = UI.Location.MainMenu;
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        Tools.PlaySound("button");
                        if (selectedButton < buttonLabels.Length - 1)
                        {
                            selectedButton++;
                        }
                        else selectedButton = 0;
                        break;
                    case ConsoleKey.UpArrow:
                        Tools.PlaySound("button");
                        if (selectedButton > 0)
                        {
                            selectedButton--;
                        }
                        else selectedButton = buttonLabels.Length - 1;
                        break;
                    case ConsoleKey.F1:
                        loc = UI.Location.Restart;
                        inMenu = false;
                        break;
                }
            }
            UI.Reddirect(loc, p);


            Console.ReadKey(true);

        }

        private static void GameOverSign()
        {
            Console.SetCursorPosition(10, 2);
            Console.WriteLine("╔" + new string('═', 100) + "╗");
            Thread.Sleep(25);
            Console.SetCursorPosition(10, 3);
            Console.Write("║" + @" ________  ________  _____ ______   _______           ________  ___      ___ _______   ________     " + "║");
            Thread.Sleep(25);
            Console.SetCursorPosition(10, 4);
            Console.Write("║" + @"|\   ____\|\   __  \|\   _ \  _   \|\  ___ \         |\   __  \|\  \    /  /|\  ___ \ |\   __  \    " + "║");
            Thread.Sleep(25);
            Console.SetCursorPosition(10, 5);
            Console.Write("║" + @"\ \  \___|\ \  \|\  \ \  \\\__\ \  \ \   __/|        \ \  \|\  \ \  \  /  / | \   __/|\ \  \|\  \   " + "║");
            Thread.Sleep(25);
            Console.SetCursorPosition(10, 6);
            Console.Write("║" + @" \ \  \  __\ \   __  \ \  \\|__| \  \ \  \_|/__       \ \  \\\  \ \  \/  / / \ \  \_|/_\ \   _  _\  " + "║");
            Thread.Sleep(25);
            Console.SetCursorPosition(10, 7);
            Console.Write("║" + @"  \ \  \|\  \ \  \ \  \ \  \    \ \  \ \  \_|\ \       \ \  \\\  \ \    / /   \ \  \_|\ \ \  \\  \| " + "║");
            Thread.Sleep(25);
            Console.SetCursorPosition(10, 8);
            Console.Write("║" + @"   \ \_______\ \__\ \__\ \__\    \ \__\ \_______\       \ \_______\ \__/ /     \ \_______\ \__\\ _\ " + "║");
            Thread.Sleep(25);
            Console.SetCursorPosition(10, 9);
            Console.Write("║" + @"    \|_______|\|__|\|__|\|__|     \|__|\|_______|        \|_______|\|__|/       \|_______|\|__|\|__|" + "║");
            Thread.Sleep(25);
            Console.SetCursorPosition(10, 10);
            Console.WriteLine("╚" + new string('═', 100) + "╝");
        }

        private static void GameWonSign()
        {
            Thread.Sleep(25);
            Console.SetCursorPosition(15, 2);
            Console.WriteLine("╔" + new string('═', 93) + "╗");
            Thread.Sleep(25);
            Console.SetCursorPosition(15, 3);
            Console.Write("║" + @" ________  ________  _____ ______   _______           ___       __   ________  ________      " + "║");
            Thread.Sleep(25);
            Console.SetCursorPosition(15, 4);
            Console.Write("║" + @"|\   ____\|\   __  \|\   _ \  _   \|\  ___ \         |\  \     |\  \|\   __  \|\   ___  \    " + "║");
            Thread.Sleep(25);
            Console.SetCursorPosition(15, 5);
            Console.Write("║" + @"\ \  \___|\ \  \|\  \ \  \\\__\ \  \ \   __/|        \ \  \    \ \  \ \  \|\  \ \  \\ \  \   " + "║");
            Thread.Sleep(25);
            Console.SetCursorPosition(15, 6);
            Console.Write("║" + @" \ \  \  __\ \   __  \ \  \\|__| \  \ \  \_|/__       \ \  \  __\ \  \ \  \\\  \ \  \\ \  \  " + "║");
            Thread.Sleep(25);
            Console.SetCursorPosition(15, 7);
            Console.Write("║" + @"  \ \  \|\  \ \  \ \  \ \  \    \ \  \ \  \_|\ \       \ \  \|\__\_\  \ \  \\\  \ \  \\ \  \ " + "║");
            Thread.Sleep(25);
            Console.SetCursorPosition(15, 8);
            Console.Write("║" + @"   \ \_______\ \__\ \__\ \__\    \ \__\ \_______\       \ \____________\ \_______\ \__\\ \__\" + "║");
            Thread.Sleep(25);
            Console.SetCursorPosition(15, 9);
            Console.Write("║" + @"    \|_______|\|__|\|__|\|__|     \|__|\|_______|        \|____________|\|_______|\|__| \|__|" + "║");
            Thread.Sleep(25);
            Console.SetCursorPosition(15, 10);
            Console.WriteLine("╚" + new string('═', 93) + "╝");
        }

        internal static void Restart(Player player)
        {
            Player p = new();
            player = null;
            UI.Start(p);
        }

        internal static bool CheckForWall(Player p)
        {
            int right = p.X + 1;
            int left = p.X - 1;
            int up = p.Y - 1;
            int down = p.Y + 1;

            switch (p.direction)
            {
                case Player.Direction.Right:
                    if (_map[p.Y, right] == TileState.WallTop || _map[p.Y, right] == TileState.WallSide || _map[p.Y, right] == TileState.BottomLeft || _map[p.Y, right] == TileState.BottomRight || _map[p.Y, right] == TileState.TopRight || _map[p.Y, right] == TileState.TopLeft || _map[p.Y, right] == TileState.GhostSpawn)
                        return false;
                    else
                        return true;
                case Player.Direction.Left:
                    if (_map[p.Y, left] == TileState.WallTop || _map[p.Y, left] == TileState.WallSide || _map[p.Y, left] == TileState.BottomLeft || _map[p.Y, left] == TileState.BottomRight || _map[p.Y, left] == TileState.TopRight || _map[p.Y, left] == TileState.TopLeft || _map[p.Y, left] == TileState.GhostSpawn)
                        return false;
                    else
                        return true;
                case Player.Direction.Down:
                    if (_map[down, p.X] == TileState.WallTop || _map[down, p.X] == TileState.WallSide || _map[down, p.X] == TileState.BottomLeft || _map[down, p.X] == TileState.BottomRight || _map[down, p.X] == TileState.TopRight || _map[down, p.X] == TileState.TopLeft || _map[down, p.X] == TileState.GhostSpawn)
                        return false;
                    else
                        return true;
                case Player.Direction.Up:
                    if (_map[up, p.X] == TileState.WallTop || _map[up, p.X] == TileState.WallSide || _map[up, p.X] == TileState.BottomLeft || _map[up, p.X] == TileState.BottomRight || _map[up, p.X] == TileState.TopRight || _map[up, p.X] == TileState.TopLeft || _map[up, p.X] == TileState.GhostSpawn)
                        return false;
                    else
                        return true;
                default:
                    return true;
            }
        }

        internal static void GameWin(Player p)
        {
            Console.Title = "PacMan 2023 - Game Won";
            //Data.Save();
            Tools.PlaySound("gameover");

            for (int i = 0; i < _height + 2; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.Write(new string(' ', Console.WindowWidth));
                Thread.Sleep(25);
            }

            Console.SetCursorPosition(Console.WindowWidth / 2 - 3, 12);
            Color.Yellow();
            Console.Write("SCORE: " + p.Score.ToString("D5"));

            Color.Green();
            GameWonSign();
            Color.White();
            GameWonSign();
            Color.Green();
            GameWonSign();

            bool inMenu = true;

            UI.Location loc = UI.Location.None;

            string[] buttonLabels = { "Restart", "Main Menu" };
            int[] buttonOffsets = { 15, 20 };

            int selectedButton = 0;

            while (inMenu)
            {
                for (int i = 0; i < buttonLabels.Length; i++)
                {
                    if (i == selectedButton)
                    {
                        Color.Green();
                    }
                    else
                    {
                        Color.Yellow();
                    }

                    UI.CreateButton(buttonLabels[i], buttonOffsets[i]);
                }

                ConsoleKey key = Console.ReadKey(true).Key;
                switch (key)
                {
                    case ConsoleKey.Enter:
                        Tools.StopMusic();
                        Tools.PlaySound("select");
                        if (selectedButton == 0)
                        {
                            UI.ButtonBlink(buttonLabels[0], buttonOffsets[0]);
                            inMenu = false;
                            loc = UI.Location.Restart;
                        }
                        else if (selectedButton == 1)
                        {
                            UI.ButtonBlink(buttonLabels[1], buttonOffsets[1]);
                            inMenu = false;
                            loc = UI.Location.MainMenu;
                        }
                        break;
                    case ConsoleKey.Spacebar:
                        Tools.StopMusic();
                        Tools.PlaySound("select");
                        if (selectedButton == 0)
                        {
                            UI.ButtonBlink(buttonLabels[0], buttonOffsets[0]);
                            inMenu = false;
                            loc = UI.Location.Restart;
                        }
                        else if (selectedButton == 1)
                        {
                            UI.ButtonBlink(buttonLabels[1], buttonOffsets[1]);
                            inMenu = false;
                            loc = UI.Location.MainMenu;
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        Tools.PlaySound("button");
                        if (selectedButton < buttonLabels.Length - 1)
                        {
                            selectedButton++;
                        }
                        else selectedButton = 0;
                        break;
                    case ConsoleKey.UpArrow:
                        Tools.PlaySound("button");
                        if (selectedButton > 0)
                        {
                            selectedButton--;
                        }
                        else selectedButton = buttonLabels.Length - 1;
                        break;
                    case ConsoleKey.F1:
                        loc = UI.Location.Start;
                        inMenu = false;
                        break;
                }
            }
            UI.Reddirect(loc, p);


            Console.ReadKey(true);
        }
    }
}
