using Raven.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dn32.infraestrutura.Contrato;

namespace dn32.infraestrutura.Generico
{
    public class ValidacaoGenerica<T> where T : IModelGenerico, new()
    {
        public RepositorioGenerico<T> Repositorio { get; set; }
        public Type TipoDeEntidade { get; set; }

        public ValidacaoGenerica(RepositorioGenerico<T> repositorio)
        {
            TipoDeEntidade = typeof(T);
            Repositorio = repositorio;
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

        public virtual void Salve(T item)
        {
            //if(item.Codigo == 0)
            //{
            //    throw new Exception("Código não pode ser 0");
            //}
        }

        public virtual void Consulte(int id)
        {
        }

        public virtual void Consulte(string termo)
        {
            if(string.IsNullOrWhiteSpace(termo))
            {
                throw new Exception("Não é permitido termo vazio");
            }
        }

        public void ConsultePorTermo<TNome>(string termo) where TNome : IModelGenerico, new()
        {
        }

        public virtual void Liste()
        {
        }

        public virtual void Liste<TNome>(int pagina, int elemtosPorPagina) where TNome : IModelGenerico, new()
        {
        }

        public virtual void Remova(int id)
        {
        }
    }
}
