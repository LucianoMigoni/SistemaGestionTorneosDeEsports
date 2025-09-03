using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestionTEsports
{
    public interface IRepositorios<T>
    {
        void Agregar(T t);
        void Eliminar(T t);
        IReadOnlyCollection<T> ListarDatos();
    }
}
