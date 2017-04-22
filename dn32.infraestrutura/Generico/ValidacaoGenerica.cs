using System;
using dn32.infraestrutura.Contrato;
using dn32.infraestrutura.Fabrica;

namespace dn32.infraestrutura.Generico
{
    public class ValidacaoGenerica<T> where T : IModelGenerico, new()
    {
        private RepositorioGenerico<T> _repositorio { get; set; }

        public RepositorioGenerico<T> Repositorio
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

        public virtual void Cadastrar(T item)
        {
            if (item.Codigo != 0 && Repositorio.Consulte(item.Codigo) != null)
            {
                throw new Exception($"Já existe um elemento com o código {item.Codigo} cadastrado.");
            }
        }

        public virtual void Atualizar(T item)
        {
            var existente = Repositorio.Consulte(item.Codigo);
            if (existente != null && item.DataDeCadastro.ToString("MMddyyyyHHmmss") != existente.DataDeCadastro.ToString("MMddyyyyHHmmss"))
            {
                throw new Exception($"Já existe um elemento com o código {item.Codigo} cadastrado.");
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

        public virtual void Consulte(string termo)
        {
        }

        public void ConsultePorTermo(string termo)
        {
        }

        public virtual void Liste()
        {
        }

        public virtual void Liste(int pagina, int elemtosPorPagina)
        {
        }

        public virtual void Remova(int id)
        {
        }
    }
}
