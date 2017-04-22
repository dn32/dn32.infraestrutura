using dn32.infraestrutura.Contrato;
using System;
using System.Collections.Generic;
using System.Text;
using static dn32.infraestrutura.Utilitarios;

namespace dn32.infraestrutura.Generico
{
    public class ModelGenerico : IModelGenerico
    {
        public int Codigo
        {
            get
            {
                return ObtenhaCodigoDoElemento(Id);
            }
            set
            {
                Id = ObtenhaIdDoElemento(GetType(), value);
            }
        }

        public string Id { get; set; }
        public DateTime DataDeCadastro { get; set; }
        public DateTime DataDeAtualizacao { get; set; }
        public string Nome { get; set; }
    }
}
