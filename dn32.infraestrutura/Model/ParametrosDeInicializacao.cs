using System;
using System.Collections.Generic;
using System.Text;

namespace dn32.infraestrutura.Model
{
    public class ParametrosDeInicializacao
    {
        public string NomeDoAssemblyDoRepositorio { get; set; }
        public string NomeDoAssemblyDoServico { get; set; }
        public string NomeDoAssemblyDaValidacao { get; set; }
        public string EnderecoDeBackupDoBancoDeDados { get; set; }
        public string EnderecoDoBancoDeDados { get; set; }
        public string NomeDoBancoDeDados { get; internal set; }
    }
}
