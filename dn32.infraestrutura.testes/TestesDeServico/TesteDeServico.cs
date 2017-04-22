using static dn32.infraestrutura.Constantes.ConstantesDeValidacao;
using static dn32.infraestrutura.testes.Constantes.ConstantesDeTeste;
using dn32.infraestrutura.Fabrica;
using dn32.infraestrutura.Generico;
using dn32.infraestrutura.testes.Model;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace dn32.infraestrutura.testes.TestesDeServico
{
    public class TesteDeServico : TesteGenerico<UnidadeDeTeste>
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

            Assert.NotEqual(codigo, 0);
            Assert.Equal(unidadeDeTeste.Nome, "Nome da unidade de teste");

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

        [Theory(DisplayName = nameof(TesteCadastroComNumeroPequeno))]
        [InlineData(13)]
        [InlineData(90)]
        public void TesteCadastroComNumeroPequeno(int value)
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
