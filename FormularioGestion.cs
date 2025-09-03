using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static SistemaGestionTEsports.Repositorios;

namespace SistemaGestionTEsports
{
    public partial class FormularioGestion : Form
    {
        public FormularioGestion()
        {
            InitializeComponent();
            CargarDatosCMB();

        }

        // Excepciones personalizadas
        public class DatosInvalidosException : Exception
        {
            public DatosInvalidosException(string mensaje) : base(mensaje) { }
        }

        public class DatosRepetidosException : Exception
        {
            public DatosRepetidosException(string mensaje) : base(mensaje) { }
        }

        // Instancia de repositorios
        Repositorios.RepositorioJuegos repositoriosJuegos = new Repositorios.RepositorioJuegos();
        Repositorios.RepositorioEntrenadores repositorioEntrenadores = new Repositorios.RepositorioEntrenadores();
        Repositorios.RepositorioJugadores repositorioJugadores = new Repositorios.RepositorioJugadores();
        Repositorios.RepositorioEquipos repositorioEquipos = new Repositorios.RepositorioEquipos();
        Repositorios.RepositorioPartidas repositorioPartidas = new Repositorios.RepositorioPartidas();
        Repositorios.RepositorioTorneos repositorioTorneos = new Repositorios.RepositorioTorneos();

