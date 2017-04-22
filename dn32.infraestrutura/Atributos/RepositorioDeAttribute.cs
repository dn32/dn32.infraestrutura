using System;
using static dn32.infraestrutura.Utilitarios;

namespace dn32.infraestrutura.Atributos
{
    public class RepositorioDeAttribute : Attribute
    {
        public Type TipoDeEntidade { get; set; }

        public RepositorioDeAttribute(Type tipoDeEntidade)
        {
            TipoDeEntidade = tipoDeEntidade;
            EhModelgenerico(TipoDeEntidade);
        }
    }  
}
