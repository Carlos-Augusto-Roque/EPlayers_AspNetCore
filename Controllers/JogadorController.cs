using EPlayers_AspNetCore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;


namespace EPlayers_AspNetCore.Controllers
{
    //Rota para acesso do endereço
    [Route("Jogador")]
    // http://localhost:5000/Equipe por exemplo
    public class JogadorController : Controller
    {
        //instanciamento do objeto tipo Equipe
        Jogador jogadorModel = new Jogador();

        [Route("Listar")]

        //método na qual trabalharemos com a página Index
        public IActionResult Index()
        {   
            //Listamos todas as equipes e enviamos para a View,através da ViewBag
            ViewBag.Equipes = jogadorModel.ReadAll();
            return View();//retorna uma view,no caso , a Index
        }
        
        [Route("Cadastrar")]

        //método que fará a interação entre a tela(view) e o código desenvolvido
        //receberá as informações do formulário que serão armazenadas dentro de um novo objeto (novaEquipe)
        public IActionResult Cadastrar(IFormCollection form)
        {
            Jogador novoJogador = new Jogador();
            novoJogador.IdJogador = Int32.Parse(form["IdJogador"]);
            novoJogador.Nome = form["Nome"];
         

            //solicitado o método Create para salvar a novaEquipe no CSV
            jogadorModel.Create(novoJogador);
            //Atualiza a lista de equipes na view
            ViewBag.Equipes = jogadorModel.ReadAll();

            return LocalRedirect("~/Jogador/Listar"); //redireciona para a página que se encontra
        }

        //http://localhost:5000/Equipe/1
        [Route("{id}")]
        public IActionResult Excluir(int id)
        {
            jogadorModel.Delete(id);
            ViewBag.Equipes = jogadorModel.ReadAll();
            
            return LocalRedirect("~/Jogador/Listar");
        }
    }
}