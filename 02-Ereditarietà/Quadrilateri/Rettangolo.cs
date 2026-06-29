using System;
using System.Collections.Generic;
using System.Text;

namespace Quadrilateri
{
    internal class Rettangolo:Quadrilatero
    {
        public Rettangolo(double @base, double altezza) : base(@base, altezza, @base, altezza);

        public double Area()
        {
            return base._lato1 * base._lato2;
        }

        public double Diagonale()
        {
            return Math.Sqrt(Math.Pow(_lato1, 2) + Math.Pow(_lato2, 2));
        }

        public override string ToString()
        {
            return $"" +
                $"base={_lato1}"+
                $", altezza={_lato2}"+
                $", area={Area()}" +
                $", diagonale={Diagonale()}";
        }

    }



}
