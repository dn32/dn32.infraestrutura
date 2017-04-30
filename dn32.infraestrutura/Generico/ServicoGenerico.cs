using Raven.Client;
using System;
using System.Collections.Generic;
using dn32.infraestrutura.Fabrica;

namespace dn32.infraestrutura.Generico
{
    public class ServicoGenerico<T> where T : ModelGenerico, new()
    {
        public Type TipoDeEntidade { get; set; }
        protected RepositorioGenerico<T> _repositorio { get; set; }
        protected ValidacaoGenerica<T> _validacao { get; set; }

        public virtual RepositorioGenerico<T> Repositorio
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

        public virtual ValidacaoGenerica<T> Validacao
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

        public virtual int Cadastre(T item)
        {
            Validacao.Cadastre(item);
            return Repositorio.Cadastre(item);
        }

        public virtual int Salve(T item)
        {
            Validacao.Salve(item);
            return Repositorio.Atualize(item, null);
        }

        public void Backup()
        {
            Validacao.Backup();
            Repositorio.Backup();
        }

        public virtual int Atualize(T item)
        {
            Validacao.Atualize(item);
            return Repositorio.Atualize(item, false);
        }

        public virtual T Consulte(int codigo)
        {
            Validacao.Consulte(codigo);
            return Repositorio.Consulte(codigo);
        }

        public virtual List<T> Liste()
        {
            Validacao.Liste();
            return Repositorio.Liste();
        }

        public virtual void Remova(int codigo)
        {
            Validacao.Remova(codigo);
            Repositorio.Remova(codigo);
        }
    }
}