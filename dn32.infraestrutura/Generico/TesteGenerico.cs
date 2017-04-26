
using dn32.infraestrutura.Contrato;
using dn32.infraestrutura.Model;

namespace dn32.infraestrutura.Generico
{
    public abstract class TesteGenerico<T> where T : IModelGenerico, new()
    {
        public TesteGenerico()
        {
            InicializarInfraestrutura();
        }

        public abstract void InicializarInfraestrutura();
    }
}
