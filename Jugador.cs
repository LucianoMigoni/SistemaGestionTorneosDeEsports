using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestionTEsports
{
    internal class Jugador : Persona
    {
        private string rango;
        private string region;

        public string Rango { get { return rango; } set { rango = value; } }
        public string Region { get { return region; } set { region = value; } }
    }
}
