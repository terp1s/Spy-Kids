using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace _pacman_
{
    public enum Directions { Up, Down, Left, Right };

    class Map
    {

        char[,] MapState;
        int MapHeight;
        int MapWidth;

        int PacmanRow;
        int PacmanCol;

        public void PacmanLeft()
        {
            if (PacmanRow == 0 && MapState[MapWidth-1, PacmanCol] != 'X')
            {
                MapState[PacmanRow, PacmanCol] = '.';
                PacmanRow = MapWidth-1;
                MapState[PacmanRow, PacmanCol] = '<';
            }
            else if (MapState[PacmanRow - 1, PacmanCol] == 'X' || (PacmanRow == 0 && MapState[MapWidth -1, PacmanCol] == 'X'))
            {
                MapState[PacmanRow, PacmanCol] = '<';
            }
            else
            {
                MapState[PacmanRow, PacmanCol] = '.';
                PacmanRow = PacmanRow - 1;
                MapState[PacmanRow, PacmanCol] = '<';
            }
        }
        public void PacmanRight()
        {
            if (PacmanRow == MapWidth-1 && MapState[0, PacmanCol] != 'X')
            {
                MapState[PacmanRow, PacmanCol] = '.';
                PacmanRow = 0;
                MapState[PacmanRow, PacmanCol] = '>';
            }
            else if (MapState[PacmanRow + 1, PacmanCol] == 'X' || (PacmanRow == MapWidth - 1 && MapState[0, PacmanCol] == 'X'))
            {
                MapState[PacmanRow, PacmanCol] = '>';
            }
            else
            {
                MapState[PacmanRow, PacmanCol] = '.';
                PacmanRow = PacmanRow + 1;
                MapState[PacmanRow, PacmanCol] = '>';
            }
        }
        public void PacmanDown()
        {
            if (PacmanCol == MapHeight-1 && MapState[PacmanRow, 0] != 'X')
            {
                MapState[PacmanRow, PacmanCol] = '.';
                PacmanCol = 0;
                MapState[PacmanRow, PacmanCol] = 'v';
            }
            else if (MapState[PacmanRow, PacmanCol + 1] == 'X' || (PacmanCol == MapHeight-1 && MapState[PacmanRow, 0] == 'X'))
            {
                MapState[PacmanRow, PacmanCol] = 'v';
            }
            else
            {
                MapState[PacmanRow, PacmanCol] = '.';
                PacmanCol = PacmanCol + 1;
                MapState[PacmanRow, PacmanCol] = 'v';
            }
        }
        public void PacmanUp()
        {
            if (PacmanCol == 0 && MapState[PacmanRow, MapHeight-1] != 'X')
            {
                MapState[PacmanRow, PacmanCol] = '.';
                PacmanCol = MapHeight-1;
                MapState[PacmanRow, PacmanCol] = '^';
            }
            else if (MapState[PacmanRow, PacmanCol - 1] == 'X' || (PacmanCol == 0 && MapState[PacmanRow, MapHeight-1] == 'X'))
            {
                MapState[PacmanRow, PacmanCol] = '^';
            }
            else
            {
                MapState[PacmanRow, PacmanCol] = '.';
                PacmanCol = PacmanCol - 1;
                MapState[PacmanRow, PacmanCol] = '^';
            }
        }

        public string PrintMap()
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < MapHeight; i++)
            {
                for (int j = 0; j < MapWidth; j++)
                {
                    sb.Append(MapState[j, i]);
                }
                sb.AppendLine();
            }

            return sb.ToString();
        }

        public void Wall(int x, int y, int x2, int y2)
        {

        }

        public static Map FirstRound()
        {
            Map t = new Map();
            t.MapState = new char[80, 25];
            t.MapHeight = 25;
            t.MapWidth = 80;
            for (int i = 0; i < t.MapHeight; i++)
            {
                for (int j = 0; j < t.MapWidth; j++)
                {
                    t.MapState[j, i] = '.';
                }
            }
            t.MapState[0, 0] = '>';

            return t;
        }

    }

    class Game
    {
        Map gameMap;

        public bool Finished { get; private set; } = false;

        public void MoveItMoveIt(Directions dir)
        {
            switch (dir)
            {
                case Directions.Up:
                    gameMap.PacmanUp();
                    break;
                case Directions.Down:
                    gameMap.PacmanDown();
                    break;
                case Directions.Left:
                    gameMap.PacmanLeft();
                    break;
                case Directions.Right:
                    gameMap.PacmanRight();
                    break;
                default:
                    break;

            }


        }

        public string PrintMap()
        {
            Console.SetCursorPosition(0,0);
            return gameMap.PrintMap();
        }

        public Game()
        {
            gameMap = Map.FirstRound();
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();

            Console.WriteLine(game.PrintMap());
            Console.ReadKey();

            while (!game.Finished)
            {
                var tt = Console.ReadKey();
                Console.WriteLine();
                var t = tt.KeyChar.ToString();
                if (t == "w")
                {
                    game.MoveItMoveIt(Directions.Up);
                }
                else if (t == "a")
                {
                    game.MoveItMoveIt(Directions.Left);
                }
                else if (t == "s")
                {
                    game.MoveItMoveIt(Directions.Down);
                }
                else if (t == "d")
                {
                    game.MoveItMoveIt(Directions.Right);
                }
                else
                {
                    Console.WriteLine("Invalid input");
                }
                //Console.Clear();
                Console.WriteLine(game.PrintMap());
            }
        }
    }
}
