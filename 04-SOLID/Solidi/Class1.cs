using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Text;

namespace Solido
{
    internal class Class1
    {
        //properties
        public double PesoSpecifico { get; set; }

        //constructor con passaggio di parametri
        public Solido(double pesoSpecifico)
        {
            PesoSpecifico = pesoSpecifico;
        }

        public double Peso()
        {
            return PesoSpecifico * Volume();
        }

        public abstract double Volume();

        public override string ToString()
        {
            return $"Peso Specifico: {PesoSpecifico}";
        }
    }
}
