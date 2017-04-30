using Raven.Client;
using dn32.infraestrutura.Banco;
using System.Collections.Generic;
using System.Linq;

namespace dn32.infraestrutura.Generico
{
    public class RepositorioGenericoComNome<T>: RepositorioGenerico<T> where T : ModelGenericoComNome, new()
    {
        public virtual List<T> Consulte(string termo)
        {
            using (IDocumentSession session = Contexto.Store.OpenSession())
            {
                return session
                       .Advanced
                       .DocumentQuery<T>()
                       .Where($"{nameof(ModelGenericoComNome.Nome)}:{termo}")
                       .OrderBy(x => x.Nome)
                       .ToList();
            }
        }

        public virtual List<T> Liste(int pagina, int elemtosPorPagina, out RavenQueryStatistics estatisticas)
        {
            var quantidadeAPular = (pagina - 1) * elemtosPorPagina;
            if (elemtosPorPagina == 0)
            {
                elemtosPorPagina = 10;
            }

            using (IDocumentSession session = Contexto.Store.OpenSession())
            {
                return session
                    .Query<T>()
                    .Statistics(out estatisticas)
                    .OrderBy(x => x.Nome)
                    .Skip(quantidadeAPular)
                    .Take(elemtosPorPagina)
                    .ToList();
            }
        }
    }
}
