using System.Collections.Generic;
using System.IO;
using EPlayers_AspNetCore.Interfaces;

namespace EPlayers_AspNetCore.Models
{
    public class Jogador : EPlayersBase,IJogador
    {
        //atributos da classe Jogador
        public int IdJogador { get; set; }
        public string Nome { get; set; }
        public int IdEquipe { get; set; }

        //constante para o caminho da pasta e arquivo 
        private const string PATH = "DataBase/Jogador.csv";

        //método construtor para criar uma pasta e um arquivo
        public Jogador()
        {
            CreateFolderAndFile(PATH);//chamado o método createfolderandfiles da superclasse "EplayersBase"
        }        

        //método para preparar a linha do csv
        public string Prepare(Jogador j)
        {
            return $"{j.IdJogador};{j.Nome};{j.IdEquipe}";
        }

        //métodos obrigatórios (Interface IEquipe) 
        public void Create(Jogador j)//método para criar a equipe
        {
            // preparado um array de string para o método AppendAllLines
            string[] linhas = {Prepare (j)};

            // acrescentado uma nova linha
            File.AppendAllLines(PATH,linhas);
        }

        public List<Jogador> ReadAll()//método para ler as equipes cadastradas
        {
            List<Jogador> jogadores = new List<Jogador>();//lista para armazenar as equipes

            //ler todas as linhas do csv
            string[] linhas = File.ReadAllLines(PATH);

            foreach (string item in linhas)//ler todos os itens nas linhas
            {
                string[] linha = item.Split(";");//separar os itens da linha 

                Jogador novoJogador = new Jogador();//instaciamento de um objeto equipe
                novoJogador.IdJogador = int.Parse(linha[0]);
                novoJogador.Nome = linha[1];
                novoJogador.IdEquipe = int.Parse(linha[2]); 

                jogadores.Add(novoJogador);//adicionar o objeto na lista de equipes
            } 

            return jogadores;
        }

        public void Update(Jogador j)//método para alterar alguma equipe
        {
            List<string> linhas = ReadAllLinesCSV(PATH);

            //Removendo as linhas com o código comparado
            linhas.RemoveAll(x => x.Split(";")[0] == j.IdJogador.ToString());

            //Adicionado na lista a equipe alterada
            linhas.Add(Prepare(j));

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