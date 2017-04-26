using static dn32.infraestrutura.Constantes.ConstantesDeValidacao;
using static dn32.infraestrutura.testes.Constantes.ConstantesDeTeste;
using dn32.infraestrutura.Fabrica;
using dn32.infraestrutura.testes.Model;
using System;
using Xunit;
using dn32.infraestrutura.testes.Repositorio;
using dn32.infraestrutura.testes._Genericos;

namespace dn32.infraestrutura.testes.TestesDeServico
{
    public class TesteDeServico : TesteBase<UnidadeDeTeste>
    {
        private UnidadeDeTeste ObtenhaElementoPadrao()
        {
            var unidadeDeTeste = new UnidadeDeTeste
            {
                Descricao = "Descrição de teste",
                Nome = "Nome da unidade de teste",
                Numero = 21
            };

            return unidadeDeTeste;
        }

        [Fact(DisplayName = nameof(TesteCadastroPadrao))]
        public void TesteCadastroPadrao()
        {
            var servico = FabricaDeServico.Crie<UnidadeDeTeste>();

            var unidadeDeTeste = ObtenhaElementoPadrao();
            var codigo = servico.Cadastre(unidadeDeTeste);
            var elementoCadastrado = servico.Consulte(codigo);

            Assert.NotEqual(codigo, 0);
            Assert.NotNull(elementoCadastrado);
            Assert.Equal(elementoCadastrado.Nome, "Nome da unidade de teste");

            servico.Remova(codigo);
        }


        [Fact(DisplayName = nameof(TesteCadastreCustomizado))]
        public void TesteCadastreCustomizado()
        {
            var servico = FabricaDeServico.Crie<UnidadeDeTeste>() as ServicoDeUnidadeDeTeste;

            var unidadeDeTeste = ObtenhaElementoPadrao();
            var codigo = servico.CadastreCustomizado(unidadeDeTeste);

            Assert.NotEqual(codigo, 0);

            var elementoCadastrado = servico.Consulte(codigo);

            Assert.NotNull(elementoCadastrado);
            Assert.Equal(elementoCadastrado.Descricao, "Descrição de teste - Servico-Repositório");

            servico.Remova(codigo);
        }

        [Fact(DisplayName = nameof(TesteCadastroSemNomeErro))]
        public void TesteCadastroSemNomeErro()
        {
            var servico = FabricaDeServico.Crie<UnidadeDeTeste>();
            var unidadeDeTeste = ObtenhaElementoPadrao();
            unidadeDeTeste.Nome = string.Empty;

            var ex = Assert.Throws<Exception>(() =>
            {
                servico.Cadastre(unidadeDeTeste);
            });

            Assert.Equal(ex.Message, O_NOME_DO_ELEMENTO_DEVE_SER_INFORMADO);
        }

        [Theory(DisplayName = nameof(TesteCadastroComNumeroErro))]
        [InlineData(13)]
        [InlineData(90)]
        public void TesteCadastroComNumeroErro(int value)
        {
            var servico = FabricaDeServico.Crie<UnidadeDeTeste>();
            var unidadeDeTeste = ObtenhaElementoPadrao();
            unidadeDeTeste.Numero = value;

            var ex = Assert.Throws<Exception>(() =>
            {
                servico.Cadastre(unidadeDeTeste);
            });

            Assert.Equal(ex.Message, O_NUMERO_DEVE_SEM_MAIOR_QUE_17_E_MENOR_QUE_80);
        }
    }
}
