using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestionTEsports
{
    internal class Partida
    {
        private string descripcion;
        private Equipo equipoLocal;
        private Equipo equipoVisitante;
        private DateTime fecha;
        private Estados estado;
        private string resultado;

        public enum Estados
        {
            [Description("No iniciado")]
            NoIniciado,
            [Description("Iniciado")]
            Iniciado,
            [Description("Finalizado")]
            Finalizado
        }

        public string Descripcion { get { return descripcion; } set { descripcion = value; } }
        public Equipo EquipoLocal { get { return equipoLocal; } set { equipoLocal = value; } }
        public Equipo EquipoVisitante { get { return equipoVisitante; } set { equipoVisitante = value; } }
        public DateTime Fecha { get { return fecha; } set { fecha = value; } }
        public Estados Estado { get { return estado; } set { estado = value; } }
        public string Resultado { get { return resultado; } set { resultado = value; } }
    }
}
