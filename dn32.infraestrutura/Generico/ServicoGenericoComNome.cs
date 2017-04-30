using Raven.Client;
using System;
using System.Collections.Generic;
using dn32.infraestrutura.Fabrica;

namespace dn32.infraestrutura.Generico
{
    public class ServicoGenericoComNome<T>: ServicoGenerico<T> where T : ModelGenericoComNome, new()
    {
        public new RepositorioGenericoComNome<T> Repositorio
        {
            get
            {
                if (_repositorio == null)
                {
                    _repositorio = FabricaDeRepositorio.Crie<T>();
                }

                return _repositorio as RepositorioGenericoComNome<T>;
            }
        }

        public new ValidacaoGenericaComNome<T> Validacao
        {
            get
            {
                if (_validacao == null)
                {
                    _validacao = FabricaDeValidacao.Crie<T>();
                }

                return _validacao as ValidacaoGenericaComNome<T>;
            }
        }

        public List<T> Consulte(string termo)
        {
            Validacao.ConsultePorTermo(termo);
            return Repositorio.Consulte(termo);
        }

        public virtual List<T> Liste(int pagina, int elemtosPorPagina, out RavenQueryStatistics estatisticas)
        {
            Validacao.Liste(pagina, elemtosPorPagina);
            return Repositorio.Liste(pagina, elemtosPorPagina, out estatisticas);
        }
    }
}