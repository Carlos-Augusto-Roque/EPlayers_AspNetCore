using System;
using EPlayers_AspNetCore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EPlayers_AspNetCore.Controllers
{
    //Rota para acesso do endereço
    [Route("Equipe")]
    // http://localhost:5000/Equipe por exemplo
    public class EquipeController : Controller
    {
        //instanciamento do objeto tipo Equipe
        Equipe equipeModel = new Equipe();

        [Route("Listar")]

        //método na qual trabalharemos com a página Index
        public IActionResult Index()
        {   
            //Listamos todas as equipes e enviamos para a View,através da ViewBag
            ViewBag.Equipes = equipeModel.ReadAll();
            return View();//retorna uma view,no caso , a Index
        }
        
        [Route("Cadastrar")]

        //método que fará a interação entre a tela(view) e o código desenvolvido
        //receberá as informações do formulário que serão armazenadas dentro de um novo objeto (novaEquipe)
        public IActionResult Cadastrar(IFormCollection form)
        {
            Equipe novaEquipe = new Equipe();
            novaEquipe.IdEquipe = Int32.Parse(form["IdEquipe"]);
            novaEquipe.Nome = form["Nome"];
            novaEquipe.Imagem = form["Imagem"];

            //solicitado o método Create para salvar a novaEquipe no CSV
            equipeModel.Create(novaEquipe);
            //Atualiza a lista de equipes na view
            ViewBag.Equipes = equipeModel.ReadAll();

            return LocalRedirect("~/Equipe/Listar"); //redireciona para a página que se encontra
        }
    }
}