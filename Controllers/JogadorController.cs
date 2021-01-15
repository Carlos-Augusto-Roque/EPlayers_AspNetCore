using EPlayers_AspNetCore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;


namespace EPlayers_AspNetCore.Controllers
{
    //Rota para acesso do endereço
    [Route("Jogador")]
    // http://localhost:5000/Jogador por exemplo
    public class JogadorController : Controller
    {
        //instanciamento do objeto tipo Jogador
        Jogador jogadorModel = new Jogador();

        [Route("Listar")]

        //método na qual trabalharemos com a página Index
        public IActionResult Index()
        {   
            //Listamos todas os jogadores e enviamos para a View,através da ViewBag
            ViewBag.Jogadores = jogadorModel.ReadAll();
            return View();//retorna uma view,no caso , a Index
        }
        
        [Route("Cadastrar")]

        //método que fará a interação entre a tela(view) e o código desenvolvido
        //receberá as informações do formulário que serão armazenadas dentro de um novo objeto (novoJogador)
        public IActionResult Cadastrar(IFormCollection form)
        {
            Jogador novoJogador = new Jogador();
            novoJogador.IdJogador = Int32.Parse(form["IdJogador"]);
            novoJogador.Nome = form["Nome"];
         

            //solicitado o método Create para salvar o novoJogador no CSV
            jogadorModel.Create(novoJogador);
            //Atualiza a lista de jogadores na view
            ViewBag.Jogadores = jogadorModel.ReadAll();

            return LocalRedirect("~/Jogador/Listar"); //redireciona para a página que se encontra
        }

        //http://localhost:5000/Jogador/1
        [Route("{id}")]
        public IActionResult Excluir(int id)
        {
            jogadorModel.Delete(id);
            ViewBag.Jogadores = jogadorModel.ReadAll();
            
            return LocalRedirect("~/Jogador/Listar");
        }
    }
}