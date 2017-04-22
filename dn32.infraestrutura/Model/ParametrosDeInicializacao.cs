using System;
using System.Collections.Generic;
using System.Text;

namespace dn32.infraestrutura.Model
{
    public class ParametrosDeInicializacao
    {
        public string EnderecoDoBancoDeDados { get; set; }
        public string NomeDoAssemblyDoRepositorio { get; set; }
        public string NomeDoAssemblyDoServico { get; set; }
        public string NomeDoAssemblyDaValidacao { get; set; }
    }
}
