using dn32.infraestrutura.Contrato;
using dn32.infraestrutura.Generico;
using dn32.infraestrutura.Model;
using dn32.infraestrutura.testes.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace dn32.infraestrutura.testes._Genericos
{
    public class TesteBase<T> : TesteGenerico<T> where T : IModelGenerico, new() 
    {
        public override void InicializarInfraestrutura()
        {
            var parametrosDeInicializacao = new ParametrosDeInicializacao
            {
                EnderecoDeBackupDoBancoDeDados = "c:/ravendb-backup",
                EnderecoDoBancoDeDados = "http://localhost:8080",
                NomeDoAssemblyDaValidacao = "dn32.infraestrutura.testes",
                NomeDoAssemblyDoRepositorio = "dn32.infraestrutura.testes",
                NomeDoAssemblyDoServico = "dn32.infraestrutura.testes",
                NomeDoBancoDeDados = "bd-teste"
            };

            Inicializar.Inicialize(parametrosDeInicializacao);
        }
    }
}
