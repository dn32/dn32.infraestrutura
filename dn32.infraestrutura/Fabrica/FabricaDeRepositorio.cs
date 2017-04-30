using dn32.infraestrutura.Contrato;
using dn32.infraestrutura.Generico;
using System;

namespace dn32.infraestrutura.Fabrica
{
    public class FabricaDeRepositorio
    {
        public static RepositorioGenerico<T> Crie<T>() where T : IModelGenerico, new()
        {
            if (!Compartilhado.Inicializado)
            {
                throw new Exception("A infraestrutura deve ser inicializada antes do início da utilização.");
            }

            Compartilhado.DicionarioDeRepositorio.TryGetValue(typeof(T).Name, out Type type);
            type = type ?? typeof(RepositorioGenerico<T>);
            return (RepositorioGenerico<T>)Activator.CreateInstance(type);
        }
    }
}