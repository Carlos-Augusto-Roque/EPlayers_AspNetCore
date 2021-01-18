using System.Collections.Generic;
using E_Players_AspNETCore.Models;

namespace EPlayers_AspNetCore.Interfaces
{
    public interface IJogador
    {
        //Criar
        void Create(Jogador j);
        //Ler
        List<Jogador> ReadAll();
        //Alterar
        void Update(Jogador j);
        //Excluir
        void Delete(int id); 
    }
}