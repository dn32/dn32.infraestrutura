using dn32.infraestrutura.Fabrica;
using dn32.infraestrutura.Generico;
using dn32.infraestrutura.testes.Model;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace dn32.infraestrutura.testes.TestesDeServico
{
    public class TesteDeServico: TesteGenerico<UnidadeDeTeste>
    {
        [Fact]
        public void TesteCadastro()
        {
            var servico = FabricaDeServico.Crie<UnidadeDeTeste>();

            var unidadeDeTeste = new UnidadeDeTeste
            {
                Descricao = "Descrição de teste",
                Nome = "Nome da unidade de teste",
                Numero = 123
            };

            var codigo = servico.Cadastre(unidadeDeTeste);

            Assert.NotEqual(codigo, 0);
            Assert.Equal(unidadeDeTeste.Nome, "Nome da unidade de teste");

            servico.Remova(codigo);
        }
    }
}
