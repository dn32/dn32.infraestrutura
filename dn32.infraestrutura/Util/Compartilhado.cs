using dn32.infraestrutura.Model;
using System;
using System.Collections.Generic;

namespace dn32.infraestrutura
{
    internal class Compartilhado
    {
        internal static Dictionary<string, Type> DicionarioDeServico { get; set; }
        internal static Dictionary<string, Type> DicionarioDeRepositorio { get; set; }
        internal static Dictionary<string, Type> DicionarioDeValidacao { get; set; }
        internal static ParametrosDeInicializacao ParametrosDeInicializacao { get; set; }
        internal static bool Inicializado { get; set; }
        internal static object TravaDeInicializacao { get; set; } = new object();
    }
}
