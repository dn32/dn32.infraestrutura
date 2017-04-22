using dn32.infraestrutura.Atributos;
using dn32.infraestrutura.Banco;
using dn32.infraestrutura.Contrato;
using dn32.infraestrutura.Generico;
using dn32.infraestrutura.Util;
using System;

namespace dn32.infraestrutura.Fabrica
{
    public class FabricaDeRepositorio
    {
        public static RepositorioGenerico<T> Crie<T>(bool usarSpecifico = false) where T : IModelGenerico, new()
        {
            Type type = typeof(T);
            Compartilhado.DicionarioDeRepositorio.TryGetValue(type.Name, out type);

            if (usarSpecifico && type == null)
            {
                throw new Exception(string.Format($"Não foi encontrado o tipo específico '{typeof(T).Name}'. Use o atributo '[{(nameof(RepositorioDeAttribute))}(typeof({typeof(T).Name}))]' para definir um repositório."));
            }

            type = type ?? typeof(RepositorioGenerico<T>);

            return (RepositorioGenerico<T>)Activator.CreateInstance(type);
        }
    }
}
