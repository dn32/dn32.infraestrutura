using dn32.infraestrutura.Atributos;
using dn32.infraestrutura.Generico;
using dn32.infraestrutura.testes.Model;

namespace dn32.infraestrutura.testes.Repositorio
{
    [ValidacaoDe(typeof(UnidadeDeTeste))]
    public class ValidacaoDeUnidadeDeTeste : ValidacaoGenerica<UnidadeDeTeste>
    {
        public void CadastreCustomizado(UnidadeDeTeste unidadeDeteste)
        {
        }
    }
}
