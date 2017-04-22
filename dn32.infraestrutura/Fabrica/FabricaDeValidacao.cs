using dn32.infraestrutura.Atributos;
using dn32.infraestrutura.Contrato;
using dn32.infraestrutura.Generico;
using dn32.infraestrutura.Util;
using System;

namespace dn32.infraestrutura.Fabrica
{
    public class FabricaDeValidacao
    {
        public static ValidacaoGenerica<T> Crie<T>(bool usarSpecifico = false) where T : IModelGenerico, new()
        {
            Type type = typeof(T);
            Compartilhado.DicionarioDeValidacao.TryGetValue(type.Name, out type);

            if (usarSpecifico && type == null)
            {
                throw new Exception(string.Format($"Não foi encontrado o tipo específico '{typeof(T).Name}'. Use o atributo '[{(nameof(ValidacaoDeAttribute))}(typeof({typeof(T).Name}))]' para definir uma validação."));
            }

            type = type ?? typeof(ValidacaoGenerica<T>);

            return (ValidacaoGenerica<T>)Activator.CreateInstance(type);
        }
    }
}
