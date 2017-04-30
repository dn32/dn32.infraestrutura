using dn32.infraestrutura.Generico;
using System;

namespace dn32.infraestrutura.Fabrica
{
    public class FabricaDeValidacao
    {
        public static ValidacaoGenerica<T> Crie<T>() where T : ModelGenerico, new()
        {
            if (!Compartilhado.Inicializado)
            {
                throw new Exception("A infraestrutura deve ser inicializada antes do início da utilização.");
            }

            Compartilhado.DicionarioDeValidacao.TryGetValue(typeof(T).Name, out Type type);
            type = type ?? typeof(ValidacaoGenerica<T>);
            return (ValidacaoGenerica<T>)Activator.CreateInstance(type);
        }
    }
}
