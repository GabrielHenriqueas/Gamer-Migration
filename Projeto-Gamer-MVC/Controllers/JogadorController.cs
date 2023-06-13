using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Projeto_Gamer_MVC.Infra;
using Projeto_Gamer_MVC.Models;

namespace Projeto_Gamer_MVC.Controllers
{
    [Route("[controller]")]
    public class JogadorController : Controller
    {
        private readonly ILogger<JogadorController> _logger;

        public JogadorController(ILogger<JogadorController> logger)
        {
            _logger = logger;
        }

        Context c = new Context();

        [Route("Listar")]
        public IActionResult Index()
        {
            ViewBag.UserName = HttpContext.Session.GetString("UserName");

            if (ViewBag.UserName == null)
            {
                return LocalRedirect("~/Login/Login");
            }
            else
            {
                ViewBag.UserName = HttpContext.Session.GetString("UserName");
                ViewBag.Jogador = c.Jogador.ToList();
                ViewBag.Equipe = c.Equipe.ToList();

                return View();
            }
        }

        [Route("Excluir/{id}")]
        public IActionResult Excluir(int id)
        {
            Jogador jogadorBuscado = c.Jogador.First(j => j.IdJogador == id);

            c.Jogador.Remove(jogadorBuscado);
            c.SaveChanges();
            return LocalRedirect("~/Jogador/Listar");
        }

        [Route("Editar")]
        public IActionResult Editar(int id)
        {
            ViewBag.UserName = HttpContext.Session.GetString("UserName");

            Jogador jogadorBuscado = c.Jogador.First(j => j.IdJogador == id);

            ViewBag.Jogador = jogadorBuscado;
            ViewBag.Equipe = c.Equipe.ToList();

            if (ViewBag.UserName == null)
            {
                return LocalRedirect("~/Login/Login");
            }
            else
            {
                return View("Edit");
            }
        }

        [Route("Atualizar")]
        public IActionResult Atualizar(IFormCollection form)
        {
            Jogador novoJogador = new Jogador();

            novoJogador.IdJogador = int.Parse(form["IdJogador"].ToString());
            novoJogador.Nome = form["Nome"].ToString();
            novoJogador.Email = form["Email"].ToString();
            novoJogador.Senha = form["Senha"].ToString();
            novoJogador.IdEquipe = int.Parse(form["IdEquipe"].ToString());

            Jogador jogadorBuscado = c.Jogador.First(j => j.IdJogador == novoJogador.IdJogador);

            jogadorBuscado.Nome = novoJogador.Nome;
            jogadorBuscado.Email = novoJogador.Email;
            jogadorBuscado.Senha = novoJogador.Senha;
            jogadorBuscado.IdEquipe = novoJogador.IdEquipe;

            c.Jogador.Update(jogadorBuscado);

            c.SaveChanges();

            return LocalRedirect("~/Jogador/Listar");
        }

        [Route("Cadastrar")]
        public IActionResult Cadastrar(IFormCollection form)
        {
            Jogador novoJogador = new Jogador();

            novoJogador.Nome = form["Nome"].ToString();
            novoJogador.Email = form["Email"].ToString();
            novoJogador.Senha = form["Senha"].ToString();
            novoJogador.IdEquipe = int.Parse(form["IdEquipe"].ToString());

            c.Jogador.Add(novoJogador);

            c.SaveChanges();

            return LocalRedirect("~/Jogador/Listar");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}