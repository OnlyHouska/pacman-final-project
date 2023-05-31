using BasicFunctionsLibrary;

namespace PR_Projekt
{
    public enum Ghosts
    {
        Blinky,
        Inky,
        Pinky,
        Clyde
    }
    public enum Direction
    {
        Right,
        Left,
        Down,
        Up,
        None
    }

    internal class Ghost
    {
        private readonly char _dot = '°';
        private readonly char _powerPellet = 'O';
        private readonly char _fruit = '%';


        private readonly char _ghost = 'G';

        private int _x = Console.WindowWidth / 2;
        private int _y = 15; //15 default

        public static int _min = 0;
        public static int _Xmax = Console.WindowWidth - 1;
        public static int _Ymax = Console.WindowHeight - 1;
        public int X
        {
            get => _x;
            private set
            {
                if (value > _min && value < _Xmax)
                    _x = value;
            }
        }
        public int Y
        {
            get => _y;
            private set
            {
                if (value > _min && value < _Ymax)
                    _y = value;
            }
        }
        public bool Vulnerable { get; set; }
        public bool VulnerableMove { get; set; }

        public Ghost()
        {
            VulnerableMove = false;
            Vulnerable = false;
            X = _x;
            Y = _y;
        }
        public void SetCoordinates(int x, int y)
        {
            X = x;
            Y = y;
        }

        public void Move(Player p, Ghost g)
        {
            int dx = p.X - X;
            int dy = p.Y - Y;

            Direction dir = Direction.None;
            TileState[,] _map = Game._map;
            TileState currentState = _map[Y, X];
            if (Vulnerable)
            {
                if (dx > 0)
                    dir = Direction.Left;
                else if (dx < 0)
                    dir = Direction.Right;
                else if (dy > 0)
                    dir = Direction.Up;
                else if (dy < 0)
                    dir = Direction.Down;


                if (CheckForWall(dir, g))
                {
                    RewriteTileState(currentState);
                    if (dir == Direction.Left)
                        X--;
                    else if (dir == Direction.Right)
                        X++;
                    else if (dir == Direction.Up)
                        Y--;
                    else if (dir == Direction.Down)
                        Y++;
                }
                else
                {
                    if (dir == Direction.Left || dir == Direction.Right)
                    {
                        if (dy > 0)
                            dir = Direction.Up;
                        else if (dy < 0)
                            dir = Direction.Down;

                        if (CheckForWall(dir, g))
                        {
                            RewriteTileState(currentState);
                            if (dir == Direction.Up)
                                Y--;
                            else if (dir == Direction.Down)
                                Y++;
                        }
                    }
                    else if (dir == Direction.Up || dir == Direction.Down)
                    {
                        if (dx > 0)
                            dir = Direction.Left;
                        else if (dx < 0)
                            dir = Direction.Right;

                        if (CheckForWall(dir, g))
                        {
                            RewriteTileState(currentState);
                            if (dir == Direction.Left)
                                X--;
                            else if (dir == Direction.Right)
                                X++;
                        }
                    }
                }
            }
            else
            {
                if (dx < 0)
                    dir = Direction.Left;
                else if (dx > 0)
                    dir = Direction.Right;
                else if (dy < 0)
                    dir = Direction.Up;
                else if (dy > 0)
                    dir = Direction.Down;

                if (CheckForWall(dir, g))
                {
                    RewriteTileState(currentState);
                    if (dir == Direction.Left)
                        X--;
                    else if (dir == Direction.Right)
                        X++;
                    else if (dir == Direction.Up)
                        Y--;
                    else if (dir == Direction.Down)
                        Y++;
                }
                else
                {
                    if (dir == Direction.Left || dir == Direction.Right)
                    {
                        if (dy < 0)
                            dir = Direction.Up;
                        else if (dy > 0)
                            dir = Direction.Down;

                        if (CheckForWall(dir, g))
                        {
                            RewriteTileState(currentState);
                            if (dir == Direction.Up) 
                                Y--;
                            else if (dir == Direction.Down)
                                Y++;
                        }
                    }
                    else if (dir == Direction.Up || dir == Direction.Down)
                    {
                        if (dx < 0)
                            dir = Direction.Left;
                        else if (dx > 0)
                            dir = Direction.Right;

                        if (CheckForWall(dir, g))
                        {
                            RewriteTileState(currentState);
                            if (dir == Direction.Left)
                                X--;
                            else if (dir == Direction.Right)
                                X++;
                        }
                    }
                }
            }
            RenderGhost();
        }


        private void RewriteTileState(TileState currentState)
        {
            Console.SetCursorPosition(X, Y);

            switch (currentState)
            {
                case TileState.Dot:
                    Color.White();
                    Console.Write(_dot);
                    break;
                case TileState.PowerPellet:
                    Color.DarkYellow();
                    Console.Write(_powerPellet);
                    break;
                case TileState.Apple:
                    Color.Green();
                    Console.Write(_fruit);
                    break;
                case TileState.Orange:
                    Color.DarkYellow();
                    Console.Write(_fruit);
                    break;
                case TileState.Cherry:
                    Color.DarkRed();
                    Console.Write(_fruit);
                    break;
                case TileState.Melon:
                    Color.DarkGreen();
                    Console.Write(_fruit);
                    break;
                case TileState.Strawberry:
                    Color.Red();
                    Console.Write(_fruit);
                    break;
                default:
                    Console.WriteLine(' ');
                    break;
            }
        }


        private bool CheckForWall(Direction direction, Ghost g)
        {
            int right = X + 1;
            int left = X - 1;
            int up = Y - 1;
            int down = Y + 1;

            TileState[,] _map = Game._map;
            switch (direction)
            {
                case Direction.Right:
                    if (_map[Y, right] == TileState.WallTop || _map[Y, right] == TileState.WallSide || _map[Y, right] == TileState.BottomLeft || _map[Y, right] == TileState.BottomRight || _map[Y, right] == TileState.TopRight || _map[Y, right] == TileState.TopLeft)
                        return false;
                    else
                        return true;
                case Direction.Left:
                    if (_map[Y, left] == TileState.WallTop || _map[Y, left] == TileState.WallSide || _map[Y, left] == TileState.BottomLeft || _map[Y, left] == TileState.BottomRight || _map[Y, left] == TileState.TopRight || _map[Y, left] == TileState.TopLeft)
                        return false;
                    else
                        return true;
                case Direction.Down:
                    if (_map[down, X] == TileState.WallTop || _map[down, X] == TileState.WallSide || _map[down, X] == TileState.BottomLeft || _map[down, X] == TileState.BottomRight || _map[down, X] == TileState.TopRight || _map[down, X] == TileState.TopLeft)
                        return false;
                    else
                        return true;
                case Direction.Up:
                    if (_map[up, X] == TileState.WallTop || _map[up, X] == TileState.WallSide || _map[up, X] == TileState.BottomLeft || _map[up, X] == TileState.BottomRight || _map[up, X] == TileState.TopRight || _map[up, X] == TileState.TopLeft)
                        return false;
                    else
                        return true;
                default:
                    return false;
            }
        }

        public void RenderGhost()
        {
            if (Vulnerable)
                Color.Blue();
            else
                Color.Red();

            Console.SetCursorPosition(X, Y);
            Console.Write(_ghost);
        }
    }
}
