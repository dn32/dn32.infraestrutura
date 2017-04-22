using System;
using System.Collections.Generic;
using System.Text;
using static dn32.infraestrutura.Utilitarios;

namespace dn32.infraestrutura.Generico
{
    public class ModelGenerico
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
        string Nome { get; set; }
    }
}
