using System;
using System.Collections.Generic;

namespace dn32.infraestrutura.Util
{
    public class Compartilhado
    {
        public static Dictionary<string, Type> DicionarioDeServico { get; set; }
        public static Dictionary<string, Type> DicionarioDeRepositorio { get; set; }
        public static Dictionary<string, Type> DicionarioDeValidacao { get; set; }
        public static string EnderecoDoBancoDeDados { get; set; }
        public static string EnderecoDeBackupDoBancoDeDados { get; set; }
    }
}
