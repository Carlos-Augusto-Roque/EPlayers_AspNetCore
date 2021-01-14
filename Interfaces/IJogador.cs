using System.Collections.Generic;
using EPlayers_AspNetCore.Models;

namespace EPlayers_AspNetCore.Interfaces
{
    public interface IJogador
    {
          //métodos do CRUD -> (create-read-update-delete)
        void Create(Jogador j);//método para criar uma equipe
        List<Jogador> ReadAll();//método para ler uma lista de equipes
        void Update(Jogador j);//método para alterar uma equipe
        void Delete(int id);//método para deletar uma equipe a partir do id 
    }
}