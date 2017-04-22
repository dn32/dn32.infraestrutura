using dn32.infraestrutura.Model;
using System;
using System.Collections.Generic;

namespace dn32.infraestrutura
{
    public class Compartilhado
    {
        public static Dictionary<string, Type> DicionarioDeServico { get; set; }
        public static Dictionary<string, Type> DicionarioDeRepositorio { get; set; }
        public static Dictionary<string, Type> DicionarioDeValidacao { get; set; }
        public static ParametrosDeInicializacao ParametrosDeInicializacao { get; set; }
    }
}
