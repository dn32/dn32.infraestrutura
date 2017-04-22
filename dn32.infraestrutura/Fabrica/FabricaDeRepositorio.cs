using dn32.infraestrutura.Contrato;
using dn32.infraestrutura.Generico;
using System;

namespace dn32.infraestrutura.Fabrica
{
    public class FabricaDeRepositorio
    {
        public static RepositorioGenerico<T> Crie<T>() where T : IModelGenerico, new()
        {
            Compartilhado.DicionarioDeRepositorio.TryGetValue(typeof(T).Name, out Type type);
            type = type ?? typeof(RepositorioGenerico<T>);
            return (RepositorioGenerico<T>)Activator.CreateInstance(type);
        }
    }
}