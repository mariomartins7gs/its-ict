using System;
using System.Collections.Generic;
using System.Text;

namespace Atleti
{
    internal class Atleta:IAtleta
    {
        //proprietà

        public required string Nome { get; set; }

        public required string Cognome { get; set; }

        public required int Pettorina { get; set; }

        public required string Disciplina { get; set; }

        string IAtleta.Corro()
        {
            return "Sto correndo...";
        }

        string IAtleta.Salto()
        {
            return "Sto saltando...";
        }

        public override string ToString()
        {
            return $"{GetType().Name}: " +
                $
        }
    }
}
