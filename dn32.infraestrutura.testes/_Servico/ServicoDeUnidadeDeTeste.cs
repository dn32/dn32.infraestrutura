using dn32.infraestrutura.Atributos;
using dn32.infraestrutura.Generico;
using dn32.infraestrutura.testes.Model;

namespace dn32.infraestrutura.testes.Repositorio
{
    [ServicoDe(typeof(UnidadeDeTeste))]
    public class ServicoDeUnidadeDeTeste : ServicoGenerico<UnidadeDeTeste>
    {
        public int CadastreCustomizado(UnidadeDeTeste unidadeDeteste)
        {
            unidadeDeteste.Descricao += " - Repositório";
            ((ValidacaoDeUnidadeDeTeste)Validacao).CadastreCustomizado(unidadeDeteste);
            return CadastreCustomizado(unidadeDeteste);
        }
    }
}
