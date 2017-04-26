using dn32.infraestrutura.Generico;
using dn32.infraestrutura.testes.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace dn32.infraestrutura.testes._Contoller
{
    public class UnidadeDeTesteController : ControladorGenerico<UnidadeDeTeste>
    {
        public ActionResult Cadastro(UnidadeDeTeste unidadeDeTeste)
        {
            Servico.Cadastre(unidadeDeTeste);
            return View();
        }
    }
}
