using dn32.infraestrutura.Contrato;
using dn32.infraestrutura.Generico;
using System;

namespace dn32.infraestrutura.Fabrica
{
    public class FabricaDeServico
    {
        public static ServicoGenerico<T> Crie<T>() where T : IModelGenerico, new()
        {
            if (!Compartilhado.Inicializado)
            {
                throw new Exception("A infraestrutura deve ser inicializada antes do início da utilização.");
            }

            Compartilhado.DicionarioDeServico.TryGetValue(typeof(T).Name, out Type type);
            type = type ?? typeof(ServicoGenerico<T>);
            return (ServicoGenerico<T>)Activator.CreateInstance(type); ;
        }
    }
}