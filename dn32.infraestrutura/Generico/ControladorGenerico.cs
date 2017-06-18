using Raven.Client;
using System;
using System.Collections.Generic;
using dn32.infraestrutura.Fabrica;
using Microsoft.AspNetCore.Mvc;

namespace dn32.infraestrutura.Generico
{
    public class ControladorGenerico<T> : Controller where T : ModelGenerico, new()
    {
        private ServicoGenerico<T> _servico { get; set; }

        public virtual ServicoGenerico<T> Servico
        {
            get
            {
                if (_servico == null)
                {
                    _servico = FabricaDeServico.Crie<T>();
                }

                return _servico;
            }
        }

        public ControladorGenerico()
        {
            ViewBag.ControllerName = typeof(T).Name;
        }
    }
}
