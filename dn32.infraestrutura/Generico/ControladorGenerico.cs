using Raven.Client;
using System;
using System.Collections.Generic;
using dn32.infraestrutura.Fabrica;
using Microsoft.AspNetCore.Mvc;

namespace dn32.infraestrutura.Generico
{
    public class ControladorGenerico<T> : Controller where T : ModelGenerico, new()
    {
        public ServicoGenerico<T> Servico { get; set; }

        public ControladorGenerico()
        {
            ViewBag.ControllerName = typeof(T).Name;
            Servico = FabricaDeServico.Crie<T>() as ServicoGenerico<T>;
        }
    }
}
