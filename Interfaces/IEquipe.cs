using System.Collections.Generic;
using EPlayers_AspNetCore.Models;

namespace EPlayers_AspNetCore.Interfaces
{
    public interface IEquipe
    {
        
        //métodos do CRUD -> (create-read-update-delete)
        void Create(Equipe e);//método para criar uma equipe
        List<Equipe> ReadAll();//método para ler uma lista de equipes
        void Update(Equipe e);//método para alterar uma equipe
        void Delete(int id);//método para deletar uma equipe a partir do id 
    }
}