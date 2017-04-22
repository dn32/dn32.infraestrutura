using System;

namespace dn32.infraestrutura.Contrato
{
    public interface IModelGenerico
    {
        int Codigo { get; set; }
        string Id { get; set; }
        string Nome { get; set; }
        DateTime DataDeCadastro { get; set; }
        DateTime DataDeAtualizacao { get; set; }
    }
}
