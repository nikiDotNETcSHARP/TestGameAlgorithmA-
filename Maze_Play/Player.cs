namespace Maze_Play
{
    public class Player : Unit
    {
        private char[,] _map;

        public Player(int startX, int StartY, ConsoleRenderer renderer, char[,] map) : base(startX, StartY, '@', renderer)
        {
            _map = map;
        }

        public override void Update()
        {
            ConsoleKeyInfo KeyInfo;

            if (Console.KeyAvailable)
            {
                KeyInfo = Console.ReadKey();

                switch (KeyInfo.Key)
                {
                    case ConsoleKey.UpArrow:
                        TryMoveUp(_map);
                        break;
                    case ConsoleKey.DownArrow:
                        TryMoveDown(_map);
                        break;
                    case ConsoleKey.LeftArrow:
                        TryMoveLeft(_map);
                        break;
                    case ConsoleKey.RightArrow:
                        TryMoveRight(_map);
                        break;
                }
            }
        }

    }
}
