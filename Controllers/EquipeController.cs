using System;
using System.IO;
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

            // Upload início

            //verificado se o usuário selecionou um arquivo (imagem)
            if(form.Files.Count >0)
            {
                //Se usuário selecionou um arquivo,então recebe-se e armazena na variável file
                var file = form.Files[0];
                var folder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/Equipes");

                //verifica se o diretório (pasta) já existe
                //caso não exista,então cria-se
                if(!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }
                                        //localhost:5001                                Equipes  imagem.jpg
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/", folder,file.FileName);
                
                using(var stream = new FileStream(path,FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                novaEquipe.Imagem = file.FileName;


            }
            else//se o usuário não selecionou nenhuma imagem,o sistema fornecerá a imagem padrão
            {
                novaEquipe.Imagem = "padrao.png";
            }

            //Upload final

            //solicitado o método Create para salvar a novaEquipe no CSV
            equipeModel.Create(novaEquipe);
            //Atualiza a lista de equipes na view
            ViewBag.Equipes = equipeModel.ReadAll();

            return LocalRedirect("~/Equipe/Listar"); //redireciona para a página que se encontra
        }

        //http://localhost:5000/Equipe/1
        [Route("{id}")]
        public IActionResult Excluir(int id)
        {
            equipeModel.Delete(id);
            ViewBag.Equipes = equipeModel.ReadAll();
            
            return LocalRedirect("~/Equipe/Listar");
        }
    }
}