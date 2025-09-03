using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestionTEsports
{
    internal class Torneo
    {
        private string nombre;
        private Juego juego;
        private DateTime fechaInicio;
        private DateTime fechaFin;
        private string premio;
        private string formato;
        private Estados estado;
        private List<Partida> listaPartidas;

        public enum Estados
        {
            [Description("No iniciado")]
            NoIniciado,
            [Description("Iniciado")]
            Iniciado,
            [Description("Finalizado")]
            Finalizado
        }

        public string Nombre { get { return nombre; } set { nombre = value; } }
        public Juego Juego { get { return juego; } set { juego = value; } }
        public DateTime FechaInicio { get { return fechaInicio; } set { fechaInicio = value; } }
        public DateTime FechaFin { get { return fechaFin; } set { fechaFin = value; } }
        public string Premio { get { return premio; } set { premio = value; } }
        public string Formato { get { return formato; } set { formato = value; } }
        public Estados Estado { get { return estado; } set { estado = value; } }
        public List<Partida> ListaPartidas { get { return listaPartidas; } set { listaPartidas = value; } }
    }
}
