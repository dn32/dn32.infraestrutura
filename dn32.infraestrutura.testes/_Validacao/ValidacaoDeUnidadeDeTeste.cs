using dn32.infraestrutura.Atributos;
using dn32.infraestrutura.Generico;
using dn32.infraestrutura.testes.Constantes;
using dn32.infraestrutura.testes.Model;
using System;

namespace dn32.infraestrutura.testes.Repositorio
{
    [ValidacaoDe(typeof(UnidadeDeTeste))]
    public class ValidacaoDeUnidadeDeTeste : ValidacaoGenerica<UnidadeDeTeste>
    {
        public override void Cadastre(UnidadeDeTeste item)
        {
            if(item.Numero < 17 || item.Numero > 80)
            {
                throw new Exception(ConstantesDeTeste.O_NUMERO_DEVE_SEM_MAIOR_QUE_17_E_MENOR_QUE_80);
            }

            base.Cadastre(item);
        }
        public void CadastreCustomizado(UnidadeDeTeste unidadeDeteste)
        {

        }
    }
}
