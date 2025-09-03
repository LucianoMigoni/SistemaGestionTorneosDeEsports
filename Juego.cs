using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestionTEsports
{
    internal class Juego
    {
        private string nombre;
        private Categorias categoria;
        private int cantJugadores;

        public enum Categorias
        {
            MOBA,
            FPS,
            BattleRoyale,
            SportSim,
        }

        public string Nombre { get { return nombre; } set { nombre = value; } }
        public Categorias Categoria { get { return categoria; } set { categoria = value; } }
        public int CantJugadores { get { return cantJugadores; } set { cantJugadores = value; } }
    }
}
