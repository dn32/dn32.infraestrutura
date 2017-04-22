
using dn32.infraestrutura.Contrato;
using dn32.infraestrutura.Model;

namespace dn32.infraestrutura.Generico
{
    public class TesteGenerico<T> where T : IModelGenerico, new()
    {
        public TesteGenerico()
        {
            InicializarInfraestrutura();
        }

        public virtual void InicializarInfraestrutura()
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
