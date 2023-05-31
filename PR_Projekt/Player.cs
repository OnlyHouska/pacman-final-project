using BasicFunctionsLibrary;
using System.Text.Json.Serialization;

namespace PR_Projekt
{
    public enum Fruits
    {
        Cherry,
        Strawberry,
        Orange,
        Apple,
        Melon
    }
    internal class Player
    {
        public enum Direction
        {
            Right,
            Left,
            Down,
            Up,
            None
        }

        public Direction direction = Direction.None;

        private readonly char _pacman = 'c';

        public bool isAlive;
        public bool powerUp;
        private int _x = Console.WindowWidth / 2;
        private int _y = 6;

        public static int Min = 0;
        public static int Xmin = 1;
        public static int Ymin = 1;
        public static int Xmax = Console.WindowWidth - 1;
        public static int Ymax = Console.WindowHeight - 1;

        public static List<Player> Players = new();

        public string? Name { get; private set; }
        public int Score { get; set; }
        public int TotalDots { get; private set; }
        public int KilledGhosts { get; private set; }
        [JsonIgnore]
        public int X
        {
            get => _x;
            set
            {
                if (value > Min && value < Xmax)
                    _x = value;
            }
        }
        [JsonIgnore]
        public int Y
        {
            get => _y;
            set
            {
                if (value > Min && value < Ymax)
                    _y = value;
            }
        }

        /// <summary>
        /// Creates a new player
        /// </summary>
        /// <param name="name"></param>
        public Player()
        {
            Score = default;
            isAlive = true;
            powerUp = false;
            X = _x;
            Y = _y;

            Players.Add(this);
        }

        public void CheckForPlayerState(Player p)
        {
            if (Game.TotalDots <= 0  && Game.TotalFruits <= 0)
            {
                Game.GameWin(p);
            }

            if (!isAlive)
            {
                PlayerBlink();
                Game.GameOver(p);
            }
        }

        private void PlayerBlink()
        {
            for (int i = 0; i < 4; i++)
            {
                Console.SetCursorPosition(X, Y);
                Console.Write(_pacman);
                Thread.Sleep(50);
                Console.SetCursorPosition(X, Y);
                Console.Write(' ');
                Thread.Sleep(50);
            }
        }

        public void UpdatePlayerScore()
        {
            Color.Yellow();
            Console.Title = $"SCORE: {Score:D5}";
            Console.SetCursorPosition(Console.WindowWidth / 2 - 8, 0);
            Console.WriteLine(" SCORE: " + Score.ToString("D5") + " ");
        }

        public void CollectedDot()
        {
            if (powerUp)
                Tools.PlaySound("dotPU");
            else
                Tools.PlaySound("dot");
            Game.TotalDots--;
            Score += 10;
            UpdatePlayerScore();
        }
        public void KilledGhost(Ghost g)
        {
            //Tools.PlaySound("killed-ghost");
            Score += 800;
            UpdatePlayerScore();
        }
        public async void PickedUpPowerPellet(Player p, Ghost g)
        {
            await Task.Run(async () =>
            {
                Tools.PlaySound("powerup");
                Score += 50;
                UpdatePlayerScore();
                p.powerUp = true;
                g.Vulnerable = true;
                for (int i = 10; i >= 0; i--)
                {
                    await Task.Delay(1000);
                }
                g.Vulnerable = false;
                p.powerUp = false;
            });
        }


        /// <summary>
        /// Adds a score depending on what fruit did the player picked up
        /// </summary>
        /// <param name="fruit"></param>
        

        //internal int[] GetCoords()
        //{
        //    int[] coords = new int[2];
        //    coords[0] = X;
        //    coords[1] = Y;

        //    return coords;
        //}
    }
}
