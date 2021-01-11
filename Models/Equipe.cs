using System.Collections.Generic;
using System.IO;
using EPlayers_AspNetCore.Interfaces;

namespace EPlayers_AspNetCore.Models
{
    public class Equipe : EPlayersBase , IEquipe //classe "Equipe" pode herdar da superclasse "EplayerBase" e da interface "IEquipe"
    {
        //atributos para classe Equipe
        public int IdEquipe { get; set; }
        public string Nome { get; set; }
        public string Imagem { get; set; }

        //constante para o caminho da pasta e arquivo 
        private const string PATH = "DataBase/Equipe.csv";

        //método construtor para criar uma pasta e um arquivo
        public Equipe()
        {
            CreateFolderAndFile(PATH);//chamado o método createfolderandfiles da superclasse "EplayersBase"
        }        

        //método para preparar a linha do csv
        public string Prepare(Equipe e)
        {
            return $"{e.IdEquipe};{e.Nome};{e.Imagem}";
        }

        //métodos obrigatórios (Interface IEquipe) 
        public void Create(Equipe e)//método para criar a equipe
        {
            // preparado um array de string para o método AppendAllLines
            string[] linhas = {Prepare (e)};

            // acrescentado uma nova linha
            File.AppendAllLines(PATH,linhas);
        }

        public List<Equipe> ReadAll()//método para ler as equipes cadastradas
        {
            List<Equipe> equipes = new List<Equipe>();//lista para armazenar as equipes

            //ler todas as linhas do csv
            string[] linhas = File.ReadAllLines(PATH);

            foreach (string item in linhas)//ler todos os itens nas linhas
            {
                string[] linha = item.Split(";");//separar os itens da linha 

                Equipe novaEquipe = new Equipe();//instaciamento de um objeto equipe
                novaEquipe.IdEquipe = int.Parse(linha[0]);
                novaEquipe.Nome = linha[1];
                novaEquipe.Imagem = linha[2]; 

                equipes.Add(novaEquipe);//adicionar o objeto na lista de equipes
            } 

            return equipes;
        }

        public void Update(Equipe e)//método para alterar alguma equipe
        {
            List<string> linhas = ReadAllLinesCSV(PATH);

            //Removendo as linhas com o código comparado
            linhas.RemoveAll(x => x.Split(";")[0] == e.IdEquipe.ToString());

            //Adicionado na lista a equipe alterada
            linhas.Add(Prepare(e));

            //Reescreve o csv com a lista alterada
            RewriteCSV(PATH,linhas);
        }
        public void Delete(int id)//método para deletar uma equipe
        {
             List<string> linhas = ReadAllLinesCSV(PATH);

            //Removendo as linhas com o código comparado
            //ToString -> converte para texto (string)
            linhas.RemoveAll(x => x.Split(";")[0] == id.ToString());

            //Reescreve o csv com a lista alterada
            RewriteCSV(PATH,linhas);          

        } 
    }
}