        // Logica para agregar juegos
        private void btnAgregarJuego_Click(object sender, EventArgs e)
        {
            try
            {
                string NombreJuego = tbNomJueg.Text;

                if (NombreJuego.Length == 0) throw new DatosInvalidosException("No puede introducir un campo vacio como nombre.");

                if (repositoriosJuegos.ListarDatos().Any(x => x.Nombre == NombreJuego)) throw new DatosRepetidosException("Este juego ya ha sido agregado con anterioridad.");

                int CantJugadoresPorEquipo = int.Parse(tbCantJug.Text);

                if (CantJugadoresPorEquipo <= 0) throw new DatosInvalidosException("Como minimo debe haber un jugador por equipo.");

                int SelectCat = cmbCateg.SelectedIndex;

                Juego juego = new Juego();
                juego.Nombre = NombreJuego;
                juego.CantJugadores = CantJugadoresPorEquipo;

                juego.Categoria = (Juego.Categorias)SelectCat;

                repositoriosJuegos.Agregar(juego);

                cmbJuego.Items.Add(juego.Nombre);
                cmbJuegoTor.Items.Add(juego.Nombre);

                MessageBox.Show($"El juego '{NombreJuego}' ha sido agregado correctamente.", "Introduccion valida", MessageBoxButtons.OK);
                tbNomJueg.Text = "";
                tbCantJug.Text = "";
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Logica para agregar entrenadores
        private void btnAgregarEntrenador_Click(object sender, EventArgs e)
        {
            try
            {
                string NombreEntrenador = tbNomEntren.Text;

                if (NombreEntrenador.Length == 0) throw new DatosInvalidosException("No puede introducir un campo vacio como nombre.");

                if (repositorioEntrenadores.ListarDatos().Any(x => x.Nombre == NombreEntrenador)) throw new DatosRepetidosException("Ya ha agregado este entrenador anteriormente.");

                int AniosExperiencia = int.Parse(tbAnosExp.Text);

                if (AniosExperiencia < 0 || AniosExperiencia > 100) throw new DatosInvalidosException("Los años de experiencia no pueden ser menor a 0 ni mayores a 100.");

                Entrenador entrenador = new Entrenador();
                entrenador.Nombre = NombreEntrenador;
                entrenador.AniosExperiencia = AniosExperiencia;

                repositorioEntrenadores.Agregar(entrenador);

                cmbEntren.Items.Add(entrenador.Nombre);

                MessageBox.Show($"El entrenador '{NombreEntrenador}' ha sido agregado correctamente.", "Introduccion valida", MessageBoxButtons.OK);
                tbNomEntren.Text = "";
                tbAnosExp.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void CargarDatosCMB()
        {
            cmbCateg.DataSource = Enum.GetValues(typeof(Juego.Categorias));
            cmbEstado.DataSource = Enum.GetValues(typeof(Partida.Estados));
            cmbEstadoTor.DataSource = Enum.GetValues(typeof(Torneo.Estados));
        }

        // Logica para agregar jugadores
        private void btnAgregarJugador_Click(object sender, EventArgs e)
        {
            try
            {
                string NombreJugador = tbNomJug.Text;

                if (NombreJugador.Length == 0) throw new DatosInvalidosException("No puede introducir un campo vacio como nombre.");

                if (repositorioJugadores.ListarDatos().Any(x => x.Nombre == NombreJugador)) throw new DatosRepetidosException("Ya ha agregado este jugador con anterioridad.");

                string Rango = tbRango.Text;

                if (Rango.Length == 0) throw new DatosInvalidosException("No puede introducir un campo vacio como rango.");

                string Region = tbRegion.Text;

                if (Region.Length == 0) throw new DatosInvalidosException("No puede introducir un campo vacio como region."); 

                Jugador jugador = new Jugador();
                jugador.Nombre = NombreJugador;
                jugador.Rango = Rango;
                jugador.Region = Region;

                repositorioJugadores.Agregar(jugador);

                cmbJug.Items.Add(jugador.Nombre);

                MessageBox.Show($"El jugador '{NombreJugador}' ha sido agregado correctamente.", "Introduccion valida", MessageBoxButtons.OK);
                tbNomJug.Text = "";
                tbRango.Text = "";
                tbRegion.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Logica para agregar equipos
        private void btnAgregarEquipo_Click(object sender, EventArgs e)
        {
            try
            {
                string Nombre = tbNomEquipo.Text;

                if (Nombre.Length == 0) throw new DatosInvalidosException("No puede introducir un campo vacio como nombre.");

                if (repositorioEquipos.ListarDatos().Any(x => x.Nombre == Nombre)) throw new DatosRepetidosException("Ya ha introducido este equipo con anterioridad.");

                Juego juegoElegido = repositoriosJuegos.ListarDatos().FirstOrDefault(x => x.Nombre == cmbJuego.Text);

                if (juegoElegido == null) throw new DatosInvalidosException("Por favor, elija un juego antes de agregar un equipo.");

                Entrenador entrenadorElegido = repositorioEntrenadores.ListarDatos().FirstOrDefault(x => x.Nombre == cmbEntren.Text);

                if (entrenadorElegido == null) throw new DatosInvalidosException("Por favor, seleccione un entrenador antes de agregar un equipo.");

                if (repositorioEquipos.ListarDatos().Any(x => x.Entrenador.Nombre == Nombre)) throw new DatosRepetidosException("Ya ha introducido este entrenador en otro equipo con anterioridad.");

                if (repositorioEquipos.ListarJugadores().Count != 0)
                    foreach (Jugador jugador in (List<Jugador>)repositorioEquipos.ListarJugadores())
                        if (repositorioEquipos.ListarDatos().Any(x => x.Jugadores.Any(z => z.Nombre == jugador.Nombre))) throw new DatosRepetidosException("Este jugador ya se encuentra en un equipo");

                Equipo equipo = new Equipo();
                equipo.Nombre = Nombre;
                equipo.Juego = juegoElegido;
                equipo.Entrenador = entrenadorElegido;
                equipo.Jugadores = (List<Jugador>)repositorioEquipos.ListarJugadores();

                repositorioEquipos.Agregar(equipo);
                CargarDatosEquipo(equipo);
                repositorioEquipos.BorrarDatosListaJugadores();

                cmbEquipLoc.Items.Add(Nombre);

                cmbJuego.Enabled = true;
                lblJugadores.Text = "0/0";

                MessageBox.Show($"El equipo '{Nombre}' ha sido agregado correctamente.", "Introduccion valida", MessageBoxButtons.OK);
                tbNomEquipo.Text = "";
            } 
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAgregarJugEq_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbJuego.Text.Length == 0) throw new DatosInvalidosException("Por favor, seleccione un juego antes de agregar jugadores");

                string Jugador = cmbJug.Text;

                if (Jugador.Length == 0) throw new DatosInvalidosException("Debe de seleccionar un jugador para agregarlo.");

                if (repositorioEquipos.ListarJugadores().Any(x => x.Nombre == Jugador)) throw new DatosRepetidosException("Este jugador ya ha sido agregado con anterioridad.");

                Jugador jugador = new Jugador();

                List<Jugador> listaJugadores = (List<Jugador>)repositorioJugadores.ListarDatos();

                jugador = listaJugadores.FirstOrDefault(x => x.Nombre == Jugador);

                Juego juegoElegido = repositoriosJuegos.ListarDatos().FirstOrDefault(x => x.Nombre == cmbJuego.Text);

                int CantJugadoresPorEquipo = juegoElegido.CantJugadores;

                int CantJugadoresIntroducidos = repositorioEquipos.ListarJugadores().Count;

                if (CantJugadoresIntroducidos >= CantJugadoresPorEquipo) throw new DatosInvalidosException("Ya ha introducido la cantidad maxima de jugadores para este equipo.");
                else
                {
                    repositorioEquipos.AgregarJugador(jugador);
                    CantJugadoresIntroducidos++;
                }

                lblJugadores.Text = $"{CantJugadoresIntroducidos.ToString()}/{CantJugadoresPorEquipo}";

                btnAgregarEquipo.Enabled = CantJugadoresIntroducidos == CantJugadoresPorEquipo ? true : false;
                cmbJuego.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbJuego_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Juego juegoElegido = repositoriosJuegos.ListarDatos().FirstOrDefault(x => x.Nombre == cmbJuego.Text);

                int CantJugadoresPorEquipo = juegoElegido.CantJugadores;

                int CantJugadoresIntroducidos = repositorioEquipos.ListarJugadores().Count;

                lblJugadores.Text = $"{CantJugadoresIntroducidos.ToString()}/{CantJugadoresPorEquipo}";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Logica para agregar partidas
        private void btnAgregarPartida_Click(object sender, EventArgs e)
        {
            try
            {
                string Descripcion = tbDescrip.Text;

                if (Descripcion.Length == 0) throw new DatosInvalidosException("No puede introducir un campo vacio como descripcion.");

                Equipo equipoLocal = repositorioEquipos.ListarDatos().FirstOrDefault(x => x.Nombre == cmbEquipLoc.Text);

                if (equipoLocal == null) throw new DatosInvalidosException("Debe de seleccionar un equipo local antes de agregar una partida.");

                Equipo equipoVisitante = repositorioEquipos.ListarDatos().FirstOrDefault(x => x.Nombre == cmbEquipVis.Text);

                if (equipoVisitante == null) throw new DatosInvalidosException("Debe de seleccionar un equipo visitante antes de agregar una partida.");

                if (equipoVisitante == equipoLocal) throw new DatosRepetidosException("No puede seleccionar dos equipos iguales en contra en una partida.");

                DateTime fecha = new DateTime();

                fecha = repositorioPartidas.ListarDatos().Any(x => x.Fecha == fecha) && equipoLocal.Juego == equipoVisitante.Juego ? throw new DatosRepetidosException("No puede seleccionar fechas iguales a las de otras partidas del mismo juego.") : dtpFecha.Value;

                int EstadoSeleccionado = cmbEstado.SelectedIndex;

                string resultado = tbResult.Text;

                if (resultado.Length == 0) throw new DatosInvalidosException("No puede introducir un campo vacio como resultado.");

                Partida partida = new Partida();
                partida.Descripcion = Descripcion;
                partida.EquipoLocal = equipoLocal;
                partida.EquipoVisitante = equipoVisitante;
                partida.Fecha = fecha;
                partida.Estado = (Partida.Estados)EstadoSeleccionado;
                partida.Resultado = resultado;

                if (repositorioPartidas.ListarDatos().Any(x => x == partida)) throw new DatosRepetidosException("Ya ha agregado esta partida con anterioridad.");

                repositorioPartidas.Agregar(partida);
                CargarDatosDGVPartidas(partida);

                MessageBox.Show($"La partida de '{equipoLocal.Nombre}' vs '{equipoVisitante.Nombre}' ha sido agregado correctamente.", "Introduccion valida", MessageBoxButtons.OK);
                tbDescrip.Text = "";
                tbResult.Text = "";
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAgregarPartTor_Click(object sender, EventArgs e)
        {
            try
            {
                Partida partida = repositorioPartidas.ListarDatos().FirstOrDefault(x => $"{x.EquipoLocal.Nombre} vs {x.EquipoVisitante.Nombre}" == cmbPartidas.Text);

                if (partida == null) throw new DatosInvalidosException("Por favor seleccione una partida a agregar.");

                if (repositorioTorneos.ListarPartidas().Any(x => x == partida)) throw new DatosRepetidosException("Ya ha agregado esta partida con anterioridad.");

                repositorioTorneos.AgregarPartidas(partida);

                btnFinalizarTorneo.Enabled = repositorioTorneos.ListarPartidas().Count() >= 2 ? true : false;
            }
            catch(Exception ex )
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbEquipLoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                cmbEquipVis.Items.Clear();

                Equipo equipoLocal = repositorioEquipos.ListarDatos().FirstOrDefault(x => x.Nombre == cmbEquipLoc.Text);

                Juego juegoEquipoLocal = repositoriosJuegos.ListarDatos().FirstOrDefault(x => x.Nombre == equipoLocal.Juego.Nombre);

                List<Equipo> equipoCMB = (List<Equipo>)repositorioEquipos.ListarDatos().Where(x => x.Juego == juegoEquipoLocal).ToList();

                foreach (Equipo equipo in equipoCMB)
                {
                    cmbEquipVis.Items.Add(equipo.Nombre);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Logica para finalizar torneos
        private void btnFinalizarTorneo_Click(object sender, EventArgs e)
        {
            try
            {
                string Nombre = tbNomTorneo.Text;

                if (Nombre.Length == 0) throw new DatosInvalidosException("No puede introducir un campo vacio como nombre.");

                Juego juego = repositoriosJuegos.ListarDatos().FirstOrDefault(x => x.Nombre == cmbJuegoTor.Text);

                if (juego == null) throw new DatosInvalidosException("Debe de introducir un juego para crear un torneo.");

                DateTime fechaInicio = dtpFechaInicio.Value;

                DateTime fechaFin = dtpFechaFin.Value;

                if (fechaInicio >= fechaFin) throw new DatosInvalidosException("La fecha de inicio debe ser menor a la fecha de finalizacion.");

                string Premio = tbPremio.Text;

                if (Premio.Length == 0) throw new DatosInvalidosException("No puede introducir un campo vacio como premio.");

                string Formato = tbForm.Text;

                if (Formato.Length == 0) throw new DatosInvalidosException("No puede introducir un campo vacio como formato.");

                int estadoSeleccionado = cmbEstado.SelectedIndex;

                Torneo torneo = new Torneo();
                torneo.Nombre = Nombre;
                torneo.Juego = juego;
                torneo.FechaInicio = fechaInicio;
                torneo.FechaFin = fechaFin;
                torneo.Premio = Premio;
                torneo.Formato = Formato;
                torneo.Estado = (Torneo.Estados)estadoSeleccionado;
                torneo.ListaPartidas = repositorioTorneos.ListarPartidas().ToList();

                if (repositorioTorneos.ListarDatos().Any(x => x == torneo)) throw new DatosRepetidosException("Ya ha agregado este torneo con anterioridad.");

                repositorioTorneos.Agregar(torneo);

                CargarDatosDGVTorneos(torneo);

                MessageBox.Show($"El torneo '{Nombre}' ha sido agregado correctamente.", "Introduccion valida", MessageBoxButtons.OK);
                tbNomTorneo.Text = "";
                tbPremio.Text = "";
                tbForm.Text = "";
                
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbJuegoTor_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                cmbPartidas.Items.Clear();

                Juego juego = repositoriosJuegos.ListarDatos().FirstOrDefault(x => x.Nombre == cmbJuegoTor.Text);

                List<Partida> listaPartidas = repositorioPartidas.ListarDatos().Where(x => x.EquipoLocal.Juego == juego).ToList();

                foreach (Partida partida in listaPartidas)
                {
                    cmbPartidas.Items.Add($"{partida.EquipoLocal.Nombre} vs {partida.EquipoVisitante.Nombre}");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Metodos para cargar datos
        private void CargarDatosDGVTorneos(Torneo torneo)
        {
            int fila = dgvTorneos.Rows.Add();
            dgvTorneos.Rows[fila].Cells[0].Value = torneo.Nombre.ToString();
            dgvTorneos.Rows[fila].Cells[1].Value = torneo.Juego.Nombre.ToString();
            dgvTorneos.Rows[fila].Cells[2].Value = torneo.FechaInicio.ToString("dd/MM/yyyy");
            dgvTorneos.Rows[fila].Cells[3].Value = torneo.FechaFin.ToString("dd/MM/yyyy");
            dgvTorneos.Rows[fila].Cells[4].Value = torneo.Premio.ToString();
            dgvTorneos.Rows[fila].Cells[5].Value = torneo.Formato.ToString();
            dgvTorneos.Rows[fila].Cells[6].Value = torneo.Estado.ToString();

            string partidas = "";

            foreach(Partida partida in torneo.ListaPartidas)
            {
                partidas += partidas == "" ? partidas += partida.EquipoLocal.Nombre + "vs" + partida.EquipoVisitante.Nombre : partidas += " | ";
                partidas += partida.EquipoLocal.Nombre + "vs" + partida.EquipoVisitante.Nombre;
            }

            partidas = $"[ {partidas} ]";
            dgvTorneos.Rows[fila].Cells[7].Value = partidas;
        }

        private void CargarDatosDGVPartidas(Partida partida)
        {
            int fila = dgvPartidas.Rows.Add();
            dgvPartidas.Rows[fila].Cells[0].Value = partida.Descripcion;
            dgvPartidas.Rows[fila].Cells[1].Value = partida.EquipoLocal.Nombre.ToString();
            dgvPartidas.Rows[fila].Cells[2].Value = partida.EquipoVisitante.Nombre.ToString();
            dgvPartidas.Rows[fila].Cells[3].Value = partida.Fecha.ToString("dd/MM/yyyy");
            dgvPartidas.Rows[fila].Cells[4].Value = partida.Estado;
            dgvPartidas.Rows[fila].Cells[5].Value = partida.Resultado;
        }

        private void CargarDatosEquipo(Equipo equipo)
        {
            int fila = dgvEquipos.Rows.Add();
            dgvEquipos.Rows[fila].Cells[0].Value = equipo.Nombre;
            dgvEquipos.Rows[fila].Cells[1].Value = equipo.Juego.Nombre;
            dgvEquipos.Rows[fila].Cells[2].Value = equipo.Entrenador.Nombre;

            string jugadores = "";

            foreach (Jugador jugador in equipo.Jugadores)
            {
                jugadores += jugadores == "" ? jugador.Nombre : ", ";
                jugadores += jugador.Nombre;
            }

            jugadores = $"[ {jugadores} ]";
            dgvEquipos.Rows[fila].Cells[3].Value = jugadores;
        }

        //Logica para borrar datos
        private void dgvTorneos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewRow fila = dgvTorneos.Rows[e.RowIndex];

                string nombre = fila.Cells[0].Value?.ToString();

                DialogResult resultado = MessageBox.Show($"¿Desea borrar el torneo '{nombre}'?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (resultado == DialogResult.Yes)
                {
                    repositorioTorneos.Eliminar(repositorioTorneos.ListarDatos().FirstOrDefault(x => x.Nombre == nombre));

                    foreach (DataGridViewRow fila1 in dgvTorneos.Rows)
                    {
                        if (fila.Cells[0].Value?.ToString() == nombre)
                        {
                            dgvTorneos.Rows.Remove(fila1);
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvPartidas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewRow fila = dgvPartidas.Rows[e.RowIndex];

                string descripcion = fila.Cells[0].Value?.ToString();

                DialogResult resultado = MessageBox.Show($"¿Desea borrar la partida nro: '{e.RowIndex}'?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (resultado == DialogResult.Yes)
                {
                    repositorioPartidas.Eliminar(repositorioPartidas.ListarDatos().FirstOrDefault(x => x.Descripcion == descripcion));

                    foreach (DataGridViewRow fila1 in dgvPartidas.Rows)
                    {
                        if (fila.Cells[0].Value?.ToString() == descripcion)
                        {
                            dgvPartidas.Rows.Remove(fila1);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvEquipos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewRow fila = dgvEquipos.Rows[e.RowIndex];

                string nombre = fila.Cells[0].Value?.ToString();

                DialogResult resultado = MessageBox.Show($"¿Desea borrar el equipo '{nombre}'?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (resultado == DialogResult.Yes)
                {
                    repositorioEquipos.Eliminar(repositorioEquipos.ListarDatos().FirstOrDefault(x => x.Nombre == nombre));

                    foreach (DataGridViewRow fila1 in dgvEquipos.Rows)
                    {
                        if (fila.Cells[0].Value?.ToString() == nombre)
                        {
                            dgvEquipos.Rows.Remove(fila1);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
