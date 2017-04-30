using dn32.infraestrutura.Generico;
using System;
using System.Collections.Generic;
using System.Text;

namespace dn32.infraestrutura.Constantes
{
    public static class ConstantesDeValidacao
    {
        public static string O_NOME_DO_ELEMENTO_DEVE_SER_INFORMADO = $"O {nameof(ModelGenericoComNome.Nome)} do {typeof(ModelGenerico).Name} deve ser iformado.";
        public static string JA_EXISTE_UM_ELEMENTO_CADASTRADO_COM_ESSE_CODIGO = $"Já existe um elemento {nameof(ModelGenerico.Codigo)} cadastrado com o código.";
    }
}
