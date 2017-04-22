using System;
using static dn32.infraestrutura.Utilitarios;

namespace dn32.infraestrutura.Atributos
{
    public class ServicoDeAttribute : Attribute
    {
        public Type TipoDeEntidade { get; set; }

        public ServicoDeAttribute(Type tipoDeEntidade)
        {
            TipoDeEntidade = tipoDeEntidade;
            EhModelgenerico(TipoDeEntidade);
        }
    }
}
