using System;
using dn32.infraestrutura.Fabrica;
using dn32.infraestrutura.Constantes;

namespace dn32.infraestrutura.Generico
{
    public class ValidacaoGenerica<T> where T : ModelGenerico, new()
    {
        private RepositorioGenerico<T> _repositorio { get; set; }

        public virtual RepositorioGenerico<T> Repositorio
        {
            get
            {
                if(_repositorio == null)
                {
                    _repositorio = FabricaDeRepositorio.Crie<T>();
                }

                return _repositorio;
            }
        }

        public virtual void Cadastre(T item)
        {
            if (item.Codigo != 0 && Repositorio.Consulte(item.Codigo) != null)
            {
                throw new Exception(ConstantesDeValidacao.JA_EXISTE_UM_ELEMENTO_CADASTRADO_COM_ESSE_CODIGO);
            }
        }

        public virtual void Atualize(T item)
        {
            var existente = Repositorio.Consulte(item.Codigo);
            if (existente != null && item.DataDeCadastro.ToString("MMddyyyyHHmmss") != existente.DataDeCadastro.ToString("MMddyyyyHHmmss"))
            {
                throw new Exception(ConstantesDeValidacao.JA_EXISTE_UM_ELEMENTO_CADASTRADO_COM_ESSE_CODIGO);
            }
        }

        public virtual void Backup()
        {
        }

        public virtual void Salve(T item)
        {
        }

        public virtual void Consulte(int id)
        {
        }

        public virtual void Liste()
        {
        }

        public virtual void Remova(int id)
        {
        }
    }
}
