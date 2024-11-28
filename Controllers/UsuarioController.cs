using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Bibliotec.Contexts;
using Bibliotec.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Bibliotec_mvc.Controllers
{
    [Route("[controller]")]
    public class UsuarioController : Controller
    {
        private readonly ILogger<UsuarioController> _logger;

        public UsuarioController(ILogger<UsuarioController> logger)
        {
            _logger = logger;
        }
        Context context = new Context();

        public IActionResult Index()
        {
            int Id = int.Parse(HttpContext.Session.GetString("UsuarioID"));
            ViewBag.Admin = HttpContext.Session.GetString("Admin");
        

            Usuario usuarioEncontrado = context.Usuario.FirstOrDefault(Usuario => Usuario.UsuarioID == Id)!;

            if (usuarioEncontrado == null)
            {
                return NotFound();
            }
            Curso cursoEncontrado = context.Curso.FirstOrDefault(Curso => Curso.CursoID == usuarioEncontrado.CursoID)!;

            if (cursoEncontrado == null ){
               ViewBag.Curso = "O Usuario nao possui curso";
                
            }else{
                
                 ViewBag.Curso = cursoEncontrado.Nome;
            }

            ViewBag.Nome = usuarioEncontrado.Nome;
            ViewBag.Email = usuarioEncontrado.Email;
            ViewBag.Contato = usuarioEncontrado.Contato;
            ViewBag.Data = usuarioEncontrado.DtNascimento.ToString("dd/MM/yyyy");
            return View();
        }

        // [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        // public IActionResult Error()
        // {
        //     return View("Error!");
        // }
    }
}