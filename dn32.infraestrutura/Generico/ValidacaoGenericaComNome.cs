using System;
using dn32.infraestrutura.Fabrica;
using dn32.infraestrutura.Constantes;

namespace dn32.infraestrutura.Generico
{
    public class ValidacaoGenericaComNome<T>: ValidacaoGenerica<T> where T : ModelGenericoComNome, new()
    {
        private RepositorioGenerico<T> _repositorio { get; set; }

        public new RepositorioGenericoComNome<T> Repositorio
        {
            get
            {
                if(_repositorio == null)
                {
                    _repositorio = FabricaDeRepositorio.Crie<T>();
                }

                return _repositorio as RepositorioGenericoComNome<T>;
            }
        }

        public override void Cadastre(T item)
        {
            if (string.IsNullOrWhiteSpace(item.Nome))
            {
                throw new Exception(ConstantesDeValidacao.O_NOME_DO_ELEMENTO_DEVE_SER_INFORMADO);
            }

            base.Cadastre(item);
        }

        public override void Atualize(T item)
        {
            if (string.IsNullOrWhiteSpace(item.Nome))
            {
                throw new Exception(ConstantesDeValidacao.O_NOME_DO_ELEMENTO_DEVE_SER_INFORMADO);
            }

            base.Atualize(item);
        }

        public virtual void Consulte(string termo)
        {
        }

        public void ConsultePorTermo(string termo)
        {
        }

        public virtual void Liste(int pagina, int elemtosPorPagina)
        {
        }
    }
}
