using System.Collections.Generic;

namespace matrix1
{
    class Vect2d
    {
        public int Row { get; private set; }
        public int Col { get; private set; }
        public int Hloubka { get; private set; }

        public Vect2d(int row, int col)
        {
            Row = row;
            Col = col;
        }

        /// <summary>
        /// Returns sum of 2 vectors as a new vector 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Vect2d Add(Vect2d a, Vect2d b)
        {
            return new Vect2d(a.Row + b.Row, a.Col + b.Col);
        }

        /// <summary>
        /// Returns list of vectors with representing vertical and horizontal moves.
        /// </summary>
        /// <returns></returns>
        public static List<Vect2d> Moves()
        {
            var l = new List<Vect2d>();

            Vect2d nahoru = new Vect2d(-1, 0);
            Vect2d dolu = new Vect2d(1, 0);
            Vect2d doleva = new Vect2d(0, -1);
            Vect2d doprava = new Vect2d(0, 1);
            l.Add(nahoru);
            l.Add(dolu);
            l.Add(doleva);
            l.Add(doprava);

            return l;
        }
    }
}

