using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestionTEsports
{
    public class Repositorios
    {
        internal class RepositorioJuegos : IRepositorios<Juego>
        {
            List<Juego> ListaJuegos;

            public RepositorioJuegos()
            {
                ListaJuegos = new List<Juego>();
            }

            public void Agregar(Juego juego)
            {
                ListaJuegos.Add(juego);
            }

            public void Eliminar(Juego juego)
            {
                ListaJuegos.Remove(juego);
            }

            public IReadOnlyCollection<Juego> ListarDatos()
            {
                return ListaJuegos;
            }
        }

        internal class RepositorioEntrenadores : IRepositorios<Entrenador>
        {
            List<Entrenador> ListaEntrenadores;

            public RepositorioEntrenadores()
            {
                ListaEntrenadores = new List<Entrenador>();
            }

            public void Agregar(Entrenador entrenador)
            {
                ListaEntrenadores.Add(entrenador);
            }

            public void Eliminar(Entrenador entrenador)
            {
                ListaEntrenadores.Remove(entrenador);
            }

            public IReadOnlyCollection<Entrenador> ListarDatos()
            {
                return ListaEntrenadores;
            }
        }


        internal class RepositorioJugadores : IRepositorios<Jugador>
        {
            List<Jugador> ListaJugadores;

            public RepositorioJugadores()
            {
                ListaJugadores = new List<Jugador>();
            }

            public void Agregar(Jugador jugador)
            {
                ListaJugadores.Add(jugador);
            }

            public void Eliminar(Jugador jugador)
            {
                ListaJugadores.Remove(jugador);
            }

            public IReadOnlyCollection<Jugador> ListarDatos()
            {
                return ListaJugadores;
            }
        }

        internal class RepositorioEquipos : IRepositorios<Equipo>
        {
            List<Equipo> ListaEquipos;
            List<Jugador> ListaJugadores;

            public RepositorioEquipos()
            {
                ListaEquipos = new List<Equipo>();
                ListaJugadores = new List<Jugador>();
            }

            public void AgregarJugador(Jugador jugador)
            {
                ListaJugadores.Add(jugador);
            }

            public void BorrarDatosListaJugadores()
            {
                ListaJugadores = new List<Jugador>();
            }

            public List<Jugador> ListarJugadores()
            {
                return ListaJugadores;
            }

            public void Agregar(Equipo equipo)
            {
                ListaEquipos.Add(equipo);
            }

            public void Eliminar(Equipo equipo)
            {
                ListaEquipos.Remove(equipo);
            }

            public IReadOnlyCollection<Equipo> ListarDatos()
            {
                return ListaEquipos;
            }
        }

        internal class RepositorioPartidas : IRepositorios<Partida>
        {
            List<Partida> ListaPartidas;

            public RepositorioPartidas()
            {
                ListaPartidas = new List<Partida>();
            }

            public void Agregar(Partida partida)
            {
                ListaPartidas.Add(partida);
            }

            public void Eliminar(Partida partida)
            {
                ListaPartidas.Remove(partida);
            }

            public IReadOnlyCollection<Partida> ListarDatos()
            {
                return ListaPartidas;
            }
        }

        internal class RepositorioTorneos : IRepositorios<Torneo>
        {
            List<Torneo> ListaTorneos;
            List<Partida> ListaPartidas;

            public RepositorioTorneos()
            {
                ListaTorneos = new List<Torneo>();
                ListaPartidas = new List<Partida>();
            }

            public void AgregarPartidas(Partida partida)
            {
                ListaPartidas.Add(partida);
            }

            public void Agregar(Torneo torneo)
            {
                ListaTorneos.Add(torneo);
            }

            public void Eliminar(Torneo torneo)
            {
                ListaTorneos.Remove(torneo);
            }

            public IReadOnlyCollection<Torneo> ListarDatos()
            {
                return ListaTorneos;
            }

            public IReadOnlyCollection<Partida> ListarPartidas()
            {
                return ListaPartidas;
            }
        }
    }
}
