using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestionTEsports
{
    internal class Entrenador : Persona
    {
        private int aniosExperiencia;

        public int AniosExperiencia { get { return aniosExperiencia; } set { aniosExperiencia = value; } }
    }
}
