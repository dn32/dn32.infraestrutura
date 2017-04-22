using dn32.infraestrutura.Atributos;
using dn32.infraestrutura.Generico;
using dn32.infraestrutura.testes.Model;

namespace dn32.infraestrutura.testes.Repositorio
{
    [RepositorioDe(typeof(UnidadeDeTeste))]
    public class RepositorioDeUnidadeDeTeste : RepositorioGenerico<UnidadeDeTeste>
    {
        public int CadastreCustomizado(UnidadeDeTeste unidadeDeteste)
        {
            unidadeDeteste.Descricao = "Descricao do cadastro customizado";
           return Cadastrar(unidadeDeteste);
        }
    }
}
