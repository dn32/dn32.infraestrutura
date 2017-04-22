using System;
using static dn32.infraestrutura.Utilitarios;

namespace dn32.infraestrutura.Atributos
{
    public class ValidacaoDeAttribute : Attribute
    {
        public Type TipoDeEntidade { get; set; }

        public ValidacaoDeAttribute(Type tipoDeEntidade)
        {
            TipoDeEntidade = tipoDeEntidade;
            EhModelgenerico(TipoDeEntidade);
        }
    }
}
