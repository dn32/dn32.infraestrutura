using Raven.Abstractions.Data;
using Raven.Client;
using dn32.infraestrutura.Banco;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.IO.Compression;
using dn32.infraestrutura.Contrato;
using static dn32.infraestrutura.Compartilhado;
using static dn32.infraestrutura.Utilitarios;

namespace dn32.infraestrutura.Generico
{
    public class RepositorioGenerico<T> where T : IModelGenerico, new()
    {
        private static Contexto Contexto { get; set; }

        public RepositorioGenerico()
        {
            if (Contexto == null)
            {
                Contexto = new Contexto();
            }
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

        public List<T> ConsultePorTermo(string termo)
        {
            using (IDocumentSession session = Contexto.Store.OpenSession())
            {
                return session
                       .Advanced
                       .DocumentQuery<T>()
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
            var temp = $"{ParametrosDeInicializacao.EnderecoDeBackupDoBancoDeDados}{Guid.NewGuid()}";
            Directory.CreateDirectory(temp);
            Contexto.Store
                    .DatabaseCommands
                    .GlobalAdmin
                    .StartBackup(temp, new DatabaseDocument(), incremental: false, databaseName: ParametrosDeInicializacao.NomeDoBancoDeDados)
                    .WaitForCompletion();

            var dir = $"{ParametrosDeInicializacao.EnderecoDeBackupDoBancoDeDados}{ DateTime.Now.ToString("yyyy/MM/dd/")}";
            var arquivoZip = $"{dir}{ DateTime.Now.ToString("HH-mm-ss")}.zip";
            Directory.CreateDirectory(dir);
            ZipFile.CreateFromDirectory(temp, arquivoZip);
            Directory.Delete(temp, true);
        }
    }
}
