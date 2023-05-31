namespace PR_Projekt
{
    internal class Movement
    {
        /// <summary>
        /// 1. Up
        /// 2. Down
        /// 3. Left
        /// 4. Right
        /// </summary>
        /// <param name="p"></param>
        public static void Move(Player p, Ghost g)
        {
            switch (p.direction)
            {
                case Player.Direction.Right:
                    while (p.direction == Player.Direction.Right)
                    {
                        if (g.Vulnerable)
                        {
                            if (g.VulnerableMove)
                            {
                                g.Move(p, g);
                                g.VulnerableMove = false;
                            }
                            else
                                g.VulnerableMove = true;
                        }
                        else
                            g.Move(p, g);

                        MoveRight(p, g);
                        Thread.Sleep(Settings.GetPlayerSpeed());
                        p.CheckForPlayerState(p);
                        LoadKey(p, g);
                    }
                    break;
                case Player.Direction.Left:
                    while (p.direction == Player.Direction.Left)
                    {
                        if (g.Vulnerable)
                        {
                            if (g.VulnerableMove)
                            {
                                g.Move(p, g);
                                g.VulnerableMove = false;
                            }
                            else
                                g.VulnerableMove = true;
                        }
                        else
                            g.Move(p, g);
                        MoveLeft(p, g);
                        Thread.Sleep(Settings.GetPlayerSpeed());
                        p.CheckForPlayerState(p);
                        LoadKey(p, g);
                    }
                    break;
                case Player.Direction.Down:
                    while (p.direction == Player.Direction.Down)
                    {
                        if (g.Vulnerable)
                        {
                            if (g.VulnerableMove)
                            {
                                g.Move(p, g);
                                g.VulnerableMove = false;
                            }
                            else
                                g.VulnerableMove = true;
                        }
                        else
                            g.Move(p, g);
                        MoveDown(p, g);
                        Thread.Sleep(Settings.GetPlayerSpeed());
                        p.CheckForPlayerState(p);
                        LoadKey(p, g);
                    }
                    break;
                case Player.Direction.Up:
                    while (p.direction == Player.Direction.Up)
                    {
                        if (g.Vulnerable)
                        {
                            if (g.VulnerableMove)
                            {
                                g.Move(p, g);
                                g.VulnerableMove = false;
                            }
                            else
                                g.VulnerableMove = true;
                        }
                        else
                            g.Move(p, g);
                        MoveUp(p, g);
                        Thread.Sleep(Settings.GetPlayerSpeed());
                        p.CheckForPlayerState(p);
                        LoadKey(p, g);
                    }
                    break;
                case Player.Direction.None:
                    while (p.direction == Player.Direction.None)
                    {
                        p.CheckForPlayerState(p);
                        LoadKey(p, g);
                    }
                    break;
                default:
                    break;
            }
        }

        public static void LoadKey(Player p, Ghost g)
        {
            while (Console.KeyAvailable)
            {
                ConsoleKey key = Console.ReadKey(true).Key;
                if (key == Settings.ControlsSettings["MoveUp"])
                {
                    Player.Direction returnDir = p.direction;
                    p.direction = Player.Direction.Up;
                    if (!Game.CheckForWall(p) || p.Y == Player.Ymax)
                        p.direction = returnDir;
                    Move(p, g);
                }
                else if (key == ConsoleKey.DownArrow || key == ConsoleKey.S)
                {
                    Player.Direction returnDir = p.direction;
                    p.direction = Player.Direction.Down;
                    if (!Game.CheckForWall(p))
                        p.direction = returnDir;
                    Move(p, g);
                }
                else if (key == ConsoleKey.LeftArrow || key == ConsoleKey.A)
                {
                    Player.Direction returnDir = p.direction;
                    p.direction = Player.Direction.Left;
                    if (!Game.CheckForWall(p))
                        p.direction = returnDir;
                    Move(p, g);
                }
                else if (key == ConsoleKey.RightArrow || key == ConsoleKey.D)
                {
                    Player.Direction returnDir = p.direction;
                    p.direction = Player.Direction.Right;
                    if (!Game.CheckForWall(p))
                        p.direction = returnDir;
                    Move(p, g);

                }
                else if (key == ConsoleKey.Escape)
                {
                    Tools.StopMusic();
                    Game.GameOver(p);
                }
            }
        }

        private static void MoveRight(Player p, Ghost g)
        {
            if (Game.CheckForWall(p))
            {
                Console.SetCursorPosition(p.X, p.Y);
                if (!(p.X == g.X && p.Y == g.Y))
                {
                    Console.Write(' ');
                }
                p.X++;

                Game.RenderPlayer(p);
            }
            Game.CheckTile(p, g);
        }

        private static void MoveLeft(Player p, Ghost g)
        {
            if (Game.CheckForWall(p))
            {
                Console.SetCursorPosition(p.X, p.Y);
                if (!(p.X == g.X && p.Y == g.Y))
                {
                    Console.Write(' ');
                }
                p.X--;

                Game.RenderPlayer(p);
            }
            Game.CheckTile(p, g);
        }

        private static void MoveUp(Player p, Ghost g)
        {
            if (Game.CheckForWall(p))
            {
                Console.SetCursorPosition(p.X, p.Y);

                if (!(p.X == g.X && p.Y == g.Y))
                {
                    Console.Write(' ');
                }
                p.Y--;

                Game.RenderPlayer(p);
            }
            Game.CheckTile(p, g);
        }

        private static void MoveDown(Player p, Ghost g)
        {
            if (Game.CheckForWall(p))
            {
                Console.SetCursorPosition(p.X, p.Y);
                if (!(p.X == g.X && p.Y == g.Y))
                {
                    Console.Write(' ');
                }
                p.Y++;

                Game.RenderPlayer(p);
            }
            Game.CheckTile(p, g);
        }
    }
}