using FuscaFilmes.API.Domain.Entities;
using FuscaFilmes.Repo.Contratos;

namespace FuscaFilmes.API.EndpointHandler;

public static class DiretoresHandlers
{
    public static List<Diretor> GetDiretores(IDiretorRepository diretorRepository)
    {
        return diretorRepository.GetDiretores();
    }

    public static Diretor GetDiretorByName(string name, IDiretorRepository diretorRepository)
    {
        return diretorRepository.GetDiretorByName(name);
    }

    public static List<Diretor> GetDiretorById(int id, IDiretorRepository diretorRepository)
    {
        return diretorRepository.GetDiretorById(id);
    }

    public static void AddDiretor(IDiretorRepository diretorRepository, Diretor diretor)
    {
        diretorRepository.Add(diretor);
        diretorRepository.SaveChanges();
    }

    public static void UpdateDiretor(IDiretorRepository diretorRepository, Diretor diretorNovo)
    {
        diretorRepository.Update(diretorNovo);
        diretorRepository.SaveChanges();
    }
    
    public static void DeleteDiretor(IDiretorRepository diretorRepository, int diretorId)
    {
        diretorRepository.Delete(diretorId);
        diretorRepository.SaveChanges();
    }

}


