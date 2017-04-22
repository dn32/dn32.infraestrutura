using Raven.Client;
using System;
using System.Collections.Generic;
using dn32.infraestrutura.Contrato;
using dn32.infraestrutura.Fabrica;

namespace dn32.infraestrutura.Generico
{
    public class ServicoGenerico<T> where T : IModelGenerico, new()
    {
        public Type TipoDeEntidade { get; set; }
        private RepositorioGenerico<T> _repositorio { get; set; }
        private ValidacaoGenerica<T> _validacao { get; set; }

        public RepositorioGenerico<T> Repositorio
        {
            get
            {
                if (_repositorio == null)
                {
                    _repositorio = FabricaDeRepositorio.Crie<T>();
                }

                return _repositorio;
            }
        }

        public ValidacaoGenerica<T> Validacao
        {
            get
            {
                if (_validacao == null)
                {
                    _validacao = FabricaDeValidacao.Crie<T>();
                }

                return _validacao;
            }
        }

        public ServicoGenerico()
        {
            TipoDeEntidade = typeof(T);
        }

        public virtual int Cadastrar(T item)
        {
            Validacao.Cadastrar(item);
            return Repositorio.Cadastrar(item);
        }

        public virtual int Salvar(T item)
        {
            Validacao.Salve(item);
            return Repositorio.Salve(item, null);
        }

        public void Backup()
        {
            Validacao.Backup();
            Repositorio.Backup();
        }

        public virtual int Atualizar(T item)
        {
            Validacao.Atualizar(item);
            return Repositorio.Salve(item, false);
        }

        public virtual T Consulte(int codigo)
        {
            Validacao.Consulte(codigo);
            return Repositorio.Consulte(codigo);
        }

        public virtual T Consulte(string termo)
        {
            Validacao.Consulte(termo);
            return Repositorio.Consulte(termo);
        }

        public List<T> ConsultePorTermo(string termo)
        {
            Validacao.ConsultePorTermo(termo);
            return Repositorio.ConsultePorTermo(termo);
        }

        public virtual List<T> Liste()
        {
            Validacao.Liste();
            return Repositorio.Liste();
        }

        public virtual List<T> Liste(int pagina, int elemtosPorPagina, out RavenQueryStatistics estatisticas)
        {
            Validacao.Liste(pagina, elemtosPorPagina);
            return Repositorio.Liste(pagina, elemtosPorPagina, out estatisticas);
        }

        public virtual void Remova(int codigo)
        {
            Validacao.Remova(codigo);
            Repositorio.Remova(codigo);
        }
    }
}