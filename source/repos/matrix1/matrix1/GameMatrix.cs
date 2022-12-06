using System;
using System.Collections.Generic;

namespace matrix1
{
    /// <summary>
    /// Represents game.
    /// </summary>
    class GameMatrix
    {
        int Rows { get; set; }
        int Cols { get; set; }

        Vect2d Start { get; set; }
        Vect2d Finnish { get; set; }

        int[,] matrix;

        /// <summary>
        /// Creates game.
        /// </summary>
        /// <param name="rows">Number of rows</param>
        /// <param name="cols">Number of collumns</param>
        /// <param name="start">Starting square</param>
        /// <param name="finnish">Target square</param>
        /// <param name="obstacles">list of obstacles coordinates</param>
        public GameMatrix(int rows, int cols, Vect2d start, Vect2d finnish, List<Vect2d> obstacles)
        {
            Rows = rows;
            Cols = cols;

            matrix = new int[Rows, Cols];

            // init
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Cols; j++)
                {
                    matrix[i, j] = int.MaxValue;
                }
            }

            // set obstacles
            foreach (Vect2d t in obstacles)
            {
                matrix[t.Row, t.Col] = -1;
            }

            matrix[start.Row, start.Col] = 0;

            Start = start;
            Finnish = finnish;
        }

        /// <summary>
        /// from starting point using BFS explores matrix, counts number of steps from starting point to each square
        /// </summary>
        void FillMatrix()
        {
            // pokaždé, když na nějaké políčko zapíšeme hodnotu, tak ho dáme do fronty
            Queue<Vect2d> que = new Queue<Vect2d>();
            que.Enqueue(Start);

            while (que.Count != 0)
            {
                // vezmeme první člen fronty
                Vect2d position = que.Dequeue();
                // a zpracujeme ho
                MakeMoves(position, que);
            }

        }

        /// <summary>
        /// explores squares adjacent to position, if they are unexplored, writes number of steps and adds them to the que
        /// </summary>
        /// <param name="position"></param>
        /// <param name="que"></param>
        void MakeMoves(Vect2d position, Queue<Vect2d> que)
        {
            foreach(Vect2d vect in Vect2d.Moves())
            {
                if (matrix[vect.Row, vect.Col] > 0 && matrix[vect.Row, vect.Col] != int.MaxValue)
                {
                    que.Enqueue(vect);
                    matrix[vect.Row, vect.Col] = matrix[position.Row, position.Col] + 1;
                }
            }
        }
    }
}

