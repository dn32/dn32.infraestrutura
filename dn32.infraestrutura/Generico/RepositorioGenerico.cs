using Raven.Abstractions.Data;
using Raven.Client;
using dn32.infraestrutura.Banco;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static dn32.infraestrutura.Utilitarios;
using System.IO.Compression;
using dn32.infraestrutura.Contrato;
using static dn32.infraestrutura.Util.Compartilhado;

namespace dn32.infraestrutura.Generico
{
    public class RepositorioGenerico<T> where T : IModelGenerico, new()
    {
        public Contexto Contexto { get; set; }

        public RepositorioGenerico(Contexto Contexto)
        {
            this.Contexto = Contexto;
        }

        public virtual int Cadastrar(T item)
        {
            if (item.Codigo == 0)
            {
                item.Id = null;
            }
            return Salve(item, true);
        }

        public virtual int Atualizar(T item)
        {
            return Salve(item, false);
        }

        public virtual int Salve(T item, bool? novo)
        {
            using (IDocumentSession session = Contexto.Store.OpenSession())
            {
                if (novo == true || novo == null)
                {
                    item.DataDeCadastro = DateTime.Now;
                }

                item.DataDeAtualizacao = DateTime.Now;

                session.Store(item);
                session.SaveChanges();
            }

            return item.Codigo;
        }

        public virtual T Consulte(int codigo)
        {
            using (IDocumentSession session = Contexto.Store.OpenSession())
            {
                return session.Load<T>(ObtenhaIdDoElemento<T>(codigo));
            }
        }

        public virtual T Consulte(string termo)
        {
            throw new NotImplementedException();
        }

        public List<TNome> ConsultePorTermo<TNome>(string termo) where TNome : IModelGenerico, new()
        {
            using (IDocumentSession session = Contexto.Store.OpenSession())
            {
                return session
                       .Advanced
                       .DocumentQuery<TNome>()
                       .Where($"{nameof(IModelGenerico.Nome)}:{termo}")
                       .OrderBy(x => x.Nome)
                       .ToList();
            }
        }

        public virtual List<T> Liste()
        {
            using (IDocumentSession session = Contexto.Store.OpenSession())
            {
                return session.Query<T>()
                       .OrderBy(x => x.Codigo)
                       .ToList();
            }
        }

        public virtual List<TNome> Liste<TNome>(int pagina, int elemtosPorPagina, out RavenQueryStatistics estatisticas) where TNome : IModelGenerico, new()
        {
            var quantidadeAPular = (pagina - 1) * elemtosPorPagina;
            if (elemtosPorPagina == 0)
            {
                elemtosPorPagina = 10;
            }

            using (IDocumentSession session = Contexto.Store.OpenSession())
            {
                return session
                    .Query<TNome>()
                    .Statistics(out estatisticas)
                    .OrderBy(x => x.Nome)
                    .Skip(quantidadeAPular)
                    .Take(elemtosPorPagina)
                    .ToList();
            }
        }

        public virtual void Remova(int codigo)
        {
            using (IDocumentSession session = Contexto.Store.OpenSession())
            {
                session.Delete(ObtenhaIdDoElemento<T>(codigo));
                session.SaveChanges();
            }
        }

        public void Backup()
        {
            var temp = $"{EnderecoDeBackupDoBancoDeDados}{Guid.NewGuid()}";
            Directory.CreateDirectory(temp);
            Contexto.Store
                    .DatabaseCommands
                    .GlobalAdmin
                    .StartBackup(temp, new DatabaseDocument(), incremental: false, databaseName: "dn32")
                    .WaitForCompletion();

            var dir = $"{EnderecoDeBackupDoBancoDeDados}{ DateTime.Now.ToString("yyyy/MM/dd/")}";
            var arquivoZip = $"{dir}{ DateTime.Now.ToString("HH-mm-ss")}.zip";
            Directory.CreateDirectory(dir);
            ZipFile.CreateFromDirectory(temp, arquivoZip);
            Directory.Delete(temp, true);
        }
    }
}
