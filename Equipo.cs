using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestionTEsports
{
    internal class Equipo
    {
        private string nombre;
        private Juego juego;
        private Entrenador entrenador;
        private List<Jugador> jugadores;

        public string Nombre { get { return nombre; } set { nombre = value; } }
        public Juego Juego { get { return juego; } set { juego = value; } }
        public Entrenador Entrenador { get { return entrenador; } set { entrenador = value; } }
        public List<Jugador> Jugadores { get { return jugadores; } set { jugadores = value; } }
    }
}
